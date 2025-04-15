using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using DeepCore.Client;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace DeepCore.ServerAPI
{
	// Token: 0x02000006 RID: 6
	internal class AuthAPI
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002828 File Offset: 0x00000A28
		public AuthAPI()
		{
			this.appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DPC", "DeepCoreClient");
			this.keyFilePath = Path.Combine(this.appDataFolder, "TONTOELQUELOLEA");
			if (!Directory.Exists(this.appDataFolder))
			{
				Directory.CreateDirectory(this.appDataFolder);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002894 File Offset: 0x00000A94
		public void Auth()
		{
			string hardwareID = AuthAPI.GetHardwareID();
			string ipv4Address = AuthAPI.GetIPv4Address();
			string ipv6Address = AuthAPI.GetIPv6Address();
			if (!File.Exists(this.keyFilePath))
			{
				Console.Clear();
				DeepConsole.LogConsole("Auth", "Enter your Key: ");
				this.AuthKey = Console.ReadLine().Trim();
				if (!Regex.IsMatch(this.AuthKey, "^KEY_[A-Za-z0-9]{60}$"))
				{
					DeepConsole.LogConsole("Auth", "Invalid Key format. Authentication failed.");
					Thread.Sleep(3000);
					Process.GetCurrentProcess().Kill();
				}
				if (!this.ValidateKey(this.AuthKey, ipv4Address, ipv6Address, hardwareID))
				{
					this.ReportUnauthorizedAttempt(this.AuthKey);
					Process.GetCurrentProcess().Kill();
				}
				File.WriteAllText(this.keyFilePath, this.AuthKey);
			}
			else
			{
				DeepConsole.LogConsole("Auth", "Connecting to server...");
				this.AuthKey = File.ReadAllText(this.keyFilePath).Trim();
				if (!Regex.IsMatch(this.AuthKey, "^KEY_[A-Za-z0-9]{60}$"))
				{
					DeepConsole.LogConsole("Auth", "Invalid Key format in key file. Authentication failed.");
					Thread.Sleep(3000);
					Process.GetCurrentProcess().Kill();
				}
				if (!this.ValidateKey(this.AuthKey, ipv4Address, ipv6Address, hardwareID))
				{
					this.ReportUnauthorizedAttempt(this.AuthKey);
					Process.GetCurrentProcess().Kill();
				}
			}
			if (this.isAuthenticated)
			{
				this.ReportSuccessfulAuth(this.AuthKey);
				LoggerAPI.OnPostprocessAllAssets();
				DeepConsole.LogConsole("Auth", "Authentication successful");
				string usernameFromKey = this.GetUsernameFromKey(this.AuthKey);
				DeepConsole.LogConsole("Auth", "Welcome, " + usernameFromKey + "!");
				Thread.Sleep(3000);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A34 File Offset: 0x00000C34
		private bool ValidateKey(string key, string ipv4, string ipv6, string hwid)
		{
			string text = "https://nigga.rest/where/is/biden/AUTHER/KeyValidator.php";
			NameValueCollection nameValueCollection = new NameValueCollection
			{
				{ "key", key },
				{ "ipv4", ipv4 },
				{ "ipv6", ipv6 },
				{ "hwid", hwid }
			};
			bool flag;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					byte[] array = webClient.UploadValues(text, "POST", nameValueCollection);
					JObject jobject = JObject.Parse(Encoding.UTF8.GetString(array));
					if (jobject["status"].ToString() == "authenticated")
					{
						this.isAuthenticated = true;
						flag = true;
					}
					else
					{
						Console.WriteLine(jobject["message"].ToString());
						flag = false;
					}
				}
				catch (WebException)
				{
					DeepConsole.LogConsole("Auth", "Error connecting to the server for key validation.");
					Thread.Sleep(3000);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B30 File Offset: 0x00000D30
		private void ReportUnauthorizedAttempt(string key)
		{
			string hardwareID = AuthAPI.GetHardwareID();
			string ipv4Address = AuthAPI.GetIPv4Address();
			string ipv6Address = AuthAPI.GetIPv6Address();
			string computerName = AuthAPI.GetComputerName();
			string userName = AuthAPI.GetUserName();
			string text = string.Concat(new string[]
			{
				"Unauthorized key attempt: HWID: ", hardwareID, ", IPV4: ", ipv4Address, ", IPV6: ", ipv6Address, ", PC: ", computerName, ", USER: ", userName,
				", Auth Key: ", key
			});
			this.SendDiscordWebhookNoAuth(text);
			DeepConsole.LogConsole("Auth", "Unauthorized key/banned user or server is unreachable");
			Thread.Sleep(3000);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BD8 File Offset: 0x00000DD8
		private void ReportSuccessfulAuth(string key)
		{
			string hardwareID = AuthAPI.GetHardwareID();
			string ipv4Address = AuthAPI.GetIPv4Address();
			string ipv6Address = AuthAPI.GetIPv6Address();
			string computerName = AuthAPI.GetComputerName();
			string userName = AuthAPI.GetUserName();
			string text = string.Concat(new string[]
			{
				"Authentication successful: HWID: ", hardwareID, ", IPV4: ", ipv4Address, ", IPV6: ", ipv6Address, ", PC: ", computerName, ", USER: ", userName,
				", Auth Key: ", key
			});
			this.SendDiscordWebhook(text);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C68 File Offset: 0x00000E68
		private void SendDiscordWebhook(string message)
		{
			string text = "https://nigga.rest/where/is/biden/AUTHER/WBHSender.php";
			NameValueCollection nameValueCollection = new NameValueCollection
			{
				{ "type", "auth" },
				{ "message", message }
			};
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.UploadValues(text, "POST", nameValueCollection);
				}
				catch (WebException)
				{
					DeepConsole.LogConsole("Auth", "Fatal error");
					Thread.Sleep(1000);
					DeepConsole.LogConsole("Auth", "Preventing application crash...");
					Thread.Sleep(3000);
					DeepConsole.LogConsole("Auth", "Closing in safe mode...");
					Thread.Sleep(3000);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D34 File Offset: 0x00000F34
		private void SendDiscordWebhookNoAuth(string message)
		{
			string text = "https://nigga.rest/where/is/biden/AUTHER/WBHSender.php";
			NameValueCollection nameValueCollection = new NameValueCollection
			{
				{ "type", "noAuth" },
				{ "message", message }
			};
			using (WebClient webClient = new WebClient())
			{
				try
				{
					webClient.UploadValues(text, "POST", nameValueCollection);
				}
				catch (WebException)
				{
					DeepConsole.LogConsole("Auth", "Fatal error");
					Thread.Sleep(1000);
					DeepConsole.LogConsole("Auth", "Preventing application crash...");
					Thread.Sleep(3000);
					DeepConsole.LogConsole("Auth", "Closing in safe mode...");
					Thread.Sleep(3000);
					Process.GetCurrentProcess().Kill();
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E00 File Offset: 0x00001000
		private string GetUsernameFromKey(string key)
		{
			string text = "https://nigga.rest/where/is/biden/AUTHER/UsernameManager.php";
			NameValueCollection nameValueCollection = new NameValueCollection { { "key", key } };
			string text2;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					byte[] array = webClient.UploadValues(text, "POST", nameValueCollection);
					text2 = Encoding.UTF8.GetString(array);
				}
				catch (WebException)
				{
					DeepConsole.LogConsole("Auth", "Error connecting to the server for key validation.");
					Thread.Sleep(3000);
					Process.GetCurrentProcess().Kill();
					text2 = null;
				}
			}
			return text2;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E9C File Offset: 0x0000109C
		public static string GetHardwareID()
		{
			string text = "SOFTWARE\\Microsoft\\Cryptography";
			string text2 = "MachineGuid";
			string text3;
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(text))
				{
					if (registryKey2 == null)
					{
						throw new KeyNotFoundException("Key Not Found: " + text);
					}
					object value = registryKey2.GetValue(text2);
					if (value == null)
					{
						throw new IndexOutOfRangeException("Index Not Found: " + text2);
					}
					text3 = value.ToString();
				}
			}
			return text3;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F38 File Offset: 0x00001138
		public static string GetIPv4Address()
		{
			string text = "https://api.ipify.org?format=text";
			string text2;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					text2 = webClient.DownloadString(text).Trim();
				}
				catch (WebException)
				{
					DeepConsole.LogConsole("Auth", "ERROR FETCHING IP");
					Process.GetCurrentProcess().Kill();
					text2 = "Unknown";
				}
			}
			return text2;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002FAC File Offset: 0x000011AC
		public static string GetIPv6Address()
		{
			string text = "https://api6.ipify.org?format=text";
			string text2;
			using (WebClient webClient = new WebClient())
			{
				try
				{
					text2 = webClient.DownloadString(text).Trim();
				}
				catch (WebException)
				{
					DeepConsole.LogConsole("Auth", "ERROR FETCHING IPV6");
					text2 = "Unknown";
				}
			}
			return text2;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003014 File Offset: 0x00001214
		public static string GetComputerName()
		{
			return Environment.MachineName;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000301B File Offset: 0x0000121B
		public static string GetUserName()
		{
			return Environment.UserName;
		}

		// Token: 0x04000034 RID: 52
		private const string KeyFormat = "^KEY_[A-Za-z0-9]{60}$";

		// Token: 0x04000035 RID: 53
		private bool isAuthenticated;

		// Token: 0x04000036 RID: 54
		private string AuthKey = "";

		// Token: 0x04000037 RID: 55
		private readonly string appDataFolder;

		// Token: 0x04000038 RID: 56
		private readonly string keyFilePath;
	}
}
