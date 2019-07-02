using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(NetFlow.Web.Areas.Identity.IdentityHostingStartup))]
namespace NetFlow.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}