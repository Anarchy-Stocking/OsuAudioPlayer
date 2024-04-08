using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuData.FileExplainer
{
    internal class ClassifiedSongsEditor
    {
        public String fileDir{set; get; }
        public FileInfo classifiedSongFile {private set; get; }
        public ClassifiedSongsEditor(string address)
        {
            fileDir = address;
            classifiedSongFile = new FileInfo(fileDir);
        }
        public ClassifiedSongsEditor(FileInfo file):this(file.FullName) { }
        public void AddLovedSong(string songName)
        {
            if (classifiedSongFile.Exists)
            {
                foreach (string line in File.ReadAllLines(fileDir))
                {
                    if (line == songName)
                    {
                        return;
                    }
                }
                using (StreamWriter sw = classifiedSongFile.AppendText())
                {
                    sw.WriteLine(songName);
                }
            }
            else
            {
                using (StreamWriter sw = classifiedSongFile.CreateText())
                {
                    sw.WriteLine(songName);
                }
            }
        }
        public void AddLovedSong(List<string> songNames)
        {
            if (classifiedSongFile.Exists)
            {
                // 感觉如果用hash table来把歌曲名存储在文件中的对应行会更快
                List<string> lines = new List<string>(File.ReadAllLines(fileDir));
                List<string> distinguished = new List<string>();
                distinguished.AddRange(lines.Except(songNames));
                using (StreamWriter sw = classifiedSongFile.AppendText())
                {
                    foreach (string songName in songNames)
                    {
                        sw.WriteLine(songName);
                    }
                }
            }
            else
            {
                using (StreamWriter sw = classifiedSongFile.CreateText())
                {
                    foreach (string songName in songNames)
                    {
                        sw.WriteLine(songName);
                    }
                }
            }
        }
        public void RemoveClassifiedSong(string songName)
        {
            if (classifiedSongFile.Exists)
            {
                string[] lines = File.ReadAllLines(fileDir);
                List<string> newLines = new List<string>();
                foreach (string line in lines)
                {
                    if (line != songName)
                    {
                        newLines.Add(line);
                    }
                }
                File.WriteAllLines(fileDir, newLines.ToArray());
            }
        }   
        public void RemoveClassifiedSong(List<string> songNames)
        {
            if (classifiedSongFile.Exists)
            {
                List<string> lines = new List<string>(File.ReadAllLines(fileDir));
                List<string> distinguished = new List<string>();
                distinguished.AddRange(lines.Except(songNames));
                File.WriteAllLines(fileDir, distinguished.ToArray());
            }
        }
    }
}
