using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.Data
{
	public interface IBoardsDataService
	{
		Task<BoardsBrieflyOutputVM> ImportDataForBrieflyBoardAsync();
		public Task<BoardsAllOutputVM> ImportDataForAllBoardAsync();
	}
}