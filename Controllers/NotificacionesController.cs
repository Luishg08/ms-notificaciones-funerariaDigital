using Microsoft.AspNetCore.Mvc;
using ms_notificaciones.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ms_notificaciones.Controllers;

[ApiController]
[Route("[controller]")]

public class NotificacionesController : ControllerBase
{   
    [HttpPost]
    [Route("correo-bienvenida")]
    public async Task<ActionResult> EnviarCorreo(ModeloCorreo datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),
        Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN_API_KEY")!)
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
        request.AddParameter ("to", datos.correoDestino);
        request.AddParameter ("subject", datos.asuntoCorreo);
        request.AddParameter ("text", datos.contenidoCorreo);
        request.AddParameter("template", "plantillaBienvenida");
        request.Method = Method.Post;
        var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }

    [HttpPost]
    [Route("correo-recuperar-contraseña")]
    public async Task<ActionResult> EnviarRecuperaciónContraseña(ModeloCorreoRecuperarContraseña datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),
        Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN_API_KEY")!)
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
        request.AddParameter ("to", datos.correoDestino);
        request.AddParameter ("subject", datos.asuntoCorreo);
        request.AddParameter("template", "plantillarecuperacioncontraseña");
        request.AddParameter("h:X-Mailgun-Variables", "{\"contraseña\": \"" + datos.contraseña + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"usuario\": \"" + datos.usuario + "\"}");
        Console.WriteLine("Datos:"+datos.contraseña);
        Console.WriteLine("Correo destino:"+datos.correoDestino);
        request.Method = Method.Post;
        var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }

    [HttpPost]
    [Route("correo-2fa")]
    public async Task<ActionResult> EnviarCodigo2FA(ModeloCorreo datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),
        Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN_API_KEY")!)
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
        request.AddParameter ("to", datos.correoDestino);
        request.AddParameter ("subject", datos.asuntoCorreo);
        request.AddParameter ("text", datos.contenidoCorreo);
        request.AddParameter("template", "plantilla2fa");
        request.AddParameter("h:X-Mailgun-Variables", "{\"codigo\": \"" + datos.contenidoCorreo + "\"}");
        request.Method = Method.Post;
        var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            Console.WriteLine(datos.correoDestino+" "+datos.asuntoCorreo+" "+datos.contenidoCorreo);
            return BadRequest("Error al enviar el correo");
        }
    }
    [HttpPost]
    [Route("correo-verificacion")]
    public async Task<ActionResult> EnviarCorreoVerificacion(ModeloCorreo datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),
        Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN_API_KEY")!)
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
        request.AddParameter ("to", datos.correoDestino);
        request.AddParameter ("subject", datos.asuntoCorreo);
        request.AddParameter ("text", datos.contenidoCorreo);
        request.AddParameter("template", "plantillacorreoverificacion");
        Console.WriteLine("Datos:"+datos.contenidoCorreo);
        request.AddParameter("h:X-Mailgun-Variables", "{\"Mensaje\": \"" + datos.contenidoCorreo + "\"}");
        request.Method = Method.Post;
        var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }

    [HttpPost]
    [Route("correo-servicio-funerario")]
    public async Task<ActionResult> EnviarInformacionServicioFunerario(ModeloCorreoServicioFunerario datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),
        Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN_API_KEY")!)
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
        request.AddParameter ("to", datos.correoDestino);
        request.AddParameter ("subject", datos.asuntoCorreo);
        request.AddParameter("template", "enviarinformacióndeserviciofunerario");
        request.AddParameter("h:X-Mailgun-Variables", "{\"ciudad\": \"" + datos.ciudad + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"fechaIngreso\": \"" + datos.fechaIngreso + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"fechaSalida\": \"" + datos.fechaSalida + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"sala\": \"" + datos.sala + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"sede\": \"" + datos.sede + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"usuario\": \"" + datos.nombreUsuario + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"beneficiario\": \"" + datos.beneficiario + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"tipoSepultura\": \"" + datos.tipoSepultura + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"ubicacionCuerpo\": \"" + datos.ubicacionCuerpo + "\"}");
        request.Method = Method.Post;
        var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }

    [HttpPost]
    [Route("correo-codigo-servicio-funerario")]
    public async Task<ActionResult> EnviarCodigoUnicoServicioFunerario(ModeloCorreoCodigo datos)
    {
        Console.WriteLine("Enviando correo");
        var options = new RestClientOptions
        {
        BaseUrl = new Uri("https://api.mailgun.net/v3"),
        Authenticator = new HttpBasicAuthenticator("api", Environment.GetEnvironmentVariable("MAILGUN_API_KEY")!)
        };
        using var client = new RestClient(options);		
        RestRequest request = new RestRequest ();
        request.AddParameter ("domain", "sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter ("from", datos.nombreDestino+" <"+datos.nombreDestino+"@sandbox8f1b39cd63b3454f93202852c09bb380.mailgun.org>");
        request.AddParameter ("to", datos.correoDestino);
        request.AddParameter ("subject", datos.asuntoCorreo);
        request.AddParameter("template", "plantillaenviarcodigounicoserviciofunerario");
        request.AddParameter("h:X-Mailgun-Variables", "{\"codigoUnico\": \"" + datos.codigoUnico + "\"}");
        request.AddParameter("h:X-Mailgun-Variables", "{\"usuario\": \"" + datos.usuario + "\"}");
        request.Method = Method.Post;
        var respuesta = await client.ExecuteAsync(request);
        if(respuesta.StatusCode == System.Net.HttpStatusCode.OK){
            return Ok("Correo enviado");
        }else{
            return BadRequest("Error al enviar el correo");
        }
    }



//Envió de SMS 
    [HttpPost]
    [Route("sms")]
    public async Task<ActionResult> EnviarSMSNuevaClave(ModeloSms datos)
    {
        var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        var authToken = Environment.GetEnvironmentVariable("TWILIO_TOKEN");
        Console.WriteLine(accountSid);
        Console.WriteLine(authToken);
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
        new PhoneNumber(datos.numeroDestino));
        messageOptions.From = new PhoneNumber("+12512443728");
        messageOptions.Body = datos.contenidoMensaje;


        var message = MessageResource.Create(messageOptions);
        Console.WriteLine(message.Body);

        return Ok("SMS enviado");
    }
}