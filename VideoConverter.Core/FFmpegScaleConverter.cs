using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VideoConverter.Core
{
    public class FFmpegScaleConverter
    {
        private readonly FFmpegScaleConverterArguments _FFmpegScaleConverterArguments = new FFmpegScaleConverterArguments();
        public static FFmpegScaleConverter ConvertFromFileName(String fileName)
        {
            return new FFmpegScaleConverter(fileName);
        }
        private FFmpegScaleConverter (String fileName)
        {            
            _FFmpegScaleConverterArguments.FileName = fileName;
        }        
        public FFmpegScaleConverter ToScale(int scale)
        {
            _FFmpegScaleConverterArguments.Scale = scale;
            return this;
        }
        public FFmpegScaleConverter ToOutPutDirectory(String directory)
        {
            _FFmpegScaleConverterArguments.Directory = directory;
            return this;
        }
        public async Task Run()
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(_FFmpegScaleConverterArguments.Directory))
                    Directory.CreateDirectory(_FFmpegScaleConverterArguments.Directory);
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.FileName = "ffmpeg";
                ps.Arguments = _FFmpegScaleConverterArguments.ToString();
                Process p = new Process();
                p.StartInfo = ps;
                p.Start();
                //p.WaitForExit();
            });
        }
        
    }

}
