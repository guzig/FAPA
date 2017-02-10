using System;
using FaPA.Core;

namespace FaPA.DomainServices.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {

        public UserData AuthenticateUser(string username, string clearTextPassword)
        {
            var user = AuthDbHelpers.TryGetUser(username);

            if ( user == null )
                throw new UnauthorizedAccessException( "Access negato: utente non riconosciuto." );

            if ( string.IsNullOrWhiteSpace( user.HashedPassword ) && string.IsNullOrWhiteSpace( clearTextPassword ) )
                return user;

            if ( !PasswordHasher.HashPassword( user.UserName, clearTextPassword ).Equals( user.HashedPassword ) )
                throw new UnauthorizedAccessException( "Access negato: password non valida." );

            return user;
        }

        //private string CalculateHash(string clearTextPassword, string salt)
        //{
        //    // Convert the salted password to a byte array
        //    byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
        //    // Use the hash algorithm to calculate the hash
        //    HashAlgorithm algorithm = new SHA256Managed();
        //    byte[] hash = algorithm.ComputeHash(saltedHashBytes);
        //    // Return the hash as a base64 encoded string to be compared to the stored password
        //    return Convert.ToBase64String(hash);
        //}
    }
}