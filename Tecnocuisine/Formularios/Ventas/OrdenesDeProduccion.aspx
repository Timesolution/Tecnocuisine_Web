<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenesDeProduccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.OrdenesDeProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
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
                                                        <div class="btn-group" style="margin-right: 5px">
                                                            <linkbutton type="button" data-toggle="modal" href="#modalBusqueda" class="btn btn-success">Filtrar&nbsp;<i style="color: white" class="fa fa-filter"></i></linkbutton>
                                                        </div>
                                                        <a href="OrdenesDeProduccionABM.aspx" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>

                                                    </div>
                                                </div>

                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 5%;">Orden numero</th>
                                                            <th style="width: 5%;">Cliente</th>
                                                            <th style="text-align: left; width: 4%;">Fecha entrega</th>
                                                            <th style="width: 5%; text-align: left;">Estado</th>
                                                            <th style="width: 5%">Acciones</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phOrdenesProduccion" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>

                                                <asp:TextBox runat="server" ID="IDsOrdenesDeProduccion" Text="" Style="display: none"></asp:TextBox>
                                                <a href="CronogramaProduccion.aspx" class="btn btn-primary dim" target="_blank"
                                                    id="cronogramaProduccionURL" style="margin-right: 1%; float: right">Generar cronograma de produccion</a>


                                            </div>
                                        </div>
                                        <datalist id="ListaProveedores" runat="server">
                                        </datalist>
                                        <%--  <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                            <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                <a href="OrdenesDeProduccionABM.aspx" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento"
                                            Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <label class="col-md-4">Cliente</label>
                            <div class="col-md-6">
                                <div class="form-group" id="data_3">
                                    <%--<div class="input-group date">--%>
                                    <%--<span class="input-group-addon"><i class="fa fa-calendar"></i></span>--%>
                                    <datalist id="ListaNombreCliente" runat="server">
                                    </datalist>
                                    <asp:TextBox class="form-control" runat="server" ID="TxtClientes"
                                        Style="margin-left: 0px; width: 100%;" list="ContentPlaceHolder1_ListaNombreCliente"></asp:TextBox>
                                    <%--</div>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>



                <div class="modal-footer" style="border-color: transparent;">
                    <a id="btnFiltrar" onclick="FiltrarOrdenesProduccion()" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Filtrar </a>
                    <input type="hidden" name="ctl00$ContentPlaceHolder1$hiddenEditar" id="ContentPlaceHolder1_hiddenEditar">
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>

            </div>

        </div>
    </div>



    <div id="modalConfirmacion2" class="modal" tabindex="-1" role="dialog">
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
                    <%--<asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="btn btn-danger" OnClick="btnSi_Click" />--%>
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="btn btn-danger" OnClick="btnSi_Click" />
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>


    <%--<script src="../Scripts/plugins/toastr/toastr.min.js"></script>--%>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>


    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });



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

            establecerDiaHoy();

        });


        function FiltrarOrdenesProduccion() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
            let Cliente = document.getElementById("ContentPlaceHolder1_TxtClientes").value
            let numeroEncontrado = Cliente.match(/^\d+/);
            let IdCliente = parseInt(numeroEncontrado[0], 10)

            //console.log(numero);

            //let Proveedor = document.getElementById("txtProveedor").value
            //let proveedorValiva = document.getElementById("ValivaProveedor");
            //if (Proveedor == "") {
            //    proveedorValiva.className = "text-danger"
            //    return
            //} else {
            //    proveedorValiva.className = "text-danger text-hide"
            //}

            window.location.href = "OrdenesDeProduccion.aspx?c=" + IdCliente + "&FechaD=" + FechaD + "&FechaH=" + FechaH;
        }


        function establecerDiaHoy() {
            var fechaActual = new Date();

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fechaActual.getDate() + 1) + '/' + fechaActual.getMonth() + '/' + fechaActual.getFullYear();

            // Establecer la fecha actual como valor predeterminado del DatePicker 
            $('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada);
            $('#ContentPlaceHolder1_txtFechaVencimiento').datepicker('setDate', fechaFormateada);

        }

        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }


        function guardarIdOrdenDeProduccion(idOrdenDeProduccion) {



            let chk2 = document.getElementById("ContentPlaceHolder1_chkSeleccionar_" + idOrdenDeProduccion);

            if (chk2.checked == true) {


                if ((document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value) == "") {
                    document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value = idOrdenDeProduccion + ",";
                }
                else {
                    document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value += idOrdenDeProduccion + ",";
                }

                console.log(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value)

            }

            else {
                let idsOrdenes = document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value;
                // Crear una expresión regular para buscar el ID en la cadena
                let regex = new RegExp('\\b' + idOrdenDeProduccion + '\\b', 'g');
                // Reemplazar el ID encontrado en la cadena por una cadena vacía
                let nuevaCadena = idsOrdenes.replace(regex, '');
                // Quitar las comas duplicadas si es necesario
                nuevaCadena = nuevaCadena.replace(/,+/g, ',');
                // Quitar comas al principio y al final de la cadena
                nuevaCadena = nuevaCadena.replace(/^,|,$/g, '');
                // Asignar la nueva cadena como el valor del elemento de texto oculto
                document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value = nuevaCadena + ",";



                console.log(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value)
            }

            var enlace = document.getElementById("cronogramaProduccionURL");
            enlace.href = "CronogramaProduccion.aspx?ids=" + encodeURIComponent(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value);
            //IDsOrdenesDeProduccion
        }

    </script>
</asp:Content>
