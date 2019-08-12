namespace NetFlow.Web.Extensions.ServiceCollections
{
    using Microsoft.Extensions.DependencyInjection;
    using NetFlow.Services;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            Assembly.GetAssembly(typeof(IServices))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces()
                                          .Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Impementation = t
                })
                .ToList()
                .ForEach(s => services.AddTransient(s.Interface, s.Impementation));

            return services;
        }
    }
}
