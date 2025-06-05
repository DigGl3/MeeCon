using MeeCon.Domain.Enum;
using MeeCon.Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using MeeCon.Domain.Model.Home;
using System.Web;

namespace MeeCon.BusinessLogic.Core
{
    public class UserApi
    {
        //=================================AUTH======================
        public UserLoginResult RegisterUser(ULoginData data)
        {
            try
            {
                using (var context = new DataContext())
                {
                    if (context.Users.Any(u => u.Username == data.Credential))
                    {
                        return new UserLoginResult
                        {
                            Status = false,
                            StatusMsg = "Username already exists",
                            Success = false
                        };
                    }

                    var newUser = new UDbModel
                    {
                        Username = data.Credential,
                        Password = data.Password,
                        CreatedAt = DateTime.UtcNow,
                        Email = data.Email,
                        ProfileImage = "~/wwwroot/images/avatar/user.png",
                        Role = AppRoles.User // Set default role for new users
                    };

                    context.Users.Add(newUser);
                    
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);
                        var fullErrorMessage = string.Join("; ", errorMessages);
                        return new UserLoginResult
                        {
                            Status = false,
                            StatusMsg = "Validation error: " + fullErrorMessage,
                            Success = false
                        };
                    }
                    catch (Exception ex)
                    {
                        return new UserLoginResult
                        {
                            Status = false,
                            StatusMsg = "Database error: " + ex.Message,
                            Success = false
                        };
                    }

                    return new UserLoginResult
                    {
                        Status = true,
                        StatusMsg = "Registration successful",
                        Success = true,
                        UserId = newUser.UserId,
                        FullName = data.FullName
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResult
                {
                    Status = false,
                    StatusMsg = "Registration failed: " + ex.Message + " Stack trace: " + ex.StackTrace,
                    Success = false
                };
            }
        }

        public UserLoginResult LoginUser(ULoginData data)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var user = context.Users.FirstOrDefault(u => u.Username == data.Credential);
                    if (user == null)
                    {
                        return new UserLoginResult
                        {
                            Status = false,
                            StatusMsg = "Invalid username or password",
                            Success = false
                        };
                    }

                    if (user.Password != data.Password)
                    {
                        return new UserLoginResult
                        {
                            Status = false,
                            StatusMsg = "Invalid username or password",
                            Success = false
                        };
                    }

                    // Store user role in session
                    HttpContext.Current.Session["UserRole"] = user.Role;

                    return new UserLoginResult
                    {
                        Status = true,
                        StatusMsg = "Login successful",
                        Success = true,
                        UserId = user.UserId,
                        FullName = user.Username
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserLoginResult
                {
                    Status = false,
                    StatusMsg = "Login failed: " + ex.Message,
                    Success = false
                };
            }
        }
    }
}
