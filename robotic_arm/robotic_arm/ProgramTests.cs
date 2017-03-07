using System;
using NUnit.Framework;

namespace robotic_arm
{
	[TestFixture]
	public class ProgramTests
	{
		[TestCase("", false)]
		[TestCase("Add", false)]
		[TestCase("add 2", true)]
		public void ValidateCommandSyntaxTest(string command, bool valid)
		{
			BlockController bc = new BlockController();

			BaseCommand validatedCommand = bc.ParseCommandSyntax(command);

			// Assert
			Assert.That(validatedCommand != null, Is.EqualTo(valid));
		}

		[Test]
		public void SizeCommandAdjustsNumberOfSlotsTest()
		{
			// Arrange
			BlockController bc = new BlockController();
			SizeCommand sc = new SizeCommand(3);

			// Act
			sc.Execute(bc);

			// Assert
			Assert.That(bc.NumSlots, Is.EqualTo(3));
		}

		[TestCase(2, 2, 1)]
		[TestCase(2, 3, 0)]
		public void AddCommandAddsBlockToSlotTest(int size, int slot, int expectedNumBlocks)
		{
			// Arrange
			BlockController bc = new BlockController();
			SizeCommand sc = new SizeCommand(size);
			AddCommand ac = new AddCommand(slot);

			// Act
			sc.Execute(bc);
			ac.Execute(bc);

			// Assert
			Assert.That(bc.NumBlocksInSlot(slot), Is.EqualTo(expectedNumBlocks));
		}

		[TestCase(2, 2, 1, 1)]
		[TestCase(3, 2, 4, 0)]
		public void MoveCommandMovesBlockToSlotTest(int size, int source, int destination, int expectedNumBlocks)
		{
			// Arrange
			BlockController bc = new BlockController();
			SizeCommand sc = new SizeCommand(size);
			AddCommand ac = new AddCommand(source);
			MoveCommand mc = new MoveCommand(source, destination);

			// Act
			sc.Execute(bc);
			ac.Execute(bc);
			mc.Execute(bc);

			// Assert
			Assert.That(bc.NumBlocksInSlot(source), Is.EqualTo(0));
			Assert.That(bc.NumBlocksInSlot(destination), Is.EqualTo(expectedNumBlocks));
		}

		// TODO: Add more tests for remaining commands.
	}
}
