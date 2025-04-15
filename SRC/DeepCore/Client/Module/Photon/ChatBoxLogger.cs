using System;
using System.Linq;
using System.Text;
using DeepCore.Client.Misc;
using ExitGames.Client.Photon;

namespace DeepCore.Client.Module.Photon
{
	// Token: 0x02000047 RID: 71
	internal class ChatBoxLogger
	{
		// Token: 0x0600019F RID: 415 RVA: 0x0000B83C File Offset: 0x00009A3C
		internal static bool OnEvent(EventData param_1)
		{
			if (ChatBoxLogger.isEnabled && param_1.Code == 43)
			{
				byte[] array = SerializationUtil.ToByteArray(param_1.CustomData);
				DeepConsole.LogConsole("Photon", "Someone say: " + ChatBoxLogger.ConvertBytesToText(array));
			}
			return true;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B884 File Offset: 0x00009A84
		public static string PrintByteArray(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder("");
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		public static string ConvertBytesToText(byte[] bytes)
		{
			string text2;
			try
			{
				string text = Encoding.UTF8.GetString(bytes).TrimEnd(new char[1]);
				text = new string(text.Where((char c) => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray<char>());
				if (text.Any((char c) => char.IsControl(c) && !char.IsWhiteSpace(c)))
				{
					text2 = "[Non-printable characters detected]";
				}
				else
				{
					text2 = text;
				}
			}
			catch
			{
				text2 = "[Invalid Encoding]";
			}
			return text2;
		}

		// Token: 0x040000D6 RID: 214
		public static bool isEnabled;
	}
}
