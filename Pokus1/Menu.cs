using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokus1
{
	public partial class Menu : GameObjectControl
	{
		public Menu()
		{
			InitializeComponent();
		}
		private void Menu_Load(object sender, EventArgs e)
		{
			foreach (Control item in Controls)
				item.Left = (this.Width - item.Width) / 2;
		}

		private void Exit_Click(object sender, EventArgs e) => Form.Close();

		private void Options_Click(object sender, EventArgs e) => Form.OpenControl<Options>();
		
		private void Editor_Click(object sender, EventArgs e) => Form.OpenControl<Editor>();

		private void NewGame_Click(object sender, EventArgs e)
		{
			GameControl gf = new GameControl();
			Game game = new Game(new DefaultMap.AlsoMove(80, 80).GetMap(), gf, gf, gf);
			gf.Form = Form;
			gf.Visible = true;
			gf.Dock = DockStyle.Fill;
			Form.OpenControl(gf);
			game.FirstRun();
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				Exit.PerformClick();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void Exit_Click_1(object sender, EventArgs e)
		{
			Form.Close();
		}
	}
}
