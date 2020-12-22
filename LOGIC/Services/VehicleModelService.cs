using DAL.DataContext;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly DatabaseContext _context;
        public VehicleModelService(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<VehicleModel[]> GetIncompleteItemsAsync()
        {
            return await _context.VehicleModels
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
        }
    }
}
