using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Controllers;

[ApiController]
[Route("api/Products")]
public class ProductController : ControllerBase
{
    string search;
    string sortBy;
    string sortOrder;
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
       
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
        [FromQuery] string? search,
        [FromQuery] string? sortBy="name",
        [FromQuery] string? sortOrder="asc"
)
    {

        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
        }

        query = sortBy?.ToLower() switch
        {
            "name" => sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            "price" => sortOrder?.ToLower() == "desc"
            ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),

            _ => query
        };
        
        var products = await query.ToListAsync();

        return Ok(products);
    }
}
