using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData.FileExplainer
{
    internal class ClassifiedSongsExplainer
    {
        public String fileDir { set; get; }
        public FileInfo classifiedSongFile { private set; get; }
        public List<String> classifiedSongs { private set; get; }
        public ClassifiedSongsExplainer(string address)
        {
            fileDir = address;
            classifiedSongFile = new FileInfo(fileDir);
            classifiedSongs = new List<String>();
            if(classifiedSongFile.Exists)
            {
                ReadClassifiedSongs();
            }
        }
        public ClassifiedSongsExplainer(FileInfo file) : this(file.FullName) { }
        public void ReadClassifiedSongs()
        {
            classifiedSongs.Clear();
            if (classifiedSongFile.Exists)
            {
                classifiedSongs.AddRange(File.ReadAllLines(fileDir));
            }
        }
    }
}
