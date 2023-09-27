namespace TODOAPP.Repositoies
{
    public interface IBaseRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);

        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
