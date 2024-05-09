using Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Backend.Clases
{
    public class clsEnvioTerreste
    {
        private LogisticaEntities logistica = new LogisticaEntities();

        /*
         Antes de insertar un envío, se debe verificar que sus campos
        de llaves foráneas existan en la base de datos
         */
        public string Insertar(EnvioTerrestre envio)
        {
            try
            {
                if (Buscar(envio.numeroGuia) != null)
                {
                    return "El número de guía ingresado ya está registrado";
                }else if (ValidarCampos(envio)!="")
                {
                    return ValidarCampos(envio);
                }

                CalcularDescuento(envio);
                logistica.EnvioTerrestres.Add(envio);
                logistica.SaveChanges();
                return "Registro agregado correctamente";
                
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public string Actualizar(EnvioTerrestre envio)
        {
            try
            {
                if (Buscar(envio.numeroGuia)==null)
                {
                    return "El número de guía ingresado no está registrado";
                }
                if (ValidarCampos(envio) != "")
                {
                    return ValidarCampos(envio);
                }
                CalcularDescuento(envio);
                logistica.EnvioTerrestres.AddOrUpdate(envio);
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
                EnvioTerrestre envio = Buscar(numeroGuia);
                logistica.EnvioTerrestres.Remove(envio);
                logistica.SaveChanges();
                return "Registro eliminado exitosamente";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        public EnvioTerrestre Buscar(string numeroGuia)
        {
            try
            {
                EnvioTerrestre envio = logistica.EnvioTerrestres.Find(numeroGuia);
                return envio;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        private void CalcularDescuento(EnvioTerrestre envio)
        {
            if (envio.cantidad>10)
            {
                envio.descuento = envio.precioEnvio * (decimal)0.05;
                envio.valorTotal = envio.precioEnvio - envio.descuento;
            }
            else
            {
                envio.descuento = 0;
                envio.valorTotal = envio.precioEnvio;
            }
        }

        private string ValidarCampos(EnvioTerrestre envio)
        {
            string cadena = "";
            if (envio.numeroGuia.Length != 10)
            {
                cadena = "El número de la guía debe tener 10 caracteres";
            }
            else if (!ValidarDocumento(envio))
            {
                cadena = "El documento del cliente ingresado no está registrado";
            }
            else if (!ValidarBodega(envio))
            {
                cadena = "El número de bodega ingresado, no existe";
            }
            else if (!ValidarVehiculo(envio))
            {
                cadena = "La placa del vehículo ingresado, no existe";
            }
            return cadena;
        }
        private Boolean ValidarDocumento(EnvioTerrestre envio)
        {
            return logistica.Clientes.Find(envio.documentoCliente) != null;
        }

        private Boolean ValidarBodega(EnvioTerrestre envio)
        {
            return logistica.Bodegas.Find(envio.idBodega) != null;
        }

        private Boolean ValidarVehiculo(EnvioTerrestre envio)
        {
            return logistica.Vehiculoes.Find(envio.placaVehiculo) != null;
        }

    }
}