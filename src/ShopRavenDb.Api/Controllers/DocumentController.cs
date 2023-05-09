using ShopRavenDb.Domain.Model;

namespace ShopRavenDb.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentController : ControllerBase
{
    private readonly IDocumentApplication _documentApplication;

    public DocumentController(IDocumentApplication documentApplication)
    {
        _documentApplication = documentApplication;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddAnnexDocument(IFormFile file, [FromQuery]string version, [FromQuery]string description, [FromQuery]IEnumerable<Build> builds )
    {

        await _documentApplication.AttachDocument(version, description, builds, file).ConfigureAwait(false);
        return Ok("Customer Inserted successfully!");
    }
}