﻿namespace Online.Travel.AuthService.API.Controllers
{
    using System;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using MediatR;
    using global::Online.Travel.AuthService.API.Model;
    using global::Online.Travel.AuthService.API.Infrastructure;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AuthServiceController : Controller
    {
        private readonly IMediator mediatR;

        private readonly ITokenGenerator tokenGenerator;

        /// <summary>
        /// AuthServiceController constructor
        /// </summary>
        /// <param name="mediatR"></param>
        /// <param name="tokenGenerator"></param>
        public AuthServiceController(IMediator mediatR, ITokenGenerator tokenGenerator)
        {
            this.mediatR = mediatR;
            this.tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userModelRequest">Create new user</param>
        /// <returns>Saved user data object</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Post([FromBody]UserModel userModelRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await mediatR.Send(new CreateUserRequest(userModelRequest));

                if (response == null)
                {
                    return StatusCode(409, $"Error Occurred While Creating a User {userModelRequest.Email}");
                }

                return Created("user created", response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred While Creating a User" + ex.Message);
            }
        }

        /// <summary>
        /// verify user detail
        /// </summary>
        /// <param name="user">login user info</param>
        /// <returns>verify user detail</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Login([FromBody]UserModel userModelRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await mediatR.Send(new LoginUserRequest(userModelRequest));

                if (response == null)
                {
                    return NotFound($"Email Id {userModelRequest.Email} not found");
                }

                string value = tokenGenerator.GetJwtTokenLoggedinUser(userModelRequest);

                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occurred While login" + ex.Message);
            }
        }
    }
}