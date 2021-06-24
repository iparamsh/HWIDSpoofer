using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace HWIDSpoofer
{
    class MemorizeHWID
    {
        private string filePath;
		private List<string> memorizedIDs = new List<string>();

        public MemorizeHWID(string filePath) 
        {
            this.filePath = filePath;
        }

        public void memorizeHWID() 
        {
			memorizeCentralProcessor();
			memorizeHardDriveLUI0();
			memorizeCCS();
			memorizeBIOS();
			memorizeIdentifier();
			memorizeIdentifier1();
			memorizeWinUpdate();
			memorizeMAC();
        }

		private void memorizeCentralProcessor()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Hardware\\Description\\System\\CentralProcessor\\0", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("SystemProductName"));
			memorizedIDs.Add((string)registryKey.GetValue("Identifier"));
			memorizedIDs.Add((string)registryKey.GetValue("Previous Update Revision"));
			memorizedIDs.Add((string)registryKey.GetValue("ProcessorNameString"));
			memorizedIDs.Add((string)registryKey.GetValue("VendorIdentifier"));
			memorizedIDs.Add((string)registryKey.GetValue("Platform Specific Field1"));
			memorizedIDs.Add((string)registryKey.GetValue("Component Information"));
			registryKey.Close();

		}

		private void memorizeHardDriveLUI0()
		{
			memorizedIDs.Add("");

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string) registryKey.GetValue("SerialNumber"));
			memorizedIDs.Add((string)registryKey.GetValue("Identifier"));
			memorizedIDs.Add((string)registryKey.GetValue("SystemManufacturer"));
			registryKey.Close();
		}

		private void memorizeCCS()
		{
			memorizedIDs.Add("");

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("ComputerHardwareId"));
			memorizedIDs.Add((string)registryKey.GetValue("ComputerHardwareIds"));

			registryKey.Close();
		}
		private void memorizeBIOS()
		{
			memorizedIDs.Add("");

			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("BIOSVendor"));
			memorizedIDs.Add((string)registryKey.GetValue("ProductId"));
			memorizedIDs.Add((string)registryKey.GetValue("ProcessorNameString"));
			memorizedIDs.Add((string)registryKey.GetValue("BIOSReleaseDate"));

			registryKey.Close();
		}
		private void memorizeIdentifier()
		{
			memorizedIDs.Add("");

			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("ProductId"));
			memorizedIDs.Add((string)registryKey.GetValue("InstallDate"));
			memorizedIDs.Add((string)registryKey.GetValue("InstallTime")); 

			registryKey.Close();
		}
		private void memorizeWinUpdate()
		{
			memorizedIDs.Add("");

			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("SusClientId"));

			registryKey.Close();
		}
		private void memorizeIdentifier1()
		{
			memorizedIDs.Add("");

			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0001", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("NetCfgInstanceId"));
			memorizedIDs.Add((string)registryKey.GetValue("NetLuidIndex")); 

			registryKey.Close();
		}
		private void memorizeMAC()
		{
			memorizedIDs.Add("");

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0012", true);

			if (registryKey == null)
				return;

			memorizedIDs.Add((string)registryKey.GetValue("NetworkAddress"));
			memorizedIDs.Add((string)registryKey.GetValue("NetCfgInstanceId"));

			registryKey.Close();

			File.WriteAllLines(filePath, memorizedIDs);
		}
	}
}
