using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllAsync(cancellationToken);

        return Ok(products);
    }

    [HttpGet("holamundo")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ActionResult<string> GetHolaMundo()
    {
        return Ok("Hola Mundo");
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("The product id must be greater than zero.");
        }

        var product = await _productService.GetByIdAsync(id, cancellationToken);

        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductResponseDto>> Create(
        ProductCreateDto dto,
        CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        ProductUpdateDto dto,
        CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("The product id must be greater than zero.");
        }

        var wasUpdated = await _productService.UpdateAsync(id, dto, cancellationToken);

        return wasUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest("The product id must be greater than zero.");
        }

        var wasDeleted = await _productService.DeleteAsync(id, cancellationToken);

        return wasDeleted ? NoContent() : NotFound();
    }
}
