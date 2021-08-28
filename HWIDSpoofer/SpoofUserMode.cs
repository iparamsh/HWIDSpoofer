using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HWIDSpoofer
{
	class SpoofUserMode
	{
		private string generatedID;

		//generates random string
		private static Random random = new Random();
		public static string randomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		private string[] registryKeys = new string[] 
		{
			"Hardware\\Description\\System\\CentralProcessor\\0",
			"HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0",
			"SYSTEM\\CurrentControlSet\\Control\\SystemInformation",
			"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
			"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate",
			"SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0001",
			"SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}\\0012"
		};

		//WARNING: registryKeysValues.GetLength(0) equal to registryKeys.Length 
		//nop - means nothing, its like "no operation"
		private string[,] registryKeysValues = new string[,]
		{
			{"SystemProductName", "Identifier", "Previous Update Revision", "ProcessorNameString", "VendorIdentifier", "Platform Specific Field1", "Component Information"},
			{"SerialNumber", "Identifier", "SystemManufacturer", "nop", "nop", "nop", "nop"},
			{"ComputerHardwareId", "ComputerHardwareIds", "BIOSVendor", "ProductId", "ProcessorNameString", "BIOSReleaseDate", "nop"},
			{"ProductId", "InstallDate", "InstallTime", "nop", "nop", "nop", "nop"},
			{"SusClientId", "nop", "nop", "nop", "nop", "nop", "nop"},
			{"NetCfgInstanceId", "NetLuidIndex", "nop", "nop", "nop", "nop", "nop"},
			{"NetworkAddress", "NetCfgInstanceId", "NetworkInterfaceInstallTimestamp", "nop", "nop", "nop", "nop"}
		};

		public void spoofUserMode()
		{
			generatedID = randomString(20);
			for (int ctr = 0; ctr < registryKeys.Length; ctr++) 
			{
				spoofRegistryKey(ctr);
			}
		}

		private void spoofRegistryKey(int regKeyIndex) 
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeys[regKeyIndex], true);

			if (registryKey == null)
				return;

			for (int ctr = 0; ctr < registryKeysValues.GetLength(1); ctr++) 
			{
				if (registryKeysValues[regKeyIndex, ctr] == "nop")
					break;

				registryKey.SetValue(registryKeysValues[regKeyIndex, ctr], generatedID);
				generatedID = randomString(20);
			}

			registryKey.Close();
		}

		public string[] getSpoofingRegistryKeys() 
		{
			return registryKeys;
		}
		public string[,] getSpoofingRegistryKeyValues()
		{
			return registryKeysValues;
		}

	}
}
