using System;

namespace Entelect.Data.Ssdt
{
    public class Publisher
    {
        internal protected virtual string GetProgramfilesPath()
        {
            var x86Path = Environment.GetEnvironmentVariable("programfiles(x86)");
            if (!string.IsNullOrWhiteSpace(x86Path))
            {
                return x86Path;
            }
            var x64Path = Environment.GetEnvironmentVariable("programfiles");
            return x64Path;
        }

        /// <summary>
        /// Publishes the db using the supplied path to the .dacpac file and path to the .xml publish file
        /// </summary>
        /// <param name="dacpacPath"></param>
        /// <param name="publishFilePath"></param>
        public void Publish(string dacpacPath, string publishFilePath)
        {
            ValidateInputs(dacpacPath, publishFilePath);
            throw new NotImplementedException();
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
