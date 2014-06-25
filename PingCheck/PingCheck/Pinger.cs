using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PingCheck
{
    class Pinger
    {
        const int PING_INTERVAL = 4000;
        const bool SEND_BOOL = true;
        public static String website;
        public static int average;
        public static bool connectiontosite;

        taskbarIcon taskIcon = new taskbarIcon();

        public Pinger()
        {
            taskIcon.Display();
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(pingSite);
            myTimer.Interval = PING_INTERVAL;
            myTimer.Enabled = SEND_BOOL;
        }

        private void pingSite(object source, ElapsedEventArgs e)
        {
            PingOptions pingOptions = new PingOptions(128, true);
            Ping ping = new Ping();
            byte[] buffer = new byte[32];
            string returnMessage = string.Empty;
            int[] roundtripholder = new int[4];

            if (website != null && CheckConnection())
            {
                    try
                    {
                        IPAddress[] address = Dns.GetHostAddresses(website);
                        for (int i = 0; i < 4; i++)
                        {
                            PingReply pingReply = ping.Send(address[0], 1000, buffer, pingOptions);
                            if (!(pingReply == null))
                            {
                                if (pingReply.Status == IPStatus.Success)
                                {
                                    roundtripholder[i] = (int)pingReply.RoundtripTime;
                                    returnMessage = string.Format("Reply from {0}: bytes={1} time={2}ms TTL={3}", pingReply.Address, pingReply.Buffer.Length, pingReply.RoundtripTime, pingReply.Options.Ttl);
                                    connectiontosite = true;
                                }
                                else
                                {
                                    returnMessage = "Ping timed out";
                                    connectiontosite = false;
                                }
                            }
                            else
                            {
                                returnMessage = "Connection failed for an unknown reason...";
                                connectiontosite = false;
                            }
                        }
                        average = (int)roundtripholder.Average();
                        taskIcon.changeIcon(average);
                    }
                    catch
                    {
                        returnMessage = "Could not find IP Address";
                        connectiontosite = false;
                        taskIcon.changeIcon(0);
                    }
            }
        }

        private bool CheckConnection()
        {
            Uri url = new Uri("http://google.ca/");
                string pingurl = string.Format("{0}", url.Host);
                string host = pingurl;
                bool result = false;
                Ping p = new Ping();
                try
                {
                    PingReply reply = p.Send(host, 3000);
                    if (reply.Status == IPStatus.Success)
                    {
                        result = true;
                    }  
                }
                catch { }
                taskIcon.Display();
                return result;
            }
        }
}


