using System;
using System.Collections.Generic;

namespace userForm.Models
{
    public partial class CountryTable
    {
        public CountryTable()
        {
            StateTables = new HashSet<StateTable>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<StateTable> StateTables { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
