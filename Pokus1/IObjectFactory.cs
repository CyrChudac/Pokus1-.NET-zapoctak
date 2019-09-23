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

	internal interface IPlayerFactory : IObjectFactory<PlayerCharacter> { }

	internal class JumperFactory : IPlayerFactory
	{
		public PlayerCharacter GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new Jumper(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	internal class KnifeThrowerFactory : IPlayerFactory
	{
		public PlayerCharacter GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new KnifeThrower(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	internal class PuddlerFactory : IPlayerFactory
	{
		public PlayerCharacter GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			=> new Puddler(maxHealth, maxHealth, movement, name, location, Life.DefaultSize, map);
	}

	interface IEnemyFactory : IObjectFactory<Enemy> { }

	internal class PassiveEnemyFactory : IEnemyFactory
	{
		public Enemy GetObj(int maxHealth, Movement movement, string name, Location location, Environment map)
			 => new PassiveEnemy(location, -1, map);
	}

}
