using System;
using Memory;

namespace NoMansSkyTrainer.Features
{
    /// <summary>
    /// Speeds up the scanning process by modifying scan time memory value.
    /// </summary>
    public class FastScanning
    {
        private readonly Mem _memory;
        private readonly IntPtr _scanTimeAddress;
        private const float FastScanTime = 0.1f; // Almost instant scan

        /// <summary>
        /// Initializes a new instance of the FastScanning feature.
        /// </summary>
        /// <param name="memory">Memory library instance attached to the game process.</param>
        public FastScanning(Mem memory)
        {
            _memory = memory;
            // Example address (placeholder)
            _scanTimeAddress = (IntPtr)0x00F1A2B3;
        }

        /// <summary>
        /// Reduces the scan timer to near-instant completion.
        /// </summary>
        public void Apply()
        {
            try
            {
                float currentScanTime = _memory.ReadFloat(_scanTimeAddress.ToString("X"));
                if (currentScanTime > FastScanTime)
                {
                    _memory.WriteMemory(_scanTimeAddress.ToString("X"), "float", FastScanTime.ToString());
                    Console.WriteLine("Fast Scanning: Scan time reduced.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FastScanning error: {ex.Message}");
            }
        }
    }
}
