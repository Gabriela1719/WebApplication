using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LOGIC
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private DatabaseContext context;
        public VehicleModelRepository(DatabaseContext context)
        {
            this.context=context;
        }
        public IEnumerable<VehicleModel> GetVehicleModles()
        {
            return context.VehicleModels.ToList();
        }
        public async Task<VehicleModel>GetVehicleModelByID(int ID)
        {
            return await context.VehicleModels.FindAsync(ID);
        }
        public async Task<VehicleModel> InsertVehicleModel(VehicleModel vehicleModel)
        {
            var result = await context.VehicleModels.AddAsync(vehicleModel);
            return result.Entity;
        }
        public async Task<VehicleModel> DeleteVehicleModel(int ID)
        {
            var result = await context.VehicleModels.FirstOrDefaultAsync(v => v.ID == ID);
            if (result != null)
            {
               context.VehicleModels.Remove(result);
                await context.SaveChangesAsync();

            }
            return result;
        }
        public async Task<VehicleModel> UpdateVehicleModel(VehicleModel vehicleModel)
        {
            var result = await context.VehicleModels.FirstOrDefaultAsync(v => v.ID == vehicleModel.ID);
            return result;
        }

        public async Task<VehicleModel> GetVehicleModelByID(int? id)
        {
            return await context.VehicleModels.FindAsync(id);
        }
    }
}
