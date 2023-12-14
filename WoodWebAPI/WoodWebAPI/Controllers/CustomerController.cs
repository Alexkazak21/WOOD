using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WoodWebAPI.Auth;
using WoodWebAPI.Data.Models.Customer;
using WoodWebAPI.Services;

namespace WoodWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManage _entityService;

        public CustomerController(ICustomerManage entity)
        {
            _entityService = entity;
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _entityService.GetAsync();

            if (result == null)
            {
                return Ok("Customers list is empty"); //BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomers(CreateCustomerDTO model)
        {
            var result = await _entityService.CreateAsync(model);

            if (!result.Success)
            {
                return Ok(result.Message); //BadRequest(result.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> DeleteCustomers(DeleteCustomerDTO model)
        {
            var result = await _entityService.DeleteAsync(model);

            if (!result.Success)
            {
                return Ok(result.Message);
            }

            return Ok(result);
        }
    }
}
