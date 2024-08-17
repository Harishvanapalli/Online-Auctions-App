using AutoMapper;
using Data_Access_Layer.Entities;
using Online_Auction.API.Dto_s;

namespace Online_Auction.API.Helpers
{
    public class MappingConfigure
    {
        public static MapperConfiguration RegisterMaps()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateProductDto, Products>();
                config.CreateMap<CreateAuctionDto, Auctions>();
                config.CreateMap<CreateUserBidDto, Bids>();
                config.CreateMap<Auctions, AuctionsDto>();
                config.CreateMap<Products, ProductsDto>();
                config.CreateMap<Bids, BidsDto>();
            });
            return configuration;
        }
    }
}
