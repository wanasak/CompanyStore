﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Entity
{
    public class Employee : IEntityBase
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid UniqueKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
}
