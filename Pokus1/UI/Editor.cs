using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreLib;
using System.IO;

namespace Pokus1
{
	public partial class Editor : GameObjectControl, IWithCanvasSize
	{
		sealed class EditorObject<T> : IGameObject  where T : IGameObject
		{
			public Size Size { get; set; } = new Size();

			public int Width => Size.Width;

			public int Height => Size.Height;

			public Location Location { get; set; } = new Location();

			public string Name { get; set; } = String.Empty;

			public IAnimation Animation => NoAnimation.Singleton;

			public IObjectFactory<T> Factory;

			public bool Dropped;

			public Brush Brush;
		}
		static new readonly Size DefaultSize = new Size(45, 45);
		static int DefaultWidth => DefaultSize.Width;
		static int DefaultHeight => DefaultSize.Height;
		Camera Camera;
		Image background;

		public Size CanvasSize => new Size(Size.Width, Size.Height - panel1.Height);

		private int CurrMapWidth = 1;
		private int CurrMapHeight = 1;
		private IMapTile[,] tiles = new IMapTile[1, 1] { { Pokus1.NoTile.Tile } };
		IMapTile active = FullWall.Tile;
		public Editor()
		{
			InitializeComponent();

			timer1.Interval = Time.delay;

			//this makes it not blinking
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer
				| ControlStyles.UserPaint
				| ControlStyles.AllPaintingInWmPaint,
				true);
		}

		private void Editor_Load(object sender, EventArgs e)
		{
			Camera = new Camera(new Size(), this)
			{
				locationHolder = Movement
			};
			MapHeight.Text = DefaultHeight.ToString();
			MapWidth.Text = DefaultWidth.ToString();
			NewTiles(DefaultWidth, DefaultHeight);
			Wall.BackColor = FullWall.Tile.Color;
			Wall.ForeColor = FullWall.Tile.Color.OpositeColor();
			NoTile.BackColor = Pokus1.NoTile.Tile.Color;
			NoTile.ForeColor = Pokus1.NoTile.Tile.Color.OpositeColor();
			Time.Start();
			timer1.Start();
		}

		private void Width_TextChanged(object sender, EventArgs e)
		{
			if (int.TryParse(MapWidth.Text, out int width))
			{
				NewTiles(width, CurrMapHeight);
			}
		}

		private void Wall_Click(object sender, EventArgs e)
			=> ChangeTileTo(FullWall.Tile);
		void ChangeTileTo(IMapTile tile) => active = tile;

		private void NoTile_Click(object sender, EventArgs e)
			=> active = Pokus1.NoTile.Tile;

		private void MapHeight_TextChanged(object sender, EventArgs e)
		{
			if (int.TryParse(MapHeight.Text, out int height))
			{
				NewTiles(CurrMapWidth, height);
			}
		}

		private void NewTiles(int width, int height)
		{
			bool update = false;
			if(width > tiles.GetLength(0))
			{
				update = true;
			}
			CurrMapWidth = width;
			if (height > tiles.GetLength(1))
			{
				update = true;
			}
			CurrMapHeight = height;
			if (update)
			{
				IMapTile[,] newField = new IMapTile[CurrMapWidth, CurrMapHeight];
				for (int i = 0; i < CurrMapWidth; i++)
					for (int j = 0; j < CurrMapHeight; j++)
					{
						if (i < tiles.GetLength(0) && j < tiles.GetLength(1))
							newField[i, j] = tiles[i, j];
						else
							newField[i, j] = Pokus1.NoTile.Tile;
					}
				tiles = newField;
			}
			MakeBackground();
			Camera = new Camera(new Size(CurrMapWidth * Environment.OneTileWidth,
					CurrMapHeight * Environment.OneTileHeight),
				this,
				Camera.Location)
			{
				locationHolder = Movement
			};
			Refresh();
			
		}

		void MakeBackground()
		{
			Image result = new Bitmap(CurrMapWidth * Environment.OneTileWidth, CurrMapHeight * Environment.OneTileHeight);
			using (Graphics g = Graphics.FromImage(result))
			{
				for (int i = 0; i < CurrMapWidth; i++)
				{
					for (int j = 0; j < CurrMapHeight; j++)
					{
						g.FillRectangle(tiles[i, j].Brush,
							i * Environment.OneTileWidth,
							j * Environment.OneTileHeight,
							Environment.OneTileWidth,
							Environment.OneTileHeight);
					}
				}
			}
			background = result;
		}
		
		Movement Movement = new Movement(Life.defaultSpeed);
		private void timer1_Tick(object sender, EventArgs e)
		{
			Time.Update();
			foreach (var item in moves)
				Movement.AddDirectionToDirection(item.ToDirection());
			Location vector = Movement.CalculatedVector;
			Movement.FinalLocation = vector;
			MoveAllMovable(vector);
			Camera.Update();
			Refresh();
			Movement.Reset();
			moves.Clear();
			shiftActive = false;
		}
		void MoveAllMovable(Location vector)
		{
			MoveEditorObjects(players, vector);
			MoveEditorObjects(enemies, vector);
			MoveEditorObjects(interactiveItems, vector);
			MoveEditorObjects(noninteractiveItems, vector);
		}
		void MoveEditorObjects<T>(IEnumerable<EditorObject<T>> objects, Location vector) where T : IGameObject
		{
			foreach (var l in objects)
				l.Location = new Point(l.Location.x - vector.x, l.Location.y - vector.y);
		}

		PlayerButtons inputGetter = new PlayerButtons();
		List<Input.Player.Movement> moves = new List<Input.Player.Movement>();

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (inputGetter.Keys.Contains(keyData) && inputGetter[keyData] is Input.Player.Movement)
				moves.Add((Input.Player.Movement)inputGetter[keyData]);
			if (keyData == Keys.Escape)
				ToMenu();
			if (keyData == (Keys.Shift | Keys.ShiftKey))
				shiftActive = true;
			if (keyData == Keys.W)
				ChangeTileTo(FullWall.Tile);
			if (keyData == Keys.N)
				ChangeTileTo(Pokus1.NoTile.Tile);
			return base.ProcessCmdKey(ref msg, keyData);
		}

		bool shiftActive = false;

		private void ToMenu()
		{
			if(Form.ShowDialog(new ReallyEndDialog()))
				Form.ToMenu();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawImage(background, Camera.Location * -1);

			PaintOthers(e, players);
			PaintOthers(e, enemies);
			PaintOthers(e, interactiveItems);
			PaintOthers(e, noninteractiveItems);
		}

		void PaintOthers<T>(PaintEventArgs e, IEnumerable<EditorObject<T>> others) where T : IGameObject
		{
			foreach (var l in others)
				e.Graphics.FillRectangle(l.Brush,
					l.Location.x, l.Location.y,
					l.Width, l.Height);
		}

		private void Editor_MouseClick(object sender, MouseEventArgs e)
		{
			ChangeTile(e.Location);
		}

		Point LastMouseLoc = new Point();
		private void Editor_MouseMove(object sender, MouseEventArgs e)
		{
			if (shiftActive)
				ChangeTile(e.Location);

			if (!Dragging(noninteractiveItems, e.Location))
				if (!Dragging(interactiveItems, e.Location))
					if (!Dragging(enemies, e.Location))
						Dragging(players, e.Location);
			LastMouseLoc = e.Location;
		}


		/// <returns> bool that indicates, if there was any dragged item found.</returns>
		bool Dragging<T>(IEnumerable<EditorObject<T>> collection, Point mouseLocation) where T : IGameObject
			=> ForeachIfConditionDoAction(collection,
				(EditorObject<T> l) => l.Dropped,
				(EditorObject<T> l) => l.Location = new Point(
					l.Location.x + mouseLocation.X - LastMouseLoc.X,
					l.Location.y + mouseLocation.Y - LastMouseLoc.Y));

		bool ForeachIfConditionDoAction<T>(IEnumerable<T> collection,
			Func<T, bool> condition, Action<T> action)
		{
			foreach (var t in collection)
			{
				if (condition(t))
				{
					action(t);
					return true;
				}
			}
			return false;
		}

		void ChangeTile(Point mouseLocation)
		{
			int row = (mouseLocation + Camera.Location).x / Environment.OneTileWidth;
			int column = (mouseLocation + Camera.Location).y / Environment.OneTileHeight;
			if (row < tiles.GetLength(0) && row >= 0
				&& column < tiles.GetLength(1) && column >= 0)
			{
				tiles[row, column] = active;
				using (Graphics g = Graphics.FromImage(background))
				{
					g.FillRectangle(active.Brush,
						row * Environment.OneTileWidth,
						column * Environment.OneTileHeight,
						Environment.OneTileWidth,
						Environment.OneTileHeight);
				}
				Refresh();
			}

		}

		private void save_Click(object sender, EventArgs e)
		{
			Saving dialog = new Saving();
			if(Form.ShowDialog(dialog))
			{
				if (!Directory.Exists(Game.MapsFilePath))
					Directory.CreateDirectory(Game.MapsFilePath);
				Environment map = new Environment(tiles);
				FromObjectsToLife(players, map.Players, map);
				FromObjectsToLife(enemies, map.Enemies, map);
				FromObjectsToLife(interactiveItems, map.InteractiveItems, map);
				FromObjectsToLife(noninteractiveItems, map.NoninteractiveItems, map);
				Stream stream = new FileStream(
					Game.MapsFilePath + @"\" + 
					dialog.fileName.Text,
					FileMode.Create);
				JsonMapSerializer serializer = new JsonMapSerializer(stream);
				serializer.Save(map);
				stream.Dispose();
			}
		}

		List<EditorObject<PlayerCharacter>> players = new List<EditorObject<PlayerCharacter>>();
		List<EditorObject<Enemy>> enemies = new List<EditorObject<Enemy>>();
		List<EditorObject<IInteractiveItem>> interactiveItems = new List<EditorObject<IInteractiveItem>>();
		List<EditorObject<INoninteractiveItem>> noninteractiveItems = new List<EditorObject<INoninteractiveItem>>();

		void FromObjectsToLife<T>(IEnumerable<EditorObject<T>> objs, 
			IList<T> finalCollection, Environment envir) where T : IGameObject
		{
			foreach (var obj in objs)
			{
				finalCollection.Add(obj.Factory.GetObj(
				   Life.defaultHealth,
				   new Movement(Life.defaultSpeed),
				   new Location(Camera.Location.x + obj.Location.x, Camera.Location.y + obj.Location.y),
				   envir));
			}
		}

		private EditorObject<T> ItemInCentre<T>(Color c, IObjectFactory<T> factory,
			IList<EditorObject<T>> addTo) where T : IGameObject
		{
			EditorObject<T> o = new EditorObject<T>()
			{
				Brush = new SolidBrush(c),
				Size = Life.DefaultSize,
				Location = new Point(CanvasSize.Width / 2, CanvasSize.Height / 2),
				Factory = factory
			};
			addTo.Add(o);
			return o;
		}

		private void jumper_Click(object sender, EventArgs e)
		{
			PlayerInCentre(Jumper.Color, new JumperFactory(), players);
		}

		private void knifeThrower_Click(object sender, EventArgs e)
		{
			PlayerInCentre(KnifeThrower.Color, new KnifeThrowerFactory(), players);
		}

		private void passiveEnemy_Click(object sender, EventArgs e)
		{
			ItemInCentre(PassiveEnemy.Color, new PassiveEnemyFactory(), enemies);
		}

		void PlayerInCentre<T>(Color c, IObjectFactory<T> factory,
			IList<EditorObject<T>> addTo) where T : IGameObject
		{
			ItemInCentre(c, factory, addTo);
			save.Enabled = true;
		}

		bool FoundDragged<T>(IEnumerable<EditorObject<T>> collection, Point mouseLocation) where T : IGameObject
			=>
			ForeachIfConditionDoAction(collection,
				l => l.Location.x < mouseLocation.X &&
					l.Location.y < mouseLocation.Y &&
					l.Location.x + l.Width > mouseLocation.X &&
					l.Location.y + l.Height > mouseLocation.Y,
				l => l.Dropped = true);
		private void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			if (!FoundDragged(players, e.Location))
				if (!FoundDragged(enemies, e.Location))
					if (!FoundDragged(interactiveItems, e.Location))
						FoundDragged(noninteractiveItems, e.Location);
		}

		private void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			players.ForEach(p => p.Dropped = false);
			enemies.ForEach(en => en.Dropped = false);
			interactiveItems.ForEach(ii => ii.Dropped = false);
			noninteractiveItems.ForEach(ni => ni.Dropped = false);
		}

		private void load_Click(object sender, EventArgs e)
		{
			Environment m = Form.Loading();
			if (m != null)
			{
				GetFromMap(m);
				Refresh();
			}
		}

		void GetFromMap(Environment map)
		{
			MapHeight.Text = map.Height.ToString();
			MapWidth.Text = map.Width.ToString();
			tiles = map.Tiles;
			MakeBackground();
			players = new List<EditorObject<PlayerCharacter>>();
			enemies = new List<EditorObject<Enemy>>();
			interactiveItems = new List<EditorObject<IInteractiveItem>>();
			noninteractiveItems = new List<EditorObject<INoninteractiveItem>>();
			var pd = new Dictionary<Type, Func<EditorObject<PlayerCharacter>>>()
			{
				{typeof(Jumper), () => ItemInCentre(Jumper.Color,
				new JumperFactory(), players) },
				{typeof(KnifeThrower), () => ItemInCentre(KnifeThrower.Color,
				new KnifeThrowerFactory(), players) },
				{typeof(Puddler), () => ItemInCentre(Puddler.Color,
				new PuddlerFactory(), players) },
			};
			var ed = new Dictionary<Type, Func<EditorObject<Enemy>>>()
			{
				{typeof(PassiveEnemy), () => ItemInCentre(PassiveEnemy.Color,
				new PassiveEnemyFactory(), enemies) }
			};
			foreach (PlayerCharacter p in map.Players)
				ObjectAtRightLocation(p, pd);
			foreach (Enemy e in map.Enemies)
				ObjectAtRightLocation(e, ed);
			//foreach (IInteractiveItem i in map.InteractiveItems)
			//	FakeGameObjectAtRightLocation(i, x);
			//foreach (INoninteractiveItem n in map.NoninteractiveItems)
			//	FakeGameObjectAtRightLocation(n, x);
		}

		void ObjectAtRightLocation<T>(T obj, Dictionary<Type, Func<EditorObject<T>>> dic) where T : Life
		{
			EditorObject<T> l = dic[obj.GetType()]();
			l.Location = obj.Location - Camera.Location;
		}
	}
}
