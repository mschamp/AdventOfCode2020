using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.DataAccess
{
	public class FileStorage : ICachedInput
	{
		public bool TryLoadPuzzleInput(int year, int day, string user, out string PuzzleInput)
		{
			throw new NotImplementedException();
		}

		public void StorePuzzleInput(PuzzleData puzzleData)
		{
			throw new NotImplementedException();
		}
	}
}
