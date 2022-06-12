using CAFEMENUPROJECT.DATA.Entity;
using CAFEMENUPROJECT.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.DataAccess
{
    public class UserDataAccess
    {
        public static List<User> GetList()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static User GetById(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(User model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = UserDataAccess.GetList().Where(i => i.Username == model.Username).FirstOrDefault();
                    if (data != null)
                    {
                        return new ResponseMessage
                        {
                            Status = false,
                            Message = "Zaten Bu Kullanıcı Adı Var Lütfen Yeni Bir Kullanıcı Adı Giriniz..."
                        };
                    }
                    model.HashPassword = Helper.HashHelper.Hash(model.HashPassword);
                    model.SaltPassword = Helper.HashHelper.Salt(model.HashPassword);

                    db.Users.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Kullanıcı Başarıyla Eklendi..."
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage
                {
                    Status = false,
                    Message = "Bir hata oluştu.",
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Delete(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Find(id);
                    db.Users.Remove(data);

                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Kullanıcı Başarıyla Silindi..."
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage
                {
                    Status = false,
                    Message = "Bir hata oluştu.",
                    Code = e.StackTrace
                };
            }

        }

        public static ResponseMessage Update(User model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Users.Find(model.Id);

                    data.Name = model.Name;
                    data.Surname = model.Surname;
                    data.Username = model.Username;
                    //data.hashpasseord = model.hashpass
                    data.HashPassword = Helper.HashHelper.Hash(model.HashPassword);
                    //data.saltpass = data.hashpass because of model.hash is not hashing password
                    data.SaltPassword = Helper.HashHelper.Salt(data.HashPassword);

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Kategori Başarıyla Güncellendi..."
                    };
                }

            }
            catch (Exception e)
            {
                return new ResponseMessage
                {
                    Status = false,
                    Message = "Bir hata oluştu.",
                    Code = e.StackTrace
                };
            }
        }

        public static User Login(string username, string password)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var user = db.Users.Where(x => x.Username == username).FirstOrDefault();

                    var data = Helper.HashHelper.Verify(password, user.HashPassword, user.SaltPassword);

                    if (data)
                    {
                        return user;
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}
