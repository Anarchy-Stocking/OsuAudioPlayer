using OsuData;
using System.Windows.Forms;
using System.Linq;
using OsuData.FileExplainer;
using NAudio;
using NAudio.Wave;
using System.Windows.Input;
using StockingNAudio.Track;
using System.Drawing.Printing;
namespace OsuAudioPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        List<SingleSong> singleSongList = [];
        private void Form1_Load(object sender, EventArgs e)
        {
            SongPack songPack = new(@"E:\SmallGame\osu");
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
            #region loadListView2
            // Set the initial sorting type for the ListView.
            this.listView2.Sorting = SortOrder.None;
            // Disable automatic sorting to enable manual sorting.
            this.listView2.View = View.Details;
            // Add columns and set their text.
            this.listView2.Columns.Add(new ColumnHeader());
            this.listView2.Columns[0].Text = "Version";
            this.listView2.Columns[0].Width = 400;
            //listView1.Columns.Add(new ColumnHeader());
            //listView1.Columns[1].Text = "Artist";
            //this.listView1.Columns[1].Width = 100;
            // Suspend control logic until form is done configuring form.
            this.SuspendLayout();
            // Add Items to the ListView control.
            #endregion

            foreach (var dir in songPack.songDirs)
            {
                singleSongList.Add(new SingleSong(dir));
            }

            foreach (var singleSong in singleSongList)
            {
                singleSong.GenRandomSelector();
                singleSong.readInformationFromDotOsu();
                ListViewItem listViewItem =
                    new ListViewItem(new string[]
                    {
                        singleSong.title ?? "null",
                        singleSong.artist ?? "null"
                    },
                    -1, Color.Empty, Color.BurlyWood, null);
                // cell click event
                listView1.ItemSelectionChanged += ListView1_ItemCheck;
                this.listView1.Items.Add(listViewItem);
            }
        }
        private int chosedIndex = 0;
        private void ListView1_ItemCheck(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.ItemIndex != chosedIndex && e.IsSelected)
            {
                chosedIndex = e.ItemIndex;
                GetSelectedSong();
            }
        }
        private void GetSelectedSong()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Selected = false;
            }
            listView1.Items[chosedIndex].Selected = true;
            if (textBox1.Text != listView1.Items[chosedIndex].Text)
            {
                textBox1.Text = listView1.Items[chosedIndex].Text;
                var ienuSelectedSong =
                    from singleSong in singleSongList
                    where singleSong.title == listView1.Items[chosedIndex].Text
                    select singleSong;
                var listSelectedSong = ienuSelectedSong.ToList();
                this.listView2.Items.Clear();
                foreach (var dotosu in listSelectedSong[0].dotOsus)
                {
                    ListViewItem listViewItem =
                    new ListViewItem(new string[]
                    {
                        dotosu.Name
                    },
                    -1, Color.Empty, Color.BurlyWood, null);
                    this.listView2.Items.Add(listViewItem);
                }
                this.listView2.ItemSelectionChanged += ListView2_ItemSelectionChanged;

                // temp codes
                var pathOsu = listSelectedSong[0].dotOsus[0];
                DotOsuReader reader = new(pathOsu);
                //MessageBox.Show(reader.imgDir);
                pictureBox1.Image = new Bitmap(reader.imgDir);
                tmpPathOsu = pathOsu.ToString();
            }
        }
        String recorder;
        String tmpPathOsu;
        private void ListView2_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (recorder != e.Item.Text && e.IsSelected)
            {
                recorder = e.Item.Text;
                var ienuSelectedSong =
                    from singleSong in singleSongList
                    where singleSong.title == textBox1.Text
                    select singleSong;
                var listSelectedSong = ienuSelectedSong.ToList();
                var pathOsu = listSelectedSong[0].address + @"\" + e.Item.Text;
                DotOsuReader reader = new(pathOsu);
                //MessageBox.Show(reader.imgDir);
                pictureBox1.Image = new Bitmap(reader.imgDir);
                tmpPathOsu = pathOsu;
            }
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        bool trackOccupied { set; get; } = false;
        Track track;
        private void Play()
        {
            DotOsuReader osuReader = new(tmpPathOsu);
            if (!trackOccupied)
            {
                track = new(osuReader.audioDir);
                track.Init();
                track.Play();
                trackOccupied = true;
            }
            else
            {
                track.Stop();
                track.Dispose();
                track = new(osuReader.audioDir);
                track.Init();
                track.Play();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Play();
        }
        
        /// <summary>
        /// shortcut key
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                Play();
                return true;
            }
            if (keyData == (Keys.Control | Keys.D))
            {
                chosedIndex = chosedIndex == listView1.Items.Count - 1 ? 0 : chosedIndex + 1;
                GetSelectedSong();
                Play();
                return true;
            }
            if (keyData == (Keys.Control | Keys.A))
            {
                chosedIndex = chosedIndex == 0 ? singleSongList.Count - 1 : chosedIndex - 1;
                GetSelectedSong();
                Play();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
