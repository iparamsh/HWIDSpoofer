using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace HWIDSpoofer
{
    class RestoreHWIDKeys
    {
        private string filePath;
        private List<string> memorizedIDs = new List<string>();
        public RestoreHWIDKeys(string filePath) 
        {
            this.filePath = filePath;
        }

        public void restoreHWID() 
        {
            memorizedIDs = File.ReadAllLines(filePath).ToList();

			spoofCentralProcessor();
			spoofHardDriveLUI0();
			spoofCCS();
			spoofBIOS();
			spoofIdentifier();
			spoofWinUpdate();
			spoofIdentifier1();
			spoofMAC();
		}

		private void spoofCentralProcessor()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Hardware\\Description\\System\\CentralProcessor\\0", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("SystemProductName", memorizedIDs[0]);
			registryKey.SetValue("Identifier", memorizedIDs[1]);
			registryKey.SetValue("Previous Update Revision", memorizedIDs[2]);
			registryKey.SetValue("ProcessorNameString", memorizedIDs[3]);
			registryKey.SetValue("VendorIdentifier", memorizedIDs[4]);
			registryKey.SetValue("Platform Specific Field1", memorizedIDs[5]);
			registryKey.SetValue("Component Information", memorizedIDs[6]);
			registryKey.Close();
		}

		private void spoofHardDriveLUI0()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("SerialNumber", memorizedIDs[8]);
			registryKey.SetValue("Identifier", memorizedIDs[9]);
			registryKey.SetValue("SystemManufacturer", memorizedIDs[10]);
			registryKey.Close();
		}

		private void spoofCCS()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("ComputerHardwareId", memorizedIDs[12]);
			registryKey.SetValue("ComputerHardwareIds", memorizedIDs[13]);

			registryKey.Close();
		}
		private void spoofBIOS()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("BIOSVendor", memorizedIDs[15]);
			registryKey.SetValue("ProductId", memorizedIDs[16]);
			registryKey.SetValue("ProcessorNameString", memorizedIDs[17]);
			registryKey.SetValue("BIOSReleaseDate", memorizedIDs[18]);

			registryKey.Close();
		}
		private void spoofIdentifier()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("ProductId", memorizedIDs[20]);
			registryKey.SetValue("InstallDate", memorizedIDs[21]); //dont know if it will work
			registryKey.SetValue("InstallTime", memorizedIDs[22]); //dont know if it will work

			registryKey.Close();
		}
		private void spoofWinUpdate()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("SusClientId", memorizedIDs[24]);

			registryKey.Close();
		}
		private void spoofIdentifier1()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0001", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("NetCfgInstanceId", memorizedIDs[26]);
			registryKey.SetValue("NetLuidIndex", memorizedIDs[27]); //dont know if will work

			registryKey.Close();
		}
		private void spoofMAC()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0012", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("NetworkAddress", memorizedIDs[29]);
			registryKey.SetValue("NetCfgInstanceId", memorizedIDs[29]);

			registryKey.Close();
		}
	}
}
