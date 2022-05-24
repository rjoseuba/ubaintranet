using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using Intranet.Models.Kkk;

namespace Intranet.Data
{
  public partial class KkkContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public KkkContext(DbContextOptions<KkkContext> options):base(options)
    {
    }

    public KkkContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Intranet.Models.Kkk.Department>()
              .HasOne(i => i.Department1)
              .WithMany(i => i.Departments1)
              .HasForeignKey(i => i.DeptoId)
              .HasPrincipalKey(i => i.DeptoId);


        builder.Entity<Intranet.Models.Kkk.App>()
              .Property(p => p.LastUpdate)
              .HasColumnType("date");

        builder.Entity<Intranet.Models.Kkk.Department>()
              .Property(p => p.LastUpdate)
              .HasColumnType("date");

        builder.Entity<Intranet.Models.Kkk.Staff>()
              .Property(p => p.DateBirth)
              .HasColumnType("date");

        builder.Entity<Intranet.Models.Kkk.Staff>()
              .Property(p => p.CurrentYear)
              .HasColumnType("date");

        builder.Entity<Intranet.Models.Kkk.Staff>()
              .Property(p => p.DateLastPromotion)
              .HasColumnType("date");

        builder.Entity<Intranet.Models.Kkk.Staff>()
              .Property(p => p.StartDate)
              .HasColumnType("date");

        builder.Entity<Intranet.Models.Kkk.App>()
              .Property(p => p.AppId)
              .HasPrecision(10, 0);

        builder.Entity<Intranet.Models.Kkk.Department>()
              .Property(p => p.DeptoId)
              .HasPrecision(10, 0);

        builder.Entity<Intranet.Models.Kkk.Department>()
              .Property(p => p.StaffId)
              .HasPrecision(10, 0);

        builder.Entity<Intranet.Models.Kkk.Staff>()
              .Property(p => p.StaffId)
              .HasPrecision(10, 0);

        builder.Entity<Intranet.Models.Kkk.Staff>()
              .Property(p => p.Age)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<Intranet.Models.Kkk.App> Apps
    {
      get;
      set;
    }

    public DbSet<Intranet.Models.Kkk.Department> Departments
    {
      get;
      set;
    }

    public DbSet<Intranet.Models.Kkk.Staff> Staffs
    {
      get;
      set;
    }
  }
}
