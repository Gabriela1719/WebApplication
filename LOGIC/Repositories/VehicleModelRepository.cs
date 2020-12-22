using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable GetVehicleModles()
        {
            return context.VehicleModels.ToList();
        }
        public VehicleModel GetVehicleModelByID(int ID)
        {
            return context.VehicleModels.Find(ID);
        }
        public void InsertVehicleModel(VehicleModel vehicleModel)
        {
            context.VehicleModels.Add(vehicleModel);
        }
        public void DeleteVehicleModel(int ID)
        {
            VehicleModel vehicleModel = context.VehicleModels.Find(ID);
            context.VehicleModels.Remove(vehicleModel);
        }
         public void UpdateVehicleModel(VehicleModel vehicleModel)
        {
            context.Entry(vehicleModel).State=EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}