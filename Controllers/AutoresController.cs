using Microsoft.AspNetCore.Mvc;
using BreidyVasquez_p1_p4.Api.Models;
using BreidyVasquez_p1_p4.Api.Services;

namespace BreidyVasquez_p1_p4.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutoresController(AutorService autorService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetAutores()
    {
        var autores = await autorService.GetAllAsync();

        return Ok(autores);
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAutor(int id)
    {
        var autor = await autorService.GetByIdAsync(id);

        if (autor == null)
        {
            return NotFound(new
            {
                mensaje = "Autor no encontrado."
            });
        }

        return Ok(autor);
    }

    
    [HttpPost]
    public async Task<IActionResult> CrearAutor([FromBody] Autor autor)
    {
        int id = await autorService.CreateAsync(autor);

        autor.IdAutor = id;

        return CreatedAtAction(
            nameof(GetAutor),
            new { id = id },
            autor
        );
    }

    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarAutor(
        int id,
        [FromBody] Autor autor)
    {
        bool actualizado = await autorService.UpdateAsync(id, autor);

        if (!actualizado)
        {
            return NotFound(new
            {
                mensaje = "Autor no encontrado."
            });
        }

        autor.IdAutor = id;

        return Ok(autor);
    }

    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarAutor(int id)
    {
        bool eliminado = await autorService.DeleteAsync(id);

        if (!eliminado)
        {
            return NotFound(new
            {
                mensaje = "Autor no encontrado."
            });
        }

        return Ok(new
        {
            mensaje = "Autor eliminado correctamente."
        });
    }
}