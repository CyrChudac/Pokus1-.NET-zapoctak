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
		public GameControl()
		{
			InitializeComponent();
			timer1.Interval = Time.delay;
		}

		protected void GameForm_Load(object sender, EventArgs e)
		{
			timer1.Start();
		}

		internal Game Game { get; set; }

		private void timer1_Tick(object sender, EventArgs e)
		{
			Game.Update();
		}
	}
}
