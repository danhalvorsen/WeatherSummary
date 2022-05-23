using System.Collections.Generic;
using System.Threading.Tasks;
using BasicWebAPI.Controllers;

namespace BasicWebAPI.Factory
{
    public interface IQuery
    {
        Task<List<CityDto>> GetAllCities();
    }
}