using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
	// Every place within the world needs an address, in this case a coord
	public struct Place
	{
		public int Y { get; set; }
		public int X { get; set; }

		// A default
		public Place(int y, int x)
		{
			Y = y;
			X = x;
		}
	}
}
