using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OSRSPredator.Extensions;

namespace OSRSPredator.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null)
                return NotFound();
            if(result.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(result.Value);
            }  
            if(result.StatusCode == StatusCodes.Status400BadRequest)
            {
                return ValidationProblem(new ValidationProblemDetails(result.Errors));
            }
            if(result.StatusCode == StatusCodes.Status500InternalServerError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
        {
            if(result == null)
                return NotFound();
            if(result.StatusCode == StatusCodes.Status200OK)
            {
                Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize, 
                    result.Value.TotalCount, result.Value.TotalPages);
                return Ok(result.Value);
            }  
            if(result.StatusCode == StatusCodes.Status400BadRequest)
            {
                return ValidationProblem(new ValidationProblemDetails(result.Errors));
            }
            if(result.StatusCode == StatusCodes.Status500InternalServerError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}