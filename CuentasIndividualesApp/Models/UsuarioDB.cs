namespace CuentasIndividualesApp.Models
{
    public class UsuarioDB
    {
        public int Id { get; set; }
        public string NombreCuenta { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Rol { get; set; } =string.Empty;
        public string Claim { get; set; } = string.Empty;
        //Aqui se colocan todos los demas datos que tenga el usuario como nombres, direcciones, fechas, edad, estados etc

    }
}
