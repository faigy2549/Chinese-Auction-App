using auction_webapi.Models;
using Microsoft.OpenApi.Any;
using System;
using System.Diagnostics;
using System.Resources;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace auction_webapi.DAL
{
    public class RaffleDAL : IRaffleDAL
    {
        private AuctionContext _auctionContext;
        public RaffleDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<Winner> Raffle(Present p)
        {
            List<OrderItem> oi = await _auctionContext.OrderItem
                .Include(i => i.Order)
                .Where(o=>o.PresentId==p.PresentId&&o.Status)
                .ToListAsync();

            Random random = new Random();
            int winnerIndex = random.Next(oi.Count);
            var winnerOrderItem = oi[winnerIndex];
            var winnerUser = winnerOrderItem.UserId;

            Raffle r = new Raffle { RaffleDate = DateTime.Today };
            await _auctionContext.Raffles.AddAsync(r);
            await _auctionContext.SaveChangesAsync();

            var user = _auctionContext.Users.FirstOrDefault(u => u.UserId == winnerUser);
            var present = _auctionContext.Presents.FirstOrDefault(y => y.PresentId == p.PresentId);
            present.Winner = user.Email;
            await _auctionContext.SaveChangesAsync();

            Winner w = new Winner
            {
                PresentId = p.PresentId,
                UserId = winnerUser,
                RaffleId = r.RaffleId,
                Email=user.Email
            };

            await _auctionContext.Winners.AddAsync(w);
            await _auctionContext.SaveChangesAsync();
            return w;

    }
        public async Task<string> Results() {
            string filePath = "C://Users//faigy//OneDrive//Documents//results.txt";
            List<Winner> winners = await _auctionContext.Winners.ToListAsync();
            List<Present> presents = await _auctionContext.Presents.ToListAsync();
            List<User> users = await _auctionContext.Users.ToListAsync();

            var queryResult = winners.Select(w =>
            {
                var present = presents.FirstOrDefault(p => p.PresentId == w.PresentId);
                var user = users.FirstOrDefault(u => u.UserId == w.UserId);

                return $"Present: {present?.Name}, Winner: {user?.FirstName} {user?.LastName}";
            });

            string text = string.Join(Environment.NewLine, queryResult);

            await File.WriteAllTextAsync(filePath, text);
            Process.Start("notepad.exe", filePath);
            return text;
        }


        public async Task<string> Income()
        {
            string filePath = "C://Users//faigy//OneDrive//Documents//income.txt";
            List<Order> orders = await _auctionContext.Orders.ToListAsync();
            double sum = 0;
            for(int i = 0; i < orders.Count; i++)
            {
                sum+=orders[i].Sum;
            }

            string text = string.Join(Environment.NewLine,$"Total income for the auction is:"+ sum.ToString());

            await File.WriteAllTextAsync(filePath, text);
            Process.Start("notepad.exe", filePath);
            return text;
        }
    }
}
