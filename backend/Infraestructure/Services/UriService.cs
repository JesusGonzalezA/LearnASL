using Infraestructure.Interfaces;

namespace Infraestructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        private readonly string _staticBaseUri;

        public UriService
        (
            string baseUri,
            string staticBaseUri
        )
        {
            _baseUri = baseUri;
            _staticBaseUri = staticBaseUri;
        }

        public string GetVideoUri(string? filename)
        {
            return filename == null ? null : $"{_baseUri}{_staticBaseUri}/{filename}";
        }
    }
}