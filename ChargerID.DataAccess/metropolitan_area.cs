//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChargerID.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class metropolitan_area
    {
        public metropolitan_area()
        {
            this.locations = new HashSet<location>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
    
        public virtual ICollection<location> locations { get; set; }
    }
}
