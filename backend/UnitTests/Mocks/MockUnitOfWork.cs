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
            DbContextOptionsBuilder<DatabaseContext> dbOptions =
                new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            DatabaseContext context = new DatabaseContext(dbOptions.Options);

            return new UnitOfWork(context);
        }
    }
}
