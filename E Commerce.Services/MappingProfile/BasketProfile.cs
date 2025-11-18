using AutoMapper;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfile
{
    public class BasketProfile: Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDTO,CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDTO,BasketItem>().ReverseMap();

        }
    }
}
