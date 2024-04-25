using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace ConcurrentFileDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> links = new List<string>
            {
                "https://dls.music-fa.com/tagdl/1402/Unknown%20Artist%20-%20Babaei%20To%20Bazigooshi%20(320).mp3",
                "https://dls.music-fa.com/song/alibz/1403/Unknown%20Artist%20-%20Babaei%20Sheytoon%20Bala%20(320).mp3",
                "https://dls.music-fa.com/song/alibz/1403/Unknown%20Artist%20-%20Dige%20Vaghteshe%20Bargardi%20(320).mp3"
            };

            DownloadFiles(links);

            Console.WriteLine("Downloded successfully");
            Console.ReadKey();
        }

        static void DownloadFiles(List<string> links)
        {
            List<Thread> threads = new List<Thread>();

            foreach (string link in links)
            {
                Thread thread = new Thread(() =>
                {
                    string fileName = GetFileNameFromUrl(link);
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(link, fileName);
                    }

                    Console.WriteLine($"Downloaded: {fileName}");
                });

                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        static string GetFileNameFromUrl(string url)
        {
            Uri uri = new Uri(url);
            return uri.Segments[^1];
        }
    }
}