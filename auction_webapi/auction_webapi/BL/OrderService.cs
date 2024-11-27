using auction_webapi.DAL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using static System.Net.Mail.MailAddress;

namespace auction_webapi.BL
{
    public class OrderService:IOrderService
    {
        private IOrderDAL _OrderDAL;
        public OrderService(IOrderDAL OrderDAL)
        {
                this._OrderDAL= OrderDAL ?? throw new ArgumentNullException(nameof(OrderDAL));       
        }
        public Task<List<Order>> GetAsync()
        {
            return _OrderDAL.GetAsync();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            return _OrderDAL.GetByIdAsync(id);
        }

        public Task<List<Order>> GetByUserAsync(string UserName)
        {
            return _OrderDAL.GetByUserAsync(UserName);
        }

        public Task<Order> PostAsync(Order o)
        {
            o.Date = DateTime.Now;
            return _OrderDAL.PostAsync(o);
        }
    }
}
