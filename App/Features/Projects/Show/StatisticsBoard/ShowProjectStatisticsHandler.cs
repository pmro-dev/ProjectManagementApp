using App.Infrastructure.Databases.App.Interfaces;
using MediatR;

namespace App.Features.Projects.Show.StatisticsBoard;

public class ShowProjectStatisticsHandler :
    IRequestHandler<ShowProjectStatisticsQuery, ShowProjectStatisticsQueryResponse>
{
    private readonly IDataUnitOfWork _dataUnitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ShowProjectStatisticsHandler> _logger;

    public ShowProjectStatisticsHandler(IDataUnitOfWork dataUnitOfWork, ILogger<ShowProjectStatisticsHandler> logger)
    {
        _dataUnitOfWork = dataUnitOfWork;
        _projectRepository = _dataUnitOfWork.ProjectRepository;
        _logger = logger;
    }

    public Task<ShowProjectStatisticsQueryResponse> Handle(ShowProjectStatisticsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
