using Assignment2Api.Base;
using Assignment2Api.Data.Domain;
using Assignment2Api.Data.Repository;
using Assignment2Api.Schema;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Assignment2Api.Service;



[ApiController]
[Route("sipy/api/[controller]")]
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
        // Create a filter expression based on the parameters
        Expression<Func<Transaction, bool>> filterExpression =
            x => (
                string.IsNullOrEmpty(accountNumber) || x.AccountNumber.ToString() == accountNumber) &&
                (!minAmountCredit.HasValue || x.CreditAmount >= minAmountCredit) &&
                (!maxAmountCredit.HasValue || x.CreditAmount <= maxAmountCredit) &&
                (!minAmountDebit.HasValue || x.DebitAmount >= minAmountDebit) &&
                (!maxAmountDebit.HasValue || x.DebitAmount <= maxAmountDebit) &&
                (string.IsNullOrEmpty(description) || x.Description.Contains(description)) &&
                (!beginDate.HasValue || x.TransactionDate >= beginDate) &&
                (!endDate.HasValue || x.TransactionDate <= endDate) &&
                (string.IsNullOrEmpty(referenceNumber) || x.ReferenceNumber == referenceNumber);

        var entityList = repository.GetByParameter(filterExpression);   // Get the matching transactions

        var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList); // Map the entities to response objects

        return new ApiResponse<List<TransactionResponse>>(mapped);     // Return the response objects
    }

    // GetByParameter fonksiyonu, parametreler ile işlemler alır.
    // Parametreler hesap numarası, minimum kredi tutarı, maksimum kredi tutarı,
    // minimum borç tutarı, maksimum borç tutarı, açıklama, başlangıç ​​tarihi, bitiş tarihi ve
    // referans numarası'dır.
    // Fonksiyon, parametrelere göre bir filtre ifadesi oluşturur ve eşleşen işlemleri depolardan alır.
    // Fonksiyon daha sonra varlıkları yanıt nesnelerine eşler ve yanıt nesnelerini döndürür.
}



