﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PedidosOrdenes.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.PedidosOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
    </style>

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="wrapper wrapper-content animated fadeInRight">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins" style="padding: 1rem">
                                    <div class="ibox-content">
                                        <div style="margin-left: 0px; margin-right: 0px;">
                                            <div style="display: flex; justify-content: space-between; column-gap: 5rem; margin-bottom: 3rem; padding-right: 1rem; flex-wrap: nowrap">

                                                <%--Seccion filtros--%>
                                                <div style="width: 100%">

                                                    <strong style="font-size: 2rem">Filtrar</strong>

                                                    <div style="margin-top: 1rem; display: none">
                                                        <div class="input-group m-b">
                                                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" />
                                                        </div>
                                                    </div>

                                                    <%--Botones ayer hoy mañana pasado--%>
                                                    <div>
                                                        <%--<a id="btnAyer" onclick="filtrartTransferenciasAyer()" class="btn btn-warning"
                                                                    title="filtrar ayer" style="height: 32px; margin-left: 10px">Ayer
                                                                </a>--%>
                                                        <asp:Button runat="server" ID="btnAyer" OnClientClick="setFecha(-1); return false" class="btn btn-default"
                                                            title="filtrar ayer" Style="height: 32px;" Text="Ayer"></asp:Button>

                                                        <%--<a id="btnHoy" onclick="filtrartTransferenciasHoy()" class="btn btn-warning"
                                                                    title="filtrar hoy" style="height: 32px; margin-left: 10px">Hoy
                                                                </a>--%>
                                                        <asp:Button runat="server" ID="btnHoy" OnClientClick="setFecha(0); return false" class="btn btn-default"
                                                            title="filtrar hoy" Style="height: 32px;" Text="Hoy"></asp:Button>

                                                        <%--<a id="btnMañana" onclick="filtrartTransferenciasMañana()" class="btn btn-warning"
                                                                    title="filtrar mañana" style="height: 32px; margin-left: 10px">Mañana
                                                                </a>--%>
                                                        <asp:Button runat="server" ID="btnMañana" OnClientClick="setFecha(1); return false" class="btn btn-default"
                                                            title="filtrar mañana" Style="height: 32px;" Text="Mañana"></asp:Button>

                                                        <%--<a id="btnPasado" onclick="filtrartTransferenciasPasado()" class="btn btn-warning"
                                                                    title="filtrar pasado" style="height: 32px; margin-left: 10px">Pasado 
                                                                </a>--%>
                                                        <asp:Button runat="server" ID="btnPasado" OnClientClick="setFecha(2); return false" class="btn btn-default"
                                                            title="filtrar pasado" Style="height: 32px;" Text="Pasado"></asp:Button>
                                                    </div>

                                                    <%--Fila--%>
                                                    <div style="display: flex; justify-content: flex-start; column-gap: 2rem; width: 100%">

                                                        <%--Fecha Desde--%>
                                                        <div style="width: 33%">
                                                            <label style="margin-top: 5px;">Desde:</label>
                                                            <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy"
                                                                data-date-format="dd/mm/yyyy">
                                                            </asp:TextBox>
                                                        </div>

                                                        <%--Fecha Hasta--%>
                                                        <div style="width: 33%">
                                                            <label style="margin-top: 5px;">Hasta:</label>
                                                            <asp:TextBox class="form-control" runat="server" type="date"
                                                                ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy">
                                                            </asp:TextBox>
                                                        </div>

                                                        <%--Origen--%>
                                                        <div style="width: 31%">
                                                            <label style="margin-top: 5px;">Origen</label>
                                                            <div>
                                                                <asp:DropDownList ID="ddlOrigen" runat="server"
                                                                    CssClass="chosen-select form-control"
                                                                    DataTextField="CountryName" DataValueField="CountryCode"
                                                                    Data-placeholder="Seleccione Origen..." Width="100%">
                                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <%--Destino--%>
                                                        <div style="width: 31%">
                                                            <label style="margin-top: 5px;">Destino</label>
                                                            <div>
                                                                <asp:DropDownList ID="ddlDestino" runat="server"
                                                                    CssClass="chosen-select form-control"
                                                                    DataTextField="CountryName" DataValueField="CountryCode"
                                                                    Data-placeholder="Seleccione Origen..." Width="100%">
                                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <%--Estado--%>
                                                        <div style="width: 33%">
                                                            <label style="margin-top: 5px;">Estado</label>
                                                            <asp:DropDownList ID="ddlEstado" runat="server"
                                                                CssClass="chosen-select form-control"
                                                                DataTextField="CountryName" DataValueField="CountryCode"
                                                                Data-placeholder="Seleccione Estado..." Width="100%">
                                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <%--Boton Filtrar--%>
                                                        <div style="align-self: flex-end; justify-self: flex-start">
                                                            <%--<a id="btnfiltrar" onclick="filtrarordenesproduccion()" class="btn btn-primary" title="filtrar" style="width: 100%">
                                                               <i class="fa fa-search"></i>&nbsp;Filtrar
                                                           </a>--%>
                                                            <%--<asp:LinkButton ID="btnfiltrar" runat="server" OnClick="btnfiltrar_Click" OnClientClick="limpiarUrl()" title="filtrar" Style="width: 100%; margin-bottom: 0" CssClass="btn btn-primary btn-with-icon">
                                                                         <i class="fa fa-search"></i>
                                                            </asp:LinkButton>--%>
                                                        </div>

                                                    </div>


                                                    <%--Fila 2--%>
                                                    <div style="display: flex; justify-content: flex-start; column-gap: 2rem; width: 100%">



                                                        <%--<label style="margin-top: 5px; margin-left: 10px; margin-right: 10px">Sector</label>
                                                                <div>
                                                                    <asp:DropDownList ID="ddlSectorUsuario" runat="server"
                                                                        CssClass="chosen-select form-control"
                                                                        DataTextField="CountryName" DataValueField="CountryCode"
                                                                        Data-placeholder="Seleccione Estado..." Width="100%">
                                                                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>--%>
                                                    </div>

                                                </div>

                                                <%--Seccion busqueda avanzada--%>
                                                <%--    <div style="width: 50%">
                                                        </div>--%>
                                            </div>

                                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table class="table table-hover no-margins table-bordered" style="width: 100%; margin-top: 30px" id="editable">
                                                                <thead>
                                                                    <tr style="height: 20px">
                                                                        <th style="width: 5%; margin-top: 20px; height: 10px; padding-left: 4px">Fecha</th>
                                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Origen</th>
                                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Destino</th>
                                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Orden</th>
                                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Estado</th>
                                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Acciones</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tableTransferencias">
                                                                    <asp:PlaceHolder ID="phTransferencias" runat="server"></asp:PlaceHolder>
                                                                </tbody>
                                                            </table>

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>--%>

                                            <table class="table table-hover no-margins table-bordered" style="width: 100%; margin-top: 30px" id="editable">
                                                <thead>
                                                    <tr style="height: 20px">
                                                        <th style="width: 5%; margin-top: 20px; height: 10px; padding-left: 4px">Fecha</th>
                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Origen</th>
                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Destino</th>
                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Orden</th>
                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Estado</th>
                                                        <th style="width: 5%; margin-top: 20px; padding-left: 4px">Acciones</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tableTransferencias">
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>



    <div id="modalOrigenDestino" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Origen/Destino</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel6">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <div style="display: flex; justify-content: space-between">
                                            <h5>Origen</h5>
                                            <h5 id="modalOrigenDestino_fecha"></h5>
                                        </div>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <table class="table table-hover no-margins table-bordered" style="" id="tableDatosTransferencia">
                                            <thead>
                                                <tr>
                                                    <td style="width: 25%"><strong>Sector Productivo</strong></td>
                                                    <td style="width: 35%"><strong>Producto</strong></td>
                                                    <td class="text-right" style="width: 14%"><strong>Cantidad</strong></td>
                                                    <td class="text-right" style="width: 14%"><strong>Confirmada</strong></td>
                                                    <td style="width: 10%"><strong>Acciones</strong></td>
                                                    <%--<td><strong>SectorDestino</strong></td>--%>
                                                    <%--<td><strong>Orden destino</strong></td>--%>
                                                    <%--<td><strong>Cliente destino</strong></td>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="tableOrigen">
                                            </tbody>
                                        </table>
                                        <asp:HiddenField ID="idsDatosTransferencias" runat="server" />

                                        <div style="text-align: right; margin-top: 10px">
                                            <asp:Button ID="btnConfirmar" runat="server"
                                                OnClientClick="confirmarTransferencia(); return false" class="btn btn-primary"
                                                title="Confirmar" Text="Confirmar" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="modalDetallePedidos" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Origen/Destino</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel7">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Origen</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered" id="tablePedidos">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                    <td class="text-right" style="display: none"><strong>Confirmada</strong></td>
                                                    <td><strong>Producto Destino</strong></td>
                                                    <td><strong>SectorDestino</strong></td>
                                                    <td><strong>Orden destino</strong></td>
                                                    <td><strong>Cliente destino</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tableDetallePedidos">
                                            </tbody>
                                        </table>

                                        <%--    <div style="text-align: right; margin-top:10px">
                                        <asp:Button ID="btnConfirmar" runat="server" 
                                            OnClick="btnConfirmar_Click" class="btn btn-primary" 
                                            title="Confirmar" Text="Confirmar"
                                         />
                                    </div>  --%>
                                        <div style="text-align: right; margin-top: 10px">
                                            <asp:Button ID="Button1" runat="server"
                                                OnClientClick="guardarDatosTransferencia(); return false" class="btn btn-primary"
                                                title="Guardar" Text="Guardar" />
                                        </div>
                                        <asp:HiddenField ID="idTransferencia" runat="server" />
                                        <asp:HiddenField ID="transferencias" Value="" runat="server" />
                                        <asp:HiddenField ID="idsPedidos" Value="" runat="server" />
                                        <asp:HiddenField ID="cantidadesPedidos" Value="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script src="../../js/bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/2.1.5/i18n/es-ES.json"></script>

    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
            //establecerDiaHoy();

            var table = $('#editable').DataTable({
                "ajax": {
                    "url": "PedidosOrdenes.aspx/GetTransferencias",
                    "type": "POST",
                    "dataType": "json",
                    contentType: "application/json; charset=utf-8",
                    "dataSrc": function (json) {
                        // Accede al array dentro de data.d.data
                        return json.d.data; // Aquí especificas que los datos están en json.d.data
                    },
                    "error": function (xhr, status, error) {
                        console.log('Error:', error);
                        //console.log('Response Text:', xhr.responseText);
                    }
                },
                "columns": [
                    { "data": "fecha" },
                    { "data": "origen" },
                    { "data": "destino" },
                    { "data": "orden" },
                    { "data": "estado" },
                    {
                        "data": "id",
                        "render": function (data, type, row) {
                            return "<button style='background-color: transparent' class='btn btn-xs btnVerDetalle' onclick='verDetalleTranferencia(\"" + data + "\", \"" + row.fecha + "\"); return false;'>"
                                + "<span title='Detalle'><i class='fa fa-exchange' style='color: black;'></i></span>"
                                + "</button>";
                        }
                    }
                ],
                "order": [[0, 'desc']], // Ordenar por la primera columna (fecha) en orden descendente
                "searching": true, // Ocultar el buscador
                "lengthChange": false, // Ocultar el filtro de cantidad de registros
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.1.5/i18n/es-ES.json',
                },
                "initComplete": function (settings, json) {
                    $('#editable_filter').hide(); // Ocultar el campo de búsqueda global después de la inicialización
                }
            });

            ////////////////////////////////////////

            // Filtro personalizado
            $.fn.dataTable.ext.search.push(
                function (settings, data, dataIndex) {
                    var fechaDesde = $('#<%= txtFechaHoy.ClientID %>').val();
                    var fechaHasta = $('#<%= txtFechaVencimiento.ClientID %>').val();
                    var origenText = $('#<%= ddlOrigen.ClientID %> option:selected').text();
                    var destinoText = $('#<%= ddlDestino.ClientID %> option:selected').text();
                    var estadoText = $('#<%= ddlEstado.ClientID %> option:selected').text();

                    var fecha = data[0]; // columna Fecha, fecha de la fila recorrida (ej 31/08/2024)
                    var origen = data[1]; // columna Origen
                    var destino = data[2]; // columna Destino
                    var estado = data[4]; // columna Estado

                    var fechaFilaAISO = formatDateToISO(fecha); // Se convierte la fecha de la fila a formato ISO (2024-08-31) para compararla con los filtros

                    // Comprobar rango de fechas
                    var dateInRange = (!fechaDesde || fechaFilaAISO >= fechaDesde) && (!fechaHasta || fechaFilaAISO <= fechaHasta);

                    // Comparar origen, destino y estado
                    var origenMatch = (origenText === "Seleccione..." || origenText === origen);
                    var destinoMatch = (destinoText === "Seleccione..." || destinoText === destino);
                    var estadoMatch = (estadoText === "Seleccione..." || estadoText === estado);

                    return dateInRange && origenMatch && destinoMatch && estadoMatch;
                }
            );

            // Aplicar filtros cuando cambian
            $('#<%= txtFechaHoy.ClientID %>, #<%= txtFechaVencimiento.ClientID %>, #<%= ddlOrigen.ClientID %>, #<%= ddlDestino.ClientID %>, #<%= ddlEstado.ClientID %>').on('change', function () {
                table.draw();
            });

            ////////////////////////////////////


            var updateOutput = function (e) {
                var list = e.length ? e : $(e.target),
                    output = list.data('output');
                if (window.JSON) {
                    //output.val(window.JSON.stringify(list.nestable('serialize')));
                } else {
                    output.val('JSON browser support required for this demo.');
                }
            };

            $('#nestable').nestable({
                group: 1
            }).on('change', updateOutput);

            $('#nestable2').nestable({
                group: 1
            }).on('change', updateOutput);

            updateOutput($('#nestable').data('output', $('#nestable-output')));
            updateOutput($('#nestable2').data('output', $('#nestable2-output')));

            $('#nestable-menu').on('click', function (e) {
                var target = $(e.target),
                    action = target.data('action');
                if (action === 'expand-all') {
                    $('.dd').nestable('expandAll');
                }
                if (action === 'collapse-all') {
                    $('.dd').nestable('collapseAll');
                }
            });
        });


        // Función para convertir fecha en formato dd/mm/yyyy o dd-mm-yyyy a objeto Date
        function parseDate(dateString) {
            var parts = dateString.split(/[/\-]/); // Dividir por / o -
            // Asegúrate de ajustar el año si es necesario
            return new Date(parts[2], parts[1] - 1, parts[0]); // Meses en JavaScript van de 0 a 11
        }

        /**
 * Convierte una fecha en formato 'dd/mm/yyyy' o 'dd-mm-yyyy' a 'yyyy-mm-dd'.
 * @param {string} dateString - La fecha en formato 'dd/mm/yyyy' o 'dd-mm-yyyy'.
 * @returns {string} La fecha en formato 'yyyy-mm-dd'.
 */
        function formatDateToISO(dateString) {
            // Dividir la fecha en partes usando '/' o '-'
            var parts = dateString.split(/[/\-]/);

            // Verificar que la fecha tiene las partes correctas
            if (parts.length !== 3) {
                throw new Error('Formato de fecha inválido. Use "dd/mm/yyyy" o "dd-mm-yyyy".');
            }

            var day = parts[0];
            var month = parts[1];
            var year = parts[2];

            // Asegurarse de que el mes sea 2 dígitos
            month = month.length === 1 ? '0' + month : month;

            // Asegurarse de que el día sea 2 dígitos
            day = day.length === 1 ? '0' + day : day;

            // Devolver la fecha en formato 'yyyy-mm-dd'
            return `${year}-${month}-${day}`;
        }


        function establecerDiaHoy() {
            var fechas = obtenerRangoFechas();
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.diaActual.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.diaActual.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

        }


        function obtenerRangoFechas() {
            var fechaActual = new Date();
            let diaActual = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());
            var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1);
            var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0);

            return {
                primerDia: primerDiaMes,
                ultimoDia: ultimoDiaMes,
                diaActual: diaActual
            };
        }


        async function verDetalleTranferencia(idTransferencia, fecha) {
            // Espera el resultado de la función VisibilidadBotonConfirmar
            const disabledInputs = await VisibilidadBotonConfirmar(idTransferencia);


            $.ajax({
                method: "POST",
                url: "PedidosOrdenes.aspx/verDetallesTransferencia",
                data: JSON.stringify({ idTransferencia: idTransferencia }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    console.log("Error");
                },
                success: function (data) {
                    const arraydetalleTransferencia = data.d.split(";").filter(Boolean);
                    document.getElementById('tableOrigen').innerHTML = "";
                    document.getElementById('<%= idTransferencia.ClientID %>').value = idTransferencia;
                    let cont = 0;
                    let estado = false;
                    arraydetalleTransferencia.forEach(function (detalleTransferencia) {
                        cont++;
                        //let cantidadAConfirmar = "";
                        const partesDetalle = detalleTransferencia.split(",").filter(Boolean);
                        document.getElementById('<%= idsDatosTransferencias.ClientID %>').value += partesDetalle[0];
                        const sectorOrigen = partesDetalle[1];
                        const ProductoOrigen = partesDetalle[2];
                        const cantidad = partesDetalle[3];
                        const cantidadAConfirmar = "<input id=\"cantAproducir_" + cont + "\" " +
                            "style=\"width: 100%; text-align: right;\" " +
                            "placeholder=\"Cantidad\" " +
                            "value=\"" + partesDetalle[4] + "\" " +
                            (disabledInputs ? "disabled" : "") + " />";



                        let btnVerDetalle =
                            '<a id="btnVerDetalle_' + idTransferencia + '" class="btn btn-xs" style="background-color: transparent;" data-toggle="modal" href="#modalConfirmacion2" onclick="verDetallePedidos(\'' + idTransferencia + '\', \'' + ProductoOrigen + '\');">' +
                            '<span title="Ver detalle"><i class="fa fa-exchange" style="color: black;"></i></span>'
                        '</a>';


                        let plantillaDetalleTransferencia = `
                         <tr id="${partesDetalle[0]}">
                             <td>${sectorOrigen}</td>
                             <td>${ProductoOrigen}</td>
                             <td style="text-align: right;">${cantidad}</td>
                             <td style="text-align: right;">${cantidadAConfirmar}</td>
                             <td style="text-align: left;">${btnVerDetalle}</td>
                         </tr>
                     `;

                        document.getElementById('tableOrigen').innerHTML += plantillaDetalleTransferencia;
                    });

                    //TODO: traer fecha de la data
                    document.getElementById('modalOrigenDestino_fecha').innerHTML = 'Fecha: ' + fecha;

                    $("#modalOrigenDestino").modal("hide");
                    setTimeout(function () {
                        $("#modalOrigenDestino").modal("show");
                    }, 500);
                }
            });
        }

        // Mostrar boton confirmar solo si el estado de la transferencia es "A confirmar"
        function VisibilidadBotonConfirmar(idTransferencia) {
            return GetIdEstadoTransferencia(idTransferencia).then(function (response) {
                var estadoTransferencia = response.d;

                if (estadoTransferencia === 2) { // A Confirmar
                    document.getElementById('<%= btnConfirmar.ClientID %>').style.display = 'inline-block';
            return false; 
        } else {
            document.getElementById('<%= btnConfirmar.ClientID %>').style.display = 'none';
            return true;
        }
    }).fail(function (xhr, status, error) {
        console.error("Error al llamar al método del servidor: ", error);
        return true; // Retornar false en caso de error
    });
        }


        function GetIdEstadoTransferencia(idTransferencia) {
            return $.ajax({
                method: "POST",
                url: "PedidosOrdenes.aspx/GetEstadoTransferencia",
                data: JSON.stringify({ idTransferencia: idTransferencia }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }

        function verDetallePedidos(idTransferencia, ProductoOrigen) {
            $.ajax({
                method: "POST",
                url: "PedidosOrdenes.aspx/verDetallesPedidos",
                data: JSON.stringify({ idTransferencia: idTransferencia, ProductoOrigen: ProductoOrigen }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    console.log("Error");
                },
                success: function (data) {

                    const arraydetalleTransferencia = data.d.split(";").filter(Boolean);
                    document.getElementById('tableDetallePedidos').innerHTML = "";
                    document.getElementById('<%= idTransferencia.ClientID %>').value = idTransferencia;
                    let cont = 0;
                    document.getElementById('<%= idsPedidos.ClientID %>').value = "";
                    let estado = false;
                    arraydetalleTransferencia.forEach(function (detalleTransferencia) {
                        cont++;
                        let cantidadAConfirmar = "";
                        const partesDetalle = detalleTransferencia.split(",").filter(Boolean);
                        document.getElementById('<%= idsPedidos.ClientID %>').value += partesDetalle[0] + ",";
                        const SectorOrigen = partesDetalle[1];
                        const ProductoOrigen = partesDetalle[2];
                        const cantidadOrigen = partesDetalle[3];
                        const estadoTransferencia = partesDetalle[10];



                        // if(estadoTransferencia == "Confirmado" || estadoTransferencia == "A confirmar") 
                        // {
                        //    cantidadAConfirmar = "<input id=\"" + "cantAproducir_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Cantidad\" value=\"" + partesDetalle[11] + "\" oninput=\"validarTextBox(this);\" />";
                        //    estado = false;
                        // }
                        //
                        // else
                        // {
                        //    cantidadAConfirmar = "<input id=\"cantAproducir_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Cantidad\" value=\"" + partesDetalle[11] + "\" disabled />";
                        //    estado = true;
                        // }
                        const productoDestino = partesDetalle[4];
                        const sectorDestino = partesDetalle[5];
                        const orden = partesDetalle[6];
                        const razonSocialActual = partesDetalle[7];


                        if (estado == true) {
                            document.getElementById('<%= btnConfirmar.ClientID %>').disabled = true;
                        }
                        else {
                            document.getElementById('<%= btnConfirmar.ClientID %>').disabled = false;
                        }

                        let plantillaDetalleTransferencia = `
                          <tr>
                              <td>${SectorOrigen}</td>
                              <td>${ProductoOrigen}</td>
                              <td style="text-align: right;">${cantidadOrigen}</td>
                              <td>${productoDestino}</td>
                              <td>${sectorDestino}</td>
                              <td>${orden}</td>
                              <td>${razonSocialActual}</td>
                          </tr>
                      `;


                        document.getElementById('tableDetallePedidos').innerHTML += plantillaDetalleTransferencia;
                    });
                    $("#modalDetallePedidos").modal("show")
                }
            });
        }


        function formatearFechas(fecha) {

            var partes = fecha.split('/');
            var dia = partes[2];
            var mes = partes[1];
            var anio = partes[0];

            if (dia < 10) {
                dia = '0' + dia;
            }

            if (mes < 10) {
                mes = '0' + mes;
            }

            return fechafinal = anio + '-' + mes + '-' + dia;

        }


        function guardarDatosTransferencia() {
                //document.getElementById('<%= btnConfirmar.ClientID %>').disabled = true;
            document.getElementById('<%= cantidadesPedidos.ClientID %>').value = "";
            let idTransferencia = document.getElementById('<%= idTransferencia.ClientID %>').value;

            for (var i = 0; i < document.getElementById('tableDetallePedidos').rows.length; i++) {
                let cantidad = document.getElementById('tableDetallePedidos').rows[i].cells[3].querySelector('input').value;

                document.getElementById('<%= cantidadesPedidos.ClientID %>').value += cantidad + ",";

            }

            let idsPedidos = document.getElementById('<%= idsPedidos.ClientID %>').value
            let cantidadesPedidos = document.getElementById('<%= cantidadesPedidos.ClientID %>').value

            $.ajax({
                method: "POST",
                url: "PedidosOrdenes.aspx/guardarDatosTransferencia",
                data: JSON.stringify({ idsPedidos: idsPedidos, cantidadesPedidos: cantidadesPedidos, idTransferencia: idTransferencia }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    toastr.error("La transferencia no pudo ser confirmada.", "Error");
                    document.getElementById('<%= btnConfirmar.ClientID %>').disabled = false;
                },
                success: function (response) {

                    if (response.d == 1) {
                        toastr.success("Transferencia confirmada con exito!", "Exito")
                        $("#modalDetallePedidos").modal("hide");
                        //$("#modalOrigenDestino").modal("hide");
                        verDetalleTranferencia(idTransferencia);

                        //setTimeout(function() {
                        //   //window.location.reload();
                        //
                        //}, 1000);

                    }
                    else {
                        toastr.error("La transferencia no pudo ser confirmada.", "Error");
                        document.getElementById('<%= btnConfirmar.ClientID %>').disabled = false;

                    }
                }
            });


        }


        function limpiarUrl() {
            // Obtener la URL actual sin parámetros
            var urlBase = window.location.pathname;
            window.history.replaceState({}, document.title, urlBase);
        }


        function validarTextBox(input) {
            // Obtener el valor del input
            var valor = input.value;

            // Expresión regular para validar que solo contenga números y '.'
            var regex = /^[0-9.]+$/;

            // Validar el valor
            if (!regex.test(valor)) {
                // Mostrar un mensaje de error o tomar alguna acción
                alert("La cantidad debe contener solo números y el carácter '.'");
                // Establecer el estado a false si es necesario
                estado = false;
            } else {
                // Establecer el estado a true si es válido
                estado = true;
            }
        }

        function confirmarTransferencia() {
            //document.getElementById('<%= btnConfirmar.ClientID %>').disabled = true;
            let idTransferencia = document.getElementById('<%= idTransferencia.ClientID %>').value;

            var rows = document.querySelectorAll('#tableOrigen tr');
            var tableData = [];

            rows.forEach(function (row) {
                var cells = row.getElementsByTagName('td');
                if (cells.length > 0) {
                    var rowData = {
                        id: row.id,
                        sectorProductivo: cells[0].innerText.trim(),
                        producto: cells[1].innerText.trim(),
                        cantidad: cells[2].innerText.trim(),
                        confirmada: cells[3].getElementsByTagName('input')[0].value.trim()
                    };
                    tableData.push(rowData);
                }
            });

            var data = {
                idTransferencia: idTransferencia,
                tableData: tableData
            };



            $.ajax({
                method: "POST",
                url: "PedidosOrdenes.aspx/confirmarTransferencia",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    toastr.error("La transferencia no pudo ser confirmada.", "Error");
                },
                success: function (response) {

                    if (response.d > 0) {
                        var table = $('#editable').DataTable();
                        table.ajax.reload(null, false); // El segundo parámetro false evita reiniciar la paginación

                        toastr.success("Transferencia confirmada con exito!", "Exito")

                        // Obtener la URL actual
                        <%--var urlActual = window.location.href;
                        var urlSinParametros = urlActual.split('?')[0];

                        var parametros =
                            "a=1" + "&" +
                            "fDesde=" + document.getElementById('<%=txtFechaHoy.ClientID%>').value + "&" +
                            "fHasta=" + document.getElementById('<%=txtFechaVencimiento.ClientID%>').value + "&" +
                            "origen=" + document.getElementById('<%=ddlOrigen.ClientID%>').value + "&" +
                            "destino=" + document.getElementById('<%=ddlDestino.ClientID%>').value + "&" +
                            "estado=" + document.getElementById('<%=ddlEstado.ClientID%>').value;
                        // Recargar la página con la nueva URL
                        window.location.href = urlSinParametros + "?" + parametros;--%>


                        //OTRA MANERA
                        <%--// RECARGAR TABLA
                        var data = {
                            fDesde: document.getElementById('<%=txtFechaHoy.ClientID%>').value,
                            fHasta: document.getElementById('<%=txtFechaVencimiento.ClientID%>').value,
                            origen: document.getElementById('<%=ddlOrigen.ClientID%>').value,
                            destino: document.getElementById('<%=ddlDestino.ClientID%>').value,
                            estado: document.getElementById('<%=ddlEstado.ClientID%>').value,
                        };
                        $.ajax({
                            method: "POST",
                            url: "PedidosOrdenes.aspx/GetTransferenciasConFiltros",
                            data: JSON.stringify(data),
                            contentType: "application/json",
                            dataType: "json",
                            error: function (error) {
                            },
                            success: function (response) {
                                if (response.d > 0) {
                                }
                                else {
                                }
                            }
                        });--%>

                        //dibujar en js la tabla

                        $("#modalOrigenDestino").modal("hide");
                    }
                    else {
                        toastr.error("La transferencia no pudo ser confirmada.", "Error");
                    }
                }
            });


        }

    </script>

    <script>
        // Esta funcion cambiara el valor de los inputs de fecha agregandole los dias que indique el boton seleccionado
        function setFecha(diasASumar) {
            // Obtener la fecha actual
            var fecha = new Date();

            // Sumar los días especificados
            fecha.setDate(fecha.getDate() + diasASumar);

            // Formatear la fecha a YYYY-MM-DD
            var dia = ('0' + fecha.getDate()).slice(-2);
            var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
            var anio = fecha.getFullYear();

            // Establecer el valor del textbox de fecha
            var txtFechaDesde = document.getElementById('<%=txtFechaHoy.ClientID%>');
            var txtFechaHasta = document.getElementById('<%=txtFechaVencimiento.ClientID%>');

            txtFechaDesde.value = anio.toString() + "-" + mes.toString() + "-" + dia.toString();
            txtFechaHasta.value = anio.toString() + "-" + mes.toString() + "-" + dia.toString();


            // Llamar al evento onchange del select
            var event = new Event('change');
            txtFechaDesde.dispatchEvent(event);
            txtFechaHasta.dispatchEvent(event);
        }

    </script>

    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText = "Produccion / Pedidos";
        });
    </script>
</asp:Content>
