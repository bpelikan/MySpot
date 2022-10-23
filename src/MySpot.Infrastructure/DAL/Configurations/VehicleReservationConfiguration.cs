using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.DAL.Configurations
{
    internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
    {
        public void Configure(EntityTypeBuilder<VehicleReservation> builder)
        {
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
            builder.Property(x => x.UserId)
                .IsRequired()
                .HasConversion(x => x.Value, x => new UserId(x));
            builder.Property(x => x.EmployeeName)
                .HasConversion(x => x.Value, x => new EmployeeName(x));
            builder.Property(x => x.LicensePlate)
                .HasConversion(x => x.Value, x => new LicensePlate(x));
        }
    }
}
