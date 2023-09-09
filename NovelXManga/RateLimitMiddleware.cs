using System.Collections.Concurrent;

namespace NovelXManga
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TimeSpan _timeLimit;
        private readonly ConcurrentDictionary<string, RateLimitInfo> _requestTimes;

        public RateLimitMiddleware(RequestDelegate next, TimeSpan timeLimit)
        {
            _next = next;
            _timeLimit = timeLimit;
            _requestTimes = new ConcurrentDictionary<string, RateLimitInfo>();
        }

        public class RateLimitInfo
        {
            public DateTime LastRequest { get; set; }
            public int RequestCount { get; set; }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var clientIp = context.Connection.RemoteIpAddress.ToString();
            var dictionaryKey = $"{clientIp}";

            if (path.StartsWithSegments(new PathString("/Login/LoginIndex")) ||
                path.StartsWithSegments(new PathString("/Login/ForgotPassword")) ||
                path.StartsWithSegments(new PathString("/Login/ResetPasswordPage")) ||
                path.StartsWithSegments(new PathString("/Login/LogOut")) ||
                path.StartsWithSegments(new PathString("/Register/UserRegister")))
            {
                if (!_requestTimes.TryGetValue(dictionaryKey, out var rateLimitInfo))
                {
                    rateLimitInfo = new RateLimitInfo { LastRequest = DateTime.Now, RequestCount = 1 };
                    _requestTimes.TryAdd(dictionaryKey, rateLimitInfo);
                }
                else
                {
                    rateLimitInfo.RequestCount++;
                    if (rateLimitInfo.LastRequest.Add(_timeLimit) > DateTime.Now)
                    {
                        if (rateLimitInfo.RequestCount > 15)  // Allow 6 requests in a 1-minute window
                        {
                            context.Response.StatusCode = 429; // Too Many Requests
                            await context.Response.WriteAsync("Rate limit exceeded. Wait a short while.");
                            return;
                        }
                    }
                    else
                    {
                        rateLimitInfo.LastRequest = DateTime.Now;
                        rateLimitInfo.RequestCount = 1;
                    }
                }
            }

            await _next(context);
        }
    }
}