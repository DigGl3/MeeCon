using MeeCon.Domain.Enum;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeeCon.BusinessLogic.Core
{
    public class UserApi
    {
        //=================================AUTH======================
        public void RegisterUser(ULoginData data)
        {
           
                using (var context = new DataContext())
                {
                    if (context.Users.Any(u => u.Username == data.Credential))
                    {
                        throw new Exception("User already exists");
                    }

                    var newUser = new User
                    {
                        Username = data.Credential,
                        Password = data.Password,

                        CreatedAt = DateTime.UtcNow,
                        FullName = data.FullName
                    };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
            
        }
    }
}
