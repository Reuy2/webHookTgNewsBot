using WorkOutWebHookBot.DAL.Entities;
using WorkOutWebHookBot.DAL.Repository.Interfaces;

namespace WorkOutWebHookBot.DAL.Repository.Implementations
{
    public class BotUserRepository : IBaseRepository<BotUser>
    {
        private readonly ApplicationContext _db;

        public BotUserRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task Create(BotUser entity)
        {
            _db.BotUsers.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(BotUser entity)
        { 
            _db.BotUsers.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<BotUser> GetAll()
        {
            return _db.BotUsers;
        }

        public async Task Update(BotUser entity)
        {
            _db.BotUsers.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
