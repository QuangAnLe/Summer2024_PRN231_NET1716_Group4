using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaRepository.Repo;
using MilkTeaServices.IServices;
using MilkTeaServices.Services;
using MilkTeaStore.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapper
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

builder.Services.AddScoped<ITeaRepo, TeaRepo>();
builder.Services.AddScoped<ITeaServices, TeaServices>();
builder.Services.AddScoped<IMaterialRepo, MaterialRepo>();
builder.Services.AddScoped<IMaterialServices, MaterialServices>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserServices, UserServices>();

//Odata
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Tea>("Tea");
modelBuilder.EntitySet<Material>("Material");
modelBuilder.EntitySet<User>("User");
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        routePrefix: "odata",
        model: modelBuilder.GetEdmModel()));




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
