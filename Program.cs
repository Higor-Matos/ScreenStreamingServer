using System.Drawing.Imaging;
using System.Net;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ScreenStreamingServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WebSocketServer server = new(IPAddress.Any, 1234);
            server.AddWebSocketService<ScreenStreaming>("/screen");
            server.Start();
            server.Stop();
        }
    }

    internal class ScreenStreaming : WebSocketBehavior
    {
        private bool _isStreaming = false;

        protected override void OnOpen()
        {
            _isStreaming = true;
            _ = Task.Run(StreamScreen);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _isStreaming = false;
        }

        private void StreamScreen()
        {
            Encoder encoder = Encoder.Quality;
            EncoderParameters parameters = new(1);
            parameters.Param[0] = new EncoderParameter(encoder, 50L);
            ImageCodecInfo? jpegEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            WebSocket socket = Context.WebSocket;

            while (_isStreaming)
            {
                using Bitmap bitmap = new(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using Graphics graphics = Graphics.FromImage(bitmap);
                using MemoryStream memoryStream = new();
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                bitmap.Save(memoryStream, jpegEncoder, parameters);
                byte[] buffer = memoryStream.ToArray();
                if (socket.ReadyState == WebSocketState.Open)
                {
                    socket.Send(buffer);
                }
            }
        }
    }
}
