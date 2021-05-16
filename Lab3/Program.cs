using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


//-----------COOKBOOK--------------

//Inserting, updating & removing data. To use exampels below, use copy+paste and update w your own data. 
var factory = new CookbookContextFactory();
using var context = factory.CreateDbContext(args);

//Insert data 
Console.WriteLine("Add omelette for breakfast");
var Omelette = new Dish { Title = "Breakfast omelette", Notes = "This is the best", Stars = 4 };
context.Dishes.Add(Omelette);
await context.SaveChangesAsync();
Console.WriteLine($"Added omelette(id = {Omelette.Id}) successfully");

//Update data 
Console.WriteLine("Change omelette stars to 5");
Omelette.Stars = 5;
await context.SaveChangesAsync();
Console.WriteLine("Stars successfully changed");

//Remove single dish
Console.WriteLine("Removing omelette from database");
context.Dishes.Remove(Omelette);
await context.SaveChangesAsync();
Console.WriteLine("Omelette successfully removed");

//Create new dish with ingredients

//Dish:
Console.WriteLine("Add panncakes for lunch");
var Panncakes = new Dish { Title = "Panncakes Lunch", Notes = "Grandmas reciepe", Stars = 5};
context.Dishes.Add(Panncakes);
await context.SaveChangesAsync();
Console.WriteLine($"Added panncakes(id = {Omelette.Id}) successfully");

//Ingredients:
Console.WriteLine("Add ingredients for panncakes");
var Milk = new DishIngredient { Description = "Milk", UnitofMeasure= "Dl", Amount = 6, DishId = 10};
var Flour = new DishIngredient { Description = "Flour", UnitofMeasure = "Dl", Amount = 3, DishId = 10 };
var MeltedButter = new DishIngredient { Description = "MeltedButter", UnitofMeasure = "G", Amount = 50, DishId = 10 };
var Egg = new DishIngredient { Description = "Egg", UnitofMeasure = "St", Amount = 1, DishId = 10 };
var Salt = new DishIngredient { Description = "Salt", UnitofMeasure = "Tsk", Amount = 1/2, DishId = 10 };
context.Ingredients.Add(Milk);
context.Ingredients.Add(Flour);
context.Ingredients.Add(MeltedButter);
context.Ingredients.Add(Egg);
context.Ingredients.Add(Salt);
await context.SaveChangesAsync();
Console.WriteLine("Added ingredients for panncakes succesfully!");

//Add single ingredient to dish
Console.WriteLine("Add baking powder for panncakes");
await context.AddAsync(new DishIngredient() { Dish = Panncakes, Description = "BakingPowder", Amount = 1, UnitofMeasure = "Msk" });
Console.WriteLine("Baking powder added successfully!");

////Remove a dish and all of the ingredients (To use uncomment CTRL+K +C)
//Console.WriteLine("Removing panncakes and all its ingredients");
//foreach (var ingredient in await context.Ingredients.Where(i => i.DishId == Panncakes.Id ).ToArrayAsync())
//{
//    context.Remove(ingredient);
//}

//context.Remove(Panncakes);
//await context.SaveChangesAsync();

//Print all dishes (To use uncomment CTRL+K +C)

//await NoTracking(factory);
//static async Task NoTracking(CookbookContextFactory factory)
//{
//    using var dbContext = factory.CreateDbContext();
//    var dishes = await dbContext.Dishes.ToArrayAsync(); //Same as SQL SELECT * FROM Dishes
//}


// Create model classes that will become tables in in our database
class Dish
{
    public int Id { get; set; }

    [MaxLength(100)] //All MaxLenghts below is constrains 
    public string Title { get; set; } = string.Empty; //Title of Dish

    [MaxLength(1000)]
    public string? Notes { get; set; } // Optional notes

    public int? Stars { get; set; } // Optional stars

    public List<DishIngredient> Ingredients { get; set; } = new();  // The forgein key between Dish & DishIngredient
}

class DishIngredient 
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty; //Description of dishIngredient

    [MaxLength(50)]
    public string UnitofMeasure { get; set; } = string.Empty;  //The Measure

    [Column(TypeName = "decimal(5,2)")]
    public decimal Amount { get; set; } //How much of the ingredient

    public Dish? Dish { get; set; }

    public int DishId { get; set; } //Foregin key from DishIngredient to Dish
}

class CookbookContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }

    public DbSet<DishIngredient> Ingredients { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CookbookContext(DbContextOptions<CookbookContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        : base(options)
    {

    }
}

class CookbookContextFactory : IDesignTimeDbContextFactory<CookbookContext>
{
    public CookbookContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<CookbookContext>();
        optionsBuilder
                .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new CookbookContext(optionsBuilder.Options);
    }
}