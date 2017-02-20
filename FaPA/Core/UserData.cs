using System;
using System.Collections.Generic;

namespace FaPA.Core
{
    public class UserData : BaseEntity
    {

        public virtual string UserName
        {
            get;
            set;
        }

        public virtual string Email
        {
            get;
            set;
        }

        public virtual string HashedPassword
        {
            get;
            protected set;
        }

        public virtual DateTime? PasswordExpiration { get; set; }

        public virtual DateTime? PasswordGeneration { get; protected set; }

        public virtual TipoUtenteEnums Role
        {
            get;
            set;
        }

        public virtual string Preferences { get; set; }

        public virtual string Claims { get; set; }

        public virtual void SetCredentials(string username, string plainTextPassword)
        {
            UserName = username;
            PasswordGeneration = DateTime.Today;
            SetPassword(plainTextPassword);
        }

        public virtual void SetPassword(string plainTextPassword)
        {
            HashedPassword = PasswordHasher.HashPassword(
              UserName, plainTextPassword);
        }

        public virtual void ResetPassword()
        {
            HashedPassword = string.Empty;
            PasswordExpiration = null;
            PasswordGeneration = null;
        }

        public override DomainResult Validate()
        {
            var errors = new Dictionary<string, List<string>>();

            GetPersistentErrors( errors );

            DomainResult = new DomainResult( errors );

            return DomainResult;
        }

        public override DomainResult ValidatePropertyValue( string prop )
        {
            Validate();
            return DomainResult;
        }
    }
}
