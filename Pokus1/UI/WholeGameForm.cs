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
		T OpenControl<T>() where T : GameObjectControl, new();
		T OpenControl<T>(T control, bool add = false) where T : GameObjectControl, new ();
	}

	public partial class WholeGameForm : Form, IGameObjectOpener
	{ 
		Stack<GameObjControlRet> ControlOrder = new Stack<GameObjControlRet>();
		Stack<GameObjectControl> CurrControls = new Stack<GameObjectControl>();
		public WholeGameForm()
		{
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
		
		public T OpenControl<T>() where T : GameObjectControl, new()
		 => OpenControl(new T());
		public T OpenControl<T>(T control, bool add = false) where T : GameObjectControl, new()
		{
			if (!add)
			{
				if (CurrControls.Count > 0)
				{
					GameObjectControl previous = CurrControls.Pop();
					Controls.Remove(previous);
					previous.Dispose();
				}
				bool foundSame = false;
				foreach (GameObjControlRet con in ControlOrder)
					if (con.Type == typeof(T))
					{
						foundSame = true;
						break;
					}
				if (!foundSame)
					ControlOrder.Push(new ControlReturner<T>());
				else
					while (ControlOrder.Peek().Type != typeof(T))
						ControlOrder.Pop();
			}
			else if (CurrControls.Count > 0)
				Controls.Remove(CurrControls.Peek());
			CurrControls.Push(control);
			control.Size = this.Size;
			control.Dock = DockStyle.Fill;
			control.Form = this;
			Controls.Add(control);
			control.Focus();
			return control;
		}
		public void CloseControl()
		{
			Controls.Remove(CurrControls.Pop());
			if (CurrControls.Count > 0)
			{
				OpenControl(CurrControls.Pop(), true);
			}
			else
			{
				ControlOrder.Pop();
				if (ControlOrder.Count > 0)
				{
					OpenControl(ControlOrder.Pop().Get());
				}
				else
					Close();
			}
		}
		public void ToMenu()
		{
			Controls.Clear();
			ControlOrder.Clear();
			foreach (GameObjectControl con in CurrControls)
				con.Dispose();
			CurrControls.Clear();
			OpenControl<Menu>();
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
			Loading loading = new Loading();
			if (ShowDialog(loading))
			{
				try
				{
					Stream s = new FileStream(Game.SaveFilePath + @"\" +
						(string)loading.list.SelectedItem, FileMode.Open);
				Environment result = new JsonMapDeserializer(s).GetMap();
				s.Dispose();
				return result;
				}
				catch (IOException) { }
			}
			return null;
		}

		abstract class GameObjControlRet
		{
			public abstract GameObjectControl Get();
			public abstract Type Type { get; }
		}

		class ControlReturner<T> : GameObjControlRet where T : GameObjectControl, new()
		{
			public override GameObjectControl Get() => new T();
			public override Type Type => typeof(T);
		}
	}
}
