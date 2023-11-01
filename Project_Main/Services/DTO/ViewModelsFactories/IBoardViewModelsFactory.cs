using Project_DomainEntities;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.DTO.ViewModelsFactories
{
    public interface IBoardViewModelsFactory
    //public interface IBoardViewModelsFactory : IViewModelFactory
    {
        IBoardViewModel GetViewModelAsync<ViewModelType>(IEnumerable<ITodoListModel> todoLists) where ViewModelType : class, IBoardViewModel;

        //public IBoardsBrieflyOutputVM CreateView(IEnumerable<ITodoListModel> todoLists);

            //public IBoardsAllOutputVM CreateView(IEnumerable<ITodoListModel> todoLists);
            //IBoardViewModel GetViewModelAsync<ViewModelType>(IEnumerable<ITodoListModel> todoLists) where ViewModelType : class, IBoardViewModel;
    }
}