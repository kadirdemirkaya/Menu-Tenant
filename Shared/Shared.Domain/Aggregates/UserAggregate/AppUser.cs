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
            Username = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public AppUser(AppUserId appUserId, string userName, string email, string password, string phoneNumber)
        {
            Id = appUserId;
            Username = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public AppUser(AppUserId appUserId, string userName, string email, string password, string phoneNumber, string tenantId)
        {
            Id = appUserId;
            Username = userName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            SetTenantIdForEntity(tenantId);
        }

        public static AppUser Create(string userName, string email, string password, string phoneNumber)
            => new(userName, email, password, phoneNumber);

        public static AppUser Create(AppUserId appUserId, string userName, string email, string password, string phoneNumber)
            => new(appUserId, userName, email, password, phoneNumber);

        public static AppUser Create(AppUserId appUserId, string userName, string email, string password, string phoneNumber, string tenantId)
            => new(appUserId, userName, email, password, phoneNumber, tenantId);


        public AppUser SetId(AppUserId id) { Id = id; return this; }
        public AppUser SetUsername(string userName) { Username = Username; return this; }
        public AppUser SetEmail(string email) { Email = email; return this; }
        public AppUser SetPassword(string password) { Password = password; return this; }
        public AppUser SetPhoneNumber(string phoneNumber) { PhoneNumber = phoneNumber; return this; }
        public AppUser SetTenantIdForEntity(string id) { SetTenantId(id); return this; }
        public AppUser SetUpdatedDate(DateTime updateDate) { SetCreatedDateUTC(updateDate); return this; }
        public AppUser SetCreatedDate(DateTime createdDate) { SetCreatedDateUTC(createdDate); return this; }
        public AppUser SetIsDeletedForEntity(bool isDeleted) { SetIsDeleted(isDeleted); return this; }

        #region Old Seter Method
        //public void SetUsername(string userName)
        //{
        //    Username = userName;
        //}

        //public void SetEmail(string email)
        //{
        //    Email = email;
        //}

        //public void SetPassword(string password)
        //{
        //    Password = HashProvider.HashPassword(password);
        //}

        //public void SetPhoneNumber(string phoneNumber)
        //{
        //    PhoneNumber = phoneNumber;
        //}

        //public void SetTenantIdForEntity(string id)
        //{
        //    SetTenantId(id);
        //}
        #endregion

        public void UpdateUser(string userName, string email, string password, string phoneNumber)
        {
            SetUsername(userName)
            .SetEmail(email)
            .SetPassword(password)
            .SetPhoneNumber(phoneNumber);
        }

        public void UpdateUser(AppUserId appUserId, string userName, string email, string password, string phoneNumber)
        {
            SetId(appUserId)
            .SetUsername(userName)
            .SetEmail(email)
            .SetPassword(password)
            .SetPhoneNumber(phoneNumber);
        }

        public void UpdateUser(AppUserId appUserId, string userName, string email, string password, string phoneNumber, string tenantId)
        {
            SetId(appUserId)
             .SetUsername(userName)
             .SetEmail(email)
             .SetPassword(password)
             .SetPhoneNumber(phoneNumber)
             .SetTenantId(tenantId);
        }


        public void AddCompany(Company company)
        {
            Companies.Add(company);
        }

        public void RemoveCompany(CompanyId companyId)
        {
            Company company = Companies.FirstOrDefault(c => c.Id == companyId);
            Companies.Remove(company);
        }

        public void UpdateCompany(Company company)
        {
            Company? updateEntity = Companies.FirstOrDefault(c => c.Id == company.Id);

            if (updateEntity is null)
                return;

            updateEntity.UpdateCompany(company.Id, company.Name, company.DatabaseName, company.AppUserId);

            if (company.TenantId is not null)
                updateEntity.UpdateCompany(company.Id, company.Name, company.DatabaseName, company.AppUserId, company.TenantId);
        }
    }
}
