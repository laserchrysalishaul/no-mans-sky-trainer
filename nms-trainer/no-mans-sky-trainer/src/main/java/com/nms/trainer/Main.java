package com.nms.trainer;

/**
 * Entry point for the No Man's Sky Trainer application.
 * This trainer allows modification of in-game values such as units, nanites, and health
 * by reading and writing to the game's process memory on Windows.
 */
public class Main {

    public static void main(String[] args) {
        System.out.println("=== No Man's Sky Trainer v1.0 ===");
        System.out.println("Launching trainer interface...");

        // Initialize the trainer with default memory offsets (example values)
        Trainer trainer = new Trainer("NMS.exe");
        trainer.addOffset("units", 0x12345678L);
        trainer.addOffset("nanites", 0x12345680L);
        trainer.addOffset("health", 0x12345688L);

        // Attempt to attach to the game process
        if (trainer.attachToProcess()) {
            System.out.println("Successfully attached to No Man's Sky.");
            
            // Example: read current values
            int currentUnits = trainer.readInt("units");
            int currentNanites = trainer.readInt("nanites");
            int currentHealth = trainer.readInt("health");
            System.out.println("Current Units: " + currentUnits);
            System.out.println("Current Nanites: " + currentNanites);
            System.out.println("Current Health: " + currentHealth);

            // Example: set values to maximum
            trainer.writeInt("units", 999999999);
            trainer.writeInt("nanites", 999999);
            trainer.writeInt("health", 100);
            System.out.println("Values updated successfully.");
        } else {
            System.err.println("Failed to attach to No Man's Sky. Ensure the game is running.");
        }

        // Clean up
        trainer.detach();
        System.out.println("Trainer detached.");
    }
}
