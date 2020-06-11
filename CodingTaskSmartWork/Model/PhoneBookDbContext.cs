using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTaskSmartWork.Model
{
    public class PhoneBookDbContext : DbContext //This will be used for Database implementation
    {
     
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options)
          : base(options)
        {
        
        }
        public virtual DbSet<PhoneBook> PhoneBook { get; set; }
        public virtual DbSet<PhoneType> PhoneType { get; set; }
    }
}
