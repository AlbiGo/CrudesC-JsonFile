using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.Model
{
    public class PhoneType
    {
        [Key]
        public Guid id { get; set; }
        public string Type { get; set; }
        //public virtual ICollection<PhoneBook> PhoneBook { get; set; }

    }
}
