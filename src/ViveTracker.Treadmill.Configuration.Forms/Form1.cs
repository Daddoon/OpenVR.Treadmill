using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViveTracker.Treadmill.VirtualGamePad.Helper;

namespace ViveTracker.Treadmill.Configuration.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void installGamepadDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GamepadInfo.IsGamepadDriverInstalled())
            {
                bool success = GamepadInfo.InstallGamepadDriver();
                if (success)
                {
                    MessageBox.Show("Gamepad Driver installation is a success!");
                    return;
                }
                MessageBox.Show("An error occured during Gamepad Driver installation");
            }
            else
            {
                MessageBox.Show("The Gamepad Driver is already installed!");
            }
        }
    }
}
