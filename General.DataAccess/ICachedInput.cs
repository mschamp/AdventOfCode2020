using Interfaces;

namespace General.DataAccess
{
	public interface ICachedInput
	{
		/// <summary>
		/// Load the input data from a certain puzzle from cache
		/// </summary>
		/// <param name="year">year of the puzzle</param>
		/// <param name="day">day of the puzzle</param>
		/// <param name="user">Name of the user that got the input</param>
		/// <param name="PuzzleInput">output parameter that will contain the input if found</param>
		/// <returns><c>True</c> if input is found</returns>
		bool TryLoadPuzzleInput(int year, int day, string user, out IList<(string, string)> PuzzleInput);
		bool TryLoadPuzzleInputAllUsers(int year, int day, out IList<(string, string)> PuzzleInput);
		
		void StorePuzzleInput(PuzzleData puzzleData);
	}
}