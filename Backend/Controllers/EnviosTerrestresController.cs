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
    public class EnviosTerrestresController : ApiController
    {
        public string Post([FromBody] EnvioTerrestre envio)
        {
            clsEnvioTerreste _envio = new clsEnvioTerreste();
            return _envio.Insertar(envio);
        }

        public string Put([FromBody] EnvioTerrestre envio)
        {
            clsEnvioTerreste _envio = new clsEnvioTerreste();
            return _envio.Actualizar(envio);
        }

        public EnvioTerrestre Get(string numeroGuia)
        {
            clsEnvioTerreste _envio = new clsEnvioTerreste();
            return _envio.Buscar(numeroGuia);
        }

        public string Delete(string numeroGuia)
        {
            clsEnvioTerreste _envio = new clsEnvioTerreste();
            return _envio.Eliminar(numeroGuia);
        }

    }
}