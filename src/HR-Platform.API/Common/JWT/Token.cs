using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace HR_Platform.API.Common.JWT;

public class Token
{
    public string GetEmailFromToken(HttpRequest httpRequest)
    {
        try
        {
            string token = GetToken(httpRequest);

            JwtSecurityTokenHandler handler = new();

            JwtSecurityToken decodedToken = handler.ReadJwtToken(token);

            string email = string.Empty;

            email = decodedToken.Claims.First(x => x.Type == "email" || x.Type == "username").Value;

            return email;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public string GetToken(HttpRequest httpRequest)
    {
        try
        {
            StringValues bearerToken = string.Empty;

            httpRequest.Headers.TryGetValue("Authorization", out bearerToken);

            string[] bearerTokenSplit = bearerToken.ToString().Split(" ");

            string token = string.Empty;

            if (bearerTokenSplit != null && bearerTokenSplit.Length > 1)
                token = bearerTokenSplit[1];

            return token;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
