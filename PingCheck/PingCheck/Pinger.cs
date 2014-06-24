using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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

            if (website != null)
            {
                if (HasConnection())
                {
                    IPAddress[] address = Dns.GetHostAddresses(website);
                    for (int i = 0; i < 4; i++)
                    {
                        PingReply pingReply = ping.Send(address[0], 1000, buffer, pingOptions);
                        if (!(pingReply == null))
                        {
                            if (pingReply.Status == IPStatus.Success)
                            {
                                roundtripholder[i] = (int) pingReply.RoundtripTime;
                                returnMessage = string.Format("Reply from {0}: bytes={1} time={2}ms TTL={3}", pingReply.Address, pingReply.Buffer.Length, pingReply.RoundtripTime, pingReply.Options.Ttl);
                            }
                            else
                            {
                                returnMessage = "Ping timed out";
                            }
                        }
                        else
                            returnMessage = "Connection failed for an unknown reason...";

                    }
                    average = (int) roundtripholder.Average();
                    taskIcon.changeIcon(average);
                }
            }
        }

        private bool HasConnection()
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
                        return result;
                    }  
                }
                catch { }
                taskIcon.Display();
                return result;
            }
        }
}


