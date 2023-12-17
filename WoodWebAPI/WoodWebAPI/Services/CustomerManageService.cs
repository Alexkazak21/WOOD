using Microsoft.EntityFrameworkCore;
using WoodWebAPI.Data;
using WoodWebAPI.Data.Models;
using WoodWebAPI.Data.Entities;
using WoodWebAPI.Data.Models.Customer;

namespace WoodWebAPI.Services
{
    public class CustomerManageService : ICustomerManage
    {
        private readonly WoodDBContext _db;

        public CustomerManageService(WoodDBContext dbcontext)
        {
            _db = dbcontext;
        }
        public async Task<ExecResultModel> CreateAsync(CreateCustomerDTO model)
        {
            await _db.Customers.AddAsync(
                new Customer
                {
                    TelegramID = model.TelegtamId,
                    Name = model.Name,
                }
                );

            if(_db.SaveChangesAsync().Result > 0) 
            {
                return new ExecResultModel() 
                {
                    Success = true,
                    Message = "Customer added successfully"
                }; 
            }
            else
            {
                return new ExecResultModel()
                {
                    Success = false,
                    Message = "Customer not added, check the input"
                };
            }
        }

        public Task<ExecResultModel> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ExecResultModel> DeleteAsync(DeleteCustomerDTO model)
        {
            var data = await _db.Customers.Where(x => x.TelegramID == model.TelegramId).FirstAsync();

            if( data != null ) 
            {
               _db.Customers.Remove(data);
                await _db.SaveChangesAsync();
                return new ExecResultModel()
                {
                    Success = true,
                    Message = $"Customer with TelegramId = {data.TelegramID} was remowed!",
                };
            }
            else
            {
                return new ExecResultModel()
                {
                    Success = false,
                    Message = $"Customer with TelegramId = {data.TelegramID} doesn`t exist!",
                };
            }

        }

        public Task<ExecResultModel> DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetCustomerModel[]?> GetAsync()
        {
            var customersArray = new List<GetCustomerModel>();

            var data = await _db.Customers.CountAsync();

            if (data == 0)
            {
                return null;
            }

            var result = await _db.Customers.Include(x => x.Orders).ToArrayAsync();

            foreach (var item in result) 
            {
                customersArray.Add(new GetCustomerModel()
                {
                    TelegramId = item.TelegramID,
                    Name = item.Name,
                    Orders = item.Orders
                });
            }

            return customersArray.ToArray();
        }

        public Task<ExecResultModel> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
