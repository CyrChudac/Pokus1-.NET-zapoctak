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
using System.Threading;

namespace Pokus1
{
	public partial class GameScreenControl : GameObjectControl
	{
		public GameScreenControl()
		{
			InitializeComponent();
		}

		internal CharactersUi charactersUi;
		internal GameControl gameControl;

		private Thread GameLoopThread;
		private GameLoop GameLoop;

		private void GameScreenControl_Load(object sender, EventArgs e)
		{
			Controls.Add(gameControl);
			Controls.Add(charactersUi);
			gameControl.Focus();
			charactersUi.TabStop = false;

			GameLoop = new GameLoop(gameControl.Game, this);
			GameLoopThread = new Thread(new ThreadStart(GameLoop.Start));
			GameLoopThread.Start();
			Form.SetGameThread(GameLoopThread);

			Cursor.Hide();
		}
		public new void Update()
		{
			gameControl.Update();
			charactersUi.Update();
		}
	}
}
