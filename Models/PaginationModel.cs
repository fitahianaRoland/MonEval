namespace dotnetEval.Models;
public class PaginationModel<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public List<T>? ListObjets { get; set; }
}
