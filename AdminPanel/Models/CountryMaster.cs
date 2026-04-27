using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models
{
    public  class CountryMaster
    {
        [Key]
        public int CountryId { get; set; }
        [Required]  
        public string CountryName { get; set; }
        public string CountryType { get;set; }
        public bool IsActive { get; set; }

    }
}
