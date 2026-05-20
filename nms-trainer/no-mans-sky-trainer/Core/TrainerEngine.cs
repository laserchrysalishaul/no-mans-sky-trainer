using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Memory;
using NoMansSkyTrainer.Features;

namespace NoMansSkyTrainer.Core
{
    /// <summary>
    /// Core engine that manages the trainer lifecycle and coordinates features.
    /// </summary>
    public class TrainerEngine : IDisposable
    {
        private Mem? _memoryLib;
        private Process? _gameProcess;
        private bool _disposed;

        // Trainer features
        private InfiniteHealth? _infiniteHealth;
        private InfiniteUnits? _infiniteUnits;
        private FastScanning? _fastScanning;

        /// <summary>
        /// Initializes the engine by attaching to the No Man's Sky process.
        /// </summary>
        public void Initialize()
        {
            _memoryLib = new Mem();

            // Find the No Man's Sky process (executable name: NMS.exe)
            var processes = Process.GetProcessesByName("NMS");
            if (processes.Length == 0)
            {
                throw new InvalidOperationException("No Man's Sky is not running. Please start the game first.");
            }

            _gameProcess = processes[0];
            _memoryLib.OpenProcess(_gameProcess.Id);

            Console.WriteLine($"Attached to process: {_gameProcess.ProcessName} (PID: {_gameProcess.Id})");

            // Initialize features
            _infiniteHealth = new InfiniteHealth(_memoryLib);
            _infiniteUnits = new InfiniteUnits(_memoryLib);
            _fastScanning = new FastScanning(_memoryLib);

            Console.WriteLine("Features loaded: Infinite Health, Infinite Units, Fast Scanning");
        }

        /// <summary>
        /// Updates all active features. Called periodically from the main loop.
        /// </summary>
        public void Update()
        {
            if (_gameProcess == null || _gameProcess.HasExited)
            {
                Console.WriteLine("Game process exited. Stopping trainer.");
                Environment.Exit(0);
                return;
            }

            // Run features concurrently for performance
            Parallel.Invoke(
                () => _infiniteHealth?.Apply(),
                () => _infiniteUnits?.Apply(),
                () => _fastScanning?.Apply()
            );
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _memoryLib?.CloseProcess();
                _memoryLib?.Dispose();
                _gameProcess?.Dispose();
                _disposed = true;
            }
        }
    }
}
