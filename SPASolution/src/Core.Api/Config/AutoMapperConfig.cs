using AutoMapper;
using Model;
using Model.DTOs;
using Service.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Api.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            CreateMap<Client, ClientDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<DataCollection<Client>, DataCollection<ClientDTO>>();
            CreateMap<DataCollection<Product>, DataCollection<ProductDTO>>();
            CreateMap<DataCollection<Order>, DataCollection<OrderDTO>>();
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderDetailCreateDTO, OrderDetail>();
        }
    }
}
