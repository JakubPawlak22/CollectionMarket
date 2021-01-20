using AutoMapper;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Message, MessageDTO>()
                .ForMember(x => x.ReceiverName, opt => opt.MapFrom(x => x.Receiver.UserName))
                .ForMember(x => x.SenderName, opt => opt.MapFrom(x => x.Sender.UserName));
            CreateMap<MessageDTO, Message>();
            CreateMap<MessageFilters, MessageFiltersDTO>().ReverseMap();

            CreateMap<UserRegisterDTO, User>()
                .ForMember(x => x.Money, opt => opt.MapFrom(x => 0));
            CreateMap<User, UserProfileDTO>();
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.UserName));

            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>()
                .ForMember(x => x.Attributes,
                    opt => opt.MapFrom(x => x.CategoryAttributes.Select(att => att.Attribute)));
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

            CreateMap<Data.Attribute, AttributeCreateDTO>().ReverseMap();
            CreateMap<Data.Attribute, AttributeDTO>().ReverseMap();
            CreateMap<Data.Attribute, AttributeUpdateDTO>().ReverseMap();

            CreateMap<AttributeValue, AttributeValueDTO>()
                .ForMember(x => x.AttributeName, opt => opt.MapFrom(x => x.Attribute.Name))
                .ForMember(x => x.AttributeValue, opt => opt.MapFrom(x => x.Value))
                .ForMember(x => x.DataType, opt => opt.MapFrom(x => x.Attribute.DataType))
                .ReverseMap();

            CreateMap<ProductType, ProductTypeDTO>()
                .ForMember(x => x.CategoryName,
                    opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Attributes,
                    opt => opt.MapFrom(x => x.AttributeValues))
                .ReverseMap();

            CreateMap<SaleOffer, SaleOfferDTO>()
                .ForMember(x => x.SellerUsername, opt => opt.MapFrom(x => x.Seller.UserName));
            CreateMap<SaleOfferCreateDTO, SaleOffer>();
        }
    }
}
