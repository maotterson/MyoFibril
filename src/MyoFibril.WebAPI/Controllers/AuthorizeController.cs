using Microsoft.AspNetCore.Mvc;
using MyoFibril.WebAPI.Services.Interfaces;

namespace MyoFibril.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController
{
    private readonly ILogger<ActivityController> _logger;
}
