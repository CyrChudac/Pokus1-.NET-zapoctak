using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CoreLib;
using System.Drawing;

using System.Diagnostics;

namespace Pokus1
{
	public class Game
	{
		public static int ThreadsCount => Process.GetCurrentProcess().Threads.Count;
		public static readonly string LevelsFileName = "Levels";
		public static readonly string SaveFileName = "Saves";
		public static readonly string MapsFileName = "Saves";
		public static readonly string ImagesFileName = "Images";
		public static readonly string CurrentDirectory = 
			Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString(); 

		internal Environment map;
		private readonly Environment startingMap;
		private readonly IGameObjectOpener opener;
		public IMapRenderer Renderer { get; private set; }
		private IInputGetter inputGetter;
		private UiDoThis uiDoThis; 
		public Game(Environment map, IMapRenderer renderer, IGameObjectOpener opener, IInputGetter inputGetter, IWithToDo withToDo)
		{
			this.startingMap = map;
			this.opener = opener;
			this.inputGetter = inputGetter;
			this.Renderer = renderer;
			DelegatesQueue dq = new DelegatesQueue();
			this.uiDoThis = dq;
			withToDo.ToDo = dq;
		}
		int activePlayer = 0;
		public void FirstRun()
		{
			Time.Start();
			map = startingMap.Clone(); 
			Renderer.Camera = new Camera(new Size(map.Width * Environment.OneTileWidth, map.Height * Environment.OneTileHeight), Renderer);
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
			{
				Time.Stop();
				if (map.Victory)
				{
					uiDoThis.Do(() => opener.OpenControl<EndControl>());
				}
				else if (map.Defeat)
				{
					uiDoThis.Do(() => opener.OpenControl(
						new EndControl() { Text = "You Failed!" }));
				}
				else throw new Exception("Unexpected game end");
			}//<-----------TODO: udělat game over
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
					uiDoThis.Do(() => opener.OpenControl(menu));
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
					FirstRun();
					break;
				default:
					throw InputPoss.wrongInputFoundException;
			}
		}

		
	}
}