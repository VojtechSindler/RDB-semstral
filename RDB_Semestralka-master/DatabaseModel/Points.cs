//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Points
    {
        public Points()
        {
            this.Measurement = new HashSet<Measurement>();
        }
    
        public int pointID { get; set; }
        public string description { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double value1 { get; set; }
        public double value2 { get; set; }
        public double accuracy { get; set; }
        public int variableID { get; set; }
    
        public virtual ICollection<Measurement> Measurement { get; set; }
        public virtual Variables Variables { get; set; }
    }
}
