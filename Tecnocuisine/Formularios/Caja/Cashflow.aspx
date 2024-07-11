<%@ Page Title="CashFlow" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Cashflow.aspx.cs" Inherits="Tecnocuisine.Formularios.Caja.Cashflow" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .table-wrapper {
            position: relative;
        }

            .table-wrapper table {
                table-layout: fixed;
            }

            .table-wrapper th:first-child,
            .table-wrapper td:first-child {
                position: sticky;
                left: 0;
                z-index: 1;
                background-color: white;
                width: 100px; /* Ajusta el valor en píxeles según el ancho deseado */
            }

            .table-wrapper th:nth-child(2),
            .table-wrapper td:nth-child(2) {
                position: sticky;
                left: 149.8px; /* Ajusta el valor en píxeles según el ancho de la primera columna */
                z-index: 1;
                background-color: white;
                width: 100px; /* Ajusta el valor en píxeles según el ancho deseado */
            }

            /* Agrega este estilo para las celdas de la segunda columna */
            .table-wrapper td:nth-child(2) {
                border-left: 1px solid #ddd; /* Establece el borde izquierdo */
                border-right: 1px solid #ddd; /* Establece el borde derecho */
            }

        #editable_length {
            margin-left: 0px !important;
        }
    </style>


    <div class="wrapper wrapper-content">
        <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px; padding-top: 0px;">

            <div class="row">
                <div class="col-lg-4">
                    <div class="p-xs">
                        <div class="pull-left m-r-md">
                            <i class="fa fa-dollar text-navy mid-icon"></i>
                        </div>
                        <h2 id="DivINGRESOSESTIMADOS" style="font-weight: bold;">0.00</h2>
                        <span>INGRESOS ESTIMADOS</span>
                    </div>
                </div>
                <div class="col-lg-4" style="border: 1px solid #e5e6e7;">
                    <div class="p-xs">
                        <div class="pull-left m-r-md">
                            <i class="fa fa-dollar text-navy mid-icon"></i>
                        </div>
                        <h2 id="DivEGRESOSESTIMADOS" style="font-weight: bold;">0.00</h2>
                        <span>EGRESOS ESTIMADOS</span>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="p-xs">
                        <div class="pull-left m-r-md">
                            <i class="fa fa-dollar text-navy mid-icon"></i>
                        </div>
                        <h2 id="DivRESULTADO" style="font-weight: bold;">0.00</h2>
                        <span>RESULTADO</span>
                    </div>
                </div>
            </div>
        </div>

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
                                                                    <input type="text" id="txtBusqueda" placeholder="Búsqueda..." class="form-control" style="width: 100%" />
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
                                                </div>

                                                <div class="col-lg-12" style="background-color: white">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <div class="table-responsive">
                                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="text-align: left; width: 15%">Tipo</th>
                                                                            <th style="width: 35%">Detalle</th>
                                                                            <th style="text-align: right; width: 25%">Importe</th>
                                                                            <th style="width: 20%"></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:PlaceHolder ID="phProductosyRecetas" runat="server"></asp:PlaceHolder>
                                                                    </tbody>
                                                                </table>
                                                            </div>
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
                                <div class="row">
                                    <div>
                                    </div>
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
    </div>
    <style>
        #CashFlowDiario th:not(.no-right-align) {
            text-align: right;
        }
    </style>


    <%-- Aca empieze el div del cashFlowDiario --%>
    <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px; padding-top: 0px;">
        <div class="row">
            <div class="col-lg-12" style="overflow-x: auto;">
                <div class="p-xs">
                    <div style="width: 100%; overflow-x: scroll;">
                        <div class="table-wrapper">
                            <table class="table table-striped table-bordered table-hover" id="CashFlowDiario" style="width: 100%; border-spacing: 0;">
                                <thead>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="phProductosyRecetasDiariosCabecera" runat="server"></asp:PlaceHolder>
                                    <asp:PlaceHolder ID="phProductosyRecetasDiarios" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalAgregar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 id="titleAdd" class="modal-title">Agregar</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <label class="col-sm-2 control-label editable">Fecha</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDate" type="date" Style="margin-left: 3%;" onchange="validarCashFlow()" class="form-control" runat="server"></asp:TextBox>
                            <p id="ValivaFecha" class="text-danger text-hide">Tienes que ingresar una fecha</p>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Tipo</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlOptionsTipo" runat="server" Style="margin-left: 3%;" class="form-control">
                                <asp:ListItem Text="Egreso" Value="Egreso"></asp:ListItem>
                                <asp:ListItem Text="Ingreso" Value="Ingreso"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Conceptos</label>
                        <div class="col-sm-8">
                            <datalist id="ListConceptos" runat="server"></datalist>
                            <asp:TextBox ID="txtConceptos" list="ContentPlaceHolder1_ListConceptos" onchange="validarCashFlow()" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                            <p id="ValivaConceptos" class="text-danger text-hide">Tienes que ingresar un Concepto valido</p>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Importe</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtImporte" onchange="FormatearNumImput(this)" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                            <p id="ValivaImporte" class="text-danger text-hide">Tienes que ingresar un Importe</p>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="TxtIDEditar" Text="0" Style="display: none"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardarModal" disabled="disabled" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>



    <div id="modalEliminar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Eliminar</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <h5>
                                    <asp:Label runat="server" ID="lblMensaje" Text="Esta seguro que desea eliminar Ingreso o Egreso?" Style="text-align: center"></asp:Label>
                                </h5>
                            </div>
                        </div>
                    </div>
                    <asp:TextBox runat="server" ID="txtIDIngresoEgreso" Text="0" Style="display: none"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="LinkButton1" class="buttonLoading btn btn-danger" OnClick="LinkButton1_Click"><i class="fa fa-check"></i>&nbsp;Eliminar </asp:LinkButton>
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>






    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.min.js"></script>

    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

            if (fechad != "" && fechah != "") {

                establecerFechasSeleccionadas();
            } else {

                establecerDiaHoy();
            }
            let ingresoTotal = document.getElementById("ContentPlaceHolder1_IngresoTotal").value;
            let EgresoTotal = document.getElementById("ContentPlaceHolder1_EgresoTotal").value;
            if (ingresoTotal != "") {

                document.getElementById("DivINGRESOSESTIMADOS").innerText = formatearNumero(ingresoTotal)
            } else {

                ingresoTotal = 0;
            }

            if (EgresoTotal != "") {

                document.getElementById("DivEGRESOSESTIMADOS").innerText = formatearNumero(EgresoTotal)
            } else {

                EgresoTotal = 0;
            }
            let resultado = 0;
            resultado = revertirNumero(ingresoTotal) - revertirNumero(EgresoTotal);

            document.getElementById("DivRESULTADO").innerText = formatearNumero(resultado)
            if (resultado >= 0) {
                document.getElementById("DivRESULTADO").style.color = "#1ab394"
            } else {
                document.getElementById("DivRESULTADO").style.color = "#ED5565"
            }

            let total = revertirNumero(ingresoTotal);
            let total2 = revertirNumero(EgresoTotal);
            data = [total, total2];
            /*editable*/

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


            var doughnutData = {
                labels: ["Ingresos", "Egresos"],
                datasets: [{
                    data: data,
                    backgroundColor: ["#1ab394", "#ED5565"]
                }]
            };

            var doughnutOptions = {
                responsive: false,
                legend: {
                    display: false
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
                            return formatearNumero(value); // Aplica formato con separador de miles
                        }
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart2").getContext("2d");
            new Chart(ctx4, { type: 'doughnut', data: doughnutData, options: doughnutOptions });

        });
        function obtenerRangoFechas() {
            var fechaActual = new Date(); // Obtiene la fecha actual
            var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1); // Primer día del mes actual
            var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0); // Último día del mes actual

            return {
                primerDia: primerDiaMes,
                ultimoDia: ultimoDiaMes
            };
        }

        function validarCashFlow() {
            let errores = 0;
            let importe = document.getElementById("ContentPlaceHolder1_txtImporte").value;
            let ValivaImporte = document.getElementById("ValivaImporte");
            if (importe == "NaN" || importe == "") {
                ValivaImporte.className = "text-danger"
                errores++;

            } else {
                ValivaImporte.className = "text-danger text-hide"
            }
            let Conceptos = document.getElementById("ContentPlaceHolder1_txtConceptos").value
            let ValivaConceptos = document.getElementById("ValivaConceptos");
            if (Conceptos == "" || !Conceptos.includes("-")) {
                ValivaConceptos.className = "text-danger"
                errores++;

            } else {
                ValivaConceptos.className = "text-danger text-hide"
            }

            let ValivaFecha = document.getElementById("ValivaFecha");
            let fecha = document.getElementById("ContentPlaceHolder1_txtDate").value
            if (fecha == "") {
                ValivaFecha.className = "text-danger"
                errores++;

            } else {
                ValivaFecha.className = "text-danger text-hide"
            }
            if (errores == 0) {
                document.getElementById("ContentPlaceHolder1_btnGuardarModal").removeAttribute("disabled");


            } else {

                let btn = document.getElementById("ContentPlaceHolder1_btnGuardarModal");
                btn.setAttribute("disabled", "disabled");

            }

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
        function establecerDiaHoy() {
            var fechas = obtenerRangoFechas();
            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.primerDia.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.ultimoDia.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

        }
        function FiltrarVentas() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");


            window.location.href = "Cashflow.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH;
        }

        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function revertirNumero(numeroFormateado) {
            var numero = parseFloat(numeroFormateado.replace(/,/g, ''));
            return numero;
        }
        function AbrirModalCashflow() {
            document.getElementById("titleAdd").innerHTML = 'Agregar'
            document.getElementById("<%=btnGuardarModal.ClientID%>").innerHTML = '<i class="fa fa-check"></i>&nbsp;Agregar'
            let fecha = new Date();
            let año = fecha.getFullYear();
            let mes = (fecha.getMonth() + 1).toString().padStart(2, '0');  // El mes se cuenta desde 0, por eso se suma 1
            let dia = fecha.getDate().toString().padStart(2, '0');
            document.getElementById('<%= txtDate.ClientID %>').value = año + '-' + mes + '-' + dia;
            document.getElementById('<%= txtImporte.ClientID %>').value = "";
            document.getElementById('<%= ddlOptionsTipo.ClientID %>').selectedIndex = 0;        
            document.getElementById('<%= txtConceptos.ClientID %>').value = "";
            $("#modalAgregar").modal("show");
        }

        function AbrirModalEliminarCashflow(IDEgresoIngreso) {
            document.getElementById('<%= txtIDIngresoEgreso.ClientID %>').value = IDEgresoIngreso;
            $("#modalEliminar").modal("show");
        }
        //Esta funcion edito el cashflow 
        function AbrirModalEditarCashflow(IDEgresoIngreso) {
            document.getElementById("titleAdd").innerHTML = 'Modificar'
            document.getElementById("<%=btnGuardarModal.ClientID%>").innerHTML = '<i class="fa fa-check"></i>&nbsp;Modificar'
            document.getElementById('<%= TxtIDEditar.ClientID %>').value = IDEgresoIngreso;
            RellenarCampoEditar(IDEgresoIngreso);
            $("#modalAgregar").modal("show");
        }

        function FormatearNumero2(input) {
            let value = input.value
            if (value != "" && !value.includes(",")) {
                input.value = formatearNumero(Number(input.value));

            } else {
                input.value = formatearNumero(revertirNumero(value));
            }
        }

        function FormatearNumImput(input) {
            if (input.value != "") {
                FormatearNumero2(input)
            }
            validarCashFlow();
        }

        //Esta funcion rellena los campos al editar
        function RellenarCampoEditar(IDEgresoIngreso) {
            fetch('Cashflow.aspx/RellenarCamposEditar', {
                method: 'POST',
                body: JSON.stringify({ idCashFlow: IDEgresoIngreso }),
                headers: { 'Content-Type': 'application/json' },
            })

                .then(response => response.json())
                .then(data => {
                    // Asignar los valores del objeto cashflow a los textbox
                    console.log(data.d)
                    let cashFlowObject = JSON.parse(data.d);
                    document.getElementById('<%= txtDate.ClientID %>').value = cashFlowObject.Fecha;
                    document.getElementById('<%= txtImporte.ClientID %>').value = cashFlowObject.Importe;
                    document.getElementById('<%= ddlOptionsTipo.ClientID %>').value = cashFlowObject.Tipo;
                    document.getElementById('<%= txtConceptos.ClientID %>').value = cashFlowObject.Concepto;
                    document.getElementById("ContentPlaceHolder1_btnGuardarModal").removeAttribute("disabled");
                })
                .catch(error => {
                    // Manejo de errores...
                    console.error(error);
                });
        }

    </script>
</asp:Content>

