using Interfaces;
using System.Configuration;

namespace General.DataAccess
{
	public class FileStorage : ICachedInput
	{
		private string GetFilePath(int year, int day, string user)
		{
			return $"{ConfigurationManager.ConnectionStrings["DefaultFileLocation"].ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)}\\{year}\\{day}\\{user}.txt";
		}

		public bool TryLoadPuzzleInput(int year, int day, string user, out IList<(string, string)> PuzzleInput)
		{
			string path = GetFilePath(year, day, user);
			throw new NotImplementedException();
		}

		public void StorePuzzleInput(PuzzleData puzzleData)
		{
			string path = GetFilePath(puzzleData.Year, puzzleData.Day, puzzleData.user);
			throw new NotImplementedException();
		}

		public bool TryLoadPuzzleInputAllUsers(int year, int day, out IList<(string, string)> PuzzleInput)
		{
			throw new NotImplementedException();
		}
	}
}
