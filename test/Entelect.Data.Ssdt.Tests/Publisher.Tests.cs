using System;
using System.IO;
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

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeNull()
        {
            var publisher = new Publisher();
            const string dacpacPath = null;
            const string publishFilePath = "asd";
            publisher.Publish(dacpacPath, publishFilePath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeNull()
        {
            var publisher = new Publisher();
            const string dacpacPath = "asd";
            const string publishFilePath = null;
            publisher.Publish(dacpacPath, publishFilePath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeWhitespace()
        {
            var publisher = new Publisher();
            const string publishFilePath = "asd";
            publisher.Publish(string.Empty, publishFilePath);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeWhitespace()
        {
            var publisher = new Publisher();
            const string dacpacPath = "asd";
            publisher.Publish(dacpacPath, string.Empty);
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
            const string dacpacPath = @"C:\Development\EntelectOpenSorceLibrary\Entelect.Data.Ssdt\test\TestDatabase\bin\Debug";
            const string publishFilePath = @"C:\Development\EntelectOpenSorceLibrary\Entelect.Data.Ssdt\test\TestDatabase\TestDatabase.LocalDb.publish.xml";
            publisher.Publish(dacpacPath, publishFilePath);
        }
    }
}
