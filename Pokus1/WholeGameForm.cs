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
			this.WindowState = FormWindowState.Maximized;
			this.Location = new Point(0, 0);
			this.Size = Screen.PrimaryScreen.WorkingArea.Size;
			InitializeComponent();
			KeyPreview = true;
			//ClientSize = Size;
			OpenControl<Menu>();
		}
		 
		protected void GameForm_Load(object sender, EventArgs e)
		{
		}
		public void OpenControl<T>() where T : GameObjectControl, new()
		{
			T control = new T();
			control.Parent = this;
			control.Form = this;
			control.Visible = true;
			control.Dock = DockStyle.Fill;
			OpenControl(control);
		}
		public void OpenControl(GameObjectControl control)
		{
			if (ControlOrder.Count > 0)
				ControlOrder.Peek().Visible = false;
			ControlOrder.Push(control);
			Controls.Add(control);
		}
		public void CloseControl()
		{
			ControlOrder.Pop().Visible = false;
			//OpenControl(ControlOrder.Pop());
			ControlOrder.Peek().Visible = true;
		}
		public new void Close()
		{
			ReallyEndDialog dialog = new ReallyEndDialog();
			dialog.StartPosition = FormStartPosition.CenterScreen;
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.Yes)
				base.Close();
		}
	}
}
