using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using NAudio.Wave;
using System.IO;
using System.Media;
namespace ChatApp
{
    static class Speaker
    {
        private static WaveOut speaker;
        private static WaveFormat format;
        private static BufferedWaveProvider bufferedProvider;
        //play audio from buffer
        //http://stackoverflow.com/questions/28792548/how-can-i-play-byte-array-of-audio-raw-data-using-naudio
        public static void Init(int rate, int bits, int channels)
        {
            Dispose();
            speaker = new WaveOut();
            format = new WaveFormat(rate, bits, channels);
            bufferedProvider = new BufferedWaveProvider(format);
            speaker.Init(bufferedProvider);
            //speaker.DesiredLatency = 25;
            speaker.Play();
        }

        public static void PlayBuffer(byte[] bytes)
        {
            bufferedProvider.AddSamples(bytes, 0, bytes.Length);
        }

        public static void Dispose()
        {
            if(speaker != null)
            {
                speaker.Stop();
                speaker.Dispose();
            }
        }
    }
    static class Camera
    {
        private static VideoCaptureDevice VideoSource = null;
        private static FilterInfoCollection VideoDevices;
        private static Action<Bitmap> onNewFrame;
        public static void OnNewFrameCallback(Action<Bitmap> callback)
        {
            onNewFrame = callback;
        }
        private static bool canSend = true;
        public static bool CanSend
        {
            get { return canSend; }
            set { canSend = value; }
        }
        public static int OwnerChat { get; set; }
        public static bool Detect()
        {
            try
            {
                if (VideoSource != null)
                {
                    return true;
                }
                VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (VideoDevices.Count > 0)
                {
                    VideoSource = new VideoCaptureDevice(VideoDevices[0].MonikerString);
                    VideoSource.VideoResolution = VideoSource.VideoCapabilities[0];
                    VideoSource.NewFrame += new NewFrameEventHandler(NewFrameEvent);
                    return true;
                }
                return false;
            }
            catch
            {
                //Error ocurred...
                return false;
            }
        }

        public static void Start() { VideoSource.Start(); }
        public static void Stop() { VideoSource.Stop(); }
        public static void Release()
        {
            VideoSource.SignalToStop();
            VideoSource = null;
        }
        public static bool IsRunning
        {
            get { return VideoSource.IsRunning; }
        }
        private static void NewFrameEvent(object sender, NewFrameEventArgs eventArgs)
        {
            onNewFrame((Bitmap)eventArgs.Frame.Clone());
        }
    }
    //http://stackoverflow.com/questions/15101889/hearing-the-incoming-audio-from-mic
    static class Microphone
    {
        private static WaveIn audioInput = null;
        private static Action<byte[]> onAudioIn;
        public static void OnAudioInCallback(Action<byte[]> callback)
        {
            onAudioIn = callback;
        }
        public static void StartRecording()
        {
            if (audioInput == null)
            {
                audioInput = new WaveIn();
                audioInput.BufferMilliseconds = 25;
                audioInput.DataAvailable += DataAvailable;
                audioInput.StartRecording();
            }
        }
        public static void EndRecording()
        {
            if (audioInput != null)
            {
                audioInput.StopRecording();
            }
        }
        public static void Dispose()
        {
            if (audioInput != null)
            {
                audioInput.StopRecording();
                audioInput.Dispose();
                audioInput = null;
            }
        }
        public static Dictionary<string, int> GetWaveFormat()
        {
            Dictionary<string, int> waveFormat = new Dictionary<string, int>();
            WaveFormat format;
            if (audioInput == null)
            {
                audioInput = new WaveIn();
                format = audioInput.WaveFormat;
                audioInput.Dispose();
                audioInput = null;
            }
            else
            {
                format = audioInput.WaveFormat;
            }
            waveFormat.Add("rate", format.SampleRate);
            waveFormat.Add("bits", format.BitsPerSample);
            waveFormat.Add("channels", format.Channels);
            return waveFormat;
        }
        private static void DataAvailable(object sender, WaveInEventArgs e)
        {
            int i = e.BytesRecorded;
            onAudioIn(e.Buffer);
        }
    }
}
