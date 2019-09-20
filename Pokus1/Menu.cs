using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
			if (Directory.Exists(Game.SaveFileName) && Directory.GetFiles(Game.SaveFileName).Length > 0)
				Loader.Enabled = true;
		}

		private void Options_Click(object sender, EventArgs e) => Form.OpenControl<Options>();

		private void NewGame_Click(object sender, EventArgs e)
		=> Play(new DefaultMap.WithPassiveEnemy(50, 50).GetMap());

		public void Play(Map map)
		{
			GameControl gf = new GameControl();
			Game game = new Game(map.Clone(), gf, gf, gf);
			gf.Game = game;
			gf.Form = Form;
			gf.Dock = DockStyle.Fill;
			gf.Name = "LOVE";

			CharactersUi charactersUi = new CharactersUi() { map = map };
			charactersUi.Dock = DockStyle.Bottom;
			charactersUi.Form = this.Form;

			GameScreenControl gs = new GameScreenControl();
			gs.Form = Form;
			gs.Dock = DockStyle.Fill;
			gs.charactersUi = charactersUi;
			gs.gameControl = gf;

			Form.OpenControl(gs);
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

		private void Loader_Click(object sender, EventArgs e)
		{
			Map map = Form.Loading();
			if(map != null)
				Play(map);
		}

		private void Editor_Click_1(object sender, EventArgs e)
		{
			Form.OpenControl<Editor>();
		}
	}
}
