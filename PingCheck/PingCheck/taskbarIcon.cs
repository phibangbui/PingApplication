using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PingCheck.Properties;
using System.Diagnostics;
using System.Timers;

namespace PingCheck
{
    
    class taskbarIcon : IDisposable
    {
        // Fields for taskbarIcon object
        private NotifyIcon ni;
        public bool highping = false;
        public bool showNotification = true;

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

        public void changeIcon(int ping, String website)
        {
            if (!Pinger.connectiontosite)
            {
                ni.Icon = Resources.noconnectionlogo;
                ni.Text = "Could not contact website";
            }
            else if (ping >= 200)
            {
                ni.Icon = Resources.badlogo;
                ni.Text = Pinger.average + "ms to " + website;
                highping = true;
                this.pingNotification(highping);
            }
            else if (ping < 200 && ping > 100)
            {
                ni.Icon = Resources.okaylogo;
                ni.Text = Pinger.average + "ms to " + website;
                highping = false;
            }
            else if (ping <= 100)
            {
                ni.Icon = Resources.goodlogo;
                ni.Text = Pinger.average + "ms to " + website;
                highping = false;
            }
        }

        public void pingNotification(bool highping)
        {
            if (highping == true && showNotification == true)
            {
                ni.BalloonTipText = "High Ping!: " + Pinger.average + "ms";
                ni.ShowBalloonTip(6000);
                ni.BalloonTipClicked += new EventHandler(dismissPing);
            }
        }

        public void dismissPing(object sender, EventArgs e)
        {
            showNotification = false;
            System.Timers.Timer sleepTimer = new System.Timers.Timer();
            sleepTimer.Interval = 300000;
            sleepTimer.Enabled = true;
            sleepTimer.Elapsed += new ElapsedEventHandler(sleepPing);
            sleepTimer.Start();
        }

        public void sleepPing(object source, ElapsedEventArgs e)
        {
            showNotification = true;
            ((System.Timers.Timer)source).Close();
        }

    }
}
