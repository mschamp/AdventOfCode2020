﻿using System.Configuration;
using System.Data.SQLite;

namespace General.DataAccess
{
	public class SQLiteDB : IDB
	{

		private string GetConnectionString()
		{
			return ConfigurationManager.ConnectionStrings["default"].ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory); ;
		}

		public bool LoadPuzzleInput(int year, int day, string user, out string PuzzleInput)
		{
			PuzzleInput = "";
			bool found = false;

			using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
			{
				SQLiteDataReader SQLite_datareader;
				SQLiteCommand SQLite_cmd;
				SQLite_cmd = conn.CreateCommand();
				SQLite_cmd.CommandText = "SELECT input FROM puzzleInputs where year=@year and day=@day and user=@user LIMIT 1";
				SQLite_cmd.Parameters.AddWithValue("@year", year);
				SQLite_cmd.Parameters.AddWithValue("@day", day);
				SQLite_cmd.Parameters.AddWithValue("@user", user);

				conn.Open();
				SQLite_datareader = SQLite_cmd.ExecuteReader();
				if (SQLite_datareader.Read())
				{
					PuzzleInput = SQLite_datareader.GetString(0);
					found = true;
				}
				conn.Close();
			}

			return found;
		}

		public void StorePuzzleInput(Interfaces.PuzzleData puzzleData)
		{
			using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString()))
			{
				SQLiteDataReader SQLite_datareader;
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