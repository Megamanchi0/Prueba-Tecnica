$("#dvMenu").load("../Paginas/Menu.html");

$("#btnInsertar").on("click", (e) => {
    e.preventDefault()
    EjecutarComando("POST")
})

$("#btnActualizar").on("click", (e) => {
    e.preventDefault()
    EjecutarComando("PUT")
})

$("#btnEliminar").on("click", (e) => {
    e.preventDefault()
    Eliminar()
})

$("#btnConsultar").on("click", (e) => {
    e.preventDefault()
    Consultar()
})


async function EjecutarComando(Comando) {
    const numeroGuia = $("#txtNumeroGuia").val();
    const documentoCliente = $("#txtDocumentoCliente").val();
    const idTipoProducto = $("#cboTipoDeProducto").val();
    const cantidad = $("#txtCantidad").val();
    const fechaRegistro = $("#txtFechaRegistro").val();
    const fechaEntrega = $("#txtFechaEntrega").val();
    const idPuerto = $("#txtIdPuerto").val();
    const numeroFlota = $("#txtNumeroFlota").val();
    const precioEnvio = $("#txtPrecioEnvio").val();

    const datosEnvio = {
        numeroGuia: numeroGuia,
        documentoCliente: documentoCliente,
        idTipoProducto,
        cantidad: cantidad,
        fechaRegistro: fechaRegistro,
        fechaEntrega: fechaEntrega,
        idPuerto: idPuerto,
        numeroFlota: numeroFlota,
        precioEnvio: precioEnvio,
    }

    try {
        const Respuesta = await fetch("http://localhost:57414/api/EnviosMaritimos",
            {
                method: Comando,
                mode: "cors",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(datosEnvio)
            });
        const Resultado = await Respuesta.json();
        $("#dvMensaje").html(Resultado);
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }
}
async function Consultar() {
    const numeroGuia = $("#txtNumeroGuia").val();

    try {
        const Respuesta = await fetch("http://localhost:57414/api/EnviosMaritimos?numeroGuia=" + numeroGuia,
            {
                method: "GET",
                mode: "cors",
                headers: { "Content-Type": "application/json" }
            });
        const Resultado = await Respuesta.json();

        $("#txtDocumentoCliente").val(Resultado.documentoCliente);
        $("#cboTipoDeProducto").val(Resultado.idTipoProducto);
        $("#txtCantidad").val(Resultado.cantidad);
        $("#txtFechaRegistro").val(Resultado.fechaRegistro.split('T')[0]);
        $("#txtFechaEntrega").val(Resultado.fechaEntrega.split('T')[0]);
        $("#txtIdPuerto").val(Resultado.idPuerto);
        $("#txtNumeroFlota").val(Resultado.numeroFlota);
        $("#txtPrecioEnvio").val(Resultado.valorTotal);

        $("#dvMensaje").html("");
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }
}
async function Eliminar() {
    const numeroGuia = $("#txtNumeroGuia").val();

    try {
        const Respuesta = await fetch("http://localhost:57414/api/EnviosMaritimos?numeroGuia=" + numeroGuia,
            {
                method: "DELETE",
                mode: "cors",
                headers: { "Content-Type": "application/json" }
            });
        const Resultado = await Respuesta.json();

        $("#dvMensaje").html(Resultado);
    }
    catch (error) {
        $("#dvMensaje").html(error);
    }

}