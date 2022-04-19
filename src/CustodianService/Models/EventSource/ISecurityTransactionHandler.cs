using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace CustodianService.Models.EventSource
{
    public interface ISecurityTransactionHandler
    {
        void Handle(CustomerPortfolio portFolio, string data);
    }

    public class StockBoughtHandler : ISecurityTransactionHandler
    {
        public void Handle(CustomerPortfolio portFolio, string data)
        {
            // deserialize event data
            StockBoughEvent @event = JsonSerializer.Deserialize<StockBoughEvent>(data);
            // do update portFoliotypeof(StockBoughEvent
            portFolio.StockHoldings.Add(new StockHolding(){ Symbol = @event.Symbol, StockValue= @event.StockTotal});
            portFolio.TotalHodings +=@event.StockTotal;
        }
    }

    public class StockBoughEvent
    {
        public string Symbol { get; set; }
        public int TradeType { get; set; }// buy or sell
        public int NoOfShares { get; set; }
        public decimal SharePrice{get;set;}
        public decimal StockTotal => NoOfShares * SharePrice;
        public DateTime TradeAt { get; set; }
    }
}