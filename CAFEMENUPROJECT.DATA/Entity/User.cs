using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }
    }
}
