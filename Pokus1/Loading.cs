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
	public partial class Loading : Form
	{
		public Loading()
		{
			InitializeComponent();
		}

		private void cancel_Click(object sender, EventArgs e)
			=> this.DialogResult = DialogResult.No;

		private void Loading_Load(object sender, EventArgs e)
		{
			foreach (var f in Directory.GetFiles(Game.CurrentDirectory + @"\" + Game.SaveFileName))
				list.Items.Add(f.Substring(f.LastIndexOf('\\') + 1));
		}

		private void load_Click(object sender, EventArgs e)
			=> this.DialogResult = DialogResult.Yes;
	}
}
