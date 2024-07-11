<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstadoDeResultado.aspx.cs" Inherits="Tecnocuisine.Formularios.Caja.EstadoDeResutaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="ibox float-e-margins">
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="wrapper wrapper-content animated fadeInRight">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content">
                                            <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                                <div class="col-md-10">
                                                    <div style="display: flex;">

                                                        <div class="input-group m-b">
                                                            <div style="display: flex;">
                                                                <span class="input-group-addon" style="padding-right: 15%;"><i style='color: black;' class='fa fa-search'></i></span>
                                                                <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 100%" />
                                                            </div>
                                                        </div>
                                                        <div class="input-group m-b row">
                                                            <div class="row">
                                                                <div class="col-md-2" style="margin-left: 15px; margin-right: 15px;">
                                                                    <label class="col-md-4" style="margin-top: 5px;">Desde</label>
                                                                </div>
                                                                <div class="col-md-8">

                                                                    <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="input-group m-b">
                                                            <div class="row">
                                                                <div class="col-md-2" style="margin-right: 15px;">
                                                                    <label style="margin-top: 5px;" class="col-md-4">Hasta</label>
                                                                </div>
                                                                <div class="col-md-8">
                                                                    <asp:TextBox class="form-control" runat="server" type="date" ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                    <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                                                    <a onclick="AbrirModalCashflow()" class="btn btn-primary dim" data-toggle="tooltip" data-placement="top"
                                                        data-original-title="Agregar Nueva Entrega" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                                                </div>

                                                <div class="col-lg-12" style="background-color: white">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <table class="table table-striped table-bordered table-hover " id="editable">
                                                                <thead>
                                                                    <tr>
                                                                        <%--<th style="text-align: left; width: 15%">Tipo</th>--%>
                                                                        <th style="width: 35%">Tipo rubro</th>
                                                                        <th style="text-align: right; width: 25%">Importe</th>
                                                                        <th style="text-align: right; width: 20%">Real</th>
                                                                        <th style="text-align: right; width: 20%">Plan</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:PlaceHolder ID="phRubroRecaudacionMensual" runat="server"></asp:PlaceHolder>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                        <div class="col-lg-6">
                                                            <canvas id="doughnutChart2" style="margin: 18px auto 0px; display: block; height: 300px; width: 600px"></canvas>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div>
                                </div>
                            </div>
                            <asp:HiddenField ID="IngresoTotal" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="EgresoTotal" runat="server"></asp:HiddenField>

                            <asp:HiddenField ID="AliasCliente" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="FechaDesde" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="FechaHasta" runat="server"></asp:HiddenField>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <script>

        $(document).ready(function () {

            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value


            if (fechad != "" && fechah != "") {

                establecerFechasSeleccionadas();
            } else {

                establecerDiaHoy();
            }

            var oTable = $('#editable').dataTable({
                "bLengthChange": false,
                "pageLength": 100, // Establece la cantidad predeterminada de registros por página
                "lengthMenu": [25, 50, 87, 100], // Opciones de cantidad de registros por página
            });

            oTable.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable.fnGetPosition(this);
                    oTable.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable.fnGetPosition(this)[2]
                    };
                },
                "width": "90%",
                "height": "100%"
            });

            $("#editable_filter").css('display', 'none');

            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });

        });

        function establecerDiaHoy() {
            var fechas = obtenerRangoFechas();
            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.primerDia.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.ultimoDia.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

        }

        function obtenerRangoFechas() {
            var fechaActual = new Date(); // Obtiene la fecha actual
            var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1); // Primer día del mes actual
            var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0); // Último día del mes actual

            return {
                primerDia: primerDiaMes,
                ultimoDia: ultimoDiaMes
            };
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

        function establecerFechasSeleccionadas() {
            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fehcah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

            fechad = fechad.replaceAll("/", "-");
            fehcah = fehcah.replaceAll("/", "-");

            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechad;
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fehcah
        }

        function FiltrarVentas() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");
            window.location.href = "EstadoDeResultado.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH;
        }

    </script>


</asp:Content>
