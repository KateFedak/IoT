using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
    public class MySingletone
    {
        public static ManualResetEvent manualResetEvent = new(false);
        public static Queue<Weather> qwQueue = new Queue<Weather>();
        public static Timer time = new Timer(BackGroundTask, manualResetEvent, 5000, 5000);
        public static Weather lastWeather = new Weather();
        private static object writeLock = new object();

        public static void BackGroundTask(Object stateInfo)
        {
            if (!qwQueue.TryDequeue(out lastWeather))
            {
                manualResetEvent.Reset();

                lock (writeLock)
                {
                    foreach (var weather in qwQueue)
                    {
                        File.AppendAllTextAsync(@"C:\Users\kateryna.fedak\Iot\WebApi_IoT\bin\Debug\net5.0\information.txt", weather.Id + " " + weather.Name + " " + weather.TemperatureC + "\n");
                    }
                }

                qwQueue.Clear();
            }
            manualResetEvent.Set();
        }
    }
}
