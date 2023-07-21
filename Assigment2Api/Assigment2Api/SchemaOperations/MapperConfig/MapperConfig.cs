using Assigment2Api.DBOperations.Domain;
using TransactionModel = Assigment2Api.DBOperations.Domain.Transaction; // alias for the Transaction class
using AutoMapper;
using Assigment2Api.SchemaOperations.Transaction;

namespace Assigment2Api.SchemaOperations.MapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<TransactionRequest, TransactionModel>(); // alias for Transaction
            CreateMap<TransactionModel, TransactionResponse>(); // alias for Transaction
        }
    }
}
