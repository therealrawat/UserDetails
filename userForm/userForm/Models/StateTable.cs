using System;
using System.Collections.Generic;

namespace userForm.Models
{
    public partial class StateTable
    {
        public StateTable()
        {
            CityTables = new HashSet<CityTable>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }

        public virtual CountryTable Country { get; set; } = null!;
        public virtual ICollection<CityTable> CityTables { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
