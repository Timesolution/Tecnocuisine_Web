<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CuentaCorriente.aspx.cs" Inherits="Tecnocuisine.Compras.CuentaCorriente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        tr > td > a {
            color: black !important;
            text-decoration: none;
        }

            tr > td > a:hover, a:focus {
                color: black;
                text-decoration: none;
            }
    </style>
   <div class="wrapper wrapper-content">
       <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px;padding-top: 0px;">
                            <div class="p-xs">
                                <div class="pull-left m-r-md">
                                    <i class="fa fa-dollar text-navy mid-icon"></i>
                                </div>
                                <h2 id="DivSaldo" style="font-weight: bold;">0.00</h2>
                                <span id="ProveedorSelec">Proveedor no Seleccionado</span>
                            </div>
                        </div>
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="wrapper wrapper-content animated fadeInRight">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-content">
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
                                                                            <div>
                                                                                <p id="validaFechaHasta" class="text-danger text-hide">
                                                                                    * La fecha debe ser mayor a desde 
                                                                                </p>
                                                                            </div>
                                                                    </div>
                                                                </div>
                                                             </div>
                                                            <div class="input-group m-b">
                                                                <div class="row">
                                                                    <div  class="col-md-2"  style="margin-right: 15px;">
                                                                <label style="margin-top: 5px; margin-right:10px" class="col-md-4">Proveedores</label>
                                                                    </div>
                                                                    <div class="col-md-8" style="margin-left:10px">
                                                              
                                                                    <datalist id="ListClientes" runat="server"></datalist>
                                                                    <asp:TextBox name="txtProveedor" type="text" onchange="ValidarProveedor()" ID="txtProveedor" runat="server" list="ContentPlaceHolder1_ListClientes" class="form-control" style="margin-left:15px;margin-bottom: 15px; width: 100%;"> </asp:TextBox>
                                                                    <p id="ValivaProveedor"  class="text-danger text-hide" style="margin-left:13px">*Tienes que ingresar un Cliente</p>
                                                                </div>
                                                                    
                                                                </div>
                                                            </div>
                                                        </div>



                                                    </div>
                                                    <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                        <a id="btnFiltrar" onclick="FiltrarVentas()" class="btn btn-primary" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>
                                                        </div>
                                                </div>

                                      <%--          <div class="col-md-12">
                                                    <div style="display:flex; flex-direction: row; justify-content:center; align-items: center;" id="DivSaldo">
                                                        <h4 style="margin-right: 15px;">Saldo:</h4>
                                                    </div>

                                                </div>--%>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style=" text-align: right; width: 2%;">#</th>
                                                            <th  style="width: 2%;">Fecha</th>
                                                            <th style=" text-align: left; width: 8%;">Descripción</th>
                                                            <th style="width: 3%; text-align: right;">Debe</th>
                                                            <th style="width: 3%; text-align: right;">Haber</th>
                                                            <th style="width: 3%; text-align: right;">Saldo</th>

                                                            <th style="width: 2%";></th>

                                                    
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phCuentaCorriente" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                        <datalist id="ListaProveedores" runat="server">
                        </datalist>
                        <asp:HiddenField id="SaldoTotal" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="AliasProveedor" runat="server"></asp:HiddenField>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="FechaDesde" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="FechaHasta" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="ddlCliente" runat="server"></asp:HiddenField>
                </div>
            </div>
        </div>
    </div>

   



    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
        <style>
            #editable_length {
                margin-left: 0px !important;
            }
        </style>
    <script>
        $(document).ready(function () {

           let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
           let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

            if (fechad != "" && fechah != "") {

                establecerFechasSeleccionadas();
                //establecerProveedorSeleccionado();
            }
            else{
               establecerDiaHoy();
            }


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
            prov = document.getElementById("ContentPlaceHolder1_AliasProveedor").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;
                document.getElementById("ProveedorSelec").innerText = prov;
            }
            document.getElementById("txtBusqueda").addEventListener("input", buscarEnTabla);
            


            var oTable = $('#editable').dataTable({
                "bLengthChange": false,
                "pageLength": 100,// Establece la cantidad predeterminada de registros por página
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
        function FiltrarVentas() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");
            let Clientes = document.getElementById("ContentPlaceHolder1_txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaProveedor");

            let formValido = validarFecha();
             if(formValido == false){
                 return;
             }

            if (Clientes == "") {
                proveedorValiva.className = "text-danger"
                return 0;
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }

            window.location.href = "CuentaCorriente.aspx?c=" + Clientes.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }


         function establecerFechasSeleccionadas() {
          
             let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
          let fehcah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

          fechad = fechad.replaceAll("/", "-");
          fehcah = fehcah.replaceAll("/", "-");

          document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechad;
          document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fehcah
        }


      function establecerProveedorSeleccionado(){
          let ddlProveedor = document.getElementById("ContentPlaceHolder1_ddlCliente").value
          document.getElementById("ContentPlaceHolder1_txtProveedor").value = ddlProveedor;
      }

       function validarFecha(){
            let formValido = false;
            let fechaDesde = document.getElementById("<%=txtFechaHoy.ClientID%>").value;
            let fechaHasta = document.getElementById("<%=txtFechaVencimiento.ClientID%>").value;

            let fechaHastaValiva = document.getElementById("validaFechaHasta");
            if(fechaHasta < fechaDesde){
                fechaHastaValiva.className = "text-danger"
                formValido = false;
            }
            else{
                fechaHastaValiva.className = "text-danger text-hide"
                formValido = true;
            }
            return formValido
       }

        function ValidarProveedor() {
            let Proveedor = document.getElementById("ContentPlaceHolder1_txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaProveedor");
            if (Proveedor == "") {
                proveedorValiva.className = "text-danger"
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }
        }
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
        function buscarEnTabla() {
            // Obtener el valor del input de búsqueda
            var valorBusqueda = document.getElementById("txtBusqueda").value.toLowerCase();

            // Obtener las filas de la tabla
            var filasTabla = document.getElementById("editable").getElementsByTagName("tr");

            // Recorrer las filas de la tabla y ocultar/mostrar según la búsqueda
            for (var i = 1; i < filasTabla.length; i++) {
                var fila = filasTabla[i];
                var celdas = fila.getElementsByTagName("td");
                var coincide = false;

                for (var j = 0; j < celdas.length; j++) {
                    var celda = celdas[j];
                    if (celda.innerText.toLowerCase().indexOf(valorBusqueda) > -1) {
                        coincide = true;
                        break;
                    }
                }

                if (coincide) {
                    fila.style.display = "";
                } else {
                    fila.style.display = "none";
                }
            }
        }

        // Agregar evento de cambio en el input de búsqueda
        document.getElementById("txtBusqueda").addEventListener("input", buscarEnTabla);
    </script>

</asp:Content>
