using InstitucionEducativaAPI.Data;
using InstitucionEducativaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO; // Necesario para usar Path

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos con la cadena de conexión desde appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para los controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de Kestrel (para HTTPS) antes de crear la aplicación
builder.WebHost.ConfigureKestrel(options =>
{
    var certPath = Path.Combine(Directory.GetCurrentDirectory(), "certificados", "certificado.pfx");
    string certPassword = "tu_password";

    if (File.Exists(certPath))
    {
        options.Listen(IPAddress.Any, 5001, listenOptions =>
        {
            listenOptions.UseHttps(certPath, certPassword);
        });
    }
    else
    {
        Console.WriteLine($"No se pudo encontrar el archivo del certificado en la ruta {certPath}. Asegúrate de que el archivo exista.");
    }
});

var app = builder.Build();

// Inicializar la base de datos y agregar registros
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Estudiantes.Any())
    {
        // Agregar estudiantes si no existen
        context.Estudiantes.AddRange(
            new Estudiante { Nombre = "Juan", Apellido = "Pérez", NumeroIdentificacion = "1234567890", FechaNacimiento = new DateTime(2002, 5, 10) },
            new Estudiante { Nombre = "Ana", Apellido = "Gómez", NumeroIdentificacion = "2345678901", FechaNacimiento = new DateTime(2001, 3, 15) },
            new Estudiante { Nombre = "Carlos", Apellido = "López", NumeroIdentificacion = "3456789012", FechaNacimiento = new DateTime(2000, 12, 25) },
            new Estudiante { Nombre = "Laura", Apellido = "Martínez", NumeroIdentificacion = "4567890123", FechaNacimiento = new DateTime(1999, 7, 30) },
            new Estudiante { Nombre = "Pedro", Apellido = "Sánchez", NumeroIdentificacion = "5678901234", FechaNacimiento = new DateTime(1998, 2, 5) },
            new Estudiante { Nombre = "Marta", Apellido = "Rodríguez", NumeroIdentificacion = "6789012345", FechaNacimiento = new DateTime(2003, 8, 18) },
            new Estudiante { Nombre = "José", Apellido = "Hernández", NumeroIdentificacion = "7890123456", FechaNacimiento = new DateTime(2000, 1, 20) },
            new Estudiante { Nombre = "Isabel", Apellido = "Díaz", NumeroIdentificacion = "8901234567", FechaNacimiento = new DateTime(1997, 11, 22) },
            new Estudiante { Nombre = "David", Apellido = "Fernández", NumeroIdentificacion = "9012345678", FechaNacimiento = new DateTime(2004, 4, 14) },
            new Estudiante { Nombre = "Sara", Apellido = "Pérez", NumeroIdentificacion = "0123456789", FechaNacimiento = new DateTime(2002, 6, 19) },
            new Estudiante { Nombre = "Antonio", Apellido = "García", NumeroIdentificacion = "1234098765", FechaNacimiento = new DateTime(2001, 9, 2) },
            new Estudiante { Nombre = "Raquel", Apellido = "Vázquez", NumeroIdentificacion = "2345098764", FechaNacimiento = new DateTime(2000, 12, 1) },
            new Estudiante { Nombre = "Luis", Apellido = "Ramírez", NumeroIdentificacion = "3456098763", FechaNacimiento = new DateTime(2003, 11, 10) },
            new Estudiante { Nombre = "Elena", Apellido = "Jiménez", NumeroIdentificacion = "4567098762", FechaNacimiento = new DateTime(1998, 4, 25) },
            new Estudiante { Nombre = "Ricardo", Apellido = "Gómez", NumeroIdentificacion = "5678098761", FechaNacimiento = new DateTime(2002, 9, 13) }
        );
        context.SaveChanges();  // Guardar los cambios en la base de datos
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // Redirige HTTP a HTTPS
app.MapControllers();  // Mapea los controladores de la API

app.Run();  // Inicia la aplicación
