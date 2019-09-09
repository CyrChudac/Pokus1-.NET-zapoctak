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
		public List<Player> Players { get; private set; } = new List<Player>();
		[DataMember()]
		public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

		[DataMember()]
		public List<IInteractiveItem> InteractiveItems { get; private set; } = new List<IInteractiveItem>();
		[DataMember()]
		public List<INoninteractiveItem> NoninteractiveItems { get; private set; } = new List<INoninteractiveItem>();
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
				if (!Players[activePlayer].Alive)
					activePlayer = (activePlayer + 1).Modulo(Players.Count);

				for (int i = 0; i < Players.Count; i++)
					Players[i].Update();
				for (int i = 0; i < Enemies.Count; i++)
					Enemies[i].Update();
				for(int i = 0; i < NoninteractiveItems.Count; i++)
					NoninteractiveItems[i].Update();
			}
		}
		public bool AmIFalling(IMovableObject obj)
			=> DirectionAccesable(obj, Direction.down);
		public bool DirectionAccesable(IMovableObject obj, Direction dir) 
			=> new DirectionAccesor(this).DirectionAccesable(obj, dir);
		public void RemoveMe(IGameObject obj)
		{
			if (obj is Player)
				Players.Remove((Player)obj);
			else if (obj is Enemy)
				Enemies.Remove((Enemy)obj);
			else if (obj is IInteractiveItem)
				InteractiveItems.Remove((IInteractiveItem)obj);
			else if (obj is INoninteractiveItem)
				NoninteractiveItems.Remove((INoninteractiveItem)obj);
			else throw new Exception("Trying to destroy unknown type: " + obj.ToString());
		}

		/// <summary>
		/// Determines what alive player is the object touching by any part.
		/// </summary>
		public Player AmIOnPlayer(IMovableObject obj) => AmIOnAliveLife(obj, Players);
		/// <summary>
		/// Determines what alive enemy is the object touching by any part.
		/// </summary>
		public Enemy AmIOnEnemy(IMovableObject obj) => AmIOnAliveLife(obj, Enemies);
		public IInteractiveItem AmIOnInteractiveItem(IMovableObject obj) => AmIOnsomething(obj, InteractiveItems);

		private T AmIOnAliveLife<T>(IMovableObject obj, IEnumerable<T> collection) where T : Life
		=> AmIOnsomething(obj, collection, t => t.Alive);

		private T AmIOnsomething<T>(IMovableObject obj, IEnumerable<T> collection) where T : class, IGameObject
		=> AmIOnsomething<T>(obj, collection, t => true);
		private T AmIOnsomething<T>(IMovableObject obj, IEnumerable<T> collection,
			Func<T, bool> condition) where T : class, IGameObject
		{
			foreach (T t in collection)
			{
				if (condition(t))
					if (ObjInObj(t, obj) || ObjInObj(obj, t))
						return t;
			}
			return null;
		}

		private bool ObjInObj(IGameObject first, IGameObject second)
		=> LocationInObject(first.Location, second) ||
					LocationInObject(first.Location + new Location(0, first.Width), second) ||
					LocationInObject(first.Location + new Location(first.Height, 0), second) ||
					LocationInObject(first.Location + (Location)first.Size, second);

		private bool LocationInObject(Location loc, IGameObject obj)
		=> (loc.x > obj.Location.x && loc.y > obj.Location.y) &&
					(loc.x < obj.Location.x + obj.Size.Width && loc.y < obj.Location.y + obj.Size.Height);

		class DirectionAccesor
		{
			Map map;
			public DirectionAccesor(Map map) => this.map = map;
			double left;
			double top;
			public bool DirectionAccesable(IMovableObject obj, Direction direction)
			{
				left = obj.Location.x   ;  /* - obj.Width / 2;		/*			*/
				top = obj.Location.y	;  /* - obj.Height / 2;		/*			*/
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
			bool CanGoUp(IMovableObject obj)
				=> CanGoSomewhere(obj.Width, map.oneTileWidth,
					i => top - obj.Movement.Speed, i => left + i);
			//{
			//	for (double i = 0; i <= obj.Width; i += map.oneTileWidth)
			//	{
			//		if (!LocationAccesable(top - obj.Movement.Speed, left + i))
			//		{
			//			return false;
			//		}
			//	}
			//	return true;
			//}
			bool CanGoDown(IMovableObject obj)
				=> CanGoSomewhere(obj.Width, map.oneTileWidth,
					i => top + obj.Height + obj.Movement.Speed, i => left + i);
			//{
			//	for (double i = 0; i <= obj.Width; i += map.oneTileWidth)
			//	{
			//		if (!LocationAccesable(top + obj.Height + obj.Movement.Speed, left + i))
			//		{
			//			return false;
			//		}
			//	}
			//	return true;
			//}
			bool CanGoLeft(IMovableObject obj)
				=> CanGoSomewhere(obj.Height, map.oneTileHeight,
					i => top + i, i => left - obj.Movement.Speed);
			//{
			//	for (double i = 0; i <= obj.Height; i += map.oneTileHeight)
			//	{
			//		if (!LocationAccesable(top + i, left - obj.Movement.Speed))
			//		{
			//			return false;
			//		}
			//	}
			//	return true;
			//}
			bool CanGoRight(IMovableObject obj)
				=> CanGoSomewhere(obj.Height, map.oneTileHeight,
					i => top + i, i => left + obj.Width + obj.Movement.Speed);
			//{
			//	for (double i = 0; i <= obj.Height; i += map.oneTileHeight)
			//	{
			//		if (!LocationAccesable(top + i, left + obj.Width + obj.Movement.Speed))
			//		{
			//			return false;
			//		}
			//	}
			//	return true;
			//}
			/// <summary>
			/// In for cyclus goes on the edge of an object and determines,
			/// wheter the given place can be accessable.
			/// </summary>
			/// <param name="maxPlus1">max for the for cyclus</param>
			/// <param name="step">step for the for cyclus</param>
			/// <param name="LocationX">function, that for given iteration of the cyclus gives the X coordinate</param>
			/// <param name="LocationY">function, that for given iteration of the cyclus gives the Y coordinate</param>
			/// <returns></returns>
			bool CanGoSomewhere(double maxPlus1, int step,
				Func<double, double> LocationX, Func<double, double> LocationY)
			{
				for (double i = 0; i < maxPlus1; i += step)
				{
					if (!LocationAccesable(LocationX(i), LocationY(i)))
					{
						return false;
					}
				}
				return true;
			}
			bool LocationAccesable(double y, double x)
			{
				int X = (int)x / map.oneTileWidth;
				int Y = (int)y / map.oneTileHeight;
				switch (map[X, Y])
				{
					case null:
					case var ig when (ig is NoTile):
						return true;
					case var ig2 when (ig2 is Killer):
					case var ig3 when (ig3 is FullWall):
						return false;
				}
				throw new Exception("Unknown map tile detected next to given object.");
			}
			bool LocationAccesable(Location loc)
			{ return LocationAccesable(loc.x, loc.y); }
		}

		public Map(IMapTile[,] map,	int tileHeight, int tileWidth)
		{
			this.map = map;
			this.Height = map.GetLength(1);
			this.Width = map.GetLength(0);
			this.oneTileHeight = tileHeight;
			this.oneTileWidth = tileWidth;
		}
	}

	public interface IMapTile {
		System.Drawing.Color Color { get; }
		System.Drawing.Brush Brush { get; }
	}
}
