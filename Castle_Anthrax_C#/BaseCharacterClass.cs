using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
	public class BaseCharacterClass
	{
		public string name;
		public string description;

		//Stats
		private int health;
		private int strenght;
		private int gold;
		private int intelligence;
		private int luck;

		

		public string Name_
		{
			get { return name; }
			set { name = value; }
		}
		public string _Description
		{
			get { return description; }
			set { description = value; }
		}
		public int _Health
		{
			get { return health; }
			set { health = value; }
		}
		public int _Strength
		{
			get { return strenght; }
			set { strenght = value; }
		}
		public int _Gold
		{
			get { return gold; }
			set { gold = value; }
		}

		public int _Intelligence
		{
			get { return intelligence; }
			set { intelligence = value; }
		}
		public int _Luck
		{
			get { return luck; }
			set { luck = value; }
		}
		
	}
}
