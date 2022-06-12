using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEMENUPROJECT.DATA.Model
{
    public class SessionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }
    }
}
