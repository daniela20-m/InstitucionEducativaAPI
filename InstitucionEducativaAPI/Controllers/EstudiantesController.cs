using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InstitucionEducativaAPI.Data;
using InstitucionEducativaAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class EstudiantesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EstudiantesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Estudiantes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);

        if (estudiante == null)
        {
            return NotFound();
        }

        return estudiante;
    }

    // Obtener estudiantes con paginación
    [HttpGet]
    public async Task<ActionResult> GetEstudiantes(int page = 1, int limit = 10)
    {
        // Validación para la página y el límite
        if (page < 1) page = 1;
        if (limit < 1) limit = 10;

        // Obtener el número total de estudiantes
        var totalEstudiantes = await _context.Estudiantes.CountAsync();

        // Si no hay estudiantes, devolver un mensaje
        if (totalEstudiantes == 0)
        {
            return NotFound("No hay estudiantes registrados.");
        }

        // Calcular la cantidad total de páginas
        var totalPages = (int)Math.Ceiling(totalEstudiantes / (double)limit);

        // Obtener los estudiantes paginados
        var estudiantes = await _context.Estudiantes
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        // Crear la respuesta con los datos paginados
        var result = new
        {
            data = estudiantes,
            total = totalEstudiantes,
            page = page,
            limit = limit,
            totalPages = totalPages
        };

        return Ok(result);
    }

    // POST: api/Estudiantes
    [HttpPost]
    public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
    {
        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.EstudianteId }, estudiante);
    }

    // PUT: api/Estudiantes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
    {
        if (id != estudiante.EstudianteId)
        {
            return BadRequest();
        }

        _context.Entry(estudiante).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Estudiantes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstudiante(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null)
        {
            return NotFound();
        }

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
