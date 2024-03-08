<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CuentasBancarias.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.CuentasBancarias" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                                    <div class="col-md-2">
                                                        <a data-toggle="modal" data-backdrop="static" title="Agregar Cuenta bancaria"
                                                            data-target="#ModalNuevoPais" class="btn btn-primary dim"
                                                            style="margin-right: 1%; float: right"><i class='fa fa-plus'></i>
                                                        </a>
                                                    </div>
                                                </div>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style="width: 30%">Entidad Bancaria</th>
                                                            <th style="width: 30%">Numero de Cuenta</th>
                                                            <th style="width: 30%">Descripción</th>
                                                            <th style="width: 10%"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phPaises" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="ModalNuevoPais" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h2 class="modal-title">Agregar Cuenta Bancaria
                        <span>

                            <i class="fa fa-globe"></i>
                        </span>
                        </h2>
                    </div>
                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-12">



                                <div class="row">
                                    <div class="form-group">
                                        <label for="exampleInputEmail2" class="sr-only">Entidad Bancaria</label>
                                        <asp:DropDownList ID="txtEntidadBancaria" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword2" class="sr-only">Numero/Cuenta</label>
                                        <asp:TextBox runat="server" ID="txtNumeroCuenta" placeholder="Cuenta Numero" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword2" class="sr-only">Descripcion</label>
                                        <asp:TextBox runat="server" ID="txtDescripcion" placeholder="Descripcion" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Button class="btn btn-primary" Text="Registrar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click" />
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>

                                </div>
                            </div>



                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalConfirmacion2" class="modal fade" role="dialog">
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
                        <asp:HiddenField ID="Editar" runat="server" />

                    </div>
                </div>
            </div>
        </div>


        <div id="ModalEditar" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h2 class="modal-title">Agregar Cuenta Bancaria
                        <span>

                            <i class="fa fa-globe"></i>
                        </span>
                        </h2>
                    </div>
                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-12">



                                <div class="row">
                                    <div class="form-group">
                                        <label for="exampleInputEmail2" class="sr-only">Entidad Bancaria</label>
                                        <asp:DropDownList ID="DropDownListEditar" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword2" class="sr-only">Numero/Cuenta</label>
                                        <asp:TextBox runat="server" ID="txtNumeroEditar" placeholder="Cuenta Numero" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputPassword2" class="sr-only">Descripcion</label>
                                        <asp:TextBox runat="server" ID="txtDescripcionEditar" placeholder="Descripcion" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:Button class="btn btn-primary" Text="Registrar" runat="server" ID="Button1" OnClick="btnEditar_Click" />
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>

                                </div>
                            </div>



                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <%--seccion modales --%>
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
            button.style.float = "right";
            button.style.marginRight = "1%";
            button.setAttribute("href", "ProductosABM.aspx");
            button.setAttribute("class", "btn");

            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';


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
            document.getElementById('<%=Editar.ClientID%>').value = "false";

        }

        function abrirdialog2(id, entidadBancaria, NumeroCuenta, Descripcion) {
            console.log("Prueba");
            document.getElementById("ContentPlaceHolder1_DropDownListEditar").value = entidadBancaria;
            document.getElementById("ContentPlaceHolder1_txtNumeroEditar").value = NumeroCuenta;
            document.getElementById("ContentPlaceHolder1_txtDescripcionEditar").value = Descripcion;
            $('#ModalEditar').modal('show');
            document.getElementById('<%=hiddenID.ClientID%>').value = id;
            document.getElementById('<%=Editar.ClientID%>').value = "true";

        }
    </script>
</asp:Content>
