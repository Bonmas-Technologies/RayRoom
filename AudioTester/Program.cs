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
#pragma warning disable CA1416 // Проверка совместимости платформы
        static void Main(string[] args)
        {
            AudioSimulator a = new AudioSimulator();

            var array = a.Simulate(Vector2.Zero, 360);
            var bitmap = new Bitmap(2000, 2000);

            Matrix2x2 mat = new Matrix2x2(Vector2.UnitX * 100f, Vector2.UnitY * -100f);

            Pen background = new Pen(Color.FromArgb(5, Color.White), 4);
            Pen foreground = new Pen(Color.FromArgb(25, Color.White), 1);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Black);

                for (int i = 0; i < array.Length; i++)
                {
                    g.DrawLine(background, (array[i].a * mat + new Vector2(1000, 1000)).GetPoint(), (array[i].b * mat + new Vector2(1000, 1000)).GetPoint());
                    g.DrawLine(foreground, (array[i].a * mat + new Vector2(1000, 1000)).GetPoint(), (array[i].b * mat + new Vector2(1000, 1000)).GetPoint());
                }
            }

            bitmap.Save("bitmap.bmp");
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