using OsuData.FileExplainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData
{
    public partial class SingleSong
    {
        public int dotOsuSelector { set; get; } = 0;
        public void GenRandomSelector()
        {
            Random rand = new Random();
            dotOsuSelector = rand.Next(0,dotOsus.Count-1);
        }
        public FileInfo? mainMusic { set; get; }
        public FileInfo? mainImg { set; get; }
        public String? title { set; get; }
        public String? artist { set; get; }
        public void readInformationFromDotOsu()
        {
            DotOsuReader osuReader = new DotOsuReader(dotOsus[dotOsuSelector]);
            title = osuReader.title??"notSetYet";
            artist = osuReader.artist??"notSetYet";
            mainMusic = new FileInfo(osuReader.audioDir?? "notSetYet");
            mainImg = new FileInfo(osuReader.imgDir?? "notSetYet");
        }
    }
}
