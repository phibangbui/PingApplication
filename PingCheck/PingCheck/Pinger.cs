using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PingCheck
{
    class Pinger
    {
        const int PING_INTERVAL = 10000;
        const bool SEND_BOOL = true;
        Timer myTimer;
        public String website;
        public String returnMessage;
        public Pinger()
        {
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

            if (HasConnection())
            {
                for (int i = 0; i < 4; i++)
                {
                    PingReply pingReply = ping.Send(website, 1000, buffer, pingOptions);
                    if (!(pingReply == null))
                    {
                        if (pingReply.Status == IPStatus.Success)
                        {
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
            }
        }


        private bool HasConnection()
        {
            return true;
        }
    }
}

