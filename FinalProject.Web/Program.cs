using FinalProject.Repository;
using FinalProject.Services.Implementations.Authentication;
using FinalProject.Services.Interfaces.Authentication;
using FinalProject.Repository.Interfaces.User;
using FinalProject.Repository.Implementations.User;
using FinalProject.Repository.Interfaces.BankAccount;
using FinalProject.Repository.Implementations.BankAccount;
using FinalProject.Services.Interfaces.BankAccount;
using FinalProject.Services.Implementations.BankAccount;
using FinalProject.Repository.Interfaces.UserToAccount;
using FinalProject.Repository.Implementations.UserToAccount;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserToAccountRepository, UserToAccountRepository>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();

ConnectionFactory.SetConnectionString(
    builder.Configuration.GetConnectionString("DefaultConnection"));

// Add services to the container.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
