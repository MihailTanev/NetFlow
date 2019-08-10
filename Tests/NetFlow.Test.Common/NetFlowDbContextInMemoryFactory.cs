namespace NetFlow.Test.Common
{
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using System;

    public static class StopifyDbContextInMemoryFactory
    {
        public static NetFlowDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<NetFlowDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new NetFlowDbContext(options);
        }
    }
}
