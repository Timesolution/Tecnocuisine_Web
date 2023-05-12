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
                                                            <div class="input-group m-b" style="width:30%">
                                                                <div style="display: flex;">
                                                                    <span class="input-group-addon" style="padding-right: 15%;"><i style='color: black;' class='fa fa-search'></i></span>
                                                                    <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 100%" />
                                                                </div>
                                                            </div>

                                                            <div class="input-group m-b"  style="width:30%">
                                                                <label class="col-md-4">Desde</label>
                                                                <div class="form-group" id="data_1">
                                                                    <div class="input-group date">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" Style="margin-left: 0px; width: 120%;"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="input-group m-b"  style="width:30%">
                                                                <label style="margin-left: 15px" class="col-md-4">Hasta</label>
                                                                <div class="form-group" id="data_2">
                                                                    <div class="input-group date">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento" Style="margin-left: 0px; width: 140%;"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="input-group m-b"  style="width:30%;margin-left: 15px">

                                                                <label style="margin-left: 15px" class="col-md-4">Cliente</label>
                                                                <div class="col-md-6" style="padding: 0px;">
                                                                    <datalist id="ListClientes" runat="server"></datalist>
                                                                    <input name="txtProveedor" type="text" id="txtProveedor" list="ContentPlaceHolder1_ListClientes" class="form-control" style="margin-left: 0px; margin-bottom: 15px; width: 80%;">
                                                                    <p id="ValivaCliente" class="text-danger text-hide">Tienes que ingresar un Cliente</p>
                                                                </div>
                                                            </div>
                                                            <div class="input-group m-b" 30%>
                                                                <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                                                            </div>
                                                        </div>



                                                    </div>
                                                    <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                        <%-- <div class="btn-group" style="margin-right: 5px">
                                                            <linkbutton type="button" data-toggle="modal" href="#modalBusqueda" class="btn btn-success">Filtrar&nbsp;<i style="color: white" class="fa fa-filter"></i></linkbutton>

                                                        </div>--%>
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
    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });

            $('#data_1 .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'mm/dd/yyyy'
            });
            $('#data_2 .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'mm/dd/yyyy'
            });
            saldo = document.getElementById("ContentPlaceHolder1_SaldoTotal").value;
            prov = document.getElementById("ContentPlaceHolder1_AliasCliente").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;

            }
            if (prov != "") {
                document.getElementById("ClienteSelec").innerText = prov;
            }
            establecerDiaHoy();
        });
        function FiltrarVentas() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
            let Cliente = document.getElementById("txtProveedor").value
            let ClienteValiva = document.getElementById("ValivaProveedor");
            if (Cliente == "") {
                ClienteValiva.className = "text-danger"
                return
            } else {
                ClienteValiva.className = "text-danger text-hide"
            }

            window.location.href = "Ventas.aspx?p=" + Cliente.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }

        function establecerDiaHoy() {
            var fechaActual = new Date();

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fechaActual.getMonth() + 1) + '/' + fechaActual.getDate() + '/' + fechaActual.getFullYear();
            var fechaFormateada2 = ("01" + '/' + "01" + '/' + "2000");
            // Establecer la fecha actual como valor predeterminado del DatePicker 
            $('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada2);
            $('#ContentPlaceHolder1_txtFechaVencimiento').datepicker('setDate', fechaFormateada);

        }
        function FiltrarVentas() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
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

