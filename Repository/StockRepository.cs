using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
	public class StockRepository : IStockRepository
	{
		//dependency injection
		private readonly ApplicationDBContext _context;
		public StockRepository(ApplicationDBContext context)
		{
			_context = context;
		}

		public async Task<Stock> CreateAsync(Stock stockModel)
		{
			await _context.AddAsync(stockModel);
			await _context.SaveChangesAsync();
			return stockModel;
		}

		public async Task<Stock?> DeleteAsync(int id)
		{
			var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
			if (stockModel == null)
			{
				return null;
			}
			_context.Remove(stockModel);
			await _context.SaveChangesAsync();
			return stockModel;
		}

		public async Task<List<Stock>> GetAllASync()
		{
			return await _context.Stocks.ToListAsync();
		}

		public async Task<Stock?> GetByIdASync(int id)
		{
			return await _context.Stocks.FindAsync(id);
		}

		public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
		{
			var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
			if (existingStock == null)
			{
				return null;
			}
			existingStock.Symbol = stockDto.Symbol;
			existingStock.CompanyName = stockDto.CompanyName;
			existingStock.Purchase = stockDto.Purchase;
			existingStock.LastDiv = stockDto.LastDiv;
			existingStock.Industry = stockDto.Industry;
			existingStock.MarketCap = stockDto.MarketCap;

			await _context.SaveChangesAsync();

			return existingStock;

		}
	}

}