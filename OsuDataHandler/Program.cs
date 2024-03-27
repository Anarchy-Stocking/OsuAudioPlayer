using OsuData;
using System.Text.RegularExpressions;

SongPack songPack = new SongPack(@"E:\SmallGame\osu");
songPack.ShowAddress();
songPack.showSongDirs();
SingleSong singleSong = new SingleSong(@"E:\SmallGame\osu\Songs\2164 Lucky Star Cast - Hamatte Sabotte Oh My Ga! (Short Ver)");
singleSong.ShowMixFiles();
singleSong.ShowFiles();

//Console.WriteLine(singleSong.name);
//DotOsuReader dotOsuReader = new DotOsuReader(@"E:\SmallGame\osu\Songs\16600 Hatsune Miku, Megurine Luka - World's End Dancehall\Hatsune Miku, Megurine Luka - World's End Dancehall (NatsumeRin) [Shinxyn's Hard].osu");

//dotOsuReader.ShowText();
//String pattern = String.Format("^{0}:\\s*\\S+$", "test");

//Console.WriteLine($"pattern: {pattern}");
//pattern = pattern.Remove(0, 5);
//Console.WriteLine($"pattern: {pattern}");
//Console.WriteLine(dotOsuReader.artist);
//Console.WriteLine(dotOsuReader.imgDir);
//Console.WriteLine(dotOsuReader.audioDir);
//Console.WriteLine(dotOsuReader.title);






