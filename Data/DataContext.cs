﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Ritzpa_Stock_Exchange.Models;
using RitzpaStockExchange.Models;
using RitzpaStockExchange.Utils;

namespace RitzpaStockExchange.Data
{
    public class DataContext : DbContext // TODO: Mayne to change all of the usage of the dbContext to a using(context) statement.
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Stock> Stocks{ get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
        public DbSet<UserAction> UserActions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasMany(stock => stock.Buys)
                .WithOne(command => command.BuyStock)
                .HasForeignKey(command => command.BuyStockName)
                .OnDelete(DeleteBehavior.ClientSetNull);


            //modelBuilder.Entity<User>()
            //    .Property(user => user.UserStocks)
            //    .HasConversion(
            //        d => JsonConvert.SerializeObject(d),
            //        s => JsonConvert.DeserializeObject<Dictionary<string, Holding>>(s));

            modelBuilder.Entity<Stock>()
                .HasMany(stock => stock.Sells)
                .WithOne(command => command.SellStock)
                .HasForeignKey(command => command.SellStockName)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Stock>()
                .HasMany(stock => stock.Trades)
                .WithOne(trade => trade.Stock)
                .HasForeignKey(trade => trade.StockName);

            modelBuilder.Entity<User>()
                .HasMany(user => user.UserStocks)
                .WithOne(share => share.User)
                .HasForeignKey(share => share.UserId);

            //modelBuilder.Entity<Holding>()
            //    .HasKey(h => h.Id);

            //modelBuilder.Entity<Command>()
            //    .HasOne(c => c.Initiator)
            //    .WithMany()
            //    .HasForeignKey(c => c.InitiatorId);



            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Stocks;Trusted_Connection=True;");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Stock>()
        //        .HasMany(s => s.Buys)
        //        .WithOne()
        //        .HasForeignKey(c => c.StockName)
        //        .OnDelete(DeleteBehavior.Restrict)
        //        .IsRequired();

        //    modelBuilder.Entity<Stock>()
        //        .HasMany(s => s.Sells)
        //        .WithOne()
        //        .HasForeignKey(c => c.StockName)
        //        .OnDelete(DeleteBehavior.Restrict)
        //        .IsRequired();

        //    modelBuilder.Entity<Stock>()
        //    .HasMany(s => s.Trades)
        //    .WithOne()
        //    .HasForeignKey(t => t.StockName)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .IsRequired();

        //}


    }
}
