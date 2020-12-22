using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;


namespace LOGIC
{
    public interface IVehicleModelRepository
    {
        IEnumerable GetVehicleModles();        
        VehicleModel GetVehicleModelByID(int ID);        
        void InsertVehicleModel(VehicleModel vehicleModel);        
        void DeleteVehicleModel(int ID);        
        void UpdateVehicleModel(VehicleModel vehicleModel);        
        void Save(); 
    }
}
