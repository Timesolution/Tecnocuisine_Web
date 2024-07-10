<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EntregasE.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.EntregasE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        #editable span {
            display: none;
        }
    </style>
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
                                                            <input type="text" id="txtBusqueda" placeholder="Búsqueda..." class="form-control" style="width: 90%" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                        <div class="btn-group">
                                                            <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle" style="margin-right: 3%; float: right" aria-expanded="true"><i class="fa fa-file-text-o"></i></button>
                                                            <ul class="dropdown-menu" x-placement="bottom-start" style="position: absolute; top: 33px; left: 0px; will-change: top, left;">
                                                                <li><a class="dropdown-item" onclick="ValidarOptionSelected(event)">Facturar entrega</a></li>
                                                            </ul>
                                                        </div>
                                                        <a href="Entregas.aspx" class="btn btn-primary dim" title="Agregar entrega"
                                                            style="margin-right: 1%; float: right"><i class='fa fa-plus'></i>
                                                        </a>
                                                    </div>
                                                </div>
                                                <table class="table-striped table-bordered table-hover " id="editable" style="width:100%">
                                                    <thead>
                                                        <tr>

                                                            <th style="max-width: 100px">Fecha Entrega</th>
                                                            <th>PROVEEDOR </th>
                                                            <%--<th>Sector</th>--%>
                                                            <th>Código Entrega</th>
                                                            <th> Estado Facturado</th>
                                                            <th style="max-width: 100px"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phEntregas" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <%--seccion modales --%>

    <div id="modalConfirmacion2" class="modal" role="dialog">
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
                    <button type="button" class="btn btn-white" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:Button runat="server" ID="btnEliminar" OnClick="btnSi_Click" Text="Eliminar" class="buttonLoading btn btn-danger" />
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script>

        $(document).ready(function () {
            $('.dataTables-example').dataTable({
                responsive: true,
                "dom": 'T<"clear">lfrtip',
                "tableTools": {
                    "sSwfPath": "js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
                }
            });
            /* Init DataTables */
            var oTable = $('#editable').dataTable();
            var oTable2 = $('#editable2').dataTable();

            /* Apply the jEditable handlers to the table */
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
                "height": "100%",
                "pageLength": 25
            });

            /* Apply the jEditable handlers to the table */
            oTable2.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable2.fnGetPosition(this);
                    oTable2.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable2.fnGetPosition(this)[2]
                    };
                },

                "width": "90%",
                "height": "100%"
            });
            $("#editable_filter").appendTo("#editable_length");

            $("#editable_filter").css('display', 'none');
            $("#editable_filter").css('padding-left', '5%');
            var parent = $("#editable_length")[0].parentNode;
            parent.className = 'col-sm-12';
            parent.style = 'display:none';
            var div = document.getElementById('editable_filter');
            var button = document.createElement('a');
            /* button.id = "btnAgregar";*/
            button.style.float = "right";
            button.style.marginRight = "1%";
            //button.setAttribute("type", "button");
            button.setAttribute("href", "ProductosABM.aspx");
            //button.setAttribute("href", "#modalAgregar");
            //button.setAttribute("onclick", "vaciarFormulario()");
            //button.setAttribute("data-toggle", "modal");
            button.setAttribute("class", "btn");

            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';

            //var filter = $("#editable_length");
            //filter[0].id = 'editable_length2';


            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });
        });


    </script>
    <script type="text/javascript">
        function openModal() {
            $('#ModalNuevoPais').modal('show');
        }
        function abrirdialog(id) {
            $('#modalConfirmacion2').modal('show');
            document.getElementById('<%=hiddenID.ClientID%>').value = id;
        }


        function ValidarOptionSelected(e) {
            e.preventDefault();
            console.log("ENTRE")
            var rows = $('#editable tbody tr'); // Obtener todas las filas de la tabla
            var proveedores = []; // Crear un array para almacenar los proveedores de las filas seleccionadas
            var ids = []; // Crear un array para almacenar los IDs de las filas seleccionadas

            rows.each(function () {
                var chkBox = $(this).find('input[type="checkbox"]'); // Obtener el input checkbox de la fila actual
                if (chkBox.is(':checked')) { // Verificar si el checkbox está seleccionado
                    var proveedor = $(this).find('td:eq(1)').text().trim(); // Obtener el valor de la segunda columna (proveedor)
                    proveedores.push(proveedor); // Agregar el proveedor al array
                    var id = $(this).attr('id').split('_')[1]; // Obtener el ID de la fila sin el prefijo "ContextPlaceHolder_"
                    ids.push(id); // Agregar el ID al array
                }
            });

            // Verificar si todos los proveedores seleccionados son iguales
            var proveedorUnico = proveedores[0];
            var todosIguales = true;
            for (var i = 1; i < proveedores.length; i++) {
                if (proveedores[i] != proveedorUnico) {
                    todosIguales = false;
                    break;
                }
            }
            let idsString = "";
            for (let item of ids) {
                idsString += item += "-";
            }

            if (todosIguales) {
              
                window.location.href = "Compras.aspx?i=" + idsString;
            } else {
                toastr.error('No puede seleccionar "Facturar Entrega" con diferentes proveedores.')
            }

            // Imprimir los IDs de las filas seleccionadas
        }
    </script>
</asp:Content>
