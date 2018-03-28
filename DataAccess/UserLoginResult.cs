using DataAccess.Entity.Entities;
using System.Linq;

namespace DataAccess
{
    public enum ULResult
    {
        Valid,
        NoUserCode,
        IncorrectPassword,
        EmpNotFound
    }

    public class UserLoginResult
    {
        public int UserID { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
        public int EmployeeID { get; set; }
        public ULResult ulResult { get; set; }

        public bool LoginUser(string userCode, string password)
        {
            UserCode = userCode;
            Password = password;

            using (RetailDbContext retailDbContext = new RetailDbContext())
            {
                User user = retailDbContext.Users.Where(x => x.UserCode == userCode).FirstOrDefault();
                if (user == null)
                {
                    ulResult = ULResult.NoUserCode;
                    return false;
                }
                else
                {
                    UserID = user.UserID;
                    if (user.Password != password)
                    {
                        ulResult = ULResult.IncorrectPassword;
                        return false;
                    }
                    else
                    {
                        Employee emp = retailDbContext.Employees.Where(x => x.UserID == UserID).FirstOrDefault();
                        if (emp == null)
                        {
                            ulResult = ULResult.EmpNotFound;
                            return false;
                        }
                        else
                        {
                            EmployeeID = emp.EmployeeID;
                            return true;
                        }
                    }
                }
            }
        }
    }

}
