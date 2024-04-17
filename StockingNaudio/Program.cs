using System;
using System.Text.RegularExpressions;
using NAudio.Wave;
using StockingNAudio.StockingSampleProvider;
using System.Threading.Tasks;
using System.Threading;

//Track track = new(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3");
//track.Init();
//Console.WriteLine("Initialized");
//Console.WriteLine(track.rawAudioReader.WaveFormat);

//while (true)
//{
//    var read = Console.ReadLine();
//    switch (read)
//    {
//        case "play":
//            track.Play();
//            break;
//        case "stop":
//            track.Stop();
//            break;
//        case "init":
//            track.Init();
//            Console.WriteLine("Initialized");
//            break;
//        case ">>":
//            track.JumpForward(5);
//            break;
//        case "<<":
//            track.JumpBack(5);
//            break;
//        case "0.5":
//            track.Volume(0.5f);
//            break;
//        case "2":
//            track.Volume(2f);
//            break;
//        case "0.1":
//            track.Volume(0.1f);
//            break;
//        case "byte":
//            track.ByteView();
//            break;
//        default:
//            try
//            {
//                var num = Int32.Parse(read);
//                track.Volume(num);
//            }
//            catch
//            {

//            }
//            break;
//    }
//}

//WaveOutEvent device = new();
//AudioFileReader reader = new(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3");
//StockingVolumeSampleProvider volumeSampleProvider = new(reader);
//device.Init(volumeSampleProvider);
//device.Play();

WaveOutEvent device = new();
StockingWaveEndsSampleProvider endProvider = new StockingWaveEndsSampleProvider(new AudioFileReader(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3"));
StockingWaveEndsSampleProvider endProvider2 = new StockingWaveEndsSampleProvider(new AudioFileReader(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3"));

List<Task> tasks  = new();
var t = Task.Run(() =>
{
    while (!endProvider.PlayEnds)
    {
        //do nothing
        //Console.WriteLine("Checking");
    }
    device.Stop();
    device.Init(endProvider2);
    device.Play();
    Console.WriteLine("Ends");
});
tasks.Add(t);
tasks.Add(Task.Run(()=>
{
    device.Init(endProvider);
    //device.Init(new StockingWaveEndsSampleProvider(new AudioFileReader(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3")));
    device.Play();
}));
Task.WaitAll(tasks.ToArray());
while (true) ;


