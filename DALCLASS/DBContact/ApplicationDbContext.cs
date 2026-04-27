using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DALCLASS.DBContact
{
    public class ApplicationDbContext1: DbContext
    {
        public ApplicationDbContext1(DbContextOptions<ApplicationDbContext1> options) : base(options)
        {
            
        }

        public DbSet<CountryMaster> CountryMaster { get; set; }   



    }
}
