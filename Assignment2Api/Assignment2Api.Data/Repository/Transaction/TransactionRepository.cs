

using Assignment2Api.Data.Domain;
using System.Linq.Expressions;

namespace Assignment2Api.Data.Repository;


public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly SimDbContext dbContext;
    public TransactionRepository(SimDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Transaction> GetByReference(string reference)
    {
        return dbContext.Set<Transaction>().Where(x => x.ReferenceNumber == reference).ToList();
    }

    //GetBy parameter method
    public List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> filterExpression)
    {
        return dbContext.Set<Transaction>().Where(filterExpression).ToList();
    }
}
