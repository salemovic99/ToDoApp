using Microsoft.EntityFrameworkCore;
using TODOAPP.Data;

namespace TODOAPP.Repositoies
{
    public class BaseReopsitory<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;

        public BaseReopsitory(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public bool Add(T entity)
        {
            try
            {
                dbSet.Add(entity);
                savechanages();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                dbSet.Remove(GetById(id));
                savechanages();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IList<T> GetAll() => dbSet.ToList();

        public T GetById(int id) => dbSet.Find(id);

        public bool Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
                savechanages();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        void savechanages()
        {
            context.SaveChanges();
        }
    }
}
