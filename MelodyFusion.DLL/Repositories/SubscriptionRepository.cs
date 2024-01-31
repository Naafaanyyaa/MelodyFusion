using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MelodyFusion.DLL.Repositories
{
    public class SubscriptionRepository : Repository<SubscriptionDto>, ISubscriptionRepository
    {
        private readonly ILogger<SubscriptionRepository> _logger;
        public SubscriptionRepository(ApplicationDbContext context, ILogger<SubscriptionRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public override async Task<SubscriptionDto> AddAsync(SubscriptionDto entity)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while creating disease: {EMessage}", e.Message);
            }

            return new SubscriptionDto();
        }

        public override Task<SubscriptionDto?> GetByIdAsync(string id)
        {
            return _context.Subscription.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task UpdateAsync(SubscriptionDto entity)
        {

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Subscription.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while updating disease: {EMessage}", e.Message);
            }
        }
    }
}
