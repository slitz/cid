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
    
    public partial class location
    {
        public location()
        {
            this.charging_station_data = new HashSet<charging_station_data>();
        }
    
        public string postal_code { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public string description { get; set; }
        public Nullable<int> metropolitan_area_id { get; set; }
    
        public virtual metropolitan_area metropolitan_area { get; set; }
        public virtual ICollection<charging_station_data> charging_station_data { get; set; }
    }
}
