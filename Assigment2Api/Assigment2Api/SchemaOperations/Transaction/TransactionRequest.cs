namespace Assigment2Api.SchemaOperations.Transaction
{
    public class TransactionRequest
    {
        public int AccountNumber { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
