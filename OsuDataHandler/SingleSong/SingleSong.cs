using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuData;

namespace OsuData
{
    /// <summary>
    /// everything in one song folder of osu
    /// </summary>
    public partial class SingleSong
    {
        public DirectoryInfo address { private set; get; }
        public String name { set; get; }
        
        FileInfo []mixFiles;
        public List<FileInfo> dotOsus { set; get; }
        List<FileInfo> dotmp3s;
        List<FileInfo> images;
        /// <summary>
        /// classify files by their extension name
        /// </summary>
        void ClassifyFiles()
        {
            foreach(var file in mixFiles)
            {
                if(file.Extension == @".osu")
                {
                    dotOsus.Add(file);
                    continue;
                }
                if (file.Extension == @".mp3")
                {
                    dotmp3s.Add(file);
                    continue;
                }
                // images
                if (file.Extension == @".png")
                {
                    images.Add(file);
                    continue;
                }
                if (file.Extension == @".jpg")
                {
                    images.Add(file);
                    continue;
                }
            }
        }
        public void ShowMixFiles()
        {
            foreach(var file in mixFiles)
            {
                Console.WriteLine(file.Extension);
            }
        }

        public void ShowFiles()
        {
            foreach(var file in dotOsus)
            {
                Console.WriteLine(file.ToString());
            }
            foreach (var file in dotmp3s)
            {
                Console.WriteLine(file.ToString());
            }
            foreach (var file in images)
            {
                Console.WriteLine(file.ToString());
            }
        }
    }
}
