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
		Input CurrButton { get; }
	}
	partial class GameControl : IInputGetter
	{ 
		private Input currButton;
		public Input CurrButton
		{
			get
			{
				Input ret = currButton;
				currButton = InputPoss.nothing;
				return ret;
			}
			private set { currButton = value; }
		}

		protected void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (inputCheck.Keys.Contains(e.KeyCode))
			{
				CurrButton = inputCheck[e.KeyCode];

			}
		}
	}
}
