using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DAL.DataContext;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LOGIC
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
         private DatabaseContext context;
        public VehicleMakeRepository(DatabaseContext context)
        {
            this.context=context;
        }
        public IEnumerable GetVehicleMakes()
        {
            return context.VehicleMakes.ToList();
        }
        public VehicleMake GetVehicleMakeByID(int ID)
        {
            return context.VehicleMakes.Find(ID);
        }
        public void InsertVehicleMake(VehicleMake vehicleMake)
        {
            context.VehicleMakes.Add(vehicleMake);
        }
        public void DeleteVehicleMake(int ID)
        {
            VehicleMake vehicleMake = context.VehicleMakes.Find(ID);
            context.VehicleMakes.Remove(vehicleMake);
        }
         public void UpdateVehicleMake(VehicleMake vehicleMake)
        {
            context.Entry(vehicleMake).State=EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}