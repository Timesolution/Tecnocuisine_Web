<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Ventas.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Ventas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div>
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


                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <a href="../Ventas/GenerarVenta.aspx" class="btn btn-primary dim" data-toggle="tooltip" data-placement="top"
                                                            data-original-title="Agregar Nueva Entrega" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                                                    </div>
                                                </div>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>

                                                            <th style="max-width: 100px; text-align: right;" >ID</th>
                                                            <th>Fecha</th>
                                                            <th style="text-align: right">Numero</th>
                                                            <th style="text-align: right">Costo</th>
                                                            <th style="text-align: right">Precio Venta</th>
                                                            <th style="max-width: 100px"></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phProductosyRecetas" runat="server"></asp:PlaceHolder>
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
</asp:Content>
