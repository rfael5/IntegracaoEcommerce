using System.Net;

public static class GetIp
{
    public static void GetClientIpAddress(this HttpContext context)
    {
        try
        {
            var forwarded = context.Request.Headers["X-Forwarded-For"].ToString();
            if(!string.IsNullOrEmpty(forwarded))
            {
                var firstIp = forwarded.Split(',')[0].Trim();
                if (IPAddress.TryParse(firstIp, out _))
                {
                    Console.WriteLine("###################################");
                    Console.WriteLine(firstIp);
                    Console.WriteLine("###################################");
                }
            }

            Console.WriteLine("###################################");
            Console.WriteLine(context.Connection.RemoteIpAddress?.ToString());
            Console.WriteLine("###################################");
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }
    // private readonly IHttpContextAccessor httpContextAccessor;

    // public GetIp(IHttpContextAccessor httpContextAcessor)
    // {
    //     this.httpContextAccessor = httpContextAcessor;
    // }

    // public string? GetClientIp()
    // {
    //     return httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
    // }
}
