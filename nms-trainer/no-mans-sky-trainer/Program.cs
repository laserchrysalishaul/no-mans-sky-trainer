using System;
using System.Threading;
using NoMansSkyTrainer.Core;

namespace NoMansSkyTrainer
{
    /// <summary>
    /// Entry point for the No Man's Sky Trainer application.
    /// Initializes the memory manager and trainer features.
    /// </summary>
    internal class Program
    {
        private static TrainerEngine? _engine;
        private static bool _running = true;

        static void Main(string[] args)
        {
            Console.WriteLine("No Man's Sky Trainer v1.0");
            Console.WriteLine("==========================");
            Console.WriteLine("Starting trainer...");

            try
            {
                _engine = new TrainerEngine();
                _engine.Initialize();

                Console.WriteLine("Trainer initialized. Press Ctrl+C to exit.");
                Console.CancelKeyPress += OnCancelKeyPress;

                // Main loop: update features every 100ms
                while (_running)
                {
                    _engine.Update();
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            finally
            {
                _engine?.Dispose();
                Console.WriteLine("Trainer stopped.");
            }
        }

        private static void OnCancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _running = false;
        }
    }
}
