using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;
using System.IO;

namespace Pokus1
{
	public partial class HelpControl : GameObjectControl
	{
		public HelpControl()
		{
			InitializeComponent();
			//imageList1.ImageSize = Screen.PrimaryScreen.Bounds.Size;
		}

		List<Image> images = new List<Image>();
		int currImage = 0;

		private void HelpControl_Click(object sender, EventArgs e)
		{
			currImage = ++currImage;
			if (currImage == images.Count)
				End();
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawImage(images[currImage], 0, 0,
				Screen.PrimaryScreen.Bounds.Width,
				Screen.PrimaryScreen.Bounds.Height);
		}

		private void HelpControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				End();
		}

		private void End() => Form.ToMenu();

		private void HelpControl_Load(object sender, EventArgs e)
		{
			foreach (var i in Directory.GetFiles(
				Game.CurrentDirectory + @"\" + Game.ImagesFileName))
				if (i.Contains("Help"))
					images.Add(Image.FromFile(i));
		}
	}
}
