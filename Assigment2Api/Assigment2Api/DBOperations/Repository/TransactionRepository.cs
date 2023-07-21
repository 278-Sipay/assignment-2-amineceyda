using Assigment2Api.DBOperations.DBContext;
using Assigment2Api.DBOperations.Domain;
using Assigment2Api.DBOperations.Repository.Base;
using System.Linq.Expressions;

namespace Assigment2Api.DBOperations.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly SimDbContext dbContext;
        public TransactionRepository(SimDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> filterExpression)
        {
            return dbContext.Set<Transaction>().Where(filterExpression).ToList();
        }

        public List<Transaction> GetByReference(string reference)
        {
            return dbContext.Set<Transaction>().Where(x => x.ReferenceNumber == reference).ToList();
        }
    }
}
