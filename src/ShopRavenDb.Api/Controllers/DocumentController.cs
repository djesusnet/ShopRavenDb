using Microsoft.AspNetCore.Mvc;

namespace ShopRavenDb.Api.Controllers;

public class DocumentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}