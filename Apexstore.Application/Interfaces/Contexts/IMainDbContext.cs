//using ApexStore.Domain.Entities.User;
using ApexStore.Domain.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexStore.Domain.Entities.Carts;
using ApexStore.Domain.Entities.Finances;
using ApexStore.Domain.Entities.HomePages;
using ApexStore.Domain.Entities.Orders;
using ApexStore.Domain.Entities.Products;
using ApexStore.Domain.Entities.User;


namespace ApexStore.Application.Interface.Contexts
{
    public interface IMainDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImages> ProductImages { get; set; }
        DbSet<ProductFeatures> ProductFeatures { get; set; }
        DbSet<Slider> Sliders { get; set; }
        DbSet<HomePageImages> HomePageImages { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<RequestPay> RequestPays { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }

        int SaveChanges(bool acceptAllChangesonSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesonSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
