using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
	// Every place is a location that can be described as having a name, description
	// doors or paths that lead to a neighbour. This location has an address.
	public struct Location
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Thing { get; set; }
		public Place Address { get; set; }
		public Doors Neighbours { get; set; }
		public bool Visited { get; set; }

		public Location(string name, string description, string thing, bool visited,
			Place address, Doors neighbours)
		{
			Name = name;
			Description = description;
			Thing = thing;
			Visited = visited;
			Address = address;
			Neighbours = neighbours;
		}
	}
}
