
using AspNetCore.Scalar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRavenDb();
builder.Services.AddAutoMapper();
builder.Services.AddAutoMapper();
builder.Services.AddDomainServices();
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddValidators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseScalar(options =>
    {
        options.UseTheme(Theme.Default);
        options.RoutePrefix = "api-docs";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();