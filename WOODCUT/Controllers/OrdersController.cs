using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WOODCUT.Data;
using WOODCUT.Data.Entities;

namespace WOODCUT.Controllers;

[Route("/[controller]/[action]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly WoodDBContext _db;

    public OrdersController(WoodDBContext db)
    {
        _db = db;
    }

    [HttpPost("{CustomerId}")]
    public async Task<Order[]> GetOrderssAsync(int CustomerId)
    {
        var data = await _db.Orders.Where(x => x.Customer.Id == CustomerId).ToArrayAsync(); //.Include(x => x.Orders).ToArrayAsync();

        return data;
    }
}
