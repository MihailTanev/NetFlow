using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetFlow.Data
{
    public class NetFlowDbContext : IdentityDbContext
    {
        public NetFlowDbContext(DbContextOptions<NetFlowDbContext> options)
            : base(options)
        {
        }
    }
}
