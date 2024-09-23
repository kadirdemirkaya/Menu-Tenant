using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.BaseTypes;

namespace Shared.Domain.Aggregates.UserAggregate
{
    public class AppUser : AggregateRoot<AppUserId>
    {
        public string? Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }


        public readonly List<Company> Companies = new();
        public IReadOnlyCollection<Company> _companies => Companies.AsReadOnly();


        public AppUser()
        {

        }

        public AppUser(AppUserId id) : base(id)
        {
            Id = id;
        }

        public AppUser(string userName, string email, string password, string phoneNumber)
        {
            Id = AppUserId.CreateUnique();
            Username = Username;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public AppUser(AppUserId appUserId, string userName, string email, string password, string phoneNumber)
        {
            Id = appUserId;
            Username = Username;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public static AppUser Create(string userName, string email, string password, string phoneNumber, List<Company> companies)
            => new(userName, email, password, phoneNumber);

        public static AppUser Create(AppUserId appUserId, string userName, string email, string password, string phoneNumber)
            => new(appUserId, userName, email, password, phoneNumber);

        public void AddCompany(Company company)
        {
            Companies.Add(company);
        }

        public void RemoveCompany(CompanyId companyId)
        {
            Company company = Companies.FirstOrDefault(c => c.Id == companyId);
            Companies.Remove(company);
        }

        public void SetTenantIdForAppUser(string id)
        {
            SetTenantId(id);
        }

    }
}
