using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Api_Factura.Models
{
    public partial class BD_facturaContext : DbContext
    {
        public BD_facturaContext()
        {
        }

        public BD_facturaContext(DbContextOptions<BD_facturaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<FaacturaProducto> FaacturaProductos { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database= BD_factura;user=sa;password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Identificacion)
                    .HasName("PK__Cliente__D6F931E4610ACE72");

                entity.ToTable("Cliente");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Tipoidentificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FaacturaProducto>(entity =>
            {
                entity.ToTable("FaacturaProducto");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.FaacturaProductos)
                    .HasForeignKey(d => d.CodigoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Producto");

                entity.HasOne(d => d.NumeroFacturaNavigation)
                    .WithMany(p => p.FaacturaProductos)
                    .HasForeignKey(d => d.NumeroFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Factura");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.NumeroFactura)
                    .HasName("PK__Factura__CF12F9A712CEDFEC");

                entity.ToTable("Factura");

                entity.Property(e => e.NumeroFactura)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificacionCliente)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TotalFactura).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdentificacionClienteNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdentificacionCliente)
                    .HasConstraintName("fk_Cliente");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Producto__06370DADF6EE69B4");

                entity.ToTable("Producto");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ValorUnitario).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
