using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.AuctionsRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.BGService
{
    public class AuctionCompleteTask : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AuctionCompleteTask(IServiceProvider _serviceProvider)
        {
            this._serviceProvider = _serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    var _service = scope.ServiceProvider.GetRequiredService<IAuctionsRepository>();

                    var ActiveAuctions = await _service.GetAllActiveAuctionsToUpdate();

                    if(ActiveAuctions != null)
                    {
                        foreach (var Auction in ActiveAuctions)
                        {
                            if (Auction.AcutionStartedTime.HasValue)
                            {
                                var AuctionEndTime = Auction.AcutionStartedTime.Value.AddHours(Auction.Product.AuctionDuration);

                                if (DateTime.Now >= AuctionEndTime)
                                {
                                    await HandleEndedAuction(_service, Auction);
                                }
                            }
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task HandleEndedAuction(IAuctionsRepository _service,Auctions Auction)
        {
            Auction.AuctionInProgress = false;
            if(Auction.CurrentBidValue > Auction.Product.ReservedPrice)
            {
                Auction.Sold = true;
                Auction.Product.ProductSold = true;
            }
            await _service.UpdateAuction(Auction);
        }
    }
}
