﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dneEntity : DbContext
    {
        public dneEntity()
            : base("name=dneEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ect_Pais> Ect_Pais { get; set; }
        public virtual DbSet<Log_Bairro> Log_Bairro { get; set; }
        public virtual DbSet<Log_CPC> Log_CPC { get; set; }
        public virtual DbSet<Log_Faixa_Bairro> Log_Faixa_Bairro { get; set; }
        public virtual DbSet<Log_Faixa_CPC> Log_Faixa_CPC { get; set; }
        public virtual DbSet<Log_Faixa_Localidade> Log_Faixa_Localidade { get; set; }
        public virtual DbSet<Log_Faixa_UF> Log_Faixa_UF { get; set; }
        public virtual DbSet<Log_Faixa_Uop> Log_Faixa_Uop { get; set; }
        public virtual DbSet<Log_Grande_Usuario> Log_Grande_Usuario { get; set; }
        public virtual DbSet<Log_Localidade> Log_Localidade { get; set; }
        public virtual DbSet<Log_Logradouro> Log_Logradouro { get; set; }
        public virtual DbSet<Log_Num_Sec> Log_Num_Sec { get; set; }
        public virtual DbSet<Log_Unid_Oper> Log_Unid_Oper { get; set; }
        public virtual DbSet<Log_Var_Bai> Log_Var_Bai { get; set; }
        public virtual DbSet<Log_Var_Loc> Log_Var_Loc { get; set; }
        public virtual DbSet<Log_Var_Log> Log_Var_Log { get; set; }
        public virtual DbSet<vw_BuscarEndereco> vw_BuscarEndereco { get; set; }
    }
}
