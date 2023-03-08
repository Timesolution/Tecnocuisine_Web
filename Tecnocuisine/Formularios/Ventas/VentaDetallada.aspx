<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VentaDetallada.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.VentaDetallada" %>

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
        <div style="margin: 20px;">

            <table class="table table-bordered table-hover" id="tableProductos" style="margin-top: 2%; max-width: 99%;">
                <thead>
                    <tr>
                        <th style="width: 5%; text-align: right;">#</th>
                        <th style="width: 5%">Producto</th>
                        <th style="width: 5%; text-align: right;">Costo</th>
                        <th style="width: 5%; text-align: right;">Cantidad</th>
                        <th style="width: 10%; text-align: right;">Precio Venta</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="phTablaProductos" runat="server" />
                </tbody>
            </table>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText += " / " + document.getElementById("ContentPlaceHolder1_NombreProd").value;
        });
    </script>
</asp:Content>
