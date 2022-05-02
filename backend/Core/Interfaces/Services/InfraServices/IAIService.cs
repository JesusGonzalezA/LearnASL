using System.Collections.Generic;
using System.Threading.Tasks;
using Core.CustomEntities;
using Core.Enums;

namespace Core.Interfaces
{
    public interface IAIService
    {
        Task<List<Prediction> > SendRequest(Difficulty difficulty, string filename, string token);
    }
}
