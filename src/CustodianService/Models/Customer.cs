using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustodianService.Models
{
    public enum RecordStatus
    {
        Initial =0,
        Draft =1,
        PendngReview = 2,
        Published=3,
        Deleted = 4,
        Obsolete = 5,
        Inactive=6

    }
    public class Customer:BseAuditableEntity
    {
        public string CdsIdentifier { get; set; }
        public string Address{ get; set; }
        public string TelephotneNumber{get;set;}

        public static Customer InitCustomer()
        {
            Customer customer = new Customer();
            customer.RecStatus = RecordStatus.Initial;
            customer.CdsIdentifier = "cds-1";
            customer.Address = "test address";
            customer.DocNumber = 1;
            customer.Created = DateTime.Now;
            return customer;
        }

    }

    public class BseAuditableEntity
    {
        public Guid Id { get; set; }
        public int DocNumber { get; set; }
        public RecordStatus RecStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}