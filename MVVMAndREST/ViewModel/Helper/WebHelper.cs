using System.Net.Http;
using System.Text.Json;
using MVVMAndREST.Model;

namespace MVVMAndREST.ViewModel.Helper;

public class WebHelper
{
    private static string API_KEY = "XGGro9YFALgB5GM6d6EM2THdJAMY5v4C";
    private static string AUTOCOMPETE_PLACEHOLDER = 
        "http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={0}&q={1}";

    private static string CURRENT_CONDITIONS_PLACEHOLDER = 
        "http://dataservice.accuweather.com/currentconditions/v1/{0}?apikey={1}";

    public static async Task<List<City>> GetAutocomplete(string query)
    {
        List<City> cities = new();
        string url = string.Format(AUTOCOMPETE_PLACEHOLDER, API_KEY, query);
        using HttpClient client = new();
        var result = await client.GetAsync(url);
        string json = await result.Content.ReadAsStringAsync();
        cities = JsonSerializer.Deserialize<List<City>>(json);
        return cities;
    }

    public static async Task<CurrentConditions> GetCurrentConditions(string id)
    {
        List<CurrentConditions> conditions = new();
        string url = string.Format(CURRENT_CONDITIONS_PLACEHOLDER, id, API_KEY);
        using HttpClient client = new();
        var result = await client.GetAsync(url);
        string json = await result.Content.ReadAsStringAsync();
        conditions = JsonSerializer.Deserialize<List<CurrentConditions>>(json);
        
        return conditions[0];
    }
}