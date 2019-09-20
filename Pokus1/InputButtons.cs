using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;

namespace Pokus1
{
	class WholeGameButtons : Dictionary<Keys,Input>
	{
		public WholeGameButtons()
		{
			Add(System.Windows.Forms.Keys.Escape, InputPoss.menu);
			Add(System.Windows.Forms.Keys.P, InputPoss.menu);
			Add(System.Windows.Forms.Keys.R, InputPoss.restart);
			Add(System.Windows.Forms.Keys.A, InputPoss.changePlayerLeft);
			Add(System.Windows.Forms.Keys.S, InputPoss.changePlayerRight);
		}
	}
	class PlayerButtons : Dictionary<Keys, Input>
	{
		public PlayerButtons()
		{
			Add(System.Windows.Forms.Keys.Up, InputPoss.up);
			Add(System.Windows.Forms.Keys.Down, InputPoss.down);
			Add(System.Windows.Forms.Keys.Right, InputPoss.right);
			Add(System.Windows.Forms.Keys.Left, InputPoss.left);
			Add(System.Windows.Forms.Keys.Space, InputPoss.skill);
		}
	}
}
