using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokus1
{
	public interface IAction
	{
		void Proceed();
	}

	public class NoAction: IAction
	{
		public void Proceed()
		{
			throw new NotImplementedException();
			//pokud startTime < currTime -> animace a změna startTime
		}
	}
}
