using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;

namespace ChatApp
{
    static class Camera
    {
        private static VideoCaptureDevice VideoSource = null;
        private static FilterInfoCollection VideoDevices;
        private static Action<Bitmap> onNewFrame;
        public static Action<Bitmap> OnNewFrame
        {
            set { onNewFrame = value; }
        }
        public static bool CanSend { get; set; } = true;
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
}
