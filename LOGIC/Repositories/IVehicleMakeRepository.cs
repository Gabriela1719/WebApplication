using DAL.Models;
using System.Collections;


namespace LOGIC
{
    public interface IVehicleMakeRepository
    {
       IEnumerable GetVehicleMakes();        
        VehicleMake GetVehicleMakeByID(int ID);        
        void InsertVehicleMake(VehicleMake vehicleMake);        
        void DeleteVehicleMake(int ID);        
        void UpdateVehicleMake(VehicleMake vehicleMake);        
        void Save(); 
    }
}