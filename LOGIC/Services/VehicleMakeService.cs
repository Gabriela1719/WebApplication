using DAL.DataContext;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class VehicleMakeServices : IVehicleMakeService
    {
        private readonly DatabaseContext _context;
        public VehicleMakeServices(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<VehicleMake[]> GetIncompleteItemsAsync()
        {
            return await _context.VehicleMakes
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
        }
    }
}
