using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjekatASP.Api.Core;
using ProjekatASP.Api.Extensions;
using ProjekatASP.Application.Email;
using ProjekatASP.Application.Loggers;
using ProjekatASP.Application.UseCases;
using ProjekatASP.Application.UseCases.Commands;
using ProjekatASP.Application.UseCases.Queries;
using ProjekatASP.DataAccess;
using ProjekatASP.Domain;
using ProjekatASP.Implementation;
using ProjekatASP.Implementation.Email;
using ProjekatASP.Implementation.Loggers;
using ProjekatASP.Implementation.UseCases.Commands;
using ProjekatASP.Implementation.UseCases.Queries;
using ProjekatASP.Implementation.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var settings = new AppSettings();

builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);
builder.Services.AddApplicationUser();
builder.Services.AddJwt(settings);
builder.Services.AddProjekatAspDbContext();

#region Use Cases

builder.Services.AddTransient<IGetCategoryQuery, GetCategoryQueryEf>();
builder.Services.AddTransient<ICreateCategoryCommand, CreateCategoryCommandEf>();
builder.Services.AddTransient<ICreateUserCommand, CreateUserCommandEf>();
builder.Services.AddTransient<IGetUserQuery, GetUserQueryEf>();
builder.Services.AddTransient<IUseCaseLogger, UseCaseLogger>();
builder.Services.AddTransient<IGetProductQuery, GetProductsQueryEf>();
builder.Services.AddTransient<IUpdateUserInfoCommand, UpdateUserCommandEf>();
builder.Services.AddTransient<IGetSingleProductQuery, GetSingleProductQueryEf>();
builder.Services.AddTransient<IGetCartQuery, GetSingleCartQueryEf>();
builder.Services.AddTransient<ICreateCartItemCommand, CreateCartItemCommandEf>();
builder.Services.AddTransient<IUpdateCartItemCommand, UpdateCartItemCommandEf>();
builder.Services.AddTransient<IDeleteCartItemCommand, DeleteCartItemCommandEf>();
builder.Services.AddTransient<ICreateProductCommand, CreateProductCommandEf>();
builder.Services.AddTransient<IDeleteProductCommand, DeleteProductCommandEf>();
builder.Services.AddTransient<IGetSingleReceiptQuery, GetSingleReceiptQueryEf>();
builder.Services.AddTransient<IConfirmOrderCommand, ConfirmOrderCommandEf>();
builder.Services.AddTransient<IGetReceiptsQuery, GetReceiptsQueryEf>();
builder.Services.AddTransient<IDeleteReceiptCommand, DeleteReceiptCommandEf>();
builder.Services.AddTransient<IFillDatabaseCommand, FillDatabaseCommandEf>();
builder.Services.AddTransient<IAddProductPriceCommand, AddProductPriceCommandEf>();

#endregion


builder.Services.AddTransient<IEmail>(x =>
new SmtpEmailSender(settings.EmailOptions.FromEmail,
                    settings.EmailOptions.Password,
                    settings.EmailOptions.Port,
                    settings.EmailOptions.Host));

#region Validators

builder.Services.AddTransient<CreateCategoryValidator>();
builder.Services.AddTransient<CreateUserValidator>();
builder.Services.AddTransient<UpdateUserInfoValidator>();
builder.Services.AddTransient<CreateCartItemValidator>();
builder.Services.AddTransient<UpdateCartItemValidator>();
builder.Services.AddTransient<PictureDtoValidator>();
builder.Services.AddTransient<CreateProductValidator>();
builder.Services.AddTransient<AddPriceValidator>();

#endregion

builder.Services.AddTransient<UseCaseHandler>();
builder.Services.AddTransient<IExceptionLogger, ConsoleLogger>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token with Bearer format like Bearer[space] token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{ }
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Web shop API",
        Version = "v1",
        Description = "An API to perform shopping operations",
        Contact = new OpenApiContact
        {
            Name = "Nenad Jevtic",
            Email = "nenad.jevtic.60.20@ict.edu.rs",
            //Url = new Uri("https://twitter.com/jwalkner"),
        },
        //License = new OpenApiLicense
        //{
        //    Name = "Employee API LICX",
        //    Url = new Uri("https://example.com/license"),
        //}
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
