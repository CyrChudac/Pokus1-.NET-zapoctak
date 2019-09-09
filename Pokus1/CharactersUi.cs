using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokus1
{
	public partial class CharactersUi : GameObjectControl
	{
		internal Map map;

		public CharactersUi()
		{
			InitializeComponent();
		}

		List<Character> characters;
		private void CharactersUi_Load(object sender, EventArgs e)
		{
			characters = new List<Character>();
			foreach (Player p in map.Players)
			{
				Character ch = new Character();
				ch.Player = p;
				ch.Dock = DockStyle.Left;
				ch.Form = this.Form;
				characters.Add(ch);
				Controls.Add(ch);
			}
		}

		public new void Update()
		=> characters.ForEach(ch => ch.Update());
	}
}
