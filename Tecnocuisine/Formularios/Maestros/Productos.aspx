<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Tecnocuisine.Productos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Productos</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-4">
                            <div id="nestable-menu">
                                <linkbutton type="button" data-toggle="modal" href="#modalAgregar" class="btn btn-success">Nuevo&nbsp;<i style="color:white" class="fa fa-upload"></i></linkbutton>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="wrapper wrapper-content animated fadeInRight">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">

                                            <div class="ibox-content">
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Descripcion</th>
                                                            <th>Categoria</th>
                                                            <th>Atributo</th>
                                                            <th>Unidades de medida</th>
                                                            <th>Costo</th>
                                                            <th>Presentaciones</th>
                                                            <th>Alicuota</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phProductos" runat="server"></asp:PlaceHolder>
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
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="btn btn-danger" OnClick="btnSi_Click" />
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:HiddenField ID="hiddenID" runat="server" />
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
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtDescripcionProducto" class="form-control" runat="server" />
                        </div>


                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Categoria</label>
                        <div class="col-sm-7">

                            <asp:TextBox ID="txtDescripcionCategoria" class="form-control" runat="server" />
                            <asp:HiddenField ID="idCategoria" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" ID="btnCategorias" class="btn btn-primary dim" data-toggle="modal" href="#modalCategoria"><i style="color: white" runat="server" class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Atributo</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtDescripcionAtributo" class="form-control" runat="server" />
                            <asp:HiddenField ID="idAtributo" runat="server" />

                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" disabled="disabled" ID="btnAtributos" class="btn btn-primary dim" data-toggle="modal" href="#modalAtributo" ><i style="color: white"  class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Costo</label>
                        <div class="col-sm-7">
                            <div class="input-group m-b">
                                <span class="input-group-addon">$</span><asp:TextBox ID="txtCosto" class="form-control" runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Unidad de medida</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ListUnidadMedida" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Presentacion</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ListPresentaciones" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Alicuota IVA</label>
                        <div class="col-sm-7">
                            <div class="input-group m-b">
                                <asp:DropDownList ID="ListAlicuota" class="form-control m-b" runat="server">
                                </asp:DropDownList><span class="input-group-addon">%</span>
                            </div>
                        </div>

                    </div>


                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardar" class="btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                </div>
            </div>
        </div>
    </div>


    <div id="modalAtributo" class="modal" tabindex="-2" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Elegir atributo</h4>
                </div>
                <div class="modal-body">
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="wrapper wrapper-content  animated fadeInRight">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <div class="ibox-content">



                                                    <div class="dd" id="nestable3">
                                                        <ol id="main" runat="server" class="dd-list">
                                                        </ol>
                                                    </div>


                                                </div>
                                            </div>
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
    </div>


    <div id="modalCategoria" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Elegir Categoria</h4>
                </div>
                <div class="modal-body">
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="wrapper wrapper-content  animated fadeInRight">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <div class="ibox-content">



                                                    <div class="dd" id="nestable">
                                                        <ol id="olCategorias" runat="server" class="dd-list">
                                                        </ol>
                                                    </div>


                                                </div>
                                            </div>
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
    </div>








    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>



    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
    </script>

    <script type="text/javascript">
        function openModalCategorias() {
            $('#modalCategorias').modal('show');
        }
    </script>

    <script type="text/javascript">
        function openModalAtributos() {
            $('#modalAtributos').modal('show');
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
                "height": "100%"
            });


        });
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

            // activate Nestable for list 1
            $('#nestable3').nestable({
                group: 1
            }).on('change', updateOutput);



            // output initial serialised data
            updateOutput($('#nestable').data('output', $('#nestable-output')));
            updateOutput($('#nestable3').data('output', $('#nestable-output')));

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
