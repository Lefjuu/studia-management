using System.Security.Cryptography;


public class GenerateKey
{
  public static string GenerateSecureKey()
  {
    using (var randomNumberGenerator = new RNGCryptoServiceProvider())
    {
      var randomBytes = new byte[32]; // 32 bytes will give us 256 bits.
      randomNumberGenerator.GetBytes(randomBytes);
      return Convert.ToBase64String(randomBytes);
    }
  }
}