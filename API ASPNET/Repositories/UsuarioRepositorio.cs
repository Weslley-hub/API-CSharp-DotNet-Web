using API_ASPNET.Data;
using API_ASPNET.Models;
using API_ASPNET.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_ASPNET.Repositories
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly TarefasDBContext _dBContext;

        public UsuarioRepositorio(TarefasDBContext tarefasDBContext)
        {
            _dBContext = tarefasDBContext;
        }
        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            return await _dBContext.Usuarios.ToListAsync();
        }
        public async Task<UsuarioModel> BuscarUsuarioId(int id)
        {
            return await _dBContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {

            await _dBContext.Usuarios.AddAsync(usuario);
            await _dBContext.SaveChangesAsync();
            
            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioId(id);

            if (usuarioPorId == null)
            {
                throw new Exception(message: $"Usúario para o ID: {id} não foi encontrado no banco de dados.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dBContext.Usuarios.Update(usuarioPorId);
            await _dBContext.SaveChangesAsync();

            return usuarioPorId;
        }
        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarUsuarioId(id);

            if (usuarioPorId == null)
            {
                throw new Exception(message: $"Usúario para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dBContext.Usuarios.Remove(usuarioPorId);
            await _dBContext.SaveChangesAsync();

            return true;
        }
    }
}
