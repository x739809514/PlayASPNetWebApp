using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x=>x.Id==id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllASync(QueryObject query)
        {
            //var list = await _context.Stock.Include(x=>x.comments).ToListAsync(); // using IEnumerable

            // using IQueryable
            var stocks =  _context.Stock.Include(x=>x.comments).AsQueryable();
            if (string.IsNullOrWhiteSpace(query.CompanyName) == false)
            {
                stocks = stocks.Where(x=>x.Company.Contains(query.CompanyName));
            }
            if (string.IsNullOrWhiteSpace(query.Symbol)==false)
            {
                stocks = stocks.Where(x=>x.Symbol.Contains(query.Symbol));
            }

            if (string.IsNullOrWhiteSpace(query.SortBy) == false)
            {
                if (query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDesending ? stocks.OrderByDescending(s=>s.Symbol): stocks.OrderBy(s=>s.Symbol);
                }
            }
        
            return await stocks.ToListAsync(); // apply the operation here
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(x=>x.comments).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stock.AnyAsync(s=>s.Id==id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stock.FirstOrDefaultAsync(x=>x.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.Company = stockDto.Company;
            existingStock.Industry =stockDto.Industry;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.MarketCap =stockDto.MarketCap;
            existingStock.Purchase = stockDto.Purchase;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}