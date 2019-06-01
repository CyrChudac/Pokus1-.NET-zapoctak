using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CoreLib;

namespace Pokus1
{
	class Game
	{
		private Map map;
		private readonly Map startingMap;
		private readonly UserControl gameForm;
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
			map = startingMap; //<-------TODO: musim udělat klonování map
			renderer.FirstRender(map);
		}
		public void Update()
		{
			if (Time.IsRunning)
			{
				map.Update(ref activePlayer);
				DetermineInput(inputGetter.CurrButton);
				renderer.Render();
			}
			if (map.GameEnd)
			{ }//<-----------TODO: udělat game over
		}
		void DetermineInput(Input input)
		{
			switch (input)
			{
				case null:
				case var x when (x is Input.Player):
					map.Players.ElementAt(activePlayer).PropagateInput(input);
					break;
				case var x when (x is Input.WholeGame):
					ProcessGameInput(input);
					break;
			}
		}
		void ProcessGameInput(Input input)
		{
			switch (input)
			{
				case var x when (x is Input.WholeGame.Restart):
					map.SetDefeat();
					FirstRun();
					break;
				case var x when (x is Input.WholeGame.ChangeChar.Right):
					activePlayer = (activePlayer - 1) % map.Players.Count;
					break;
				case var x when (x is Input.WholeGame.ChangeChar.Left):
					activePlayer = (activePlayer + 1) % map.Players.Count;
					break;
				default:
					throw InputPoss.wrongInputFoundException;
			}
		}
	}
}