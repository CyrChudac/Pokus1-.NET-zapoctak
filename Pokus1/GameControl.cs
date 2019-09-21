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

	public interface IWithToDo
	{
		ToDo ToDo { get; set; }
	}

	public partial class GameControl : GameObjectControl, IWithToDo
	{
		public GameControl()
		{
			InitializeComponent();
		}
		
		protected void GameForm_Load(object sender, EventArgs e)
		{
		}

		internal Game Game { get; set; }
		public ToDo ToDo { get; set; }

		IMapRenderer renderer => Game.Renderer;

		public new void Update()
		{
			ToDo.RunAll();
			renderer.Render();
		}
	}
}
