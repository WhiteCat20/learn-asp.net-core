using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    // buat controller untuk satu tabel/model
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo; // untuk dependency injection
        public StockController(ApplicationDBContext context, IStockRepository stockRepo) // constructor
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        // yang masuk atau keluar dari database saja yang async await!

        // get all value controller
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var stocks = await _context.Stocks.ToListAsync();
            var stocks = await _stockRepo.GetAllASync();
            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        // find by id controller
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // var stock = await _context.Stocks.FindAsync(id);
            // dengan repo dari stock repository
            var stock = await _stockRepo.GetByIdASync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        // post controller
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            // await _context.Stocks.AddAsync(stockModel);
            // await _context.SaveChangesAsync();
            // dengan repo dari StockRepository
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // update controller
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            //searching algorithm
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            // stockModel.Symbol = updateDto.Symbol;
            // stockModel.CompanyName = updateDto.CompanyName;
            // stockModel.Purchase = updateDto.Purchase;
            // stockModel.LastDiv = updateDto.LastDiv;
            // stockModel.Industry = updateDto.Industry;
            // stockModel.MarketCap = updateDto.MarketCap;

            // await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        // delete controller
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            // _context.Stocks.Remove(stockModel);
            // await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}