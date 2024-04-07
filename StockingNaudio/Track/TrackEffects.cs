using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using StockingNAudio.StockingSampleProvider;

namespace StockingNAudio.Track
{
    public partial class Track
    {
        #region AudioJumps
        public void JumpAt(int time)
        {
            device.Stop();
            rawAudioReader.CurrentTime = TimeSpan.FromSeconds(time);
            device.Play();
        }
        public void JumpForward(int time)
        {
            rawAudioReader.CurrentTime =
            rawAudioReader.TotalTime <= TimeSpan.FromSeconds(time) + rawAudioReader.CurrentTime ?
                rawAudioReader.TotalTime : TimeSpan.FromSeconds(time) + rawAudioReader.CurrentTime;
        }
        public void JumpBack(int time)
        {
            rawAudioReader.CurrentTime -=
                rawAudioReader.CurrentTime < TimeSpan.FromSeconds(time) ?
                rawAudioReader.CurrentTime : TimeSpan.FromSeconds(time);
        }
        #endregion

        public void FadeIn(int time)
        {
            var fadeInOutSampleProvider = new FadeInOutSampleProvider(sampleProvider ?? rawAudioReader, true);
            fadeInOutSampleProvider.BeginFadeIn(time);
            sampleProvider = fadeInOutSampleProvider;
        }
        public void FadeOut(int time)
        {
            var fadeInOutSampleProvider = new FadeInOutSampleProvider(sampleProvider ?? rawAudioReader, true);
            fadeInOutSampleProvider.BeginFadeOut(time);
            sampleProvider = fadeInOutSampleProvider;
        }

        //public void Volume(float volume)
        //{
        //    var volumeSampleProvider = new VolumeSampleProvider(sampleProvider ?? rawAudioReader);
        //    volumeSampleProvider.Volume = volume;
        //    sampleProvider = volumeSampleProvider;
        //}
        public void Volume(float volume)
        {
            var volumeSampleProvider = new StockingVolumeSampleProvider(sampleProvider ?? rawAudioReader);
            volumeSampleProvider.Volume = volume;
            sampleProvider = volumeSampleProvider;
            Stop();
            Init();
            Play();
        }
        public void ByteView()
        {
            var byteViewSampleProvider = new StockingByteViewSampleProvider(sampleProvider ?? rawAudioReader);
            sampleProvider = byteViewSampleProvider;
            Stop();
            Init();
            Play();

        }
    }
}
