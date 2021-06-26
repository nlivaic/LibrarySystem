using AutoMapper;
using LibrarySystem.Api.Models.UserContacts;
using LibrarySystem.Api.Models.Users;
using LibrarySystem.Application.UserContacts.Commands;
using LibrarySystem.Application.UserContacts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("/api/users/{userId}/[controller]")]
    public class UserContactsController : Controller
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UserContactsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a single user contact.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="userContactId">User contact identifier.</param>
        /// <returns>User contact.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("{userContactId}", Name = "GetUserContact")]
        public async Task<ActionResult<UserContactGetResponse>> Get(
            [FromRoute] Guid userId,
            [FromRoute] Guid userContactId)
        {
            var getUserContactQuery = new GetUserContactQuery
            {
                UserId = userId,
                UserContactId = userContactId
            };
            var result = await _sender.Send(getUserContactQuery);
            var response = _mapper.Map<UserContactGetResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user contact.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="request">User contact data.</param>
        /// <returns>User contact representation.</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<UserGetResponse>> PostAsync(
            [FromRoute] Guid userId,
            [FromBody] UserContactCreateRequest request)
        {
            var createUserContactCommand  = _mapper.Map<CreateUserContactCommand>(request);
            createUserContactCommand.UserId = userId;
            var result = await _sender.Send(createUserContactCommand);
            var response = _mapper.Map<UserContactGetResponse>(result);
            return CreatedAtRoute(
                "GetUserContact",
                new { userId, userContactId = response.Id },
                response);
        }

        /// <summary>
        /// Updates a user contact.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="userContactId">User contact identifier.</param>
        /// <param name="request">User contact data.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{userContactId}")]
        public async Task<ActionResult> PutAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid userContactId,
            [FromBody] UserContactUpdateRequest request)
        {
            var updateUserContactCommand = _mapper.Map<UpdateUserContactCommand>(request);
            updateUserContactCommand.UserId = userId;
            updateUserContactCommand.UserContactId = userContactId;
            await _sender.Send(updateUserContactCommand);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="userContactId">User contact identifier.</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status409Conflict)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpDelete("{userContactId}")]
        public async Task<ActionResult> DeleteAsync(
            [FromRoute] Guid userId,
            [FromRoute] Guid userContactId)
        {
            var deleteUserContactCommand = new DeleteUserContactCommand
            {
                UserId = userId,
                UserContactId = userContactId
            };
            await _sender.Send(deleteUserContactCommand);
            return NoContent();
        }
    }
}
