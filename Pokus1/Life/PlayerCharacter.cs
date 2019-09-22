using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;
using Newtonsoft.Json;

namespace Pokus1
{
	public abstract class PlayerCharacter : Life
	{
		public PlayerCharacter(int maxHealth, int currHealth,
			Movement movement, string name, Location location, IAnimation animation, Size size, Environment map)
			: base(maxHealth, currHealth, location, movement, animation, size, name, map)
		{
		}
		public Inventory Items { get; protected set; } = new Inventory();

		protected abstract void UseSkill();

		[JsonIgnore]
		public virtual Image DefaultImage => Animation.Image;
		public void PropagateInput(Input input)
		{
			if (input is Input.Player.Movement.Left)
				PropagateDir(Direction.left);
			else if (input is Input.Player.Movement.Right)
				PropagateDir(Direction.right);
			else if (input is Input.Player.SkillUse)
				UseSkill();
			else if (input is Input.Player.Movement.Down || input is Input.Player.Movement.Up) { }
			else
				throw new ArgumentException("Unknown input type = " + input.GetType());
		}
		private void PropagateDir(Direction dir)
		{
			LookingAt = dir;
			Movement.AddDirectionToDirection(dir);
		}
		protected override void DuringUpdate() => SkillUpdate();
		protected abstract void SkillUpdate();
	}

	internal interface ILifeFactory<T> where T : Life
	{
		T GetPlayer(int maxHealth, Movement movement, string name, Location location, Environment map);
	}

	internal interface PlayerFactory : ILifeFactory<PlayerCharacter> { }

	internal class JumperFactory : PlayerFactory
	{
		public PlayerCharacter GetPlayer(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new Jumper(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	internal class KnifeThrowerFactory : PlayerFactory
	{
		public PlayerCharacter GetPlayer(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new KnifeThrower(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	internal class PuddlerFactory : PlayerFactory
	{
		public PlayerCharacter GetPlayer(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new Puddler(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	interface EnemyFactory: ILifeFactory<Enemy> { }

	internal class PassiveEnemyFactory : EnemyFactory
	{
		public Enemy GetPlayer(int maxHealth, Movement movement, string name, Location location, Environment map)
			 => new PassiveEnemy(location, -1, map);
	}

	public class Inventory : List<Item> { }
}
