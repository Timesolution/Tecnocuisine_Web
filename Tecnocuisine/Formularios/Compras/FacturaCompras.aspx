<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacturaCompras.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.FacturaCompras" %>

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
                                                <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                                    <div class="col-md-10">

                                                        <div class="input-group m-b">
                                                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>
                                                    </div>
                                                   <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                        <div class="btn-group" style="margin-right:5px">
                                                             <linkbutton type="button" data-toggle="modal" href="#modalBusqueda" class="btn btn-success">Filtrar&nbsp;<i style="color:white" class="fa fa-filter"></i></linkbutton>
                                               
                                                        </div>
                                                       <a href="Compras.aspx" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>

                                                    </div>

                                                 <%--   <div class="col-md-2">

                                                       
                                                  <div class="btn-group pull-right" style="height: 100%">
                                                <linkbutton type="button" data-toggle="modal" href="#modalBusqueda" class="btn btn-success">Filtrar&nbsp;<i style="color:white" class="fa fa-filter"></i></linkbutton>
                                               <a href="Compras.aspx" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>

                                                  </div>
                                                        </div>--%>
                                                </div>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style=" text-align: right; width: 2%;">#</th>
                                                            <th  style="width: 5%;">Fecha</th>
                                                            <th  style="width: 5%;">Fecha Vencimiento</th>

                                                            <th style=" text-align: left; width: 4%;">Proveedor</th>
                                                            <th style="width: 5%; text-align: right;">Importe</th>
                                                            <th style="width: 2%";></th>
                                                    
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phProductos" runat="server"></asp:PlaceHolder>
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
                </div>
            </div>
        </div>
    </div>

        <div id="modalEliminar" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Confirmar eliminacion</h4>
                </div>
                <div class="modal-body">
                    <p>
                        Esta seguro que lo desea eliminar?
                    </p>
                </div>
                <div class="modal-footer">
                    <button  id="btnEliminar" Text="Eliminar" class="btn btn-danger" onclick="EliminarFactura(event)" >Eliminar</button>
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:HiddenField ID="idEliminar" runat="server" />
                </div>
            </div>
        </div>
    </div>


   <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
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
                                        
                                            <label class="col-md-4">Buscar Proveedor</label>
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
            </div>



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
            prov = document.getElementById("ContentPlaceHolder1_AliasProveedor").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;

            }
            if (prov != "") {
                document.getElementById("ProveedorSelec").innerText = prov;
            }
            establecerDiaHoy();


            var oTable = $('#editable').dataTable({
                "bLengthChange": true,
                "pageLength": 25, // Establece la cantidad predeterminada de registros por página
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
        function EliminarFac(id) {
            document.getElementById("ContentPlaceHolder1_idEliminar").value = id;
            $("#modalEliminar").modal("show");
        }

        function EliminarFactura(e) {
            e.preventDefault();
            id = document.getElementById("ContentPlaceHolder1_idEliminar").value;
            $.ajax({
                method: "POST",
                url: "FacturaCompras.aspx/EliminarFac",
                data: '{id: "' + id
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                error: (error) => {
                    toastr.error('Error al generar factura')
                    let btn = document.getElementById("btnGuardar")
                    btn.disabled = false;
                },
                success: (respuesta) => {
                    window.location.reload();
                },
            });
        }
        function FiltrarCuentaCorriente() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
            let Proveedor = document.getElementById("txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaProveedor");
            if (Proveedor == "") {
                proveedorValiva.className = "text-danger"
                return
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }

            window.location.href = "FacturaCompras.aspx?p=" + Proveedor.split("-")[0].trim() + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }
        function establecerDiaHoy() {
            var fechaActual = new Date();

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fechaActual.getMonth() + 1) + '/' + fechaActual.getDate() + '/' + fechaActual.getFullYear();

            // Establecer la fecha actual como valor predeterminado del DatePicker 
            $('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada);
            $('#ContentPlaceHolder1_txtFechaVencimiento').datepicker('setDate', fechaFormateada);

        }
    </script>

</asp:Content>