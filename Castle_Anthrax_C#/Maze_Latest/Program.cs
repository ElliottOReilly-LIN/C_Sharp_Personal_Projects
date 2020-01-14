using System;
using System.Collections.Generic; // List

namespace Maze
{
	class Program
	{
		static void Main(string[] args)
		{
			/* Size of maze scales with terminal size
			int XSize = (int)(Console.WindowWidth / 4) - 2;
			int YSize = (int)(Console.WindowHeight / 3) - 2;
			Will change back to this when I've sortout the console-resizing issue*/

			// How big is the world - set the size
			Location[,] World = new Location[6, 10];
			
			// Create a simple world
			CreateWorld(ref World);

			// Create a stack
			Stack stack = new Stack();

			// Seed stack with starting place
			stack.Push(new Place(0, 0));

			// We will save the path that the backtracker uses
			List<Place> Path = new List<Place>();

			// Create Maze using recursive BackTracker
			BackTracker(ref World, new Place(0, 0), ref stack, ref Path);

			// Display the Maze
			//Console.BackgroundColor = ConsoleColor.Black;
			//Console.CursorVisible = false;
			// Set cursor colour
			//Console.Clear();
			DisplayWorld(World);

			// Display the path. We could save this to a file to reproduce
			// the world
			// DisplayPath(Path);

			// Show the path that was taken when creating
			////AnimatePath(World, Path);
			//Console.SetCursorPosition(0, Console.WindowHeight - 1);
			//Console.CursorVisible = true;
			//Console.ResetColor();
			
			navigate(World, new Place(0, 0));
			
		}
		private static void navigate(Location[,] world, Place currentPlace)
		{
			int start_X = 3; // Start coords for the dot display 
			int start_Y = 2;
			Console.SetCursorPosition(start_X, start_Y);
			Console.Write("♦");

			string arrowKey = keyStroke(); // Take input and let the console detect an arrow key
			bool Direction = getDir(world, arrowKey, currentPlace); //Can we go that way?
			
			Place nextPlace = Move(world, Direction, arrowKey, currentPlace); //Move in valid direction and send it back to nextPlace

			diplayDot(world, nextPlace, start_X, start_Y); // Send off nextPlace to display function

			navigate(world, nextPlace); // Iterate through navigate with the next place
		}

		private static bool getDir(Location[,] world, string arrowKey, Place startPlace)
		{
			bool vaildDir = checkValidPath(world,  startPlace, arrowKey); // Check there is a valid direction to go in
			return vaildDir;
		}

		private static Place Move(Location[,] world, bool vaildDir, string arrowKey, Place currentPlace)
		{
			

			//Console.SetCursorPosition(st_X, st_Y);
			if (vaildDir == true && arrowKey == "East")
			{
				Place nextPlace = new Place(currentPlace.Y, currentPlace.X + 1);
				return nextPlace;
				//Console.Write("\n({0},{1})\n",nextPlace.Y, nextPlace.X); // test next line display
			}
		
		if (vaildDir == true && arrowKey == "West")
			{
				Place nextPlace = new Place(currentPlace.Y, currentPlace.X -1);
				return nextPlace;
			}

			if (vaildDir == true && arrowKey == "North")
			{
				Place nextPlace = new Place(currentPlace.Y -1, currentPlace.X);
				return nextPlace;
			}

			if (vaildDir == true && arrowKey == "South")
			{
				Place nextPlace = new Place(currentPlace.Y + 1, currentPlace.X);
				return nextPlace;
			}
			else return currentPlace;
		}

		private static void diplayDot(Location[,] world, Place nextPlace, int Y, int X)
		{

			Console.OutputEncoding = System.Text.Encoding.Unicode;

			// need to do some testing in here to increment x, and y
			Console.SetCursorPosition(Y, X);
			Console.WriteLine("♦");
			Console.ReadKey();

		}

		private static string keyStroke()
		{
			Console.WriteLine("Enter direction arrow Key: \n");

			bool isValid = true;
			while (isValid) { 
			ConsoleKey readKey = Console.ReadKey().Key;

				if (readKey == ConsoleKey.RightArrow)
				{
					string East = Convert.ToString(readKey);
					return "East";
				}
				else if (readKey == ConsoleKey.LeftArrow)
				{
					string West = Convert.ToString(readKey);
					return "West";
				}
				else if (readKey == ConsoleKey.UpArrow)
				{
					string North = Convert.ToString(readKey);
					return "North";
				}
				else if (readKey == ConsoleKey.DownArrow)
				{
					string South = Convert.ToString(readKey);
					return "South";
				}
				Console.WriteLine("\nInvaled key entry, please select an approprite Arrow Key!\n");

				}
			isValid = false;

			return "False";
			
		}
		private static bool checkValidPath(Location[,] World, Place dir, string arrowKey)
		{
			bool condition = true;
			
			{
				Doors validRoom = World[dir.X, dir.Y].Neighbours;

				if (validRoom.East.X != -1 && arrowKey == "East")
				{
					condition = true;
				}
				else if (validRoom.East.X == -1 && arrowKey == "East")
				{
					Console.WriteLine("Can not go East, try again!: ");
					condition = false;
				}

				if (validRoom.West.X != -1 && arrowKey == "West")
				{
					condition = true;
				}
				else if (validRoom.West.X == -1 && arrowKey == "West")
				{
					Console.WriteLine("Can not go that way, try again!: ");
					condition = false;
				}

				if (validRoom.North.Y != -1 && arrowKey == "North")
				{
					condition = true;
				}
				else if (validRoom.North.X == -1 && arrowKey == "North")
				{
					Console.WriteLine("Can not go that way, try again!: ");
					condition = false;

				}

				if (validRoom.South.Y != -1 && arrowKey == "South")
				{
					condition = true;
				}
				else if (validRoom.South.X == -1 && arrowKey == "South")
				{
					Console.WriteLine("Can not go that way, try again!: ");
					condition = false;

				}
			}
			return condition;
		}	
		



		// Animate Path
		//public static void AnimatePath(Location[,] World, List<Place> Path)
		//{
		

		//	int x = 2;
		//	int y = 1;
		//	int oldX = 2;
		//	int oldY = 2;
		//	foreach (Place address in Path)
		//	{
		//		System.Threading.Thread.Sleep(50);
		//		Console.BackgroundColor = ConsoleColor.Black;
		//		Console.SetCursorPosition(oldX, oldY);
		//		Console.Write("   ");
		//		Console.SetCursorPosition(x, y + 1);
		//		Console.Write("   ");

		//		x = address.X * 4 + 2;
		//		y = address.Y * 3 + 2;
		//		Console.BackgroundColor = ConsoleColor.Green;
		//		Console.SetCursorPosition(x, y);
		//		Console.Write("   ");
		//		Console.SetCursorPosition(x, y + 1);
		//		Console.Write("   ");

			
		//		oldX = x;
		//		oldY = y;

		//	}
		//	Console.BackgroundColor = ConsoleColor.Green;
		//}

		// Display the path taken to make the maze
		//static void DisplayPath(List<Place> path)
		//{

		//	Console.WriteLine("Number of places: {0}", path.Count);
		//	foreach (Place item in path)
		//	{
		//		Console.Write("({0},{1}) ", item.Y, item.X);
		//	}
		//}

		// Recursive Backtracker Algorithm 
		static void BackTracker(ref Location[,] World, Place current, ref Stack stack, ref List<Place> Path)
		{
			// Exit case
			if (stack.Count() == 0) return;

			// Save path
			Path.Add(current);

			// Mark the current place as visited
			World[current.Y, current.X].Visited = true;

			// Get current neighbours
			List<Place> neighbours = FindNeighbours(World, current);

			// If no neighbours recurse / backtrack hence the name
			while (neighbours.Count == 0)
			{
				current = stack.Pop();
				if (stack.Count() == 0) return;
				neighbours = FindNeighbours(World, current);
			}

			// Save our place
			Place previous = current;

			// We have neighbours, pick one
			current = GetRandomNeighbour(neighbours);

			// What direction did we go in?
			string direction = Direction(previous, current);

			// Open that door
			SetDoor(ref World, direction, previous);

			// Push the new place onto the statck
			stack.Push(current);

			// Recurse
			BackTracker(ref World, current, ref stack, ref Path);
		}

		// What direction did we go in? 
		static string Direction(Place previous, Place current)
		{
			if (current.X > previous.X) return "East";
			if (current.X < previous.X) return "West";
			if (current.Y > previous.Y) return "South";
			if (current.Y < previous.Y) return "North";
			return "";
		}

		// Pick a random neighbour
		static Place GetRandomNeighbour(List<Place> Neighbours)
		{
			// Get out quick if no neighbours
			if (Neighbours.Count == 0) return new Place(-1, -1);

			// Because this function is called within a loop we
			// need to randomise the seed!!!! Took a while to find this.
			// Try it without Guid.NewGuid().GetHashCode()
			Random random = new Random(Guid.NewGuid().GetHashCode());
			return Neighbours[random.Next(Neighbours.Count)];
		}

		// Find unvisited neighbours to this address
		static List<Place> FindNeighbours(Location[,] World, Place address)
		{
			List<Place> unVisitedNeighbours = new List<Place>();

			// Look west
			if (address.X > 0)
			{
				// If western neighbour hasn't been visited
				if (World[address.Y, address.X - 1].Visited == false)
				{
					// Western neighbour can be added to the list
					unVisitedNeighbours.Add(new Place(address.Y, address.X - 1));
				}
			}

			// Look north
			if (address.Y > 0)
			{
				// If northern neighbour hasn't been visited
				if (World[address.Y - 1, address.X].Visited == false)
				{
					// Northern neighbour can be added to the list
					unVisitedNeighbours.Add(new Place(address.Y - 1, address.X));
				}
			}

			// Look east
			if (address.X < World.GetLength(1) - 1)
			{
				// If eastern neighbour hasn't been visited
				if (World[address.Y, address.X + 1].Visited == false)
				{
					// Eastern neighbour can be added to the list
					unVisitedNeighbours.Add(new Place(address.Y, address.X + 1));
				}
			}

			// Look south
			if (address.Y < World.GetLength(0) - 1)
			{
				// If southern neighbour hasn't been visited
				if (World[address.Y + 1, address.X].Visited == false)
				{
					// Southern neighbour can be added to the list
					unVisitedNeighbours.Add(new Place(address.Y + 1, address.X));
				}
			}

			// Return all the valid neighbours, could be empty
			return unVisitedNeighbours;
		}

		// Open the door in the direction of Dir. The neighbour in that direction
		// needs the opposite door opening.
		static void SetDoor(ref Location[,] World, string Dir, Place address)
		{
			// Get current neighbours, so we don't lose them
			Doors doors = World[address.Y, address.X].Neighbours;

			// Which door?
			switch (Dir)
			{
				case "East":
					// You can only have an eastern neighbour if not on east edge
					if (address.X < World.GetLength(1) - 1)
					{
						// Set the east door to point to eastern neighbour
						doors.East = new Place(address.Y, address.X + 1);
						// Set our eastern neighbour's west door
						Doors doorsNeighbour = World[address.Y, address.X + 1].Neighbours;
						doorsNeighbour.West = address;
						World[address.Y, address.X + 1].Neighbours = doorsNeighbour;
					}
					break;

				case "West":
					// You can only have an western neighbour if not on west edge
					if (address.X > 0)
					{
						// Set the east door to point to western neighbour
						doors.West = new Place(address.Y, address.X - 1);
						// Set our eastern neighbour's east door
						Doors doorsNeighbour = World[address.Y, address.X - 1].Neighbours;
						doorsNeighbour.East = address;
						World[address.Y, address.X - 1].Neighbours = doorsNeighbour;

						
					}
					break;

				case "South":
					if (address.Y < World.GetLength(0) - 1)
					{
						// Set the south door to point to southern neighbour
						doors.South = new Place(address.Y + 1, address.X);
						// Set our eastern neighbour's north door
						Doors doorsNeighbour = World[address.Y + 1, address.X].Neighbours;
						doorsNeighbour.North = address;
						World[address.Y + 1, address.X].Neighbours = doorsNeighbour;
					}
					break;

				case "North":
					if (address.Y > 0)
					{
						// Set the north door to point to northern neighbour
						doors.North = new Place(address.Y - 1, address.X);
						// Set our eastern neighbour's south door
						Doors doorsNeighbour = World[address.Y - 1, address.X].Neighbours;
						doorsNeighbour.South = address;
						World[address.Y - 1, address.X].Neighbours = doorsNeighbour;
					}
					break;
			}

			// Put this address's modified doors back
			World[address.Y, address.X].Neighbours = doors;
		}

		// Create default world, populate with defaults
		public static void CreateWorld(ref Location[,] World)
		{
			for (int i = 0; i < World.GetLength(0); i++)
			{
				for (int j = 0; j < World.GetLength(1); j++)
				{
					// This means a wall, or more specifically NO door
					Place wall = new Place(-1, -1);
					World[i, j].Address = new Place(i, j); // set address
					World[i, j].Visited = false;
					World[i, j].Neighbours = new Doors(wall, wall, wall, wall);
				}
			}

		}

		// Draw world
		public static void DisplayWorld(Location[,] World)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;

			Console.BackgroundColor = ConsoleColor.Black;
			// Set cursor colour
			Console.ForegroundColor = ConsoleColor.White;
			// Reset cursor position
			int x = 1;
			int y = 1;
			// Iterate through rows and columns
			for (int i = 0; i < World.GetLength(0); i++)
			{
				for (int j = 0; j < World.GetLength(1); j++)
				{
					// Draw all walls for each cell
					Console.SetCursorPosition(x, y);
					Console.Write("+———+");
					Console.SetCursorPosition(x, y + 1);
					Console.Write("|   |");
					Console.SetCursorPosition(x, y + 2);
					Console.Write("|   |");
					Console.SetCursorPosition(x, y + 3);
					Console.Write("+———+");
					Location room = World[i, j];
					// Draw doors by over writing walls
					if (room.Neighbours.East.X != -1)
					{
						Console.SetCursorPosition(x + 4, y + 1);
						Console.Write(" ");
						Console.SetCursorPosition(x + 4, y + 2);
						Console.Write(" ");
					}
					if (room.Neighbours.West.X != -1)
					{
						Console.SetCursorPosition(x, y + 1);
						Console.Write(" ");
						Console.SetCursorPosition(x, y + 2);
						Console.Write(" ");
					}
					if (room.Neighbours.South.X != -1)
					{
						Console.SetCursorPosition(x, y + 3);
						Console.Write("+   +");
					}
					if (room.Neighbours.North.X != -1)
					{
						Console.SetCursorPosition(x, y);
						Console.Write("+   +");
					}
					// next cell
					x += 4;
				}
				// next row
				y += 3;
				// reset x
				x = 1;
			}
		
			// Force blank line
			Console.SetCursorPosition(x, y + 1);
			Console.WriteLine();

			
		}

	}
}


