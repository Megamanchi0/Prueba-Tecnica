using Backend.Clases;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [EnableCors(origins: "http://localhost:50247", headers: "*", methods: "*")]
    public class EnviosMaritimosController : ApiController
    {
        public string Post([FromBody] EnvioMaritimo envio)
        {
            clsEnvioMaritimo _envio = new clsEnvioMaritimo();
            return _envio.Insertar(envio);
        }

        public string Put([FromBody] EnvioMaritimo envio)
        {
            clsEnvioMaritimo _envio = new clsEnvioMaritimo();
            return _envio.Actualizar(envio);
        }

        public EnvioMaritimo Get(string numeroGuia)
        {
            clsEnvioMaritimo _envio = new clsEnvioMaritimo();
            return _envio.Buscar(numeroGuia);
        }

        public string Delete(string numeroGuia)
        {
            clsEnvioMaritimo _envio = new clsEnvioMaritimo();
            return _envio.Eliminar(numeroGuia);
        }
    }
}