namespace General
{
	public interface IAoC
    {
        public string SolvePart1(string input = null);
        public string SolvePart2(string input = null);
        public void Tests();

        public int Day { get; }
        public int Year { get; }
    }
}
