using OsuData.FileExplainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData
{
    public class OsuDataManager
    {
        public Dictionary<string, FileInfo> DotOsuFileInfoDictionary { set; get; }
        SongPack osuGameFile;
        public OsuDataManager(string osuGameFileAddress, string classifiedSongsAddress,string selectedRecordFileName = "loved")
        {
            osuGameFile = new(osuGameFileAddress);
            this.selectedRecordFileName = selectedRecordFileName;
            classifyManager = new(classifiedSongsAddress);
            GenDotOsuFileInfoDictionary();
        }
        void GenDotOsuFileInfoDictionary()
        {
            DotOsuFileInfoDictionary = new Dictionary<string, FileInfo>();
            foreach (var singleSongDir in osuGameFile.songDirs)
            {
                SingleSong singleSong = new(singleSongDir);
                if(!DotOsuFileInfoDictionary.ContainsKey(singleSong.title))
                    DotOsuFileInfoDictionary.Add(singleSong.title, singleSong.dotOsus[0]);
            }
        }
        /// <summary>
        /// DotOsuReader contains basic information of a song
        /// </summary>
        /// <param name="songName"></param>
        /// <returns></returns>
        public DotOsuReader GenDotOsuReader(string songName)
        {
            return new(DotOsuFileInfoDictionary[songName]);
        }
        ClassifiedSongsEditorAndExplainerManager classifyManager;
        string selectedRecordFileName;
        public List<string> GetClassifiedSongs(string markName)
        {
            classifyManager.selectedRrcordFileName = markName; 
            return classifyManager.GetClassifiedSongs();
        }
        public void AddSongToMark (string songName)
        {
            classifyManager.AddSong(songName);
        }
        public void AddSongToMark(List<string> songNames)
        {
            classifyManager.AddSong(songNames);
        }
        public void RemoveSongFromMark(string songName)
        {
            classifyManager.RemoveSong(songName);
        }
        public void RemoveSongFromMark(List<string> songNames)
        {
            classifyManager.RemoveSong(songNames);
        }
    }
}
