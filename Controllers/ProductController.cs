using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogApi.Data;
using ProductCatalogApi.Models;

namespace ProductCatalogApi.Controllers;

[ApiController]
[Route("api/Products")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken, 
    [FromQuery] string? search, 
    [FromQuery] string? sortBy = "name",
    [FromQuery] string? sortOrder = "asc")
    {

        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            
        } 
        
        var products = await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        return Ok(products);
    }
}
