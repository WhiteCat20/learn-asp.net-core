using api.Dtos.Comment;

namespace api.Dtos.Stock
{
 public class StockDto
 {
  public int Id { get; set; }
  public string Symbol { get; set; } = string.Empty;
  public string CompanyName { get; set; } = string.Empty;
  public decimal Purchase { get; set; }
  public decimal LastDiv { get; set; }
  public string Industry { get; set; } = string.Empty;
  public long MarketCap { get; set; }
  // comments (menambahkan relation one to many)
  public List<CommentDto> Comments { get; set; }
 }
}