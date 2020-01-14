using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
	// A stack to hold "Places"
	class Stack
	{
		List<Place> LIFO;
		int Top;
		// Construct
		public Stack()
		{
			// Pointer to nothing - stack is empty
			this.Top = -1;
			// Create empty stack;
			this.LIFO = new List<Place>();
		}
		// Push an address onto stack
		public void Push(Place address)
		{
			// Put the address onto stack
			this.LIFO.Add(address);
			// Increment pointer to the top of our stack
			this.Top++;
		}
		// Pop the top of the stack
		public Place Pop()
		{
			// If empty can't return anything
			if (this.Top < 0)
			{
				return new Place(-1, -1);
			}

			// Get the entry
			Place Popped = this.LIFO[Top];
			// Delete the entry
			this.LIFO.RemoveAt(Top);
			// Decrement the pointer
			this.Top--;

			return Popped;
		}
		// Count number of items on stack
		public int Count()
		{
			// Top is the pointer to the first Item
			// Therefore top + 1 is the number of items
			return this.Top + 1;
		}
	}
}
