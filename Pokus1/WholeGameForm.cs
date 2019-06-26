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
			dialog.StartPosition = FormStartPosition.CenterScreen;
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.Yes)
				base.Close();
		}

		private void WholeGameForm_Load(object sender, EventArgs e)
		{

		}
	}
}
