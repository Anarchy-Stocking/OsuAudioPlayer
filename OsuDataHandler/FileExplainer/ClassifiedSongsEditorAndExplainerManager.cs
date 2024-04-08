using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData.FileExplainer
{
    public class ClassifiedSongsEditorAndExplainerManager
    {
        DirectoryInfo folderAddress;
        Dictionary<string, FileInfo> classifiedSongsRecordFile;
        ClassifiedSongsEditor songsEditor;
        ClassifiedSongsExplainer songsExplainer;
        string selectedRrcordFileName;
        public ClassifiedSongsEditorAndExplainerManager(string address)
        {
            this.folderAddress = new DirectoryInfo(address);
            classifiedSongsRecordFile = new Dictionary<string, FileInfo>();
            selectedRrcordFileName = "loved";
            songsEditor = new ClassifiedSongsEditor(address + @"\loved");
            songsExplainer = new ClassifiedSongsExplainer(address + @"\loved");
            FileInfo[] files = folderAddress.GetFiles();
            foreach (FileInfo file in files)
            {
                classifiedSongsRecordFile.Add(file.Name, file);
            }
        }
        public void AddLovedSong(string songName)
        {
            songsEditor.AddLovedSong(songName);
        }
        public void AddLovedSong(List<string> songNames)
        {
            songsEditor.AddLovedSong(songNames);
        }
        public void RemoveLovedSong(string songName)
        {
            songsEditor.RemoveClassifiedSong(songName);
        }
        public void RemoveLovedSong(List<string> songNames)
        {
            songsEditor.RemoveClassifiedSong(songNames);
        }
        public void SelecetRecordFile(string name)
        {
               selectedRrcordFileName = name;
               songsEditor = new ClassifiedSongsEditor(classifiedSongsRecordFile[selectedRrcordFileName]);
               songsExplainer = new ClassifiedSongsExplainer(classifiedSongsRecordFile[selectedRrcordFileName]);
        }
        public void CreateNewRecordFile(string name)
        {
            FileInfo newFile = new FileInfo(folderAddress.FullName + @"\" + name);
            if (!newFile.Exists)
            {
                newFile.Create();
                classifiedSongsRecordFile.Add(name, newFile);
            }
        }
        public List<string> GetClassifiedSongs()
        {
            return songsExplainer.classifiedSongs;
        }
    }
}
