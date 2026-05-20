package com.nms.trainer;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

/**
 * Unit tests for the Trainer class.
 */
public class TrainerTest {

    private Trainer trainer;

    @BeforeEach
    public void setUp() {
        trainer = new Trainer("NMS.exe");
        trainer.addOffset("units", 0x12345678L);
        trainer.addOffset("nanites", 0x12345680L);
    }

    @Test
    public void testAttachAndDetach() {
        assertTrue(trainer.attachToProcess(), "Should attach successfully");
        trainer.detach();
        // No exception should occur
    }

    @Test
    public void testReadIntWhenNotAttached() {
        int result = trainer.readInt("units");
        assertEquals(-1, result, "Should return -1 when not attached");
    }

    @Test
    public void testReadIntWithUnknownOffset() {
        trainer.attachToProcess();
        int result = trainer.readInt("unknown");
        assertEquals(-1, result, "Should return -1 for unknown offset");
        trainer.detach();
    }

    @Test
    public void testWriteIntWhenAttached() {
        trainer.attachToProcess();
        // Should not throw exception
        trainer.writeInt("units", 999999999);
        trainer.detach();
    }

    @Test
    public void testWriteIntWhenNotAttached() {
        // Should not throw exception, but print error
        trainer.writeInt("units", 500);
    }
}
