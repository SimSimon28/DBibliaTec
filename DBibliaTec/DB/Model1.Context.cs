﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBibliaTec.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BIbliaEntities : DbContext
    {
        public BIbliaEntities()
            : base("name=BIbliaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ClassificIzdat> ClassificIzdats { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Formular> Formulars { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Izdatel> Izdatels { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Personal> Personals { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<VidDeitIzdat> VidDeitIzdats { get; set; }
        public virtual DbSet<VidIzdat> VidIzdats { get; set; }
    }
}
