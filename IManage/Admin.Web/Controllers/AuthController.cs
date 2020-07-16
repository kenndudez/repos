using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Auth.Core.Services.Interfaces;
using Auth.Core.ViewModels;
using Imanage.Shared;
using Imanage.Shared.AspNetCore;
using Imanage.Shared.Enums;
using Imanage.Shared.Helpers;
using Imanage.Shared.Models;
using Imanage.Shared.Timing;
using Imanage.Shared.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Mvc.Internal;
using OpenIddict.Server;

namespace Auth.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly SignInManager<ImanageUser> _signInManager;
        private readonly UserManager<ImanageUser> _userManager;
        private readonly IUserService _userService;

        public AuthController(IOptions<IdentityOptions> identityOptions, SignInManager<ImanageUser> signInManager,
            UserManager<ImanageUser> userManager, IUserService userService)
        {
            _identityOptions = identityOptions;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }


        /// <summary>
        /// Get logged-in user profile basic information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<UserProfileViewModel>), 200)]
        public async Task<IActionResult> GetBasicProfile()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(CurrentUser.UserId.ToString());
                var profile = new UserProfileViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    Activated = user.Activated,
                    CreatedOnUtc = user.CreatedOnUtc,
                    FullName = user.FullName,
                    LastLoginDate = user.LastLoginDate,
                    RoleName = user.UserType.ToString()
                };

                var rsp = new ApiResponse<UserProfileViewModel>
                {
                    Code = ApiResponseCodes.OK,
                    Description = "User profile loaded ok",
                    Payload = profile
                };
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            try
            {
                ApiResponse<bool> rsp = new ApiResponse<bool>();

                if (model == null)
                {
                    rsp.Code = ApiResponseCodes.INVALID_REQUEST;
                    rsp.Description = "Model cannot null";
                    return Ok(rsp);
                }


                if (!ModelState.IsValid)
                {
                    rsp.Code = ApiResponseCodes.INVALID_REQUEST;
                    rsp.Description = GetModelStateValidationError();
                    return Ok(rsp);
                }

                var user = await _userManager.FindByIdAsync(CurrentUser.UserId.ToString());

                // verify new password is not same as current
                if (await _userManager.CheckPasswordAsync(user, model.NewPassword))
                {
                    rsp.Code = ApiResponseCodes.INVALID_REQUEST;
                    rsp.Description = "Your new password must be different from the current one";
                    return Ok(rsp);
                }

                var changeResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!changeResult.Succeeded)
                {
                    rsp.Code = ApiResponseCodes.ERROR;
                    rsp.Description = changeResult.Errors.FirstOrDefault().Description;
                    return Ok(rsp);
                }

                rsp.Code = ApiResponseCodes.OK;
                rsp.Payload = true;
                rsp.Description = $"You've changed your password";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [AllowAnonymous]
        [HttpPost("~/api/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Token([ModelBinder(BinderType = typeof(OpenIddictMvcBinder))] OpenIdConnectRequest request)
        {
            try
            {
                if (request.IsPasswordGrantType())
                {
                    var user = await _userManager.FindByEmailAsync(request.Username);

                    if (user == null)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "Email or password is incorrect."
                        });
                    }

                    if (user.IsDeleted)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "You are not allowed to sign in."
                        });
                    }

                    // Check if profile is activated
                    if (!user.Activated)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.AccessDenied,
                            ErrorDescription = "Your profile has not been activated."
                        });
                    }

                    // Ensure the user is allowed to sign in.
                    if (!await _signInManager.CanSignInAsync(user))
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.AccessDenied,
                            ErrorDescription = "You are not allowed to sign in."
                        });
                    }

                    // Reject the token request if two-factor authentication has been enabled by the user.
                    /*if (_userManager.SupportsUserTwoFactor && await _userManager.GetTwoFactorEnabledAsync(user))
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.AccessDenied,
                            ErrorDescription = "You are not allowed to sign in."
                        });
                    }*/


                    // Ensure the user is not already locked out.
                    if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user))
                    {
                        var lockoutMinutes = _userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalMinutes;

                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.AccessDenied,
                            ErrorDescription = $"Your account has been locked. " +
                            $"Please contact the platform Administrators immediately or wait for {lockoutMinutes} minute(s) to retry"
                        });
                    }

                    // Ensure the password is valid.
                    if (!await _userManager.CheckPasswordAsync(user, request.Password))
                    {
                        if (_userManager.SupportsUserLockout)
                        {
                            await _userManager.AccessFailedAsync(user);
                        }

                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "Email or password is incorrect."
                        });
                    }

                    //2FA 
                    if (_userManager.SupportsUserTwoFactor && await _userManager.GetTwoFactorEnabledAsync(user))
                    {
                        if (!await _signInManager.IsTwoFactorClientRememberedAsync(user))
                        {
                            //TODO set if remember device me is true
                            await _signInManager.RememberTwoFactorClientAsync(user);
                        }

                        if (string.IsNullOrEmpty(request.Code))
                        {
                            //Sign In
                            var ticket2 = await CreateTicketAsync(request, user);
                            SignIn(ticket2.Principal, ticket2.Properties, ticket2.AuthenticationScheme);
                            //Send 2fa code
                            await _userService.Send2FAToken(user);
                            return Ok("2FA Token has been sent");
                        }
                        else
                        {
                            //Verify 2fa
                            var verifyTwoFAResult = await _userManager.VerifyTwoFactorTokenAsync(user, "Phone2FA", request.Code);
                            if (!verifyTwoFAResult)
                            {
                                return Ok("Invalid Two Factor Token");
                            }

                        }
                    }

                    if (_userManager.SupportsUserLockout)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                    }

                    user.LastLoginDate = Clock.Now;
                    await _userManager.UpdateAsync(user);

                    // Create a new authentication ticket.
                    var ticket = await CreateTicketAsync(request, user);
                    return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
                }
                else if (request.IsRefreshTokenGrantType())
                {
                    // Retrieve the claims principal stored in the refresh token.
                    var info = await HttpContext.AuthenticateAsync(
                        OpenIddictServerDefaults.AuthenticationScheme);

                    // Retrieve the user profile corresponding to the refresh token.
                    // Note: if you want to automatically invalidate the refresh token
                    // when the user password/roles change, use the following line instead:
                    // var user = _signInManager.ValidateSecurityStampAsync(info.Principal);
                    var user = await _userManager.GetUserAsync(info.Principal);
                    if (user == null)
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "The refresh token is no longer valid."
                        });
                    }

                    // Ensure the user is still allowed to sign in.
                    if (!await _signInManager.CanSignInAsync(user))
                    {
                        return BadRequest(new OpenIdConnectResponse
                        {
                            Error = OpenIdConnectConstants.Errors.InvalidGrant,
                            ErrorDescription = "The user is no longer allowed to sign in."
                        });
                    }

                    // Create a new authentication ticket, but reuse the properties stored
                    // in the refresh token, including the scopes originally granted.
                    var ticket = await CreateTicketAsync(request, user, info.Properties);
                    return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
                }

                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                    ErrorDescription = "The specified grant type is not supported."
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest oidcRequest, ImanageUser user,
          AuthenticationProperties properties = null)
        {
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            AddUserClaims(user, identity);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal, properties, OpenIddictServerDefaults.AuthenticationScheme);

            if (!oidcRequest.IsRefreshTokenGrantType())
            {
                // Set the list of scopes granted to the client application.
                // Note: the offline_access scope must be granted
                // to allow OpenIddict to return a refresh token.
                ticket.SetScopes(new[]
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles,

                }.Intersect(oidcRequest.GetScopes()));
            }

            ticket.SetResources("resource_server");

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.
            var destinations = new List<string>
            {
                OpenIdConnectConstants.Destinations.AccessToken
            };

            foreach (var claim in ticket.Principal.Claims)
            {
                // Never include the security stamp in the access and identity tokens, as it's a secret value.
                if (claim.Type == _identityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
                {
                    continue;
                }

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if ((claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Roles)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Audience && ticket.HasScope(OpenIddictConstants.Claims.Audience))

                    )
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                claim.SetDestinations(destinations);
            }

            var name = new Claim(OpenIdConnectConstants.Claims.GivenName, user.FullName ?? "[NA]");
            name.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken);
            identity.AddClaim(name);

            //var usertype = new Claim(OpenIdConnectConstants.Claims.Audience, user.UserType.ToString());
            //usertype.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken);
            //identity.AddClaim(usertype);
            return ticket;
        }

        private void AddUserClaims(ImanageUser user, ClaimsIdentity identity)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            //Todo: Claims here such as permissions

            identity.AddClaim(ClaimTypesHelper.FirstName, user.FirstName);
            identity.AddClaim(ClaimTypesHelper.LastName, user.LastName);

            if (user.LastLoginDate.HasValue)
                identity.AddClaim(ClaimTypesHelper.LastLogin, user.LastLoginDate.Value.ToDateString("dd/MM/yyyy"));

            identity.AddClaim(ClaimTypesHelper.UserType, ((int)user.UserType).ToString());
        }
    }
}
