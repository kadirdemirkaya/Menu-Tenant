namespace Auth.Application.Dtos.User
{
    public class UserRegisterModelDto
    {
        public UserModelDto userModelDto { get; set; }
        public CompanyModelDto companyModelDto { get; set; }
        public ConnectionPoolModelDto connectionPoolModelDto { get; set; }
    }

    /*
{
  "userModelDto": {
    "username": "kadir",
    "email": "kadir@gmail.com",
    "password": "kadir123",
    "phoneNumber": "5556667788"
  },
  "companyModelDto": {
    "name": "kadircompany",
    "databaseName": "shared"
  },
  "connectionPoolModelDto": {
    "isWantShared": true
  }
}
     */
}
