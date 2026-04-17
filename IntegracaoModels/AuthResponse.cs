public record AuthResponse 
{
    public string code { get; init; }
    public string message { get; init; }
    public string access_token { get; init; }
    public string refresh_token { get; init; }
    public DateTime date_expiration_access_token { get; init; }
    public DateTime public_expiration_refresh_token { get; init; }
    public DateTime date_activated { get; init; }
    public string api_host { get; init; }
    public string store_id { get; init; }  
};