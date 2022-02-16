using _0effort_crm_api.Auth;
using _0effort_crm_api.Contracts;
using _0effort_crm_api.Contracts.DTO;
using _0effort_crm_api.Core;
using _0effort_crm_api.Mongo.Validators;
using _0effort_crm_api.Services;
using FluentValidation;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton<IDataService, DataService>();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoConnection"));
builder.Services.AddSingleton<MongoContext>();

// Validator singleton services
builder.Services.AddSingleton<IValidator<CreateOrUpdateCustomerDto>, CreateOrUpdateCustomerDtoValidator>();
builder.Services.AddSingleton<IValidator<CreateOrUpdateUserDto>, CreateOrUpdateUserDtoValidator>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// uncomment once we move to production
//app.UseHttpsRedirection();



app.UseCors("corsapp");

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

IdentityModelEventSource.ShowPII = true;

app.MapControllers();


app.Run();
