﻿using System;
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
		}

		static Menu()
		{
			for (int i = 0; File.Exists(Game.LevelsFilePath + @"\Level" + i.ToString()); i++)
				maxLevel = i;
			maxLevel++;
		}

		private void Menu_Load(object sender, EventArgs e)
		{
			foreach (Control item in Controls)
				item.Left = (this.Width - item.Width) / 2;
			if (Directory.Exists(Game.SaveFilePath) && Directory.GetFiles(Game.SaveFilePath).Length > 0)
				Loader.Enabled = true;
			if (currLevel != 0)
				NewGame.Text = "Next Level";
			else
				NewGame.Text = "New Game";
		}

		private void Options_Click(object sender, EventArgs e) => Form.OpenControl<Options>();

		static int maxLevel;
		static int currLevel = 0;
		private void NewGame_Click(object sender, EventArgs e)
		{
			string fileName = GetCurrLevelName();
			currLevel = (++currLevel).Modulo(maxLevel);
			using (Stream s = new FileStream(fileName, FileMode.Open))
				Play(new JsonMapDeserializer(s).GetMap());
		}

		string GetCurrLevelName()
		{
			if (File.Exists(Game.LevelsFilePath + @"\Level" + currLevel.ToString()))
				return Game.LevelsFilePath + @"\Level" + currLevel.ToString();
			return null;
		}

		public void Play(Environment map)
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

			GameScreenControl gs = new GameScreenControl()
			{
				Form = Form,
				Dock = DockStyle.Fill,
				charactersUi = charactersUi,
				gameControl = gf
			};

			//game.FirstRun();
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
			Environment map = Form.Loading();
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
