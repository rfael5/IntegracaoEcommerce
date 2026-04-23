using System.Globalization;
using System.Text.Json;
using RestSharp;

public class EcommerceAuthService 
{
    public readonly string api_address = "https://celiasouttomayor.commercesuite.com.br/web_api";
    private readonly string consumer_key = "a75943d6601451a79a1d80b8b6eb3ccd32fcf9d3e7fa2c39ad32010180e9a0ac";
    private readonly string consumer_secret = "5706705aba4d2cf32d6024adf9878926799f6ad4e777d5ebc5eafb41850db83a";
    private readonly string storeCode = "7690ae9e20502a84af649f187a54a8203daa140aab2491eda027a79f3d8504b9";

    public int code;
    public string message;
    public string access_token;
    public string refresh_token;
    public DateTime date_expiration_access_token;
    public DateTime date_expiration_refresh_token;
    public DateTime date_activated;
    public string api_host;
    public string store_id ; 

    public async Task<string?> Authorize()
        {
        var client = new RestClient($"{api_address}/auth");
        var request = new RestRequest()
            .AddParameter("consumer_key", consumer_key)
            .AddParameter("consumer_secret", consumer_secret)
            .AddParameter("code", storeCode);
        try
        {
            var response = client.Post(request);
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(response.Content);
            // using var doc = JsonDocument.Parse(response.Content);
            // var content = doc.RootElement;
            access_token = authResponse.access_token;
            refresh_token = authResponse.refresh_token;
            date_expiration_access_token = DateTime.Parse(authResponse.date_expiration_access_token, CultureInfo.InvariantCulture);
            date_expiration_refresh_token = DateTime.Parse(authResponse.date_expiration_refresh_token, CultureInfo.InvariantCulture);

            Console.WriteLine("Token criado");

            return response.Content;
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro ao criar token");
            Console.WriteLine(e);
            throw;
        }
    }

     public async Task<RefreshResponse> Refresh()
    {
        try
        {
            var refresh = new RestClient($"{api_address}/auth?refresh_token={refresh_token}");
            var request = new RestRequest();

            var response = refresh.Get(request);
            var refreshResponse = JsonSerializer.Deserialize<RefreshResponse>(response.Content);
            access_token = refreshResponse.access_token;
            refresh_token = refreshResponse.refresh_token;
            date_expiration_access_token = DateTime.Parse(refreshResponse.date_expiration_access_token, CultureInfo.InvariantCulture);
            date_expiration_refresh_token = DateTime.Parse(refreshResponse.date_expiration_refresh_token, CultureInfo.InvariantCulture);

            Console.WriteLine("Token atualizado");

            return refreshResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine("Erro ao atualizar token");
            Console.WriteLine(e.Message);
            throw;
        }
    }
};