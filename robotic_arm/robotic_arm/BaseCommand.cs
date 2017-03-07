using System;
using System.Collections.Generic;

namespace robotic_arm
{
	public abstract class BaseCommand
	{
		protected List<int> _commandDetails;

		public abstract void Execute(BlockController bc);

	}

	public class SizeCommand : BaseCommand
	{
		public SizeCommand(int size)
		{
			_commandDetails = new List<int>();
			_commandDetails.Add(size);
		}

		public override void Execute(BlockController bc)
		{
			bc.CreateSlots(_commandDetails[0]);
		}
	}

	public class AddCommand : BaseCommand
	{
		public AddCommand(int slotNumber)
		{
			_commandDetails = new List<int>();
			_commandDetails.Add(slotNumber);
		}

		public override void Execute(BlockController bc)
		{
			bc.AddBlockToSlot(_commandDetails[0]);
		}
	}

	public class MoveCommand : BaseCommand
	{
		public MoveCommand(int source, int destination)
		{
			_commandDetails = new List<int>();
			_commandDetails.Add(source);
			_commandDetails.Add(destination);
		}

		public override void Execute(BlockController bc)
		{
			bc.AddBlockToSlot(_commandDetails[1]);
			bc.RemoveBlockFromSlot(_commandDetails[0]);
		}
	}

	public class RemoveCommand : BaseCommand
	{
		public RemoveCommand(int slotNumber)
		{
			_commandDetails = new List<int>();
			_commandDetails.Add(slotNumber);
		}

		public override void Execute(BlockController bc)
		{
			bc.RemoveBlockFromSlot(_commandDetails[0]);
		}
	}

}
