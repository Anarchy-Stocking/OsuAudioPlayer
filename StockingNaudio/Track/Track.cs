using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StockingNAudio.Track
{
    public partial class Track
    {
        WaveOutEvent device = new();
        public AudioFileReader? rawAudioReader { set; get; }
        ISampleProvider? sampleProvider;
        public Track(string audioFilePath)
        {
            rawAudioReader = new(audioFilePath);
        }
        public Track(ISampleProvider sampleProvider)
        {
            this.sampleProvider = sampleProvider;
        }
        public Track(AudioFileReader audioFileReader)
        {
            rawAudioReader = audioFileReader;
        }

        public void Init()
        {
            device.Init(sampleProvider??rawAudioReader);
        }
        public void Play()
        {
            device.Play();
        }
        public void Stop()
        {
            device.Stop();
        }
        public void Dispose()
        {
            device.Dispose();
            rawAudioReader.Dispose();
        }
    }
}
