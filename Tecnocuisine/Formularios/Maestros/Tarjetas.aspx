﻿<%@ Page Title="Tarjetas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarjetas.aspx.cs" Inherits="Tecnocuisine.Tarjetas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">

            <div class="ibox float-e-margins">

                <div class="ibox-content">
                    <div style="margin-left: 0px; margin-right: 0px;" class="row">
                        <div class="col-md-10">

                            <div class="input-group m-b">
                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                            </div>
                        </div>
                        <div class="col-md-2">

                            <a data-toggle="modal" data-backdrop="static" data-target="#modalAgregar" title="Agregar tarjeta"
                                class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%">#</th>
                                            <th style="width: 20%">Nombre</th>
                                            <th style="width: 20%">Entidad</th>
                                            <th style="width: 20%; text-align: right">Acredita en (Dias)</th>
                                            <th style="width: 20%"></th>
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
        </div>
    </div>

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
                        <label class="col-sm-4 control-label editable">Entidad</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="DropDownEntidadList" class="form-control" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-4 control-label editable">Nombre Tarjeta</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtDescripcionInsumo" class="form-control" runat="server" />

                        </div>
                    </div>

                    <div class="row" style="margin-top: 15px;">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-4 control-label editable">Acredita en (Dias)</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtAcreditaEnDias" type="number" class="form-control" 
                             runat="server" min="0" />
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <asp:HiddenField ID="hiddenValueDropDown" runat="server" />
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
                        <label class="col-sm-4 control-label editable">Entidad</label>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="DropDownListEdit" class="form-control" runat="server" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-4 control-label editable">Nombre Tarjeta</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtNombreTarjetaEditar" class="form-control" runat="server" />

                        </div>
                    </div>


                    <div class="row" style="margin-top: 15px;">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-4 control-label editable">Acredita en (Dias)</label>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtAcreditaEnEdit" type="number" class="form-control" runat="server" min="0" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="LinkButton1" class="buttonLoading btn btn-primary" 
                     OnClientClick="CargarHiddenEdit()" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Editar </asp:LinkButton>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <button type="button" class="btn btn-danger" onclick="CancelarEdit(event)" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>


                </div>
            </div>
        </div>
    </div>


    <script>
        function GuardarDropDownValue(event) {
            event.preventDefault();
            document.getElementById("ContentPlaceHolder1_hiddenValueDropDown").value = document.getElementById("ContentPlaceHolder1_DropDownEntidadList").value;
            return true;
        }
        function CancelarEdit(event) {
            event.preventDefault();
            document.getElementById("ContentPlaceHolder1_hiddenEditar").value = "";
        }
        function CargarHiddenEdit() {
            document.getElementById("ContentPlaceHolder1_hiddenEditar").value = "Edit";
        }
        function abrirdialog2(valor, text,idEntidad,AcreditaEn) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            document.getElementById('ContentPlaceHolder1_txtNombreTarjetaEditar').value = text;
            document.getElementById('ContentPlaceHolder1_DropDownListEdit').value = idEntidad;
            document.getElementById('ContentPlaceHolder1_txtAcreditaEnEdit').value = AcreditaEn;
            $('#modalEditar').modal('show');
        }


        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>

    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtDescripcionInsumo.value = "";
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

            validarTxtAcreditaEnDias();
        });
    </script>
    <script>
        function validarTxtAcreditaEnDias() {
            document.getElementById('<%=txtAcreditaEnDias.ClientID%>').addEventListener('keydown', function(event) {
                if (!/[0-9]/.test(event.key) && !event.ctrlKey && event.key.length === 1) {
                    event.preventDefault();
                }
            });

            document.getElementById('<%=txtAcreditaEnEdit.ClientID%>').addEventListener('keydown', function(event) {
                if (!/[0-9]/.test(event.key) && !event.ctrlKey && event.key.length === 1) {
                    event.preventDefault();
                }
            });
        }
    </script>

</asp:Content>
