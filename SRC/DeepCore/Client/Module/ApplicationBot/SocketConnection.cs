using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeepCore.Client.Module.ApplicationBot
{
	// Token: 0x02000073 RID: 115
	internal class SocketConnection
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		public static void SendCommandToClients(string Command)
		{
			DeepConsole.Log("Server", string.Format("Sending Message ({0})", Command));
			foreach (Socket socket in SocketConnection.ServerHandlers.Where((Socket s) => s != null).ToList<Socket>())
			{
				try
				{
					socket.Send(Encoding.ASCII.GetBytes(Command));
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000F554 File Offset: 0x0000D754
		public static void OnClientReceiveCommand(string Command)
		{
			DeepConsole.Log("Bot", string.Format("Received Message ({0})", Command));
			Bot.ReceiveCommand(Command);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000F571 File Offset: 0x0000D771
		public static void StartServer()
		{
			SocketConnection.ServerHandlers.Clear();
			Task.Run(new Action(SocketConnection.HandleServer));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000F590 File Offset: 0x0000D790
		public static void StopServer()
		{
			SocketConnection._cancellationTokenSource.Cancel();
			Socket listenerSocket = SocketConnection._listenerSocket;
			if (listenerSocket != null)
			{
				listenerSocket.Close();
			}
			SocketConnection._listenerSocket = null;
			List<Socket> serverHandlers = SocketConnection.ServerHandlers;
			lock (serverHandlers)
			{
				foreach (Socket socket in SocketConnection.ServerHandlers)
				{
					socket.Close();
				}
				SocketConnection.ServerHandlers.Clear();
			}
			DeepConsole.Log("Server", "Server stopped.");
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000F640 File Offset: 0x0000D840
		private static void HandleServer()
		{
			IPAddress ipaddress = Dns.GetHostEntry("localhost").AddressList[0];
			IPEndPoint ipendPoint = new IPEndPoint(ipaddress, 11000);
			try
			{
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				socket.Bind(ipendPoint);
				socket.Listen(10);
				DeepConsole.Log("Server", "Waiting for connections...");
				for (;;)
				{
					SocketConnection.ServerHandlers.Add(socket.Accept());
				}
			}
			catch (Exception ex)
			{
				DeepConsole.Log("Server", ex.ToString());
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public static void Client()
		{
			Task.Run(new Action(SocketConnection.HandleClient));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		public static void HandleClient()
		{
			byte[] array = new byte[1024];
			try
			{
				IPAddress ipaddress = Dns.GetHostEntry("localhost").AddressList[0];
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, 11000);
				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					socket.Connect(ipendPoint);
					DeepConsole.Log("Bot", "Socket connected to {0}" + socket.RemoteEndPoint.ToString());
					for (;;)
					{
						int num = socket.Receive(array);
						SocketConnection.OnClientReceiveCommand(Encoding.ASCII.GetString(array, 0, num));
					}
				}
				catch (ArgumentNullException ex)
				{
					DeepConsole.Log("Bot", "ArgumentNullException : {0}" + ex.ToString());
				}
				catch (SocketException ex2)
				{
					DeepConsole.Log("Bot", "SocketException : {0}" + ex2.ToString());
				}
				catch (Exception ex3)
				{
					DeepConsole.Log("Bot", "Unexpected exception : {0}" + ex3.ToString());
				}
			}
			catch (Exception ex4)
			{
				DeepConsole.Log("Bot", ex4.ToString());
			}
		}

		// Token: 0x0400014E RID: 334
		private static List<Socket> ServerHandlers = new List<Socket>();

		// Token: 0x0400014F RID: 335
		private static Socket _listenerSocket;

		// Token: 0x04000150 RID: 336
		private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		// Token: 0x04000151 RID: 337
		public static List<Socket> ServerHandler = new List<Socket>();
	}
}
