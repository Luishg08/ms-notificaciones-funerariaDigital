namespace ms_notificaciones.Models;

public class ModeloCorreoPQRSAdmin
{
    public string? correoDestino { get; set; }
    public string? nombreDestino { get; set; }
    public string? asuntoCorreo { get; set; }

    public string? contenidoMensaje { get; set; }

    public string? usuario {get; set;}

    public string? tipoPQRS {get; set;}
}