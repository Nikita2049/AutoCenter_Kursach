using AutoCenterDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCenterDatabaseImplement
{
    public class KorytoDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-7825FI3\SQLEXPRESS;Initial Catalog=KorytoDatabase1;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Order> Orders { set; get; }

        public DbSet<Client> Clients { set; get; }

        public DbSet<Request> Requests { set; get; }

        public DbSet<Spare> Spares { set; get; }

        public DbSet<Car> Cars { set; get; }

        public DbSet<OrderCar> OrderCars { set; get; }

        public DbSet<CarSpare> CarSpares { set; get; }

        public DbSet<SpareRequest> SpareRequests { set; get; }
    }
}