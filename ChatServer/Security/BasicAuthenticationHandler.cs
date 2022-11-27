using ChatServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ChatServer.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserService userService;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, UserService userService) : base(options, logger, encoder, clock)
        {
            this.userService = userService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var header = Request.Headers["Authorization"].ToString();
            if(header is null || !header.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            {
                Response.StatusCode = 401;
                Response.Headers.Add("WWW-Authenticate", "Basic");
                return Task.FromResult(AuthenticateResult.Fail("Missing or invalid header"));
            }

            var token = header[6..];

            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
            if (!userService.CheckLogin(credentials[0], credentials[1]))
            {
                Response.StatusCode = 401;
                Response.Headers.Add("WWW-Authenticate", "Basic");
                return Task.FromResult(AuthenticateResult.Fail("Bad password or login"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, credentials[0]), new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, "Basic")));

        }
    }
}
