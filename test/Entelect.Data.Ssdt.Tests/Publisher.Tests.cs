using System;
using System.IO;
using NUnit.Framework;

namespace Entelect.Data.Ssdt.Tests
{
    [TestFixture]
    public class PublisherTests
    {
        const string ExistingDacpacPath = @"C:\Development\EntelectOpenSorceLibrary\Entelect.Data.Ssdt\test\TestDatabase\bin\Debug\TestDatabase.dacpac";
        const string ExistingPublishFilePath = @"C:\Development\EntelectOpenSorceLibrary\Entelect.Data.Ssdt\test\TestDatabase\TestDatabase.LocalDb.publish.xml";

        [Test]
        public void GetProgramfilesPath()
        {
            var publisher = new Publisher(null,null);
            var programFilesPath = publisher.GetProgramfilesPath();
            Assert.False(string.IsNullOrWhiteSpace(programFilesPath));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeNull()
        {
            var publisher = new Publisher(null, ExistingPublishFilePath);
            publisher.Publish();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeNull()
        {
            var publisher = new Publisher(ExistingDacpacPath, null);
            publisher.Publish();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeWhitespace()
        {
            var publisher = new Publisher(string.Empty, ExistingPublishFilePath);
            publisher.Publish();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeWhitespace()
        {
            var publisher = new Publisher(ExistingDacpacPath, string.Empty);
            publisher.Publish();
        }

        [Test]
        public void CheckSqlpackageExePathCorrect()
        {
            var publisher = new Publisher(null, null);
            var pathToSqlpackageExe = publisher.GetPathToSqlpackageExe();
            Assert.False(string.IsNullOrWhiteSpace(pathToSqlpackageExe));
            Assert.True(File.Exists(pathToSqlpackageExe), string.Format("SqlpackageExe not found at path: {0}", pathToSqlpackageExe));
        }

        [Test]
        public void AbsoluteDacpacAndPublishFilePath()
        {
            var publisher = new Publisher(ExistingDacpacPath, ExistingPublishFilePath);
            publisher.Publish();
        }
    }
}
