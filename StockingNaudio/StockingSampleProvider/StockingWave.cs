using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockingNAudio.StockingSampleProvider
{
    public class StockingWave : ISampleProvider
    {
        private readonly ISampleProvider source;

        /// <summary>
        /// Initializes a new instance of StockingWaveEndsSampleProvider
        /// </summary>
        /// <param name="source">Source Sample Provider</param>
        public StockingWave(ISampleProvider source)
        {
            this.source = source;
        }

        /// <summary>
        /// WaveFormat
        /// </summary>
        public WaveFormat WaveFormat => source.WaveFormat;

        public bool PlayEnds { get; set; } = false;
        public float[] currBuffer;
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
                // _ = Task.Run(() => Console.WriteLine(currByte));
                _ = Task.Run(() => CopyCurrBuffer(buffer,offset,sampleCount));
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }

        private void CopyCurrBuffer(float[] buffer, int offset, int sampleCount)
        {
            Array.Resize(ref currBuffer, sampleCount);
            
            // copy sample to internal buffer
            for (int n = 0; n < sampleCount; n++)
            {
                currBuffer[n] = buffer[offset + n];
                //Console.Write($"{currBuffer[n]}  :  {buffer[offset + n]}");
                //Console.WriteLine();
            }
        }

        private void Dispose()
        {
            currBuffer = null;
        }

        ~StockingWave()
        {
            Dispose();
        }
    }
}
