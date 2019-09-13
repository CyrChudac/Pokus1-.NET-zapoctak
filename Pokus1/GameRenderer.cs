using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using CoreLib;

namespace Pokus1
{
	public interface IMapRenderer
	{
		void FirstRender(Map map);
		void Render();
		void SetCameraMovement(Movement movement);
		void NewPlayerLocation(Location location);
		ICamera Camera { set; }
		Image Screenshot();
		Image DarkenImage(Image i, float DarkenCoeficient);
		Size CanvasSize { get; }
	}

	//public partial class GameControl : IMapRenderer
	//{
	//	Map map;
	//	Label[,] tiles;
	//	List<PictureBox> players;
	//	List<PictureBox> enemies;
	//	List<Label> items;
	//	public ICamera Camera { set; protected get; }

	//	public void SetCameraMovement(Movement movement) => Camera.Movement = movement;

	//	public virtual void FirstRender(Map map)
	//	{
	//		Form.Bounds = Screen.PrimaryScreen.Bounds;
	//		this.map = map;
	//		MakeAll();
	//		RenderMap();
	//		Render();
	//	}
	//	public virtual void Render()
	//	{
	//		Camera.Update();
	//		RenderPlayers();
	//		RenderEnemies();
	//		RenderItems();
	//	}

	//	void RenderMap()
	//	{
			
	//		for(int i = 0; i < tiles.GetLength(0); i++)
	//			for(int j = 0; j < tiles.GetLength(1); j++)
	//			{
	//				if(map[i,j] != NoTile.Tile)
	//					tiles[i, j].BackColor = map[i, j].Color;
	//			}
	//	}

	//	void RenderPlayers()
	//	{
	//		for (int i = 0; i < players.Count; i++)
	//		{
	//			players[i].Image = map.Players[i].Animation.Image;
	//			players[i].Size = map.Players[i].Size;
	//			players[i].Location = map.Players[i].Location;
	//		}
	//	}

	//	void RenderEnemies()
	//	{
	//		for (int i = 0; i < enemies.Count; i++)
	//		{
	//			enemies[i].Image = map.Enemies[i].Animation.Image;
	//			enemies[i].Location =
	//				new Point(map.Enemies[i].Location.x, map.Enemies[i].Location.y);
	//		}
	//	}

	//	void RenderItems()
	//	{
	//		for (int i = 0; i < items.Count; i++)
	//		{
	//			items[i].BackColor = map.OtherItems[i].Color;
	//			items[i].Location = 
	//				new System.Drawing.Point(map.OtherItems[i].Location.x, map.OtherItems[i].Location.y);
	//		}
	//	}

	//	#region void MakeAll()...
	//	void MakeAll()
	//	{
	//		// Order of this determines the Z axes
	//		MakeTiles();
	//		MakeEnemies();
	//		MakeItems();
	//		MakePlayers();
	//	}

	//	void MakeTiles()
	//	{
	//		tiles = new Label[map.Height, map.Height];

	//		int height = map.oneTileHeight;//ClientSize.Height / map.Height;
	//		int width = map.oneTileWidth;//ClientSize.Width / map.Width;

	//		for (int i = 0; i < map.Width; i++)
	//			for (int j = 0; j < map.Height; j++)
	//			{
	//				if (map[i, j] != NoTile.Tile)
	//				{
	//					Label label = new Label()
	//					{
	//						Parent = Form,
	//						Location = new Point(i * width, j * height),
	//						Text = "",
	//						Height = height,
	//						Width = width,
	//						Tag = i.ToString() + " " + j.ToString()
	//					};
	//					tiles[i, j] = label;
	//					this.Controls.Add(label);
	//				}
	//			}

	//		//Form.Size = new Size(tiles[map.Width - 1, map.Height - 1].Location + tiles[map.Width - 1, map.Height - 1].Size);
	//		Refresh();
	//	}

	//	void MakePlayers()
	//	{
	//		players = new List<PictureBox>(map.Players.Count);
	//		foreach (Player player in map.Players)
	//		{
	//			PictureBox pic = new PictureBox()
	//			{
	//				BorderStyle = BorderStyle.None,
	//				SizeMode = PictureBoxSizeMode.StretchImage,
	//				Tag = player.name
	//			};
	//			players.Add(pic);
	//			Controls.Add(pic);
	//		}
	//	}

	//	void MakeEnemies()
	//	{
	//		enemies = new List<PictureBox>(map.Enemies.Count);
	//		foreach (Enemy enemy in map.Enemies)
	//		{
	//			PictureBox pic = new PictureBox() {
	//				BorderStyle = BorderStyle.None,
	//				SizeMode = PictureBoxSizeMode.AutoSize,
	//				Tag = enemy.Type.ToString()
	//			};
	//			players.Add(pic);
	//			Controls.Add(pic);
	//		}
	//	}

	//	void MakeItems()
	//	{
	//		items = new List<Label>(map.OtherItems.Count);
	//		int width = 10;
	//		int height = 10;
	//		foreach (IInteractiveItem item in map.OtherItems)
	//		{
	//			Label label = new Label()
	//			{
	//				BackColor = item.Color,
	//				Location = new Point(item.Location.x, item.Location.y),
	//				Text = "",
	//				Tag = item.Name,
	//				Height = height,
	//				Width = width
	//			};
	//			this.Controls.Add(label);
	//			items.Add(label);
	//		}
	//	}
	//	#endregion
	//}

	public partial class GameControl : IMapRenderer, IDisposable
	{
		Map map;
		Image background;
		public ICamera Camera { set; protected get; }

		public void SetCameraMovement(Movement movement) => Camera.Movement = movement;
		public void NewPlayerLocation(Location location) => Camera.NewPlayerLocation(location);

		public void Render()
		{
			if(Time.IsRunning)
				Camera.Update();
			Refresh();
		}

		public Size CanvasSize => this.Size;

		public new void Dispose()
		{
			//background.Dispose();
			base.Dispose();
		}

		public void FirstRender(Map map)
		{
			this.map = map;
			SetStyle(ControlStyles.OptimizedDoubleBuffer //Canvas do not blink cause of that
				| ControlStyles.UserPaint
				| ControlStyles.AllPaintingInWmPaint,
				true);
			MakeBackground();
		}

		void MakeBackground()
		{
			background = new Bitmap(map.Width * Map.OneTileWidth, map.Height * Map.OneTileHeight);
			using (Graphics g = Graphics.FromImage(background))
				for (int i = 0; i < map.Height; i++)
				{
					for (int j = 0; j < map.Width; j++)
					{
						g.FillRectangle(
							map[j, i].Brush,
							j * Map.OneTileWidth,
							i * Map.OneTileHeight,
							Map.OneTileWidth,
							Map.OneTileHeight);
					}
				}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (map == null)
				base.OnPaint(e);
			else
			{
				e.Graphics.DrawImage(background, -Camera.Location);
				RenderPlayers(e.Graphics);
				RenderEnemies(e.Graphics);
				RenderInteractiveItems(e.Graphics);
				RenderNoninteractiveItems(e.Graphics);
			}
		}

		void RenderPlayers(Graphics g) => RenderLife(g, map.Players);
		void RenderEnemies(Graphics g) => RenderLife(g, map.Enemies);
		void RenderLife(Graphics g, IEnumerable<Life> collection)
		{
			foreach (var life in collection)
			{
				if (life.Alive)
					g.DrawImage(life.Animation.Image,
						life.Location.x - Camera.Location.x,	//here the thread could be stopped so the location x and y 
						life.Location.y - Camera.Location.y,	//would be inconsistent. However I think this would only 
						life.Width,								//make the moves smoother.
						life.Height);
				else
					g.DrawImage(DefaultDeadAnimation.instance.Image,
						life.Location.x - Camera.Location.x, 
						life.Location.y - Camera.Location.y,        
						life.Width,                                 
						life.Height);
			}
		}
		void RenderInteractiveItems(Graphics g) => RenderItems(g, map.InteractiveItems);
		void RenderNoninteractiveItems(Graphics g) => RenderItems(g, map.NoninteractiveItems);
		void RenderItems(Graphics g, IEnumerable<IItem> collection)
		{
			foreach (var item in collection)
			{
				g.DrawImage(item.Animation.Image,
					item.Location.x - Camera.Location.x,
					item.Location.y - Camera.Location.y,
					item.Size.Width,
					item.Size.Height);
				//g.FillRectangle(item.Brush,
				//	item.Location.x - Camera.Location.x,
				//	item.Location.y - Camera.Location.y,
				//	item.Size.Width,
				//	item.Size.Height);
			}
		}


		Image IMapRenderer.Screenshot()
		{
			Bitmap result = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
					Screen.PrimaryScreen.Bounds.Y,
					0,
					0,
					Screen.PrimaryScreen.Bounds.Size,
					CopyPixelOperation.SourceCopy);
			}
			return result;
		}

		/// <param name="darkenCoeficient">The less the darker.</param>
		Image IMapRenderer.DarkenImage(Image image, float darkenCoeficient)
		{
			float dc = darkenCoeficient;
			ColorMatrix cm = new ColorMatrix(new float[][]
			{
						new float[] {dc, 0, 0, 0, 0},
						new float[] {0, dc, 0, 0, 0},
						new float[] {0, 0, dc, 0, 0},
						new float[] {0, 0, 0, 1, 0},
						new float[] {0, 0, 0, 0, 1},
			});
			ImageAttributes ia = new ImageAttributes();
			ia.SetColorMatrix(cm);
			Point[] points =
			{
						new Point(0, 0),
						new Point(image.Width, 0),
						new Point(0, image.Height),
					};
			Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
			Bitmap result = new Bitmap(image.Width, image.Height);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.DrawImage(image, points, rect, GraphicsUnit.Pixel, ia);
			}

			return result;
		}
	}
}
