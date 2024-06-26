using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaRepository.Repo;
using MilkTeaServices.IServices;
using MilkTeaServices.Services;
using MilkTeaStore.Mapper;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddMvc()
     .AddNewtonsoftJson(
          options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; }
      );

//Mapper
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

builder.Services.AddScoped<ITeaRepo, TeaRepo>();
builder.Services.AddScoped<ITeaServices, TeaServices>();
builder.Services.AddScoped<IMaterialRepo, MaterialRepo>();
builder.Services.AddScoped<IMaterialServices, MaterialServices>();
builder.Services.AddScoped<IDetailsMaterialRepo, DetailsMaterialRepo>();
builder.Services.AddScoped<IDetailsMaterialServices, DetailsMaterialServices>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<ITaskUserRepo, TaskUserRepo>();
builder.Services.AddScoped<ITaskUserServices, TaskUserServices>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddScoped<ICommentServices, CommentServices>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
builder.Services.AddScoped<IDistrictRepo, DistrictRepo>();
builder.Services.AddScoped<IDistrictServices, DistrictServices>();

//Odata
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Tea>("Tea");
modelBuilder.EntitySet<Material>("Material");
modelBuilder.EntitySet<User>("User");
modelBuilder.EntitySet<TaskUser>("TaskUser");
modelBuilder.EntitySet<Comment>("Comment");
modelBuilder.EntitySet<User>("District");
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        routePrefix: "odata",
        model: modelBuilder.GetEdmModel()));

//Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


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
