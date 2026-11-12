using Microsoft.EntityFrameworkCore;

namespace Eindproject.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
}
