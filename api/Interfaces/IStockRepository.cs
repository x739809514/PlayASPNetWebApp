using api.Dtos.Stock;
using api.Helpers;
using api.Model;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllASync(QueryObject query);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}