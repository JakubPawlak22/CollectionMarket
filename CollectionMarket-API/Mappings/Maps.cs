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
                .ForMember(x => x.ReceiverName, opt => opt.MapFrom(x =>
                      string.Format("{0} {1}", x.Receiver.FirstName, x.Receiver.LastName)))
                .ForMember(x => x.SenderName, opt => opt.MapFrom(x =>
                      string.Format("{0} {1}", x.Sender.FirstName, x.Sender.LastName)));
            CreateMap<MessageDTO, Message>();
            CreateMap<Message, MessageCreateDTO>().ReverseMap();
            CreateMap<MessageFilters, MessageFiltersDTO>().ReverseMap();
            CreateMap<UserRegisterDTO, User>()
                .ForMember(x => x.Money, opt => opt.MapFrom(x => 0));
        }
    }
}
