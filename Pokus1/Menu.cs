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
using CoreLib;

namespace Pokus1
{
	public partial class Menu : GameObjectControl
	{
		public Menu()
		{
			InitializeComponent();
			for (int i = 0; File.Exists(Game.CurrentDirectory + @"\" + Game.LevelsFileName + @"\Level" + i.ToString()); i++)
				maxLevel = i;
			maxLevel++;
		}
		private void Menu_Load(object sender, EventArgs e)
		{
			foreach (Control item in Controls)
				item.Left = (this.Width - item.Width) / 2;
			if (Directory.Exists(Game.SaveFileName) && Directory.GetFiles(Game.SaveFileName).Length > 0)
				Loader.Enabled = true;
		}

		private void Options_Click(object sender, EventArgs e) => Form.OpenControl<Options>();

		int maxLevel;
		int _currLevel = 0;
		int CurrLevel
		{
			get => _currLevel;
			set
			{
				if (value != 0)
					NewGame.Text = "Next Level";
				else
					NewGame.Text = "New Game";
				_currLevel = value;
			}
		}
		private void NewGame_Click(object sender, EventArgs e)
		{
			string fileName = GetCurrLevelName();
			CurrLevel = (++CurrLevel).Modulo(maxLevel);
			using (Stream s = new FileStream(fileName, FileMode.Open))
				Play(new JsonMapDeserializer(s).GetMap());
		}

		string GetCurrLevelName()
		{
			if (File.Exists(Game.CurrentDirectory + @"\" + Game.LevelsFileName + @"\Level" + CurrLevel.ToString()))
				return Game.CurrentDirectory + @"\" + Game.LevelsFileName + @"\Level" + CurrLevel.ToString();
			return null;
		}

		public void Play(Map map)
		{
			GameControl gf = new GameControl();
			Game game = new Game(map.Clone(), gf, Form, gf, gf);
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

			game.FirstRun();
			Form.OpenControl(gs);
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

		private void HelpButton_Click(object sender, EventArgs e)
		{
			Form.OpenControl<HelpControl>();
		}
	}
}
