<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Produccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Produccion" %>

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


                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <a href="../Ventas/GenerarProduccion.aspx" class="btn btn-primary dim" data-toggle="tooltip" data-placement="bottom" data-original-title="Agregar Nueva Produccion" style="margin-right: 1%; float: right"><i class="fa fa-plus"></i></a>
                                                        <%--<a href="../Ventas/GenerarProduccion.aspx" class="btn btn-primary dim" data-toggle="tooltip" data-placement="top" data-original-title="Agregar Nueva Produccion" style="margin-right: 1%; float: right"><i class="fa fa-plus"></i></a>--%>
                                                    </div>
                                                </div>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style=" text-align: right; width: 2%;">#</th>
                                                            <th  style="width: 5%;">Receta</th>
                                                            <th style=" text-align: right; width: 4%;">Numero Produccion</th>
                                                            <th style="width: 5%; text-align: right;">Fecha Produccion</th>
                                                            <th style=" text-align: right; width: 3%;">Cantidad Producida</th>
                                                            <th style="width: 3%;">Lote</th>
                                                            <th style=" text-align: right; width: 3%;">Costo Total</th>
                                                            <th style="width: 2%";></th>
                                                    
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phProductos" runat="server"></asp:PlaceHolder>
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













    <script>    

        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
        });
        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function revertirNumero(numeroFormateado) {
            var numero = parseFloat(numeroFormateado.replace(/,/g, ''));
            return numero;
        }
    </script>


        <script>
            $(document).ready(function () {
                document.getElementById("lblSiteMap").innerText = "Produccion / Produccion";
            });
        </script>



</asp:Content>
