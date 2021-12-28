using System;
using Core.Options;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Tests.Mocks
{
    public static class MockUnitOfWork
    {
        public static UnitOfWork GetMockUnitOfWork()
        {
            IOptions<VideoServingOptions> options = Options.Create<VideoServingOptions>(new VideoServingOptions()
            {
                Directory = "static",
                Route = "/api/static"
            });
            DbContextOptionsBuilder<DatabaseContext> dbOptions =
                new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            StoreService storeService = new StoreService(options);

            DatabaseContext context = new DatabaseContext(dbOptions.Options, storeService);

            return new UnitOfWork(context);
        }
    }
}
