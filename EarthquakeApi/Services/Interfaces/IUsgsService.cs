using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EarthquakeApi.Services.Interfaces
{
    public interface IUsgsService : IDisposable
    {
        Task<IEnumerable<string>> ReadCsv();
    }
}
