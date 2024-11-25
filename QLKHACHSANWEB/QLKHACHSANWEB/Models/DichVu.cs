using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLKHACHSANWEB.Models
{
    public class DichVu
    {
        [Key]   
        public int DichVuId { get; set; }
        public string DichVuName { get; set; }
        public decimal Gia {  get; set; }
       
    }

}