namespace HungryWeb.Models3
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StoreContext : DbContext
    {
        public StoreContext()
            : base("name=StoreContext3")
        {
        }

        public virtual DbSet<Alimentos> Alimentos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Comensales> Comensales { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<FoodImageMapping> FoodImageMapping { get; set; }
        public virtual DbSet<FoodImages> FoodImages { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Ordenes> Ordenes { get; set; }
        public virtual DbSet<Tipos> Tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alimentos>()
                .Property(e => e.Precio)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Alimentos>()
                .HasMany(e => e.Menu)
                .WithOptional(e => e.sopa)
                .HasForeignKey(e => e.bebidaID);

            modelBuilder.Entity<Alimentos>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.platoFuerte)
                .HasForeignKey(e => e.bocadilloID);

            modelBuilder.Entity<Alimentos>()
                .HasMany(e => e.Menu2)
                .WithOptional(e => e.bebida)
                .HasForeignKey(e => e.complementoID);

            modelBuilder.Entity<Alimentos>()
                .HasMany(e => e.Menu3)
                .WithOptional(e => e.postre)
                .HasForeignKey(e => e.platoFuerteID);

            modelBuilder.Entity<Alimentos>()
                .HasMany(e => e.Menu4)
                .WithOptional(e => e.complemento)
                .HasForeignKey(e => e.postreID);

            modelBuilder.Entity<Alimentos>()
                .HasMany(e => e.Menu5)
                .WithOptional(e => e.bocadillo)
                .HasForeignKey(e => e.sopaID);

            modelBuilder.Entity<Categorias>()
                .HasMany(e => e.Alimentos)
                .WithRequired(e => e.Categorias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comensales>()
                .HasMany(e => e.Ordenes)
                .WithRequired(e => e.Comensales)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Ordenes)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodImages>()
                .HasMany(e => e.FoodImageMapping)
                .WithOptional(e => e.FoodImages)
                .HasForeignKey(e => e.AlimentosImageID);

            modelBuilder.Entity<Tipos>()
                .HasMany(e => e.Alimentos)
                .WithRequired(e => e.Tipos)
                .WillCascadeOnDelete(false);
        }
    }
}
