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
		internal Environment map;

		public CharactersUi()
		{
			InitializeComponent();
		}

		List<CharacterStats> characters;
		private void CharactersUi_Load(object sender, EventArgs e)
		{
			characters = new List<CharacterStats>();
			foreach (PlayerCharacter p in map.Players)
			{
				CharacterStats ch = new CharacterStats();
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
