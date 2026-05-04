public record AuthResponse 
{
    public int code { get; init; }
    public string message { get; init; }
    public string access_token { get; init; }
    public string refresh_token { get; init; }
    public string date_expiration_access_token { get; init; }
    public string date_expiration_refresh_token { get; init; }
    public string date_activated { get; init; }
    public string api_host { get; init; }
    public string store_id { get; init; }  
};

public record RefreshResponse 
{
    public string code { get; init; }
    public string message { get; init; }
    public string access_token { get; init; }
    public string refresh_token { get; init; }
    public string date_expiration_access_token { get; init; }
    public string date_expiration_refresh_token { get; init; }
    public string date_activated { get; init; }
    public string api_host { get; init; }
    public string store_id { get; init; }  
};