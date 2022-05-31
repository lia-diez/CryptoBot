using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoController : Controller
{
    [HttpGet]
    public IActionResult Get(string currency)
    {
        var response = new
        {
            name = currency,
            exchangeRate = Random.Shared.NextSingle()
        };
        return Json(response);
    }
}