using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Dto;

namespace Pharmacy.APIs;

[Authorize]
[Route("/api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult<Result<T>> BaseResponseHandler<T>(Result<T> response)
    {
        return Ok(response);
    }
}
