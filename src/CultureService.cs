using Microsoft.JSInterop;
using System.Globalization;

namespace MultilingualApp
{
    public class CultureService
    {
        private IJSRuntime _jsRuntime;
        private HttpClient _httpClient;

        public CultureService(IJSRuntime js, HttpClient http)
        {
            _jsRuntime = js;
            _httpClient = http;
        }

        public async Task InitAsync()
        {
            var cultureName = await _jsRuntime.InvokeAsync<string>("window.localStorage.getItem", "current-culture");

            if (cultureName == null)
            {
                //cultureName = await _httpClient.GetStringAsync("http://getcurrentculture...");
                cultureName = "en-US";
            }
            await SetCurrentCultureAsync(cultureName);
        }

        public async Task SetCurrentCultureAsync(string name)
        {
            var culture = new CultureInfo(name);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            await _jsRuntime.InvokeVoidAsync("window.localStorage.setItem", "current-culture", name);
        }
    }
}
