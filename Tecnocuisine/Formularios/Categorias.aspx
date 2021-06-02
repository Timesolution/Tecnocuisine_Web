<%@ Page Title="InsumosF" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Tecnocuisine.Categorias" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">

            <div class="container-fluid">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Datos</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="form-horizontal">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Descripción</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDescripcionCategoria" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                    
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Categoria Padre</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList id="ListCategorias" class="form-control m-b" runat="server">
                                            <asp:ListItem Text="Sin relacion" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 ">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary" OnClick="btnGuardar_Click" />
                                    </div>
                                </div>



                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="container-fluid">

                <div class="ibox float-e-margins">
                    <div class="ibox-title">

                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <%--<div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover" id="dataTables-example">
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
                                </div>--%>
                                <div class="dd" id="nestable">
                                <ol class="dd-list">
                                    <li class="dd-item" data-id="1">
                                        <div class="dd-handle">1 - Lorem ipsum</div>
                                    </li>
                                    <li class="dd-item" data-id="2">
                                        <div class="dd-handle">2 - Dolor sit</div>
                                        <ol class="dd-list">
                                            <li class="dd-item" data-id="3">
                                                <div class="dd-handle">3 - Adipiscing elit</div>
                                            </li>
                                            <li class="dd-item" data-id="4">
                                                <div class="dd-handle">4 - Nonummy nibh</div>
                                            </li>
                                        </ol>
                                    </li>
                                    <li class="dd-item" data-id="5">
                                        <div class="dd-handle">5 - Consectetuer</div>
                                        <ol class="dd-list">
                                            <li class="dd-item" data-id="6">
                                                <div class="dd-handle">6 - Aliquam erat</div>
                                            </li>
                                            <li class="dd-item" data-id="7">
                                                <div class="dd-handle">7 - Veniam quis</div>
                                            </li>
                                        </ol>
                                    </li>
                                    <li class="dd-item" data-id="8">
                                        <div class="dd-handle">8 - Tation ullamcorper</div>
                                    </li>
                                    <li class="dd-item" data-id="9">
                                        <div class="dd-handle">9 - Ea commodo</div>
                                    </li>
                                </ol>
                            </div>
                                <div class="m-t-md">
                                <h5>Serialised Output</h5>
                            </div>
                            <textarea id="nestable-output" class="form-control"></textarea>

                        </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>


        </div>
    </div>

    <div class="modal fade" id="modalConfirmacion">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Eliminar insumo</h4>
                </div>
                <div class="modal-body">
                    <p>
                        Esta seguro que desea eliminar el insumo?
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:TextBox runat="server" ID="txtMovimiento" Text="0" Style="display: none"></asp:TextBox>
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function abrirdialog(valor) {
            document.getElementById('<%= txtMovimiento.ClientID %>').value = valor;
        }
    </script>


    <script>
        $(document).ready(function () {

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

            // activate Nestable for list 2
            $('#nestable2').nestable({
                group: 1
            }).on('change', updateOutput);

            // output initial serialised data
            updateOutput($('#nestable').data('output', $('#nestable-output')));
            updateOutput($('#nestable2').data('output', $('#nestable2-output')));

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
</asp:Content>
