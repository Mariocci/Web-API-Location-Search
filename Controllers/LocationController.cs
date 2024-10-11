using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiLocationSearch.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    
}

