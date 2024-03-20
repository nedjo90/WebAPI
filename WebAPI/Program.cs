var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

var app = builder.Build();


//app.UseHttpsRedirection();

//map the request to the controllers 
app.MapControllers();

app.Run();
