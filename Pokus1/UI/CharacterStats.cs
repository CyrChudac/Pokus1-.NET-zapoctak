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
	public partial class CharacterStats : GameObjectControl
	{
		public PlayerCharacter Player { get; set; }
		int initialHealthSize;
		public CharacterStats()
		{
			InitializeComponent();
		}

		private void Character_Load(object sender, EventArgs e)
		{
			Health.BackColor = Color.Red;
			CharName.Text = Player.Name;
			Picture.Image = Player.DefaultImage;
			initialHealthSize = Health.Width;
		}

		public new void Update()
		{
			Health.Width = initialHealthSize * Player.CurrHealth / Player.MaxHealth;
		}
	}
}
