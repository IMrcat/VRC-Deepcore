using System;
using System.Text;
using DeepCore.Client.Misc;
using ExitGames.Client.Photon;

namespace DeepCore.Client.Module.Photon
{
	// Token: 0x0200004C RID: 76
	internal class PhtonManagerUtils
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000BE18 File Offset: 0x0000A018
		public static bool PhotonEvent(EventData param_1)
		{
			if (PhtonManagerUtils.isdebugtime)
			{
				DeepConsole.LogConsole("Photon", "--------------------------------------------------");
				DeepConsole.LogConsole("Photon", string.Format("Event Code:{0}", param_1.Code));
				DeepConsole.LogConsole("Photon", string.Format("Event type:{0}", param_1.Sender));
				DeepConsole.LogConsole("Photon", string.Format("Sender:{0}", param_1.Sender));
				DeepConsole.LogConsole("Photon", string.Format("SenderKey:{0}", param_1.SenderKey));
				DeepConsole.LogConsole("Photon", string.Format("Parameters:{0}", param_1.Parameters));
				DeepConsole.LogConsole("Photon", string.Format("Pointer:{0}", param_1.Pointer));
				DeepConsole.LogConsole("Photon", "Data:" + PhtonManagerUtils.PrintByteArray(SerializationUtil.ToByteArray(param_1.CustomData)));
				DeepConsole.LogConsole("Photon", "--------------------------------------------------");
			}
			if (param_1.Code == 33)
			{
				return ModerationNotice.OnEventPatch(param_1);
			}
			ChatBoxLogger.OnEvent(param_1);
			return true;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000BF40 File Offset: 0x0000A140
		public static string PrintByteArray(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString() + ", ");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x040000DA RID: 218
		public static bool isdebugtime;
	}
}
