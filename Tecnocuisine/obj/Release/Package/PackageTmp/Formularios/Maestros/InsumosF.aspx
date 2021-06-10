<%@ Page Title="InsumosF" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsumosF.aspx.cs" Inherits="Tecnocuisine.InsumosF" %>

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
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Insumos</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtDescripcionInsumo" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4 ">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Guardar</asp:LinkButton>
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
                                                <asp:PlaceHolder ID="phInsumos" runat="server"></asp:PlaceHolder>
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
    </div>


    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>


</asp:Content>
