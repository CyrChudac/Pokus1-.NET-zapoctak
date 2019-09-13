﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using CoreLib;

namespace Pokus1
{
	public partial class WholeGameForm : Form
	{
		Stack<GameObjectControl> ControlOrder = new Stack<GameObjectControl>();
		public WholeGameForm()
		{
			//this.WindowState = FormWindowState.Maximized;
			InitializeComponent();
			this.Location = new Point(0, 0);
			this.Size = Screen.PrimaryScreen.Bounds.Size;
			KeyPreview = true;
			OpenControl<Menu>();
		}

		internal void SetGameThread(Thread gameThread)
		{
			if (this.gameThread != null)
				throw new Exception("Active game already exists. You cannot start any other one.");
			this.gameThread = gameThread;
		}
		Thread gameThread = null;
		internal void KillGame()
		{
			if (gameThread == null)
				throw new Exception("Cannot kill game: no game found.");
			gameThread.Abort();
			gameThread = null;
		}

		protected void GameForm_Load(object sender, EventArgs e)
		{
		}
		public GameObjectControl OpenControl<T>() where T : GameObjectControl, new()
		{
			T control = new T();
			control.Visible = true;
			control.Dock = DockStyle.Fill;
			return OpenControl(control);
		}
		public GameObjectControl OpenControl(GameObjectControl control)
		{
			Controls.Add(control);
			if (ControlOrder.Count > 0)
					ControlOrder.Peek().Visible = false;
			ControlOrder.Push(control);
			control.Form = this;
			control.Focus();
			return control;
		}
		public void CloseControl()
		{
			Controls.Remove(ControlOrder.Pop());

			if (ControlOrder.Count > 0)
			{
				ControlOrder.Peek().Visible = true;
				ControlOrder.Peek().Focus();
			}
		}
		public void ToMenu()
		{
			while(ControlOrder.Count > 1)
				CloseControl();
		}
		public new void Close()
		{
			ReallyEndDialog dialog = new ReallyEndDialog();
			if (ShowDialog(dialog))
				base.Close();
		}

		private void WholeGameForm_Load(object sender, EventArgs e)
		{

		}

		public void Saving(Map map)
		{
			Pokus1.Saving saving = new Saving();
			if (ShowDialog(saving))
			{
				Stream s = new FileStream(Game.SaveFileName + @"\" + saving.fileName.Text, FileMode.Create);
				new MapSerializer(s).Save(map, Json.DefaultSerializer);
				s.Dispose();
			}
		}

		private bool ShowDialog(Form dialog)
		{
			dialog.StartPosition = FormStartPosition.CenterScreen;
			DialogResult result = dialog.ShowDialog();
			return result == DialogResult.Yes;
		}

		public Map Loading()
		{
			Pokus1.Loading loading = new Loading();
			if (ShowDialog(loading))
			{
				Stream s = new FileStream(Game.SaveFileName + @"\" + (string)loading.list.SelectedItem, FileMode.Open);
				Map result = new BinaryMapDeserializer(s).GetMap(Json.DefaultSerializer);
				s.Dispose();
				return result;
			}
			return null;
		}
	}
}
