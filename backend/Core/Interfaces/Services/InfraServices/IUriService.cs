using Core.Contracts.Incoming;
using Core.CustomEntities;
using Core.Entities.Tests;

namespace Core.Interfaces
{
    public interface IUriService
    {
        string GetVideoUri(string? filename);
        Metadata<TestWithQuestions> UpdateMetadataTests(Metadata<TestWithQuestions> metadata, TestQueryFilterDto filters, string actionUrl);
    }
}
