using System;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
