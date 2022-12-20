using BaseAndApiDocker.Config;
using Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
DatabaseConfig.Setup(builder.Services, builder.Configuration);
ServiceRegistration.Setup(builder.Services, builder.Configuration);

var app = builder.Build();

builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"\temp-keys\"))
.UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
{
    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
