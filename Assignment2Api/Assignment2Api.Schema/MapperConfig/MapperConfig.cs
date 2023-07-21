
using SchemaTransaction = Assignment2Api.Schema.Transaction; // Alias for the Schema.Transaction namespace
using DomainTransaction = Assignment2Api.Data.Domain.Transaction; // Alias for the Transaction class in the Domain namespace
using AutoMapper;

namespace Assignment2Api.Schema.MapperConfig
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SchemaTransaction.TransactionRequest, DomainTransaction>();
            CreateMap<DomainTransaction, SchemaTransaction.TransactionResponse>();
        }
    }
}
