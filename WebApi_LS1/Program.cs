using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi_LS1.Data;
using WebApi_LS1.Formatters;
using WebApi_LS1.Repositories.Abstract;
using WebApi_LS1.Repositories.Concrete;
using WebApi_LS1.Services.Abstract;
using WebApi_LS1.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    opt.InputFormatters.Insert(0, new VCardInputFormatter());
    opt.OutputFormatters.Insert(0, new VCardOutputFormatter());
    opt.OutputFormatters.Insert(1,new TextCsvOutputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v3" });
//    // Vcard format?n? destekleyecek ?ekilde aç?klamalar? ekleyin
//    c.OperationFilter<AddVcardContentTypeOperationFilter>();
//});

builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<StudentDbContext>(opt => opt.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
