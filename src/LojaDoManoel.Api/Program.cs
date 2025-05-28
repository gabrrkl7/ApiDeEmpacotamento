using LojaDoManoel.Api.Data;
using LojaDoManoel.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurações
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmpacotadorService>();

// Configuração do DbContext (ATUALIZADA)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaDoManoelDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => 
    {
        sqlOptions.EnableRetryOnFailure();
    }));

var app = builder.Build();

// Configuração do pipeline (ATUALIZADA)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Aplicar migrações automaticamente apenas em desenvolvimento
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<LojaDoManoelDbContext>();
            context.Database.Migrate(); // Usar Migrate() em vez de EnsureCreated()
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Erro ao migrar banco de dados");
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();