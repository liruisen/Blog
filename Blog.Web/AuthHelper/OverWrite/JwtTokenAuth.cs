﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Web.AuthHelper.OverWrite
{
    public class JwtTokenAuth
    {
        private readonly RequestDelegate _next;
        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含"Authorization"请求头
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                if (tokenHeader.Length>=128)
                {
                    TokenModelJwt tm = JwtHelper.SerializeJwt(tokenHeader);
                    //授权 claim 关键
                    var claimList = new List<Claim>();
                    var claim = new Claim(ClaimTypes.Role, tm.Role);
                    claimList.Add(claim);
                    var identity = new ClaimsIdentity(claimList);
                    var principal = new ClaimsPrincipal(identity);
                    httpContext.User = principal;
                }
            }catch(Exception e)
            {
                Console.WriteLine($"{DateTime.Now} midddleware wrong :{e.Message}");
            }
            return _next(httpContext);
        }
    }
    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
    }
}
