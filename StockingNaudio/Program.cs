using System;
using System.Text.RegularExpressions;
using NAudio.Wave;
using StockingNAudio.Track;
using StockingNAudio.StockingSampleProvider;

Track track = new(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3");
track.Init();
Console.WriteLine("Initialized");
Console.WriteLine(track.rawAudioReader.WaveFormat);

while (true)
{
    var read = Console.ReadLine();
    switch (read)
    {
        case "play":
            track.Play();
            break;
        case "stop":
            track.Stop();
            break;
        case "init":
            track.Init();
            Console.WriteLine("Initialized");
            break;
        case ">>":
            track.JumpForward(5);
            break;
        case "<<":
            track.JumpBack(5);
            break;
        case "0.5":
            track.Volume(0.5f);
            break;
        case "2":
            track.Volume(2f);
            break;
        case "0.1":
            track.Volume(0.1f);
            break;
        case "byte":
            track.ByteView();
            break;
        default:
            try
            {
                var num = Int32.Parse(read);
                track.Volume(num);
            }
            catch
            {

            }
            break;
    }
}

//WaveOutEvent device = new();
//AudioFileReader reader = new(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3");
//StockingVolumeSampleProvider volumeSampleProvider = new(reader);
//device.Init(volumeSampleProvider);
//device.Play();

