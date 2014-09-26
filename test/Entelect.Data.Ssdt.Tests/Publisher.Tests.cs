using System;
using System.IO;
using NUnit.Framework;

namespace Entelect.Data.Ssdt.Tests
{
    [TestFixture]
    public class PublisherTests
    {
        const string AbsoluteExistingDacpacPath = @"C:\Development\EntelectOpenSorceLibrary\Entelect.Data.Ssdt\test\TestDatabase\bin\Debug\TestDatabase.dacpac";
        const string AbsoluteExistingPublishFilePath = @"C:\Development\EntelectOpenSorceLibrary\Entelect.Data.Ssdt\test\TestDatabase\TestDatabase.LocalDb.publish.xml";
        const string RelativeExistingDacpacPath = @"..\..\..\TestDatabase\bin\Debug\TestDatabase.dacpac";
        const string RelativeExistingPublishFilePath = @"..\..\..\TestDatabase\TestDatabase.LocalDb.publish.xml";

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
            var publisher = new Publisher(null, AbsoluteExistingPublishFilePath);
            publisher.Publish();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeNull()
        {
            var publisher = new Publisher(AbsoluteExistingDacpacPath, null);
            publisher.Publish();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DacpacPathCannotBeWhitespace()
        {
            var publisher = new Publisher(string.Empty, AbsoluteExistingPublishFilePath);
            publisher.Publish();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PublishFilePathCannotBeWhitespace()
        {
            var publisher = new Publisher(AbsoluteExistingDacpacPath, string.Empty);
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
            var publisher = new Publisher(AbsoluteExistingDacpacPath, AbsoluteExistingPublishFilePath);
            publisher.Publish();
        }

        [Test]
        public void RelativeDacpacAndPublishFilePath()
        {
            var publisher = new Publisher(RelativeExistingDacpacPath, RelativeExistingPublishFilePath);
            publisher.Publish();
        }
    }
}
