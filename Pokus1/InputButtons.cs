using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;

namespace Pokus1
{
	class InputButtons : Dictionary<Keys,Input>
	{
		public InputButtons()
		{
			Add(System.Windows.Forms.Keys.Right, InputPoss.right);
			Add(System.Windows.Forms.Keys.Left, InputPoss.left);
			Add(System.Windows.Forms.Keys.Space, InputPoss.skill);
			Add(System.Windows.Forms.Keys.Escape, InputPoss.menu);
			Add(System.Windows.Forms.Keys.P, InputPoss.menu);
			Add(System.Windows.Forms.Keys.R, InputPoss.restart);
			Add(System.Windows.Forms.Keys.A, InputPoss.changePlayerLeft);
			Add(System.Windows.Forms.Keys.S, InputPoss.changePlayerRight);
		}
		
	}
}
