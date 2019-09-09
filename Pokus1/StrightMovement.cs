using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;

namespace Pokus1
{
	public class StrightMovement : Movement
	{
		/// <param name="vector">The direction of the movement.</param>
		public StrightMovement(int speed, Location vector): base (speed, 0)
		{
			this.Vector = vector;
		}
		public StrightMovement(int speed, Direction direction) : this(speed, (Location)direction)
		{
		}
		Location Vector;
		public override void Move()
		{
			location += (Vector*Speed).Normalize(shift,0);
		}
	}
}
