using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace demo3.Models;

public partial class Agent
{
    public string? Name { get; set; }

    public int Typeid { get; set; }

    public string Email { get; set; } = null!;

    public int Priority { get; set; }

    public string? Imagepath { get; set; }

    public string? Address { get; set; }

    public string? Inn { get; set; }

    public string? Kpp { get; set; }

    public string? Dirname { get; set; }

    public string? Phone { get; set; }

    public int Agentid { get; set; }

    public virtual ICollection<Salehistory> Salehistories { get; set; } = new List<Salehistory>();

    public virtual Type Type { get; set; } = null!;

    public int CountOfSells => Salehistories
        .Where(x => x.Saledate >= DateOnly.FromDateTime(DateTime.Today.AddYears(-1)))
        .Select(x => x.Products.Count)
        .Sum();

    public int Discount => SetDiscount();

    public Bitmap Image => Imagepath == null
        ? new Bitmap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/", "picture.png"))
        : File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/", Imagepath))
            ? new Bitmap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/", Imagepath!))
            : new Bitmap(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/", "picture.png"));
    public SolidColorBrush Color => Discount >= 25
        ? new SolidColorBrush(Colors.LightGreen)
        : new SolidColorBrush(Colors.White);

    private int SetDiscount()
    {
        var sum = Salehistories.Select(x => x.Products.Count).Sum();
        if (sum >= 0 && sum < 10000) return 0;
        if (sum >= 10000 && sum < 50000) return 5;
        if (sum >= 50000 && sum < 150000) return 10;
        if (sum >= 150000 && sum < 500000) return 20;
        return 25;
    }
}
