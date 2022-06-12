using CAFEMENUPROJECT.DATA.Entity;
using CAFEMENUPROJECT.DATA.Helper;
using CAFEMENUPROJECT.DATA.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.DataAccess
{
    public class CategoryDataAccess
    {
        public static List<Category> GetList()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Categories.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static Category GetById(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Categories.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static ResponseMessage Add(Category model,SessionDto user)
        {
            try
            {
                using (var db = new DataContext())
                {
                    model.CreatedDate = DateTime.Now;
                    model.IsDeleted = false;
                    model.CreatorUserId = user.Id;
                    db.Categories.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Kategori Başarıyla Eklendi..."
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
                    var data = db.Categories.Find(id);

                    data.IsDeleted = true;

                    var categories = db.Categories.ToList();

                    foreach (var item in categories)
                    {
                        if (item.ParentCategoryId == id)
                        {
                            item.ParentCategoryId = 0;
                        }
                    }

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Kategori Başarıyla Silindi..."
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

        public static ResponseMessage Update(Category model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Categories.Find(model.Id);

                    data.Name = model.Name;
                    data.ParentCategoryId = model.ParentCategoryId;
                    data.IsDeleted = data.IsDeleted;
                    data.CreatedDate = data.CreatedDate;
                    data.CreatorUserId = data.CreatorUserId;

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

    }
}
