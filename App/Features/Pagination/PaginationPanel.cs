namespace App.Features.Pagination;

public record PaginationPanel(
    PaginationData PaginData,
    string ControllerName,
    string ActionName
);