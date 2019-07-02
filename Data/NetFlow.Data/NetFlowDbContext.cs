namespace NetFlow.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data.Models;

    public class NetFlowDbContext : IdentityDbContext<User>
    {
        public NetFlowDbContext(DbContextOptions<NetFlowDbContext> options)
            : base(options)
        {
        }
    }
}
