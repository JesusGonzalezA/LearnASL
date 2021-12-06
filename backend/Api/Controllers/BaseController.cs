using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

[Controller]
public abstract class BaseController : ControllerBase
{
    // returns the current authenticated email (null if not logged in)
    public string EmailOfCurrentUser => User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
}