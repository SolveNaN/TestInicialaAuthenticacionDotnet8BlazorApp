using CuentasIndividualesApp.Data;
using CuentasIndividualesApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CuentasIndividualesApp.Services
{
    public class UsuarioService:IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        UsuarioDB usuariodb=new UsuarioDB();
        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {

            CreatePasswordHash(usuario.Clave, out byte[] passwordHash, out byte[] passwordSalt);            
            usuariodb.NombreCuenta = usuario.Cuenta;
            usuariodb.PasswordHash = passwordHash;
            usuariodb.PasswordSalt = passwordSalt;
            usuariodb.Rol = usuario.Rol;
            _context.UsuarioDBs.Add(usuariodb); 
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<string> IniciarSesion(Usuario usuario)
        {
            var cuenta=await _context.UsuarioDBs.Where(x=>x.NombreCuenta== usuario.Cuenta).FirstOrDefaultAsync();

            if (cuenta == null)
            {
                return "Usuario no encontrado";
            }
            if (!VerifyPasswordHash(usuario.Clave, cuenta.PasswordHash, cuenta.PasswordSalt))
            {
                return "Contraseña incorrecta";
            }

            string token = CreateToken(cuenta);
            return token;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UsuarioDB user)
        {
            List<Claim> claims = new List<Claim>
     {
         new Claim(ClaimTypes.Name, user.NombreCuenta),
         new Claim(ClaimTypes.Role, user.Rol),
         new Claim(ClaimTypes.DateOfBirth, DateTime.Now.AddDays(-5).ToString())
     };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "CLAVE SECRETA DE AL MENOS 128 BITS ojo_ ESTA LONGITUD DE 128 ES LO MINIMO"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        
        public Task<string> ObtenerMensaje(string token)
        {
            throw new NotImplementedException();
        }
    }
}
