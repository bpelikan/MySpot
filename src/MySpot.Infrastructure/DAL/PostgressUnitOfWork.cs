namespace MySpot.Infrastructure.DAL
{
    internal sealed class PostgressUnitOfWork : IUnitOfWork
    {
        public MySpotDbContext _dbContext;

        public PostgressUnitOfWork(MySpotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
