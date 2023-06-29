using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        // why not declare _context in GenericRepository as protected?
        private readonly HotelListingDbContext _context;

        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetDetailsAsync(int? id)
        {
            return await _context.Countries.Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
