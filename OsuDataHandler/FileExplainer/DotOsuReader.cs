using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OsuData.FileExplainer
{
    public class DotOsuReader
    {
        string file;
        public string? directoryName { private set; get; }
        IEnumerable<string> content;
        public DotOsuReader(string file)
        {
            this.file = file;
            content = File.ReadLines(this.file);
            directoryName = Path.GetDirectoryName(this.file);
            FindInformation();
        }
        public DotOsuReader(FileInfo file) : this(file.ToString()) { }
        public void ShowText()
        {
            foreach (var line in content)
            {
                Console.WriteLine(line);
            }
        }
        public string? artist { private set; get; }
        public string? title { private set; get; }
        public string? imgDir { private set; get; }
        public string? audioDir { private set; get; }
        bool PreFixCatcher(string line, string preFix)
        {
            // match $"{preFix}:"
            string pattern = string.Format("^{0}.*", preFix);
            if (Regex.IsMatch(line, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// judge if the line has the certain prefix and trim it
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        bool LineJudgeAndTrim(ref string line, string preFix)
        {
            if (PreFixCatcher(line, preFix))
            {
                // remove prefix
                line = line.Remove(0, preFix.Length);
                // remove spaces
                line = line.TrimStart();
                return true;
            }
            return false;
        }
        /// <summary>
        /// find <see cref="artist"/> , <see cref="title"/> , <see cref="audioDir"/>and <see cref="imgDir"/> in <see cref="file"/>
        /// </summary>
        void FindInformation()
        {
            foreach (var line in content)
            {
                var tmpLine = line;
                if (LineJudgeAndTrim(ref tmpLine, "AudioFilename:"))
                {
                    //Console.WriteLine(1);
                    audioDir = directoryName + @"\" + tmpLine;
                    continue;
                }
                if (LineJudgeAndTrim(ref tmpLine, "Title:"))
                {
                    title = tmpLine;
                    continue;
                }
                if (LineJudgeAndTrim(ref tmpLine, "Artist:"))
                {
                    artist = tmpLine;
                    continue;
                }
                if (LineJudgeAndTrim(ref tmpLine, "0,0,\""))
                {
                    var cnt = 0;
                    for(var i = 0; i < tmpLine.Length; i++)
                    {
                        if ('A'<=tmpLine[i] && tmpLine[i] <= 'z' )
                        {
                            cnt = i + 1;
                        }
                    }
                    tmpLine = tmpLine.Substring(0, cnt);
                    imgDir = directoryName + @"\" + tmpLine;
                    continue;
                }
            }
        }
    }
}


