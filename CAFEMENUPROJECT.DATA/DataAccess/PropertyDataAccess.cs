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
    public class PropertyDataAccess
    {
        public static List<Property> GetList()
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Properties.ToList();
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static Property GetById(int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Properties.Find(id);
                    return data;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static ResponseMessage Add(Property model, int id)
        {
            try
            {
                using (var db = new DataContext())
                {
                    //List<int> IdList = new List<int>();

                    //var data = db.Properties.ToList();
                    //foreach (var item in data)
                    //{
                    //    IdList.Add(item.Id);
                    //}

                    //if (IdList.Count() == 0)
                    //{
                    //    model.Id = 1;
                    //}
                    //else
                    //{
                    //    IdList.Sort();

                    //    model.Id = IdList[0] + 1;

                    //}

                    db.Properties.Add(model);
                    db.SaveChanges();


                    db.ProductProperties.Add(new ProductProperty()
                    {
                        ProductId = id,
                        PropertyId = model.Id
                    });
                    
                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Özellik Başarıyla Kaydedildi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu...",
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
                    var productProperty = db.ProductProperties.Where(i => i.PropertyId == id).FirstOrDefault();
                    db.ProductProperties.Remove(productProperty);
                    
                    var property= db.Properties.Find(id);
                    db.Properties.Remove(property);

                    db.SaveChanges();

                    return new ResponseMessage()
                    {
                        Status = true,
                        Message = "Özellik Başarıyla Silindi"
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu...",
                    Code = e.StackTrace
                };
            }
        }

        public static ResponseMessage Update(Property model)
        {
            try
            {
                using (var db = new DataContext())
                {
                    var data = db.Properties.Find(model.Id);

                    data.Key = model.Key;
                    data.Value = model.Value;

                    db.Entry(data).State = EntityState.Modified;
                    db.SaveChanges();

                    return new ResponseMessage
                    {
                        Status = true,
                        Message = "Özellik Başarıyla Güncellendi..."
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage()
                {
                    Status = false,
                    Message = "Bir Hata Oluştu...",
                    Code = e.StackTrace
                };
            }
        }

    }
}
