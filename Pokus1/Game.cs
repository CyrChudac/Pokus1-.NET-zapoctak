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
		private UserControl gameForm;
		private IMapRenderer renderer;
		private IInputGetter inputGetter;
		private readonly long delay;
		public Game(Map map, IMapRenderer renderer, UserControl gameForm, IInputGetter inputGetter, long delay)
		{
			this.startingMap = map;  
			this.gameForm = gameForm;
			this.inputGetter = inputGetter;
			this.delay = delay;
			this.renderer = renderer;
		}
		int activePlayer = 0;
		public void Run()
		{
			map = startingMap; //<-------TODO: musim udělat klonování map
			gameForm.Show();
			renderer.FirstRender(map);
			long nextMove = delay;
			while (true)
			{
				if (Time.IsRunning)
				{
					Time.Update();
					nextMove -= Time.DeltaTime;

					if (nextMove <= 0)
					{
						map.Update(ref activePlayer);
						DetermineInput(inputGetter.CurrButton);
						renderer.Render();
					}
				}
				if (map.GameEnd)
					break;
			}
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
					Run();
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