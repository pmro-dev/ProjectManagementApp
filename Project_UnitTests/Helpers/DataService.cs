using Project_Main.Models.DataBases.AppData.DbSetup;

namespace Project_UnitTests.Helpers
{
	public static class DataService
	{
		public static SeedData SeedBaseData { get; private set; } = new();

		public static void PrepareAllData()
		{
			TasksData.PrepareData(SeedBaseData);
			TodoListsData.PrepareData(SeedBaseData);
		}
	}
}
