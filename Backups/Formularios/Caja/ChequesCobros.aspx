﻿<%@ Page Title="Cheques" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChequesCobros.aspx.cs" Inherits="Tecnocuisine.ChequesCobros" %>

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
                                                                        <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy"  Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="input-group m-b">
                                                                <div class="row">
                                                                    <div class="col-md-2" style="margin-right: 15px;">
                                                                        <label style="margin-top: 5px;" class="col-md-4">Hasta</label>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox class="form-control" runat="server" type="date" ID="txtFechaVencimiento" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                             </div>
                                                            <div class="input-group m-b">
                                                              <div class="row">
                                                                    <div  class="col-md-2"  style="margin-right: 15px;">
                                                                        <label style="margin-top: 5px;" class="col-md-4">Cliente</label>
                                                                    </div>
                                                                 <div class="col-md-8">                                                            
                                                                    <datalist id="ListClientes" runat="server"></datalist>
                                                                    <asp:TextBox name="txtProveedor" type="text" ID="txtCliente" runat="server" 
                                                                        list="ContentPlaceHolder1_ListClientes" class="form-control" style="margin-left:15px;
                                                                        margin-bottom: 15px; width: 100%;"> 
                                                                    </asp:TextBox>
                                                                    <p id="ValivaCliente" class="text-danger hide">Ingresar un Cliente</p>
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ErrorMessage="El campo es obligatorio" ControlToValidate="txtCliente"
                                                                        ValidationGroup="grupoValidacion" SetFocusOnError="true" Font-Bold="true" ForeColor="Red">
                                                                   </asp:RequiredFieldValidator>                                                                     
                                                                </div>                                                                    
                                                              </div>
                                                            </div>
                                                            <%--  <div class="input-group m-b" 30%>
                                                                <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                        <%--<a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary" style="margin-right: 15px;" ValidationGroup=""><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>--%>
                                                        <%-- <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary" style="margin-right: 15px;"
                                                            ValidationGroup="grupoValidacion"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar</a>--%>

                                                       <%-- <asp:Button ID="btnFiltrar" runat="server" class="btn btn-primary" style="margin-right: 15px;"
                                                            ValidationGroup="grupoValidacion" OnClientClick="FiltrarVentas(event)"/>      --%>  
                                                        
                                                    <asp:LinkButton ID="lbtnAgregar" runat="server" class="btn btn-primary" 
                                                        style="margin-right: 15px;" OnClientClick="FiltrarVentas(event)" ValidationGroup="grupoValidacion">
                                                        <span class="fa fa-paper-plane"></span> Filtrar
                                                    </asp:LinkButton>
                                                    </div>
                                                </div>
                                                </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">
                                    <thead>
                                        <tr>
                                            <th style="width: 2%">#</th>
                                            <th style="width: 5%">fecha</th>
                                            <th style="width: 5%">Cliente</th>
                                            <th style="width: 10%">Descripcion</th>
                                            <th style="width: 5%">NºCheque</th>
                                            <th style="width: 15%">Banco</th>
                                            <th style="width: 5%">Cuenta</th>
                                            <th style="width: 5%">Cuit</th>
                                            <th style="width: 5%">Librador</th>
                                            <th style="width: 7%";text-align: right;>Importe</th>
                                            <th style="width: 2%"></th>
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

    


       
   

  
        <style>
            #editable_length {
                margin-left: 0px !important;
            }
        </style>
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

            saldo = document.getElementById("ContentPlaceHolder1_SaldoTotal").value;
            prov = document.getElementById("ContentPlaceHolder1_AliasCliente").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;

            }
            if (prov != "") {
                document.getElementById("ClienteSelec").innerText = prov;
            }
            establecerDiaHoy();
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

        function FiltrarVentas(e) {
            e.preventDefault()
            var textboxValue = document.getElementById('<%= txtCliente.ClientID %>').value;
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");
            if (textboxValue.trim() !== '') {
                window.location.href = "ChequesCobros.aspx?c=" + textboxValue.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
            } else {
                document.getElementById('ValivaCliente').classList.remove('hide')
            }
        }
    </script>

</asp:Content>
