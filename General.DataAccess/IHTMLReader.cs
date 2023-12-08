
namespace General.DataAccess
{
	public interface IHTMLReader
	{
		IList<(string, string)> GetInputData(int day, int year);
	}
}