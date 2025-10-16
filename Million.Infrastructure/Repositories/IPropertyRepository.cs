using Million.Domain.Entities;

namespace Million.Infrastructure.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync(string? city = null, string? type = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<Property?> GetByIdAsync(string id);
        Task CreateAsync(Property property);
        Task UpdateAsync(string id, Property property);
        Task DeleteAsync(string id);
    }
}
