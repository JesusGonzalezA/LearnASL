using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CustomEntities;

namespace Core.Interfaces
{
    public interface IAIService
    {
        Task<List<Prediction> > SendRequest(string filename, string token);
    }
}
