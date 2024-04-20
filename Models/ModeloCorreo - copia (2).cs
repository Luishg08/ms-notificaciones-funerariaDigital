namespace ms_notificaciones.Models;

public class ModeloCorreoRecuperarContraseña
{
    public string? correoDestino { get; set; }
    public string? nombreDestino { get; set; }
    public string? asuntoCorreo { get; set; }

    public string? usuario { get; set; }
    public string? contraseña { get; set; }
}