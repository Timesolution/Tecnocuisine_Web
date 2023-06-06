<%@ Page Title="Tarjeta de Credito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TarjetaDeCredito.aspx.cs" Inherits="Tecnocuisine.TarjetaDeCredito" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px; padding-top: 0px;">
            <div class="p-xs">
                <div class="pull-left m-r-md">
                    <i class="fa fa-dollar text-navy mid-icon"></i>
                </div>
                <h2 id="DivSaldo" style="font-weight: bold;">0.00</h2>
                <span id="ClienteSelec">Entidad no Seleccionada</span>
            </div>
        </div>
        <div class="container-fluid">

            <div class="ibox float-e-margins">

                <div class="ibox-content">
                    <div style="margin-left: 0px; margin-right: 0px;" class="row">
                        <div class="col-md-10">

                            <div style="display: flex;">
                                <div class="input-group m-b" style="width: 30%">
                                    <div style="display: flex;">
                                        <span class="input-group-addon" style="padding-right: 15%;"><i style='color: black;' class='fa fa-search'></i></span>
                                        <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 100%" />
                                    </div>
                                </div>

                                <div class="input-group m-b" style="width: 30%">
                                    <label class="col-md-4" style="margin-top: 5px;">Desde</label>
                                    <div class="form-group" id="data_1">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" Style="margin-left: 0px; width: 120%;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="input-group m-b" style="width: 30%">
                                    <label style="margin-left: 15px; margin-top: 5px;" class="col-md-4">Hasta</label>
                                    <div class="form-group" id="data_2">
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                            <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento" Style="margin-left: 0px; width: 140%;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="input-group m-b" style="width: 30%; margin-left: 15px">

                                    <label style="margin-left: 15px; margin-top: 5px;" class="col-md-4">Entidad</label>
                                    <div class="col-md-6" style="padding: 0px;">
                                        <datalist id="ListClientes" runat="server"></datalist>
                                        <asp:DropDownList name="txtEntidad" type="text" ID="txtEntidad" runat="server" list="ContentPlaceHolder1_ListClientes" class="form-control" style="margin-left: 0px; margin-bottom: 15px; width: 140%;" />
                                        <p id="ValivaCliente" class="text-danger text-hide">Tienes que ingresar un Cliente</p>
                                    </div>
                                </div>
                                <%--  <div class="input-group m-b" 30%>
                                                                <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                                                            </div>--%>
                            </div>



                        </div>
                        <div class="col-md-2">
                            <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary" style="margin-right: 15px; float: right;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

<%--                            <a data-toggle="modal" data-backdrop="static" data-target="#modalAgregar" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>--%>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">
                                    <thead>
                                        <tr>
                                            <th style="width: 2%">#</th>
                                            <th style="width: 10%">Entidad</th>
                                            <th style="width: 8%">Fecha</th>
                                            <th style="width: 15%">Descripcion</th>
                                            <th style="width: 10%">Cliente</th>
                                            <th style="width: 7%">Acredita en</th>
                                            <th style="width: 7%">Acredita el</th>
                                            <th style="width: 7%";text-align: right;>Importe</th>
                                            <th style="width: 10%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phInsumos" runat="server"></asp:PlaceHolder>
                                           <asp:HiddenField ID="SaldoTotal" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="AliasCliente" runat="server"></asp:HiddenField>
                                    </tbody>
                                </table>
                            </div>

                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    


       
    </div>

  

    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtDescripcionInsumo.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'InsumosF', location.protocol + '//' + location.host + location.pathname);

        }
    </script>

    <script>
        $(document).ready(function () {

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
            let Clientes = document.getElementById("ContentPlaceHolder1_txtEntidad").value
            let proveedorValiva = document.getElementById("ValivaCliente");
            if (Clientes == "-1") {
                proveedorValiva.className = "text-danger"
                return 0;
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }

            window.location.href = "TarjetaDeCredito.aspx?c=" + Clientes.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }
    </script>

</asp:Content>
