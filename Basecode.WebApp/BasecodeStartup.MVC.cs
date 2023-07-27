using Microsoft.Extensions.DependencyInjection;

namespace Basecode.WebApp
{
    public partial class BasecodeStartup
    {
        private void ConfigureMVC(IServiceCollection services)
        {
            services.AddMvc();
        }
    }
}
