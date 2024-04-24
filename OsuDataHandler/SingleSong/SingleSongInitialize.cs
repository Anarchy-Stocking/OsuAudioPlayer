using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData
{
    public partial class SingleSong
    {
        public SingleSong(DirectoryInfo address)
        {
            this.address = address;
            mixFiles = this.address.GetFiles();
            name = this.address.Name;
            dotOsus = [];
            dotmp3s = [];
            images = [];
            ClassifyFiles();
            readInformationFromDotOsu();
        }
        public SingleSong(String address) : this(new DirectoryInfo(address)) { }
    }
}
