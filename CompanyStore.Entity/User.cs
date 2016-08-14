using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Entity
{
    public class User : IEntityBase
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string HashedPassword { get; set; } 
        public string Salt { get; set; } 
        public bool IsLocked { get; set; }
        public string Image { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
