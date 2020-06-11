using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.Model
{
    [Serializable()]
    public class PhoneBook
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Nullable<int> TypeID { get; set; }
        [Phone]//Phone Number Validation needs to be implemented
        [Required]
        public string PhoneNumber { get; set; }

        public virtual PhoneType PhoneType { get; set; }

    }
}
