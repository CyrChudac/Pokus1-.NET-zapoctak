using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CoreLib;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;

namespace Pokus1
{
	[JsonObject()]
	public class Map
	{
		[JsonIgnore]
		public bool Victory
		{
			get
			{
				if (!Enemies.Any(Enemy => Enemy.Alive))
					if (Players.Count > 1)
					{
						for (int i = 1; i < Players.Count; i++)
						{
							if (!ObjInObj(Players[i], Players[0]))
								return false;
						}
						return true;
					}
					else return true;
				else return false;
			}
		}
		[JsonIgnore]
		public bool Defeat => !Players.All(Player => Player.Alive);
		[JsonIgnore]
		public bool GameEnd => Victory || Defeat;

		private static int OneTileSizeModifier = 38;
		//Both coordinates are made from width on purpose to make the tile size as square.
		public static readonly Size OneTileSize = new Size(
			Screen.PrimaryScreen.Bounds.Width / OneTileSizeModifier,
			Screen.PrimaryScreen.Bounds.Width / OneTileSizeModifier);
		public static int OneTileHeight => OneTileSize.Height;
		public static int OneTileWidth => OneTileSize.Width;
		[JsonIgnore]
		public readonly int Height;
		[JsonIgnore]
		public readonly int Width;
		public List<Player> Players { get; private set; } = new List<Player>();
		public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

		public List<IInteractiveItem> InteractiveItems { get; private set; } = new List<IInteractiveItem>();
		public List<INoninteractiveItem> NoninteractiveItems { get; private set; } = new List<INoninteractiveItem>();
		[JsonRequired]
		private readonly IMapTile[,] map;

		[JsonConstructor]
		public Map(IMapTile[,] map)
		{
			this.map = map;
			this.Height = map.GetLength(1);
			this.Width = map.GetLength(0);
		}

		/// <param name="x">Width</param>
		/// <param name="y">Height</param>
		public IMapTile this[int x, int y]
		{
			get { return map[x, y]; }

			set { map[x, y] = value; }
		}

		public Map Clone()
		{
			JsonSerializer js = Json.DefaultSerializer;
			MemoryStream s = new MemoryStream();
			new MapSerializer(s).Save(this, js);
			s.Position = 0;
			Map result = new JsonMapDeserializer(s).GetMap(js);
			s.Dispose();
			return result;
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
				for (int i = 0; i < NoninteractiveItems.Count; i++)
					NoninteractiveItems[i].Update();
			}
		}

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

		#region SomethingOnSomething

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
		=> (loc.x >= obj.Location.x && loc.y >= obj.Location.y) &&
					(loc.x <= obj.Location.x + obj.Size.Width && loc.y <= obj.Location.y + obj.Size.Height);

		#endregion

		public bool AmIFalling(IMovableObject obj)
			=> DirectionAccesable(obj, Direction.down);
		public bool DirectionAccesable(IMovableObject obj, Direction dir)
			=> new DirectionAccesor(this).DirectionAccesable(obj, dir);

		static Map()
		{
			LocFindCycles = (int)Math.Log(OneTileHeight, 2);
		}
		private static int LocFindCycles;
		public Location GoToLocation(IGameObject obj, Location vector)
		{
			DirectionAccesor da = new DirectionAccesor(this);
			Location from = DetermineCorner(obj, vector);
			int y = vector.y;
			int x = vector.x;
			if (x != 0)
				while (!da.CanGoSomewhere(obj.Height, OneTileHeight, i => from.x + x, i => obj.Location.y + i))
				{
					x -= Math.Sign(vector.x);
					if (x == 0)
						break;
				}
			if (y != 0)
				while (!da.CanGoSomewhere(obj.Width, OneTileWidth, i => obj.Location.x + i, i => from.y + y))
				{
					y -= Math.Sign(vector.y);
					if (y == 0)
						break;
				}
			return new Location(x, y);
		}

		private Location DetermineCorner( IGameObject obj, Location vector)
		{
			if (vector.x < 0)
				if (vector.y < 0)
					return obj.Location;
				else
					return new Location(obj.Location.x, obj.Location.y + obj.Size.Height);
			else
				if (vector.y < 0)
				return new Location(obj.Location.x + obj.Size.Width, obj.Location.y);
			else
				return new Location(obj.Location.x + obj.Size.Width, obj.Location.y + obj.Size.Height);
		}

		class DirectionAccesor
		{
			readonly Map map;
			public DirectionAccesor(Map map) => this.map = map;
			public bool DirectionAccesable(IMovableObject obj, Direction direction)
			{
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
				=> CanGoSomewhere(obj.Width, OneTileWidth,
					i => obj.Location.x + i,
					i => obj.Location.y - obj.Movement.ExampleLoc.y);
			bool CanGoDown(IMovableObject obj)
				=> CanGoSomewhere(obj.Width, OneTileWidth,
					i => obj.Location.x + i,
					i => obj.Location.y + obj.Height + obj.Movement.ExampleLoc.y);
			bool CanGoLeft(IMovableObject obj)
				=> CanGoSomewhere(obj.Height, OneTileHeight,
					i => obj.Location.x - obj.Movement.ExampleLoc.x,
					i => obj.Location.y + i);
			bool CanGoRight(IMovableObject obj)
				=> CanGoSomewhere(obj.Height, OneTileHeight,
					i => obj.Location.x + obj.Width + obj.Movement.ExampleLoc.x,
					i => obj.Location.y + i);

			/// <summary>
			/// In for cyclus goes on the edge of an object and determines,
			/// wheter the given place can be accessable.
			/// </summary>
			/// <param name="maxPlus1">max for the for cyclus</param>
			/// <param name="step">step for the for cyclus</param>
			/// <param name="LocationX">function, that for given iteration of the cyclus gives the X coordinate</param>
			/// <param name="LocationY">function, that for given iteration of the cyclus gives the Y coordinate</param>
			/// <returns></returns>
			public bool CanGoSomewhere(double maxPlus1, int step,
				Func<double, double> LocationX, Func<double, double> LocationY)
			{
				for (double i = 0; i < maxPlus1; i += step)
				{
					if (!LocationAccesable(LocationX(i), LocationY(i)))
					{
						return false;
					}
				}
				if (!LocationAccesable(LocationX(maxPlus1), LocationY(maxPlus1)))
					return false; 
				return true;
			}
			public bool LocationAccesable(double x, double y)
			{
				int X = (int)x / OneTileWidth;
				int Y = (int)y / OneTileHeight;
				switch (map[X, Y])
				{
					case null:
					case var ig when (ig is NoTile):
						return true;
					case var ig3 when (ig3 is FullWall):
					case var ig2 when (ig2 is Killer):
						return false;
				}
				throw new Exception("Unknown map tile detected next to given object.");
			}
		}
	}

	public interface IMapTile
	{
		System.Drawing.Color Color { get; }
		System.Drawing.Brush Brush { get; }
	}
}
