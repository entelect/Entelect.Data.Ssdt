using System;
using System.IO;

namespace Entelect.Data.Ssdt
{
    public class Publisher
    {
        /// <summary>
        /// Publishes the db using the supplied path to the .dacpac file and path to the .xml publish file
        /// </summary>
        /// <param name="dacpacPath"></param>
        /// <param name="publishFilePath"></param>
        public void Publish(string dacpacPath, string publishFilePath)
        {
            ValidateInputs(dacpacPath, publishFilePath);
            var pathToSqlpackageExe = GetPathToSqlpackageExe();
            if (!File.Exists(pathToSqlpackageExe))
            {
                throw new FileNotFoundException(string.Format("Sqlpackage.exe not found at path {0}, SSDT must be installed", pathToSqlpackageExe), "Sqlpackage.exe");
            }
            throw new NotImplementedException();
        }

        internal protected virtual string GetProgramfilesPath()
        {
            var x86Path = Environment.GetEnvironmentVariable("programfiles(x86)");
            if (!string.IsNullOrWhiteSpace(x86Path))
            {
                return x86Path;
            }
            var x64Path = Environment.GetEnvironmentVariable("programfiles");
            if (!string.IsNullOrWhiteSpace(x64Path))
            {
                return x64Path;
            }
            throw new ArgumentException("Unable to determin path to the program files folder, you must set either the \"programfiles(x86)\" or \"programfiles\" environment variable");
        }

        protected internal virtual string GetPathToSqlpackageExe()
        {
            var programFilesPath = GetProgramfilesPath();
            return string.Format(@"{0}\Microsoft SQL Server\110\DAC\bin\sqlpackage.exe", programFilesPath);
        }

        private static void ValidateInputs(string dacpacPath, string publishFilePath)
        {
            if (string.IsNullOrWhiteSpace(dacpacPath))
            {
                throw new ArgumentNullException("dacpacPath", "Dacpac Path cannot be null");
            }
            if (string.IsNullOrWhiteSpace(publishFilePath))
            {
                throw new ArgumentNullException("publishFilePath", "Publish File Path cannot be null");
            }
        }
    }
}
