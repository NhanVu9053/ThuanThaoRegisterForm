using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinhThanhPho.Models
{
    [Table("ward")]
    public class Ward : Entity
    {
        public int _district_id { get; set; }
    }
}
