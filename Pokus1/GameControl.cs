using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CoreLib;
using System.IO;

namespace Pokus1
//{
{
	public partial class GameControl : GameObjectControl
	{
		public GameControl()
		{
			InitializeComponent();
		}

		internal System.Windows.Forms.Timer timer1;
		protected void GameForm_Load(object sender, EventArgs e)
		{
			Game.FirstRun();

			GameLoop = new GameLoop(Game, timer1);
			GameLoopThread = new Thread(new ThreadStart(GameLoop.Start));
			GameLoopThread.Start();
			Form.SetGameThread(GameLoopThread);
		}

		internal Game Game { get; set; }
		internal ToDo ToDo { get; set; }

		private Thread GameLoopThread;
		private GameLoop GameLoop;

		IMapRenderer renderer => Game.Renderer;

		public new void Update()
		{
			ToDo.RunAll();
			renderer.Render();
		}
	}
}
