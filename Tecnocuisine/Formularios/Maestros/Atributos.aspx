<%@ Page Title="Atributos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Atributos.aspx.cs" Inherits="Tecnocuisine.Atributos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">
            <div class="container-fluid">

                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Atributos</h5>
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
                                <div class="wrapper wrapper-content  animated fadeInRight">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div id="nestable-menu">
                                                <button type="button" data-action="expand-all" class="btn btn-white btn-sm">Expandir todos</button>
                                                <button type="button" data-action="collapse-all" class="btn btn-white btn-sm">Collapsar todos</button>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <div class="ibox-content">



                                                    <div class="dd" id="nestable">
                                                        <ol id="main" runat="server" class="dd-list">
                                                            <%--<li class="dd-item" data-id="1">
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
                                    </li>--%>
                                                        </ol>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <%--       <div class="dd" id="nestable">
                                <ol  runat="server" id="main" class="dd-list">
                                    
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
                            <textarea id="nestable-output" class="form-control"></textarea>--%>

                        </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
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
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="btn btn-danger" OnClick="btnSi_Click" />
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div id="modalAgregarSubAtributo" class="modal" tabindex="-1" role="dialog">
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
                            <asp:TextBox ID="txtSubAtributo" class="form-control" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnAgregar" Text="Agregar" class="btn btn-primary" OnClick="btnAgregar_Click" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:HiddenField ID="hiddenID2" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div id="modalAgregarAtributo" class="modal" tabindex="-1" role="dialog">
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
                            <asp:TextBox ID="txtDescripcionAtributoModal" class="form-control" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="Button1" Text="Agregar" class="btn btn-primary" OnClick="btnAgregarAtributoFromTipoAtributo_Click" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>


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
                    <div class="form-group">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtDescripcionAtributos" class="form-control" runat="server" />

                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">Tipo de atributo</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ListTipoAtributo" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnGuardar" Text="Agregar" class="btn btn-danger" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
    </div>


    <script>           
        function abrirdialog() {
            $('#modalconfirmacion2').modal('show');
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
    </script>
</asp:Content>

