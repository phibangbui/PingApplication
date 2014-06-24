using PingCheck.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingCheck
{
    class ContextMenus
    {

        public ContextMenuStrip Create()
        {
            // Add the default menu options.

            string[] siteArray = new string[Properties.Settings.Default.websites.Count];
            Properties.Settings.Default.websites.CopyTo(siteArray, 0);
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;


            // Websites 
            item = new ToolStripMenuItem();
            foreach (string site in siteArray) {
                ToolStripMenuItem subitem = new ToolStripMenuItem(site);
                subitem.Click += new EventHandler(Site_Handler);
                item.DropDown.Items.Add(subitem);
            }
            item.Text = "Websites";
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(Exit_Click);
            menu.Items.Add(item);

            return menu;
        }

        /// <summary>
        /// Changes the uri to ping to website
        /// </summary>
        /// <param name="sender">The submenuitems site name</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Site_Handler(object sender, EventArgs e)
        {
            Pinger.website = sender.ToString();

        }

        /// <summary>
        /// Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
        {
            // Quit without further ado.
            Application.Exit();
        }



    }
}
