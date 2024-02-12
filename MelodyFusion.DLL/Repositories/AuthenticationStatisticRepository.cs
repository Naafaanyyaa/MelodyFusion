using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MelodyFusion.DLL.Repositories
{
    public  class AuthenticationStatisticRepository : Repository<AuthenticationStatisticDto>, IAuthenticationStatisticRepository
    {
        private readonly ILogger<AuthenticationStatisticRepository> _logger;

        public AuthenticationStatisticRepository(ApplicationDbContext context, ILogger<AuthenticationStatisticRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public override async Task<AuthenticationStatisticDto> AddAsync(AuthenticationStatisticDto entity)
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
                _logger.LogError("Error while creating statistic: {EMessage}", e.Message);
            }

            return new AuthenticationStatisticDto();
        }

        public override Task<AuthenticationStatisticDto?> GetByIdAsync(string id)
        {
            return _context.AuthenticationStatistic.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task UpdateAsync(AuthenticationStatisticDto entity)
        {

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.AuthenticationStatistic.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                _logger.LogError("Error while updating statistic: {EMessage}", e.Message);
            }
        }
    }
}
