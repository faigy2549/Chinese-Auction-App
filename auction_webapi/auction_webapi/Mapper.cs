using auction_webapi.DAL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;

namespace auction_webapi
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Present, PresentDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Donar, DonarDTO>().ReverseMap();
        }
    }
}
