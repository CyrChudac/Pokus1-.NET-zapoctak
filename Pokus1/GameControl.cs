using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;

namespace Pokus1
{
	public partial class GameControl : GameObjectControl
	{
		InputButtons inputCheck = new InputButtons();
		public GameControl()
		{
			InitializeComponent();
		}

		protected void GameForm_Load(object sender, EventArgs e)
		{
			Render();
		}

		void ShowThis(object sender, EventArgs e){
			this.Show();
		}
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				Form.OpenControl<InGameMenu>();
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
