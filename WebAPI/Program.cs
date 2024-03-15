WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


WebApplication app = builder.Build();


app.UseHttpsRedirection();



// Routing

// "/shirts"

//si notre api recoit une requete de type get sur le chemin (endpoint) /shirts il retourne la fonction :

app.MapGet("/shirts", () => "Reading all the shirts");

// si notre api recoit une requete de type get sur le chemin (endpoint) /shirts/{id} il retourne la fonction: 

app.MapGet("shirts/{id}", (int id) => $"Reading shirt with ID: {id}");

// si notre api recoit une requete Post sur le chemin (endpoint) /shirts il retourne la fonction:
app.MapPost("/shirts", () => "Creating a shirt.");

// si notre api recoit une requete Put sur le chemin (endpoint) /shirts il retourne la fonction:
app.MapPut("/shirts/{id}", (int id) => $"Updating shirt with ID: {id}");

//si notre api recoit une requete Delete sur le chemin (endpoint) /shirts/{id} il retourne la fonction:
app.MapDelete("/shirts/{id}", (int id) => $"Deleting shirt with ID: {id}");



app.Run();
