﻿<%@ Page Title="Vendedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vendedores.aspx.cs" Inherits="Tecnocuisine.Vendedores" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Vendedores</h5>
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
                                            <th style="width: 10%">Legajo</th>
                                            <th style="width:20%">Nombre Completo</th>
                                            <th style="width:10%">Documento</th>
                                            <th style="width:20%">Datos</th>
                                            <th style="width:5%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phVendedores" runat="server"></asp:PlaceHolder>
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
                        <label class="col-sm-2 control-label editable">Legajo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtLegajo" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Nombre</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtNombre" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                     <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Apellido</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtApellido" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">DNI</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDNI" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
               
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Datos</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDatos" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
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
            ContentPlaceHolder1_txtLegajo.value = "";
            ContentPlaceHolder1_txtNombre.value = "";
            ContentPlaceHolder1_txtApellido.value = "";
            ContentPlaceHolder1_txtDNI.value = "";
            ContentPlaceHolder1_txtDatos.value = "";
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
