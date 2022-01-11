using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IStoreService
    {
        Task SaveVideo(string filename, IFormFile video);
        void CreateUserDirectory(Guid userId);
        void CleanDirectory(string directory);
        void DeleteDirectory(string directory);
    }
}
