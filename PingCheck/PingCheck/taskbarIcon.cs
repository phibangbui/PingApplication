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
        public taskbarIcon(){
            ni = new NotifyIcon();
        }

        // Displays icon in system tray and populates menu with options.
        public void Display(){

            ni.Icon = Resources.noconnectionlogo;
            ni.Text = "Ping application";
            ni.Visible = true;

            ni.ContextMenuStrip = new ContextMenus().Create();
        }

        public void Dispose() {
            ni.Dispose();
        }

        public void changeIcon(int ping)
        {
            string connection = "no connection";
            if (ping >= 0)
                connection = "bad";
            if (ping <= 200)
                connection = "okay";
            if (ping <= 100)
                connection = "good";
            switch(connection){
                case "good":
                    ni.Icon = Resources.goodlogo;
                    break;
                case "okay":
                    ni.Icon = Resources.okaylogo;
                    break;
                case "bad":
                    ni.Icon = Resources.badlogo;
                    break;
                case "no connection":
                    ni.Icon = Resources.noconnectionlogo;
                    break;
                default:
                    ni.Icon = Resources.noconnectionlogo;
                    break;
            }
        }

    }
}
