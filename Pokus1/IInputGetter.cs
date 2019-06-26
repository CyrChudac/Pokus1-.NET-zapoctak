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
		HashSet<Input> CurrPlayerButtons { get; }
		HashSet<Input> CurrGameButtons { get; }
		void Reset();
	}
	partial class GameControl : IInputGetter
	{
		WholeGameButtons gameInputCheck = new WholeGameButtons();
		PlayerButtons playerInputCheck = new PlayerButtons();
		public HashSet<Input> CurrPlayerButtons { get; private set; }  = new HashSet<Input>();
		public HashSet<Input> CurrGameButtons { get; private set; } = new HashSet<Input>();

		protected override bool IsInputKey(Keys keyData)
		{
			if (playerInputCheck.Keys.Contains(keyData))
				return true;
			return gameInputCheck.Keys.Contains(keyData);
		}

		public void Reset() => CurrPlayerButtons = new HashSet<Input>();

		protected void GameForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (gameInputCheck.Keys.Contains(e.KeyCode))
				CurrGameButtons.Add(gameInputCheck[e.KeyCode]);
			if (playerInputCheck.Keys.Contains(e.KeyCode))
				CurrPlayerButtons.Add(playerInputCheck[e.KeyCode]);
		}

		private void GameControl_KeyUp(object sender, KeyEventArgs e)
		{
			if (playerInputCheck.Keys.Contains(e.KeyCode))
				CurrPlayerButtons.Remove(playerInputCheck[e.KeyCode]);
		}
	}
}
