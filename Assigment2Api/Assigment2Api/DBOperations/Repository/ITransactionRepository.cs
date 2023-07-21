
using Assigment2Api.DBOperations.Domain;
using Assigment2Api.DBOperations.Repository.Base;
using System.Linq.Expressions;

namespace Assigment2Api.DBOperations.Repository
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        List<Transaction> GetByReference(string reference);

        // New method to get transactions by parameters
        List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> filterExpression);
    }
}
