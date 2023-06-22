using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ScreenStreamingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebSocketServer(IPAddress.Any, 1234);
            server.AddWebSocketService<ScreenStreaming>("/screen");
            server.Start();
            server.Stop();
        }
    }

    class ScreenStreaming : WebSocketBehavior
    {
        private bool _isStreaming = false;

        protected override void OnOpen()
        {
            _isStreaming = true;
            Task.Run(() => StreamScreen());
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _isStreaming = false;
        }

        private void StreamScreen()
        {
            var encoder = Encoder.Quality;
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(encoder, 50L);
            var jpegEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
            var socket = Context.WebSocket;

            while (_isStreaming)
            {
                using (var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                using (var graphics = Graphics.FromImage(bitmap))
                using (var memoryStream = new MemoryStream())
                {
                    graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                    bitmap.Save(memoryStream, jpegEncoder, parameters);
                    var buffer = memoryStream.ToArray();
                    if (socket.ReadyState == WebSocketState.Open) 
                    {
                        socket.Send(buffer);
                    }
                }
            }
        }
    }
}
