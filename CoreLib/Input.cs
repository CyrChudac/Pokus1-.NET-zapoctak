using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
	public abstract class Input {
		public abstract class Player : Input
		{
			public abstract class Movement : Player
			{
				public abstract Direction ToDirection();
				public class Left : Movement
				{
					public override Direction ToDirection() => Direction.left;
				}
				public class Right : Movement
				{
					public override Direction ToDirection() => Direction.right;
				}
				public class Down : Movement
				{
					public override Direction ToDirection() => Direction.down;
				}
				public class Up : Movement
				{
					public override Direction ToDirection() => Direction.up;
				}
			}
			public class SkillUse : Player { }
		}
		public abstract class WholeGame : Input {
			public class Menu : WholeGame { }
			public abstract class ChangeChar : WholeGame
			{
				public class Left : ChangeChar { }
				public class Right : ChangeChar { }
			}
			public class Restart : WholeGame { }
		}
		/// <param name="i">0=NULL, 1=MoveRight, 2=MoveLeft, 3=SkillUse,
		/// 4=Menu, 5=CahngeCharLeft, 6=ChangeCharRight, 7=Restart</param>
		public static explicit operator int(Input i)
		{
			switch (i)
			{
				case null:
					return 0;
				case var x when (x is Input.Player.Movement.Right):
					return 1;
				case var x when (x is Input.Player.Movement.Left):
					return 2;
				case var x when (x is Input.Player.SkillUse):
					return 3;
				case var x when (x is Input.WholeGame.Menu):
					return 4;
				case var x when (x is Input.WholeGame.ChangeChar.Left):
					return 5;
				case var x when (x is Input.WholeGame.ChangeChar.Right):
					return 6;
				case var x when (x is Input.WholeGame.Restart):
					return 7;
				default:
					throw InputPoss.wrongInputFoundException;
			}
		}
	}
}
