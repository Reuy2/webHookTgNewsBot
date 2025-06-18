using WorkOutWebHookBot.DAL.Entities;
using WorkOutWebHookBot.DAL.Repository.Interfaces;

namespace WorkOutWebHookBot.DAL.Repository.Implementations
{
    public class PartnerRepository : IBaseRepository<Partner>
    {
        private readonly ApplicationContext _db = new ApplicationContext();

        public async Task Create(Partner entity)
        {
            _db.Partners.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Partner entity)
        {
            _db.Partners.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Partner> GetAll()
        {
            return _db.Partners;
        }

        public async Task Update(Partner entity)
        {
            _db.Partners.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
