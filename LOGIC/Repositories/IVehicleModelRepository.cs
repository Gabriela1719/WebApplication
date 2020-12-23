using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOGIC
{
    public interface IVehicleModelRepository
    {
        IEnumerable<VehicleModel> GetVehicleModles();
        Task<VehicleModel> GetVehicleModelByID(int ID);
        Task<VehicleModel> InsertVehicleModel(VehicleModel vehicleModel);
        Task<VehicleModel> DeleteVehicleModel(int ID);
        Task<VehicleModel> UpdateVehicleModel(VehicleModel vehicleModel);
        Task<VehicleModel> GetVehicleModelByID(int? id);
        void Save();
    }
}
