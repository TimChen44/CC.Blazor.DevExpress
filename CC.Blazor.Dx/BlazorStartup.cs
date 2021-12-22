using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Blazor.Dx
{
    public static class BlazorStartup
    {
        public static void AddCCPBlazor(this IServiceCollection services)
        {
            services.AddSingleton<CcMeesageBoxService>();
            services.AddSingleton<CcToastService>();
        }
    }
}
