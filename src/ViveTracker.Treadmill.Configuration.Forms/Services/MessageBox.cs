using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Models;

namespace ViveTracker.Treadmill.Configuration.Forms.Services
{
    public class CustomMessageBox : PipedEntity, IMessageBox
    {
        public void ShowAlert(string message)
        {
            MessageBox.Show(message);
        }
    }
}
