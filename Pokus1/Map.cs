using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib;
using System.Runtime.Serialization;
using System.IO;

namespace Pokus1
{
	[Serializable]
	public class Map
	{	
		[NonSerialized]
		bool victory = false;
		public bool Victory => !Enemies.Any(Enemy => Enemy.Alive) ||victory;
		public void SetVictory() { victory = true; }
		[NonSerialized]
		bool defeat = false;
		public bool Defeat => !Players.All(Player => Player.Alive) || defeat;
		public void SetDefeat() { defeat = true; }
		public bool GameEnd => victory || defeat;

		[DataMember()]
		public readonly int oneTileHeight;
		[DataMember()]
		public readonly int oneTileWidth;
		[DataMember()]
		public readonly int Height;
		[DataMember()]
		public readonly int Width;
		[DataMember()]
		public List<Player> Players { get; private set; }
		[DataMember()]
		public List<Enemy> Enemies { get; private set; }

		[DataMember()]
		public List<IInteractiveItem> OtherItems { get; private set; }
		[DataMember()]
		private readonly IMapTile[,] map;

		public Map Clone()
		{
			MemoryStream s = new MemoryStream();
			new MapSerializer(s).Save(this);
			Map result = new BinaryMapDeserializer(s).GetMap();
			s.Dispose();
			return result;
		}

		/// <param name="x">Width</param>
		/// <param name="y">Height</param>
		public IMapTile this [int x, int y]
		{
			get { return map[x, y]; }

			set { map[x, y] = value; }
		}

		public void Update(ref int activePlayer)
		{
			if (!GameEnd)
			{ 
				if (!Players.ElementAt(activePlayer).Alive)
					activePlayer = (activePlayer + 1) % Players.Count;

				foreach (Player p in Players)
				{
					p.Update();
				}
				foreach (Enemy enemy in Enemies)
					enemy.Update();
			}
		}
		public bool AmIFalling(IGameObject obj)
			=> DirectionAccesable(obj, Direction.down);
		public bool DirectionAccesable(IGameObject obj, Direction dir) 
			=> new DirectionAccesor(this).ObjectDirectionAccesable(obj, dir);

		class DirectionAccesor
		{
			Map map;
			public DirectionAccesor(Map map) => this.map = map;
			double left;
			double top;
			public bool ObjectDirectionAccesable(IGameObject obj, Direction direction)
			{
				left = obj.Location.x - obj.Width / 2;
				top = obj.Location.y - obj.Height / 2;
				switch (direction)
				{
					case Direction.up:
						return CanGoUp(obj);
					case Direction.down:
						return CanGoDown(obj);
					case Direction.left:
						return CanGoLeft(obj);
					case Direction.right:
						return CanGoRight(obj);
					default:
						throw new NotImplementedException("Unknown direction used. ( = " + direction.ToString() + " )");
				}
			}
			bool CanGoUp(IGameObject obj)
			{
				for (double i = 0; i <= obj.Width; i += map.oneTileWidth)
				{
					if (!LocationAccesable(top, left + i))
					{
						return false;
					}
				}
				return true;
			}
			bool CanGoDown(IGameObject obj)
			{
				for (double i = 0; i <= obj.Width; i += map.oneTileWidth)
				{
					if (!LocationAccesable(top + obj.Height, left + i))
					{
						return false;
					}
				}
				return true;
			}
			bool CanGoLeft(IGameObject obj)
			{
				for (double i = 0; i <= obj.Height; i += map.oneTileHeight)
				{
					if (!LocationAccesable(top + i, left))
					{
						return false;
					}
				}
				return true;
			}
			bool CanGoRight(IGameObject obj)
			{
				for (double i = 0; i <= obj.Height; i += map.oneTileHeight)
				{
					if (!LocationAccesable(top + i, left + obj.Width))
					{
						return false;
					}
				}
				return true;
			}
			bool LocationAccesable(double x, double y)
			{
				int X = (int)x / map.oneTileWidth;
				int Y = (int)y / map.oneTileHeight;
				switch (map[X, Y])
				{
					case null:
						return true;
					case var ig when (ig is Killer):
					case var ig2 when (ig2 is FullWall):
						return false;
				}
				throw new Exception("Unknown map tile detected next to given object.");
			}
			bool LocationAccesable(Location loc)
			{ return LocationAccesable(loc.x, loc.y); }
		}

		public Map(IMapTile[,] map, List<Enemy> enemies, List<Player> players,
			List<IInteractiveItem> otherItems, int tileHeight, int tileWidth)
		{
			this.Players = players;
			foreach (var player in players) player.map = this;
			this.Enemies = enemies;
			foreach (var enemy in enemies) enemy.map = this;
			this.OtherItems = otherItems;
			this.map = map;
			this.Height = map.GetLength(1);
			this.Width = map.GetLength(0);
			this.oneTileHeight = tileHeight;
			this.oneTileWidth = tileWidth;
		}
	}

	public interface IMapTile {
		System.Drawing.Color Color { get; }
	}
}
