using Assignment2Api.Data.Domain;
using Assignment2Api.Data.Repository;
using System.Linq.Expressions;

namespace Assignment2Api.Data.Repository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    List<Transaction> GetByReference(string reference);


    // New method to get transactions by parameters
    List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> filterExpression);
}
