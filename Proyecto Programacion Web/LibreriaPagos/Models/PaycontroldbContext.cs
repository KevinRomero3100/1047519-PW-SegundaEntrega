using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibreriaPagos.Models;

public partial class PaycontroldbContext : DbContext
{
    public PaycontroldbContext()
    {
    }

    public PaycontroldbContext(DbContextOptions<PaycontroldbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteDireeccion> ClienteDireeccions { get; set; }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Detallefactura> Detallefacturas { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<DireccionEmpleado> DireccionEmpleados { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Facturaempleado> Facturaempleados { get; set; }

    public virtual DbSet<Menusualidad> Menusualidads { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=10.20.1.9;userid=kromero;password=kHvilla31;database=paycontroldb;TreatTinyAsBoolean=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.Property(e => e.Dpi).HasColumnName("DPI");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.PrimerApellido).HasMaxLength(20);
            entity.Property(e => e.PrimerNombre).HasMaxLength(20);
            entity.Property(e => e.SegundoApellido).HasMaxLength(20);
            entity.Property(e => e.SegundoNombre).HasMaxLength(20);
        });

        modelBuilder.Entity<ClienteDireeccion>(entity =>
        {
            entity.HasKey(e => e.IdClienteDireeccion).HasName("PRIMARY");

            entity.ToTable("cliente-direeccion");

            entity.HasIndex(e => e.ClienteIdCliente, "fk_cliente-direeccion_cliente1_idx");

            entity.HasIndex(e => e.ColoniaIdColonia, "fk_cliente-direeccion_colonia1_idx");

            entity.HasIndex(e => e.DireccionIdDireccion, "fk_cliente-direeccion_direccion1_idx");

            entity.Property(e => e.IdClienteDireeccion).HasColumnName("idCliente-Direeccion");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("cliente_IdCliente");
            entity.Property(e => e.ColoniaIdColonia).HasColumnName("colonia_idColonia");
            entity.Property(e => e.DireccionIdDireccion).HasColumnName("direccion_idDireccion");

            entity.HasOne(d => d.ClienteIdClienteNavigation).WithMany(p => p.ClienteDireeccions)
                .HasForeignKey(d => d.ClienteIdCliente)
                .HasConstraintName("fk_cliente-direeccion_cliente1");

            entity.HasOne(d => d.ColoniaIdColoniaNavigation).WithMany(p => p.ClienteDireeccions)
                .HasForeignKey(d => d.ColoniaIdColonia)
                .HasConstraintName("fk_cliente-direeccion_colonia1");

            entity.HasOne(d => d.DireccionIdDireccionNavigation).WithMany(p => p.ClienteDireeccions)
                .HasForeignKey(d => d.DireccionIdDireccion)
                .HasConstraintName("fk_cliente-direeccion_direccion1");
        });

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PRIMARY");

            entity.ToTable("colonia");

            entity.Property(e => e.IdColonia).HasColumnName("idColonia");
            entity.Property(e => e.Departamento).HasMaxLength(30);
            entity.Property(e => e.Municipio).HasMaxLength(30);
            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<Detallefactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PRIMARY");

            entity.ToTable("detallefactura");

            entity.HasIndex(e => e.FacturaIdFactura, "fk_detallefactura_factura1_idx");

            entity.HasIndex(e => e.MenusualidadIdMenusualidad, "fk_detallefactura_menusualidad1_idx");

            entity.Property(e => e.IdDetalle).HasColumnName("idDetalle");
            entity.Property(e => e.FacturaIdFactura).HasColumnName("factura_idFactura");
            entity.Property(e => e.MenusualidadIdMenusualidad).HasColumnName("menusualidad_idMenusualidad");

            entity.HasOne(d => d.FacturaIdFacturaNavigation).WithMany(p => p.Detallefacturas)
                .HasForeignKey(d => d.FacturaIdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detallefactura_factura1");

            entity.HasOne(d => d.MenusualidadIdMenusualidadNavigation).WithMany(p => p.Detallefacturas)
                .HasForeignKey(d => d.MenusualidadIdMenusualidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detallefactura_menusualidad1");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PRIMARY");

            entity.ToTable("direccion");

            entity.Property(e => e.IdDireccion).HasColumnName("idDireccion");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Referencia).HasMaxLength(75);
        });

        modelBuilder.Entity<DireccionEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdDireccionEmpleado).HasName("PRIMARY");

            entity.ToTable("direccion-empleado");

            entity.HasIndex(e => e.DireccionIdDireccion, "fk_direccion-empleado_direccion1_idx");

            entity.HasIndex(e => e.EmpleadoIdEmpleado, "fk_direccion-empleado_empleado1_idx");

            entity.Property(e => e.IdDireccionEmpleado).HasColumnName("idDireccion-Empleado");
            entity.Property(e => e.DireccionIdDireccion).HasColumnName("direccion_idDireccion");
            entity.Property(e => e.EmpleadoIdEmpleado).HasColumnName("empleado_idEmpleado");

            entity.HasOne(d => d.DireccionIdDireccionNavigation).WithMany(p => p.DireccionEmpleados)
                .HasForeignKey(d => d.DireccionIdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_direccion-empleado_direccion1");

            entity.HasOne(d => d.EmpleadoIdEmpleadoNavigation).WithMany(p => p.DireccionEmpleados)
                .HasForeignKey(d => d.EmpleadoIdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_direccion-empleado_empleado1");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.RolIdRol, "fk_empleado_rol1_idx");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Dpi).HasColumnName("DPI");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.PrimerApellido).HasMaxLength(20);
            entity.Property(e => e.PrimerNombre).HasMaxLength(20);
            entity.Property(e => e.RolIdRol).HasColumnName("rol_idRol");
            entity.Property(e => e.SegundoApellido).HasMaxLength(20);
            entity.Property(e => e.SegundoNombre).HasMaxLength(20);

            entity.HasOne(d => d.RolIdRolNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.RolIdRol)
                .HasConstraintName("fk_empleado_rol1");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.ClienteIdCliente, "fk_factura_cliente1_idx");

            entity.HasIndex(e => e.EmpleadoIdEmpleado, "fk_factura_empleado1_idx");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.ClienteIdCliente).HasColumnName("cliente_IdCliente");
            entity.Property(e => e.EmpleadoIdEmpleado).HasColumnName("empleado_idEmpleado");
            entity.Property(e => e.FechaDeEmision).HasColumnType("date");
            entity.Property(e => e.FechaDeIngreso).HasColumnType("datetime");
            entity.Property(e => e.Iva).HasColumnName("IVA");
            entity.Property(e => e.Nit)
                .HasMaxLength(45)
                .HasColumnName("nit");

            entity.HasOne(d => d.ClienteIdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteIdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_factura_cliente1");

            entity.HasOne(d => d.EmpleadoIdEmpleadoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EmpleadoIdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_factura_empleado1");
        });

        modelBuilder.Entity<Facturaempleado>(entity =>
        {
            entity.HasKey(e => e.IdFacturaEmpleado).HasName("PRIMARY");

            entity.ToTable("facturaempleado");

            entity.HasIndex(e => e.EmpleadoIdEmpleado, "fk_facturaempleado_empleado1_idx");

            entity.HasIndex(e => e.FacturaIdFactura, "fk_facturaempleado_factura1_idx");

            entity.Property(e => e.IdFacturaEmpleado).HasColumnName("idFacturaEmpleado");
            entity.Property(e => e.EmpleadoIdEmpleado).HasColumnName("empleado_idEmpleado");
            entity.Property(e => e.FacturaIdFactura).HasColumnName("factura_idFactura");

            entity.HasOne(d => d.EmpleadoIdEmpleadoNavigation).WithMany(p => p.Facturaempleados)
                .HasForeignKey(d => d.EmpleadoIdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facturaempleado_empleado1");

            entity.HasOne(d => d.FacturaIdFacturaNavigation).WithMany(p => p.Facturaempleados)
                .HasForeignKey(d => d.FacturaIdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_facturaempleado_factura1");
        });

        modelBuilder.Entity<Menusualidad>(entity =>
        {
            entity.HasKey(e => e.IdMenusualidad).HasName("PRIMARY");

            entity.ToTable("menusualidad");

            entity.Property(e => e.IdMenusualidad).HasColumnName("idMenusualidad");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Type).HasMaxLength(45);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.EmpleadoIdEmpleado, "fk_usuario_empleado1_idx");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.EmpleadoIdEmpleado).HasColumnName("empleado_idEmpleado");
            entity.Property(e => e.NombreDeUsuario).HasMaxLength(45);
            entity.Property(e => e.Pasword).HasMaxLength(45);

            entity.HasOne(d => d.EmpleadoIdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpleadoIdEmpleado)
                .HasConstraintName("fk_usuario_empleado1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
