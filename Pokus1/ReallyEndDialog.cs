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
	public partial class ReallyEndDialog : Form
	{
		public ReallyEndDialog()
			: this (new Font("Pristina", 15.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)))) { }
		public ReallyEndDialog(Font font)
		{
			Font font2 = new Font(font.FontFamily, font.Size + 10, FontStyle.Bold);
			InitializeComponent();
			yes.Font = font;
			no.Font = font;
			label1.Font = font2;
			label1.Left = (ramecek.Width - label1.Width) / 2;
		}

		private void yes_Click(object sender, EventArgs e)
			=> this.DialogResult = DialogResult.Yes;

		private void no_Click(object sender, EventArgs e)
			=> this.DialogResult = DialogResult.No;

		private void ReallyEndDialog_Load(object sender, EventArgs e)
		{

		}
	}
}
