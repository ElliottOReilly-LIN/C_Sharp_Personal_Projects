using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
	// Every place leads to somewhere else, or has neighbours that can be addressed
	public struct Doors
	{
		public Place North { get; set; }
		public Place East { get; set; }
		public Place West { get; set; }
		public Place South { get; set; }

		public Doors(Place west, Place east, Place south, Place north)
		{
			East = east;
			West = west;
			North = north;
			South = south;
		}
	}
}
