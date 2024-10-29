using api.Data;
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


		public Task<List<Stock>> GetAllASync()
		{
			return _context.Stocks.ToListAsync();
		}
	}


}