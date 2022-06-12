using CAFEMENUPROJECT.DATA.Entity;
using CAFEMENUPROJECT.DATA.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.DataAccess
{
    public class ProductDataAccess
    {
        public static List<Product> GetList()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Products.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Product GetById(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Products.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Product model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.Products.Add(model);

                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Ürün Başarıyla Eklendi..."
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
                    var data = db.Products.Find(id);

                    data.IsDeleted = true;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Ürün Başarıyla Silindi..."
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

        public static ResponseMessage Update(Product model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Products.Find(model.Id);

                    data.Name = model.Name;
                    data.CategoryId = model.CategoryId;
                    data.Price = model.Price;
                    data.ImagePath = model.ImagePath;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Ürün Başarıyla Güncellendi..."
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
