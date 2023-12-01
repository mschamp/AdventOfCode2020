using Interfaces;

namespace General.DataAccess
{
	public interface IDB
	{
		bool LoadPuzzleInput(int year, int day, string user, out string PuzzleInput);
		void StorePuzzleInput(PuzzleData puzzleData);
	}
}