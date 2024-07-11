<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PlanDeCuentas.aspx.cs" Inherits="Tecnocuisine.Formularios.Contabilidad.PlanDeCuentas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .drag_disabled {
            pointer-events: none;
        }

        .drag_enabled {
            pointer-events: all;
        }
        .float-e-margins .btn {
    margin-bottom: 0px !important;
}
    </style>

    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">
            <div class="container-fluid-nestable">

                <div class="ibox nestable float-e-margins" style="padding: 1.5%">
                    <div class="ibox-title">
                        <h5>Plan de Cuentas</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>

                                <div class="wrapper wrapper-content  animated fadeInRight">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <div id="nestable-menu" style="margin: 0px 0 20px 0;">
                                                <div style="display: flex">

                                                    <button type="button" style="margin-right: 15px;margin-left: 10px" data-action="expand-all" class="btn btn-white btn-sm">
                                                        <i data-toggle="tooltip" data-placement="top" data-original-title="Expandir" data-action="expand-all" class="fa fa-folder-open" style="font-size: 15px;"></i>
                                                    </button>
                                                    <button type="button" style="margin-right: 15px" data-action="collapse-all" class="btn btn-white btn-sm">
                                                        <i data-toggle="tooltip" data-placement="top" data-original-title="Contraer" data-action="collapse-all" class="fa fa-folder" style="font-size: 15px;"></i>
                                                    </button>
                                                    <div class="input-group" style="width: 90%;">
                                                        <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                        <input type="text" id="txtBusqueda" onchange="Handle()" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <linkbutton type="button" data-toggle="modal" href="#modalAgregar" onclick="vaciarFormulario();" class="btn btn-primary" data-toggle="tooltip" data-placement="top"
                                                data-original-title="Crear Nuevo Plan" style="margin-right: 3%; float: right;">
                                                <i class='fa fa-plus'></i>
                                            </linkbutton>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox  float-e-margins">
                                                <div class="ibox-content">

                                                    <div class="dd" id="nestable">
                                                        <ol id="main" runat="server" class="dd-list">
                                                        </ol>
                                                    </div>

                                                </div>
                                            </div>
                                            <linkbutton type="button" onclick="guardarNestedList();" style="float: right" class="btn btn-primary">Guardar&nbsp;<i style="color: white" class="fa fa-check"></i></linkbutton>

                                        </div>

                                    </div>
                                </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="modalAgregar" class="modal" tabindex="-1" role="dialog">
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
                            <asp:TextBox ID="txtDescripcionCategoria" class="form-control" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                </div>
            </div>
        </div>
    </div>



    <div id="modalAgregarSubCategoria" class="modal" tabindex="-1" role="dialog">
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
                            <asp:TextBox ID="txtSubCategoria" class="form-control" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnAgregar" Text="Agregar" class="buttonLoading btn btn-primary" OnClick="btnAgregar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:HiddenField ID="hiddenID2" runat="server" />
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
                    <button type="button" class="btn btn-white" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div id="modalSubAtributo" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Seleccionar Tipos de Atributos</h4>
                </div>
                <div class="modal-body">
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover " id="editable">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Descripcion</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phInsumos" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnSubAtributos" Text="Guardar" class="buttonLoading btn btn-primary" OnClick="btnSubAtributos_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:HiddenField ID="hiddenSubAtributo" runat="server" />
                </div>
            </div>
        </div>
    </div>



    <script>           
       function Handle() {
                buscarEnLista();
        };

        function buscarEnLista() {
            console.log("ENTRE")
            // Obtener el texto de búsqueda y los elementos de la lista
            var searchText = $('#txtBusqueda').val().toLowerCase();
            var listItems = $('#ContentPlaceHolder1_main').find('li');
            console.log(searchText, listItems)
            // Filtrar elementos que coinciden con la búsqueda
            var filteredItems = listItems.filter(function () {
                console.log("ENTRE2")
                return $(this).text().toLowerCase().indexOf(searchText) > -1;
            });

            // Mostrar elementos que coinciden y ocultar los demás
            listItems.hide();
            filteredItems.show().parents('li').show();

            // Contraer y expandir los elementos según los resultados de la búsqueda
            if (searchText === '') {
                $('#nestable').nestable('collapseAll');
            } else if (filteredItems.length > 0) {
                $('#nestable').nestable('expandAll');
            } else {
                $('#nestable').nestable('collapseAll');
            }
        }







        function buscarEnLista2() {
            var searchText = $('#txtBusqueda').val().toLowerCase();
            var listItems = $('#main').find('li');


            // Filtrar elementos que coinciden con la búsqueda
            var filteredItems = listItems.filter(function () {
                return $(this).text().toLowerCase().indexOf(searchText) > -1;
            });

            // Expandir elementos que coinciden y contraer los demás
            listItems.not(filteredItems).hide();
            filteredItems.show().parents('li').show();
            $('#nestable').nestable('expandAll');
        }
        function abrirdialog() {
            $('#modalconfirmacion2').modal('show');
        }
        function guardarNestedList() {
            var lista = $("#ContentPlaceHolder1_main > li");
            var ids = '';
            for (var x = 0; x < lista.length; x++) {
                ids += lista[x].attributes["data-id"].value + "," + "null" + ";";
                ids += obtenerHijosNestedList(lista[x]);
            }
            $.ajax({
                method: "POST",
                url: "PlanDeCuentas.aspx/ModificarRelaciones",
                data: '{id: "' + ids + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: recargarPagina
            });
        }
        function obtenerHijosNestedList(arr) {
            var lista = $("#" + arr.attributes["id"].value + " > ol > li");
            var ids = '';
            for (var x = 0; x < lista.length; x++) {
                ids += lista[x].attributes["data-id"].value + "," + arr.attributes["id"].value + ";";
                ids += obtenerHijosNestedList(lista[x]);
            }
            return ids;
        }
        function recargarPagina() {
            alert('Guardado con exito');
            location.reload();
        }
    </script>

    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });

            var updateOutput = function (e) {
                var list = e.length ? e : $(e.target),
                    output = list.data('output');
                if (window.JSON) {
                    output.val(window.JSON.stringify(list.nestable('serialize')));//, null, 2));
                } else {
                    output.val('JSON browser support required for this demo.');
                }
            };
            // activate Nestable for list 1
            $('#nestable').nestable({
                group: 1
            }).on('change', updateOutput);

            $('.dd').each(function () {
                console.log(this)
            });

            // output initial serialised data
            updateOutput($('#nestable').data('output', $('#nestable-output')));

            $('#nestable-menu').on('click', function (e) {
                var target = $(e.target),
                    action = target.data('action');
                if (action === 'expand-all') {
                    $('.dd').nestable('expandAll');
                }
                if (action === 'collapse-all') {
                    $('.dd').nestable('collapseAll');
                }

            });
        });
    </script>

    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtDescripcionCategoria.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'Categorias', location.protocol + '//' + location.host + location.pathname);
        }
    </script>
</asp:Content>
