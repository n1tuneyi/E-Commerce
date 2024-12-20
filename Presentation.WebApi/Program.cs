
using Infrastructure.Configuration;
using Presentation.WebApi.Extensions;
using Presentation.WebApi.MapperConfig;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.ConfigureInfrastructure();
builder.Services.ConfigureDB(builder.Configuration);
builder.Services.ConfigureLogging();
builder.Services.AddValidators();
builder.Services.AddFilters();

//builder.Services.AddControllers().

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

app.ConfigureExceptionHandler();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
