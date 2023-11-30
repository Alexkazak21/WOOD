using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WOODCUT.Data;
using WOODCUT.Data.Entities;
//using WOODCUT.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WOODCUT.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly WoodDBContext _db;

        public CustomersController(WoodDBContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<Customer[]> GetCustomersAsync()
        {
            var data = await _db.Customers.Include(x => x.Orders).ToArrayAsync();

            foreach (var item in data) 
            {
                foreach (var order in item.Orders) 
                {
                    order.Customer = null;
                }
            }

            return data;
        }
       
    }
}
