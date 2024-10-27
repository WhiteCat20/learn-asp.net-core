using api.Data;
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

  [HttpGet]
  public IActionResult GetAll()
  {
   var stocks = _context.Stocks.ToList();

   return Ok(stocks);
  }

  [HttpGet("{id}")]
  public IActionResult GetById([FromRoute] int id)
  {
   var stock = _context.Stocks.Find(id);
   if (stock == null)
   {
    return NotFound();
   }
   return Ok(stock);
  }


 }
}