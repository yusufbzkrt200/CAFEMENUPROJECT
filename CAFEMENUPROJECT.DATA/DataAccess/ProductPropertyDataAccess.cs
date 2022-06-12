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
    public class ProductPropertyDataAccess
    {
        public static List<ProductProperty> GetList(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.ProductProperties.Where(i=>i.ProductId == id).ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static List<ProductProperty> GetListAll()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.ProductProperties.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ProductProperty GetById(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.ProductProperties.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(ProductProperty model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    db.ProductProperties.Add(model);

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
                    var data = db.ProductProperties.Find(id);
                    db.ProductProperties.Remove(data);

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

        

     
    }
}
