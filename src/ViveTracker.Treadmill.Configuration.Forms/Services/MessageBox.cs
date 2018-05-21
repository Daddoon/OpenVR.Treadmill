using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViveTracker.Treadmill.NugetToUnity.Interface;

namespace ViveTracker.Treadmill.Configuration.Forms.Services
{
    public class CustomMessageBox : IMessageBox
    {
        public void ShowAlert(string message)
        {
            MessageBox.Show(message);
        }
    }
}
