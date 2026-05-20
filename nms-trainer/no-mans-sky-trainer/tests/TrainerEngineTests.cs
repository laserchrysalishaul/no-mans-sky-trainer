using System;
using System.Diagnostics;
using NUnit.Framework;
using NoMansSkyTrainer.Core;

namespace NoMansSkyTrainer.Tests
{
    /// <summary>
    /// Unit tests for the TrainerEngine class.
    /// Note: These tests require No Man's Sky to be running (NMS.exe).
    /// </summary>
    [TestFixture]
    public class TrainerEngineTests
    {
        private TrainerEngine? _engine;

        [SetUp]
        public void Setup()
        {
            // Ensure the game process exists before testing
            var processes = Process.GetProcessesByName("NMS");
            if (processes.Length == 0)
            {
                Assert.Ignore("No Man's Sky is not running. Skipping integration tests.");
            }

            _engine = new TrainerEngine();
        }

        [Test]
        public void Initialize_WhenGameRunning_ShouldAttachSuccessfully()
        {
            // Act
            Assert.DoesNotThrow(() => _engine?.Initialize());
        }

        [Test]
        public void Initialize_WhenGameNotRunning_ShouldThrowException()
        {
            // Arrange
            var engine = new TrainerEngine();

            // Temporarily rename the process to simulate missing game (not ideal, but for demo)
            // In real tests, we'd use mocking. Here we just check the logic.
            // This test will be skipped if game is running.
            var processes = Process.GetProcessesByName("NMS");
            if (processes.Length > 0)
            {
                Assert.Ignore("Game is running, skipping negative test.");
            }

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => engine.Initialize());
        }

        [Test]
        public void Update_WhenEngineInitialized_ShouldNotThrow()
        {
            // Arrange
            _engine?.Initialize();

            // Act & Assert
            Assert.DoesNotThrow(() => _engine?.Update());
        }

        [TearDown]
        public void Cleanup()
        {
            _engine?.Dispose();
        }
    }
}
