using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pj_banco_quest.Data;
using pj_banco_quest.Models;
using pj_banco_quest.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao cont�iner.
builder.Services.AddControllersWithViews();

// Adicione servi�os de p�ginas Razor (necess�rio para registrar todos os servi�os necess�rios)
builder.Services.AddRazorPages();

// Configurar o banco de dados
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 32))));

// Configurar servi�os de identidade
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ContextDb>()
.AddDefaultTokenProviders();

// Configurar autentica��o JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

// Registrar o servi�o de limpeza de simulados
//builder.Services.AddHostedService<SimuladoCleanupService>();

var app = builder.Build();

// Chame o m�todo para criar roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await Roles_DB.RoleInitializer.CreateRolesAsync(services);
    }
    catch (Exception ex)
    {
        // Trate as exce��es de acordo
        Console.WriteLine($"Erro ao criar roles: {ex.Message}");
    }
}

// Configure o pipeline de solicita��o HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication(); // Adiciona o middleware de autentica��o
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
