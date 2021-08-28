using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HWIDSpoofer
{
    class ChangeVolumeID
    {

        public void changeVolumeID() 
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            process.StandardInput.WriteLine("cd C:/");
            process.StandardInput.Flush();
            process.StandardInput.WriteLine("start Volumeid.exe");
            process.StandardInput.Flush();

            for (int ctr = 0; ctr < drives.Length; ctr++) 
            {
                process.StandardInput.WriteLine("volumeid " +  drives[ctr].Name.Substring(0, 2) + " " + randomString(4) + "-" + randomString(4));
                process.StandardInput.Flush();
            }

            process.StandardInput.Close();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
        }

        private static Random random = new Random();
        public static string randomString(int length)
        {
            const string chars = "ABCDEF0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
