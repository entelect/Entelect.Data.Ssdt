using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Entelect.Data.Ssdt
{
    public class Publisher
    {
        /// <summary>
        /// The path to the .dacpac file
        /// </summary>
        protected string DacpacPath;
        /// <summary>
        /// The path to the SSDT XML publish file, in order to create one, right click on your database project, click publish... set everything up and then click create profile
        /// </summary>
        protected string PublishFilePath;

        /// <summary>
        /// Creates the publisher instance
        /// </summary>
        /// <param name="dacpacPath">The path to the .dacpac file</param>
        /// <param name="publishFilePath">
        /// The path to the SSDT XML publish file, in order to create one, right click on your database project, click publish...
        /// set everything up and then click create profile
        /// </param>
        public Publisher(string dacpacPath, string publishFilePath)
        {
            
            DacpacPath = dacpacPath;
            PublishFilePath = publishFilePath;
        }

        /// <summary>
        /// Publishes the db using the supplied path to the .dacpac file and path to the .xml publish file
        /// </summary>
        public void Publish()
        {
            var pathToSqlpackageExe = GetPathToSqlpackageExe();
            if (!File.Exists(pathToSqlpackageExe))
            {
                throw new FileNotFoundException(string.Format("Sqlpackage.exe not found at path {0}, SSDT must be installed", pathToSqlpackageExe), "Sqlpackage.exe");
            }
            ValidatePaths();
            var commandString = GetPublishCommandString(pathToSqlpackageExe);

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Minimized,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = "cmd.exe",
                Arguments = commandString
            };
            var process = new Process {StartInfo = startInfo};
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Gets the command string to run in the process
        /// </summary>
        /// <param name="pathToSqlpackageExe"></param>
        /// <returns></returns>
        internal protected string GetPublishCommandString(string pathToSqlpackageExe)
        {
            var stringBuilder = new StringBuilder("/C \"");
            stringBuilder.Append(pathToSqlpackageExe);
            stringBuilder.Append("\" /a:publish ");
            stringBuilder.AppendFormat(@"/SourceFile:{0} ", DacpacPath);
            stringBuilder.AppendFormat(@"/Profile:{0}", PublishFilePath);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the path to the computers program files path where SSDT is installed
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the path to SSDT
        /// </summary>
        /// <returns></returns>
        internal protected virtual string GetPathToSqlpackageExe()
        {
            var programFilesPath = GetProgramfilesPath();
            return string.Format(@"{0}\Microsoft SQL Server\110\DAC\bin\sqlpackage.exe", programFilesPath);
        }

        /// <summary>
        /// Ensures that the paths passed in are valid
        /// </summary>
        internal protected virtual void ValidatePaths()
        {
            if (string.IsNullOrWhiteSpace(DacpacPath))
            {
                throw new ArgumentNullException("DacpacPath", "Dacpac Path cannot be null");
            }
            if (!File.Exists(DacpacPath))
            {
                throw new FileNotFoundException(string.Format("dacpac file not found at path: {0}", DacpacPath));
            }
            if (string.IsNullOrWhiteSpace(PublishFilePath))
            {
                throw new ArgumentNullException("PublishFilePath", "Publish File Path cannot be null");
            }
            if (!File.Exists(PublishFilePath))
            {
                throw new FileNotFoundException(string.Format("Publish file path file not found at path: {0}", PublishFilePath));
            }
        }
    }
}
