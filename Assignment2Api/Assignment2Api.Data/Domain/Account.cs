
using Assignment2Api.Base.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2Api.Data.Domain
{
    [Table("Account", Schema = "dbo")]
    public class Account : BaseModel
    {
        //PK
        public int AccountNumber { get; set; }


        public int CustomerNumber { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal Balance { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsActive { get; set; }


        public virtual List<Transaction> Transactions { get; set; }
    }

}
