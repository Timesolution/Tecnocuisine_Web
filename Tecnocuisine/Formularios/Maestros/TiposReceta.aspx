<%@ Page Title="Tipos De Receta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposReceta.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.TiposReceta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">

            <div class="ibox float-e-margins">

                <div class="ibox-content">

                    <div style="margin-left: 0px; margin-right: 0px;" class="row">
                        <div class="col-md-10">
                            <div class="input-group m-b">
                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                <input type="text" id="txtBusqueda" placeholder="Búsqueda..." class="form-control" style="width: 90%" />
                            </div>
                        </div>

                        <div class="col-md-2">
                            <a data-toggle="modal" data-backdrop="static" data-target="#modalAgregar"
                                class="btn btn-primary dim" title="Agregar tipo"
                                style="margin-right: 1%; float: right"><i class='fa fa-plus'></i>
                            </a>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%">Codigo</th>
                                            <th style="width: 40%">Descripcion</th>
                                            <th style="width: 30%"></th>
                                        </tr>
                                    </thead>

                                    <tbody id="pHolderInsumo">
                                        <asp:PlaceHolder ID="phTipos" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <%--Modal Agregar--%>
        <div id="modalAgregar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Agregar</h4>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-1">
                                <label style="color: red;" class="danger">*</label>
                            </div>
                            <label class="col-sm-2 control-label editable">Descripción</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDescripcionTipo" class="form-control" runat="server" />

                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary"
                            OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                        <asp:HiddenField ID="hiddenEditar" runat="server" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    </div>
                </div>
            </div>
        </div>



        <div id="modalEditar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Editar</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-1">
                                <label style="color: red;" class="danger">*</label>
                            </div>
                            <label class="col-sm-2 control-label editable">Descripción</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDescripcion" class="form-control" runat="server" />

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="btnEditarTipo"
                            class="buttonLoading btn btn-primary"
                            OnClientClick="editarTipo(); return false"><i class="fa fa-check"></i>&nbsp;Editar </asp:LinkButton>
                        <asp:HiddenField ID="idTipoEditar" runat="server" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>


                    </div>
                </div>
            </div>
        </div>


        <div id="modalConfirmacion2" class="modal fade" tabindex="-1" role="dialog">
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
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />
                        <asp:HiddenField ID="hiddenID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            $('#editable').DataTable({
                "language": {
                    "decimal": "",
                    "emptyTable": "No hay datos disponibles en la tabla",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                    "infoEmpty": "Mostrando 0 a 0 de 0 entradas",
                    "infoFiltered": "(filtrado de _MAX_ entradas totales)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Mostrar _MENU_ entradas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "No se encontraron registros coincidentes",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "aria": {
                        "sortAscending": ": activar para ordenar la columna de manera ascendente",
                        "sortDescending": ": activar para ordenar la columna de manera descendente"
                    }
                }
            });
        });

    </script>

    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }

        function openModalEditar(idInsumo) {

            precargarCamposModalEditar(idInsumo);
            setTimeout(function () {
                $('#modalEditar').modal('show');
            }, 500);
        }

        function editarTipo() {
            let idTipo = document.getElementById('<%= idTipoEditar.ClientID %>').value
            let Descripcion = document.getElementById('<%= txtDescripcion.ClientID %>').value

            $.ajax({
                method: "POST",
                url: "TiposReceta.aspx/EditarTipo",
                data: JSON.stringify({ idTipo: idTipo, Descripcion: Descripcion }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    toastr.error("No se pudo modificar el tipo de receta.", "Error");
                },
                success: function (response) {
                    if (response.d == 3) {
                        $('#modalEditar').modal('hide')
                        window.location.href = "../../Formularios/Maestros/TiposReceta.aspx";
                    }
                    else if (response.d == 4) {
                        toastr.warning('No se pudo editar el tipo de receta.', 'Error')
                        $('#modalEditar').modal('hide')
                    }
                    else if (response.d == 5) {
                        toastr.error('Ya existe un tipo de receta con esa descripcion.', 'Error')
                        $('#modalEditar').modal('hide')
                    }


                }
            });
        }

        function CargarInsumos() {

            $.ajax({
                method: "POST",
                url: "InsumosF.aspx/CargarInsumos",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let insumos = respuesta.d
                    if (insumos != null && insumos != '[]') {
                        let ins = JSON.parse(insumos)

                        let pHolderInsumo = ''
                        ins.forEach(element => {

                            pHolderInsumo += ` <tr>
                                                <td>${element.Id}</td>
                                                <td>${element.Descripcion}</td>
                                                <td>
                                                    <a class="btn btn-xs" style="background-color: transparent; margin-right: 10px;" onclick="openModalEditar(${element.Id})" title="Editar">
                                                        <span><i style="color: black;" class="fa fa-pencil"></i></span>
                                                    </a>
                                                    <a class="btn btn-xs" style="background-color: transparent; margin-right: 10px;" onclick="abrirdialog(${element.Id})" data-toggle="modal" href="#modalConfirmacion2" title="Eliminar">
                                                        <span><i style="color: red;" class="fa fa-trash"></i></span>
                                                    </a>
                                                </td>
                                               </tr>`
                        })
                        //document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('pHolderInsumo').innerHTML = pHolderInsumo
                    } else {
                        //document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('pHolderInsumo').innerHTML = ""
                    }
                }
            });

        }

        function precargarCamposModalEditar(idTipo) {
            $.ajax({
                method: "POST",
                url: "TiposReceta.aspx/GetTipoById",
                data: JSON.stringify({ idTipo: idTipo }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    toastr.error("La transferencia no pudo ser confirmada.", "Error");
                },
                success: function (response) {
                    document.getElementById('<%= idTipoEditar.ClientID %>').value = "";
                    document.getElementById('<%= txtDescripcion.ClientID %>').value = "";
                    const arrayInsumo = response.d.split(",").filter(Boolean);
                    document.getElementById('<%= idTipoEditar.ClientID %>').value = arrayInsumo[0];
                    console.log(arrayInsumo[0])
                    document.getElementById('<%= txtDescripcion.ClientID %>').value = arrayInsumo[1]
                }
            });
        }
    </script>

    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtDescripcionTipo.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'InsumosF', location.protocol + '//' + location.host + location.pathname);

        }
    </script>

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


            document.getElementById('lblSiteMap').innerHTML = " / Maestros / Tipos de Recetas";


        });
    </script>

</asp:Content>
