using Fhi.HelseId.Web.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var authBuilder = builder.AddHelseIdWebAuthentication()
    .UseJwkKeySecretHandler()
    .Build();

var app = builder.Build();



app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);
if (authBuilder.HelseIdWebKonfigurasjon?.AuthUse ?? false)
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.UseHelseIdProtectedPaths();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
