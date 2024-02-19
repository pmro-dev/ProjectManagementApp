using App.Features.Budgets.Common.Interfaces;
using App.Features.Budgets.Common.Models;

namespace App.Features.Budgets.Common
{
    public class BudgetFactory : IBudgetFactory
    {
        public BudgetDto CreateDto()
        {
            return new BudgetDto();
        }

        public BudgetModel CreateModel()
        {
            return new BudgetModel();
        }

        public BudgetModel CreateModel(string title, string description, Guid projectId, string ownerId)
        {
            return new BudgetModel(title, description, projectId, ownerId);
        }
    }
}
