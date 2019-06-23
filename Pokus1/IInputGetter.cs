using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;

namespace Pokus1
{
	interface IInputGetter
	{
		Queue<Input> CurrButtons { get; }
	}
	partial class GameControl : IInputGetter
	{
		public Queue<Input> CurrButtons { private set; get; } = new Queue<Input>();

		protected void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (inputCheck.Keys.Contains(e.KeyCode))
			{
				CurrButtons.Enqueue(inputCheck[e.KeyCode]);
			}
		}
	}
}
