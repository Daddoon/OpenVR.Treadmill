using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViveTracker.Treadmill.Configuration.Forms
{
	class SelectablePanel : Panel
	{
		public SelectablePanel()
		{
			this.SetStyle(ControlStyles.Selectable, true);
			this.TabStop = true;
		}
	}
}
