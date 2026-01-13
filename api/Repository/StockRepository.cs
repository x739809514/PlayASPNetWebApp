using api.Data;
using api.Dtos.Stock;
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

        public async Task<List<Stock>> GetAllASync()
        {
            var list = await _context.Stock.ToListAsync();
            return list;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FirstOrDefaultAsync(x=>x.Id==id);
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