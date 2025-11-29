using Microsoft.EntityFrameworkCore;
using ServiceSoap.Interface;
using System.ComponentModel.DataAnnotations;
using trabalho_rest.Data;
using trabalho_rest.Model;


namespace ServiceSoap.Repository
{
    public class RUsuario(AppDbContext context) : IUsuario
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> Delete(int id)
        {
            var usuario = await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (usuario == null) throw new ValidationException($"Usuario não encontrado no sistema com ID: {id}");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<Usuario?> GetIsExistUsuario(Usuario usuario) => await _context.Usuarios.Where(x => x.Name == usuario.Name && x.Email == usuario.Email).FirstOrDefaultAsync();

        public async Task<int> GetIdUsuario(string nome, string Email)
        {
            var Result = await _context.Usuarios.Where(x => x.Name == nome || x.Email == Email).FirstOrDefaultAsync();

            if (Result == null) throw new ValidationException("Usuario não encontrado");

            return Result.Id;
        }

        public async Task<Usuario> Update(Usuario entity)
        {

            var usuario = await _context.Usuarios.FindAsync(entity.Id) ?? throw new KeyNotFoundException($"Usuario com ID {entity.Id} não encontrado");

            _context.Entry(usuario).CurrentValues.SetValues(new Usuario()
            {
                Id = entity.Id,
                Name = entity.Name,
                Descricao = entity.Descricao,
                Email = entity.Email
            });

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Usuario> Create(Usuario entity)
        {

            var isExistUser = await GetIsExistUsuario(entity);

            if (isExistUser == null)
            {
                await _context.Usuarios.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException("Usuario Já cadastrado.");
            }

            return await GetIsExistUsuario(entity);
        }

       public async Task<Usuario> Read(int id)
       {
            var usuario = await _context.Usuarios.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (usuario == null) throw new KeyNotFoundException($"Usuario com ID {id} não encontrado");

            return usuario;
       }

        public async Task<IEnumerable<Usuario>> ReadAll() => await _context.Usuarios.ToListAsync();
    }
}
