using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DeepCore.Client.Misc
{
	// Token: 0x02000077 RID: 119
	internal class MonoDumper
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public static void StartDump()
		{
			Console.WriteLine("[MonoDumper] StartDump initiated.");
			if (!MonoDumper.DumpDLL)
			{
				Console.WriteLine("[MonoDumper] DumpDLL is set to false. Exiting method.");
				return;
			}
			string text = Environment.CurrentDirectory + "\\DeepClient\\MonoDumps";
			string text2 = text + "\\MonoDump.txt";
			Console.WriteLine("[MonoDumper] Dump folder path: " + text);
			Console.WriteLine("[MonoDumper] Dump file path: " + text2);
			try
			{
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
					Console.WriteLine("[MonoDumper] Created dump folder.");
				}
				if (File.Exists(text2))
				{
					File.Delete(text2);
					Console.WriteLine("[MonoDumper] Deleted existing dump file.");
				}
				using (new FileStream(text2, FileMode.Create))
				{
					Console.WriteLine("[MonoDumper] Created new dump file.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("[MonoDumper] Error handling dump files: " + ex.Message);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				string text3 = Environment.CurrentDirectory + "\\MelonLoader\\Managed\\Assembly-CSharp.dll";
				Console.WriteLine("[MonoDumper] Loading assembly from: " + text3);
				foreach (Type type in Assembly.LoadFile(text3).GetTypes())
				{
					stringBuilder.Append("\n\n*********************************************************************");
					stringBuilder.Append("Class: " + type.Name + Environment.NewLine);
					Console.WriteLine("[MonoDumper] Processing class: " + type.Name);
					try
					{
						MemberInfo[] array = type.GetMethods();
						foreach (MemberInfo memberInfo in array)
						{
							stringBuilder.Append("Method: " + memberInfo.Name + Environment.NewLine);
							Console.WriteLine("[MonoDumper] Found method: " + memberInfo.Name);
						}
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							stringBuilder.Append("Property: " + propertyInfo.Name + Environment.NewLine);
							Console.WriteLine("[MonoDumper] Found property: " + propertyInfo.Name);
						}
					}
					catch (Exception ex2)
					{
						stringBuilder.Append("Failed: " + ex2.Message + "\n\n");
						Console.WriteLine("[MonoDumper] Error processing class members: " + ex2.Message);
					}
					stringBuilder.Append("*********************************************************************\n\n");
				}
			}
			catch (Exception ex3)
			{
				Console.WriteLine("[MonoDumper] Error loading assembly or iterating types: " + ex3.Message);
				return;
			}
			try
			{
				File.AppendAllText(text2, stringBuilder.ToString());
				Console.WriteLine("[MonoDumper] Dump successfully written to file.");
			}
			catch (Exception ex4)
			{
				Console.WriteLine("[MonoDumper] Error writing to dump file: " + ex4.Message);
			}
			Console.WriteLine("All dumped!");
			Console.ReadLine();
		}

		// Token: 0x0400015B RID: 347
		public static bool DumpDLL = true;
	}
}
