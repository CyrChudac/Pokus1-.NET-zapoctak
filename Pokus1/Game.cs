using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CoreLib;
using System.Drawing;

namespace Pokus1
{
	class Game
	{
		public static readonly string SaveFileName = "Saves";
		public static readonly string MapsFileName = "Maps";

		internal Map map;
		private readonly Map startingMap;
		private readonly GameControl gameForm;
		public IMapRenderer Renderer { get; private set; }
		private IInputGetter inputGetter;
		private UiDoThis uiDoThis; 
		public Game(Map map, IMapRenderer renderer, GameControl gameForm, IInputGetter inputGetter)
		{
			this.startingMap = map;
			this.gameForm = gameForm;
			this.inputGetter = inputGetter;
			this.Renderer = renderer;
			DelegatesQueue dq = new DelegatesQueue();
			this.uiDoThis = dq;
			gameForm.ToDo = dq;
		}
		int activePlayer = 0;
		public void FirstRun()
		{
			Time.Start();
			map = startingMap.Clone();  /* startingMap; /*	*/
			Renderer.Camera = new Camera(new Size(map.Width * Map.OneTileWidth, map.Height * Map.OneTileHeight), Renderer);
			SetCorrectCameraMovement();
			Renderer.FirstRender(map);
		}
		public void Update()
		{
			if (Time.IsRunning)
			{
				Time.Update();
				PropagateInput();
				map.Update(ref activePlayer);
			}
			if (map.GameEnd)
			{ }//<-----------TODO: udělat game over
		}
		void PropagateInput()
		{
			lock (inputGetter.CurrPlayerButtons)
			{
				foreach (var item in inputGetter.CurrPlayerButtons)
					map.Players[activePlayer].PropagateInput(item);
			}
			lock(inputGetter.CurrGameButtons)
			{
				foreach (var item in inputGetter.CurrGameButtons)
					ProcessGameInput(item);
				inputGetter.CurrGameButtons.Clear();
			}
		}

		void SetCorrectCameraMovement()
		{
			Renderer.NewPlayerLocation(map.Players[activePlayer].Middle);
			Renderer.SetCameraMovement(map.Players[activePlayer].Movement);
		}

		void ProcessGameInput(Input input)
		{
			switch (input)
			{
				case var x when (x is Input.WholeGame.Menu):
					inputGetter.Reset();
					InGameMenu menu = new InGameMenu();
					menu.Dock = DockStyle.Fill;
					menu.Map = this.map;
					menu.BackgroundImage = Renderer.DarkenImage( Renderer.Screenshot(), 0.8f);
					menu.BackgroundImageLayout = ImageLayout.Stretch;
					uiDoThis.Do(() => gameForm.Form.OpenControl(menu));
					break;
				case var x when (x is Input.WholeGame.ChangeChar.Right):
					activePlayer = (activePlayer - 1).Modulo(map.Players.Count);
					SetCorrectCameraMovement();
					break;
				case var x when (x is Input.WholeGame.ChangeChar.Left):
					activePlayer = (activePlayer + 1).Modulo(map.Players.Count);
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

		
	}
}