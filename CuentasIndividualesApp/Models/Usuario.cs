namespace CuentasIndividualesApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Cuenta { get; set; }=string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
