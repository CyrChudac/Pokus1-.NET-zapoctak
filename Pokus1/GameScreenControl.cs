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

namespace Pokus1
{
	public partial class GameScreenControl : GameObjectControl
	{
		public GameScreenControl()
		{
			InitializeComponent();
			timer1.Interval = Time.delay;
		}

		internal CharactersUi charactersUi;
		internal GameControl gameControl;
		private void GameScreenControl_Load(object sender, EventArgs e)
		{
			gameControl.timer1 = timer1;
			Controls.Add(gameControl);
			Controls.Add(charactersUi);
			gameControl.Focus();
			charactersUi.TabStop = false;
			
			timer1.Start();
			Cursor.Hide();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			gameControl.Update();
			charactersUi.Update();
		}
	}
}
