using OsuData;
using System.Windows.Forms;
using System.Linq;
using OsuData.FileExplainer;
using NAudio;
using NAudio.Wave;
using System.Windows.Input;
using System.Drawing.Printing;
using StockingNAudio.StockingSampleProvider;
using StockingToolKit;

namespace OsuAudioPlayer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.KeyPreview = true;
            device = new WaveOutEvent();
        }
        OsuDataManager MainManager = new(@"E:\SmallGame\osu", @"C:\Users\Alyce\Desktop\test");
        List<string> songsList = new();
        string selectedRecordFile = "default";
        void GenSongsList()
        {
            chosedIndex = 0;
            if (selectedRecordFile != "default")
                songsList = MainManager.GetClassifiedSongs(selectedRecordFile);
            else
                songsList = MainManager.DotOsuFileInfoDictionary.Keys.ToList();
            listView1.Items.Clear();
            
            songsList.Randomize();
            
            foreach (var songName in songsList)
            {
                DotOsuReader osuReader = new(MainManager.DotOsuFileInfoDictionary[songName]);
                ListViewItem listViewItem =
                    new ListViewItem(new string[]
                    {
                        osuReader.title ?? "null",
                        osuReader.artist ?? "null"
                    },
                    -1, Color.Empty, Color.Snow, null);
                // cell click event
                listView1.ItemSelectionChanged += ListView1_ItemCheck;
                this.listView1.Items.Add(listViewItem);
            }
        }

        void SetSelectedRecordFile(string name)
        {
            selectedRecordFile = name;
            GenSongsList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            #region loadListView1
            // Set the initial sorting type for the ListView.
            this.listView1.Sorting = SortOrder.None;
            // Disable automatic sorting to enable manual sorting.
            this.listView1.View = View.Details;
            // Add columns and set their text.
            this.listView1.Columns.Add(new ColumnHeader());
            this.listView1.Columns[0].Text = "Title";
            this.listView1.Columns[0].Width = 350;
            listView1.Columns.Add(new ColumnHeader());
            listView1.Columns[1].Text = "Artist";
            this.listView1.Columns[1].Width = 100;
            // Suspend control logic until form is done configuring form.
            this.SuspendLayout();
            // Add Items to the ListView control.
            #endregion
            GenSongsList();
            GetSelectedSong();
            Play();
            processingTask = Task.Run(() => AutoPlayNext());
        }
        int chosedIndex = 0;
        void ListView1_ItemCheck(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex != chosedIndex && e.IsSelected)
            {
                chosedIndex = e.ItemIndex;
                GetSelectedSong();
            }
        }
        string? selectedSongName;
        private void GetSelectedSong()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Selected = false;
            }
            listView1.Items[chosedIndex].Selected = true;
            if (selectedSongName != listView1.Items[chosedIndex].Text)
            {
                selectedSongName = listView1.Items[chosedIndex].Text;
                DotOsuReader dotOsuSelected = new(MainManager.DotOsuFileInfoDictionary[listView1.Items[chosedIndex].Text]);
                //MessageBox.Show(reader.imgDir);
                pictureBox1.Image = new Bitmap(dotOsuSelected.imgDir);
                tmpPathOsu = MainManager.DotOsuFileInfoDictionary[listView1.Items[chosedIndex].Text].ToString();
            }
        }
        String recorder;
        String tmpPathOsu;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        bool trackOccupied { set; get; } = false;
        WaveOutEvent device;
        StockingWave waveProvider;
        private void Play()
        {
            DotOsuReader osuReader = new(tmpPathOsu);
            if (!trackOccupied)
            {
                trackOccupied = true;
                waveProvider = new(new AudioFileReader(osuReader.audioDir));
                device.Init(waveProvider);
                device.Play();
            }
            else
            {
                device.Stop();
                waveProvider = new(new AudioFileReader(osuReader.audioDir));
                device.Init(waveProvider);
                device.Play();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Play();
        }
        
        bool autoPlay = true;
        private Task? processingTask;
        private void AutoPlayNext()
        {
            while (autoPlay)
            {
                if(waveProvider.PlayEnds)
                {
                    chosedIndex = chosedIndex == listView1.Items.Count - 1 ? 0 : chosedIndex + 1;
                    GetSelectedSong();
                    Play();
                }
                else
                {
                    //MessageBox.Show("Checking");
                    // do nothing
                }
            }
        }

        /// <summary>
        /// shortcut key
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.W))
            {
                // play
                Play();
            }
            if (keyData == (Keys.Control | Keys.D))
            {
                // next song
                chosedIndex = 
                    chosedIndex == listView1.Items.Count - 1 ? 
                    0 : chosedIndex + 1;
                GetSelectedSong();
                Play();
            }
            if (keyData == (Keys.Control | Keys.A))
            {
                // previous song
                chosedIndex = 
                    chosedIndex == 0 ? 
                    listView1.Items.Count - 1 : chosedIndex - 1;
                GetSelectedSong();
                Play();
            }
            if(keyData == (Keys.Control | Keys.S))
            {
                // stop
                if (trackOccupied)
                {
                    device.Stop();
                    trackOccupied = false;
                } 
            }
            if(keyData == (Keys.Control | Keys.P))
            {
                // hide controls
                foreach(Control ctrl in this.Controls)
                {
                    if(ctrl != pictureBox1)
                        ctrl.Hide();
                }
            }
            if(keyData == (Keys.Control | Keys.O))
            {
                // show controls
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Show();
                }
            }
            if(keyData == (Keys.Control | Keys.I))
            {
                // add to loved songs
                MainManager.AddSongToMark(listView1.Items[chosedIndex].Text);
                // MessageBox.Show($"Add {listView1.Items[chosedIndex].Text}");
            }
            if(keyData == (Keys.Control | Keys.M))
            {
                // change cast list to loved
                if (selectedRecordFile != "loved")
                    SetSelectedRecordFile("loved");
                else
                    SetSelectedRecordFile("default");
            }
            

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
