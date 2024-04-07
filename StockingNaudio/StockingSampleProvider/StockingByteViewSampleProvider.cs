using NAudio.Wave;

namespace StockingNAudio.StockingSampleProvider
{
    /// <summary>
    /// Very simple sample provider supporting adjustable gain
    /// </summary>
    public class StockingByteViewSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider source;

        /// <summary>
        /// Initializes a new instance of VolumeSampleProvider
        /// </summary>
        /// <param name="source">Source Sample Provider</param>
        public StockingByteViewSampleProvider(ISampleProvider source)
        {
            this.source = source;
        }

        /// <summary>
        /// WaveFormat
        /// </summary>
        public WaveFormat? WaveFormat => source.WaveFormat;
        public float[]? sampleBuffer;
        private Task? processingTask;
        /// <summary>
        /// Reads samples from this sample provider
        /// </summary>
        /// <param name="buffer">Sample buffer</param>
        /// <param name="offset">Offset into sample buffer</param>
        /// <param name="sampleCount">Number of samples desired</param>
        /// <returns>Number of samples read</returns>
        public int Read(float[] buffer, int offset, int sampleCount)
        {
            int samplesRead = source.Read(buffer, offset, sampleCount);

            // copy sample to internal buffer
            Array.Resize(ref sampleBuffer, samplesRead);
            Array.Copy(buffer, offset, sampleBuffer, 0, samplesRead);

            // activate processing task
            processingTask = Task.Run(() => PrintByte(buffer, offset, sampleCount));
            return samplesRead;
        }   
        /// <summary>
        /// do effects on samples
        /// </summary>
        /// <param name="samples"></param>
        private void PrintByte(float[] buffer, int offset, int sampleCount)
        {
            for (int n = 0; n < sampleCount; n++)
            {
                Console.WriteLine(buffer[offset + n]);
            }
            // copy data back to buffer
            Array.Copy(sampleBuffer, 0, buffer, offset, sampleCount);
        }

    }
}
