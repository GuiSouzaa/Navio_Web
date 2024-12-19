var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrando a classe Conexao
builder.Services.AddSingleton<Conexao>();

// Adicionar a configuração dos controladores
builder.Services.AddControllers();  // Adicionar esse método

var app = builder.Build();

// Habilitando o Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapear os controladores
app.MapControllers();  // Mapear os controladores

app.Run();
