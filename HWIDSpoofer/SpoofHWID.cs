using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HWIDSpoofer
{
    class SpoofHWID
	{
		private string generatedID;

		public SpoofHWID(string generatedID) 
		{
			this.generatedID = generatedID;
		}
		public void takeAction() 
		{
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

			registryKey.SetValue("SystemProductName", generatedID);
			registryKey.SetValue("Identifier", generatedID);
			registryKey.SetValue("Previous Update Revision", generatedID);
			registryKey.SetValue("ProcessorNameString", generatedID);
			registryKey.SetValue("VendorIdentifier", generatedID);
			registryKey.SetValue("Platform Specific Field1", generatedID);
			registryKey.SetValue("Component Information", generatedID);
			registryKey.Close();
		}

		private void spoofHardDriveLUI0()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("SerialNumber", generatedID);
			registryKey.SetValue("Identifier", generatedID);
			registryKey.SetValue("SystemManufacturer", generatedID);
			registryKey.Close();
		}

		private void spoofCCS()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("ComputerHardwareId", "{" + generatedID + "}");
			registryKey.SetValue("ComputerHardwareIds", "{" + generatedID + "}" + "{" + generatedID + "}" + "{" + generatedID + "}" + "{" + generatedID + "}" + "{" + generatedID + "}" + "{" + generatedID + "}" + "{" + generatedID + "}");

			registryKey.Close();
		}
		private void spoofBIOS()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("BIOSVendor", generatedID);
			registryKey.SetValue("ProductId", generatedID);
			registryKey.SetValue("ProcessorNameString", generatedID);
			registryKey.SetValue("BIOSReleaseDate", random.Next(1, 12) + "/" + random.Next(1, 31) +"/"+ random.Next(2015, 2021));

			registryKey.Close();
		}
		private void spoofIdentifier()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("ProductId", generatedID);
			registryKey.SetValue("InstallDate", generatedID); //dont know if it will work
			registryKey.SetValue("InstallTime", generatedID); //dont know if it will work

			registryKey.Close();
		}
		private void spoofWinUpdate()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("SusClientId", generatedID);

			registryKey.Close();
		}
		private void spoofIdentifier1()
		{
			var random = new Random();

			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0001", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("NetCfgInstanceId", generatedID);
			registryKey.SetValue("NetLuidIndex", "{" + generatedID + "}"); //dont know if will work

			registryKey.Close();
		}
		private void spoofMAC()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0012", true);

			if (registryKey == null)
				return;

			registryKey.SetValue("NetworkAddress", generatedID);
			registryKey.SetValue("NetCfgInstanceId", generatedID);

			registryKey.Close();
		}
	}
}
