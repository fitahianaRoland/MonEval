using dotnetEval.Models;
namespace dotnetEval;

public class GetPaginationModelService
{
public PaginationModel<T> ListShow<T>(int page, List<T> list)
{
    int pageSize = 5; // Nombre d'éléments par page
    var totalItems = list.Count(); // Nombre total d'objets dans la liste fournie

    var liste = list.Skip((page - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();

    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

    return new PaginationModel<T>
    {
        CurrentPage = page,
        TotalPages = totalPages,
        ListObjets = liste
    };
}
}
