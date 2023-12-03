namespace General
{
	public abstract class abstractPuzzleClass : General.IAoC
    {
        public abstractPuzzleClass(int day, int year)
        {
            Day = day;
            Year = year;
        }

        public int Day { get; private set; }

		public int Year { get; private set; }

		public abstract string SolvePart1(string input = null);

        public abstract string SolvePart2(string input = null);

        public abstract void Tests();
    }
}
