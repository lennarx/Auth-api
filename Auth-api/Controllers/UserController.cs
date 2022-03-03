using Auth_api.Forms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Users.Commands;
using Services.Users.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Auth_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllUsersQuery(), CancellationToken.None));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace, ex.InnerException);
                throw new Exception(Constants.GenericError);
            }
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetUserByIdQuery(id), CancellationToken.None));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message, ex.StackTrace);
                throw new Exception(Constants.GenericError);
            }
        }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> CreateUser([FromBody] UserForm createUserForm)
            {
                try
                {
                    var createdUser = _mediator.Send(new InsertUserCommand(createUserForm.UserName, createUserForm.Password, createUserForm.User));
                    return this.CreatedAtAction(nameof(this.Get), new { id = createdUser.Id}, createdUser);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.StackTrace, e.InnerException, e.Message);
                    throw new Exception("Ha ocurrido un error al crear el usuario");
                }
            }
        }
    }
