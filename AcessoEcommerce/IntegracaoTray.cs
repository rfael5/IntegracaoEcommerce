using System.Text.Json;
//using RestSharp;

// public class IntegracaoTray
// {
//         private readonly string api_address = "https://celiasouttomayor.commercesuite.com.br/web_api";
//         private readonly string consumer_key = "a75943d6601451a79a1d80b8b6eb3ccd32fcf9d3e7fa2c39ad32010180e9a0ac";
//         private readonly string consumer_secret = "5706705aba4d2cf32d6024adf9878926799f6ad4e777d5ebc5eafb41850db83a";
//         private readonly string code = "7690ae9e20502a84af649f187a54a8203daa140aab2491eda027a79f3d8504b9";
//         private string access_token;
//         private string refresh_token;

//         async Task<string?> Authorize()
//         {
//             var client = new RestClient($"{api_address}/auth");
//             var request = new RestRequest()
//                 .AddParameter("consumer_key", consumer_key)
//                 .AddParameter("consumer_secret", consumer_secret)
//                 .AddParameter("code", code);

//             var response = client.Post(request);

//             Console.WriteLine(response.Content);
//             return response.Content;
//         };
// }