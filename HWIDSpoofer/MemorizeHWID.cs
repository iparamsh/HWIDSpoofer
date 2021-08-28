/*using System;
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
		SpoofUserMode spoofUserMode = new SpoofUserMode();

		private string[] registryKeys = new string[0];
		private string[,] regestryKeyValues = new string[0, 0];

		public MemorizeHWID(string filePath)
		{
			this.filePath = filePath;
		}

		public void memorizeHWID()
		{
			if (!File.Exists(filePath))
			{
				var myFile = File.CreateText(filePath);
				myFile.Close();
			}

			this.registryKeys = spoofUserMode.getSpoofingRegistryKeys();
			this.regestryKeyValues = spoofUserMode.getSpoofingRegistryKeyValues();

			for (int ctr = 0; ctr < registryKeys.Length; ctr++)
			{
				memorizeRegistryKey(ctr);
			}

			File.WriteAllLines(filePath, memorizedIDs);
		}

		private void memorizeRegistryKey(int regKeyIndex)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryKeys[regKeyIndex], false);

			if (registryKey == null)
				return;

			for (int ctr = 0; ctr < regestryKeyValues.GetLength(1); ctr++) 
			{
				if (regestryKeyValues[regKeyIndex, ctr] == "nop")
					break;

				memorizedIDs.Add((string)registryKey.GetValue(regestryKeyValues[regKeyIndex, ctr]).ToString());
			}
			memorizedIDs.Add("new regKey");

			registryKey.Close();

		}

	}
}*/