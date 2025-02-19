var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrando a classe Conexao
builder.Services.AddSingleton<Conexao>();

//  Alterado para suportar MVC com Views
builder.Services.AddControllersWithViews(); 

var app = builder.Build();

// Habilitar o Swagger apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = "swagger"; // Agora o Swagger abre em /swagger
    });
}

// Adicionado suporte a arquivos estáticos (CSS, JS, imagens)
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Adicionado para permitir o MVC renderizar Views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
