using CargoTrack.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.Sender) //kargonun bir tane göndereni vardır
                .WithMany(u => u.SentCargos) //gönderenin birden fazla kargosu olabilir
                .HasForeignKey(c => c.SenderId) //kargo tablosunda senderId foreign key olarak tanımlanır
                .OnDelete(DeleteBehavior.Restrict); //kullanıcı silindiğinde kargo silinmez

            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.Receiver) //kargonun bir tane alıcısı vardır
                .WithMany(u => u.ReceivedCargos) //alıcı birden fazla kargo alabilir
                .HasForeignKey(c => c.ReceiverId) //kargo tablosunda receiverId foreign key olarak tanımlanır
                .OnDelete(DeleteBehavior.Restrict); //kullanıcı silindiğinde kargo silinmez

            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.OriginBranch) //kargonun bir tane çıkış şubesi vardır
                .WithMany(b => b.OriginCargos) //çıkış şubesi birden fazla kargo gönderebilir
                .HasForeignKey(c => c.OriginBranchId) //kargo tablosunda originBranchId foreign key olarak tanımlanır
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.DestinationBranch) //kargonun bir tane varış şubesi vardır
                .WithMany(b => b.DestinationCargos) //varış şubesi birden fazla kargo alabilir
                .HasForeignKey(c => c.DestinationBranchId) //kargo tablosunda destinationBranchId foreign key olarak tanımlanır
                .OnDelete(DeleteBehavior.Restrict);




            base.OnModelCreating(modelBuilder);
        }


        public DbSet<About> Abouts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
