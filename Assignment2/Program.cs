using Assignment2.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Accesses");
    options.Conventions.AuthorizeFolder("/Departments");
    options.Conventions.AuthorizeFolder("/GrantAccesses");
    options.Conventions.AuthorizeFolder("/Users");
});
builder.Services.AddDbContext<AssignmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthentication(CookieAuthenticationDefaults
 .AuthenticationScheme)
   .AddCookie(options =>
   {
       options.LoginPath = "/login";
   });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BusinessUserOnly", policy => policy.RequireClaim("BusinessUser"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AssignmentContext>();
    context.Database.EnsureCreated();
     DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
