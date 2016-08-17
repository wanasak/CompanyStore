using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Web.Models
{
    public class EmployeeViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "First Name is required."), MaxLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required."), MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required."), MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        
        public Guid UniqueKey { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }
}
