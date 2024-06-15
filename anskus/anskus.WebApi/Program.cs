using anskus.Application.DependencyInjection;
using anskus.Infrestructure.DependencyInjection;
using anskus.WebApi.EndPoints;
using anskus.WebApi.ExtensionMigration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ApplicationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.InfrestructureService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigration();
}
app.UseCors("AllowAll");
app.MapCuestionariosEndPoints();
app.MapAccountEndPoints();
app.UseHttpsRedirection();
app.Run();

