using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var constring = builder.Configuration.GetConnectionString("DB");
var con = new SqlConnection(constring);

builder.Services.AddCors(o => o.AddPolicy("AllowAll",
 P => P.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())).AddEndpointsApiExplorer().AddSwaggerGen();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
if (app.Environment.IsDevelopment())
    app.UseSwagger().UseSwaggerUI();
app.MapGet("/", () => "Welcome, Hello World! what ever.");
app.MapGet("/api/employees", () => con.Query("SELECT * FROM Employees"));
app.MapGet("/api/employees/{id}", (int id) => con.Query("SelectEmployeesByID", new { id }, commandType: CommandType.StoredProcedure));
app.Run();
