using System;
using System.IO;
using System.Threading.Tasks;
using Core.Options;
using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infraestructure.Services
{
    public class StoreService : IStoreService
    {
        private readonly VideoServingOptions _videoServingOptions;

        public StoreService(IOptions<VideoServingOptions> videoServingOptions)
        {
            _videoServingOptions = videoServingOptions.Value;
        }

        public async Task SaveVideo(string filename, IFormFile video)
        {
            DirectoryInfo root = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
            string pathStaticDirectory = Path.Combine(root.FullName, _videoServingOptions.Directory);

            string filePath = $"{pathStaticDirectory}/{filename}";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (FileStream stream = File.Create(filePath))
            {
                await video.CopyToAsync(stream);
            }
        }

        public void DeleteDirectory(string directory)
        {
            DirectoryInfo root = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
            string directoryPath = Path.Combine(
                root.FullName,
                _videoServingOptions.Directory,
                directory
            );
            Directory.Delete(directoryPath, true);
        }
    }
}
