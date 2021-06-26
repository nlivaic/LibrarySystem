using AutoMapper;
using LibrarySystem.Api.Constants;
using LibrarySystem.Api.Models.Users;
using LibrarySystem.Api.ResourceParameters;
using LibrarySystem.Application.Models;
using LibrarySystem.Application.Users;
using LibrarySystem.Application.Users.Commands;
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
        private readonly IMapper _mapper;

        public UsersController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a single user, with user contacts.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Single user, with user contacts.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("{userId}", Name = "Get")]
        public async Task<ActionResult<UserGetResponse>> Get([FromRoute] Guid userId)
        {
            var getUserQuery = new GetUserQuery
            {
                UserId = userId
            };
            var result = await _sender.Send(getUserQuery);
            var response = _mapper.Map<UserGetResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a paged list of users.
        /// Supports searching on first name and last name.
        /// </summary>
        /// <param name="userResourceParameters">Specify page number,
        /// paging parameteres, search terms and/or sort parameters.</param>
        /// <returns>Paged list of users.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetModel>>> GetPagedUsers([FromQuery] UserResourceParameters userResourceParameters)
        {
            var getUsersQuery = new GetUsersQuery
            {
                UserQueryParameters = _mapper.Map<UserQueryParameters>(userResourceParameters)
            };
            var pagedResults = await _sender.Send(getUsersQuery);
            HttpContext.Response.Headers.Add(
                Headers.Pagination,
                new StringValues(JsonSerializer.Serialize(pagedResults.Paging)));
            return Ok(pagedResults.Items);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">User data.</param>
        /// <returns>User representation.</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<UserGetResponse>> PostAsync([FromBody] UserCreateRequest request)
        {
            var createUserCommand  = _mapper.Map<CreateUserCommand>(request);
            var result = await _sender.Send(createUserCommand);
            var response = _mapper.Map<UserGetResponse>(result);
            return CreatedAtRoute("Get", new { userId = response.Id }, response);
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="request">User data.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{userId}")]
        public async Task<ActionResult> PutAsync(
            [FromRoute] Guid userId,
            [FromBody] UserUpdateRequest request)
        {
            var updateUserCommand = _mapper.Map<UpdateUserCommand>(request);
            updateUserCommand.UserId = userId;
            await _sender.Send(updateUserCommand);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status409Conflict)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid userId)
        {
            var deleteUserCommand = new DeleteUserCommand
            {
                UserId = userId
            };
            await _sender.Send(deleteUserCommand);
            return NoContent();
        }
    }
}
