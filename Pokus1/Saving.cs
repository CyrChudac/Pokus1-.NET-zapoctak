using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pokus1
{
	public partial class Saving : Form
	{
		public Saving()
		{
			InitializeComponent();
		}

		private void fileName_TextChanged(object sender, EventArgs e)
		{
			note.Text = "";
			save.Enabled = true;
			save.Text = "Save";

			if (fileName.Text == "")
			{
				note.Text = "Please, type a name...";
				save.Enabled = false;
			}
			if (Directory.Exists(Game.SaveFileName) &&
				Directory.GetFiles(Game.SaveFileName).Select(x => x.Substring(x.LastIndexOf('\\') + 1)).Contains(fileName.Text))
			{
				note.Text = "Save of this name already exists.";
				save.Text = "Overwrite";
			}
		}

		private void cancel_Click(object sender, EventArgs e)
		=> this.DialogResult = DialogResult.No;

		private void save_Click(object sender, EventArgs e)
		=> this.DialogResult = DialogResult.Yes;
	}
}
