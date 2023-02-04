namespace ASP.Net_MVC_EntityFrameworkCore.Models
{
    public interface ILibrary<T>
    {
        IEnumerable<T> GetAll();
        T Add(T item);
        T? Get(int id);
        T Edit(T item);
        bool Delete(int id);
    }
}
