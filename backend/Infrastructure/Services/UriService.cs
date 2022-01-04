using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Core.Contracts.Incoming;
using Core.CustomEntities;
using Core.Entities.Tests;
using Core.QueryFilters;
using Infrastructure.Interfaces;

namespace Infrastructure.Services
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

        public Metadata<TestWithQuestions> UpdateMetadataTests
        (
            Metadata<TestWithQuestions> metadata,
            TestQueryFilterDto filters,
            string actionUrl
        )
        {
            UriBuilder builder = new UriBuilder($"{_baseUri}{actionUrl}");
            
            if (metadata.HasPreviousPage)
            {
                TestQueryFilterDto previousPageFilter = new TestQueryFilterDto(filters);
                previousPageFilter.PageNumber--;

                builder.Query = CreateQueryFromTestQueryFilter(previousPageFilter);
                metadata.PreviousPageUrl = builder.ToString();
            }

            if (metadata.HasNextPage)
            {
                TestQueryFilterDto nextPageFilter = new TestQueryFilterDto(filters);
                nextPageFilter.PageNumber++;

                builder.Query = CreateQueryFromTestQueryFilter(nextPageFilter);
                metadata.NextPageUrl = builder.ToString();
            }
            
            return metadata;
        }

        private string CreateQueryFromTestQueryFilter(TestQueryFilterDto filter)
        {
            NameValueCollection queryString = new NameValueCollection();
            filter.GetType().GetProperties()
                .ToList()
                .ForEach(pi => queryString.Add(pi.Name, (pi.GetValue(filter, null) ?? "").ToString()));

            string query = string.Join("&", queryString.AllKeys.Where(key => !string.IsNullOrWhiteSpace(queryString[key])).Select(key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(queryString[key]))));

            return query;
        }
    }
}