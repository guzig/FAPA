using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FaPA.Core
{
    public static class ValidatableExtensions
    {
        public static bool IsValid(this IValidatable input)
        {
            return input.ValidationResult().Success;
        }

        public static string ValidationMessage(this IValidatable input)
        {
            return input.ValidationResult().Errors.ToValidationResult();
        }

        public static DomainResult ValidationResult(this IValidatable input)
        {
            var err = new Dictionary<string, List<string>>
            {
                { "Valori vuoti non ammessi", new List<string> { "Valori vuoti non ammessi" } }
            };
            if ( input == null ) return new DomainResult( false, err );
            input.Validate();
            return input.DomainResult;
        }

        public static string PropError(this DomainResult input, string propertyName)
        {
            return string.Join(Environment.NewLine, input.PropErrors(propertyName).ToArray() );
        }

        public static IEnumerable<string> PropErrors(this DomainResult input, string propertyName)
        {
            if ( string.IsNullOrWhiteSpace(propertyName) )
            {
                if ( input.Errors == null )
                    yield break;

                foreach ( var errors in input.Errors.SelectMany( error => error.Value ) )
                {
                    yield return errors;
                }
            }

            if ( input.Success ) yield break;

            if ( input.Errors == null || !input.Errors.Any() ) yield break;

            foreach (var err in input.Errors.Where( i=>i.Key==propertyName ).SelectMany(error => error.Value))
            {
                yield return err;
            }
        }

        public static string ToValidationResult(this IDictionary<string, List<string>> input)
        {
            if (input == null)
                return string.Empty;

            var errors = input.ToList();
            var success = !errors.Any();

            if ( success) return string.Empty;

            var sb = new StringBuilder();
            foreach (var item in input)
            {
                sb.Append(item.Key).Append(": ").Append(item.Value).Append("; ");
            }

            return sb.ToString();

        }

        private static readonly EmailAddressAttribute EmailValidator = new EmailAddressAttribute();

        public static bool IsValidEmail(this string email)
        {
            const int constantsMaxLengthEmail=80;
            return !string.IsNullOrWhiteSpace(email) &&
              EmailValidator.IsValid(email) && email.Length <= constantsMaxLengthEmail;
        }
    }
}