using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OsuData
{
    /// <summary>
    /// contains all songs in Osu folder
    /// </summary>
    public class SongPack
    {
        DirectoryInfo address { set; get; }
        DirectoryInfo songAddress { set; get; }
        public SongPack(String address)
        {
            this.address = new  DirectoryInfo(address);
            this.songAddress = new DirectoryInfo(address + @"\Songs");
            songDirs = songAddress.GetDirectories();
        }
        public void ShowAddress()
        {
            Console.WriteLine($"address:{address}");
            Console.WriteLine($"songAddress:{songAddress}");
        }
        public DirectoryInfo[] songDirs { set; get; }
        public void showSongDirs()
        {
            foreach (var songDir in songDirs)
            {
                Console.WriteLine(songDir.ToString());
            }
        }
    }
}
