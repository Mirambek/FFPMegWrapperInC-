using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace VideoConverter.Core
{
    public class VideoContext
    {
        private readonly static int[] scales = new int[] { 720, 360, 480 };
        public static void ScaleVideos(string fileName)
        {
            List<Task> tasks= new List<Task>();
            string directory = $"./output{DateTime.Now.Hour}" +
                     $"{DateTime.Now.Minute}" +
                     $"{DateTime.Now.Millisecond}/";
            Array.ForEach(scales, (v) =>
             {
                 //String fileName = "http://techslides.com/demos/sample-videos/small.mp4";
                 Task t = FFmpegScaleConverter
                 .ConvertFromFileName(fileName)
                 .ToScale(v)
                 .ToOutPutDirectory(directory)
                 .Run();
                 tasks.Add(t);

             });
            Task.WaitAll(tasks.ToArray());
        }
    }
}
