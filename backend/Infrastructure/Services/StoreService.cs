using System;
using System.IO;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
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
            string filePath = CreateDirectory(filename);

            using FileStream stream = File.Create(filePath);
            await video.CopyToAsync(stream);
        }

        public void DeleteDirectory(string directory)
        {
            DirectoryInfo root = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
            string directoryPath = Path.Combine(
                root.FullName,
                _videoServingOptions.Directory,
                directory
            );

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public void CleanDirectory(string directory)
        {
            DirectoryInfo root = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
            string directoryPath = Path.Combine(
                root.FullName,
                _videoServingOptions.Directory,
                directory
            );

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void CreateUserDirectory(Guid userId)
        {
            CreateDirectory(userId.ToString(), true);
        }

        private string CreateDirectory(string directoryPath, bool isDirectory = false)
        {
            DirectoryInfo root = GetRootDirectory();
            string pathStaticDirectory = Path.Combine(root.FullName, _videoServingOptions.Directory);

            string directoryAbsolutePath = $"{pathStaticDirectory}/{directoryPath}";
            if (isDirectory)
            {
                Directory.CreateDirectory(directoryAbsolutePath);
            } else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(directoryAbsolutePath));
            }
            

            return directoryAbsolutePath;
        }

        private DirectoryInfo GetRootDirectory()
        {
            return new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
        }
    }
}
