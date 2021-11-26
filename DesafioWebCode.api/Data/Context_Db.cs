using DesafioWebCode.api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioWebCode.api.Data
{
    public class Context_Db:DbContext
    {
        public Context_Db(DbContextOptions<Context_Db> options) : base(options)
        {

        }
        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Filmes> Filmes { get; set; }
        public DbSet<Assistidos> Assistidos { get; set;}

    }
}
