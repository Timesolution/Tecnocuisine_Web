<%@ Page Title="Cta Cte" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CuentaCorriente.aspx.cs" Inherits="Tecnocuisine.Ventas.CuentaCorriente" %>

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
              #editable_length {
                margin-left: 0px !important;
}
    </style>
   <div class="wrapper wrapper-content">
       <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px;padding-top: 0px;">
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
                                                                    <div class="col-md-2"  style="margin-left: 15px;margin-right: 15px;">
                                                                        <label class="col-md-4" style="margin-top: 5px;">Desde</label>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <div class="form-group" id="data_1">
                                                                            <div class="input-group date">
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="input-group m-b">
                                                                <div class="row">
                                                                    <div  class="col-md-2"  style="margin-right: 15px;">
                                                                        <label style="margin-left: 15px; margin-top: 5px;" class="col-md-4">Hasta</label>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <div class="form-group" id="data_2" style="margin-left: 15px;">
                                                                            <div class="input-group date">
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="input-group m-b">
                                                                <div class="row">
                                                                    <div  class="col-md-2"  style="margin-right: 15px;">
                                                                <label style="margin-top: 5px;" class="col-md-4">Cliente</label>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                              
                                                                   
                                                                    <input name="txtProveedor" type="text" id="txtProveedor" list="ContentPlaceHolder1_ListaProveedores" class="form-control" style="margin-left:15px;margin-bottom: 15px; width: 100%;">
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
                                                        <a id="btnFiltrar"onclick="FiltrarCuentaCorriente()" class="btn btn-primary" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>

                    
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
                                                            <th style=" text-align: left; width: 8%;">Descripcion</th>
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
                        <asp:HiddenField id="AliasCliente" runat="server"></asp:HiddenField>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

   <%-- <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
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
                                        
                                            <label class="col-md-4">Buscar Cliente</label>
                                            <div class="col-md-6" style="padding: 0px;">
                                                <input name="txtProveedor" type="text" id="txtProveedor" onchange="ValidarProveedor()" list="ContentPlaceHolder1_ListaProveedores" class="form-control" style="margin-left: 0px; margin-bottom:15px;">
                                                 <p id="ValivaProveedor" class="text-danger text-hide">Tienes que ingresar un Proveedor</p>
                                            </div>
                                       


                                        </div>
                                      
                                       
                                     
                                   
                                
                          </div>
                        </div>


                       
                        <div class="modal-footer" style="border-color: transparent;">
                    <a id="btnFiltrar" onclick="FiltrarCuentaCorriente()" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Filtrar </a>
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


            //$('#data_1 .input-group.date').datepicker({
            //    todayBtn: "linked",
            //    keyboardNavigation: false,
            //    forceParse: false,
            //    calendarWeeks: true,
            //    autoclose: true,
            //    format: 'dd/mm/yyyy'
            //});
            //$('#data_2 .input-group.date').datepicker({
            //    todayBtn: "linked",
            //    keyboardNavigation: false,
            //    forceParse: false,
            //    calendarWeeks: true,
            //    autoclose: true,
            //    format: 'dd/mm/yyyy'
            //});
            saldo = document.getElementById("ContentPlaceHolder1_SaldoTotal").value;
            prov = document.getElementById("ContentPlaceHolder1_AliasCliente").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;
                document.getElementById("ClienteSelec").innerText = prov;
            }
            document.getElementById("txtBusqueda").addEventListener("input", buscarEnTabla);
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
        function FiltrarCuentaCorriente() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
            let Proveedor = document.getElementById("txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaCliente");
            if (Proveedor == "") {
                proveedorValiva.className = "text-danger"
                return
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }

            window.location.href = "CuentaCorriente.aspx?p=" + Proveedor.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }
        function ValidarProveedor() {
            let Proveedor = document.getElementById("txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaCliente");
            if (Proveedor == "") {
                proveedorValiva.className = "text-danger"
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }
        }
        function establecerDiaHoy() {
            var fechaActual = new Date();

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = fechaActual.getDate() + '/' + (fechaActual.getMonth() + 1) + '/' + fechaActual.getFullYear();
            var fechaFormateada2 = ("01" + '/' + "01" + '/' + "2000");
            // Establecer la fecha actual como valor predeterminado del DatePicker 
            $('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada2);
            $('#ContentPlaceHolder1_txtFechaVencimiento').datepicker('setDate', fechaFormateada);

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
