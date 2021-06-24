using LibrarySystem.Api.Constants;
using LibrarySystem.Application.Models;
using LibrarySystem.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retrieves a single user, with user contacts.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Single user, with user contacts.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserGetModel>> Get([FromRoute] Guid userId)
        {
            var getUserQuery = new GetUserQuery
            {
                UserId = userId
            };
            var result = await _sender.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a paged list of users.
        /// Supports searching on first name and last name.
        /// </summary>
        /// <param name="userQueryParameters">specify page number, paging parameteres and search terms.</param>
        /// <returns>Paged list of users.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetModel>>> GetPagedUsers([FromQuery] UserQueryParameters userQueryParameters)
        {
            var getUsersQuery = new GetUsersQuery
            {
                UserQueryParameters = userQueryParameters
            };
            var pagedResults = await _sender.Send(getUsersQuery);
            HttpContext.Response.Headers.Add(
                Headers.Pagination,
                new StringValues(JsonSerializer.Serialize(pagedResults.Paging)));
            return Ok(pagedResults.Items);
        }
    }
}
