using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace bgfadmin.Models
{

    public class FieldsMetaInfo
    {
        [Key]
        public string FieldName { get; set; }
        public string Caption { get; set; }

        public bool Mandatory { get; set; }
    }
}


