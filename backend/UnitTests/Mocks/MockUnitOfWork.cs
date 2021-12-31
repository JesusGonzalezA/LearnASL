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
            IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<MockUnitOfWork>()
                    .Build();

            string connectionString = config.GetSection("DatabaseConnectionString").Value;

            DbContextOptionsBuilder<DatabaseContext> realDbOptions =
                new DbContextOptionsBuilder<DatabaseContext>()
                        .UseSqlServer(connectionString);
            DatabaseContext realContext = new DatabaseContext(realDbOptions.Options);
            UnitOfWork realUnitOfWork = new UnitOfWork(realContext);

            DbContextOptionsBuilder<DatabaseContext> fakeDbOptions =
                new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            DatabaseContext fakeContext = new DatabaseContext(fakeDbOptions.Options);

            return new UnitOfWork(fakeContext, realUnitOfWork.DatasetRepository);
        }
    }
}
