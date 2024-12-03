using System.Configuration;
using System.Data.SQLite;

namespace General.DataAccess
{
	public class SQLiteDB : ICachedInput
	{

		private string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["default"].ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory); ;
		}

		public bool TryLoadPuzzleInput(int year, int day, string user, out IList<(string,string)> PuzzleInput)
		{
			PuzzleInput = [];
			bool found = false;

			using (SQLiteConnection conn = new(GetConnectionString()))
			{
				SQLiteDataReader SQLite_datareader;
				SQLiteCommand SQLite_cmd;
				SQLite_cmd = conn.CreateCommand();
				SQLite_cmd.CommandText = "SELECT input, user FROM puzzleInputs where year=@year and day=@day and user=@user LIMIT 1";
				SQLite_cmd.Parameters.AddWithValue("@year", year);
				SQLite_cmd.Parameters.AddWithValue("@day", day);
				SQLite_cmd.Parameters.AddWithValue("@user", user);

				conn.Open();
				SQLite_datareader = SQLite_cmd.ExecuteReader();
				if (SQLite_datareader.Read())
				{
					PuzzleInput.Add((SQLite_datareader.GetString(1),SQLite_datareader.GetString(0)));
					found = true;
				}
				conn.Close();
			}

			return found;
		}

		public bool TryLoadPuzzleInputAllUsers(int year, int day, out IList<(string, string)> PuzzleInput)
		{
			PuzzleInput = [];
			bool found = false;

			using (SQLiteConnection conn = new(GetConnectionString()))
			{
				SQLiteDataReader SQLite_datareader;
				SQLiteCommand SQLite_cmd;
				SQLite_cmd = conn.CreateCommand();
				SQLite_cmd.CommandText = "SELECT input, user FROM puzzleInputs where year=@year and day=@day";
				SQLite_cmd.Parameters.AddWithValue("@year", year);
				SQLite_cmd.Parameters.AddWithValue("@day", day);

				conn.Open();
				SQLite_datareader = SQLite_cmd.ExecuteReader();
				while (SQLite_datareader.Read())
				{
					PuzzleInput.Add((SQLite_datareader.GetString(1), SQLite_datareader.GetString(0)));
					found = true;
				}
				conn.Close();
			}

			return found;
		}

		public void StorePuzzleInput(Interfaces.PuzzleData puzzleData)
		{
			using (SQLiteConnection conn = new(GetConnectionString()))
			{
				SQLiteCommand SQLite_cmd;
				SQLite_cmd = conn.CreateCommand();
				SQLite_cmd.CommandText = "insert into puzzleInputs values (@year,@day,@user,@input)";
				SQLite_cmd.Parameters.AddWithValue("@year", puzzleData.Year);
				SQLite_cmd.Parameters.AddWithValue("@day", puzzleData.Day);
				SQLite_cmd.Parameters.AddWithValue("@user", puzzleData.user);
				SQLite_cmd.Parameters.AddWithValue("@input", puzzleData.input);

				conn.Open();
				SQLite_cmd.ExecuteNonQuery();
				Thread.Sleep(1000);
				conn.Close();
			}
		}
	}
}
