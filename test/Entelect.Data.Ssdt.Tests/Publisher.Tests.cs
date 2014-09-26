using NUnit.Framework;

namespace Entelect.Data.Ssdt.Tests
{
    [TestFixture]
    public class PublisherTests
    {
        [Test]
        public void GetProgramfilesPath()
        {
            var publisher = new Publisher();
            var programFilesPath = publisher.GetProgramfilesPath();
            Assert.False(string.IsNullOrWhiteSpace(programFilesPath));
        }
    }
}
