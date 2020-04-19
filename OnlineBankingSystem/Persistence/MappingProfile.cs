using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineBankingSystem.Core;
using OnlineBankingSystem.Core.Models;
using OnlineBankingSystem.Core.ViewModel;

namespace OnlineBankingSystem.Persistence
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel,ApplicationUser>()
                .ReverseMap();
            

            CreateMap<RegisterViewModel, RegisterVM>().
                ReverseMap();

            CreateMap<AccountTypeEditViewModel, AccountType>()
                .ForMember(x => x.AccountTypeName, opt => opt.MapFrom(x => x.name))
                .ReverseMap();
        }
    }
}