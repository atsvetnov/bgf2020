using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bgfadmin.Models
{
    public class TreeAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public int OrderBy { get; set; }
        public bool Visible { get; set; }

    }
    public class Profile_TreeAdmin
    {
        public int ProfileId { get; set; }
        public int TreeId { get; set; }

    }


}
