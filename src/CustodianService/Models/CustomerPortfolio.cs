using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustodianService.Models.EventSource;

namespace CustodianService.Models
{
    public class CustomerPortfolio
    {
        public CustomerPortfolio(string cdsIdentifier)
        {
            Id= Guid.NewGuid();
            CdsIdentifier = cdsIdentifier;
        }
        public Guid Id{get;set;}
        public string CdsIdentifier { get; set; }

        public decimal TotalHodings{get;set;}
        
        public List<StockHolding> StockHoldings{get;set;} = new List<StockHolding>();
        public List<int> TreasuryBonds{get;set;}= new List<int>();

        public void BuildPortFolio(IList<CustomerSecurityTransaction> transactions)
        {
            StockHoldings = new List<StockHolding>();
            TreasuryBonds = new List<int>();
            foreach (var item in transactions)
            {
                Type t = Type.GetType(item.EventInstanceType);
                ISecurityTransactionHandler handler = Activator.CreateInstance(t) as ISecurityTransactionHandler;
                handler.Handle(this,item.Data);
            }

        }
    }
    

    public class StockHolding
    {
        public string Symbol { get; set; }
        public decimal StockValue{ get; set; }
    }

    public class Stock
    {
        public string Symbol { get; set; }
        public string CdsIdentifier { get; set; }
        public int NoOfShares { get; set; }
        public decimal SharePrice{get;set;}
        public decimal StockTotal => NoOfShares * SharePrice;
        public string AssetType {get;set;}

        public bool FullySoldStock => NoOfShares == 0;

        public List<StockTrade> StockTrades {get;set;}

        public class StockTrade
        {
            public int TradeType { get; set; }// buy or sell
            public int NoOfShares { get; set; }
            public decimal SharePrice{get;set;}
            public decimal StockTotal => NoOfShares * SharePrice;
            public DateTime TradeAt { get; set; }
        }
    }

    public class TreasuryBond
    {
        public int BondId { get; set; }
        public int BondAmount { get; set; }
        public decimal Interestrate{get;set;}
        public DateTime BoughtOn { get; set; }
        public bool StillOwned{get;set;}

        /*public List<TreasuryBondTransaction> TreasuryBondTransactions{get;set;}
        public class TreasuryBondTransaction
        {
            public int BondAmount { get; set; }
            public int TrasactionType { get; set; }
            public decimal Interestrate{get;set;}
            public DateTime TradeAt { get; set; }
        }*/
    }

}