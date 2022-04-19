using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustodianService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerPortfolioController : ControllerBase
    {
        private readonly CustodianContext conext;
        public CustomerPortfolioController()
        {
            this.conext = new CustodianContext();
            //this.conext.Database.EnsureCreated();
        }

        [HttpPost("StockBought")]
        public IActionResult StockBought(string cdsIdentifier, StockBoughEvent stockBoughEvent)
        {
            var trx = CustomerSecurityTransaction.CreateStockBoughTransaction(cdsIdentifier, stockBoughEvent);
            conext.CustomerSecurityTransactions.Add(trx);
            conext.SaveChanges();
            return Ok(trx.Id);
        }
        
        [HttpGet("BuildCustomerPortfolio")]
        public IActionResult BuildCustomerPortfolio(string cdsIdentifier)
        {
            CustomerPortfolio portfolio = this.conext.CustomerAssets.FirstOrDefault(x=> x.CdsIdentifier == cdsIdentifier);
            if(portfolio != null)
            {
                portfolio.BuildPortFolio(this.conext.CustomerSecurityTransactions.ToList());
                this.conext.Update(portfolio);
            }
            else
            {
                portfolio = new(cdsIdentifier);
                portfolio.BuildPortFolio(this.conext.CustomerSecurityTransactions.ToList());
                this.conext.Add(portfolio);
            }
            this.conext.SaveChanges();

        
            return Ok(portfolio);
        }
        
    }
}