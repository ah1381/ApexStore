using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApexStore.Common.Roles;
using Microsoft.EntityFrameworkCore;
using ApexStore.Persistence.Contexts;
using ApexStore.Application.Interface.Contexts;
using ApexStore.Application.Services.Users.Queries.GetUsers;
using ApexStore.Application.Services.Users.Queries.GetRoles;
using ApexStore.Application.Services.Users.Commands.RgegisterUser;
using ApexStore.Application.Services.Users.Commands.RemoveUser;
using ApexStore.Application.Services.Users.Commands.UserLogin;
using ApexStore.Application.Services.Users.Commands.UserSatusChange;
using ApexStore.Application.Services.Users.Commands.EditUser;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ApexStore.Application.Interfaces.FacadPatterns;
using ApexStore.Application.Services.Products.FacadPattern;
using ApexStore.Application.Services.Common.Queries.GetMenuItem;
using ApexStore.Application.Services.Common.Queries.GetCategory;
using ApexStore.Application.Services.HomePages.AddNewSlider;
using Microsoft.Extensions.Hosting.Internal;
using ApexStore.Application.Services.Common.Queries.GetSlider;
using ApexStore.Application.Services.HomePages.AddHomePageImages;
using ApexStore.Application.Services.Common.Queries.GetHomePageImages;
using ApexStore.Application.Services.Carts;
using ApexStore.Application.Services.Fainances.Commands.AddRequestPay;
using ApexStore.Application.Services.Fainances.Queries.GetRequestPayService;
using ApexStore.Application.Services.Orders.Commands.AddNewOrder;
using ApexStore.Application.Services.Orders.Queries.GetUserOrders;
using ApexStore.Application.Services.Orders.Queries.GetOrdersForAdmin;
using ApexStore.Application.Services.Fainances.Queries.GetRequestPayForAdmin;
using Parbad.Builder;
using Parbad.Gateway.ZarinPal;
using Parbad.Gateway.ParbadVirtual;
using Parbad.Storage.Abstractions;
using Microsoft.AspNetCore.Identity;
//using Zarinpal.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<MainDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
});

builder.Services.AddParbad()
    .ConfigureGateways(gateway =>
    {
        //add gatewaies to use
        gateway.AddZarinPal()
            .WithAccounts(accounts =>
            {
                accounts.AddInMemory(account =>
                {
                    account.MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX";
                    account.IsSandbox = true;
                });
            });
        gateway
            .AddParbadVirtual()
            .WithOptions(option =>
            {
                option.GatewayPath = "/MyVirtualGateway";
            });
    })
    .ConfigureHttpContext(httpContextBuilder =>
    {
        httpContextBuilder.UseDefaultAspNetCore();
    })
    .ConfigureStorage(StorageBuilder =>
    {
        StorageBuilder.UseMemoryCache();
    });

//builder.Services.AddZarinpal(options =>
//{
//    options.MerchantId = builder.Configuration["ZarinPal:MerchantID"];
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Authentication/Signin");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
    options.AccessDeniedPath = new PathString("/Authentication/Signin");
});




//builder.Services.AddScoped<IMainDbContext, MainDbContext>();
builder.Services.AddScoped<IGetUsersService, GetUsersService>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<IUserSatusChangeService, UserSatusChangeService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();

//FacadeInject
builder.Services.AddScoped<IProductFacad, ProductFacad>();


//------------------
builder.Services.AddScoped<IGetMenuItemService, GetMenuItemService>();
builder.Services.AddScoped<IGetCategoryService, GetCategoryService>();
builder.Services.AddScoped<IAddNewSliderService, AddNewSliderService>();
builder.Services.AddScoped<IGetSliderService, GetSliderService>();
builder.Services.AddScoped<IAddHomePageImagesService, AddHomePageImagesService>();
builder.Services.AddScoped<IGetHomePageImagesService, GetHomePageImagesService>();

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAddRequestPayService, AddRequestPayService>();
builder.Services.AddScoped<IGetRequestPayService, GetRequestPayService>();
builder.Services.AddScoped<IAddNewOrderService, AddNewOrderService>();
builder.Services.AddScoped<IGetUserOrdersService, GetUserOrdersService>();
builder.Services.AddScoped<IGetOrdersForAdminService, GetOrdersForAdminService>();
builder.Services.AddScoped<IGetRequestPayForAdminService, GetRequestPayForAdminService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseParbadVirtualGateway();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "detailsProductSlug",
    pattern: "products/Detail/{slug}")
    //pattern: "{controller=products}/{action=Detail}/{slug}")
    .WithStaticAssets();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
