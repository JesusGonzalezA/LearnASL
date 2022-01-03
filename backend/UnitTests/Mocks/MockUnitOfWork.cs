using System;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Tests.Mocks
{
    public class MockUnitOfWork
    {
        public static UnitOfWork GetMockUnitOfWork()
        {
            DbContextOptionsBuilder<DatabaseContext> fakeDbOptions =
                new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            DatabaseContext fakeContext = new DatabaseContext(fakeDbOptions.Options);

            return new UnitOfWork(fakeContext);
        }
    }
}
