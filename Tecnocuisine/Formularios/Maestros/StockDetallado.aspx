<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockDetallado.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.StockDetallado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .ibox-content {
            padding-right: 20px !important;
            padding-left: 20px !important;
            padding-bottom: 20px !important;
        }
    </style>

    <%-- NUEVA TABLAS--%>


    <div class="row">
        <div class="col-md-6">
            <%--EMPIEZA STOCK FINAL--%>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Stock Final</h5>

                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">#</th>
                                        <th>Unidad</th>
                                        <th style="text-align: end">Stock</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PHStockFinal" runat="server"></asp:PlaceHolder>


                                </tbody>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%--TERMINA STOCK FINAL--%>
        </div>
        <div class="col-md-6">
            <%-- STOCK SECTOR--%>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Stock Sector</h5>

                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>

                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">#</th>
                                        <th>Sector</th>
                                        <th>Unidad</th>
                                        <th style="text-align: end">Stock</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PHSector" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%--TERMINA STOCK SECTOR--%>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Stock Presentaciones</h5>

                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">#</th>
                                        <th>Presentacion</th>
                                        <th style="text-align: end">Stock</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PHPresentaciones" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Stock Lotes</h5>

                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>

                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">#</th>
                                        <th>Presentacion</th>
                                        <th>Lotes</th>
                                        <th style="text-align: end">Stock</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PHLotes" runat="server"></asp:PlaceHolder>

                                </tbody>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Stock Marcas</h5>

                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>

                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">#</th>
                                        <th>Marca</th>
                                        <th>Unidad</th>
                                        <th style="text-align: end">Stock</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PHMarcas" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="NombreProd" runat="server"/>
                </div>
            </div>
        </div>

    </div>
    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText += " / " + document.getElementById("ContentPlaceHolder1_NombreProd").value;
        });
    </script>
</asp:Content>
