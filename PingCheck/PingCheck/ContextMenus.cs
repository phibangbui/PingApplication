using PingCheck.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingCheck
{
    class ContextMenus
    {
        string siteref;

        public ContextMenuStrip Create()
        {
            // Add the default menu options.

            List<string> siteList = SiteListReader.generateSiteList(Properties.Settings.Default.default_config);
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;



            // Websites 
            item = new ToolStripMenuItem();
            foreach (string site in siteList) {
                ToolStripMenuItem subitem = new ToolStripMenuItem(site);
                subitem.Click += new EventHandler(Site_Handler);
                item.DropDown.Items.Add(subitem);
            }
            item.Text = "Websites";
            menu.Items.Add(item);

            // Self-Defined Website
            item = new ToolStripMenuItem();
            item.Text = "Enter Site";
            item.Click += new System.EventHandler(Input_Click);
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

        void Input_Click(object sender, EventArgs e)
        {
            DialogResult ib = InputBox("Input Site", ref siteref);
        }

        public static DialogResult InputBox(string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            label.Text = promptText;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            Pinger.website = value;
            return dialogResult;
        }


    }
}
