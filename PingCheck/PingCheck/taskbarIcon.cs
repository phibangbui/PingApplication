using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PingCheck.Properties;
using System.Diagnostics;

namespace PingCheck
{
    
    class taskbarIcon : IDisposable
    {
        // Fields for taskbarIcon object
        private NotifyIcon ni;

        // Constructor
        public taskbarIcon()
        {
            ni = new NotifyIcon();
        }

        // Displays icon in system tray and populates menu with options.
        public void Display()
        {
            ni.Icon = Resources.noconnectionlogo;
            ni.Visible = true;
            ni.ContextMenuStrip = new ContextMenus().Create();
        }

        public void Dispose() 
        {
            ni.Dispose();
        }
        public void changeIcon(int ping)
        {
            if (!Pinger.connectiontosite)
            {
                ni.Icon = Resources.noconnectionlogo;
                ni.Text = "Could not contact website";
            }
            else if (ping >= 200)
            {
                ni.Icon = Resources.badlogo;
                ni.Text = Pinger.average + "ms";

            }
            else if (ping < 200 && ping > 100)
            {
                ni.Icon = Resources.okaylogo;
                ni.Text = Pinger.average + "ms";
            }
            else if (ping <= 100)
            {
                ni.Icon = Resources.goodlogo;
                ni.Text = Pinger.average + "ms";
            }
        }
        

    }
}
