using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public interface IVehicleMakeService
    {
        Task<VehicleMake[]> GetIncompleteItemsAsync();
    }
}
