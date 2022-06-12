using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Entity
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatorUserId { get; set; }
    }
}
