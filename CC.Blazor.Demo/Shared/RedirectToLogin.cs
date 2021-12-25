using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CC.Blazor.Demo.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class RedirectToLogin : ComponentBase
    {
        [Inject]
        [NotNull]
        private NavigationManager? Navigation { get; set; }

#if DEBUG
        protected override void OnAfterRender(bool firstRender)
        {
            Navigation.NavigateTo($"/Account/Login", true);
        }
#else
        protected override void OnInitialized()
        {
            Navigation.NavigateTo($"/Account/Login", true);
        }
#endif
    }
}
