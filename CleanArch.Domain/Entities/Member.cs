using CleanArch.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities
{
   public sealed class Member : Entity
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Gender { get; private set; }
        public string? Email { get; private set; }
        public bool? IsActive { get; private set; }

        public Member(string firstname, string lastname, string gender, string email, bool? active)
        {
            ValidateDomain(firstname, lastname, gender, email, active);
        }

        public Member() { }

        [JsonConstructor]
        public Member(int id, string firstname, string lastname, string gender, string email, bool? active)
        {
            DomainValidation.When(id <= 0, "Invalid Id value");
            Id = id;
            ValidateDomain(firstname, lastname, gender, email, active);
        }
        public void Update(string firstname, string lastname, string gender, string email, bool? active)
        {
            ValidateDomain(firstname, lastname, gender, email, active);
        }

        private void ValidateDomain(string firstname, string lastname, string gender, string email, bool? active)
        {
            DomainValidation.When(string.IsNullOrWhiteSpace(firstname), "First Name is required");
            DomainValidation.When(firstname.Length < 3, "Invalid First Name. Mininum 3 characteres");
            DomainValidation.When(firstname.Length > 50, "Invalid First Name. Maximum 50 characteres");
            DomainValidation.When(string.IsNullOrWhiteSpace(lastname), "Last Name is required");
            DomainValidation.When(lastname.Length < 3, "Invalid Last Name. Mininum 3 characteres");
            DomainValidation.When(lastname.Length > 50, "Invalid Last Name. Maximum 50 characteres");
            DomainValidation.When(string.IsNullOrWhiteSpace(gender), "Invalid gender, Gender is required");
            DomainValidation.When(string.IsNullOrWhiteSpace(email),"Invalid E-mail");
            DomainValidation.When(email.Length < 6, "Invalid E-mail. Mininum 6 characteres");
            DomainValidation.When(email.Length > 100, "Invalid E-mail. Maximum 100 characteres");
            DomainValidation.When(!active.HasValue, "Must define activity");

            FirstName = firstname;
            LastName = lastname;
            Gender = gender;
            Email = email;
            IsActive = active;
        }
    }
}
