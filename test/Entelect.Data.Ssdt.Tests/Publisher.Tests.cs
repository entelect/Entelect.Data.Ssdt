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
            var publisher = new Publisher();
            var programFilesPath = publisher.GetProgramfilesPath();
            Assert.False(string.IsNullOrWhiteSpace(programFilesPath));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeNull()
        {
            var publisher = new Publisher();
            const string dacpacPath = null;
            publisher.Publish(dacpacPath, ExistingPublishFilePath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeNull()
        {
            var publisher = new Publisher();
            const string publishFilePath = null;
            publisher.Publish(ExistingDacpacPath, publishFilePath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeWhitespace()
        {
            var publisher = new Publisher();
            publisher.Publish(string.Empty, ExistingPublishFilePath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeWhitespace()
        {
            var publisher = new Publisher();
            publisher.Publish(ExistingDacpacPath, string.Empty);
        }

        [Test]
        public void CheckSqlpackageExePathCorrect()
        {
            var publisher = new Publisher();
            var pathToSqlpackageExe = publisher.GetPathToSqlpackageExe();
            Assert.False(string.IsNullOrWhiteSpace(pathToSqlpackageExe));
            Assert.True(File.Exists(pathToSqlpackageExe), string.Format("SqlpackageExe not found at path: {0}", pathToSqlpackageExe));
        }


        [Test]
        public void AbsoluteDacpacAndPublishFilePath()
        {
            var publisher = new Publisher();

            publisher.Publish(ExistingDacpacPath, ExistingPublishFilePath);
        }
    }
}
