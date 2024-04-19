namespace ms_notificaciones.Models;

public class ModeloCorreoServicioFunerario
{
    public string? correoDestino { get; set; }
    public string? nombreDestino { get; set; }
    public string? asuntoCorreo { get; set; }

    public string? ciudad { get; set; }

    public string? sala { get; set; }
    public string? sede { get; set; }
    public string? nombreUsuario { get; set; }

    public string? beneficiario { get; set; }

    public string? fechaIngreso { get; set; }
    public string? fechaSalida { get; set; }

    public string? tipoSepultura { get; set; }
    public string? ubicacionCuerpo { get; set; }
}