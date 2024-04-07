using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData.FileExplainer
{
    internal class LovedSongsEditor
    {
        public FileInfo lovedSongFile {private set; get; }
        public LovedSongsEditor(string address)
        {
            lovedSongFile = new FileInfo(address);
        }
    }
}
