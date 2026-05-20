using System;
using Memory;

namespace NoMansSkyTrainer.Features
{
    /// <summary>
    /// Provides infinite health by writing to the player's health memory address.
    /// </summary>
    public class InfiniteHealth
    {
        private readonly Mem _memory;
        private readonly IntPtr _healthAddress;
        private const float MaxHealth = 100.0f;

        /// <summary>
        /// Initializes a new instance of the InfiniteHealth feature.
        /// </summary>
        /// <param name="memory">Memory library instance attached to the game process.</param>
        public InfiniteHealth(Mem memory)
        {
            _memory = memory;
            // Example base address + offset pattern (placeholder for actual reverse-engineered address)
            // In a real trainer, this would be found via AOB scanning
            _healthAddress = (IntPtr)0x00A1B2C3; // Placeholder
        }

        /// <summary>
        /// Applies the infinite health cheat by setting health to maximum.
        /// </summary>
        public void Apply()
        {
            try
            {
                // Read current health to verify address validity
                float currentHealth = _memory.ReadFloat(_healthAddress.ToString("X"));
                if (currentHealth < MaxHealth)
                {
                    _memory.WriteMemory(_healthAddress.ToString("X"), "float", MaxHealth.ToString());
                    Console.WriteLine("Infinite Health: Restored to max.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"InfiniteHealth error: {ex.Message}");
            }
        }
    }
}
