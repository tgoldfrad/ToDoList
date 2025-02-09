using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ToDoDB");

builder.Services.AddDbContext<ToDoDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


 builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

        builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
        app.UseCors("MyPolicy");  


// מיפוי Route לשליפת כל המשימות
app.MapGet("/items", async (ToDoDbContext db) => await db.Items.ToListAsync());

// מיפוי Route להוספת משימה חדשה
app.MapPost("/items", async (Item item, ToDoDbContext db) => {
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{item.Id}", item);
});

// מיפוי Route לעדכון משימה
app.MapPut("/items/{id}", async (int id, Item updatedItem, ToDoDbContext db) => {
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();
    if(updatedItem.Name!=null)
        item.Name = updatedItem.Name;
    item.IsComplete = updatedItem.IsComplete;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// מיפוי Route למחיקת משימה
app.MapDelete("/items/{id}", async (int id, ToDoDbContext db) => {
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();
    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/", () =>  "Server API is running");

app.Run();











