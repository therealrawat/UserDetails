using System;
using System.Collections.Generic;

namespace userForm.Models
{
    public partial class CityTable
    {
        public CityTable()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int StateId { get; set; }

        public virtual StateTable State { get; set; } = null!;
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
