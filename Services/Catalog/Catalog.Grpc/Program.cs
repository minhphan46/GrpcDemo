using Catalog.API.Data;
using Catalog.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<CatalogService>();

app.Run();
