<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Ventas.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Ventas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px; padding-top: 0px;">
            <div class="p-xs">
                <div class="pull-left m-r-md">
                    <i class="fa fa-dollar text-navy mid-icon"></i>
                </div>
                <h2 id="DivSaldo" style="font-weight: bold;">0.00</h2>
                <span id="ClienteSelec">Cliente no Seleccionado</span>
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
                                                    <div class="input-group m-b">
                                                        <div class="row">
                                                            <div class="col-md-2" style="margin-right: 15px;">
                                                                <label style="margin-top: 5px;" class="col-md-4">Cliente</label>
                                                            </div>
                                                            <div class="col-md-8">

                                                                <datalist id="ListClientes" runat="server"></datalist>
                                                                <input name="txtProveedor" type="text" id="txtProveedor" list="ContentPlaceHolder1_ListClientes" class="form-control" style="margin-left: 15px; margin-bottom: 15px; width: 100%;">
                                                                <p id="ValivaCliente" class="text-danger text-hide">Tienes que ingresar un Cliente</p>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <%--  <div class="input-group m-b" 30%>
                                                                <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                                                            </div>--%>
                                                </div>



                                            </div>
                                            <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                                                <a href="../Ventas/GenerarVenta.aspx" class="btn btn-primary dim" data-toggle="tooltip" data-placement="top"
                                                    data-original-title="Agregar Nueva Entrega" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                                            </div>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover " id="editable">
                                            <thead>
                                                <tr>

                                                    <th style="text-align: right; width: 2%">ID</th>
                                                    <th style="width: 7%">Fecha</th>
                                                    <th style="text-align: left; width: 10%">Cliente</th>
                                                    <th style="text-align: left; width: 10%">Forma de Pago</th>
                                                    <th style="text-align: left; width: 10%">Factura</th>
                                                    <th style="text-align: right; width: 5%">Numero</th>
                                                    <th style="text-align: right; width: 5%">Costo</th>
                                                    <th style="text-align: right; width: 8%">Precio Venta</th>
                                                    <th style="width: 5%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="phProductosyRecetas" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <asp:HiddenField ID="SaldoTotal" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="AliasCliente" runat="server"></asp:HiddenField>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--  <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Busqueda</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel2">


                        <div role="form" class="form-horizontal col-md-12">

                            <label class="col-md-4">Desde</label>
                            <div class="col-md-6">
                                <div class="form-group" id="data_1">
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <label class="col-md-4">Hasta</label>
                            <div class="col-md-6">
                                <div class="form-group" id="data_2">
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <label class="col-md-4">Cliente</label>
                            <div class="col-md-6" style="padding: 0px;">
                                  <datalist id="ListClientes" runat="server"></datalist>  
                                <input name="txtProveedor" type="text" id="txtProveedor" list="ContentPlaceHolder1_ListClientes" class="form-control" style="margin-left: 0px; margin-bottom: 15px;">
                                <p id="ValivaCliente" class="text-danger text-hide">Tienes que ingresar un Cliente</p>
                            </div>



                        </div>





                    </div>
                </div>



                <div class="modal-footer" style="border-color: transparent;">
                    <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Filtrar </a>
                    <input type="hidden" name="ctl00$ContentPlaceHolder1$hiddenEditar" id="ContentPlaceHolder1_hiddenEditar">
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>

            </div>

        </div>
    </div>--%>
    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>
         <style>
            #editable_length {
                margin-left: 0px !important;
            
}
        </style>
    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });

            saldo = document.getElementById("ContentPlaceHolder1_SaldoTotal").value;
            prov = document.getElementById("ContentPlaceHolder1_AliasCliente").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;

            }
            if (prov != "") {
                document.getElementById("ClienteSelec").innerText = prov;
            }
            establecerDiaHoy();


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
            $("#dataTables_length").css('display', 'none');


            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });

          



        });


        function establecerDiaHoy() {
            var fechaActual = new Date();
            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fechaActual.getFullYear() + '/' + (fechaActual.getMonth() + 1) + '/' + fechaActual.getDate())
            // Establecer la fecha actual como valor predeterminado del DatePicker 
            //$('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada);
            //$('#ContentPlaceHolder1_txtFechaHoy').datepicker('todayBtn', true);
            var partes = fechaFormateada.split('/');
            var dia = partes[2];
            var mes = partes[1];
            var anio = partes[0];

            if (dia < 10) {
                dia = '0' + dia;
            }

            if (mes < 10) {
                mes = '0' + mes;
            }

            fechafinal = anio + '-' + mes + '-' + dia;

            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechafinal;
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fechafinal;

        }
        function FiltrarVentas() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");
            let Clientes = document.getElementById("txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaCliente");
            if (Clientes == "") {
                proveedorValiva.className = "text-danger"
                return
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }

            window.location.href = "Ventas.aspx?c=" + Clientes.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }
    </script>
</asp:Content>

