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
                    <b style="font-size: 1.8rem">Stock Final</b>
                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <%--<th style="width: 10%">#</th>--%>
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
                    <b style="font-size: 1.8rem">Stock Sector</b>
                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>

                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <%--<th style="width: 10%">#</th>--%>
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

    <%--<div class="row">
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
    </div>--%>
    <div class="row">
        <%--<div class="col-md-6">
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
        </div>--%>
    </div>

    <div class="row">
        <%-- EN TRANSITO --%>
        <div class="col-md-6">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <b style="font-size: 1.8rem">En Transito</b>
                </div>
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>

                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th style="text-align: left">Unidad</th>
                                        <th style="text-align: end">Stock</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PHTransito" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<asp:HiddenField ID="NombreProd" runat="server" />--%>
                </div>
            </div>
        </div>
        <%-- FIN --%>
    </div>



    <div id="modalDetalleTransito" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 class="modal-title" style="font-weight: bold"><%--Origen/Destino--%>Detalle de Transito</h3>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel7">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <table class="table table-hover no-margins table-bordered" id="tableModalTransito">
                                        <thead>
                                            <tr>
                                                <td><strong>Sector Origen</strong></td>
                                                <td><strong>Sector Destino</strong></td>
                                                <td class="text-right"><strong>Cantidad</strong></td>
                                            </tr>
                                        </thead>
                                        <tbody id="tableModalDetalleTransito">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            //document.getElementById("lblSiteMap").innerText += " / " + document.getElementById("ContentPlaceHolder1_NombreProd").value;
        });
    </script>

    <script>
        function verDetalleTransito(descripcion) {
            $.ajax({
                method: "POST",
                url: "StockDetallado.aspx/verDetalleTransito",
                data: JSON.stringify({ descripcion: descripcion }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    console.log("Error");
                },
                success: function (data) {
                    const arrayDetalleTransito = data.d.split(";").filter(Boolean);

                    document.getElementById('tableModalDetalleTransito').innerHTML = "";

                    arrayDetalleTransito.forEach(function (detalleTransito) {
                        const partesDetalle = detalleTransito.split(",").filter(Boolean);

                        const sectorOrigen = partesDetalle[0];
                        const sectorDestino = partesDetalle[1];
                        const cantidad = partesDetalle[2];

                        let plantillaDetalleTransito = `
                        <tr>
                            <td>${sectorOrigen}</td>
                            <td>${sectorDestino}</td>
                            <td style="text-align: right;">${cantidad}</td>
                        </tr>
                    `;

                        document.getElementById('tableModalDetalleTransito').innerHTML += plantillaDetalleTransito;
                    });

                    $("#modalDetalleTransito").modal("show")
                }
            });
        }
    </script>
</asp:Content>
