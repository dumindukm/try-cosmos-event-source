using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustodianService.Models
{
    public class CustomerSecurityTransaction
    {
        public CustomerSecurityTransaction()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

        public static CustomerSecurityTransaction CreateStockBoughTransaction(string cdsIdentifier, StockBoughEvent stockBoughEvent)
        {
            var trx = new CustomerSecurityTransaction();
            trx.CdsIdentifier = cdsIdentifier;
            trx.EventType = "StockBought";
            trx.EventInstanceType = typeof(StockBoughtHandler).AssemblyQualifiedName;
            trx.Data = JsonSerializer.Serialize(stockBoughEvent);
            return trx;
        }
        
        public Guid Id { get; set; }
        public string CdsIdentifier { get; set; }
        public int Position { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EventType { get; set; } // E.g: buy stock , sell stock, dividend pay , buy bond
        public string EventInstanceType { get; set; } // actual class for handle event

        public string Data { get; set; } // serialized event or transaction data
    }

    
}