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
	public partial class EndControl : GameObjectControl
	{
		public EndControl()
		{
			InitializeComponent();
		}

		public new string Text = "You have successfuly finished the level!";
		private void WellDoneControl_Load(object sender, EventArgs e)
		{
			label1.Text = Text;
			Size = Screen.PrimaryScreen.Bounds.Size;
			label1.Location = new Point((Width - label1.Width) / 2, (Height - label1.Height) / 2);
		}

		private void WellDoneControl_MouseClick(object sender, MouseEventArgs e)
			=> End();

		private void End()
		{
			Form.KillGame();
			Cursor.Show();
			Form.ToMenu();
		}

		private void WellDoneControl_KeyDown(object sender, KeyEventArgs e)
			=> End();
	}
}
