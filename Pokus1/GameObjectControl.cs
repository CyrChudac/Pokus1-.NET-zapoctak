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
	//[System.ComponentModel.DesignerCategory("")]
	public partial class GameObjectControl : UserControl {
		public WholeGameForm Form { get; set; }
		public GameObjectControl()
		{
			this.InitializeComponent();
		}

		private void GameObjectControl_Load(object sender, EventArgs e)
		{
		}
	}
}
