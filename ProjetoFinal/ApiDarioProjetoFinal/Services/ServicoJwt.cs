using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDarioProjetoFinal.Services
{
    public class ServicoJwt
    {
        public string CriarToken(string email, string perfil)
        {
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("chave-super-secreta-api-dario-123")
            );

            var credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256
            );

            var dados = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, perfil)
            };

            var token = new JwtSecurityToken(
                claims: dados,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}