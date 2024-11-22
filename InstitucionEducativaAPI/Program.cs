using InstitucionEducativaAPI.Data;
using InstitucionEducativaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.IO; // Necesario para usar Path

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la base de datos con la cadena de conexi�n desde appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para los controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de Kestrel (para HTTPS) antes de crear la aplicaci�n
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
        Console.WriteLine($"No se pudo encontrar el archivo del certificado en la ruta {certPath}. Aseg�rate de que el archivo exista.");
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
            new Estudiante { Nombre = "Juan", Apellido = "P�rez", NumeroIdentificacion = "1234567890", FechaNacimiento = new DateTime(2002, 5, 10) },
            new Estudiante { Nombre = "Ana", Apellido = "G�mez", NumeroIdentificacion = "2345678901", FechaNacimiento = new DateTime(2001, 3, 15) },
            new Estudiante { Nombre = "Carlos", Apellido = "L�pez", NumeroIdentificacion = "3456789012", FechaNacimiento = new DateTime(2000, 12, 25) },
            new Estudiante { Nombre = "Laura", Apellido = "Mart�nez", NumeroIdentificacion = "4567890123", FechaNacimiento = new DateTime(1999, 7, 30) },
            new Estudiante { Nombre = "Pedro", Apellido = "S�nchez", NumeroIdentificacion = "5678901234", FechaNacimiento = new DateTime(1998, 2, 5) },
            new Estudiante { Nombre = "Marta", Apellido = "Rodr�guez", NumeroIdentificacion = "6789012345", FechaNacimiento = new DateTime(2003, 8, 18) },
            new Estudiante { Nombre = "Jos�", Apellido = "Hern�ndez", NumeroIdentificacion = "7890123456", FechaNacimiento = new DateTime(2000, 1, 20) },
            new Estudiante { Nombre = "Isabel", Apellido = "D�az", NumeroIdentificacion = "8901234567", FechaNacimiento = new DateTime(1997, 11, 22) },
            new Estudiante { Nombre = "David", Apellido = "Fern�ndez", NumeroIdentificacion = "9012345678", FechaNacimiento = new DateTime(2004, 4, 14) },
            new Estudiante { Nombre = "Sara", Apellido = "P�rez", NumeroIdentificacion = "0123456789", FechaNacimiento = new DateTime(2002, 6, 19) },
            new Estudiante { Nombre = "Antonio", Apellido = "Garc�a", NumeroIdentificacion = "1234098765", FechaNacimiento = new DateTime(2001, 9, 2) },
            new Estudiante { Nombre = "Raquel", Apellido = "V�zquez", NumeroIdentificacion = "2345098764", FechaNacimiento = new DateTime(2000, 12, 1) },
            new Estudiante { Nombre = "Luis", Apellido = "Ram�rez", NumeroIdentificacion = "3456098763", FechaNacimiento = new DateTime(2003, 11, 10) },
            new Estudiante { Nombre = "Elena", Apellido = "Jim�nez", NumeroIdentificacion = "4567098762", FechaNacimiento = new DateTime(1998, 4, 25) },
            new Estudiante { Nombre = "Ricardo", Apellido = "G�mez", NumeroIdentificacion = "5678098761", FechaNacimiento = new DateTime(2002, 9, 13) }
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

app.Run();  // Inicia la aplicaci�n
