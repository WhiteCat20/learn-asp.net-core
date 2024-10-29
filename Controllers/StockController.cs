using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	// buat controller untuk satu tabel/model
	[Route("api/stocks")]
	[ApiController]
	public class StockController : ControllerBase
	{
		private readonly ApplicationDBContext _context;
		public StockController(ApplicationDBContext context)
		{
			_context = context;
		}


		// get all value controller
		[HttpGet]
		public IActionResult GetAll()
		{
			var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());

			return Ok(stocks);
		}

		// find by id controller
		[HttpGet("{id}")]
		public IActionResult GetById([FromRoute] int id)
		{
			var stock = _context.Stocks.Find(id);
			if (stock == null)
			{
				return NotFound();
			}
			return Ok(stock.ToStockDto());
		}

		// post controller
		[HttpPost]
		public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
		{
			var stockModel = stockDto.ToStockFromCreateDTO();
			_context.Stocks.Add(stockModel);
			_context.SaveChanges();
			return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
		}

		// update controller
		[HttpPut]
		[Route("{id}")]
		public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
		{
			//searching algorithm
			var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
			if (stockModel == null)
			{
				return NotFound();
			}
			stockModel.Symbol = updateDto.Symbol;
			stockModel.CompanyName = updateDto.CompanyName;
			stockModel.Purchase = updateDto.Purchase;
			stockModel.LastDiv = updateDto.LastDiv;
			stockModel.Industry = updateDto.Industry;
			stockModel.MarketCap = updateDto.MarketCap;
			_context.SaveChanges();
			return Ok(stockModel.ToStockDto());
		}

		// delete controller
		[HttpDelete]
		[Route("{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
			if (stockModel == null)
			{
				return NotFound();
			}
			_context.Stocks.Remove(stockModel);
			_context.SaveChanges();
			return NoContent();
		}
	}
}