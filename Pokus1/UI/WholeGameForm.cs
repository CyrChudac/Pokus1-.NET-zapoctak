using System;
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
	public interface IGameObjectOpener
	{
		GameObjectControl OpenControl<T>() where T : GameObjectControl, new();
		GameObjectControl OpenControl(GameObjectControl control);
	}

	public partial class WholeGameForm : Form, IGameObjectOpener
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
			return OpenControl(control);
		}
		public GameObjectControl OpenControl(GameObjectControl control)
		{
			Controls.Add(control);
			if (ControlOrder.Count > 0)
					ControlOrder.Peek().Visible = false;
			ControlOrder.Push(control);
			control.Dock = DockStyle.Fill;
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
			if (ShowDialog(new ReallyEndDialog()))
				base.Close();
		}

		private void WholeGameForm_Load(object sender, EventArgs e)
		{

		}

		public void Saving(Environment map)
		{
			Pokus1.Saving saving = new Saving();
			if (ShowDialog(saving))
			{
				try
				{
					if (!Directory.Exists(Game.SaveFilePath))
						Directory.CreateDirectory(Game.SaveFilePath);
					Stream s = new FileStream(Game.SaveFilePath + @"\" + saving.fileName.Text, FileMode.Create);
					new JsonMapSerializer(s).Save(map, JsonDefault.DefaultSerializer);
					s.Dispose();
				}
				catch (IOException){ }
			}
		}

		internal bool ShowDialog(Form dialog)
		{
			dialog.StartPosition = FormStartPosition.CenterScreen;
			DialogResult result = dialog.ShowDialog();
			return result == DialogResult.Yes;
		}

		public Environment Loading()
		{
			Pokus1.Loading loading = new Loading();
			if (ShowDialog(loading))
			{
				try
				{
					Stream s = new FileStream(Game.SaveFilePath + @"\" +
						(string)loading.list.SelectedItem, FileMode.Open);
				Environment result = new JsonMapDeserializer(s).GetMap(JsonDefault.DefaultSerializer);
				s.Dispose();
				return result;
				}
				catch (IOException) { }
			}
			return null;
		}
	}
}
