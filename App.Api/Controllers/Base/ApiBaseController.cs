
namespace App.Api.Controllers.Base;

[ApiController]
[Authorize(AuthenticationSchemes = "Identity.Application, Bearer")]
public class ApiBaseController : ControllerBase
{
}
