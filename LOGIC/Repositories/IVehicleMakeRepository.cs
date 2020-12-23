using DAL.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LOGIC
{
    public interface IVehicleMakeRepository
    {
        IEnumerable<VehicleMake> GetVehicleMakes();
        Task<VehicleMake> GetVehicleMakeByID(int ID);
        Task<VehicleMake> InsertVehicleMake(VehicleMake vehicleMake);
        Task<VehicleMake> DeleteVehicleMake(int ID);
        Task<VehicleMake> UpdateVehicleMake(VehicleMake vehicleMake);
        Task<VehicleMake> GetVehicleMakeByID(int? id);
    }
}
