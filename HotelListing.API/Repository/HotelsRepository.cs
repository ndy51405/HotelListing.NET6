using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        // why not declare _context in GenericRepository as protected?
        private readonly HotelListingDbContext _context;

        public HotelsRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        async Task<Hotel> IHotelsRepository.GetDetailsAsync(int? id)
        {
            return await _context.Hotels.Include(q => q.Country)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
