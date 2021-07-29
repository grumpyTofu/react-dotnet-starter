using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using Google.Apis.Auth;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{

    //AuthorizationFilterContext
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new ApiResponse<dynamic>(true, "Unauthorized")) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}