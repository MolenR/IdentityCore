using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using LeaveManagement.Data;
using LeaveManagement.Repository.Services;
using LeaveManagement.Repository.Interfaces;
using LeaveManagement.Repository.Repositories;
using LeaveManagement.Repository.Configurations;
using Serilog;
using IdentityCore.Web.Services.Identity;

namespace LeaveManagement.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        /* Add services to the container. 
        -------------------------------*/

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        /* SET EMPLOYEE TO HAVE IDENTITYROLE 
        ----------------------------------*/
        builder.Services.AddDefaultIdentity<Employee>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;

            options.SignIn.RequireConfirmedAccount = true;

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.MaxFailedAccessAttempts = 3;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()

            /* PASSWORD VALIDATION PIPELINE*/
            .AddPasswordValidator<PasswordValidatorService>();

        /* ADD CLAIMS PRINCIPAL
        ---------------------*/
        //.AddClaimsPrincipalFactory<CustomClaimService>();

        /* ADD AUTHENTICATION
        -------------------*/
        builder.Services.AddAuthentication()
            /* ADD THIRTH PARTY LOGIN
            -----------------------*/
            .AddGoogle(googleOptions => {
                googleOptions.ClientId = builder.Configuration["Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Google:ClientSecret"]; ;
            });

        /* ADD POLICY AUTHORIZATION
        -------------------------*/
        /*builder.Services.AddAuthorization(auth =>
        {
            auth.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

            auth.AddPolicy(Policies.IsMinimumAge, policy => {
                policy.RequireClaim(EmployeeClaims.isMinimumAge, "true");
            });
        });*/

        /* ADD COOKIE AUTHENTICATION 
        --------------------------*/
        /*builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddCookie(cookie => cookie.LoginPath = " CUSTOM PATH GOES HERE ")*/

                                                  /* AUTHORIZATION GLOBAL */
        builder.Services.AddControllersWithViews()/*.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()))*/;

        /* ADDED HTTP CONTEXT 
        -------------------*/
        builder.Services.AddHttpContextAccessor();

        /* CONFIGURE AUTOMAPPER
        ---------------------*/
        builder.Services.AddAutoMapper(typeof(MapperConfig));

        /* CONFIGURE SERILOG 
        ------------------*/
        builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

        /* IMPLEMENT EMAIL SERVICES WITH PAPERCUT 
        ---------------------------------------*/
        builder.Services.AddTransient<IEmailSender>(s => new EmailSender("localhost", 25, "no-reply@leavemanagement.com"));

        /* REGISTER MANUAL MADE REPOSITORIES 
        ----------------------------------*/
        /* WHILE INJECTION SCOPED IS TRANSIENT */
        builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
        builder.Services.AddScoped<ILeaveTypeRepo, LeaveTypeRepo>();
        builder.Services.AddScoped<ILeaveAllocationRepo, LeaveAllocationRepo>();
        builder.Services.AddScoped<ILeaveRequestRepo, LeaveRequestRepo>();

        /* BUILD Builder
        ------------------------*/
        var app = builder.Build();

        /* INVOKE SERILOG 
        ------------------------*/
        app.UseSerilogRequestLogging();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}