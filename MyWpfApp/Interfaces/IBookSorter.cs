using DataAccess.Entitites;
using Domain.Entities;

namespace MyWpfApp.Interfaces;

public interface IBookSorter
{
    public List<BookDto> ApplySort(List<Book> books);
}
