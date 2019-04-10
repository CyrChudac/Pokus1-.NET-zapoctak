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
		public UsualMovement(int speed) : base(speed)
		{
		}
		static readonly int shift = 20;
		protected override void Move(int speed)
		{
			int temp = this.speed;
			this.speed = speed;
			Move();
			this.speed = temp;
		}
		protected override void Move()
		{
			location = new Location();
			foreach (Input key in ActiveKeys)
					switch (key)
					{
						case null:
							break;
						case var x when (x is Input.Player.Movement.Right):
							location.x += shift;
							break;
						case var x when (x is Input.Player.Movement.Left):
							location.x -= shift;
							break;
						default:
							throw InputPoss.wrongInputFoundException;
					}
			location = location.Normalize();
			location.x *= speed;
			location.y *= speed;
			ActiveKeys.Clear();
		}
		public override List<Input> ActiveKeys { protected get; set; }
	}
}
