<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerarProduccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.GenerarProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        tr > td > a {
            color: black !important;
            text-decoration: none;
        }

            tr > td > a:hover, a:focus {
                color: black;
                text-decoration: none;
            }
    </style>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">

                    <div class="ibox-content">
                        <div class="row" style="padding-left: 15px;">

                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h5 style="margin-left: 5%">Productos y Recetas:</h5>
                                        </div>
                                        <div class="col-md-6">
                                            <datalist id="ListaNombreProd" runat="server">
                                            </datalist>
                                            <asp:TextBox runat="server" ID="txtDescripcionProductos" onfocusout="handle(event)"
                                                list="ContentPlaceHolder1_ListaNombreProd" class="form-control"
                                                Style="margin-left: 15px; width: 95%" />
                                            <asp:HiddenField ID="Hiddentipo" runat="server" />
                                            <asp:HiddenField ID="HiddenUnidad" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="margin-top: 8px;">
                                            <a id="StockChange" target="_blank" data-toggle="tooltip" data-placement="top" title data-original-title="Stock" style="color: black;">
                                                <h5 id="StockDisponible"></h5>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Produccion Numero:</h5>
                                            </div>
                                            <div class="col-md-8" id="lblProdNum" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%--Columna 1--%>
                                <div class="col-lg-6">

                                    <div>
                                        <div class="row" style="margin-top: 2%;">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Cantidad a Producir:</h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" onChange="ChangeTableCantidad()" ID="NCantidad" type="number" class="form-control" Style="margin-left: 15px; width: 70%;" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Rinde: </h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NRinde" disabled="true" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Unidad Medida:</h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NUnidadMedida" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Sector:</h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NSector" list="ContentPlaceHolder1_ListaDLLSector" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <%-- Columna 2--%>
                                <div class="col-lg-6">
                                    <div>
                                        <div>
                                            <div class="row" style="margin-top: 2%">
                                                <div class="col-md-4">
                                                    <h5 style="margin-left: 5%">Presentacion:</h5>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="txtPresentacion" list="ContentPlaceHolder1_ListaDLLPresentaciones" class="form-control" Style="margin-left: 15px; width: 70%" />
                                                </div>
                                            </div>
                                        </div>
                                        <div>

                                            <div class="row" style="margin-top: 2%">
                                                <div class="col-md-4">
                                                    <h5 style="margin-left: 5%">Lote:</h5>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="NLote" class="form-control" Style="margin-left: 15px; width: 70%" />
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="row" style="margin-top: 2%">
                                                <div class="col-md-4">
                                                    <h5 style="margin-left: 5%">Marca:</h5>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="NMarca" list="ContentPlaceHolder1_ListaDLLMarca" class="form-control" Style="margin-left: 15px; width: 70%" />
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="row" style="margin-top: 2%">
                                                <div class="col-md-4">
                                                    <h5 style="margin-left: 5%">Cantidad Producida:</h5>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox runat="server" ID="NCantidadProducida" class="form-control" Style="margin-left: 15px; width: 70%; text-align: right;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="DivTablaRow">
                                <div style="margin: 20px;">

                                    <table class="table table-bordered table-hover" id="tableProductos" style="margin-top: 2%; max-width: 99%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 2%; text-align: right;">#</th>
                                                <th style="width: 5%">Insumo/Receta</th>
                                                <th style="width: 2%; text-align: right;">Cantidad Necesaria</th>
                                                <th style="width: 2%; text-align: right;">Costo Unitario</th>
                                                <th style="width: 2%; text-align: right;">Stock</th>
                                                <th style="width: 4%; text-align: left;">Unidad de Medida</th>
                                                <th style="width: 5%; text-align: center;">
                                                    <div class="row">
                                                        Unidad Real

                                                        <div style="float: right; margin-right: 15px;">

                                                            <a onclick="CargarDatosDeLaTabla()" data-toggle="tooltip" data-placement="top" data-original-title="Rellenar con Cantidad Necesaria">
                                                                <i class="fa fa-arrow-circle-o-down" style="color: black;"></i>
                                                            </a>
                                                        </div>
                                                    </div>


                                                </th>
                                                <th style="width: 2%; text-align: right;">Costo total</th>

                                                <th style="width: 2%; text-align: center;"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phTablaProductos" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div>



                                <button class="btn btn-sm btn-primary pull-right m-t-n-xs" style="margin-right: 25px; margin-bottom: 5px; margin-top: 5px;" data-toggle="tooltip" data-placement="top" title data-original-title="Producir"
                                    text="Confirmar Venta" validationgroup="AgregarEntregas" id="btnGuardar" onclick="GenerarProduccionVerificar(event)">
                                    Generar Produccion</button>
                                <div class="pull-right" style="margin-right: 45px; display: flex; margin-bottom: 5px;">
                                    <h4 class="h4" style="margin-right: 25px;">Costo Total Final:  </h4>
                                    <h4 style="margin: auto;" id="CostoFinalTotal">$0</h4>
                                </div>
                            </div>



                            <asp:HiddenField runat="server" ID="idProductosRecetas" />
                            <asp:HiddenField runat="server" ID="hiddenReceta" />
                            <asp:HiddenField runat="server" ID="HiddenRinde" />
                            <asp:HiddenField runat="server" ID="HiddenCosto" />
                            <asp:HiddenField runat="server" ID="HiddenCantidadAnterior" />
                            <asp:HiddenField runat="server" ID="HiddenCostoAnterior" />
                            <asp:HiddenField runat="server" ID="HiddenPrecioVenta" />
                            <asp:HiddenField runat="server" ID="VerSiElProductoFueCargado"></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="ListaTablaFinal" />
                            <asp:HiddenField runat="server" ID="ListaCostoCambiado" />
                            <asp:HiddenField runat="server" ID="StockAlteradoFinal" />
                            <asp:HiddenField runat="server" ID="IDParaBorrar" />
                            <asp:HiddenField runat="server" ID="HistoricoProduccion" />
                            <asp:HiddenField runat="server" ID="CostoOriginalProductos" />
                            <div style="display: none;">

                                <table>
                                    <tr></tr>
                                    <tbody id="GuardarAnteriorHTML">
                                    </tbody>
                                </table>
                            </div>


                            <datalist id="ListaDLLMarca" runat="server">
                                <option>No tiene marca</option>
                            </datalist>
                            <datalist id="ListaDLLSector" runat="server">
                            </datalist>
                            <datalist id="ListaDLLPresentaciones" runat="server">
                            </datalist>
                            <datalist id="ListaDLLUnidadMedida" runat="server">
                            </datalist>
                            <datalist id="ListaDLLProductos" runat="server">
                            </datalist>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <%--MODAL CAMBIAR PRODUCTO--%>

    <div id="modalProductos" class="modal fade">
        <div class="modal-dialog modal-xl" style="width: 850px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <div>
                        <h2 style="font-weight: 700; margin-bottom: 20px;" class="modal-title"><i class="fa fa-exchange" style="margin-right: 15px;"></i>Cambiar Producto</h2>
                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <div style="display: flex">

                                <h4 class="modal-title">Producto para intercambiar: </h4>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div style="display: flex">
                                <h4 id="DescricionProductoACambiar"></h4>
                            </div>
                        </div>




                        <div class="col-md-4">
                            <div style="display: flex">

                                <h4 class="modal-title">Cantidad Utilizada: </h4>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div style="display: flex">
                                <h4 id="CantidadUtilizadaProdOriginal"></h4>
                            </div>
                        </div>


                    </div>
                    <asp:HiddenField runat="server" ID="IDFilaCambiar" />
                    <asp:HiddenField runat="server" ID="IDPosicionCambiar" />

                </div>
                <div class="col-lg-6">

                    <div>
                        <div class="row" style="margin-top: 2%">
                            <div class="col-md-4">
                                <h5 style="margin-left: 5%">Productos:</h5>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtProductoElegido" list="ContentPlaceHolder1_ListaDLLProductos" onfocusout="CargarStock(event)" class="form-control" Style="margin-left: 15px; width: 70%" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 2%;">
                            <div class="col-md-4">
                                <h5 style="margin-left: 5%">Stock Disponible:</h5>
                            </div>
                            <div class="col-md-8">
                                <p id="StockDisponibleChange" class="form-control" style="margin-left: 15px; width: 70%; background: #00000024; text-align: right;" />
                            </div>
                        </div>

                        <div>
                            <div class="row" id="divCantidadNecesaria">
                                <div class="col-md-4">
                                    <h5 style="margin-left: 5%">Cantidad Necesaria:</h5>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="CantidadNecesaria" runat="server" data-toggle="tooltip" data-placement="top" data-original-title="Recuerda que la cantidad necesaria se calculara con cantidad a producir, se podra modificar en Unidad Real" type="number" Style="margin-left: 15px; width: 70%; text-align: right;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div>
                        <table class="table table-bordered table-hover" id="TablaCambiarProductoHistorico" style="margin-top: 2%; max-width: 99%;">
                            <thead>
                                <tr>
                                    <th style="width: 1%; text-align: right;">#</th>
                                    <th style="width: 5%">Producto</th>
                                    <th style="width: 2%; text-align: right;">Cantidad</th>
                                    <th style="width: 2%; text-align: center;"></th>
                                </tr>
                            </thead>
                            <tbody id="tbodyTablaCambiarProducto">
                            </tbody>
                        </table>
                    </div>
                </div>


                <div class="modal-footer" style="border-color: transparent">
                    <asp:LinkButton runat="server" ID="ButtonCambiarProducto" OnClientClick="CambiarProducto(event)" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>


    <%--MODAL STOCK--%>


    <div id="modalStock" class="modal" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h2 style="font-weight: 700; margin-bottom: 20px;" class="modal-title"><i class="fa fa-calculator" style="margin-right: 15px;"></i>Seleccionar Stock</h2>
                    <asp:HiddenField runat="server" ID="IDProductoReceta" />
                    <asp:HiddenField runat="server" ID="IDCalculadoraChange" />

                </div>
                <div>
                    <div id="wizard">
                        <form id="form" action="#" class="wizard-big">
                            <h1>Marca</h1>
                            <fieldset>
                                <div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad Necesaria:
                                            </p>
                                        </div>
                                        <div class="col-md-8">
                                            <p id="CantidadNecesariaParaContinuar1"></p>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad  Total:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="CantidadNecesariaParaContinuarMarca">0.00</p>
                                        </div>
                                        <p id="valivaMarca" class="text-danger text-hide">La cantidad Necesaria y la Cantidad Total tienen que ser IGUALES</p>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Falta para completar:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="RestoNecesariaParaContinuar1"></p>
                                        </div>
                                    </div>
                                    <table id="TableProductosRecetaStockMarca" class="table table-bordered table-hover" style="margin-top: 2%; max-width: 99%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 1%; text-align: right;">#</th>
                                                <th style="width: 5%">Descripcion</th>
                                                <th style="width: 3%; text-align: right;">Stock</th>
                                                <th style="width: 5%;"></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyEditableProductosRecetaStockMarca">
                                            <asp:PlaceHolder ID="phTableProductosRecetasStockMarca" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                    <asp:HiddenField runat="server" ID="ListaOcultaMarcas" />
                                </div>

                            </fieldset>
                            <h1>Presentacion</h1>
                            <fieldset>
                                <div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad Necesaria:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="CantidadNecesariaParaContinuar2"></p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad  Total:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="CantidadNecesariaParaContinuarPresentacion">0.00</p>
                                        </div>
                                        <p id="valivaPres" class="text-danger text-hide">La cantidad Necesaria y la Cantidad Total tienen que ser IGUALES</p>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Falta para completar:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="RestoNecesariaParaContinuar2"></p>
                                        </div>
                                    </div>
                                    <table id="TableProductosRecetaStockPresentacion" class="table table-bordered table-hover" style="margin-top: 2%; max-width: 99%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 1%; text-align: right;">#</th>
                                                <th style="width: 5%">Descripcion</th>
                                                <th style="width: 3%; text-align: right;">Stock</th>
                                                <th style="width: 5%;"></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyEditableProductosRecetaStockPresentacion">
                                            <asp:PlaceHolder ID="phTableProductosRecetasStockPresentacion" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                    <asp:HiddenField runat="server" ID="ListOcultaPresentacion" />
                                </div>

                            </fieldset>
                            <h1>Lotes</h1>
                            <fieldset>
                                <div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad Necesaria:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="CantidadNecesariaParaContinuar3"></p>
                                        </div>
                                        <p id="valivaLotes" class="text-danger text-hide">La cantidad Necesaria y la Cantidad Total tienen que ser IGUALES</p>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad  Total:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="CantidadNecesariaParaContinuarLotes">0.00</p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Falta para completar:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="RestoNecesariaParaContinuar3"></p>
                                        </div>
                                    </div>
                                    <table id="TableProductosRecetaStockLotes" class="table table-bordered table-hover" style="margin-top: 2%; max-width: 99%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 1%; text-align: right;">#</th>
                                                <th style="width: 5%">Descripcion Presentacion</th>
                                                <th style="width: 4%">Lote</th>
                                                <th style="width: 2%; text-align: right;">Stock</th>
                                                <th style="width: 6%;"></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyEditableProductosRecetaStockLotes">
                                            <asp:PlaceHolder ID="phTableProductosRecetasStockLotes" runat="server"></asp:PlaceHolder>

                                        </tbody>
                                    </table>
                                    <asp:HiddenField runat="server" ID="ListOcultaLotes" />
                                </div>

                            </fieldset>
                            <h1>Sectores</h1>
                            <fieldset>
                                <div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad Necesaria:
                                            </p>
                                        </div>
                                        <div class="col-md-8">
                                            <p id="CantidadNecesariaParaContinuar4"></p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Cantidad  Total:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="CantidadNecesariaParaContinuarSector">0.00</p>
                                        </div>
                                        <p id="valivaSector" class="text-danger text-hide">La cantidad Necesaria y la Cantidad Total tienen que ser IGUALES</p>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">

                                            <p style="margin-right: 15px; margin-left: 5px;">
                                                Falta para completar:
                                            </p>
                                        </div>
                                        <div class="col-md-8">

                                            <p id="RestoNecesariaParaContinuar4"></p>
                                        </div>
                                    </div>
                                    <table id="TableProductosRecetaStockSector" class="table table-bordered table-hover" style="margin-top: 2%; max-width: 99%;">
                                        <thead>
                                            <tr>
                                                <th style="width: 1%; text-align: right;">#</th>
                                                <th style="width: 3%">Descripcion</th>
                                                <th style="width: 3%; text-align: right;">Stock</th>
                                                <th style="width: 5%;"></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbodyEditableProductosRecetaStockSector">
                                            <asp:PlaceHolder ID="phTableProductosRecetasStockSector" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                    <asp:HiddenField runat="server" ID="ListOcultaSector" />
                                </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--    MODAL CAMBIAR COSTO--%>
    <div id="modalCosto" class="modal" role="dialog">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <div>
                        <h2 style="font-weight: 700; margin-bottom: 20px;" class="modal-title"><i class="fa fa-dollar" style="margin-right: 15px;"></i>Cambiar Costo de Producto</h2>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div style="display: flex">

                                <h4 class="modal-title">Producto seleccionado: </h4>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div style="display: flex">
                                <h4 id="DescricionProductoCosto"></h4>
                            </div>
                        </div>



                        <div class="col-md-4">
                            <div style="display: flex">

                                <h4 class="modal-title">Costo Actual (Unitario): </h4>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div style="display: flex">
                                <h4 id="CostoActual"></h4>
                            </div>
                        </div>

                    </div>
                    <asp:HiddenField runat="server" ID="IDFilaCambiarCosto" />
                    <asp:HiddenField runat="server" ID="IDPosicion" />

                </div>
                <div class="col-lg-10" style="margin-bottom: 25px;">

                    <div>
                        <div class="row" style="margin-top: 2%">
                            <div class="col-md-4">
                                <h5 style="margin-left: 5%; margin-top: 5%; font-size: 100%;">Costo Nuevo $:</h5>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="CostoNuevo" class="form-control" Style="margin-left: 15px; width: 70%" />
                            </div>
                        </div>
                    </div>
                </div>


                <div class="modal-footer" style="border-color: transparent">
                    <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="CambiarCosto(event)" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="HiddenField3" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <%--CONFIRMAR MODAL COSTO --%>
    <div id="modalVerificacion" class="modal" role="dialog">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <div>
                        <h2 style="font-weight: 700; margin-bottom: 20px;" class="modal-title">Detectamos un cambio de COSTO.  </h2>
                        <h3>¿Desea actualizar el costo de los Productos y de Las Recetas donde se encuentre ese Producto ?</h3>
                    </div>
                </div>

                <div class="modal-footer" style="border-color: transparent">
                    <asp:LinkButton runat="server" ID="LinkButton2" OnClientClick="GenerarProduccion(event,'Actualizar')" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Actualizar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="GenerarProduccion(event, 'Cancelar')"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <%--CONFIRMAR MODAL COSTO ORIGINAL --%>
    <div id="modalVolverCostoOriginal" class="modal" role="dialog">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <div>
                        <h2 style="font-weight: 700; margin-bottom: 20px;" class="modal-title">Usted va a volver al Costo Original.  </h2>
                        <h3>¿Seguro que quiere volver al Costo original de este Producto?</h3>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="idSimbol" />
                <asp:HiddenField runat="server" ID="idToChange1" />

                <div class="modal-footer" style="border-color: transparent">
                    <asp:LinkButton runat="server" ID="LinkButton3" OnClientClick="ConfirmarCambio(event)" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Aceptar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <%--CONFIRMAR MODAL Producto ORIGINAL --%>
    <div id="modalVolverProductoOriginal" class="modal" role="dialog">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <div>
                        <h2 style="font-weight: 700; margin-bottom: 20px;" class="modal-title">Usted va a volver al Producto/Receta Original.  </h2>
                        <h3>¿Seguro que quiere volver al Producto/Receta Original?</h3>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="IDVolerProdOriginal" />

                <div class="modal-footer" style="border-color: transparent">
                    <asp:LinkButton runat="server" ID="LinkButton4" OnClientClick="VolverProductoOriginal(event)" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Aceptar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script>

        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
            let txtProd = document.getElementById("ContentPlaceHolder1_txtDescripcionProductos")
            if (txtProd.value != "") {
                let id = txtProd.value.split("-")[0].trim()
                /*  PedirStockTotal(id);*/
            }

            /*  $("#wizard").steps(); */
            $("#form").steps({
                labels: {
                    cancel: "Cancelar",
                    previous: 'Anterior',
                    next: 'Siguiente',
                    finish: 'Guardar',
                    current: ''
                },
                enableFinishButton: true,
                bodyTag: "fieldset",
                onStepChanging: function (event, currentIndex, newIndex) {
                    // Always allow going backward even if the current step contains invalid fields!
                    if (newIndex === 3) {
                        // Habilita el botón "Finish"

                        $("#form").find("a[href='#finish']").show();
                    } else {
                        // Desactiva el botón "Finish"
                        $("#form").find("a[href='#finish']").hide();
                    }
                    if (newIndex < currentIndex) {
                        return true;
                    }

                    if (currentIndex == 0) {
                        let trueOrFalse = VerificarPasoMarcas()
                        if (trueOrFalse) {
                            document.getElementById('valivaMarca').className = 'text-danger text-hide'
                            return true
                        } else {
                            document.getElementById('valivaMarca').className = 'text-danger'
                            return false
                        }
                    }

                    if (currentIndex == 1) {
                        let trueOrFalse = VerificarPasoPresentaciones()
                        if (trueOrFalse) {
                            document.getElementById('valivaPres').className = 'text-danger text-hide'
                            return true
                        } else {
                            document.getElementById('valivaPres').className = 'text-danger'
                            return false
                        }
                    }


                    if (currentIndex == 2) {
                        let trueOrFalse = VerificarPasoLotes()
                        if (trueOrFalse) {
                            document.getElementById('valivaLotes').className = 'text-danger text-hide'
                            return true
                        } else {
                            document.getElementById('valivaLotes').className = 'text-danger'
                            return false
                        }
                    }


                    if (currentIndex == 3) {
                        let trueOrFalse = VerificarPasoSectores()
                        if (trueOrFalse) {
                            document.getElementById('valivaSector').className = 'text-danger text-hide'
                            return true
                        } else {
                            document.getElementById('valivaSector').className = 'text-danger'
                            return false
                        }
                    }
                    return true

                },
                onStepChanged: function (event, currentIndex, priorIndex) {
                    // Suppress (skip) "Warning" step if the user is old enough.
                    //if (currentIndex === 2 && Number($("#age").val()) >= 18) {
                    //    $(this).steps("next");
                    //}

                    // Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
                    if (currentIndex === 2 && currentIndex === 3) {
                        $(this).steps("previous");
                    }
                },
                onFinishing: function (event, currentIndex) {
                    var form = $(this);
                    //----------------------------------------------------------------------------- REVISAR 
                    // Disable validation on fields that are disabled.
                    // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
                    /*                    form.validate().settings.ignore = ":disabled";*/
                    let trueOrFalse = VerificarPasoSectores()
                    if (trueOrFalse) {
                        document.getElementById('valivaSector').className = 'text-danger text-hide'

                    } else {
                        document.getElementById('valivaSector').className = 'text-danger'
                        return false
                    }
                    return true;

                },
                onCanceled: function (event, currentIndex) {
                    var form = $(this);
                    //----------------------------------------------------------------------------- REVISAR

                    // Submit form input
                    $("#modalStock").modal("hide");

                    // Reset Form
                    $("#form")[0].reset();
                    // Go to First Tab
                    $("#form-t-0").trigger("click");
                    // Disable other Tabs  
                    $('.done').removeClass('done').addClass('disabled');

                },
                onFinished: function (event, currentIndex) {
                    TrueOrFalse = GuardarStockEditado();
                    $("#modalStock").modal("hide");
                    if (TrueOrFalse) {
                        toastr.success("Se descontara el stock cuando Generes la produccion")
                        // Reset Form
                        $("#form")[0].reset();
                        // Go to First Tab
                        $("#form-t-0").trigger("click");
                        // Disable other Tabs  
                        $('.done').removeClass('done').addClass('disabled');

                    }
                    //----------------------------------------------------------------------------- REVISAR

                }
                // Submit form input


            })

            var list = $(".number")
            for (let i = 0; i < list.length; ++i) {
                list[i].style.display = "none";
            }
            $("#form").find("a[href='#finish']").hide();

            let url = new URL(window.location.href);

            let PPro = url.searchParams.get('PPro');

            // Obtener el valor del parámetro 'PR'
            let PR = url.searchParams.get('PR');

            // Obtener el valor del parámetro 'C'
            let C = url.searchParams.get('C');


            let ID = url.searchParams.get('i');

            if(PR != null)
            {
               
 
                 let DescripcionProducto = document.getElementById('<%=txtDescripcionProductos.ClientID%>').value = PR;
                 document.getElementById('<%=txtDescripcionProductos.ClientID%>').value = ID + " - " + PR + " - " + "Receta";

                 handle();
                 C = C.replace(',', '.');
                 document.getElementById('<%=NCantidad.ClientID%>').value = C;

                 ChangeTableCantidad();
                 //idReceta.ToString() + " - " + DescripcionReceta + " - " + "Receta";
            }

           

        });


        function CambiarCosto(e) {
            e.preventDefault();
            id2 = document.getElementById("ContentPlaceHolder1_IDFilaCambiarCosto").innerText;
            costoNuevo = document.getElementById("ContentPlaceHolder1_CostoNuevo").value;
            let list = document.getElementById(id2);
            let list2 = list.childNodes
            let costoviejo = list2[3].innerText;
            list2[3].innerText = Number(costoNuevo).toFixed(2);
            document.getElementById("ContentPlaceHolder1_CostoOriginalProductos").innerText += id2 + "-" + costoviejo + "///";
            id = id2.split("%")[1].trim()
            console.log(list2[8])
            let faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=\"Vover Costo Original\" class=\"CambioCostoActivo\"  style=\"color: blue;\"  onclick=ModalCambiarCosto('" + id + "-P" + "'" + ',' + "this)>  <i id=\"" + id + "-P" + "\" style=\"color: blue;\" class=\"fa fa-dollar\"></i> <a>"
            let faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=\"Cambiar Producto\"  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-exchange\"></i> <a>"
            let faReceta = "<a data-toggle=tooltip data-placement=top data-original-title=\"Elegir Stock\"  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-P" + "')> <i id=\"" + id + "-P" + "\" class=\"fa fa-calculator\"></i> </a> ";

            if (id.includes("Receta")) {

                faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=\"Cambiar Producto\"  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-exchange\"></i> <a>"
                faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=\"Vover Costo Original\"  style=\"color: black;\" class=\"CambioCostoActivo\"  onclick=ModalCambiarCosto('" + id + "-R" + "'" + ',' + "this)>  <i id=\"" + id + "-R" + "\"  style=\"color: blue;\" class=\"fa fa-dollar\"></i> <a>"
                faReceta = "<a data-toggle=tooltip data-placement=top data-original-title=\"Elegir Stock\"  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-R" + "')> <i id=\"" + id + "-R" + "\" class=\"fa fa-calculator\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Producir_Receta href=GenerarProduccion.aspx?i=" + id + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-cutlery\"></i> </a> " + faChangeProduct + faCosto;

            }
            list2[9].innerHTML = faReceta + faChangeProduct + faCosto;
            $("#modalCosto").modal("hide");
            RecargarLaTabla();
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
                    let faReceta = "<td><a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-P" + "')> <i id=\"" + id + "-P" + "\" class=\"fa fa-calculator\"></i> </a> " + faChangeProduct + faCosto + "  </td > ";

                    if (tipo == "Receta") {
                        faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-exchange\"></i> <a>"
                        faReceta = "<td><a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-R" + "')> <i id=\"" + id + "-R" + "\" class=\"fa fa-calculator\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Producir_Receta href=GenerarProduccion.aspx?i=" + id + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-cutlery\"></i> </a> " + faChangeProduct + faCosto + "  </td>";
                        faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-dollar\"></i> <a>"
                    }

                    let Nombre = "<td> " + name + "</td>";
                    let ID = "<td style=\" text-align: right\"> " + id + "</td>";
                    let STOCK = stock > cantNecesaria ? "<td id=\"" + stock + "-" + name.replaceAll(" ", "") + "-" + id.trim() + "\" style=\" text-align: right;\"> " + stock + "</td>" : "<td id=\"" + stock + "-" + name.replaceAll(" ", "") + "-" + id.trim() + "\" style=\" text-align: right; color: red;\"> " + stock + "</td>"
                    let CantNecesaria = "<td style=\" text-align: right\"> " + formatearNumero(Number(cantNecesaria)) + "</td>";
                    let Unidad = "<td style=\" text-align: left\">" + unidad + "</td>";


                    if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + id + "," + cantNecesaria)) {
                        $('#tableProductos').append(
                            "<tr id='" + tipo + "%" + id + "%" + cantNecesaria + "'>" +
                            ID +
                            Nombre +
                            CantNecesaria +
                            Costo +
                            STOCK +
                            Unidad +
                            "<td style=\" text-align: right\">" +
                            "<input type=\"number\" onchange=\"CambiarCostoTotal('" + idCostoFinal + "','" + costo + "',this)\" style=\"withd 100%; text-align: right;\" placeholder=\"Ingresa la cantidad real que utilizaste\" /></td > " +
                            CostoFinal +
                            faReceta +
                            "</tr>"
                        );
       <%--             if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += producto.split('-')[0].trim() + "-" + tipo + "-" + Venta.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim() + "-" + Costo.toString().trim();
                }
                else {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + producto.split('-')[0].trim() + "-" + tipo + "-" + Venta.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim() + "-" + Costo.toString().trim();
                }--%>


                    }

                }
            }
            document.getElementById("DivTablaRow").style.display = "flex"
        }


        function ModalCambiarCosto(id, thiss = "false") {
            if (thiss == "false") {
                $("#modalCosto").modal("show");
                document.getElementById("ContentPlaceHolder1_CostoNuevo").value = "";
                let idProdRect = id.split("-")[0].trim();
                let tipo = '';
                let posicion = i;
                if (id.split("-")[1].trim() == "P") {
                    tipo = "Producto";
                } else {
                    tipo = "Receta";
                }
                /* CostoActual*/
                var resume_table = document.getElementById("tableProductos");
                let stock = 0;
                let idfinal = "";
                for (var i = 1, row; row = resume_table.rows[i]; i++) {
                    tipoprod = row.id.split("%")[0]
                    id = row.id.split("%")[1]
                    if (idProdRect == id && tipo == tipoprod) {
                        idfinal = row.id;
                        posicion = i - 1;
                    }


                }
                document.getElementById("ContentPlaceHolder1_IDFilaCambiarCosto").textContent = idfinal;
                document.getElementById("ContentPlaceHolder1_IDPosicion").textContent = posicion;
                list = document.getElementById(idfinal)
                console.log(list)
                type = "";
                if (list.id.includes("Receta")) {
                    type = "Receta"
                } else {
                    type = "Producto"
                }
                list2 = list.childNodes;
                idProdTable = list2[0].innerHTML;
                descripcion = list2[1].innerHTML;
                costo = list2[3].innerHTML;
                document.getElementById("CostoActual").innerText = costo;
                document.getElementById("DescricionProductoCosto").innerText = idProdTable + " - " + descripcion + " - " + type;
            } else {
                document.getElementById("ContentPlaceHolder1_idSimbol").innerText = thiss.id;
                document.getElementById("ContentPlaceHolder1_idToChange1").innerText = id;
                $("#modalVolverCostoOriginal").modal("show");
                /* ModalVolverCostoOriginal(id, thiss);*/
            }
        }
        function ConfirmarCambio(e) {
            e.preventDefault();
            $("#modalVolverCostoOriginal").modal("hide");
            ModalVolverCostoOriginal();
        }

        function ModalVolverCostoOriginal() {
            id = document.getElementById("ContentPlaceHolder1_idToChange1").innerText;
            console.log(id);
            let id2 = id.split("-")[0]
            let type = id.split("-")[1]
            let listCostoOriginales = document.getElementById("ContentPlaceHolder1_CostoOriginalProductos").innerText
            let arrList = listCostoOriginales.split("///");
            let itemExact = ""
            let nuevalista = "";
            for (var i = 0; i < arrList.length; i++) {
                if (arrList[i] != "") {
                    let arr = arrList[i].split("%")

                    if (arr[0][0] == type && arr[1] == id2) {
                        itemExact = arrList[i];
                    } else {
                        nuevalista += arrList[i] + "///"
                    }
                }
            }
            document.getElementById("ContentPlaceHolder1_CostoOriginalProductos").innerText = nuevalista;
            let costoAntiguo = itemExact.split("-")[1];
            let idBuscar = itemExact.split("-")[0];
            let list = document.getElementById(idBuscar);
            let list2 = list.childNodes;
            list2[3].innerText = costoAntiguo;
            list3 = list2[9]
            let faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-dollar\"></i> <a>"
            let faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-exchange\"></i> <a>"
            let faReceta = "<a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-P" + "')> <i id=\"" + id + "-P" + "\" class=\"fa fa-calculator\"></i> </a> " + faChangeProduct + faCosto;

            if (id.includes("Receta")) {

                faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-exchange\"></i> <a>"
                faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-dollar\"></i> <a>"
                faReceta = "<td><a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-R" + "')> <i id=\"" + id + "-R" + "\" class=\"fa fa-calculator\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Producir_Receta href=GenerarProduccion.aspx?i=" + id + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-cutlery\"></i> </a> " + faChangeProduct + faCosto + "  </td>";

            }
            list3.innerHTML = faReceta;
            RecargarLaTabla();
        }
        function EliminarDatosDeLaLista(id) {
            let list = document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent;
            let list2 = list.split("_")
            let newList = ""
            for (let item of list2) {
                if (!item.includes(id)) {
                    if (item != "") {
                        newList += item + "_";
                    }
                }
            }
            return newList;

        }

        function GuardarStockEditado() {
            let list = document.getElementById("ContentPlaceHolder1_StockAlteradoFinal");

            let id = document.getElementById("ContentPlaceHolder1_IDProductoReceta").textContent;
            document.getElementById(id).style.color = "blue";
            let idCalculadoraAzul = document.getElementById("ContentPlaceHolder1_IDParaBorrar").textContent
            if (idCalculadoraAzul != "") {
                let newlist = EliminarDatosDeLaLista(idCalculadoraAzul);
                list.textContent = newlist;
            }
            list.textContent += id.trim() + ";";
            list.textContent += EncontrarMarcas().trim() + ";";
            list.textContent += EncontrarPresentaciones().trim() + ";";
            list.textContent += EncontrarLotes().trim() + ";";
            list.textContent += EncontrarSectores().trim() + ";" + document.getElementById("CantidadNecesariaParaContinuar4").innerText + ";" + "_";

            return true;
        }


        function EncontrarMarcas() {
            var resume_table = document.getElementById("TableProductosRecetaStockMarca");
            let list = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 0) {
                        list += col.innerText + "%";
                    }
                    if (j == 3) {
                        list += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;
                    }
                }
                list += "/";
            }
            return list;

        }


        function EncontrarPresentaciones() {
            var resume_table = document.getElementById("TableProductosRecetaStockPresentacion");
            let list = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 0) {
                        list += col.innerText + "%";
                    }
                    if (j == 3) {
                        list += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;
                        list += "%";
                    }
                }
                list += "/";
            }
            return list;

        }

        function EncontrarLotes() {
            var resume_table = document.getElementById("TableProductosRecetaStockLotes");
            let list = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 0) {
                        list += col.innerText + "%";
                    }
                    if (j == 4) {
                        list += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;
                        list += "%";
                    }
                }
                list += "/";
            }
            return list;

        }


        function EncontrarSectores() {
            var resume_table = document.getElementById("TableProductosRecetaStockSector");
            let list = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 0) {
                        list += col.innerText + "%";
                    }
                    if (j == 3) {
                        list += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;
                        list += "%";
                    }
                }
                list += "/";
            }
            return list;

        }


        function VerificarPasoMarcas() {
            let TotalStock = Number(document.getElementById("CantidadNecesariaParaContinuarMarca").innerHTML)
            let obtenido = Number(document.getElementById("CantidadNecesariaParaContinuar1").innerText)
            if (TotalStock != obtenido) {
                return false
            } else {
                return true
            }

        }
        function VerificarPasoPresentaciones() {
            let TotalStock = Number(document.getElementById("CantidadNecesariaParaContinuarPresentacion").innerHTML)
            let obtenido = Number(document.getElementById("CantidadNecesariaParaContinuar2").innerText)
            if (TotalStock != obtenido) {
                return false
            } else {
                return true
            }

        }
        function VerificarPasoLotes() {
            let TotalStock = Number(document.getElementById("CantidadNecesariaParaContinuarLotes").innerHTML)
            let obtenido = Number(document.getElementById("CantidadNecesariaParaContinuar3").innerText)
            if (TotalStock != obtenido) {
                return false
            } else {
                return true
            }

        }
        function VerificarPasoSectores() {
            let TotalStock = Number(document.getElementById("CantidadNecesariaParaContinuarSector").innerHTML)
            let obtenido = Number(document.getElementById("CantidadNecesariaParaContinuar4").innerText)
            if (TotalStock != obtenido) {
                return false
            } else {
                return true
            }

        }

        function AgregarElTotalAReducir(id) {
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                if (row.id.split("%")[0][0] == id.split("-")[1]) {
                    let id2 = 0;
                    let stock = 0;
                    for (var j = 0, col; col = row.cells[j]; j++) {
                        //alert(col[j].innerText);
                        if (j == 0) {
                            if (col.innerText.trim() == id.split("-")[0]) {
                                id2 = col.innerText.trim();
                            }
                        }
                        if (j == 3) {
                            stock = Number(col.innerText);
                        }
                        if (j == 6 && id2 == id.split("-")[0]) {
                            col2 = col.childNodes[0];
                            if (col2.value == "" || col2.value <= 0) {
                                toastr.error("No puede continuar si no ingreso unidad real para este producto.")
                                return false;
                            }
                            if (stock <= 0) {
                                toastr.error("No puede continuar si el stock es negativo, se descontaran automaticamente.")
                                return false;
                            }
                            document.getElementById("CantidadNecesariaParaContinuar1").innerText = col2.value;
                            document.getElementById("CantidadNecesariaParaContinuar2").innerText = col2.value;
                            document.getElementById("CantidadNecesariaParaContinuar3").innerText = col2.value;
                            document.getElementById("CantidadNecesariaParaContinuar4").innerText = col2.value;
                            return true;
                        }

                    }
                }
            }

        }
        function SumarCostoTotal() {
            var resume_table = document.getElementById("tableProductos");
            let costoTotal = 0;
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 7) {
                        if (col.innerText == "") {
                            costoTotal += 0;
                        } else {

                            costoTotal += revertirNumero(col.innerText)
                        }

                    }



                }



            }
            document.getElementById("CostoFinalTotal").innerText = "$" + formatearNumero(costoTotal);
        }


        function VerificarSiExisteStockEnList(id) {
            /*            let id2 = document.getElementById("ContentPlaceHolder1_IDParaBorrar").textContent;*/
            let list = document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent;
            let list2 = list.split("_")
            for (let i = 0; i < list2.length; ++i) {
                if (list2[i].includes(id)) {
                    return list2[i];
                }

            }
            return "";

        }

        function ModalTablaStocks(id) {
            let comprobacion = AgregarElTotalAReducir(id);
            document.getElementById("CantidadNecesariaParaContinuarMarca").innerText = 0;
            document.getElementById("CantidadNecesariaParaContinuarPresentacion").innerText = 0;
            document.getElementById("CantidadNecesariaParaContinuarLotes").innerText = 0;
            document.getElementById("CantidadNecesariaParaContinuarSector").innerText = 0;
            document.getElementById("ContentPlaceHolder1_IDParaBorrar").textContent = "";
            let stock = VerificarSiExisteStockEnList(id)
            if (comprobacion == false) {
                return false;
            }


            if (stock != "") {

                document.getElementById("ContentPlaceHolder1_IDParaBorrar").textContent = stock;

            }
            $("#form")[0].reset();
            // Go to First Tab
            $("#form-t-0").trigger("click");
            // Disable other Tabs  
            $('.done').removeClass('done').addClass('disabled');
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/GetStockProdRect",
                data: '{idProd: "' + id + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    if (respuesta.d == 'Error') {
                        toastr.error("Actualmente no existen Stocks para este Producto. Si continua se Generaran Stocks Negativos.")
                        return false
                    }
                    AgregarAModalStock(respuesta.d);
                    $("#modalStock").modal("show");

                }
            });

        }

        function AgregarAModalStock(listStock) {
            let ArrList = listStock.split(";")
            ids = ArrList[0];
            document.getElementById("ContentPlaceHolder1_IDProductoReceta").textContent = "";
            document.getElementById("ContentPlaceHolder1_IDProductoReceta").textContent = ids;
            StockMarca = ArrList[1];
            StockPresentacion = ArrList[2];
            StockLote = ArrList[3];
            StockSectores = ArrList[4];
            AgregarTablaMarca(StockMarca)
            AgregarTablaPresentaciones(StockPresentacion)
            AgregarTablaLote(StockLote);
            AgregarTablaSectores(StockSectores)
            let stock = document.getElementById("ContentPlaceHolder1_IDParaBorrar").textContent
            if (stock != "") {
                AgregarATodasLasTablas(stock);
            }

        }


        function AgregarATodasLasTablas(stock) {
            let arr = stock.split(";")
            let marcas = arr[1];
            let pres = arr[2];
            let lotes = arr[3];
            let sector = arr[4];
            let cantfinal = 0;


            // MARCAS
            let marcasfinal = marcas.split("/");
            var resume_table = document.getElementById("TableProductosRecetaStockMarca");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                cant = Number(marcasfinal[i - 1].split('%')[1]);
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 3) {

                        col.childNodes[0].value = cant;

                    }
                }
            }
            cantfinal = Number(document.getElementById("CantidadNecesariaParaContinuar1").textContent);
            document.getElementById("CantidadNecesariaParaContinuarMarca").textContent = cantfinal.toFixed(2);


            // PRESENTACION
            let presfinal = pres.split("/");
            var resume_table = document.getElementById("TableProductosRecetaStockPresentacion");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                cant = Number(presfinal[i - 1].split('%')[1]);
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 3) {

                        col.childNodes[0].value = cant;

                    }
                }
            }
            cantfinal = Number(document.getElementById("CantidadNecesariaParaContinuar2").textContent);
            document.getElementById("CantidadNecesariaParaContinuarPresentacion").textContent = cantfinal.toFixed(2);
            // LOTES
            let lotesfinal = lotes.split("/");
            var resume_table = document.getElementById("TableProductosRecetaStockLotes");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                cant = Number(lotesfinal[i - 1].split('%')[1]);
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 4) {

                        col.childNodes[0].value = cant;

                    }
                }
            }
            cantfinal = Number(document.getElementById("CantidadNecesariaParaContinuar3").textContent);
            document.getElementById("CantidadNecesariaParaContinuarLotes").textContent = cantfinal.toFixed(2);
            // SECTOR
            let sectorfinal = sector.split("/");
            var resume_table = document.getElementById("TableProductosRecetaStockSector");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                cant = Number(sectorfinal[i - 1].split('%')[1]);
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 3) {

                        col.childNodes[0].value = cant;

                    }
                }
            }
            cantfinal = Number(document.getElementById("CantidadNecesariaParaContinuar4").textContent);
            document.getElementById("CantidadNecesariaParaContinuarSector").textContent = cantfinal.toFixed(2);


        }

        // TABLA MARCA MODAL
        function AgregarTablaMarca(StockMarca) {
            let arr2 = StockMarca.split("%")
            let tbody = document.getElementById("tbodyEditableProductosRecetaStockMarca")
            tbody.innerHTML = ""
            for (let i = 0; i < arr2.length; ++i) {
                if (arr2[i].trim() != "") {
                    arr = arr2[i].split("-")
                    let idMarca = arr[0]
                    let DescripcionMarca = arr[1]
                    let stock = arr[2]
                    let ID = "<td style=\" text-align: right;\"> " + idMarca + "</td>";
                    let DESCRIPCION = "<td style=\" text-align: left\"> " + DescripcionMarca + "</td>";
                    let STOCK = "<td style=\" text-align: right;\"> " + Number(stock).toFixed(2) + "</td>";
                    $('#TableProductosRecetaStockMarca').append(
                        "<tr id='" + idMarca + "%" + DescripcionMarca + "'>" +
                        ID +
                        DESCRIPCION +
                        STOCK +
                        "<td style=\" text-align: center\">" +
                        "<input type=\"number\" style=\"withd 80%;text-align: right;\" onchange=\"ValidarInputMarca()\" placeholder=\"Ingresa la cantidad\" /></td > " +
                        "</tr>"
                    );
                }
            }
        }
        function ValidarInputMarca() {
            var resume_table = document.getElementById("TableProductosRecetaStockMarca");
            let Cant = 0;
            document.getElementById("CantidadNecesariaParaContinuarMarca").innerText = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 3) {
                        Cant += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;

                    }
                }
            }
            let CantTotal = Number(document.getElementById("CantidadNecesariaParaContinuar1").textContent).toFixed(2);
            document.getElementById("RestoNecesariaParaContinuar1").innerText = Number(CantTotal - Cant).toFixed(2);
            document.getElementById("CantidadNecesariaParaContinuarMarca").innerText = Cant.toFixed(2);

        }


        // Tabla PRESENTACION MODAL
        function AgregarTablaPresentaciones(StockPresentacion) {
            let arr2 = StockPresentacion.split("%")
            let tbody = document.getElementById("tbodyEditableProductosRecetaStockPresentacion")
            tbody.innerHTML = ""
            for (let i = 0; i < arr2.length; ++i) {
                if (arr2[i].trim() != "") {
                    arr = arr2[i].split("-")
                    let idPresentacion = arr[0]
                    let DescripcionPres = arr[1]
                    let stock = arr[2]
                    let ID = "<td style=\" text-align: right;\"> " + idPresentacion + "</td>";
                    let DESCRIPCION = "<td style=\" text-align: left\"> " + DescripcionPres + "</td>";
                    let STOCK = "<td style=\" text-align: right;\"> " + Number(stock).toFixed(2) + "</td>";
                    $('#TableProductosRecetaStockPresentacion').append(
                        "<tr id= id=" + idPresentacion + "%" + DescripcionPres + ">" +
                        ID +
                        DESCRIPCION +
                        STOCK +
                        "<td style=\" text-align: center\">" +
                        "<input type=\"number\" style=\"withd 90%;     text-align: right;\"  onchange=\"ValidarInputPresentaciones()\" placeholder=\"Ingresa la cantidad\" /></td > " +
                        "</tr>"
                    );
                }
            }


        }


        function ValidarInputPresentaciones() {
            var resume_table = document.getElementById("TableProductosRecetaStockPresentacion");
            let Cant = 0;
            document.getElementById("CantidadNecesariaParaContinuarPresentacion").innerText = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 3) {
                        Cant += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;

                    }
                }
            }
            let CantTotal = Number(document.getElementById("CantidadNecesariaParaContinuar2").textContent).toFixed(2);
            document.getElementById("RestoNecesariaParaContinuar2").innerText = Number(CantTotal - Cant).toFixed(2);
            document.getElementById("CantidadNecesariaParaContinuarPresentacion").innerText = Cant.toFixed(2);

        }

        // TABLA LOTES MODAL
        function AgregarTablaLote(StockLote) {
            let arr2 = StockLote.split("%")
            let tbody = document.getElementById("tbodyEditableProductosRecetaStockLotes")
            tbody.innerHTML = ""
            for (let i = 0; i < arr2.length; ++i) {
                if (arr2[i].trim() != "") {
                    arr = arr2[i].split("-")
                    let idPresentacion = arr[0]
                    let DescripcionPres = arr[1]
                    let lote = arr[2]
                    let stock = arr[3]
                    let ID = "<td style=\" text-align: right;\"> " + idPresentacion + "</td>";
                    let DESCRIPCION = "<td style=\" text-align: left\"> " + DescripcionPres + "</td>";
                    let STOCK = "<td style=\" text-align: right;\"> " + Number(stock).toFixed(2) + "</td>";
                    let LOTE = "<td style=\" text-align: left\"> " + lote + "</td>";
                    $('#TableProductosRecetaStockLotes').append(
                        "<tr id=" + lote + "%" + idPresentacion + ">" +
                        ID +
                        DESCRIPCION +
                        LOTE +
                        STOCK +
                        "<td style=\" text-align: center\">" +
                        "<input type=\"number\" style=\"withd 80%;     text-align: right;\" onchange=\"ValidarInputLotes()\" placeholder=\"Ingresa la cantidad\" /></td > " +
                        "</tr>"
                    );
                }
            }
        }


        function ValidarInputLotes() {
            var resume_table = document.getElementById("TableProductosRecetaStockLotes");
            let Cant = 0;
            document.getElementById("CantidadNecesariaParaContinuarLotes").innerText = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 4) {

                        Cant += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;

                    }
                }
            }
            let CantTotal = Number(document.getElementById("CantidadNecesariaParaContinuar3").textContent).toFixed(2);
            document.getElementById("RestoNecesariaParaContinuar3").innerText = Number(CantTotal - Cant).toFixed(2);
            document.getElementById("CantidadNecesariaParaContinuarLotes").innerText = Cant.toFixed(2);

        }

        // TABLA SECTORES MODAL
        function AgregarTablaSectores(StockSectores) {
            let arr2 = StockSectores.split("%")
            let tbody = document.getElementById("tbodyEditableProductosRecetaStockSector")
            tbody.innerHTML = ""
            for (let i = 0; i < arr2.length; ++i) {
                if (arr2[i].trim() != "") {
                    arr = arr2[i].split("-")
                    let idSector = arr[0]
                    let DescripSect = arr[1]
                    let stock = arr[2]
                    let ID = "<td style=\" text-align: right;\"> " + idSector + "</td>";
                    let DESCRIPCION = "<td style=\" text-align: left\"> " + DescripSect + "</td>";
                    let STOCK = "<td style=\" text-align: right;\"> " + Number(stock).toFixed(2) + "</td>";
                    $('#TableProductosRecetaStockSector').append(
                        "<tr id=" + idSector + "%" + DescripSect + ">" +
                        ID +
                        DESCRIPCION +
                        STOCK +
                        "<td style=\" text-align: center\">" +
                        "<input type=\"number\" style=\"withd 80%;     text-align: right;\"  onchange=\"ValidarInputSector()\" placeholder=\"Ingresa la cantidad\" /></td > " +
                        "</tr>"
                    );
                }
            }

        }



        function ValidarInputSector() {
            var resume_table = document.getElementById("TableProductosRecetaStockSector");
            let Cant = 0;
            document.getElementById("CantidadNecesariaParaContinuarSector").innerText = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 3) {

                        Cant += col.childNodes[0].value != "" ? Number(col.childNodes[0].value) : 0;

                    }
                }
            }
            let CantTotal = Number(document.getElementById("CantidadNecesariaParaContinuar4").textContent).toFixed(2);
            document.getElementById("RestoNecesariaParaContinuar4").innerText = Number(CantTotal - Cant).toFixed(2);
            document.getElementById("CantidadNecesariaParaContinuarSector").innerText = Cant.toFixed(2);

        }

        function handle(e) {
            let txtProd = document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value
            document.getElementById("ContentPlaceHolder1_HiddenCantidadAnterior").value = "";
            if (txtProd.includes(' - ')) {
                let array = txtProd.split("-")
                let type = array[2].trim()
                let id = array[0].trim()
                if (type == "Receta") {
                    CargarDllReceta(Number(id));
                    CargarTablaReceta(Number(id));
                }
            }
        }

        function RecargarLaTabla() {
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                let Cant = 0;
                let Costo = 0;
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);


                    if (j == 3) {
                        Costo = col.innerText;
                    }
                    if (j == 6) {
                        col2 = col.childNodes[0];
                        Cant = col2.value == "" ? 0.00 : col2.value;
                    }
                    if (j == 7) {
                        col.innerText = (Costo * Cant).toFixed(2);
                    }
                }
            }
            SumarCostoTotal();
        }
        function CargarDatosDeLaTabla() {
            if (document.getElementById("ContentPlaceHolder1_NCantidad").value.trim() == "") {
                toastr.error('No puedes utilizar esta opcion si Cantidad a Producir esta vacio')
                return false;
            }
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                let Cant = 0;
                let Costo = 0;
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);

                    if (j == 2) {
                        Cant = revertirNumero(col.innerText);

                    }
                    if (j == 3) {
                        Costo = revertirNumero(col.innerText);
                    }
                    if (j == 6) {
                        col2 = col.childNodes[0];
                        col2.value = Cant;
                        col2.style.color = '#676a6c';
                    }
                    if (j == 7) {
                        col.innerText = formatearNumero((Costo * Cant));
                    }
                }
            }
            SumarCostoTotal();
        }
        function CambiarCostoTotal(id, costo, input) {
            console.log(id, costo, input)
            CostoTotal = document.getElementById(id);
            CostoTotal.innerText = formatearNumero(Number(costo) * input.value);
            SumarCostoTotal();
        }

        function CargarTablaReceta(id) {
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/GetProductosEnRecetas",
                data: '{idProd: "' + id + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    AgregarATabla(respuesta.d);
                    document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent = "";
                    /*  PedirStockTotal(id);*/

                }
            });
        }
        function CargarDllProducto(id) {
            $.ajax({
                method: "POST",
                url: "GenerarVenta.aspx/GetProducto",
                data: '{idProd: "' + id + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    RellenarCamposProducto(respuesta.d.split("-"))
                }
            });
        }
        function CargarDllReceta(id) {
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/GetReceta",
                data: '{idProd: "' + id + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    document.getElementById("ContentPlaceHolder1_NCantidad").value = "";
                    document.getElementById("ContentPlaceHolder1_NSector").value = "";
                    document.getElementById("ContentPlaceHolder1_txtPresentacion").value = "";
                    document.getElementById("ContentPlaceHolder1_NLote").value = "";
                    document.getElementById("ContentPlaceHolder1_NMarca").value = "";
                    document.getElementById("ContentPlaceHolder1_NCantidadProducida").value = "";

                    RellenarCampos(respuesta.d.split(";"))
                }
            });
        }


        function ChangeTableCantidad() {
            let cant = document.getElementById("ContentPlaceHolder1_NCantidad").value;
            let rinde = document.getElementById("ContentPlaceHolder1_NRinde").value;
            let divisor = (Number(cant) / Number(rinde)).toFixed(2)
            let CantidadAnterior = document.getElementById("ContentPlaceHolder1_HiddenCantidadAnterior").value
            let GuardarCant = true;
            let arrCantGuardada;
            if (CantidadAnterior == "") {
                GuardarCant = true;
            } else {
                GuardarCant = false;
                arrCantGuardada = CantidadAnterior.split(";");
            }
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                //alert(cell[i].innerText);
                let cantidadNueva = 0;
                let costoNuevo = 0;
                let id;
                let nombre;
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);


                    if (j == 0) {
                        id = col.innerText;
                    }
                    if (j == 1) {
                        nombre = col.innerText;
                    }
                    if (j == 2) {
                        if (GuardarCant == true) {
                            document.getElementById("ContentPlaceHolder1_HiddenCantidadAnterior").value += col.innerText + ";"
                            col.innerText = (Number(revertirNumero(col.innerText)) * divisor).toFixed(2);
                            cantidadNueva = Number(col.innerText);
                        } else {
                            col.innerText = (Number(revertirNumero(arrCantGuardada[i - 1])) * divisor).toFixed(2);
                            cantidadNueva = Number(col.innerText);
                        }

                    }

                    if (j == 4) {
                        let idFinal = col.innerText + "-" + nombre.replaceAll(" ", "") + "-" + id;
                        if (cantidadNueva < Number(col.innerText)) {
                            document.getElementById(idFinal).style.color = "#676a6c"
                        } else {
                            document.getElementById(idFinal).style.color = "red"
                        }
                    }
                }
            }
        }
        function VaciarCampos() {
            toastr.success('Venta Completada');
            document.getElementById("btnGuardar").disabled = false;
            $('#tableProductos tbody')[0].innerHTML = ""
            document.getElementById('<%= idProductosRecetas.ClientID%>').value = "";
        }

        function RellenarCamposProducto(response) {
            document.getElementById("DivTablaRow").style.display = "none"
            document.getElementById('ContentPlaceHolder1_NRinde').value = 1;
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
        }

        function RellenarCampos(response) {
            document.getElementById('ContentPlaceHolder1_NRinde').value = response[2].replace(",", ".");
            document.getElementById('ContentPlaceHolder1_NUnidadMedida').value = response[3];
            document.getElementById('ContentPlaceHolder1_NUnidadMedida').disabled = true;
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
        }


        function ModalCambiarProducto(id) {
            $("#modalProductos").modal("show");
            document.getElementById("tbodyTablaCambiarProducto").innerHTML = "";
            document.getElementById("ContentPlaceHolder1_txtProductoElegido").value = "";
            document.getElementById("StockDisponibleChange").innerText = "";
            document.getElementById("ContentPlaceHolder1_CantidadNecesaria").value = "";
            document.getElementById("ContentPlaceHolder1_CantidadNecesaria").value


            /*   $('#modalAgregarNominacion').modal('show');*/
            let idProdRect = id.split("-")[0].trim();
            let tipo = '';
            let posicion = i;
            if (id.split("-")[1].trim() == "P") {
                tipo = "Producto";
            } else {
                tipo = "Receta";
            }
            var resume_table = document.getElementById("tableProductos");
            let stock = 0;
            let idfinal = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                tipoprod = row.id.split("%")[0]
                id = row.id.split("%")[1]
                if (idProdRect == id && tipo == tipoprod) {
                    idfinal = row.id;
                    posicion = i - 1;
                }


            }
            document.getElementById("ContentPlaceHolder1_IDFilaCambiar").textContent = idfinal;
            document.getElementById("ContentPlaceHolder1_IDPosicionCambiar").textContent = posicion;

            list = document.getElementById(idfinal)
            console.log(list)
            type = "";
            if (list.id.includes("Receta")) {
                type = "Receta"
            } else {
                type = "Producto"
            }
            list2 = list.childNodes;
            idProdTable = list2[0].innerHTML;
            descripcion = list2[1].innerHTML;
            cant = list2[2].innerHTML;
            document.getElementById("CantidadUtilizadaProdOriginal").innerText = cant;
            document.getElementById("DescricionProductoACambiar").innerText = idProdTable + " - " + descripcion + " - " + type;
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/ObtenerListaHistoricoCambio",
                data: '{idProd: "' + idProdTable
                    + '" , Type: "' + type
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                error: (error) => {
                    toastr.error('Error al generar Produccion')
                    let btn = document.getElementById("btnGuardar")
                    btn.disabled = false;
                },
                success: (respuesta) => {
                    if (respuesta.d != "") {
                        CargarTablaModalCambio(respuesta.d);
                    }
                },
            });
        }



        function CargarTablaModalCambio(list) {
            console.log(list);
            listArr = list.split("%");

            for (let i = 0; i < listArr.length; ++i) {
                if (listArr[i].length > 1) {
                    if (listArr != "") {

                        let item = listArr[i].split("_");
                        let id = item[0];
                        let Descripcion = item[1];
                        let Cantidad = item[2];

                        let Nombre = "<td> " + Descripcion + "</td>";
                        let ID = "<td style=\" text-align: right\"> " + id + "</td>";
                        let Cant = "<td style=\" text-align: right\"> " + Cantidad + "</td>";

                        $('#TablaCambiarProductoHistorico').append(
                            "<tr id=" + id + "%" + Descripcion.replaceAll(" ", "") + "%" + "historicoProd" + ">" +
                            ID +
                            Nombre +
                            Cant +
                            "<td style=\" text-align: right\">" +
                            "<input type=\"checkbox\" style=\"withd 100%;\" onclick=\"DeseleccionarYCargarDatos(this)\" placeholder=\"Seleccionar\" /></td > " +
                            "</tr>"
                        );


                    }
                }
            }


        }
        function DeseleccionarYCargarDatos(checkbox) {
            var resume_table = document.getElementById("TablaCambiarProductoHistorico");
            console.log(checkbox);
            let descripcionFinal = "";
            let CantFinal = "";
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                let descripcion = "";
                let cant = "";
                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 1) {
                        descripcion = col.innerText;
                    }

                    if (j == 2) {
                        cant = col.innerText;
                    }

                    if (j == 3) {
                        let boxcheck = col.childNodes[0];
                        if (boxcheck == checkbox) {
                            descripcionFinal = descripcion;
                            CantFinal = cant;
                        }
                        if (boxcheck !== checkbox && boxcheck.checked) {
                            // Desactivar el checkbox
                            boxcheck.checked = false;
                        }
                    }
                }
            }
            document.getElementById("ContentPlaceHolder1_txtProductoElegido").value = descripcionFinal;
            document.getElementById("ContentPlaceHolder1_CantidadNecesaria").value = Number(CantFinal);
            CargarStock();
        }

        function CargarStock(e) {
            id = document.getElementById("ContentPlaceHolder1_txtProductoElegido").value;
            newid = id.split("-")[0].trim();

            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/obtenerStockProd",
                data: '{id: "' + newid
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                error: (error) => {
                    toastr.error('Error al traer los stocks, elige otro producto')
                },
                success: (respuesta) => {
                    let btn = document.getElementById("ContentPlaceHolder1_ButtonCambiarProducto")
                    if (respuesta.d == "") {
                        document.getElementById("StockDisponibleChange").innerText = '0.00';

                        btn.removeAttribute("disabled");

                    } else {

                        btn.removeAttribute("disabled");
                        document.getElementById("StockDisponibleChange").innerText = " " + Number(respuesta.d).toFixed(2);
                    }
                },
            });
        }

        function CambiarProducto(e) {
            e.preventDefault();
            let cantNecesaria = document.getElementById("ContentPlaceHolder1_CantidadNecesaria").value;
            let id = document.getElementById("ContentPlaceHolder1_txtProductoElegido").value;
            let newid = id.split("-")[0].trim();
            let stock = document.getElementById("StockDisponibleChange").innerText;
            let idTabla = document.getElementById("ContentPlaceHolder1_IDFilaCambiar").textContent;
            let ProdOriginal = document.getElementById("DescricionProductoACambiar").innerText
            document.getElementById("ContentPlaceHolder1_HistoricoProduccion").innerText += ProdOriginal + " - " + id + " - " + cantNecesaria + "%";
            $("#modalProductos").modal("hide");
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/GetProductoChange",
                data: '{idProd: "' + newid
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                error: (error) => {
                    toastr.error('Error al traer los productos, elige otro')
                },
                success: (respuesta) => {
                    let btn = document.getElementById("ContentPlaceHolder1_ButtonCambiarProducto")
                    if (respuesta.d == "") {
                        toastr.error('Error inesperado al traer el producto y cambiar la tabla, intentelo de nuevo')
                    } else {
                        CambiarTabla(idTabla, respuesta.d, stock, cantNecesaria)
                    }
                },
            });
        }


        function CambiarTabla(idTabla, respuesta, stock, cantNecesarias) {
            let trTable = document.getElementById(idTabla);
            let id = respuesta.split('-')[0].trim()
            let posicion = document.getElementById("ContentPlaceHolder1_IDPosicionCambiar").innerText;
            posicion = Number(posicion + 1);
            let newId = 'Producto%' + id + "%" + Number(stock).toFixed(2);
            let cantNecesaria = Number(cantNecesarias).toFixed(2)
            document.getElementById("GuardarAnteriorHTML").innerHTML += "<tr id=" + trTable.id + ">" + trTable.innerHTML + "</tr>";
            let idviejo = trTable.id;
            trTable.id = newId;

            let idCostoFinal = "costofinal" + posicion;
            let unidad = respuesta.split('-')[2]
            let name = respuesta.split('-')[1]
            trTable.innerHTML = "";
            let costo = respuesta.split('-')[3];
            let faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-dollar\"></i> <a>"

            let Costo = "<td style=\" text-align: right\"> " + costo + "</td>";
            let CostoFinal = "<td style=\" text-align: right\" id=\"" + idCostoFinal.trim() + "\" > " + 0 + "</td>"
            let faRefresh = "<a data-toggle=tooltip data-placement=top data-original-title=\"Volver al Antiguo Producto\"  style=\"color: black;\"  onclick=ConfirmarVolverProductoOriginal('" + idviejo + "///" + newId + "')>  <i style=\"color: blue;\" class=\"fa fa-refresh\"></i> <a>"
            let faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-exchange\"></i> <a>"
            let faReceta = "<td><a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-P" + "')> <i id=\"" + id + "-P" + "\" class=\"fa fa-calculator\"></i> </a> " + faRefresh + faCosto + "  </td > ";
            let Nombre = "<td> " + name + "</td>";
            let ID = "<td style=\" text-align: right\"> " + id + "</td>";
            let STOCK = stock > cantNecesaria ? "<td id=" + stock + "-" + name.replaceAll(" ", "") + "-" + id + " style=\" text-align: right;\"> " + Number(stock).toFixed(2) + "</td>" : "<td id=" + Number(stock).toFixed(2) + "-" + name.replaceAll(" ", "") + "-" + id + " style=\" text-align: right; color: red;\"> " + stock + "</td>"
            let CantNecesaria = "<td style=\" text-align: right\"> " + cantNecesaria + "</td>";
            let Unidad = "<td style=\" text-align: left\">" + unidad + "</td>";
            trTable.innerHTML = ID +
                Nombre +
                CantNecesaria +
                Costo +
                STOCK +
                Unidad +
                "<td style=\" text-align: right\">" +
                "<input type=\"number\" style=\"withd 100%; text-align: right;\" onchange=\"CambiarCostoTotal('" + idCostoFinal + "','" + costo + "',this)\" placeholder=\"Ingresa la cantidad real que utilizaste\" /></td > " +
                CostoFinal +
                faReceta;

        }

        function ConfirmarVolverProductoOriginal(id) {
            document.getElementById("ContentPlaceHolder1_IDVolerProdOriginal").innerText = id;
            $("#modalVolverProductoOriginal").modal("show");

        }
        function VolverProductoOriginal(e) {
            e.preventDefault();
            $("#modalVolverProductoOriginal").modal("hide");
            id = document.getElementById("ContentPlaceHolder1_IDVolerProdOriginal").innerText;
            idAntiguo = id.split("///")[0];
            idActual = id.split("///")[1];
            trTablaActual = document.getElementById(idActual);
            arr = document.getElementById("GuardarAnteriorHTML").childNodes;
            for (item of arr) {
                if (item.id == idAntiguo) {
                    trTablaActual.innerHTML = item.innerHTML;
                    trTablaActual.id = idAntiguo;
                    item.remove();
                }
            }
            BorrarDeHiddenField(idAntiguo, idActual)

        }
        function BorrarDeHiddenField(antiguo, actual) {
            idAntiguo = antiguo.split("%")[1].trim();
            Antiguotype = antiguo.split("%")[0].trim();
            idactual = actual.split("%")[1].trim();
            actualtype = actual.split("%")[0].trim();
            list = document.getElementById("ContentPlaceHolder1_HistoricoProduccion").innerText;
            arrlist = list.split("%")
            newlist = "";
            for (item of arrlist) {
                if (item != "") {

                    arritem = item.split("-")
                    idAntiguoitem = arritem[0].trim();
                    DescripcionAntiguoitem = arritem[1].trim();
                    TypeAntiguoitem = arritem[2].trim();
                    idActualitem = arritem[3].trim();
                    idDescripcionitem = arritem[4].trim();
                    if (!idAntiguo == idAntiguoitem && Antiguotype == TypeAntiguoitem && idactual == idActualitem) {
                        newlist += item + "%";
                    }


                }
            }
            document.getElementById("ContentPlaceHolder1_HistoricoProduccion").innerText = newlist;

        }
        function verificarCambiosCosto() {
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                for (var j = 0, col; col = row.cells[j]; j++) {
                    if (j == 8) {
                        let list = col.childNodes
                        tdCosto = list[list.length - 2];
                        if (tdCosto.hasAttribute("class")) {
                            return true
                        }

                    }
                }
            }
            return false;
        }
        function GenerarProduccionVerificar(event) {
            event.preventDefault();
            if (verificarCambiosCosto()) {
                $("#modalVerificacion").modal("show")
            } else {
                GenerarProduccion(event);
            }

            event.preventDefault();
        }





        function GenerarProduccion(event, Actualizar = "none") {
            event.preventDefault();
            if (Actualizar != "none") {
                $("#modalVerificacion").modal("hide")
            }
            let btn = document.getElementById("btnGuardar")
            btn.disabled = true;
            let ListTablaFinal = document.getElementById("ContentPlaceHolder1_ListaTablaFinal");
            let ListaCostoCambiados = document.getElementById("ContentPlaceHolder1_ListaCostoCambiado");
            ListTablaFinal.value = "";
            ListaCostoCambiados.value = "";
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                let ListaAgregar = row.id.replaceAll(".", ",");
                let total = 0;
                let stock = 0;
                let id = 0;
                let costoU = 0;
                let type = row.id.split("%")[0];

                for (var j = 0, col; col = row.cells[j]; j++) {
                    if (j == 0) {
                        id = col.innerText
                    }
                    //alert(col[j].innerText);
                    if (j == 2) {
                        total = col.innerText;
                    }
                    if (j == 3) {
                        costoU = col.innerText;
                    }

                    if (j == 4) {
                        stock = col.innerText;
                    }

                    if (j == 6) {
                        if (col.childNodes[0].value == "" || col.childNodes[0].value <= 0) {
                            col2 = col.childNodes[0];
                            col2.style.color = "red";
                            ListaAgregar += "%" + col.childNodes[0].value.replace(".", ",");
                        }
                        else if (Number(stock) < Number(col.childNodes[0].value)) {

                            col2 = col.childNodes[0];
                            col2.style.color = "red";
                            ListaAgregar += "%" + col.childNodes[0].value.replace(".", ",");
                        } else {
                            col2 = col.childNodes[0]
                            col2.style.color = "#676a6c";
                            ListaAgregar += "%" + col.childNodes[0].value.replace(".", ",");
                        }
                    }
                    if (j == 8) {
                        let list = col.childNodes
                        tdCosto = list[list.length - 2];
                        if (tdCosto.hasAttribute("class")) {
                            ListaCostoCambiados.value += type + "%" + id + "%" + costoU + "/"
                        }

                    }
                }

                ListaAgregar += "%" + costoU;
                ListTablaFinal.value += ListaAgregar + ";"
            }
            Marca = document.getElementById("ContentPlaceHolder1_NMarca").value;
            Presentacion = document.getElementById("ContentPlaceHolder1_txtPresentacion").value;
            UnidadMedida = document.getElementById("ContentPlaceHolder1_NUnidadMedida").value;
            Sector = document.getElementById("ContentPlaceHolder1_NSector").value;
            Lote = document.getElementById("ContentPlaceHolder1_NLote").value;
            CantidadProducida = document.getElementById("ContentPlaceHolder1_NCantidadProducida").value;
            if (Actualizar != "Actualizar") {
                ListaCostoCambiados.value = ""
            }
            AjaxBajarStock();
        }



        function AjaxBajarStock() {
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/GenerarProduccionFinal",
                data: '{List: "' + document.getElementById("ContentPlaceHolder1_ListaTablaFinal").value
                    + '" , Marca: "' + document.getElementById("ContentPlaceHolder1_NMarca").value
                    + '" , Presentacion: "' + document.getElementById("ContentPlaceHolder1_txtPresentacion").value
                    + '" , UnidadMedida: "' + document.getElementById("ContentPlaceHolder1_NUnidadMedida").value
                    + '" , Sector: "' + document.getElementById("ContentPlaceHolder1_NSector").value
                    + '" , Lote: "' + document.getElementById("ContentPlaceHolder1_NLote").value
                    + '" , CantidadProducida: "' + document.getElementById("ContentPlaceHolder1_NCantidadProducida").value.replace(".", ",")
                    + '" , idReceta: "' + document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value.split("-")[0].trim()
                    + '" , ListStock: "' + document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent.replaceAll(".", ",")
                    + '" , ListHistoricoCambio: "' + document.getElementById("ContentPlaceHolder1_HistoricoProduccion").innerText
                    + '" , ListCostoCambios: "' + document.getElementById("ContentPlaceHolder1_ListaCostoCambiado").value.replaceAll(".", ",")
                    + '" , CostoTotal: "' + document.getElementById("CostoFinalTotal").innerText.split("$")[1].replaceAll(".", ",")
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                error: (error) => {
                    toastr.error('Error al generar Produccion')
                    let btn = document.getElementById("btnGuardar")
                    btn.disabled = false;
                },
                success: (respuesta) => {
                    arr = respuesta.d.split(",")
                    for (let i = 0; i < arr.length; ++i) {
                        if (arr[i] == "-1" || arr[i] == "-2" || arr[i] == "-3" || arr[i] == "-4") {
                            let btn = document.getElementById("btnGuardar")
                            btn.removeAttribute("disabled");
                            return toastr.error('Error de Produccion = ' + arr[i]);
                        }
                    }
                    window.location.href = "GenerarProduccion.aspx?m=1"
                },
            });
        }
 

        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function revertirNumero(numeroFormateado) {
            var numero = parseFloat(numeroFormateado.replace(/,/g, ''));
            return numero;
        }




    </script>

</asp:Content>
