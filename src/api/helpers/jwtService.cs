using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api.helpers;

public class JwtService
{
  private string _secureKey = "secureKey aifni2n4j23nrjingfvoi34ngfvugfdi";


  public string Generate(int id)
  {
    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
    var header = new JwtHeader(signingCredentials);
    var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(7));
    var securityToken = new JwtSecurityToken(header, payload);
    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }

  public JwtSecurityToken Verify(string jwt)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_secureKey);
    tokenHandler.ValidateToken(jwt, new TokenValidationParameters
    {
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateIssuerSigningKey = true,
      ValidateIssuer = false,
      ValidateAudience = false
    }, out SecurityToken validatedToken);
    
    return (JwtSecurityToken)validatedToken;
  }
}
