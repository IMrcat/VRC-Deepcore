using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DeepCore.Client.Patching
{
	// Token: 0x0200001B RID: 27
	internal class FakeHWIDPatch
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000509D File Offset: 0x0000329D
		public static void Patch()
		{
			EasyPatching.EasyPatchMethodPost(typeof(SystemInfo), "deviceUniqueIdentifier", typeof(FakeHWIDPatch), "FakeHWID");
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000050C4 File Offset: 0x000032C4
		public static bool FakeHWID(ref string __result)
		{
			if (FakeHWIDPatch.newHWID == string.Empty)
			{
				FakeHWIDPatch.newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
				{
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9),
					new Random().Next(0, 9)
				}))).Select(delegate(byte x)
				{
					byte b = x;
					return b.ToString("x2");
				})
					.Aggregate((string x, string y) => x + y);
				DeepConsole.Log("Patch", "[Spoofer]: Success Patched HWID {newHWID}");
			}
			__result = FakeHWIDPatch.newHWID;
			return false;
		}

		// Token: 0x04000054 RID: 84
		public static string newHWID = "";
	}
}
