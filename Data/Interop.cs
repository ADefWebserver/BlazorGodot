using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorGodot
{
    public static class Interop
    {      
        internal static ValueTask<object> CreateGodotApp(
            IJSRuntime jsRuntime,
            ElementReference GodotCanvas,
            string GodotApplicationName,
            string GodotArgs)
        {
            return jsRuntime.InvokeAsync<object>(
                "GodotApp.showApp",
                GodotCanvas,
                GodotApplicationName,
                GodotArgs);
        }
    }
}