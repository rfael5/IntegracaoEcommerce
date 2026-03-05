public class GetIp
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public GetIp(IHttpContextAccessor httpContextAcessor)
    {
        this. httpContextAccessor = httpContextAcessor;
    }

    public string? GetClientIp()
    {
        return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
    }
}
