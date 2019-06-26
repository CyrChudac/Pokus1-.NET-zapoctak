using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CoreLib;
using System.Drawing;
using System.Drawing.Imaging;

namespace Pokus1
{
	class Game
	{
		private Map map;
		private readonly Map startingMap;
		private readonly GameControl gameForm;
		private IMapRenderer renderer;
		private IInputGetter inputGetter;
		public Game(Map map, IMapRenderer renderer, GameControl gameForm, IInputGetter inputGetter)
		{
			this.startingMap = map;

			gameForm.Game = this;
			this.gameForm = gameForm;
			this.inputGetter = inputGetter;
			this.renderer = renderer;
		}
		int activePlayer = 0;
		public void FirstRun()
		{
			Time.Restart();
			map = startingMap; /* startingMap.Clone();  /*	*/
			renderer.Camera = new Camera(map);
			SetCorrectCameraMovement();
			renderer.FirstRender(map);
		}
		public void Update()
		{
			if (Time.IsRunning)
			{
				Time.Update();
				PropagateInput();
				map.Update(ref activePlayer);
				renderer.Render();
			}
			if (map.GameEnd)
			{ }//<-----------TODO: udělat game over
		}
		void PropagateInput()
		{
			foreach (var item in inputGetter.CurrPlayerButtons)
				map.Players[activePlayer].PropagateInput(item);
			foreach (var item in inputGetter.CurrGameButtons)
				ProcessGameInput(item);
			inputGetter.CurrGameButtons.Clear();
		}

		void SetCorrectCameraMovement()
		{
			renderer.NewPlayerLocation(map.Players[activePlayer].Middle);
			renderer.SetCameraMovement(map.Players[activePlayer].Movement);
		}

		void ProcessGameInput(Input input)
		{
			switch (input)
			{
				case var x when (x is Input.WholeGame.Menu):
					inputGetter.Reset();
					InGameMenu menu = new InGameMenu();
					menu.Dock = DockStyle.Fill;
					menu.BackgroundImage = ScreenoshotAndDarken(darkenCoeficient: 0.8f);
					menu.BackgroundImageLayout = ImageLayout.Stretch;
					gameForm.Form.OpenControl(menu);
					break;
				case var x when (x is Input.WholeGame.ChangeChar.Right):
					activePlayer = (activePlayer - 1) % map.Players.Count;
					SetCorrectCameraMovement();
					break;
				case var x when (x is Input.WholeGame.ChangeChar.Left):
					activePlayer = (activePlayer + 1) % map.Players.Count;
					SetCorrectCameraMovement();
					break;
				case var x when (x is Input.WholeGame.Restart):
					map.SetDefeat();
					FirstRun();
					break;
				default:
					throw InputPoss.wrongInputFoundException;
			}
		}

		/// <param name="darkenCoeficient">The less the darker.</param>
		Bitmap ScreenoshotAndDarken(float darkenCoeficient)
		{
			#region screen
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
					Screen.PrimaryScreen.Bounds.Y,
					0,
					0,
					Screen.PrimaryScreen.Bounds.Size,
					CopyPixelOperation.SourceCopy);
			}
			#endregion


			#region darken
			float b = darkenCoeficient;
			ColorMatrix cm = new ColorMatrix(new float[][]
			{
						new float[] {b, 0, 0, 0, 0},
						new float[] {0, b, 0, 0, 0},
						new float[] {0, 0, b, 0, 0},
						new float[] {0, 0, 0, 1, 0},
						new float[] {0, 0, 0, 0, 1},
			});
			ImageAttributes ia = new ImageAttributes();
			ia.SetColorMatrix(cm);
			Point[] points =
			{
						new Point(0, 0),
						new Point(bitmap.Width, 0),
						new Point(0, bitmap.Height),
					};
			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
			using (Graphics g = Graphics.FromImage(result)) {
				g.DrawImage(bitmap, points, rect, GraphicsUnit.Pixel, ia);
			}
			#endregion

			return result;
		}
	}
}