using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Eindproject.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private static readonly List<ProductDto> ProductDatabase = InitializeProducts();

    private static List<ProductDto> InitializeProducts()
    {
        return new List<ProductDto>
        {
            new ProductDto
            {
                Id = 1,
                Title = "Vintage Gazelle City Bike",
                Price = "€ 150",
                PriceAmount = 150,
                City = "Utrecht",
                Distance = "2.5 km",
                State = "Like new",
                Category = "Category 1",
                Image = "https://images.unsplash.com/photo-1485965120184-e220f721d03e?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1485965120184-e220f721d03e?auto=format&fit=crop&w=800&h=600&q=80",
                    "https://images.unsplash.com/photo-1532298229144-0ec0c57515c7?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Vintage city bike in excellent condition with 3 gears and working lights.",
                Top = true,
                Lat = 52.0907,
                Lon = 5.1214,
                SellerName = "Lucas",
                SellerInitials = "LU",
                SellerVerified = true,
                CreatedAt = "Today",
                AdStatus = "active",
                AllowsShipping = true,
                AllowsProtection = true,
                Bids = new List<BidDto>
                {
                    new BidDto { Id = "b1", BidderName = "Emma", Amount = 135, Time = "1 hour ago", Status = "pending" },
                    new BidDto { Id = "b2", BidderName = "Lars", Amount = 120, Time = "3 hours ago", Status = "pending" }
                }
            },
            new ProductDto
            {
                Id = 2,
                Title = "Canon Vintage 35mm Camera",
                Price = "€ 85",
                PriceAmount = 85,
                City = "Amsterdam",
                Distance = "5.0 km",
                State = "Used",
                Category = "Category 2",
                Image = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Classic analogue camera with 50mm f/1.8 lens. Tested and working.",
                Lat = 52.3676,
                Lon = 4.9041,
                SellerName = "Sophie",
                SellerInitials = "SO",
                SellerVerified = true,
                CreatedAt = "Yesterday",
                AdStatus = "active",
                AllowsShipping = true,
                AllowsProtection = true,
                Bids = new List<BidDto>
                {
                    new BidDto { Id = "b3", BidderName = "Dennis", Amount = 75, Time = "Yesterday", Status = "pending" }
                }
            },
            new ProductDto
            {
                Id = 3,
                Title = "Mid-Century Armchair",
                Price = "€ 220",
                PriceAmount = 220,
                City = "Haarlem",
                Distance = "8.2 km",
                State = "New",
                Category = "Category 3",
                Image = "https://images.unsplash.com/photo-1580481077197-76b611e9f196?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1580481077197-76b611e9f196?auto=format&fit=crop&w=800&h=600&q=80",
                    "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Beautiful retro armchair with solid teak wood frame and grey fabric.",
                Lat = 52.3874,
                Lon = 4.6462,
                SellerName = "Daan",
                SellerInitials = "DA",
                SellerVerified = true,
                CreatedAt = "2 days ago",
                AdStatus = "sold",
                AllowsShipping = false,
                AllowsProtection = true
            },
            new ProductDto
            {
                Id = 4,
                Title = "Nike Air Retro Sneakers (Size 43)",
                Price = "€ 45",
                PriceAmount = 45,
                City = "Rotterdam",
                Distance = "12 km",
                State = "Good",
                Category = "Category 4",
                Image = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Original red sneakers in good condition, only worn a few times.",
                Lat = 51.9244,
                Lon = 4.4777,
                SellerName = "Emre",
                SellerInitials = "EM",
                SellerVerified = false,
                CreatedAt = "Today"
            },
            new ProductDto
            {
                Id = 5,
                Title = "Trek Road Bike 56cm",
                Price = "€ 310",
                PriceAmount = 310,
                City = "Amersfoort",
                Distance = "3.8 km",
                State = "Like new",
                Category = "Category 1",
                Image = "https://images.unsplash.com/photo-1532298229144-0ec0c57515c7?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1532298229144-0ec0c57515c7?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Lightweight aluminium racing bike with Shimano gear set.",
                Top = true,
                Lat = 52.1561,
                Lon = 5.3878,
                SellerName = "Lucas",
                SellerInitials = "LU",
                SellerVerified = true,
                CreatedAt = "3 days ago"
            },
            new ProductDto
            {
                Id = 6,
                Title = "Apple iPhone 13 128GB",
                Price = "€ 60",
                PriceAmount = 60,
                City = "Leiden",
                Distance = "6.5 km",
                State = "Used",
                Category = "Category 2",
                Image = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Screen protector included, 88% battery health.",
                Lat = 52.1601,
                Lon = 4.497,
                SellerName = "Sophie",
                SellerInitials = "SO",
                SellerVerified = false,
                CreatedAt = "Last week"
            },
            new ProductDto
            {
                Id = 7,
                Title = "Large Monstera Deliciosa Plant",
                Price = "€ 175",
                PriceAmount = 175,
                City = "Delft",
                Distance = "9.1 km",
                State = "Good",
                Category = "Category 3",
                Image = "https://images.unsplash.com/photo-1485955900006-10f4d324d411?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1485955900006-10f4d324d411?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Healthy 1.2m houseplant with ceramic pot included.",
                Lat = 52.0116,
                Lon = 4.3571,
                SellerName = "Daan",
                SellerInitials = "DA",
                SellerVerified = true,
                CreatedAt = "Today"
            },
            new ProductDto
            {
                Id = 8,
                Title = "Modern Minimalist Floor Lamp",
                Price = "€ 25",
                PriceAmount = 25,
                City = "Utrecht",
                Distance = "15 km",
                State = "Good",
                Category = "Category 4",
                Image = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?auto=format&fit=crop&w=600&h=450&q=80",
                Images = new List<string>
                {
                    "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?auto=format&fit=crop&w=800&h=600&q=80"
                },
                Description = "Matte black standing lamp, includes warm LED bulb.",
                Lat = 52.0907,
                Lon = 5.1214,
                SellerName = "Emre",
                SellerInitials = "EM",
                SellerVerified = true,
                CreatedAt = "Today"
            }
        };
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> Get([FromQuery] string? q, [FromQuery] string? category)
    {
        var query = ProductDatabase.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p =>
                p.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(query.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = ProductDatabase.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound(new { error = $"Product with ID {id} not found." });
        }
        return Ok(product);
    }

    [HttpPost]
    public ActionResult<ProductDto> Create([FromBody] ProductDto input)
    {
        var newId = ProductDatabase.Count > 0 ? ProductDatabase.Max(p => p.Id) + 1 : 1;
        input.Id = newId;

        if (string.IsNullOrWhiteSpace(input.Image))
        {
            input.Image = $"https://picsum.photos/seed/bidzaar-{newId}/600/450.webp";
        }
        if (input.Images == null || input.Images.Count == 0)
        {
            input.Images = new List<string> { input.Image };
        }

        ProductDatabase.Insert(0, input);
        return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var removed = ProductDatabase.RemoveAll(p => p.Id == id);
        return Ok(new { success = removed > 0 });
    }
}

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public decimal PriceAmount { get; set; }
    public string City { get; set; } = string.Empty;
    public string Distance { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public bool Top { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string SellerName { get; set; } = string.Empty;
    public string SellerInitials { get; set; } = string.Empty;
    public bool SellerVerified { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public string AdStatus { get; set; } = "active";
    public bool AllowsShipping { get; set; }
    public bool AllowsProtection { get; set; }
    public List<BidDto> Bids { get; set; } = new();
}

public class BidDto
{
    public string Id { get; set; } = string.Empty;
    public string BidderName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Time { get; set; } = string.Empty;
    public string Status { get; set; } = "pending";
}
