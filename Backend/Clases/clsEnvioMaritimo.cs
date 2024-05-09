using Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Backend.Clases
{
    public class clsEnvioMaritimo
    {
        private LogisticaEntities logistica = new LogisticaEntities();

        /*
         Antes de insertar un envío, se debe verificar que sus campos
        de llaves foráneas existan en la base de datos
         */
        public string Insertar(EnvioMaritimo envio)
        {
            try
            {
                if (ValidarCampos(envio)!="")
                {
                    return ValidarCampos(envio);
                }
                
                CalcularDescuento(envio);
                logistica.EnvioMaritimoes.Add(envio);
                logistica.SaveChanges();
                return "Registro agregado correctamente";
                
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Actualizar(EnvioMaritimo envio)
        {
            try
            {
                if (ValidarCampos(envio) != "")
                {
                    return ValidarCampos(envio);
                }
                CalcularDescuento(envio);
                logistica.EnvioMaritimoes.AddOrUpdate(envio);
                logistica.SaveChanges();
                return "Registro actualizado correctamente";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Eliminar(string numeroGuia)
        {
            try
            {
                EnvioMaritimo envio = Buscar(numeroGuia);
                logistica.EnvioMaritimoes.Remove(envio);
                logistica.SaveChanges();
                return "Registro eliminado exitosamente";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public EnvioMaritimo Buscar(string numeroGuia)
        {
            try
            {
                EnvioMaritimo envio = logistica.EnvioMaritimoes.Find(numeroGuia);
                return envio;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        private void CalcularDescuento(EnvioMaritimo envio)
        {
            if (envio.cantidad > 10)
            {
                envio.descuento = envio.precioEnvio * (decimal)0.03;
                envio.valorTotal = envio.precioEnvio - envio.descuento;
            }
            else
            {
                envio.descuento = 0;
                envio.valorTotal = envio.precioEnvio;
            }
        }

        private string ValidarCampos(EnvioMaritimo envio)
        {
            string cadena = "";
            if (Buscar(envio.numeroGuia) != null)
            {
                cadena = "El número de guía ingresado ya está registrado";
            }
            else if (envio.numeroGuia.Length != 10)
            {
                cadena = "El número de la guía debe tener 10 caracteres";
            }
            else if (!ValidarDocumento(envio))
            {
                cadena= "El documento del cliente ingresado no está registrado";
            }
            else if (!ValidarPuerto(envio))
            {
                cadena= "El número de puerto ingresado, no existe";
            }
            else if (!ValidarFlota(envio))
            {
                cadena = "El número de la flota ingresado, no existe";
            }
            return cadena;
        }

        private Boolean ValidarDocumento(EnvioMaritimo envio)
        {
            return logistica.Clientes.Find(envio.documentoCliente) != null;
        }

        private Boolean ValidarPuerto(EnvioMaritimo envio)
        {
            return logistica.Puertoes.Find(envio.idPuerto) != null;
        }

        private Boolean ValidarFlota(EnvioMaritimo envio)
        {
            return logistica.Flotas.Find(envio.numeroFlota) != null;
        }

    }
}