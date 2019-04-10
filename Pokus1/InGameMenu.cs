using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokus1
{
	public partial class InGameMenu : GameObjectControl
	{
		public InGameMenu()
		{
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.FromArgb(130, Color.Gray);
			InitializeComponent();
		}

		private void InGameMenu_Load(object sender, EventArgs e) {}

		private void Continue_Click(object sender, EventArgs e) => Form.CloseControl();

		private void End_Click(object sender, EventArgs e) { }

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				End.PerformClick();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void InGameMenu_Load_1(object sender, EventArgs e)
		{

		}
	}
}
