using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;

namespace Pokus1
{
	public partial class InGameMenu : GameObjectControl
	{
		internal Map Map;
		Color startingBckColor;
		public InGameMenu()
		{
			InitializeComponent();
			startingBckColor = this.BackColor;
		}

		private void InGameMenu_Load(object sender, EventArgs e)
		{
			Cursor.Show();
			Time.Stop();
		}

		private void Continue_Click(object sender, EventArgs e)
		{
			Cursor.Hide();
			Time.Start();
			Form.CloseControl();
		}

		private void End_Click(object sender, EventArgs e)
		{
			Form.KillGame();
			Form.ToMenu();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				Continue.PerformClick();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void save_Click(object sender, EventArgs e)
		{
			Form.Saving(Map);
		}

		private void load_Click(object sender, EventArgs e)
		{
			Map map = Form.Loading();
			if (map != null)
			{
				Form.KillGame();
				new Menu() { Form = Form }.Play(map);
			}
		}
	}
}
