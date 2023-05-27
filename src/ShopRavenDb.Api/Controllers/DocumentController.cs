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


    [HttpPost("attach", Name = "attach-document")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AttachDocument(IFormFile file)
    {
       var result = await _documentApplication.AttachDocument(file).ConfigureAwait(false);
       
       return Ok(result);
    }
    
    [HttpGet("attach/download", Name = "get-attach-document")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAttachDocument(string documentId)
    {
        var formattedDocumentId = Uri.UnescapeDataString(documentId);
        var attachment = await _documentApplication.GetAttachDocument(formattedDocumentId).ConfigureAwait(false);
        
        if (attachment is null)
            return NotFound();

        return File(attachment.Stream, attachment.Details.ContentType, attachment.Details.Name);
    }
}