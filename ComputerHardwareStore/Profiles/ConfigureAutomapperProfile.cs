using AutoMapper;
using ComputerHardwareStore.Entities;
using ComputerHardwareStore.ViewModels;

namespace ComputerHardwareStore.Profiles
{
    public class ConfigureAutomapperProfile : Profile
    {
        public ConfigureAutomapperProfile()
        {
            CreateMap<ShoppingCartViewModel, ShoppingCart>()
                .ReverseMap();
            CreateMap<ShoppingCartItemViewModel, ShoppingCartItem>()
                .ReverseMap();
        }
    }
}