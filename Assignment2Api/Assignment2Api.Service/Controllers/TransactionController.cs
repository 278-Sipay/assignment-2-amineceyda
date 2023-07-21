using Assignment2Api.Base.Response;
using Assignment2Api.Data.Domain;
using Assignment2Api.Data.Repository;
using Assignment2Api.Schema.Transaction;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Assignment2Api.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository repository;
        private readonly IMapper mapper;
        public TransactionController(ITransactionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }



        [HttpGet]
        public ApiResponse<List<TransactionResponse>> GetAll()
        {
            var entityList = repository.GetAll();
            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapped);
        }

        [HttpGet("{id}")]
        public ApiResponse<TransactionResponse> Get(int id)
        {
            var entity = repository.GetById(id);
            var mapped = mapper.Map<Transaction, TransactionResponse>(entity);
            return new ApiResponse<TransactionResponse>(mapped);
        }

        [HttpGet("GetByReference")]
        public ApiResponse<List<TransactionResponse>> GetByReference(string ReferenceNumber)
        {
            var entityList = repository.GetByReference(ReferenceNumber);
            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapped);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] TransactionRequest request)
        {
            var entity = mapper.Map<TransactionRequest, Transaction>(request);
            repository.Insert(entity);
            repository.Save();
            return new ApiResponse();
        }

        //Get transactions by parameters
        [HttpGet("GetByParameter")]
        public ApiResponse<List<TransactionResponse>> GetByParameter(
            string accountNumber,
            decimal? minAmountCredit,
            decimal? maxAmountCredit,
            decimal? minAmountDebit,
            decimal? maxAmountDebit,
            string description,
            DateTime? beginDate,
            DateTime? endDate,
            string referenceNumber)
        {
            //filter expression based on the parameters //Call func
            var filterExpression = GetTransactionFilterExpression(
                accountNumber,
                minAmountCredit,
                maxAmountCredit,
                minAmountDebit,
                maxAmountDebit,
                description,
                beginDate,
                endDate,
                referenceNumber);

            // GenericRepository and the filter expression to get the matching transactions
            var entityList = repository.GetByParameter(filterExpression);

            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);

            return new ApiResponse<List<TransactionResponse>>(mapped);
        }

        // Helper method to build the filter expression 
        private Expression<Func<Transaction, bool>> GetTransactionFilterExpression(
            string accountNumber,
            decimal? minCreditAmount,
            decimal? maxCreditAmount,
            decimal? minDebitAmount,
            decimal? maxDebitAmount,
            string description,
            DateTime? beginDate,
            DateTime? endDate,
            string referenceNumber)
        {
            Expression<Func<Transaction, bool>> filterExpression = x =>
                (string.IsNullOrEmpty(accountNumber) || x.AccountNumber.ToString() == accountNumber) &&
                (!minCreditAmount.HasValue || x.CreditAmount >= minCreditAmount) &&
                (!maxCreditAmount.HasValue || x.CreditAmount <= maxCreditAmount) &&
                (!minDebitAmount.HasValue || x.DebitAmount >= minDebitAmount) &&
                (!maxDebitAmount.HasValue || x.DebitAmount <= maxDebitAmount) &&
                (string.IsNullOrEmpty(description) || x.Description.Contains(description)) &&
                (!beginDate.HasValue || x.TransactionDate >= beginDate) &&
                (!endDate.HasValue || x.TransactionDate <= endDate) &&
                (string.IsNullOrEmpty(referenceNumber) || x.ReferenceNumber == referenceNumber);

            return filterExpression;
        }



    }
}
