// Million.Application/DTOs/UpdatePropertyDto.cs
namespace Million.Application.DTOs
{
    public class UpdatePropertyDto
    {
        public string? IdOwner { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }

        public string? ImageKey { get; set; }
        public string? ImageUrl { get; set; }

     
    }
}
