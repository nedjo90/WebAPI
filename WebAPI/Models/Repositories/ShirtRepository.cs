namespace WebAPI.Models.Repositories;

public static class ShirtRepository
{
    public static List<Shirt> ListOfShirt = new List<Shirt>()
    {
        new Shirt {ShirtId = 1, Brand = "adidas", Color = "blue", Gender = "men", Price = 9.99, Size = 10},
        new Shirt {ShirtId = 2, Brand = "nike", Color = "orange", Gender = "men", Price = 19.99, Size = 11},
        new Shirt {ShirtId = 3, Brand = "jenifer", Color = "pink", Gender = "women", Price = 29.99, Size = 6},
        new Shirt {ShirtId = 4, Brand = "etam", Color = "white", Gender = "women", Price = 39.99, Size = 7}
    };

    public static List<Shirt> GetShirts()
    {
        return ListOfShirt;
    }
    public static bool ShirtExist(int id)
    {
        return ListOfShirt.Any(x => x.ShirtId == id);
    }

    public static Shirt? GetShirtById(int id)
    {
        return ListOfShirt.FirstOrDefault(x => x.ShirtId == id);
    }

    public static void AddShirt(Shirt shirt)
    {
        int max = ListOfShirt.Max(x => x.ShirtId);
        shirt.ShirtId = max + 1;
        ListOfShirt.Add(shirt);
    }

    public static Shirt? GetShirtProperties(string? brand, string? color, string? gender, int? size)
    {
        return ListOfShirt.FirstOrDefault(x => 
                 !string.IsNullOrWhiteSpace(brand) &&
                 !string.IsNullOrWhiteSpace(x.Brand) &&
                 x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
                 !string.IsNullOrWhiteSpace(color) &&
                 !string.IsNullOrWhiteSpace(x.Color) &&
                 x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
                 !string.IsNullOrWhiteSpace(gender) &&
                 !string.IsNullOrWhiteSpace(x.Gender) &&
                 x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
                 size.HasValue && x.Size.HasValue && size.Value == x.Size.Value
                 
        );
    }

    public static void UpdateShirt(Shirt shirt)
    {
        Shirt shirtToUpdate = ListOfShirt.First(x => x.ShirtId == shirt.ShirtId);
        shirtToUpdate.Brand = shirt.Brand;
        shirtToUpdate.Gender = shirt.Gender;
        shirtToUpdate.Color = shirt.Color;
        shirtToUpdate.Size = shirt.Size;
        shirtToUpdate.Price = shirt.Price;
    }

    public static void DeleteShirt(int id)
    {
            Shirt? shirt = GetShirtById(id);
            if (shirt != null)
                ListOfShirt.Remove(shirt);
    }
}