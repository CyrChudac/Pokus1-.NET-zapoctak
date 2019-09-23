using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreLib;

namespace Pokus1
{
	internal interface IObjectFactory<T> where T : IGameObject
	{
		T GetObj(int maxHealth, Movement movement, string name, Location location, Environment map);
	}

	internal interface PlayerFactory : IObjectFactory<PlayerCharacter> { }

	internal class JumperFactory : PlayerFactory
	{
		public PlayerCharacter GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new Jumper(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	internal class KnifeThrowerFactory : PlayerFactory
	{
		public PlayerCharacter GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new KnifeThrower(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	internal class PuddlerFactory : PlayerFactory
	{
		public PlayerCharacter GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new Puddler(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	interface EnemyFactory : IObjectFactory<Enemy> { }

	internal class PassiveEnemyFactory : EnemyFactory
	{
		public Enemy GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			 => new PassiveEnemy(location, -1, map);
	}

}
