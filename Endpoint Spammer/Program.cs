using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Collections.Specialized;

namespace Api_Spam {
    internal class Program {
        static Random rnd = new Random();

        static void Main() {
            Console.Title = "Skid Spammer 90000";
            string[] _Prox = File.ReadAllLines("proxy.txt");
            string[] _mesg = File.ReadAllLines("Mesage.txt");

            while (true) {
                string pxy = _Prox[rnd.Next(_Prox.Length)].Trim();
                string msg = _mesg[rnd.Next(_mesg.Length)].Trim();

                try {
                    using (WebClient web = new WebClient()) {
                        if (pxy.StartsWith("http")) {
                            web.Proxy = new WebProxy(pxy);
                        } else if (pxy.StartsWith("socks")) {
                            return; // Le skip p-p
                        } else {
                            return; // Just incase yall are stupid
                        }

                        web.UploadValues("https://nigga.rest/where/is/biden/AUTHER/WBHSender.php", "POST", new NameValueCollection {
                          { "type", "noAuth" },
                          { "message", msg }
                        });

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"POST|SENT\nMSG: {msg} -> PROXY: {pxy}");
                        Console.ResetColor();
                    }
                } catch (Exception e) {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"ERROR|PROXY\n{pxy} -> {e}");
                    Console.ReadKey();
                }
                Thread.Sleep(110);
            }  
        }
    }
}
