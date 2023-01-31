using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_productos.Models;

public partial class DbproductosContext : DbContext
{
    public DbproductosContext()
    {
    }

    public DbproductosContext(DbContextOptions<DbproductosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Familia> Familia { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
           #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS; database=DBPRODUCTOS; integrated security=true; Encrypt=false;");
        }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Familia>(entity =>
        {
            entity.HasKey(e => e.IdFamilia);

            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca);

            entity.ToTable("Marca");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodigoProducto);

            entity.Property(e => e.CodigoProducto).HasMaxLength(50);
            entity.Property(e => e.DescripcionProducto).HasMaxLength(50);
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.PrecioCosto).HasColumnType("decimal(2, 0)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(2, 0)");

            entity.HasOne(d => d.IdFamiliaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdFamilia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Familia");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Marca");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
