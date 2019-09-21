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
		static new readonly Size DefaultSize = new Size(45, 45);
		static int DefaultWidth => DefaultSize.Width;
		static int DefaultHeight => DefaultSize.Height;
		Camera Camera;
		Image background;

		public Size CanvasSize => new Size(Size.Width, Size.Height - panel1.Height);

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
			Camera = new Camera(new Size(), this);
			Camera.locationHolder = Movement;
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
				NewTiles(width, tiles.GetLength(1));
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
				NewTiles(tiles.GetLength(0), height);
			}
		}

		private void NewTiles(int width, int height)
		{
			IMapTile[,] newField = new IMapTile[width, height];
			for (int i = 0; i < width; i++)
				for (int j = 0; j < height; j++)
				{
					if (i < tiles.GetLength(0) && j < tiles.GetLength(1))
						newField[i, j] = tiles[i, j];
					else
						newField[i, j] = Pokus1.NoTile.Tile;
				}
			tiles = newField;
			MakeBackground();
			Camera = new Camera(new Size(tiles.GetLength(0) * Map.OneTileWidth,
					tiles.GetLength(1) * Map.OneTileHeight), 
				this,
				Camera.Location);
			Camera.locationHolder = Movement;
			Refresh();
			
		}

		void MakeBackground()
		{
			Image result = new Bitmap(tiles.GetLength(0) * Map.OneTileWidth, tiles.GetLength(1) * Map.OneTileHeight);
			using (Graphics g = Graphics.FromImage(result))
			{
				for (int i = 0; i < tiles.GetLength(0); i++)
				{
					for (int j = 0; j < tiles.GetLength(1); j++)
					{
						g.FillRectangle(tiles[i, j].Brush,
							i * Map.OneTileWidth,
							j * Map.OneTileHeight,
							Map.OneTileWidth,
							Map.OneTileHeight);
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
			MoveLabels(players, vector);
			MoveLabels(enemies, vector);
			MoveLabels(interactiveItems, vector);
			MoveLabels(noninteractiveItems, vector);
		}
		void MoveLabels(IEnumerable<Label> labels, Location vector)
		{
			foreach (var l in labels)
				l.Location = new Point(l.Location.X - vector.x, l.Location.Y - vector.y);
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

		void PaintOthers(PaintEventArgs e, IEnumerable<Label> others)
		{
			foreach (var l in others)
				e.Graphics.FillRectangle(new SolidBrush(l.BackColor),
					l.Location.X, l.Location.Y,
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

			if (!Dragging(players, e.Location))
				if (!Dragging(enemies, e.Location))
					if (!Dragging(interactiveItems, e.Location))
						Dragging(noninteractiveItems, e.Location);
			LastMouseLoc = e.Location;
		}


		/// <returns> bool that indicates, if there was any dragged item found.</returns>
		bool Dragging(IEnumerable<Label> collection, Point mouseLocation)
			=> ForeachIfConditionDoAction(collection,
				(Label l) => l.AllowDrop,
				(Label l) => l.Location = new Point(
					l.Location.X + mouseLocation.X - LastMouseLoc.X,
					l.Location.Y + mouseLocation.Y - LastMouseLoc.Y));

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
			int row = (mouseLocation + Camera.Location).x / Map.OneTileWidth;
			int column = (mouseLocation + Camera.Location).y / Map.OneTileHeight;
			if (row < tiles.GetLength(0) && row >= 0
				&& column < tiles.GetLength(1) && column >= 0)
			{
				tiles[row, column] = active;
				using (Graphics g = Graphics.FromImage(background))
				{
					g.FillRectangle(active.Brush,
						row * Map.OneTileWidth,
						column * Map.OneTileHeight,
						Map.OneTileWidth,
						Map.OneTileHeight);
				}
				Refresh();
			}

		}

		private void save_Click(object sender, EventArgs e)
		{
			Saving dialog = new Saving();
			if(Form.ShowDialog(dialog))
			{
				if (!Directory.Exists(Game.CurrentDirectory + @"\" +
					Game.MapsFileName))
					Directory.CreateDirectory(
					Game.CurrentDirectory + @"\" +
					Game.MapsFileName);
				Map map = new Map(tiles);
				players.ForEach( l => map.Players.Add(((PlayerFactory)l.Tag).GetPlayer(
					Life.defaultHealth,
					new Movement(Life.defaultSpeed),
					"Name", //TODO - implement this
					new Location(Camera.Location.x + l.Location.X, Camera.Location.y + l.Location.Y),
					map)));
				enemies.ForEach(l => map.Enemies.Add(((EnemyFactory)l.Tag).GetPlayer(
				   Life.defaultHealth,
				   new Movement(Life.defaultSpeed),
				   "Name", //TODO - implement this
				   new Location(Camera.Location.x + l.Location.X, Camera.Location.y + l.Location.Y),
				   map)));
				if (interactiveItems.Count + noninteractiveItems.Count > 0)
					throw new NotImplementedException("There is no code for making interactive and noninteractive items through editor yet.");
				Stream stream = new FileStream(
					Game.CurrentDirectory + @"\" +
					Game.MapsFileName + @"\" + 
					dialog.fileName.Text,
					FileMode.Create);
				MapSerializer serializer = new MapSerializer(stream);
				serializer.Save(map);
			}
		}

		List<Label> players = new List<Label>();
		List<Label> enemies = new List<Label>();
		List<Label> interactiveItems = new List<Label>();
		List<Label> noninteractiveItems = new List<Label>();

		private Label ItemInCentre<T>(Color c, ILifeFactory<T> type, IList<Label> addTo) where T : Life
		{
			Label l = new Label();
			l.BackColor = c;
			l.Size = Life.DefaultSize;
			l.Location = new Point(CanvasSize.Width / 2, CanvasSize.Height / 2);
			l.Tag = type;
			addTo.Add(l);
			return l;
		}

		private void jumper_Click(object sender, EventArgs e)
		{
			ItemInCentre(Jumper.Color, new JumperFactory(), players);
		}

		private void knifeThrower_Click(object sender, EventArgs e)
		{
			ItemInCentre(KnifeThrower.Color, new KnifeThrowerFactory(), players);
		}

		private void passiveEnemy_Click(object sender, EventArgs e)
		{
			ItemInCentre(PassiveEnemy.Color, new PassiveEnemyFactory(), enemies);
		}

		bool FoundDragged(IEnumerable<Label> collection, Point mouseLocation)
			=>
			ForeachIfConditionDoAction(collection,
				l => l.Location.X < mouseLocation.X &&
					l.Location.Y < mouseLocation.Y &&
					l.Location.X + l.Width > mouseLocation.X &&
					l.Location.Y + l.Height > mouseLocation.Y,
				l => l.AllowDrop = true);
		private void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			if (!FoundDragged(players, e.Location))
				if (!FoundDragged(enemies, e.Location))
					if (!FoundDragged(interactiveItems, e.Location))
						FoundDragged(noninteractiveItems, e.Location);
		}

		private void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			players.ForEach(p => p.AllowDrop = false);
			enemies.ForEach(en => en.AllowDrop = false);
			interactiveItems.ForEach(ii => ii.AllowDrop = false);
			noninteractiveItems.ForEach(ni => ni.AllowDrop = false);
		}
	}
}
