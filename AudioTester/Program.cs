using AudioTester.Core;
using AudioTester.Core.Extensions;
using NAudio.Wave;
using RayRoom.Core;
using System.Drawing;
using System.Numerics;

namespace AudioTester
{
    internal class Program
    {
        private const int width = 1280;
        private const int height = 720;
        private const float scale = 100;
#pragma warning disable CA1416 // Проверка совместимости платформы
        static void Main(string[] args)
        {
            AudioSimulator a = new AudioSimulator(new Settings(44100, 5, 330));

            var array = a.Simulate(Vector2.Zero, 3600);
            var bitmap = new Bitmap(width, height);

            Matrix2x2 mat = new(Vector2.UnitX * scale, Vector2.UnitY * -scale);

            Pen foreground = new(Color.FromArgb(20, Color.White), 1);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Black);

                Vector2 offset = new(width / 2, height / 2); 
                for (int i = 0; i < array.Length; i++)
                {
                    g.DrawLine(foreground, 
                        (array[i].a * mat + offset).GetPoint(), 
                        (array[i].b * mat + offset).GetPoint());
                }
            }

            bitmap.Save($"bitmap.png");
#pragma warning restore CA1416 // Проверка совместимости платформы
            return;
            var device = new RenderStreamer();

            using (var wo = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 150))
            {
                wo.Init(device);
                wo.Play();

                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);

                    if (Console.KeyAvailable)
                        wo.Stop();
                }
            }
        }
    }
}