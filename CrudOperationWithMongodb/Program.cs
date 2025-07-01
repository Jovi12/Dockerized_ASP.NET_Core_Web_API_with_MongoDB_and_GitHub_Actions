using CrudOperationWithMongodb.DataAccessLayer;
using CrudOperationWithMongodb.MongodbConfiguration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(option =>
{
    var connectionstring = builder.Configuration["ConnectionStrings:defaultConnection"];
    return new MongoClient(connectionstring);
});
builder.Services.AddSingleton<MongodbService>();
builder.Services.AddScoped<ICrudoperations, Crudoperations>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
