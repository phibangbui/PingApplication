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
            ni.Icon = Resources.goodlogo;
            ni.Visible = true;
            ni.ContextMenuStrip = new ContextMenus().Create();
        }

        public void Dispose() 
        {
            ni.Dispose();
        }
        

    }
}
