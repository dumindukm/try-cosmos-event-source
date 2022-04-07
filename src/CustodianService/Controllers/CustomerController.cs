using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustodianService.Infrastructure;
using CustodianService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustodianService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustodianContext conext;

        public CustomerController()
        {
            
            this.conext = new CustodianContext();
            this.conext.Database.EnsureCreated();
        }

        [HttpGet("{cdsIdentifier}")]
        public IActionResult GetCustomer(string cdsIdentifier)
        {
            var customers =conext.Customers.ToList();
            return Ok(customers);
        }

        [HttpPost("")]
        public IActionResult CreateCustomer(Customer model)
        {
            conext.Customers.Add(model);
            conext.SaveChanges();
            Console.WriteLine($"model id {model.Id}");
            return Created(nameof(GetCustomer),model.Id);
        }
        
        
    }
}