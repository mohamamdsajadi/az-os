using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("plug your usb device...");

        Thread backgroundThread = new Thread(CheckForRemovableDisk);
        backgroundThread.IsBackground = true;
        backgroundThread.Start();
        Console.ReadLine();
    }

    private static void CheckForRemovableDisk()
    {
        while (true)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Removable && drive.IsReady)
                {
                    Process.Start("mspaint.exe");
                    return;
                }
            }
            Thread.Sleep(1000);
        }
    }
}