using Microsoft.AspNetCore.Mvc;
using ms_notificaciones.Models;
using RestSharp;
using RestSharp.Authenticators;


namespace ms_notificaciones.Controllers;

[ApiController]
[Route("[controller]")]

public class NotificacionesController : ControllerBase
{   
    [HttpPost]
    [Route("correo")]
    public async Task<ActionResult> EnviarCorreo(ModeloCorreo datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),    
        Authenticator = new HttpBasicAuthenticator("api", "")
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
		request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
		request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
		request.AddParameter ("to", datos.correoDestino);
		request.AddParameter ("subject", datos.asuntoCorreo);
		request.AddParameter ("text", datos.contenidoCorreo);
		request.Method = Method.Post;
		var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }

    [Route("sms")]
    [HttpGet]
    public Task<ActionResult> mostrarMensaje(){
        
        return Task.FromResult<ActionResult>(Ok("Mensaje de prueba"));
        
    }
}