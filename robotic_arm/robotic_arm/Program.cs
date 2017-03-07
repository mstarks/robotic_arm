using System;
using System.Collections.Generic;

namespace robotic_arm
{
	public class MainProgram
	{
		public static void Main(string[] args)
		{
			BlockController bc = new BlockController();

			bc.Init();
			bc.Run();
		}

	}

}
