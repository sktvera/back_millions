
using Million.Domain.Entities;
using Million.Infrastructure.Repositories;
using Million.Application.DTOs;

namespace Million.Application.Services
{
    public class PropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        public PropertyService(IPropertyRepository propertyRepository) => _propertyRepository = propertyRepository;

        public Task<IEnumerable<Property>> GetAllAsync(string? name=null,string? address=null,decimal? minPrice=null,decimal? maxPrice=null)
            => _propertyRepository.GetAllAsync(name, address, minPrice, maxPrice);

        public Task<Property?> GetByIdAsync(string id) => _propertyRepository.GetByIdAsync(id);

        public async Task CreateAsync(CreatePropertyDto dto)
        {
            var p = new Property
            {
                IdOwner = dto.IdOwner,
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                ImageKey = dto.ImageKey,
                ImageUrl = dto.ImageUrl
            };

       

            await _propertyRepository.CreateAsync(p);
        }

        public async Task UpdateAsync(string id, UpdatePropertyDto dto)
        {
            var existing = await _propertyRepository.GetByIdAsync(id)
                           ?? throw new KeyNotFoundException("Propiedad no encontrada");

            if (dto.IdOwner != null) existing.IdOwner = dto.IdOwner;
            if (dto.Name != null) existing.Name = dto.Name;
            if (dto.Address != null) existing.Address = dto.Address;
            if (dto.Price.HasValue) existing.Price = dto.Price.Value;

            if (dto.ImageKey != null) existing.ImageKey = dto.ImageKey;
            if (dto.ImageUrl != null) existing.ImageUrl = dto.ImageUrl;

        

            await _propertyRepository.UpdateAsync(id, existing);
        }

        public Task DeleteAsync(string id) => _propertyRepository.DeleteAsync(id);
    }
}
