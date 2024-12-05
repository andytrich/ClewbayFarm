using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClewbayFarmAPI.Models;

public partial class ClewbayFarmContext : DbContext
{
    public ClewbayFarmContext()
    {
    }

    public ClewbayFarmContext(DbContextOptions<ClewbayFarmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bed> Beds { get; set; }

    public virtual DbSet<BedCrop> BedCrops { get; set; }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<BlockType> BlockTypes { get; set; }

    public virtual DbSet<Cover> Covers { get; set; }

    public virtual DbSet<Crop> Crops { get; set; }

    public virtual DbSet<CropBedAttribute> CropBedAttributes { get; set; }

    public virtual DbSet<CropPropagationAttribute> CropPropagationAttributes { get; set; }

    public virtual DbSet<ModuleTray> ModuleTrays { get; set; }

    public virtual DbSet<ModuleTrayType> ModuleTrayTypes { get; set; }

    public virtual DbSet<PropagationArea> PropagationAreas { get; set; }

    public virtual DbSet<PropagationTunnel> PropagationTunnels { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bed>(entity =>
        {
            entity.HasKey(e => e.BedId);//.HasName("PK__Beds__A8A7104048B9B9C8");

            entity.HasOne(d => d.Block).WithMany(p => p.Beds)
                .HasForeignKey(d => d.BlockId);
                //.HasConstraintName("FK__Beds__BlockId__403A8C7D");
        });

        modelBuilder.Entity<BedCrop>(entity =>
        {
            entity.HasKey(e => e.BedCropId);//.HasName("PK__BedCrops__3F4EE40282532EF8");

            entity.HasOne(d => d.Bed).WithMany(p => p.BedCrops)
                .HasForeignKey(d => d.BedId);
            //.HasConstraintName("FK__BedCrops__BedId__4316F928");

            entity.HasOne(d => d.Crop).WithMany(p => p.BedCrops)
                .HasForeignKey(d => d.CropId);
                //.HasConstraintName("FK__BedCrops__CropId__440B1D61");
        });

        modelBuilder.Entity<Block>(entity =>
        {
            entity.HasKey(e => e.BlockId);//.HasName("PK__Blocks__144215F1DBA2A1C5");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.BlockType).WithMany(p => p.Blocks)
                .HasForeignKey(d => d.BlockTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK__Blocks__BlockTyp__3D5E1FD2");
        });

        modelBuilder.Entity<BlockType>(entity =>
        {
            entity.HasKey(e => e.BlockTypeId);//.HasName("PK__BlockTyp__7FF4D0BE6AEEFBB3");

            entity.HasIndex(e => e.TypeName).IsUnique();

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Cover>(entity =>
        {
            entity.HasKey(e => e.CoverId);//.HasName("PK__Covers__A8E94256257ED025");

            entity.Property(e => e.CoverType).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Crop).WithMany(p => p.Covers)
                .HasForeignKey(d => d.CropId);
                //.HasConstraintName("FK__Covers__CropId__4D94879B");
        });

        modelBuilder.Entity<Crop>(entity =>
        {
            entity.HasKey(e => e.CropId);//.HasName("PK__Crops__92356115F9926D37");

            entity.Property(e => e.Type).HasMaxLength(100);
            entity.Property(e => e.Variety).HasMaxLength(100);
        });

        modelBuilder.Entity<CropBedAttribute>(entity =>
        {
            entity.HasKey(e => e.CropId);//.HasName("PK__CropBedA__92356115053FCE5D");

            entity.Property(e => e.CropId).ValueGeneratedNever();
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PlantSpacing).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RowSpacing).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Crop).WithOne(p => p.CropBedAttribute)
                        .HasForeignKey<CropBedAttribute>(d => d.CropId);
                //.HasConstraintName("FK__CropBedAt__CropI__4AB81AF0");
        });

        modelBuilder.Entity<CropPropagationAttribute>(entity =>
        {
            entity.HasKey(e => e.CropId);//.HasName("PK__CropProp__923561156C73C860");

            entity.Property(e => e.CropId).ValueGeneratedNever();
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PreferredTemperature).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Crop).WithOne(p => p.CropPropagationAttribute)
                .HasForeignKey<CropPropagationAttribute>(d => d.CropId);
                //.HasConstraintName("FK__CropPropa__CropI__47DBAE45");
        });

        modelBuilder.Entity<ModuleTray>(entity =>
        {
            entity.HasKey(e => e.TrayId);//.HasName("PK__ModuleTr__1A6BA1B1AD31C949");

            entity.ToTable(tb => tb.HasTrigger("SetPlantingWeek"));

            entity.HasOne(d => d.Area).WithMany(p => p.ModuleTrays)
                .HasForeignKey(d => d.AreaId);
            //.HasConstraintName("FK__ModuleTra__AreaI__628FA481");

            entity.HasOne(d => d.BedCrop).WithMany(p => p.ModuleTrays)
                .HasForeignKey(d => d.BedCropId);
            //.HasConstraintName("FK__ModuleTra__BedCr__656C112C");

            entity.HasOne(d => d.Crop).WithMany(p => p.ModuleTrays)
                .HasForeignKey(d => d.CropId);
            //.HasConstraintName("FK__ModuleTra__CropI__6477ECF3");

            entity.HasOne(d => d.TrayType).WithMany(p => p.ModuleTrays)
                .HasForeignKey(d => d.TrayTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
                //.HasConstraintName("FK__ModuleTra__TrayT__6383C8BA");
        });

        modelBuilder.Entity<ModuleTrayType>(entity =>
        {
            entity.HasKey(e => e.TrayTypeId);//.HasName("PK__ModuleTr__107FF80BBA397600");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PropagationArea>(entity =>
        {
            entity.HasKey(e => e.AreaId);//.HasName("PK__Propagat__70B8204884E4E711");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Tunnel).WithMany(p => p.PropagationAreas)
                .HasForeignKey(d => d.TunnelId);
                //.HasConstraintName("FK__Propagati__Tunne__52593CB8");
        });

        modelBuilder.Entity<PropagationTunnel>(entity =>
        {
            entity.HasKey(e => e.TunnelId);//.HasName("PK__Propagat__F7456158145AE1A4");

            entity.ToTable("PropagationTunnel");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
