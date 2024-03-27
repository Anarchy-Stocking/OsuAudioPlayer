using OsuData;
using System.Windows.Forms;
namespace OsuAudioPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SongPack songPack = new(@"E:\SmallGame\osu");
            #region loadList
            // Set the initial sorting type for the ListView.
            this.listView1.Sorting = SortOrder.None;
            // Disable automatic sorting to enable manual sorting.
            this.listView1.View = View.Details;
            // Add columns and set their text.
            this.listView1.Columns.Add(new ColumnHeader());
            this.listView1.Columns[0].Text = "Title";
            this.listView1.Columns[0].Width = 500;
            listView1.Columns.Add(new ColumnHeader());
            listView1.Columns[1].Text = "Artist";
            this.listView1.Columns[1].Width = 100;
            // Suspend control logic until form is done configuring form.
            this.SuspendLayout();
            // Add Items to the ListView control.
            #endregion
            
            foreach (var dir in songPack.songDirs)
            {
                SingleSong singleSong = new(dir);
                singleSong.GenRandomSelector();
                singleSong.readInformationFromDotOsu();
                ListViewItem listViewItem = new ListViewItem(new string[] { singleSong.title??"null", singleSong.artist??"null"}, -1, Color.Empty, Color.BurlyWood, null);
                listView1.ItemSelectionChanged += ListView1_ItemCheck;
                this.listView1.Items.Add(listViewItem);
               

            }
        }

        private void ListView1_ItemCheck(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            textBox1.Text = e.Item.ToString();`
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
