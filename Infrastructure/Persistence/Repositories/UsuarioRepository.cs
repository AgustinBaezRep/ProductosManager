using Application.Abstraction;
using Contracts.Requests;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly ProductosManagerDbContext _context;

        public UsuarioRepository(ProductosManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public Usuario? GetByEmailAndPassowrd(LoginRequest request)
        {
            return _context.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
        }
    }
}
