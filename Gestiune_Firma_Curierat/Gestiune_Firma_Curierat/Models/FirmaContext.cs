using Microsoft.EntityFrameworkCore;

public class FirmaContext : DbContext
{
    public DbSet<Colet> Colete { get; set; }
    public DbSet<Client> Clienti { get; set; }
    public DbSet<Actiune> Actiuni { get; set; }

    public DbSet<User> Users { get; set; }


    public FirmaContext(DbContextOptions<FirmaContext> options) : base(options)
    {
    }

}