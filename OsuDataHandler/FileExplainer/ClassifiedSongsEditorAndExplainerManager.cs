using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public string selectedRrcordFileName { get; set; }
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
        public void AddSong(string songName)
        {
            songsEditor.AddSong(songName);
        }
        public void AddSong(List<string> songNames)
        {
            songsEditor.AddSong(songNames);
        }
        public void RemoveSong(string songName)
        {
            songsEditor.RemoveSong(songName);
        }
        public void RemoveSong(List<string> songNames)
        {
            songsEditor.RemoveSong(songNames);
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
            songsExplainer = new ClassifiedSongsExplainer(folderAddress.FullName + @"\loved");
            return songsExplainer.classifiedSongs;
        }
    }
}
