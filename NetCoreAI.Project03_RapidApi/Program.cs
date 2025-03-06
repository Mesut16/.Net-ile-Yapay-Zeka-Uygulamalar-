using NetCoreAI.Project03_RapidApi.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;


var client = new HttpClient();
List<ApiSeriesViewModels> apiSeriesViewModels = new List<ApiSeriesViewModels>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
    Headers =
    {
        { "x-rapidapi-key", "ae5ea42d42msha213dc42aa20e95p1ea50djsn8ee371fe98c4" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    apiSeriesViewModels = JsonConvert.DeserializeObject<List<ApiSeriesViewModels>>(body);
    foreach (var series in apiSeriesViewModels)
    {
        Console.WriteLine(series.rank + " " + series.title + "-Film Puanı: " + series.rating + "-Yapım Yılı " + series.year );
    }
}

Console.ReadLine();