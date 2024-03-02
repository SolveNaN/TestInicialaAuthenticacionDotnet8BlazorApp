using CuentasIndividualesApp.Models;

namespace CuentasIndividualesApp.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<string> IniciarSesion(Usuario usuario);
        Task<string> ObtenerMensaje(string token);
        

    }
}
