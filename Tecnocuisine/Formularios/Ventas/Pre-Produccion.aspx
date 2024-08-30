<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pre-Produccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Pre_Produccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />

    <%-- ACA EMPIEZA EL CONTAINER --%>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="widget stacked">
                    <div class="stat">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="wrapper wrapper-content animated fadeInRight">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox float-e-margins" style="margin-bottom:0">
                                                <div class="ibox-content">
                                                    <div style="margin-left: 0px; margin-right: 0px;"
                                                        class="row">
                                                        <div class="row" style="display: flex; flex-wrap: wrap; padding-bottom: 2rem">

                                                            <div class="" style="margin-left: 15px; margin-right: 15px;">
                                                                <%-- Label de la ddl --%>
                                                                <div class="">
                                                                    <label class="col-sm-2 control-label">Sector</label>
                                                                </div>
                                                                <%-- Ddl sectores --%>
                                                                <div class="" style="margin-left: 15px; margin-right: 15px;">
                                                                    <asp:DropDownList ID="ddlSector" runat="server"
                                                                        CssClass="chosen-select form-control"
                                                                        DataTextField="CountryName" DataValueField="CountryCode"
                                                                        Data-placeholder="Seleccione Rubro..." Width="100%">
                                                                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div style="align-content: end">
                                                                <div style="display: block; width: 100%">
                                                                    <%-- boton ayer --%>
                                                                    <a id="btnAyer" onclick="setFecha(-1); return false;" class="btn btn-default"
                                                                        title="filtrar ayer" style="height: 32px; margin-left: 10px">Ayer
                                                                    </a>
                                                                    <%-- boton hoy --%>
                                                                    <a id="btnHoy" onclick="setFecha(0); return false;" class="btn btn-default"
                                                                        title="filtrar hoy" style="height: 32px; margin-left: 10px">Hoy
                                                                    </a>
                                                                    <a id="btnMañana" onclick="setFecha(1); return false;" class="btn btn-default"
                                                                        title="filtrar mañana" style="height: 32px; margin-left: 10px">Mañana
                                                                    </a>
                                                                    <a id="btnPasado" onclick="setFecha(2); return false;" class="btn btn-default"
                                                                        title="filtrar pasado" style="height: 32px; margin-left: 10px">Pasado 
                                                                    </a>
                                                                </div>
                                                            </div>

                                                            <div style="margin-left: 20px">
                                                                <label>Desde</label>
                                                                <div style="align-content: end">
                                                                    <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy"
                                                                        data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;">
                                                                    </asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div style="margin-left: 20px">
                                                                <label>Hasta</label>
                                                                <div style="align-content: end">
                                                                    <asp:TextBox class="form-control" runat="server" type="date"
                                                                        ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy"
                                                                        Style="margin-left: 0px; width: 100%;">
                                                                    </asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="" style="align-content: end; margin-left: 20px">
                                                                <asp:Button ID="btnSector" runat="server" class="btn btn-primary pull-right" OnClientClick="filtrarSectorOrigen(); return false" Text="Buscar" />
                                                            </div>

                                                            <%-- Boton Filtrar --%>
                                                            <%--<div class="col-md-2">
                                                                <a id="btnFiltrar" onclick="FiltrarIngredientesDeOrdenesDeProduccion()" class="btn btn-primary pull-right" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>
                                                            </div>--%>
                                                        </div>
                                                        <%-- Fin de la row --%>
                                                    </div>
                                                </div>
                                                <%--</div>--%>
                                                <%--</div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="widget-content">
        <div class="bs-example">
            <ul id="myTab" class="nav nav-tabs">
                <li class="active"><a href="#Recepcion" data-toggle="tab" id="linkDetalle">Recepcion</a></li>
                <li class=""><a href="#Produccion" data-toggle="tab" runat="server" id="linkImg">Produccion</a></li>
                <li class=""><a href="#Envio" runat="server" id="linkArticulosSucursales" data-toggle="tab">Envio</a></li>
            </ul>
        </div>
    </div>
    <div class="tab-content">
        <div class="tab-pane fade active in" id="Recepcion">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <div id="validation-form" role="form" class="form-horizontal col-md-10">
                        <div class="row" style="margin-top: 20px">
                            <div class="col-lg-12">
                                <table class="table table-striped table-bordered table-hover " id="tableRecepcion">
                                    <thead>
                                        <tr>
                                            <th style="width: 15%">Sector Destino</th>
                                            <th style="width: 15%">Numero</th>
                                            <th style="width: 15%">Fecha</th>
                                            <th style="width: 15%">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phRecepcion" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                    </div>

                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="tab-pane fade" id="Produccion">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server" style="width: 100%" >
                <ContentTemplate>
                    <div id="validation-form" role="form" class="form-horizontal col-md-10">
                        <fieldset style="width: 100%" >
                            <div class="row" style="margin-top: 10px">
                                <div class="col-lg-12">

                                    <asp:Button runat="server" OnClick="btnImprimirProduccion_Click" class="btn btn-primary pull-right" Text="Imprimir"/>

                                    <table class="table table-striped table-bordered table-hover " style="width: 100%" id="tableProduccion">
                                        <thead>
                                            <tr>
                                                <th style="width: 35%">Producto</th>
                                                <th style="width: 15%">Cantidad</th>
                                                <th style="width: 15%">Fecha</th>
                                                <th style="width: 15%">Acciones</th>
                                                <th style="width: 0%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phProduccion" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="tab-pane fade" id="Envio">
            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                <ContentTemplate>
                    <div id="validation-form1" role="form" class="form-horizontal col-md-10">
                        <fieldset>
                            <div class="row" style="margin-top: 20px">
                                <div class="col-lg-12">
                                    <table class="table table-striped table-bordered table-hover " id="tableEnvios">
                                        <thead>
                                            <tr>
                                                <th style="width:15%">Fecha</th>
                                                <th style="">Sector Origen</th>
                                                <th style="">Sector Destino</th>
                                                <%--<th style="width: 15%">Producto</th>--%>
                                                <%--<th style="width: 15%">Confirmada</th>--%>
                                                <%--<th style="width: 15%">Enviada</th>--%>
                                                <th style="">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody id="detallesTransferenciasConfirmadas">
                                            <asp:PlaceHolder ID="phEnvio" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                    <asp:LinkButton ID="btnEnviar" runat="server" Text="Enviar"> 

                                    </asp:LinkButton>
                                    <asp:HiddenField ID="FechaDesde" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="FechaHasta" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="ddlSectorSelecionado" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="ddlSectorSelecionadoValue" runat="server"></asp:HiddenField>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row" style="padding-left: 14px; padding-right: 14px">
        <!-- Utiliza col-lg-8 para la parte izquierda -->
        <%-- <div class="col-lg-4">
            <div class="ibox-content m-b-sm border-bottom">
                <div class="col-lg-12" style="background-color: white">
                    <div>
                        <h3><strong>Recepcion</strong></h3>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-striped table-bordered table-hover " id="tableRecepcion">
                                <thead>
                                    <tr>
                                        <th style="width: 15%">Campo1</th>
                                        <th style="width: 15%">Campo2</th>
                                        <th style="width: 15%">Campo3</th>
                                        <th style="width: 15%">Campo4</th>
                                        <th style="width: 15%">Campo5</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <%-- Termina el primer wigget --%>

        <%-- Empieza el segundo widget --%>

        <%-- <div class="col-lg-4">
            <div class="ibox-content m-b-sm border-bottom">
                <div class="col-lg-12" style="background-color: white">
                    <div>
                        <h3><strong>Produccion</strong></h3>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-striped table-bordered table-hover " id="tableProduccion">
                                <thead>
                                    <tr>
                                        <th style="width: 15%">Campo1</th>
                                        <th style="width: 15%">Campo2</th>
                                        <th style="width: 15%">Campo3</th>
                                        <th style="width: 15%">Campo4</th>
                                        <th style="width: 15%">Campo5</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <%--  <div class="col-lg-4">
            <div class="ibox-content m-b-sm border-bottom">
                <div class="col-lg-12" style="background-color: white">
                    <div>
                        <h3><strong>Envios</strong></h3>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-striped table-bordered table-hover " id="tableEnvios">
                                <thead>
                                    <tr>
                                        <th style="width: 15%">Campo1</th>
                                        <th style="width: 15%">Campo2</th>
                                        <th style="width: 15%">Campo3</th>
                                        <th style="width: 15%">Campo4</th>
                                        <th style="width: 15%">Campo5</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>


    <%-- ModalDetalleRemitosInternos --%>
    <div id="modalDetalleRemitoInterno" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Recepcion</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel8">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">


                                        <table class="table table-hover no-margins table-bordered" id="DetalleRemitosInternos">
                                            <thead>
                                                <tr>
                                                    <%--                                                    <td><strong>Sector Origen</strong></td>
                                                    <td><strong>Sector Destino</strong></td>--%>
                                                    <td><strong>Producto</strong></td>
                                                    <%--<td class="text-right"><strong>Cantidad</strong></td>--%>
                                                    <%--<td class="text-right"><strong>Cantidad confirmada</strong></td>--%>
                                                    <td class="text-right"><strong>Cantidad enviada</strong></td>
                                                    <td class="text-right"><strong>Cantidad Recepcionada</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tableDetalleRemitosInternos">
                                            </tbody>
                                        </table>


                                        <div style="text-align: right; margin-top: 10px">
                                            <asp:Button ID="btnRecepcion" runat="server" OnClientClick="btnRecepcion_ClientClick()"
                                                OnClick="btnRecepcion_Click" class="btn btn-primary"
                                                title="Enviar" Text="Recepcionar" />
                                        </div>
                                        <asp:HiddenField ID="HiddenField6" runat="server" />
                                        <asp:HiddenField ID="HiddenField7" Value="" runat="server" />
                                        <asp:HiddenField ID="HiddenField8" Value="" runat="server" />
                                        <asp:HiddenField ID="HiddenField9" Value="" runat="server" />
                                        <%-- Este HiddenField guarda el id y lo cantidad de los datos 
                                        de las transferencias--%>
                                        <asp:HiddenField ID="HiddenField10" Value="" runat="server" />

                                        <asp:HiddenField ID="HFIdRemitoInterno" Value="" runat="server" />
                                        <asp:HiddenField ID="HFItems" Value="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="modalDetallePedidos" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Origen/Destino</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel7">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Origen</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered" id="tablePedidos">
                                            <thead>
                                                <tr>
                                                    <td style="display:none; width:0"><strong>Id Sector Origen</strong></td>
                                                    <td><strong>Sector Origen</strong></td>
                                                    <td><strong>Sector Destino</strong></td>
                                                    <td style="display:none; width:0"><strong>Id Producto</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                    <td class="text-right"><strong>Cantidad confirmada</strong></td>
                                                    <td class="text-right"><strong>Cantidad enviada</strong></td>
                                                    <%--<td class="text-right"><strong>Acciones</strong></td>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="tableDetallePedidos">
                                            </tbody>
                                        </table>


                                        <div style="text-align: right; margin-top: 10px">
                                            <asp:Button ID="Button1" runat="server"
                                                OnClientClick="guardarDatosTransferencia(); return false" class="btn btn-primary"
                                                title="Enviar" Text="Enviar" />
                                        </div>
                                        <asp:HiddenField ID="idTransferencia" runat="server" />
                                        <asp:HiddenField ID="transferencias" Value="" runat="server" />
                                        <asp:HiddenField ID="idsPedidos" Value="" runat="server" />
                                        <asp:HiddenField ID="cantidadesDatosTransferencias" Value="" runat="server" />
                                        <%-- Este HiddenField guarda el id y lo cantidad de los datos 
                                            de las transferencias--%>
                                        <asp:HiddenField ID="datosTransferencias" Value="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalDetalleDatosTransferencia" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Origen/Destino</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel7">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Origen</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered" id="tablePedidos2">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                    <td class="text-right"><strong>Confirmada</strong></td>
                                                    <td><strong>Producto Destino</strong></td>
                                                    <td><strong>SectorDestino</strong></td>
                                                    <td><strong>Orden destino</strong></td>
                                                    <td><strong>Cliente destino</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tableDetalleDatosTransferencia">
                                            </tbody>
                                        </table>

                                        <%--    <div style="text-align: right; margin-top:10px">
                                <asp:Button ID="btnConfirmar" runat="server" 
                                    OnClick="btnConfirmar_Click" class="btn btn-primary" 
                                    title="Confirmar" Text="Confirmar"
                                 />
                            </div>  --%>
                                        <%--     <div style="text-align: right; margin-top: 10px">
                                            <asp:Button ID="Button2" runat="server"
                                                OnClientClick="guardarDatosTransferencia(); return false" class="btn btn-primary"
                                                title="Guardar" Text="Guardar" />
                                        </div>--%>
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" Value="" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" Value="" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" Value="" runat="server" />
                                        <%-- Este HiddenField guarda el id y lo cantidad de los datos 
                                        de las transferencias--%>
                                        <asp:HiddenField ID="HiddenField5" Value="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalDetalleProduccion" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><%--Origen/Destino--%>Detalle de produccion</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel7">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Origen</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered" id="tableProduccion2">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Destino</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tableDetalleProduccion">
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
    </div>



    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <!-- Chosen -->
    <%--   <script src="/js/plugins/chosen/chosen.jquery.js"></script>
   <script>
       var config = {
           '.chosen-select': {},
           '.chosen-select-deselect': { allow_single_deselect: true },
           '.chosen-select-no-single': { disable_search_threshold: 10 },
           '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
           '.chosen-select-width': { width: "95%" }
       }
       for (var selector in config) {
           $(selector).chosen(config[selector]);
       }
   </script>--%>





    <script>
        function guardarDatosTransferencia() {


            var rows = document.querySelectorAll('#tableDetallePedidos tr');
            var tableData = [];

            rows.forEach(function (row) {
                var cells = row.getElementsByTagName('td');
                if (cells.length > 0) {
                    var rowData = {
                        idSectorOrigen: cells[0].textContent.trim(),
                        SectorOrigen: cells[1].textContent.trim(),
                        SectorDestino: cells[2].textContent.trim(),
                        idProducto: cells[3].textContent.trim(),
                        Producto: cells[4].textContent.trim(),
                        Cantidad: cells[5].textContent.trim(),
                        CantidadConfirmada: cells[6].textContent.trim(),
                        cantidadEnviada: cells[7].getElementsByTagName('input')[0].value.trim()
                    };
                    tableData.push(rowData);
                }
            });
            //
            var data = {
                tableData: tableData
            };

            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/guardarDatosTransferencia",
                data: JSON.stringify(data),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    toastr.error("La transferencia no pudo ser confirmada.", "Error");
                },
                success: function (response) {

                    if (response.d > 0) {
                        let r = response.d;
                        toastr.success("Transferencia confirmada con exito!", "Exito");
                        //imprimirRemitoEnviado(r);
                        window.open('ImpresionRemitos.aspx?r=' + r, '_blank');
                        location.reload();
                    }
                    else if (response.d == -1) {
                        toastr.error("La transferencia no pudo ser confirmada.", "Error");

                    }
                    else if (response.d == -2) {
                        toastr.error("La transferencia no pudo ser confirmada, no hay stock cargado para uno de los productos.", "Error");

                    }
                    else if (response.d == -3) {
                        toastr.error("La transferencia no pudo ser confirmada, no se puede enviar una cantidad cero (0).", "Error");

                    }
                    else if (response.d == -4) {
                        toastr.error("La transferencia no pudo ser confirmada, una cantidad a enviar es mayor al stock disponible.", "Error");

                    }
                }
            });


        }

        function imprimirRemitoEnviado(r) {
            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/generarReporteEnviado",
                //body: JSON.stringify({ idRemito: r}),
                data: '{idRemito: "' + r + '"}',
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    toastr.error("Ocurrio un error al mostrar el remito generado.", "Error");
                },
                success: function (response) {
                }
            });



        }


        function abrirModalRecepcionSector() {

            cargarRecepcionAgrupadaPorSector()
            $('#recepcionSectores').modal('show');
        }

        function obteneridSectorProductivoUrl() {
            let url = new URL(window.location.href);
            let sectorProductivo = url.searchParams.get('SP');
            return sectorProductivo;
        }

        //Esta funcion se para poder filtrar todos los datos de las transferencias, cuyo campo origen sea igual al 
        //texto de la ddl seleccionada
        function filtrarSectorOrigen() {

            let ddlSector = document.getElementById('<%=ddlSector.ClientID%>');
            let sector = ddlSector.options[ddlSector.selectedIndex].text;

            let ddlSectorValue = document.getElementById('<%=ddlSector.ClientID%>').value;
            let fDesde = document.getElementById('<%=txtFechaHoy.ClientID%>').value;
            let fHasta = document.getElementById('<%=txtFechaVencimiento.ClientID%>').value;
            //la o en la url es de origen
            window.location.href = "Pre-produccion.aspx?O=" + sector + "&OV=" + ddlSectorValue + "&fDesde=" + fDesde + "&fHasta=" + fHasta;
        }

        function verDetalleTranferencia(idTransferencia, productoDescripcion) {
            console.log(idTransferencia)
            console.log(productoDescripcion)

            $.ajax({
                method: "POST",
                url: "Pre-produccion.aspx/verDetallesPedidos",
                data: JSON.stringify({ idTransferencia: idTransferencia, productoDescripcion: productoDescripcion }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    console.log("Error");
                },
                success: function (data) {
                    const arraydetalleTransferencia = data.d.split(";").filter(Boolean);
                    document.getElementById('tableDetallePedidos').innerHTML = "";
                    document.getElementById('<%= idTransferencia.ClientID %>').value = idTransferencia;
                    let cont = 0;
                    document.getElementById('<%= idsPedidos.ClientID %>').value = "";
                    document.getElementById('<%= datosTransferencias.ClientID %>').value = "";
                    let estado = false;
                    arraydetalleTransferencia.forEach(function (detalleTransferencia) {
                        cont++;
                        let cantidadAConfirmar = "";
                        const partesDetalle = detalleTransferencia.split(",").filter(Boolean);
                        document.getElementById('<%= idsPedidos.ClientID %>').value += partesDetalle[0] + ",";
                        document.getElementById('<%= datosTransferencias.ClientID %>').value += partesDetalle[0] + "," + partesDetalle[12] + ";";
                        const SectorOrigen = partesDetalle[1];
                        const ProductoOrigen = partesDetalle[2];
                        const cantidadOrigen = partesDetalle[3];
                        const estadoTransferencia = partesDetalle[10];
                        const cantEnviada = partesDetalle[12];



                        if (estadoTransferencia == "Confirmado" || estadoTransferencia == "A confirmar") {
                            cantidadAConfirmar = "<input id=\"" + "cantEnviada_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Cantidad\" value=\"" + partesDetalle[12] + "\" oninput=\"validarTextBox(this);\" />";
                            estado = false;
                        }

                        else {
                            cantidadAConfirmar = "<input id=\"cantEnviada_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Cantidad\" value=\"" + partesDetalle[12] + "\" disabled />";
                            estado = true;
                        }
                        const productoDestino = partesDetalle[4];
                        const sectorDestino = partesDetalle[5];
                        const orden = partesDetalle[6];
                        const razonSocialActual = partesDetalle[7];




                        let plantillaDetalleTransferencia = `
                              <tr>
                                  <td>${SectorOrigen}</td>
                                  <td>${ProductoOrigen}</td>
                                  <td style="text-align: right;">${partesDetalle[11]}</td>
                                  <td style="text-align: right;">${cantidadAConfirmar}</td>
                                  <td>${productoDestino}</td>
                                  <td>${sectorDestino}</td>
                                  <td>${orden}</td>
                                  <td>${razonSocialActual}</td>
                              </tr>
                          `;


                        document.getElementById('tableDetallePedidos').innerHTML += plantillaDetalleTransferencia;

                    });
                    $("#modalDetallePedidos").modal("show")
                }
            });
        }


        function guardarDetallesTransferenciasConfirmadas() {

            //    for (var i = 0; i < document.getElementById('DetallesTransferenciasConfirmadas').rows.length; i++) {
            //       let cantidad = document.getElementById('DetallesTransferenciasConfirmadas').rows[i].cells[3].querySelector('input').value;
            //       
            //
            //    }

        }


    </script>

    <script>
        function CheckAll() {
            var total = 0;
            for (var i = 1; i < document.getElementById('editable').rows.length; i++) {
                // document.getElementById('editable').rows[i].cells[4].childNodes[1].children[0].checked = true;
                let checkbox = document.getElementById('editable').rows[i].cells[6].getElementsByTagName('input')[0];


                if (checkbox != null) {
                    checkbox.checked = true;



                    let id = document.getElementById('editable').rows[i].cells[5].innerText;
                    let sectorProductivo = document.getElementById('editable').rows[i].cells[4].innerText;
                    let cantidad = document.getElementById('editable').rows[i].cells[2].innerText;
                    let descripcion = document.getElementById('editable').rows[i].cells[1].innerText;


                    verIngredientesReceta(checkbox.id, id, cantidad, descripcion, sectorProductivo);
                }

            }
        }


        function cargarInsumo() {
        }
    </script>
    <script>
        function Recepcionar(ingredienteId, ingredienteNombre, idSector, SectorProductivo) {
            window.open("../Compras/Entregas.aspx?idP=" + ingredienteId + "&Desc=" + ingredienteNombre + "&idS=" + idSector + "&SP=" + SectorProductivo, '_blank');
        }
    </script>

    <script>
        function unCheckAll() {
            var total = 0;
            for (var i = 1; i < document.getElementById('editable').rows.length; i++) {

                let checkbox = document.getElementById('editable').rows[i].cells[6].getElementsByTagName('input')[0];
                if (checkbox != null) {
                    checkbox.checked = false;



                    let id = document.getElementById('editable').rows[i].cells[5].innerText;
                    let sectorProductivo = document.getElementById('editable').rows[i].cells[4].innerText;
                    let cantidad = document.getElementById('editable').rows[i].cells[2].innerText;
                    let descripcion = document.getElementById('editable').rows[i].cells[1].innerText;


                    // Ejecutar manualmente el evento onchange


                    // let rowId = document.getElementById('editable').rows[i].id;
                    //  let onchangeFunction = checkbox.onchange;
                    //
                    verIngredientesReceta(checkbox.id, id, cantidad, descripcion, sectorProductivo);
                    //  function verIngredientesReceta(idCheckbox, idReceta, cantidad, descripcion, sectorProductivo)
                    //
                    // if (onchangeFunction && typeof onchangeFunction === 'function') {
                    //  onchangeFunction.apply(checkbox);  // Asegurar que 'this' sea la casilla de verificación
                }

                // }
            }
        }
    </script>

    <script>
        function precargarFiltros() {
            //let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            //let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value
            //let ddlSectorProductivo = document.getElementById("ContentPlaceHolder1_ddlSector").value
            //ddlSectorProductivo.value;

            //if (fechad != "" && fechah != "" && ddlSectorProductivo.value != "") {

            //    establecerFechasSeleccionadas();
            //    establecerSectorSeleccionado();
            //} else {

            //    establecerDiaHoy();
            //}


        }
    </script>

    <script>

        $(document).ready(function () {

            precargarFiltros();

            //Este codigo esta para la barra buscadora de recepcion
            // var oTable = $('#idTablaRecepcionSector').dataTable({
            //      "bLengthChange": false,
            //      "pageLength": 100, // Establece la cantidad predeterminada de registros por página
            //      "lengthMenu": [25, 50, 87, 100], // Opciones de cantidad de registros por página
            // });
            //
            // oTable.$('td').editable('../example_ajax.php', {
            //    "callback": function (sValue, y) {
            //        var aPos = oTable.fnGetPosition(this);
            //        oTable.fnUpdate(sValue, aPos[0], aPos[1]);
            //    },
            //    "submitdata": function (value, settings) {
            //        return {
            //            "row_id": this.parentNode.getAttribute('id'),
            //            "column": oTable.fnGetPosition(this)[2]
            //        };
            //    },
            //    "width": "90%",
            //    "height": "100%"
            //});


            var oTable = $('#idTablaRecepcionSector').dataTable({
                "bLengthChange": false,
                "pageLength": 100, // Establece la cantidad predeterminada de registros por página
                "lengthMenu": [25, 50, 87, 100], // Opciones de cantidad de registros por página
            });

            $("#idTablaRecepcionSector_filter").css('display', 'none');

            $('#txtBusqueda').on('keyup', function () {
                $('#idTablaRecepcionSector').DataTable().search(
                    this.value
                ).draw();
            });
            //Aca termina el codigo de la tabla






            var oTable = $('#editable').dataTable({
                "bLengthChange": false,
                "pageLength": 100, // Establece la cantidad predeterminada de registros por página
                "lengthMenu": [25, 50, 87, 100], // Opciones de cantidad de registros por página
            });


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
                "height": "100%"
            });

            $("#editable_filter").css('display', 'none');
        });

        function establecerSectorSeleccionado() {
            let ddlSectorProductivo = document.getElementById("ContentPlaceHolder1_ddlSectorSelecionado").value
            document.getElementById("ContentPlaceHolder1_ddlSector").value = ddlSectorProductivo;

        }


        function verStock(idReceta, cantidad) {

            fetch('Pre-Produccion.aspx/getIngredientesRecetaByIdReceta', {
                method: 'POST',
                body: JSON.stringify({ idReceta: idReceta, cantidad: cantidad }),
                headers: { 'Content-Type': 'application/json' }
            })
                .then(response => response.json())
                .then(data => {


                    //const ingredientesArray = data.d.split(';');
                    const ingredientesArray = data.d.split(';').filter(Boolean);
                    document.getElementById('tablaRecepcion').innerHTML = "";
                    ingredientesArray.forEach(producto => {
                        // Dividir cada elemento en valores individuales usando coma como separador
                        const ingredienteData = producto.split(',');

                        // Ahora puedes acceder a valores específicos del producto
                        const ingredienteId = ingredienteData[0];
                        const ingredienteNombre = ingredienteData[1];
                        const Cantidad = ingredienteData[2];


                        let plantillaIngredientes = `
                     <tr>
                         <td>${ingredienteNombre}</td>
                         <td style="text-align: right;">${Cantidad}</td>
                     </tr>
                 `;
                        document.getElementById('tablaRecepcion').innerHTML += plantillaIngredientes;

                    });
                    $('#modalIngredientes').modal('show');


                })
                .catch(error => {
                    // Código para manejar errores aquí
                    console.error('Error:', error);
                });

        }

        function establecerFechasSeleccionadas() {
            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fehcah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

            fechad = fechad.replaceAll("/", "-");
            fehcah = fehcah.replaceAll("/", "-");

            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechad;
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fehcah
        }



        function verEnvio(idsRecetas) {
            console.log("Recetas provenientes:" + idsRecetas);

            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/getRecetasProveniente",
                data: '{idsRecetas: "' + idsRecetas + '"}',
                contentType: "application/json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    console.log(respuesta.d);

                    const recetasArray = respuesta.d.split(';').filter(Boolean);
                    document.getElementById('tablaEnvioReceta').innerHTML = "";
                    recetasArray.forEach(receta => {
                        const recetaData = receta.split(',');
                        const fecha = recetaData[0];
                        const descripcion = recetaData[1];
                        const sectorProductivo = recetaData[2];
                        const cantidad = recetaData[3];

                        let plantillaIngredientes = `
                            <tr>
                                <td>${sectorProductivo}</td>
                                <td style="text-align: right;">${descripcion}</td>
                                <td style="text-align: right;">${cantidad}</td>
                            </tr>
                        `;
                        document.getElementById('tablaEnvioReceta').innerHTML += plantillaIngredientes;
                    });

                    $('#modalEnvioRecetas').modal('show');
                }
            }); // Cierre del $.ajax()

        } // Cierre de la función verEnvio





        function cargarIngredienteEnvio(id, descripcion, cantidad, idCheckBox) {

            //Esta funcion valida si el producto ya existe en la tabla, de ser asi acumula la cantidad de ese producto

            let checkBox = document.getElementById(idCheckBox);
            console.log("check: " + checkBox)


            if (checkBox.checked == true) {
                let existe = validarProductoExisteTabla(id, cantidad);
                if (existe != true) {
                    $.ajax({
                        method: "POST",
                        url: "Pre-Produccion.aspx/getStockCostoUnidad",
                        data: '{id: "' + id + '"}',
                        contentType: "application/json",
                        dataType: "json",
                        dataType: "json",
                        async: false,
                        error: (error) => {
                            console.log(JSON.stringify(error));
                        },
                        success: (respuesta) => {

                            let datosIngredienteSplit = respuesta.d.split(';').filter(Boolean);
                            let costo = "";
                            let stock = "";
                            let unidad = "";

                            console.log("console:" + datosIngredienteSplit);

                            datosIngredienteSplit.forEach(producto => {
                                let datosIngredientesSplitComa = producto.split(','); // Cambié datosIngredienteSplit por producto
                                costo = datosIngredientesSplitComa[0];
                                stock = datosIngredientesSplitComa[1];
                                unidad = datosIngredientesSplitComa[2];

                            });



                            let plantillaIngredientes = `
                                  <tr>
                                      <td>${id}</td>
                                      <td>${descripcion}</td>
                                      <td style="text-align: right;">${cantidad}</td>
                                      <td>${costo}</td>
                                      <td>${stock}</td>
                                      <td>${unidad}</td>
                                  </tr>
                              `;
                            document.getElementById('tableProductos').innerHTML += plantillaIngredientes;
                        }
                    });
                }


            }
            else {
                quitarProductoTabla(id, cantidad)
            }


        }


        function establecerDiaHoy() {
            var fechas = obtenerRangoFechas();
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.diaActual.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.diaActual.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

        }


        function quitarProductoTabla(id, cantidad) {
            let existe = false;
            for (var i = 1; i < document.getElementById('tableProductos').rows.length; i++) {
                let idTabla = document.getElementById('tableProductos').rows[i].cells[0].innerHTML

                if (id == idTabla) {
                    existe = true;
                    let cantidadTableStr = document.getElementById('tableProductos').rows[i].cells[2].innerHTML;
                    let cantDecimalesTotales = contarDecimales(cantidadTableStr);

                    console.log("cantidadTableStr: " + cantidadTableStr)

                    //let cantidadTable = parseFloat(cantidadTableStr).toFixed(4);

                    // console.log("cantidadTableDecimal: " + cantidadTable)

                    //let cantidadNum = parseFloat(cantidad).toFixed(4); 


                    let cantidadTable = Number(cantidadTableStr);
                    let cantidadNum = Number(cantidad);

                    let resta = cantidadTable - cantidadNum;


                    let cantDecimales = contarDecimales(resta.toString());
                    if (cantDecimales == 0) {
                        resta = resta.toFixed(cantDecimalesTotales);
                    }

                    console.log("cantidadTableSuma: " + resta)
                    document.getElementById('tableProductos').rows[i].cells[2].innerHTML = resta

                    if (resta == 0) {
                        document.getElementById('tableProductos').deleteRow(i);
                    }

                }

                console.log(idTabla)
            }

            return existe;

        }

        function validarProductoExisteTabla(id, cantidad) {

            let existe = false;
            for (var i = 1; i < document.getElementById('tableProductos').rows.length; i++) {
                let idTabla = document.getElementById('tableProductos').rows[i].cells[0].innerHTML

                if (id == idTabla) {
                    existe = true;
                    let cantidadTableStr = document.getElementById('tableProductos').rows[i].cells[2].innerHTML;
                    let cantDecimalesTotales = contarDecimales(cantidadTableStr);

                    let cantidadDecimal = "2.0000";
                    let cantidadDecimal2 = "2.2455";
                    let numeroEntero = "2";


                    //console.log("cantidadTableStr: " + cantidadTableStr)

                    //let cantidadTable = parseFloat(cantidadTableStr).toFixed(4);

                    // console.log("cantidadTableDecimal: " + cantidadTable)

                    // let cantidadNum = parseFloat(cantidad).toFixed(4); 
                    let cantidadNum1 = parseFloat(cantidadDecimal);
                    let cantidadNum2 = parseFloat(cantidadDecimal2);




                    //let suma2 = cantidadNum + cantidadNum2;
                    //console.log("Suma2:" + suma2)


                    let cantidadTable = Number(cantidadTableStr);
                    let cantidadNum = Number(cantidad);



                    let suma = cantidadTable + cantidadNum;

                    let cantDecimales = contarDecimales(suma.toString());
                    if (cantDecimales == 0) {
                        suma = suma.toFixed(cantDecimalesTotales);
                    }


                    //console.log("cantidadTableSuma: " + suma)
                    document.getElementById('tableProductos').rows[i].cells[2].innerHTML = suma;
                }

                //console.log(idTabla)
            }

            return existe;
        }



        function contarDecimales(cadena) {
            // Buscar el punto decimal y obtener los caracteres después de él
            let match = cadena.match(/\.\d+/);
            // Si se encuentra el punto decimal, contar los caracteres después de él
            if (match) {
                return match[0].length - 1; // Restar 1 para excluir el punto decimal
            } else {
                return 0; // Si no se encuentra el punto decimal, no hay decimales
            }
        }

        function formatearFechas(fecha) {

            var partes = fecha.split('/');
            var dia = partes[2];
            var mes = partes[1];
            var anio = partes[0];

            if (dia < 10) {
                dia = '0' + dia;
            }

            if (mes < 10) {
                mes = '0' + mes;
            }

            return fechafinal = anio + '-' + mes + '-' + dia;

        }


        function obtenerRangoFechas() {
            var fechaActual = new Date();
            let diaActual = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());
            var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1);
            var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0);

            return {
                primerDia: primerDiaMes,
                ultimoDia: ultimoDiaMes,
                diaActual: diaActual
            };
        }







        function CargarTablaReceta(idsRecetas) {
            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/GetProductosEnRecetas2",
                data: '{idsRecetas: "' + idsRecetas + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    AgregarATabla(respuesta.d);
                    //document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent = "";
                    /*  PedirStockTotal(id);*/

                }
            });
        }


        function AgregarATabla(list) {
            $('#tableProductos tbody')[0].innerHTML = "";
            let listArr = list.split(";");
            for (let i = 0; i < listArr.length; ++i) {
                if (listArr[i].length > 1) {
                    idCostoFinal = "costofinal" + (i + 1);
                    let item = listArr[i].split(",");
                    let id = item[0].trim();
                    let name = item[1].trim();
                    let cantNecesaria = Number(item[2].replace(',', '.')).toFixed(2);
                    let stock = Number(item[3].replace(',', '.')).toFixed(2)
                    let unidad = item[4].trim();
                    let tipo = item[5].trim();
                    let costo = item[6].trim();

                    let faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-dollar\"></i> <a>"

                    let Costo = "<td style=\" text-align: right\"> " + formatearNumero(Number(costo)) + "</td>";
                    let CostoFinal = "<td style=\" text-align: right\" id=\"" + idCostoFinal.trim() + "\" text-align: right\"> " + 0 + "</td>"
                    let faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-exchange\"></i> <a>"
                    //let faReceta = "<td></td > ";
                    let faReceta = "";


                    if (tipo == "Receta") {
                        faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-exchange\"></i> <a>"
                        //faReceta = "<td><a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-R" + "')> <i id=\"" + id + "-R" + "\" class=\"fa fa-calculator\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Producir_Receta href=GenerarProduccion.aspx?i=" + id + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-cutlery\"></i> </a> " + faChangeProduct + faCosto + "  <button id=\"Recepcion\" type=\"button\" class=\"icon-button\" style=\"float: right; margin-left: 5px; color: black;\" title=\"Recepción\" onclick=\"MostrarRecepcion(" + id + "," + cantNecesaria + ")\"><i class=\"fa fa-arrows-h\"></i></button></td>";
                        faReceta = "<td> <button id=\"Recepcion\" type=\"button\" class=\"icon-button\" style=\"float: right; margin-left: 5px; color: black; border: none;\" title=\"Recepción\" onclick=\"MostrarRecepcion(" + id + "," + cantNecesaria + ")\"><i class=\"fa fa-arrows-h\"></i></button></td>";
                        faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-dollar\"></i> <a>"
                    }

                    let Nombre = "<td> " + name + "</td>";
                    let ID = "<td style=\" text-align: right\"> " + id + "</td>";
                    let STOCK = stock > cantNecesaria ? "<td id=\"" + stock + "-" + name.replaceAll(" ", "") + "-" + id.trim() + "\" style=\" text-align: right;\"> " + stock + "</td>" : "<td id=\"" + stock + "-" + name.replaceAll(" ", "") + "-" + id.trim() + "\" style=\" text-align: right; color: red;\"> " + stock + "</td>"
                    let CantNecesaria = "<td style=\" text-align: right\"> " + formatearNumero(Number(cantNecesaria)) + "</td>";
                    let Unidad = "<td style=\" text-align: left\">" + unidad + "</td>";


                    $('#tableProductos').append(
                        "<tr id='" + tipo + "%" + id + "%" + cantNecesaria + "'>" +
                        ID +
                        Nombre +
                        CantNecesaria +
                        Costo +
                        STOCK +
                        Unidad +
                        //"<td style=\" text-align: right\">" +
                        //"<input type=\"number\" onchange=\"CambiarCostoTotal('" + idCostoFinal + "','" + costo + "',this)\" style=\"width: 100%; text-align: right;\" placeholder=\"Ingresa la cantidad real que utilizaste\" /></td>" +
                        //CostoFinal +
                        faReceta +
                        "</tr>"
                    );

                }
            }
            //document.getElementById("DivTablaRow").style.display = "flex"
        }

        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }


        function verDetallePedidos(idTransferencia, ProductoOrigen) {
            $.ajax({
                method: "POST",
                url: "PedidosOrdenes.aspx/verDetallesPedidos",
                data: JSON.stringify({ idTransferencia: idTransferencia, ProductoOrigen: ProductoOrigen }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    console.log("Error");
                },
                success: function (data) {

                    const arraydetalleTransferencia = data.d.split(";").filter(Boolean);
                    document.getElementById('tableDetalleDatosTransferencia').innerHTML = "";
                    document.getElementById('<%= idTransferencia.ClientID %>').value = idTransferencia;
                    let cont = 0;
                    document.getElementById('<%= idsPedidos.ClientID %>').value = "";
                    let estado = false;
                    arraydetalleTransferencia.forEach(function (detalleTransferencia) {
                        cont++;
                        let cantidadAConfirmar = "";
                        const partesDetalle = detalleTransferencia.split(",").filter(Boolean);
                        document.getElementById('<%= idsPedidos.ClientID %>').value += partesDetalle[0] + ",";
                        const SectorOrigen = partesDetalle[1];
                        const ProductoOrigen = partesDetalle[2];
                        const cantidadOrigen = partesDetalle[3];
                        const estadoTransferencia = partesDetalle[10];



                        if (estadoTransferencia == "Confirmado" || estadoTransferencia == "A confirmar") {
                            cantidadAConfirmar = "<input id=\"" + "cantAproducir_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Cantidad\" value=\"" + partesDetalle[11] + "\" oninput=\"validarTextBox(this);\" />";
                            estado = false;
                        }

                        else {
                            cantidadAConfirmar = "<input id=\"cantAproducir_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Cantidad\" value=\"" + partesDetalle[11] + "\" disabled />";
                            estado = true;
                        }
                        const productoDestino = partesDetalle[4];
                        const sectorDestino = partesDetalle[5];
                        const orden = partesDetalle[6];
                        const razonSocialActual = partesDetalle[7];


                        if (estado == true) {
                        }
                        else {
                        }

                        let plantillaDetalleTransferencia = `
                   <tr>
                       <td>${SectorOrigen}</td>
                       <td>${ProductoOrigen}</td>
                       <td style="text-align: right;">${cantidadOrigen}</td>
                       <td style="text-align: right;">${cantidadAConfirmar}</td>
                       <td>${productoDestino}</td>
                       <td>${sectorDestino}</td>
                       <td>${orden}</td>
                       <td>${razonSocialActual}</td>
                   </tr>
               `;


                        document.getElementById('tableDetalleDatosTransferencia').innerHTML += plantillaDetalleTransferencia;
                    });
                    $("#modalDetalleDatosTransferencia").modal("show")
                }
            });
        }


    </script>


    <script>
        function verDetalleProduccion(sector, idProducto, fecha) {
            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/verDetalleProduccion",
                data: JSON.stringify({ sector: sector, idProducto: idProducto, fecha: fecha }),
                contentType: "application/json",
                dataType: "json",
                error: function (error) {
                    console.log("Error");
                },
                success: function (data) {
                    const arraydetalleProduccion = data.d.split(";").filter(Boolean);

                    document.getElementById('tableDetalleProduccion').innerHTML = "";

                    arraydetalleProduccion.forEach(function (detalleProduccion) {

                        const partesDetalle = detalleProduccion.split(",").filter(Boolean);

                        const sectorDestino = partesDetalle[0];
                        const producto = partesDetalle[1];
                        const cantidad = partesDetalle[2];

                        let plantillaDetalleProduccion = `
                          <tr>
                              <td>${sectorDestino}</td>
                              <td>${producto}</td>
                              <td style="text-align: right;">${cantidad}</td>
                          </tr>
                      `;

                        document.getElementById('tableDetalleProduccion').innerHTML += plantillaDetalleProduccion;
                    });

                    $("#modalDetalleProduccion").modal("show")
                }
            });
        }
    </script>

    <script>
        function getDatosTransferenciaDetalle(sectorOrigen, sectorDestino, fecha) {
            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/getDatosTransferenciaDetalle",
                data: JSON.stringify({ sectorOrigen: sectorOrigen, sectorDestino: sectorDestino, fecha: fecha }),
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: (respuesta) => {
                    console.log(respuesta.d);
                    let detalleTransferencias = respuesta.d;
                    if (detalleTransferencias != null && detalleTransferencias != '[]') {
                        let dt = JSON.parse(detalleTransferencias);
                        let cont = 0;
                        let placeHolderSectores = '';
                        dt.forEach(element => {
                            cont++;
                            let cantidadAConfirmar = `<input id="cantAproducir_${cont}" value="${element.cantidadConfirmada}"
                                                     style="width: 100%; text-align: right; type="text" 
                                                     placeholder="Cantidad" 
                                                     oninput="this.value = this.value.replace(/[^0-9.]/g, '');" />`;


                            let filaProducto = "";

                            if (element.tieneStock == "True") {
                                filaProducto = `<tr>
                                                <td style="display:none">${element.idSectorOrigen}</td>
                                                <td>${element.sectorOrigen}</td>
                                                <td>${element.sectorDestino}</td>
                                                <td style="display:none">${element.idProducto}</td>
                                                <td>${element.producto}</td>
                                                <td class="text-right">${element.cantidad}</td>
                                                <td class="text-right">${element.cantidadConfirmada}</td>
                                                <td class="text-right">${cantidadAConfirmar}</td>
                                            </tr>`;
                            }
                            else {
                                filaProducto = `<tr>
                                                <td style="color:red; display:none">${element.idSectorOrigen}</td>
                                                <td style="color:red">${element.sectorOrigen}</td>
                                                <td style="color:red">${element.sectorDestino}</td>
                                                <td style="color:red; display:none">${element.idProducto}</td>
                                                <td style="color:red">${element.producto}</td>
                                                <td style="color:red" class="text-right">${element.cantidad}</td>
                                                <td style="color:red" class="text-right">${element.cantidadConfirmada}</td>
                                                <td style="color:red" class="text-right">${cantidadAConfirmar}</td>
                                            </tr>`;
                            }

                            placeHolderSectores += filaProducto;
                        });
                        document.getElementById('tableDetallePedidos').innerHTML = placeHolderSectores;
                    } else {
                        document.getElementById('tableDetallePedidos').innerHTML = "";
                    }
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            }); // Cierre del $.ajax()

            $('#modalDetallePedidos').modal('show');
        }
    </script>
    <script>
        function verDetalleRemitoInternoPdf(idRemito) {
            //window.open("ImpresionRemitos2.aspx?r=" + r, "_blank");
            window.open("../Ventas/ImpresionRemitos.aspx?r=" + idRemito, "_blank");
        }
    </script>
    <script>
        function verDetalleRemitoInternoModal(idRemito) {
            document.getElementById('<%= HFIdRemitoInterno.ClientID %>').value = idRemito;
            getItemsRemitosInternos(idRemito);
            $('#modalDetalleRemitoInterno').modal('show');
        }
    </script>

    <script>
        function btnRecepcion_ClientClick() {
            var tableName = "DetalleRemitosInternos";
            var HFItems = document.getElementById('<%= HFItems.ClientID %>');

            for (var i = 1; i < document.getElementById(tableName).rows.length; i++) {
                let producto = document.getElementById(tableName).rows[i].cells[0].innerText;
                let cantEnviada = document.getElementById(tableName).rows[i].cells[1].innerText;
                let cantRecepcionada = document.getElementById(tableName).rows[i].cells[2].querySelector('input').value;

                HFItems.value += producto + "&" + cantEnviada + "&" + cantRecepcionada + ";";
            }

            //eliminar ultimo ;
            HFItems.value = HFItems.value.slice(0, -1);

            console.log("DetalleRemitosInternos");
            console.log(HFItems.value);
        }
    </script>

    <script>
        function getItemsRemitosInternos(idRemito) {
            $.ajax({
                method: "POST",
                url: "Pre-Produccion.aspx/getItemsRemitosInternos",
                data: JSON.stringify({ idRemito: idRemito }),
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: (respuesta) => {
                    console.log(respuesta.d);
                    let detalleRemitoInternos = respuesta.d;
                    if (detalleRemitoInternos != null && detalleRemitoInternos != '[]') {
                        let dt = JSON.parse(detalleRemitoInternos);
                        let placeHolderItemRemitosInternos = '';
                        let cont = 0
                        dt.forEach(element => {
                            cont++
                            let cantidadRecepcionada = `<input id="cantARecepcionar_${cont}" value="${element.cantidadRecepcionada}"
                          style="width: 100%; text-align: right;" 
                          placeholder="Cantidad" 
                          oninput="validarTextBox(this);" />`;

                            placeHolderItemRemitosInternos += `<tr>
                                                               <td >${element.Producto}</td>
                                                               <td class="text-right">${element.cantidadEnviada}</td>
                                                               <td class="text-right">${cantidadRecepcionada}</td>
                                                           </tr>`;
                        });
                        document.getElementById('tableDetalleRemitosInternos').innerHTML = placeHolderItemRemitosInternos;
                    } else {
                        document.getElementById('tableDetalleRemitosInternos').innerHTML = "";
                    }
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            }); // Cierre del $.ajax()
        }
    </script>


        <script>
            $(document).ready(function () {
                document.getElementById("lblSiteMap").innerText = "Produccion / Pre-Produccion";
            });
        </script>


        <script>
            // Esta funcion cambiara el valor de los inputs de fecha agregandole los dias que indique el boton seleccionado
            function setFecha(diasASumar) {
                // Obtener la fecha actual
                var fecha = new Date();

                // Sumar los días especificados
                fecha.setDate(fecha.getDate() + diasASumar);

                // Formatear la fecha a YYYY-MM-DD
                var dia = ('0' + fecha.getDate()).slice(-2);
                var mes = ('0' + (fecha.getMonth() + 1)).slice(-2);
                var anio = fecha.getFullYear();

                // Establecer el valor del textbox de fecha
                var txtFechaDesde = document.getElementById('<%=txtFechaHoy.ClientID%>');
            var txtFechaHasta = document.getElementById('<%=txtFechaVencimiento.ClientID%>');

                txtFechaDesde.value = anio.toString() + "-" + mes.toString() + "-" + dia.toString();
                txtFechaHasta.value = anio.toString() + "-" + mes.toString() + "-" + dia.toString();
            }

        </script>

</asp:Content>
