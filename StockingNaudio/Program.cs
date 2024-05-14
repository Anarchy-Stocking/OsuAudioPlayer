using StockingNAudio.StockingSampleProvider;
using NAudio.Wave;
using System.Timers;
WaveOutEvent device = new();
StockingWave waveProvider = new (new AudioFileReader(@"E:\SmallGame\osu\Songs\38050 765PRO ALLSTARS - The world is all one!! (TV Size)\ed1.mp3"));
device.Init(waveProvider);
device.Play();

// Create a timer with a two second interval.
var aTimer = new System.Timers.Timer(1000/60);
// Hook up the Elapsed event for the timer. 
aTimer.Elapsed += OnTimedEvent;
aTimer.AutoReset = true;
aTimer.Enabled = true;

void OnTimedEvent(Object source, ElapsedEventArgs e)
{
    
    if (waveProvider.currBuffer != null && !waveProvider.PlayEnds)
    {
        Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                      e.SignalTime);
        var slice = 10;
        var sliceLength = waveProvider.currBuffer.Length / slice;
        var averBuffer = new float[slice];
        for (int i = 0; i < waveProvider.currBuffer.Length; i+= sliceLength)
        {
            var sum = 0f;
            for (int j = 0; j < sliceLength; j++)
            {
                sum += waveProvider.currBuffer[i + j];
            }
            averBuffer[i/sliceLength] = sum / sliceLength;
        }
        foreach (var f in averBuffer)
        {
            Console.Write(f +" ");
        }
        Console.WriteLine();
    }
    // Console.WriteLine(waveProvider.currBuffer == null ? "null" : waveProvider.currBuffer[0].ToString());
}

while (true) ;


