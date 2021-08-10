<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Tecnocuisine.Clientes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Clientes</h5>
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
                                <linkbutton type="button" data-toggle="modal" href="#modalAgregar" onclick="vaciarFormulario();" class="btn btn-success">Nuevo&nbsp;<i style="color:white" class="fa fa-plus"></i></linkbutton>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable" >
                                    <thead>
                                        <tr>
                                            <th style="width: 10%">Codigo</th>
                                            <th style="width:20%">Razon Social</th>
                                            <th style="width:20%">Alias</th>
                                            <th style="width:10%">IVA</th>
                                            <th style="width:10%">Forma Pago</th>
                                            <th style="width:5%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phClientes" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>

                        </ContentTemplate>

                    </asp:UpdatePanel>
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
                        <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <asp:HiddenField ID="hiddenID" runat="server" />
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
                        <label class="col-sm-2 control-label editable">Codigo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCodigo" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Razon Social</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtRazonSocial" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">CUIT</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCuit" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Alias</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAlias" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">IVA</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListRegimen" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Forma de Pago</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListFormaPago" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                     <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Vendedor</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListVendedor" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Vencimiento FC</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtVencimientoFC" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Saldo Maximo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSaldoMax" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Estado</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListEstado" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Observaciones</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtObservaciones" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
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
        function vaciarFormulario() {
            ContentPlaceHolder1_hiddenEditar.value = "";
            ContentPlaceHolder1_txtRazonSocial.value = "";
            ContentPlaceHolder1_txtCodigo.value = "";
            ContentPlaceHolder1_txtCuit.value = "";
            ContentPlaceHolder1_txtAlias.value = "";
            ContentPlaceHolder1_txtVencimientoFC.value = "";
            ContentPlaceHolder1_txtSaldoMax.value = "";
            ContentPlaceHolder1_txtObservaciones.value = "";
            ContentPlaceHolder1_ListRegimen.value = "-1";
            ContentPlaceHolder1_ListFormaPago.value = "-1";
            ContentPlaceHolder1_ListVendedor.value = "-1";
            ContentPlaceHolder1_ListEstado.value = "-1";

          

            window.history.pushState('', 'Alicuotas', location.protocol + '//' + location.host + location.pathname);
        }
    </script>
    <script>
    $(document).ready(function() {
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
            oTable.$('td').editable( '../example_ajax.php', {
                "callback": function( sValue, y ) {
                    var aPos = oTable.fnGetPosition( this );
                    oTable.fnUpdate( sValue, aPos[0], aPos[1] );
                },
                "submitdata": function ( value, settings ) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable.fnGetPosition( this )[2]
                    };
                },

                "width": "90%",
                "height": "100%"
            } );


        });
    </script>
    </asp:Content>
