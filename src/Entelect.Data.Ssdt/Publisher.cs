using System;

namespace Entelect.Data.Ssdt
{
    public class Publisher
    {

        public void Publish()
        {
            
        }

        internal protected virtual string GetProgramfilesPath()
        {
            var x86Path = Environment.GetEnvironmentVariable("programfiles(x86)");
            if (!string.IsNullOrWhiteSpace(x86Path))
                return x86Path;
            var x64Path = Environment.GetEnvironmentVariable("programfiles");
            return x64Path;
        }
    }
}
