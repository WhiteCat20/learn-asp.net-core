using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol Maximum length is 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "Company Name Maximum length is 20 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000, ErrorMessage = "Purchase must be between 1 and 1000000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(1, 1000000, ErrorMessage = "LastDiv must be between 1 and 1000000")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry Maximum length is 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 500000000, ErrorMessage = "MarketCap must be between 1 and 500000000")]
        public long MarketCap { get; set; }
    }
}