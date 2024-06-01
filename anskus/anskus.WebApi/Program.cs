using anskus.Application.DependencyInjection;
using anskus.Infrestructure.DependencyInjection;
using anskus.WebApi.EndPoints;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ApplicationServices();
builder.Services.InfrestructureService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapCuestionariosEndPoints();
app.UseHttpsRedirection();


app.Run();

