using System;
using System.IO;

namespace VideoConverter.Core
{
    internal class FFmpegScaleConverterArguments
    {
        private const String constantArgs = "-i {0} -vf scale=-1:{1} -c:v libx264 -crf 0 -preset veryslow -c:a copy {2}";
        public int Scale { get; set; }
        public string FileName { get; set; }
        public string Directory { get; internal set; }
        private string OutputName =>$"{Directory}{Path.GetFileNameWithoutExtension(FileName)}p{Scale}{Path.GetExtension(FileName)}";
        public override string ToString()
        {
            return String.Format(constantArgs,FileName,Scale,OutputName);
        }
    }

}
