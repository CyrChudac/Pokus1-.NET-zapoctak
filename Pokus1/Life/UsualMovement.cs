using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	 public class UsualMovement : Movement
	 {
		public UsualMovement(int speed, int fallingSpeed) : base(speed, fallingSpeed)
		{
		}
		public UsualMovement(int speed) : base(speed)
		{
		}
		public override void ResetAndMove()
		{
			location = new Location();
			foreach (Input key in ActiveKeys)
				switch (key)
				{
					case null:
						break;
					case var x when (x is Input.Player.Movement.Right):
						location.x += arrowImportness;
						break;
					case var x when (x is Input.Player.Movement.Left):
						location.x -= arrowImportness;
						break;
					default:
						throw InputPoss.wrongInputFoundException;
				}
			location *= speed;
			ActiveKeys.Clear();
		}
		public override List<Input> ActiveKeys { protected get; set; } = new List<Input>();
	}
}
