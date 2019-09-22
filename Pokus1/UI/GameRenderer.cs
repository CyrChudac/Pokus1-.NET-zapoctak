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
	public interface IMapRenderer : IWithCanvasSize
	{
		void FirstRender(Environment map);
		void Render();
		void SetCameraMovement(ILocationHolder locationHolder);
		void NewPlayerLocation(Location location);
		ICamera Camera { set; }
		Image Screenshot();
		Image DarkenImage(Image i, float DarkenCoeficient);
	}

	public partial class GameControl : IMapRenderer, IDisposable
	{
		Environment map;
		Image background;
		public ICamera Camera { set; protected get; }

		public void SetCameraMovement(ILocationHolder locationHolder) => Camera.locationHolder = locationHolder;
		public void NewPlayerLocation(Location location) => Camera.NewPlayerLocation(location);

		public void Render()
		{
			if (Time.IsRunning)
				Camera.Update();
			Refresh();
		}

		public Size CanvasSize => this.Size;

		public new void Dispose()
		{
			background.Dispose();
			base.Dispose();
		}

		public void FirstRender(Environment map)
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
			background = new Bitmap(map.Width * Environment.OneTileWidth, map.Height * Environment.OneTileHeight);
			using (Graphics g = Graphics.FromImage(background))
				for (int i = 0; i < map.Height; i++)
				{
					for (int j = 0; j < map.Width; j++)
					{
						g.FillRectangle(
							map[j, i].Brush,
							j * Environment.OneTileWidth,
							i * Environment.OneTileHeight,
							Environment.OneTileWidth,
							Environment.OneTileHeight);
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
						life.Location.x - Camera.Location.x,    //here the thread could be stopped so the location x and y 
						life.Location.y - Camera.Location.y,    //would be inconsistent. However I think this would only 
						life.Width,                             //make the moves smoother.
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
