using System;
using Memory;

namespace NoMansSkyTrainer.Features
{
    /// <summary>
    /// Provides infinite units (in-game currency) by overwriting the units value.
    /// </summary>
    public class InfiniteUnits
    {
        private readonly Mem _memory;
        private readonly IntPtr _unitsAddress;
        private const int MaxUnits = 999999999;

        /// <summary>
        /// Initializes a new instance of the InfiniteUnits feature.
        /// </summary>
        /// <param name="memory">Memory library instance attached to the game process.</param>
        public InfiniteUnits(Mem memory)
        {
            _memory = memory;
            // Example address (placeholder)
            _unitsAddress = (IntPtr)0x00D4E5F6;
        }

        /// <summary>
        /// Sets the player's units to the maximum value.
        /// </summary>
        public void Apply()
        {
            try
            {
                int currentUnits = _memory.ReadInt(_unitsAddress.ToString("X"));
                if (currentUnits < MaxUnits)
                {
                    _memory.WriteMemory(_unitsAddress.ToString("X"), "int", MaxUnits.ToString());
                    Console.WriteLine("Infinite Units: Set to max.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InfiniteUnits error: {ex.Message}");
            }
        }
    }
}
