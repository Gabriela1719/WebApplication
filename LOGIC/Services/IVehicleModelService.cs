using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOGIC.Services
{
    public interface IVehicleModelService
    {
        Task<VehicleModel[]> GetIncompleteItemsAsync();
    }
}
