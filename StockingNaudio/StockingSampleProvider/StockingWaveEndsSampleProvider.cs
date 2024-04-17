using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockingNAudio.StockingSampleProvider
{
    public class StockingWaveEndsSampleProvider:ISampleProvider
    {
        private readonly ISampleProvider source;

        /// <summary>
        /// Initializes a new instance of StockingWaveEndsSampleProvider
        /// </summary>
        /// <param name="source">Source Sample Provider</param>
        public StockingWaveEndsSampleProvider(ISampleProvider source)
        {
            this.source = source;
        }

        /// <summary>
        /// WaveFormat
        /// </summary>
        public WaveFormat WaveFormat => source.WaveFormat;

        public bool PlayEnds { get; set; } = false;
        /// <summary>
        /// Reads samples from this sample provider
        /// </summary>
        /// <param name="buffer">Sample buffer</param>
        /// <param name="offset">Offset into sample buffer</param>
        /// <param name="sampleCount">Number of samples desired</param>
        /// <returns>Number of samples read</returns>
        public int Read(float[] buffer, int offset, int sampleCount)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < sampleCount)
            {
                int bytesRead = source.Read(buffer, offset + totalBytesRead, sampleCount - totalBytesRead);
                if (bytesRead == 0)
                {
                    PlayEnds = true;
                    //Console.WriteLine("Ends");
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }

        public async Task  PlayEndsCheck()
        {
            await Task.Run(() =>
            {
                while (!PlayEnds)
                {
                    Console.WriteLine("Checking");
                }
                Console.WriteLine("Ends");
            });
        }
    }
}
