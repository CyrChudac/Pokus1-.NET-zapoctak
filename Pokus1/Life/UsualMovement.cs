using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	 public class UsualMovement : PlayerMovement
	 {
		[JsonConstructor]
		public UsualMovement(int speed, int fallingSpeed) : base(speed, fallingSpeed)
		{
		}
		public UsualMovement(int speed) : base(speed)
		{
		}
		public override void Move()
		{
			foreach (Input key in ActiveKeys)
				switch (key)
				{
					case null:
						break;
					case var x when (x is Input.Player.Movement.Right):
						location.x += directionsImportness;
						break;
					case var x when (x is Input.Player.Movement.Left):
						location.x -= directionsImportness;
						break;
					default:
						throw InputPoss.wrongInputFoundException;
				}
			location *= shift;
			ActiveKeys.Clear();
		}
		public override List<Input> ActiveKeys { protected get; set; } = new List<Input>();
	}
}
