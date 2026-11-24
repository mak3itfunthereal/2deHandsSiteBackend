using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Eindproject.Controllers;

[ApiController]
[Route("api/images")]
public class ImagesController : ControllerBase
{
    private static readonly Dictionary<string, string> CategoryImagePlaceholders = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Bikes", "https://images.unsplash.com/photo-1485965120184-e220f721d03e?auto=format&fit=crop&w=600&q=80" },
        { "Electronics", "https://images.unsplash.com/photo-1526738549149-8e07eca6c147?auto=format&fit=crop&w=600&q=80" },
        { "Furniture", "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?auto=format&fit=crop&w=600&q=80" },
        { "Clothing", "https://images.unsplash.com/photo-1523381210434-271e8be1f52b?auto=format&fit=crop&w=600&q=80" },
        { "Books", "https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?auto=format&fit=crop&w=600&q=80" },
        { "Sports", "https://images.unsplash.com/photo-1517649763962-0c623266ddc0?auto=format&fit=crop&w=600&q=80" },
        { "Garden", "https://images.unsplash.com/photo-1416879595882-3373a0480b5b?auto=format&fit=crop&w=600&q=80" },
        { "Default", "https://images.unsplash.com/photo-1526738549149-8e07eca6c147?auto=format&fit=crop&w=600&q=80" }
    };

    /// <summary>
    /// Loads image information via API.
    /// GET /api/images?category=Bikes
    /// </summary>
    [HttpGet]
    public IActionResult GetImage([FromQuery] string? category = null, [FromQuery] int? width = 600, [FromQuery] int? height = 450)
    {
        var catKey = !string.IsNullOrWhiteSpace(category) && CategoryImagePlaceholders.ContainsKey(category)
            ? category
            : "Default";

        var url = CategoryImagePlaceholders[catKey];
        return Ok(new
        {
            category = catKey,
            url = url,
            optimizedUrl = $"{url}&w={width}&h={height}",
            format = "webp",
            status = "ready"
        });
    }

    /// <summary>
    /// Image endpoint for a specific product ID.
    /// GET /api/images/{productId}
    /// </summary>
    [HttpGet("{productId:int}")]
    public IActionResult GetProductImage(int productId, [FromQuery] int? width = 600, [FromQuery] int? height = 450)
    {
        var seed = Math.Abs(productId);
        var url = $"https://picsum.photos/seed/product{seed}/{width ?? 600}/{height ?? 450}";

        return Ok(new
        {
            productId,
            url = url,
            format = "webp",
            loading = "lazy",
            decoding = "async"
        });
    }
}
