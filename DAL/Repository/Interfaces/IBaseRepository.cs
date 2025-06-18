namespace WorkOutWebHookBot.DAL.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        Task Update(T entity);
        Task Delete(T entity);
        Task Create(T entity);
    }
}
