using System;
using System.Collections.Generic;

namespace Maze
{
	class Program
	{
		
		static void Main(string[] args)
		{
			//BasePlayerClass _newPlayer = new BasePlayerClass();
			//GetNewPlayer(_newPlayer).BraveKnight();

			//printnewPlayer(_newPlayer);
			Console.CursorVisible = false;
			// How big is the world - set the size

			Location[,] World = new Location[4, 8];



			// Create a simple world
			CreateWorld(ref World);
			List<string> items = new List<string> { "Gold", "Weapon", "Potion", "scrolls"};
			AddRandomObject(ref World, items);
			// Create a stack
			Stack stack = new Stack();

			// Seed stack with starting place
			stack.Push(new Place(0, 0));

			// We will save the path that the backtracker uses
			List<Place> Path = new List<Place>();


			// Create Maze using recursive BackTracker
			BackTracker(ref World, new Place(0, 0), ref stack, ref Path);

			DisplayWorld(World);
			AnimatePath(World, Path);
			navigate(World, new Place(0, 0), new Place(0, 0));
			Console.CursorVisible = true;

		
		}
		//Animate Path

		public static void AnimatePath(Location[,] World, List<Place> Path)

		{

			int x = 2;
			int y = 1;
			int oldX = 2;
			int oldY = 2;
			foreach (Place address in Path)
			{
				System.Threading.Thread.Sleep(50);

				Console.BackgroundColor = ConsoleColor.Black;

				Console.SetCursorPosition(oldX, oldY);

				Console.Write("   ");

				Console.SetCursorPosition(x, y + 1);

				Console.Write("   ");

				x = address.X * 4 + 2;

				y = address.Y * 3 + 2;

				Console.BackgroundColor = ConsoleColor.DarkMagenta;

				Console.SetCursorPosition(x, y);

				Console.Write("   ");

				Console.SetCursorPosition(x, y + 1);

				Console.Write("   ");

				oldX = x;
				oldY = y;
			}
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}

		private static void AddRandomObject(ref Location[,] world, List<string> Items)
		{
			Random random = new Random(Guid.NewGuid().GetHashCode());
			int i = random.Next(world.GetLength(0));
			int j = random.Next(world.GetLength(1));
			world[i, j].Thing = Items[random.Next(Items.Count)];
			Console.WriteLine("({0}, {1}) has {2}", i, j, world[i, j].Thing);
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


		//private static void printnewPlayer(BasePlayerClass newPlayer)
		//{
		//	Console.WriteLine("Player Title: {0}\n Description: {1}\n Current Health: {2}\n", newPlayer.name, newPlayer.description, newPlayer._Health );
		//}

		//private static BasePlayerClass GetNewPlayer(BasePlayerClass _newPlayer)
		//{
		//	_newPlayer.name= "Brave Sir Robin";
		//	_newPlayer.description = " Valiant and brave hero Knight";
		//	_newPlayer._Health = 100;

		//	return _newPlayer;
		//}

		private static void navigate(Location[,] world, Place currentPlace, Place previousPlace)
		{
			int offset_X = 3; // Start coords for the dot display
							  //string arrowKey;

			string userInput;
			int offset_Y = 2;
			bool done = false;
			bool Direction;
			while (!(done))
			{
				if (world[currentPlace.Y, currentPlace.X].Thing != null) DisplayFound(world[currentPlace.Y, currentPlace.X].Thing, world);
				deleteDot((previousPlace.X * 4) + offset_X, (previousPlace.Y * 3) + offset_Y);
				displayDot((currentPlace.X * 4) + offset_X, (currentPlace.Y * 3) + offset_Y);
				userInput = askDirChoice(world);

				//arrowKey = keyStroke(); // Take input and let the console detect an arrow key
				if (userInput == "quit") done = true;
				ClearError(world);

				Direction = getDir(world, userInput, currentPlace); //Can we go that way?
				if
					(Direction == false)
						DisplayError(userInput, world);
				else
					ClearError(world);
				

				previousPlace = currentPlace;
				currentPlace = Move(world, Direction, userInput, currentPlace); //Move in valid direction and send it back to nextPlace
			}
		}

		private static void DisplayFound(string found, Location[,] world)
		{
			Console.SetCursorPosition(0, (world.GetLength(0)) * 3 + 5);
			Console.WriteLine("You have found: {0}", found);
		}

		private static void inputError(string userInput, Location[,] world)
		{
			//SetTextColour();
			Console.SetCursorPosition(0, (world.GetLength(0)) * 3 + 5);
			Console.Write("Please try again {0} ", userInput);
			//ResetTextColour
		}

		private static bool getDir(Location[,] World, string keyPress, Place currentPlace)
		{
			bool condition = false;
			Doors validRoom = World[currentPlace.Y, currentPlace.X].Neighbours;
			if (validRoom.East.X != -1 && keyPress == "East") condition = true;
			if (validRoom.West.X != -1 && keyPress == "West") condition = true;
			if (validRoom.North.Y != -1 && keyPress == "North") condition = true;
			if (validRoom.South.Y != -1 && keyPress == "South") condition = true;
			return condition;

		}

		private static void DisplayError(string arrowKey, Location[,] world)
		{
			
			//SetTextColour();
			Console.SetCursorPosition(0, (world.GetLength(0)) * 3 + 5);
			Console.Write("You can't go {0} ", arrowKey);
			//ResetTextColour();
		}

		private static void ClearError(Location[,] world)
		{
			//SetTextColour();
			Console.SetCursorPosition(0, (world.GetLength(0)) * 3 + 5);
			Console.Write("{0}", new String(' ', Console.BufferWidth));
			//ResetTextColour();
		}

		private static Place Move(Location[,] world, bool vaildDir, string keyPress, Place currentPlace)
		{
			if (vaildDir == true && keyPress == "East") return new Place(currentPlace.Y, currentPlace.X + 1);
			if (vaildDir == true && keyPress == "West") return new Place(currentPlace.Y, currentPlace.X - 1);
			if (vaildDir == true && keyPress == "North") return new Place(currentPlace.Y - 1, currentPlace.X);
			if (vaildDir == true && keyPress == "South") return new Place(currentPlace.Y + 1, currentPlace.X);
			return currentPlace;
		}

		private static void displayDot(int X, int Y)
		{
			Console.SetCursorPosition(X, Y);
			Console.Write("°");
			Console.SetCursorPosition(X, Y + 1);
			Console.Write("°");
		}

		private static void deleteDot(int X, int Y)
		{
			Console.SetCursorPosition(X, Y);
			Console.Write(" ");
			Console.SetCursorPosition(X, Y + 1);
			Console.Write(" ");
		}
		private static string askDirChoice(Location [,] world)
		{
			Console.SetCursorPosition(0, (world.GetLength(0)) * 3 + 3);
			bool isValid = false;
			Console.WriteLine("In which direction would you like to travel? Please enter - [E] [W] [N] [S] :");

			string dir = "";

			while (!(isValid))
			{
				ConsoleKey readKey = Console.ReadKey().Key;

				if (readKey == ConsoleKey.E)
				{
					isValid = true;
					dir = "East";
				}
				if (readKey == ConsoleKey.W)
				{
					isValid = true;
					dir = "West";
				}
				if (readKey == ConsoleKey.N)
				{
					isValid = true;
					dir = "North";
				}
				if (readKey == ConsoleKey.S)
				{
					isValid = true;
					dir = "South";
				}
				if (!(isValid))
					inputError(world);
				
			}
			return dir;

		}

		private static void inputError(Location[,] world)
		{
			Console.SetCursorPosition(0, (world.GetLength(0)) * 3 + 5);
			Console.Write("Bad Key Entry, please try again!");
		}

		//private static string keyStroke()
		//{
		//	bool isValid = true;
		//	while (isValid)
		//	{
		//		ConsoleKey readKey = Console.ReadKey().Key;
		//		if (readKey == ConsoleKey.RightArrow) return "East";
		//		if (readKey == ConsoleKey.LeftArrow) return "West";
		//		if (readKey == ConsoleKey.UpArrow) return "North";
		//		if (readKey == ConsoleKey.DownArrow) return "South";
		//		if (readKey == ConsoleKey.Q) return "quit";
		//	}
		//	return "none";
		//}

		// Recursive Backtracker Algorithm
		static void BackTracker(ref Location[,] World, Place current, ref Stack stack, ref List<Place> Path)
		{
			// Exit case
			if (stack.Count() == 0)
				return;

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

				if (stack.Count() == 0)
					return;

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
			if (current.X > previous.X)
				return "East";

			if (current.X < previous.X)
				return "West";

			if (current.Y > previous.Y)
				return "South";

			if (current.Y < previous.Y)
				return "North";

			return "";
		}

		// Pick a random neighbour
		static Place GetRandomNeighbour(List<Place> Neighbours)
		{

			// Get out quick if no neighbours
			if (Neighbours.Count == 0)
				return new Place(-1, -1);

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


		// Open the door in the direction of Dir. The neighbour in that direction needs the opposite door opening.
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

		// Draw world
		public static void DisplayWorld(Location[,] World)
		{
			//// Reset cursor position
			int x = 1;
			int y = 1;

			// Iterate through rows and columns
			for (int i = 0; i < World.GetLength(0); i++)
			{

				for (int j = 0; j < World.GetLength(1); j++)
				{

					// Draw all walls for each cell
					Console.SetCursorPosition(x, y);

					Console.Write("+---+");

					Console.SetCursorPosition(x, y + 1);

					Console.Write("|   |");

					Console.SetCursorPosition(x, y + 2);

					Console.Write("|   |");

					Console.SetCursorPosition(x, y + 3);

					Console.Write("+---+");

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
