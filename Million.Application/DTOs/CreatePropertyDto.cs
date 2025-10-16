// Million.Application/DTOs/CreatePropertyDto.cs
using System.ComponentModel.DataAnnotations;

namespace Million.Application.DTOs
{
    public class CreatePropertyDto
    {
        [Required] public string IdOwner { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Address { get; set; } = string.Empty;
        [Required] public decimal Price { get; set; }

        // Si vienes desde S3 presign/upload:
        public string? ImageKey { get; set; }
        public string? ImageUrl { get; set; }

    }
}
