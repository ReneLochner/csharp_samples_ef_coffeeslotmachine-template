using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeSlotMachine.Persistence
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CoinRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Coin> GetAllContainedCoins()
        {
            return _dbContext.Coins.OrderByDescending(coin => coin.CoinValue).ToArray();
        }

        public string GetAllContainedCoinsAsString()
        {
            return GetAllContainedCoins().ToString();
        }
    }
}
