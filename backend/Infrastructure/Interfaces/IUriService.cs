using Core.Contracts.Incoming;
using Core.CustomEntities;
using Core.Entities.Tests;

namespace Infrastructure.Interfaces
{
    public interface IUriService
    {
        string GetVideoUri(string? filename);
        Metadata<TestWithQuestions> UpdateMetadataTests(Metadata<TestWithQuestions> metadata, TestQueryFilterDto filters, string actionUrl);
    }
}
