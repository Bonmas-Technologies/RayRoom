using AudioTester.Core;
using NAudio.Mixer;
using NAudio.Wave;
using RayRoom.Core;
using RayRoom.NAudioEngine;
using System.Numerics;

namespace AudioTester
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var settings = new Settings(44100, 100, 330, 100f);
            RaySimulator simulator = new RaySimulator(settings);

            var sampler = new AudioSampler(@".\Resources\test.wav", true);

            List<ICastObject> structures = new List<ICastObject>
            {
                new AudioSource(new Vector2(0, 9), 10, sampler),
                new Line(new Vector2(-2, 10), new Vector2(-2, -10)),
                new Line(new Vector2(-1, 8), new Vector2(1, 8)),
                new Line(new Vector2(2, 10), new Vector2(2, -10)),
                new Line(new Vector2(-2, -10), new Vector2(2, -10)),
                new Line(new Vector2(-2, 10), new Vector2(2, 10)),
            };
            
            var device = new AudioOut(settings);
            device.Container = new ResultContainer(0, new AudioSourceCollision[0]);

            Vector2 pos = new Vector2(0, 0);
            using (var wo = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 150))
            {
                wo.Init(device);
                wo.Play();

                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey();

                        switch (key.Key)
                        {
                            case ConsoleKey.Q:
                                break;
                            case ConsoleKey.E:
                                break;
                            case ConsoleKey.W:
                                pos += new Vector2(0, 0.1f);
                                break;
                            case ConsoleKey.S:
                                pos += new Vector2(0, -0.1f);
                                break;
                            case ConsoleKey.A:
                                pos += new Vector2(-0.1f, 0);
                                break;
                            case ConsoleKey.D:
                                pos += new Vector2(0.1f, 0);
                                break;
                            case ConsoleKey.Escape:
                                wo.Stop();
                                break;
                            default:
                                break;
                        }
                        Console.CursorLeft = 0;
                        Console.CursorTop = 0;
                        Console.WriteLine("   {0}   ", pos.ToString("0.00"));
                    }

                    var left = simulator.Simulate(structures, -Vector2.UnitX * 0.1f + pos, 0, 36);
                    var right = simulator.Simulate(structures, Vector2.UnitX * 0.1f + pos, 1, 36);
            
                    var container = new ResultContainer(36, left.distances.Concat(right.distances).ToArray());

                    device.Container = container;
                }
            }
        }
    }
}