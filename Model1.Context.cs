﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFCLEAN
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataBaseEntities : DbContext
    {
        public DataBaseEntities()
            : base("name=DataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArhiviraniPosao> ArhiviraniPosaos { get; set; }
        public virtual DbSet<Nalog> Nalogs { get; set; }
        public virtual DbSet<Poruka> Porukas { get; set; }
        public virtual DbSet<MoguciPosao> MoguciPosaos { get; set; }
    }
}
