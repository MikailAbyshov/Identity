using System.Security.Cryptography;
using System.Text;

namespace Domain.ValueObjects;

public sealed class Password : ValueObject
{
  public string Hash { get; }
  public string Salt { get; }

  public Password(string plainTextPassword)
  {
    if (string.IsNullOrWhiteSpace(plainTextPassword))
      throw new ArgumentException("Password cannot be empty");

    if (plainTextPassword.Length < 8)
      throw new ArgumentException("Password must be at least 8 characters long");

    Salt = GenerateSalt();
    Hash = HashPassword(plainTextPassword, Salt);
  }

  public Password(string hash, string salt)
  {
    Hash = hash;
    Salt = salt;
  }

  public bool Verify(string plainTextPassword)
  {
    var hashedInput = HashPassword(plainTextPassword, Salt);
    return Hash == hashedInput;
  }

  private static string GenerateSalt()
  {
    var saltBytes = new byte[16];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(saltBytes);
    return Convert.ToBase64String(saltBytes);
  }

  private static string HashPassword(string password, string salt)
  {
    using var sha256 = SHA256.Create();
    var saltedPassword = password + salt;
    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
    return Convert.ToBase64String(hashedBytes);
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return Hash;
    yield return Salt;
  }
}