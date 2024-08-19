using Microsoft.EntityFrameworkCore;

namespace AIO.Infrastructure.Persistence;

public class ApplicationReadDbContext(DbContextOptions options) : ApplicationDbContext(options);