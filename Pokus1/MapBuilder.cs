using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CoreLib;
using System.IO;
using Newtonsoft.Json;

namespace Pokus1
{
	interface IMapBuilder
	{
		Environment GetMap();
	}

	public class DefaultMap
	{
		readonly int width;
		readonly int height;
		public DefaultMap(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		IMapTile[,] OnlyBorders_Wall()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) || (i == width - 1) || (j == 0) || (j == height - 1))
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			return field;
		}

		IMapTile[,] BordersAndSomething_Wall()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) || (i == width - 1) || (j == 0) || (j == height - 1) || ((j == 10) && (i % 7 == 1)))
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			return field;
		}
		IMapTile[,] BordersAndSomething2_Wall()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) ||
						(i == width - 1) ||
						(j == 0) ||
						(j == height - 1) ||
						((j == 10) && (i < 11)) ||
						(i == 15)
						)
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			return field;
		}
		IMapTile[,] Level1_Try()
		{
			IMapTile[,] field = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					if ((i == 0) ||
						(i == width - 1) ||
						(j == 0) ||
						(j == height - 1))
					{
						field[i, j] = FullWall.Tile;
					}
					else field[i, j] = NoTile.Tile;
				}
			}
			
			return field;
		}
		public class JustExistWithPhysics : IMapBuilder
		{
			DefaultMap builder;
			public JustExistWithPhysics(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Environment GetMap()
			{
				IMapTile[,] field = builder.OnlyBorders_Wall();
				Environment result = new Environment(field);
				result.Players.Add(new Unskilled(100, 100, NoMovement.instance,
				"Vlad", new Location{ x = 143, y = 59}, new SingleColorAnimation(Color.GreenYellow),
				Life.DefaultSize, result));
				return result;
			}
		}
		public class AlsoMove : IMapBuilder
		{
			DefaultMap builder;
			public AlsoMove(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Environment GetMap()
			{
				IMapTile[,] field = builder.BordersAndSomething_Wall();
				Environment result = new Environment(field);
				result.Players.Add(new Jumper(100, 100, new Movement(Life.defaultSpeed),
				"Vlad", new Location{ x = 143, y = 59},
				Life.DefaultSize, result));
				result.Players.Add(new Jumper(100, 100, new Movement(Life.defaultSpeed),
				"Vlad", new Location{ x = 300, y = 59}, new SingleColorAnimation(Color.OrangeRed),
				Life.DefaultSize, result));
				return result;
			}
		}
		public class WithKnifeThrower : IMapBuilder
		{
			DefaultMap builder;
			public WithKnifeThrower(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Environment GetMap()
			{
				IMapTile[,] field = builder.BordersAndSomething2_Wall();
				Environment result = new Environment(field);
				result.Players.Add(new Jumper(100, 100, new Movement(Life.defaultSpeed),
					"Vlad", new Location { x = 143, y = 59 }, new SingleColorAnimation(Color.GreenYellow),
					Life.DefaultSize, result));
				result.Players.Add(new KnifeThrower(100, 100, new Movement(Life.defaultSpeed),
					"Vlad2", new Location{ x = 300, y = 59}, new SingleColorAnimation(Color.OrangeRed),
					Life.DefaultSize, result));
				return result;
			}
		}
		public class WithPassiveEnemy : IMapBuilder
		{
			DefaultMap builder;
			public WithPassiveEnemy(int width, int height)
			{
				builder = new DefaultMap(width, height);
			}

			public Environment GetMap()
			{
				IMapTile[,] field = builder.BordersAndSomething2_Wall();
				Environment result = new Environment(field);
				result.Players.Add(new KnifeThrower(100, 100, new Movement(Life.defaultSpeed),
					"Vlad", new Location { x = 300, y = 59 }, new SingleColorAnimation(Color.GreenYellow),
					Life.DefaultSize, result));
				result.Enemies.Add(new PassiveEnemy(new Location { x = 100, y = 30 }, 1, result));
				return result;
			}
		}
	}
	
	interface IMapDesearilizer : IMapBuilder {}

	class JsonMapDeserializer : IMapDesearilizer, IDisposable
	{
		readonly Stream stream;
		public JsonMapDeserializer(Stream stream)
		{
			this.stream = stream;
		}
		public void Dispose() => stream.Dispose();
		public Environment GetMap()
		{
			return GetMap(JsonDefault.DefaultSerializer);
		}
		public Environment GetMap(JsonSerializer js)
		{
			StreamReader sr = new StreamReader(stream);
			Environment result = (Environment)js.Deserialize(sr, typeof(Environment));
			foreach (PlayerCharacter p in result.Players)
				p.Map = result;
			foreach (Enemy e in result.Enemies)
				e.Map = result;
			return result;
		}
	}
}
