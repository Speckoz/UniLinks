using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using UniLink.Dependencies.Models;

namespace UniLink.API.Services
{
	public class GenerateTokenService
	{
		private readonly IConfiguration _configuration;

		public GenerateTokenService(IConfiguration configuration) =>
			_configuration = configuration;

		public string Generate(UserModel user)
		{
			return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				audience: _configuration["JWT:Audience"],
				claims: new[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
					new Claim(ClaimTypes.Role, user.UserType.ToString()),
				},
				expires: DateTime.Now.AddHours(5),
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256
				)));
		}
	}
}