using System;
using System.Collections.Generic;

namespace robotic_arm
{
	public class Slot
	{
		int _numBlocks;

		public string DisplayBlocks()
		{
			string blocksRep = string.Empty;
			for (int i = 0; i < _numBlocks; ++i)
			{
				blocksRep += "X";
			}

			return blocksRep;
		}

		public void AddBlock()
		{
			_numBlocks++;
		}

		public void RemoveBlock()
		{
			_numBlocks--;
		}

		public int NumBlocks()
		{
			return _numBlocks;
		}

	}

	public class BlockController
	{
		protected List<Slot> _slots;

		protected List<BaseCommand> _commandHistory;

		public BlockController()
		{
			_slots = new List<Slot>();
			_commandHistory = new List<BaseCommand>();
		}

		public void Init()
		{
			Console.WriteLine("Recognized commands: size, add, mv, rm, replay, undo");
		}

		public void Run()
		{
			bool first = true;
			bool keepGoing = true;
			while (keepGoing)
			{
				Console.WriteLine("Enter a command.");
				string command = Console.ReadLine();

				BaseCommand aCommand = ParseCommandSyntax(command);

				if (first)
				{
					if (!(aCommand is SizeCommand))
					{
						break;
					}
					first = false;
				}

				if (aCommand != null)
				{
					aCommand.Execute(this);
					DisplaySlots();
					_commandHistory.Add(aCommand);
				}

				if (_slots.Count == 0)
				{
					keepGoing = false;
				}
			}

		}

		public BaseCommand ParseCommandSyntax(string command)
		{
			string[] commandWords = command.Split(' ');
			if (commandWords.Length < 2 || commandWords.Length > 3)
			{
				return null;
			}

			string commandDetails1 = commandWords[1];
			string commandDetails2;

			if (commandWords[0] == "size")
			{
				return new SizeCommand(int.Parse(commandDetails1));
			}
			else if (commandWords[0] == "add")
			{
				return new AddCommand(int.Parse(commandDetails1));
			}
			else if (commandWords[0] == "mv")
			{
				commandDetails2 = commandWords[2];
				return new MoveCommand(int.Parse(commandDetails1), int.Parse(commandDetails2));
			}
			else if (commandWords[0] == "rm")
			{
				return new RemoveCommand(int.Parse(commandDetails1));
			}
			else if (commandWords[0] == "replay")
			{
				// TODO: using _commandHistory
			}
			else if (commandWords[0] == "undo")
			{
				// TODO: using _commandHistory
			}

			return null;
		}

		public void DisplaySlots()
		{
			for (int i = 0; i < _slots.Count; ++i)
			{
				Console.WriteLine((i + 1) + ": " + _slots[i].DisplayBlocks());
			}
		}

		public void CreateSlots(int numSlots)
		{
			_slots.Clear();
			for (int i = 0; i < numSlots; ++i)
			{
				Slot newSlot = new Slot();
				_slots.Add(newSlot);
			}
		}

		public void AddBlockToSlot(int slotNumber)
		{
			int index = slotNumber - 1;
			if (index >= 0 && index < _slots.Count)
			{
				_slots[index].AddBlock();
			}
		}

		public void RemoveBlockFromSlot(int slotNumber)
		{
			int index = slotNumber - 1;
			if (index >= 0 && index < _slots.Count)
			{
				_slots[index].RemoveBlock();
			}
		}

		public int NumSlots()
		{
			return _slots.Count;
		}

		public int NumBlocksInSlot(int slot)
		{
			int index = slot - 1;
			if (index >= 0 && index < _slots.Count)
			{
				return _slots[index].NumBlocks();
			}

			return 0;
		}

	}
}
