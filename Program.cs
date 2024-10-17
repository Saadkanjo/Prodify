using api.Data;
using api.Interfaces;
using api.models;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//         options.JsonSerializerOptions.MaxDepth = 64; // Increase depth if needed
//     });


builder.Services.AddControllers().AddNewtonsoftJson(
    Options => {Options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
 option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });

 option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddDbContext<ApplicationDBContext>(Options => {
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
options.Password.RequireDigit = true;

 options.Password.RequireLowercase = true;

options.Password.RequireUppercase = true;

options.Password.RequireNonAlphanumeric = true;

options.Password.RequiredLength = 12;

})
.AddEntityFrameworkStores<ApplicationDBContext>();
builder.Services.AddAuthentication(Options => {
    Options.DefaultAuthenticateScheme =
    Options.DefaultChallengeScheme =
    Options.DefaultForbidScheme =
    Options.DefaultScheme =
    Options.DefaultSignInScheme =
    Options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(Options => {
    Options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? throw new InvalidOperationException("SigningKey is not configured"))
        )
    };
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFeedbackRepository,FeedbackRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IProtfolioRepository, ProtfolioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();


