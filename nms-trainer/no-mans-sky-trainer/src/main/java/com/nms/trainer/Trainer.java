package com.nms.trainer;

import java.util.HashMap;
import java.util.Map;

/**
 * Core trainer class that manages process attachment and memory read/write operations.
 * This is a simulated implementation for demonstration; real memory manipulation
 * would require native libraries (e.g., JNA or JNI) and platform-specific code.
 */
public class Trainer {

    private final String processName;
    private final Map<String, Long> offsets;
    private boolean attached;

    /**
     * Creates a new Trainer for the specified process.
     * @param processName The name of the target process (e.g., "NMS.exe").
     */
    public Trainer(String processName) {
        this.processName = processName;
        this.offsets = new HashMap<>();
        this.attached = false;
    }

    /**
     * Registers a memory offset for a named value.
     * @param name   The name of the value (e.g., "units").
     * @param offset The memory offset from the process base address.
     */
    public void addOffset(String name, long offset) {
        offsets.put(name, offset);
    }

    /**
     * Attempts to attach to the target process.
     * In a real implementation, this would use native APIs like OpenProcess.
     * @return true if attachment succeeded, false otherwise.
     */
    public boolean attachToProcess() {
        // Simulate attachment: In reality, check if process exists and open handle
        System.out.println("Attempting to attach to process: " + processName);
        // Assume success for demonstration
        attached = true;
        return true;
    }

    /**
     * Reads an integer value from the memory of the target process.
     * @param name The name of the value to read (must have been added via addOffset).
     * @return The integer value read, or -1 if not attached or offset not found.
     */
    public int readInt(String name) {
        if (!attached) {
            System.err.println("Not attached to any process.");
            return -1;
        }
        if (!offsets.containsKey(name)) {
            System.err.println("Offset not found for: " + name);
            return -1;
        }
        // Simulate reading memory: return a placeholder value
        long offset = offsets.get(name);
        System.out.println("Reading int at offset 0x" + Long.toHexString(offset));
        // In real code: call ReadProcessMemory
        return 100; // Placeholder
    }

    /**
     * Writes an integer value to the memory of the target process.
     * @param name  The name of the value to write.
     * @param value The integer value to write.
     */
    public void writeInt(String name, int value) {
        if (!attached) {
            System.err.println("Not attached to any process.");
            return;
        }
        if (!offsets.containsKey(name)) {
            System.err.println("Offset not found for: " + name);
            return;
        }
        long offset = offsets.get(name);
        System.out.println("Writing int " + value + " at offset 0x" + Long.toHexString(offset));
        // In real code: call WriteProcessMemory
    }

    /**
     * Detaches from the target process and releases resources.
     */
    public void detach() {
        if (attached) {
            System.out.println("Detaching from process: " + processName);
            attached = false;
            // In real code: CloseHandle
        }
    }
}
