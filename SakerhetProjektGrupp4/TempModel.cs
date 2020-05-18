namespace SakerhetProjektGrupp4
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TempModel : DbContext
    {
        public TempModel()
            : base("name=TempModel")
        {
        }

        public virtual DbSet<TempUser> TempUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TempUser>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TempUser>()
                .Property(e => e.Losenord)
                .IsUnicode(false);
        }
    }
}
