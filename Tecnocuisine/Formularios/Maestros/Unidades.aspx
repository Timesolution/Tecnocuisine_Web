<%@ Page Title="Unidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Unidades.aspx.cs" Inherits="Tecnocuisine.Unidades" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
                <div class="container-fluid">

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                        <h5>Unidades</h5>
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
                                <linkbutton type="button" data-toggle="modal" href="#modalAgregar" class="btn btn-white btn-sm">Nuevo</linkbutton>
                            </div>
                        </div>
                    </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Descripcion</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="phUnidades" runat="server"></asp:PlaceHolder>
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
                            <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                            <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="btn btn-danger" OnClick="btnSi_Click" />
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
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDescripcionUnidad" class="form-control" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:Button runat="server" ID="btnGuardar" Text="Agregar" class="btn btn-danger" OnClick="btnGuardar_Click" />
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


</asp:Content>
