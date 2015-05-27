﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Machines> Machines { get; set; }
        public virtual DbSet<Points> Points { get; set; }
        public virtual DbSet<Variables> Variables { get; set; }
        public virtual DbSet<Measurement> Measurement { get; set; }
        public virtual DbSet<Multiple_data_select> Multiple_data_select { get; set; }
    
        public virtual int savefile(string filePath)
        {
            var filePathParameter = filePath != null ?
                new ObjectParameter("filePath", filePath) :
                new ObjectParameter("filePath", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("savefile", filePathParameter);
        }
    }
}
