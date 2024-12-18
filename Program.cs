using navio_web.Models;
using navio_web.Banco;


var builder = WebApplication.CreateBuilder(args);

// Adiciona o Swagger ao serviço
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura o Swagger na aplicação
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger e Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(); // Esta linha irá habilitar a interface do Swagger para interagir com sua API
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é 30 dias. Você pode querer alterar isso para cenários de produção.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

try
{
    var fornecedorDAL = new FornecedorDAL();
    
    var listarFornecedor = fornecedorDAL.Listar();

    foreach( var fornecedor in listarFornecedor )
    {
        Console.WriteLine(fornecedor);
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
