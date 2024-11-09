using System.Text;
using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.Auth;
using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AppContext = EfcDataAccess.AppContext;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

AuthorizationPolicies.AddPolicies(builder.Services);

//builder.Services.AddScoped<IAuthService, AuthService>(); old log in
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IUserDao, UserEfcDao>();  
 
builder.Services.AddScoped<IUserSkillLogic, UserSkillLogic>();
builder.Services.AddScoped<IUserSkillDao, UserSkillEfcDao>();  

builder.Services.AddScoped<ISkillLogic, SkillLogic>();
builder.Services.AddScoped<ISkillDao, SkillEfcDao>();  

builder.Services.AddScoped<IProjectLogic, ProjectLogic>();
builder.Services.AddScoped<IProjectDao, ProjectEfcDao>();

builder.Services.AddScoped<ITagLogic, TagLogic>();
builder.Services.AddScoped<ITagDao, TagEfcDao>(); 

builder.Services.AddScoped<ITaskProjectLogic, TaskProjectLogic>();
builder.Services.AddScoped<ITaskProjectDao, TaskProjectEfcDao>(); 


//database 
builder.Services.AddDbContext<AppContext>();


//??
// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//     options.JsonSerializerOptions.IncludeFields = false; // Not needed unless using fields
// });


var app = builder.Build();


app.UseHttpsRedirection();
//app.UseRouting();
app.UseAuthentication();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();