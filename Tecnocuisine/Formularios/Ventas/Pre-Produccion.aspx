<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pre-Produccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Pre_Produccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />--%>

    <%-- ACA EMPIEZA EL CONTAINER --%>
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
                                            <div style="margin-left: 0px; margin-right: 0px;"
                                                class="row">
                                                <%--<div style="display: flex;">--%>
                                                <%--<div class="input-group m-b">--%>
                                                <div class="row">
                                                    <%-- Label de la ddl --%>
                                                    <div class="col-md-1" style="margin-left: 15px; margin-right: 15px;">
                                                        <label class="col-sm-2 control-label">Sector</label>
                                                    </div>
                                                    <%-- Ddl sectores --%>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlSector" runat="server"
                                                            CssClass="chosen-select form-control"
                                                            DataTextField="CountryName" DataValueField="CountryCode"
                                                            Data-placeholder="Seleccione Rubro..." Width="100%">
                                                            <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%-- Label desde --%>
                                                    <div class="col-md-1" style="margin-left: 15px; margin-right: 15px;">
                                                        <label class="col-md-4" style="margin-top: 5px;">Desde</label>
                                                    </div>
                                                    <%-- DatePicker desde --%>
                                                    <div class="col-md-2">
                                                        <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy"
                                                            data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;">
                                                        </asp:TextBox>
                                                    </div>
                                                    <%-- Label hasta --%>
                                                    <div class="col-md-1" style="margin-right: 15px;">
                                                        <label style="margin-top: 5px;" class="col-md-4">Hasta</label>
                                                    </div>
                                                    <%-- DatePicker hasta --%>
                                                    <div class="col-md-2">
                                                        <asp:TextBox class="form-control" runat="server" type="date" ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                    </div>
                                                    <%-- Boton Filtrar --%>
                                                    <div class="col-md-2">
                                                        <a id="btnFiltrar" onclick="FiltrarIngredientesDeOrdenesDeProduccion()" class="btn btn-primary pull-right" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1" style="margin-left: 20px; margin-top: 10px">
                                                        <label style="">Envio</label>
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 10px">
                                                        <asp:CheckBox ID="checkboxEnvio" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 10px">
                                                        <label>Recepcion</label>
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 10px">
                                                        <asp:CheckBox ID="checkboxRecepcion" runat="server" />
                                                    </div>
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
                        <asp:HiddenField ID="FechaDesde" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="FechaHasta" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="ddlSectorSelecionado" runat="server"></asp:HiddenField>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row" style="padding-left: 14px; padding-right: 14px">
        <!-- Utiliza col-lg-8 para la parte izquierda -->
        <div class="col-lg-7">
            <div class="ibox-content m-b-sm border-bottom">
                <div class="col-lg-12" style="background-color: white">


                    <a id="btnDesmarcarTodo" onclick="unCheckAll()" class="btn btn-primary pull-right"
                        style="margin-right: 15px;">Desmarcar todo</a>

                    <a id="btnMarcarTodo" onclick="CheckAll()" class="btn btn-primary pull-right"
                        style="margin-right: 15px;">Marcar todo</a>


                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-striped table-bordered table-hover " id="editable">
                                <thead>
                                    <tr>
                                        <th style="width: 17%">Fecha</th>
                                        <th style="width: 32%">Receta</th>
                                        <th style="text-align: right; width: 14%">Cantidad</th>
                                        <th style="width: 16%">Estado</th>
                                        <th style="width: 15%">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="phIngredientesFiltrados" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>


                    <asp:HiddenField runat="server" ID="detalleRecetas" />
                </div>
            </div>
        </div>

        <%-- Termina el primer wigget --%>

        <%-- Empieza el segundo widget --%>

        <div class="col-lg-5">
            <div class="ibox-content m-b-sm border-bottom">
                <div class="col-lg-12" style="background-color: white">

                    <%--<a id="btnAccion" onclick="" class="btn btn-primary pull-right" style="margin-right: 15px;">Accion</a>--%>

                    <%--     <a id="btnRecepcionSectores" href="#recepcionSectores" class="btn btn-primary pull-right" 
                style="margin-right: 15px;">Recepcion sectores</a>--%>



                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary pull-right"
                        Style="margin-right: 15px;"
                        OnClientClick="DetalleIngredientes_Click(); return false">Actualizar</asp:LinkButton>


                    <asp:LinkButton ID="btnRecepcionSectores" runat="server" class="btn btn-primary pull-right"
                        Style="margin-right: 15px;"
                        OnClientClick="abrirModalRecepcionSector(); return false">Recepcion sectores</asp:LinkButton>


                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-bordered table-hover" id="tableProductos" style="margin-top: 2%; max-width: 99%;">
                                <thead>
                                    <tr>
                                        <th style="width: 1%; text-align: right;">#</th>
                                        <th style="width: 5%">Insumo/Receta</th>
                                        <th style="width: 2%; text-align: right;">Cant. Necesaria</th>
                                        <th style="width: 2%; text-align: right;">Costo Unitario</th>
                                        <th style="width: 2%; text-align: right;">Stock</th>
                                        <th style="width: 2%; text-align: left;">Unidad de Med.</th>
                                        <th style="width: 5%; text-align: center;"
                                            hidden="hidden">
                                            <div class="row">
                                                Unidad Real

     
                                            </div>


                                        </th>
                                        <th style="width: 2%; text-align: right;"
                                            hidden="hidden">Costo total</th>

                                        <th style="width: 1%; text-align: left;"></th>
                                    </tr>
                                </thead>
                                <tbody id="tableProductosBody">
                                    <asp:PlaceHolder ID="phTablaProductos" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>




    <div id="modalIngredientes" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 40%; height: 50%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Recepción/Entrega</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel2">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Recepción</h5>
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
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Ingredientes</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaRecepcion">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Entrega</h5>
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
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Receta</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaEntrega">
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


    <div id="modalRecepcionEntrega" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 40%; height: 50%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Recepción/Entrega</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel3">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Recepción</h5>
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
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaRecepcionDerecha">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Entrega</h5>
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
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Receta</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaEntregaDerecha">
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



    <div id="modalEnvioRecetas" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 40%; height: 50%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Envio Recetas</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel4">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Envio</h5>
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
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaEnvioReceta">
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

    <%-- Aca empieza el modal RwcepcionPorSectores --%>


    <div id="recepcionSectores" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 55%; height: 65%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Recepción/Entrega</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel6">

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Recepción</h5>
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

                                        <div style="width: 35%; margin-left: 1rem">
                                            <div class="input-group m-b">
                                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                            </div>
                                        </div>

                                        <table class="table table-hover no-margins table-bordered" id="idTablaRecepcionSector">
                                            <thead>
                                                <tr>
                                                    <td style="hidden"><strong>#</strong></td>
                                                    <td><strong>Insumo/Receta</strong></td>
                                                    <td class="text-right"><strong>Cant. Necesaria</strong></td>
                                                    <td class="text-right"><strong>Costo Unitario</strong></td>
                                                    <td class="text-right"><strong>Unidad de Med.</strong></td>
                                                    <td class="text-right"><strong>Sector Productivo</strong></td>
                                                    <td class="text-right"><strong>Cantidad a prod</strong></td>
                                                    <td><strong>Acciones</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaRecepcionSector">
                                            </tbody>
                                        </table>

                                        <div>
                                            <asp:LinkButton ID="btnRecepcionar" runat="server" OnClientClick="recepcionar(); return false"
                                                class="btn btn-primary pull-right">recepcionar</asp:LinkButton>
                                        </div>
                                        <asp:HiddenField ID="cantInsumoSeleccionado" Value="" runat="server" />
                                        <asp:HiddenField ID="idIgredienteSeleccionado" Value="" runat="server" />
                                        <asp:HiddenField ID="ingredienteARecepcionar" Value="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="stockSector" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 55%; height: 65%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <div style="display: flex">
                        <h4 class="modal-title">Stock sector</h4>
                        <h4 id="cantNeceseria" class="modal-title" style="margin-left: 450px"></h4>
                    </div>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel7">

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered" id="tableSectores">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%">#</th>
                                                    <th>Sector</th>
                                                    <th>Unidad</th>
                                                    <th style="text-align: end">Stock</th>
                                                    <th style="text-align: right">Transferencia</th>
                                                    <th style="text-align: right; display: none;">idReceta</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbobyStockSectores">
                                            </tbody>
                                        </table>
                                        <div style="margin-right: 15px; margin-top: 10px; display: flex; float: right">
                                            <p id="validarTransferencia" style="margin-right: 10px; margin-top: 10px;"
                                                class="text-danger text-hide">
                                                *La transferencia debe ser igual a la cantidad ingresada
                                            </p>

                                            <asp:LinkButton ID="btnAceptarTransferencia"
                                                OnClientClick="SumarTranferencia(); return false"
                                                runat="server" class="btn btn-primary">Aceptar</asp:LinkButton>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="idVerStockButton" runat="server" />
                                    <asp:HiddenField ID="stockSectoresProductos" runat="server" />
                                    <asp:HiddenField ID="stockSectoresProductosEditar" runat="server" />
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
           function recepcionar(){
              let stockSectores = document.getElementById('<%= stockSectoresProductos.ClientID %>').value
              transferirStockAjax(stockSectores)
           }
    </script>

    <script>
           function SumarTranferencia() {

                    let transferenciaTotal = 0;
                    for (var i = 1; i < document.getElementById('tableSectores').rows.length; i++) {


                        let celdaTransferencia = document.getElementById('tableSectores').rows[i].cells[4].querySelector('input');
                        let valorCelda = parseFloat(celdaTransferencia.value) || 0;
                        transferenciaTotal += valorCelda;
                    }
                        
                    let idBtnVerStock = document.getElementById('<%= idVerStockButton.ClientID %>').value
                    let btnVerStockSector = document.getElementById(idBtnVerStock);
                    let estiloComputado = window.getComputedStyle(btnVerStockSector);
                    let colorBoton = estiloComputado.getPropertyValue('color')
                    let cantSelecionada = document.getElementById("<%= cantInsumoSeleccionado.ClientID %>").value;
                    document.getElementById('<%= stockSectoresProductosEditar.ClientID %>').value = ""
                    
                        if(cantSelecionada == transferenciaTotal){

                            document.getElementById('validarTransferencia').className = 'text-danger text-hide'

                            let stockSectores = "";
                            let nuevaCadena = "";
                            for (var i = 1; i < document.getElementById('tableSectores').rows.length; i++) {


                                let celdaId = document.getElementById('tableSectores').rows[i].cells[0].innerText
                                let celdaSector = document.getElementById('tableSectores').rows[i].cells[1].innerText
                                let celdaUnidad = document.getElementById('tableSectores').rows[i].cells[2].innerText
                                let celdaStock = document.getElementById('tableSectores').rows[i].cells[3].innerText
                                let celdaTransferencia = document.getElementById('tableSectores').rows[i].cells[4].querySelector('input').value;
                                let celdaidIngrediente = document.getElementById('tableSectores').rows[i].cells[5].innerText
                                let stockSectoresEditar = "";
                                
                                if(celdaTransferencia == ""){
                                   celdaTransferencia = "0" 
                                }

                    
                                //Aca se define si esta editando o agregando por primera vez
                                if(colorBoton != "rgb(0, 128, 0)"){
                                    document.getElementById('<%= stockSectoresProductos.ClientID %>').value += celdaId + "," + celdaSector + "," + celdaUnidad + "," + celdaStock +"," + celdaTransferencia + "," + celdaidIngrediente + ";"
                                }
                                else{
                                      stockSectoresEditar = document.getElementById('<%= stockSectoresProductosEditar.ClientID %>').value += celdaId + "," + celdaSector + "," + celdaUnidad + "," + celdaStock + "," + celdaTransferencia + "," + celdaidIngrediente + ";";
                                }
                            }


                                //Aca es donde empieza el editar
                                if(colorBoton == "rgb(0, 128, 0)"){
                                    let stockSectoresGlobal = document.getElementById('<%= stockSectoresProductos.ClientID %>').value;
                                    let stockSectoresGlobalSplit = stockSectoresGlobal.split(";").filter(Boolean);;
                                         stockSectoresGlobalSplit.forEach(stock => {
                                             let stockSectoresGlobalSplitComa = stock.split(",")
                                             let id = stockSectoresGlobalSplitComa[0]
                                             let stockSectoresEditar = document.getElementById('<%= stockSectoresProductosEditar.ClientID %>').value;
                                             let existe = false;
                                             let stockSectoresEditarSplit = stockSectoresEditar.split(";").filter(Boolean);;
                                             stockSectoresEditarSplit.forEach(stockEditar => {
                                                let stockSectoresEditarSplitComa = stockEditar.split(",")

                                                let idEditar = stockSectoresEditarSplitComa[0];
                                                let sectorEditar = stockSectoresEditarSplitComa[1];
                                                let unidadEditar = stockSectoresEditarSplitComa[2];
                                                let stockEditar2 = stockSectoresEditarSplitComa[3];
                                                let transferenciaEditar = stockSectoresEditarSplitComa[4];
                                                let idIngredienteEditar = stockSectoresEditarSplitComa[5];

                                                if(id == idEditar){
                                                   nuevaCadena += idEditar + "," + sectorEditar + "," + unidadEditar + "," + stockEditar2 + "," + transferenciaEditar + "," + idIngredienteEditar + ";";
                                                   existe = true
                                                }
                                
                                             })

                                             if(existe == false){
                                                nuevaCadena += celdaId + "," + celdaSector + "," + celdaUnidad + "," + celdaStock + "," + celdaTransferencia + "," + celdaidIngrediente + ";";
                                             }
                                          
                                         })
                                         document.getElementById('<%= stockSectoresProductos.ClientID %>').value = nuevaCadena;

                                }
                        document.getElementById(idBtnVerStock).style.color = "green";
                        $('#stockSector').modal('hide');

                      }
                      else{
                          document.getElementById('validarTransferencia').className = 'text-danger';
                      }
              
            }
    </script>

    <script>
           function abrirModalRecepcionSector(){
                 
                 cargarRecepcionAgrupadaPorSector()
                 $('#recepcionSectores').modal('show');   
           }

           function obteneridSectorProductivoUrl(){
                let url = new URL(window.location.href);
                let sectorProductivo = url.searchParams.get('SP');
                return sectorProductivo;
           }

           function transferirStockAjax(stockSectores){
                  
                   let idSectorProductivo = obteneridSectorProductivoUrl();
                   let ingredientesARecepcionar = obtenerIngredientesARecepcionar()


                   $.ajax({
                   method: "POST",
                   url: "Pre-Produccion.aspx/updateStockSector",
                   data: JSON.stringify({ stockSectores: stockSectores, idSectorProductivo: idSectorProductivo, ingredientesARecepcionar: ingredientesARecepcionar }),
                   contentType: "application/json",
                   dataType: "json",
                   async: false,
                   error: (error) => {
                       console.log(JSON.stringify(error));
                   },
                   success: (data) => {
                      document.getElementById('<%= stockSectoresProductos.ClientID %>').value = "";
                      document.getElementById('<%= ingredienteARecepcionar.ClientID %>').value = "";
                      $('#recepcionSectores').modal('hide');
                      toastr.success("guardado con exito!", "Exito")
                   }
               });
           
           }


           function obtenerIngredientesARecepcionar(){

                  let ingredientesARecepcionar = "";
                  for (var i = 1; i < document.getElementById('idTablaRecepcionSector').rows.length; i++) {

                     let id = document.getElementById('idTablaRecepcionSector').rows[i].cells[0].innerText
                     let insumo = document.getElementById('idTablaRecepcionSector').rows[i].cells[1].innerText
                     let cantNesesaria = document.getElementById('idTablaRecepcionSector').rows[i].cells[2].innerText
                     let costoUnitario = document.getElementById('idTablaRecepcionSector').rows[i].cells[3].innerText
                     let unidadMedida = document.getElementById('idTablaRecepcionSector').rows[i].cells[4].innerText
                     let sectorProductivo = document.getElementById('idTablaRecepcionSector').rows[i].cells[5].innerText
                     let cantAPro = document.getElementById('idTablaRecepcionSector').rows[i].cells[6].querySelector('input').value;
                     //let segundoBotonValue = document.getElementById('idTablaRecepcionSector').rows[i].cells[7].getElementsByTagName('input')[1];
                     let segundoBoton = document.getElementById('idTablaRecepcionSector').rows[i].cells[7].querySelector('button');

                     let estiloComputado = window.getComputedStyle(segundoBoton);
                     let colorBoton = estiloComputado.getPropertyValue('color')

                     if(colorBoton == "rgb(0, 128, 0)"){
                        ingredientesARecepcionar = document.getElementById('<%= ingredienteARecepcionar.ClientID %>').value += id + "," + cantAPro + "," + sectorProductivo + ";"
                     }
                     else{
                     }

                }

                return ingredientesARecepcionar;
           
           }


           function precargarTransferencia(idIngrediente){
                let sectoresTranferencia = document.getElementById('<%= stockSectoresProductos.ClientID %>').value
                      let sectoresTranf = sectoresTranferencia.split(';').filter(Boolean); 
                      let nuevaCadena = "";
                       sectoresTranf.forEach(stock => {
                       const stockSectores = stock.split(',');

                       let id = stockSectores[0];
                       let Sector = stockSectores[1];
                       let unidad = stockSectores[2];
                       let stockSector = stockSectores[3];
                       let cantTransferir = stockSectores[4];
                       let ingredienteId = stockSectores[5];


                       if(ingredienteId == idIngrediente){
                          nuevaCadena += id + "," + Sector + "," + unidad + "," + stockSector + "," + cantTransferir + "," + ingredienteId + ";"
                       }

                      });

                      if(nuevaCadena != ""){
                          let sectoresFiltrados = nuevaCadena.split(';').filter(Boolean); 
                          document.getElementById('tbobyStockSectores').innerHTML = "";
                          sectoresFiltrados.forEach(stck => {
                          const stockSectoresFiltrados = stck.split(',');

                          let id = stockSectoresFiltrados[0];
                          let Sector = stockSectoresFiltrados[1];
                          let unidad = stockSectoresFiltrados[2];
                          let stock = stockSectoresFiltrados[3];
                          let cantTransferir = stockSectoresFiltrados[4];
                          let ingredienteId = stockSectoresFiltrados[5];


                          let transferencia = "";
                          if(cantTransferir == "0"){
                            cantTransferir = "";
                          }
                          transferencia = `<input style="width: 100%; text-align: right;" placeholder="Ingrese el stock" value="${cantTransferir}" />`;

                              let plantillaStock = `
                               <tr>
                                   <td>${id}</td>
                                   <td>${Sector}</td>
                                   <td style="text-align: left;">${unidad}</td>
                                   <td style="text-align: right;">${stock}</td>
                                   <td style="text-align: right;">${transferencia}</td>
                                   <td style="text-align: right; display: none">${ingredienteId}</td>
                               </tr>
                           `;
                           document.getElementById('tbobyStockSectores').innerHTML += plantillaStock;
                         
  
                         });

                         return 1
                              
                      }
                      else{
                            return 0
                      }
           }

           function verStockSector(idIngrediente, idCantRecepcionar, idBotonVerStock){

               //Esta funcion se ejecuta el boton al que se le hace click ya esta en verde
               let rta = precargarTransferencia(idIngrediente);

               if(rta != 1){
                    let cantidadARecepcionar = document.getElementById(idCantRecepcionar).value;
                    document.getElementById("<%= cantInsumoSeleccionado.ClientID %>").value = cantidadARecepcionar;
                    document.getElementById("cantNeceseria").textContent = "Cantidad necesaria: " + cantidadARecepcionar;
                    document.getElementById('<%= idVerStockButton.ClientID %>').value = idBotonVerStock

                    let url = new URL(window.location.href);
                    let idSectorProductivo = url.searchParams.get('SP');
       

                          $.ajax({
                             method: "POST",
                             url: "Pre-Produccion.aspx/getStockSector",
                             data: JSON.stringify({ idIngrediente: idIngrediente, idSectorProductivo: idSectorProductivo}),
                             contentType: "application/json",
                             dataType: "json",
                             async: false,
                             error: (error) => {
                                 console.log(JSON.stringify(error));
                             },
                             success: (data) => {
                                  document.getElementById('tbobyStockSectores').innerHTML = "";
                                  const stockSectores = data.d.split(';').filter(Boolean); 
                                     stockSectores.forEach(stock => {
                                            const stockSectores = stock.split(',');

                                            const sector = stockSectores[0];
                                            const unidad = stockSectores[1];
                                            const stockSector = stockSectores[2];
                                            const id = stockSectores[3];
                                            let transferencia = "";
                                            transferencia = "<input style=\"width: 100%; text-align: right;\" placeholder=\"Ingrese el stock\" value=\"\" />";
                                                let plantillaStock = `
                                                 <tr>
                                                     <td>${id}</td>
                                                     <td>${sector}</td>
                                                     <td style="text-align: left;">${unidad}</td>
                                                     <td style="text-align: right;">${stockSector}</td>
                                                     <td style="text-align: right;">${transferencia}</td>
                                                     <td style="text-align: right; display: none;">${idIngrediente}</td>
                                                 </tr>
                                             `;
                                             document.getElementById('tbobyStockSectores').innerHTML += plantillaStock;
                                    });
                             }
                         });
                      $('#stockSector').modal('show');   
               }
               else{
                    $('#stockSector').modal('show'); 
               }
           }

           function cargarRecepcionAgrupadaPorSector(){
                
                let hiddenFieldValue = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                let hiddenFieldValueArray = hiddenFieldValue.split(";").filter(Boolean);
                let idsRecetas = "";
                document.getElementById('<%= stockSectoresProductosEditar.ClientID %>').value = ""; 
                document.getElementById('<%= stockSectoresProductos.ClientID %>').value = ""; 
                

                hiddenFieldValueArray.forEach(producto => {
                     const ingredienteData = producto.split(',');
                      let idReceta = ingredienteData[1];
                      idsRecetas += idReceta + ",";
                  });

                      $.ajax({
                      method: "POST",
                      url: "Pre-Produccion.aspx/getRecepcionEntregaAgrupadoPorSector",
                      data: '{idsRecetas: "' + idsRecetas + '"}',
                      contentType: "application/json",
                      dataType: "json",
                      dataType: "json",
                      async: false,
                      error: (error) => {
                          console.log(JSON.stringify(error));
                      },
                      success: (data) => {
                           const ingredientesArray = data.d.split(';').filter(Boolean); 
                           document.getElementById('tablaRecepcionSector').innerHTML = "";
                           let cont = 0
                           ingredientesArray.forEach(producto => {
                               const ingredienteData = producto.split(',');


                                   const ingredienteId = ingredienteData[0];
                                   const ingredienteNombre = ingredienteData[1];
                                   const Cantidad = ingredienteData[2];
                                   const unidadMedida = ingredienteData[3];
                                   const costo = ingredienteData[4];
                                   const SectorProductivo = ingredienteData[5];
                                   const idSector = ingredienteData[6];
                                   let cantidadaProducir = "";
                                   let faRecepcion = "";
                                   let faCheckbox = "";

                                   
                                    
                                   cantidadProducir = "<input  id=\"" + "cantAproducir_" + cont + "\" style=\"width: 100%; text-align: right;\" placeholder=\"Ingresa la cantidad real que utilizaste\" value=\"" + Cantidad + "\" />";


                                   var faCombined = "<div>" +
                                                      '<input id="RecepcionCheckbox" type="checkbox" class="icon-button" style="margin-left: 5px; color: black; border: none;" title="Recepción" onchange="cargarInsumo()">' +
                                                        "<button id=\"" + "verStock_" + cont + "\" type=\"button\" class=\"icon-button\" style=\"margin-left: 5px; color: black; border: none;\" title=\"Ver stock\" onclick=\"verStockSector('" + ingredienteId + "', '" + "cantAproducir_" + cont + "', '" + "verStock_" + cont + "')\"><i class=\"fa fa-clipboard\"></i></button>" +
                                                    "</div>";
                                       cont++;
                                       let plantillaIngredientes = `
                                        <tr>
                                            <td>${ingredienteId}</td>
                                            <td>${ingredienteNombre}</td>
                                            <td style="text-align: right;">${Cantidad}</td>
                                            <td style="text-align: right;">${costo}</td>
                                            <td style="text-align: right;">${unidadMedida}</td>
                                            <td style="text-align: right;">${SectorProductivo}</td>
                                            <td style="text-align: right;">${cantidadProducir}</td>
                                            <td>${faCombined}</td>
                                        </tr>
                                    `;
                                    document.getElementById('tablaRecepcionSector').innerHTML += plantillaIngredientes;


                           });

                          //AgregarATabla(respuesta.d);
                          //document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent = "";
                          /*  PedirStockTotal(id);*/
             
                      }
                  });
           }

    </script>

    <script>
       function CheckAll() {
           var total = 0;
           for (var i = 1; i < document.getElementById('editable').rows.length; i++) {
              // document.getElementById('editable').rows[i].cells[4].childNodes[1].children[0].checked = true;
             let checkbox = document.getElementById('editable').rows[i].cells[6].getElementsByTagName('input')[0];


             if(checkbox != null){
                checkbox.checked = true;



                let id = document.getElementById('editable').rows[i].cells[5].innerText;
                let sectorProductivo = document.getElementById('editable').rows[i].cells[4].innerText;
                let cantidad = document.getElementById('editable').rows[i].cells[2].innerText;
                let descripcion = document.getElementById('editable').rows[i].cells[1].innerText;


                     verIngredientesReceta(checkbox.id, id, cantidad, descripcion, sectorProductivo);
             }

           }
       }


       function cargarInsumo(){
       }
    </script>
    <script>
            function Recepcionar(ingredienteId, ingredienteNombre, idSector, SectorProductivo){
                window.open("../Compras/Entregas.aspx?idP=" + ingredienteId + "&Desc=" + ingredienteNombre + "&idS=" + idSector + "&SP=" + SectorProductivo, '_blank');
            }
    </script>

    <script>
             function unCheckAll() {
                 var total = 0;
                 for (var i = 1; i < document.getElementById('editable').rows.length; i++) {

                       let checkbox = document.getElementById('editable').rows[i].cells[6].getElementsByTagName('input')[0];
                     if(checkbox != null){
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

        $(document).ready(function () {

            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value
            let ddlSectorProductivo = document.getElementById("ContentPlaceHolder1_ddlSector").value
            ddlSectorProductivo.value;

            if (fechad != "" && fechah != "" && ddlSectorProductivo.value != "") {

                establecerFechasSeleccionadas();
                establecerSectorSeleccionado();
            } else {

                establecerDiaHoy();
            }

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

        function establecerSectorSeleccionado(){
            let ddlSectorProductivo = document.getElementById("ContentPlaceHolder1_ddlSectorSelecionado").value
            document.getElementById("ContentPlaceHolder1_ddlSector").value = ddlSectorProductivo;
            
        }


        function verStock(idReceta, cantidad){
        
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

        function establecerDiaHoy() {
            var fechas = obtenerRangoFechas();
            // Convertir la fecha en un formato legible xpara el DatePicker   
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.primerDia.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.ultimoDia.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

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


        function DetalleIngredientes_Click(){
        
            let hiddenFieldValue = document.getElementById("<%= detalleRecetas.ClientID %>").value;
            let elementos = hiddenFieldValue.split(";");
            let idsRecetas = "";
            elementos.forEach(producto => {
                const productoData = producto.split(',');
                if (productoData[0].trim() !== "") {

                    let idCheckbox = productoData[0];
                    let idReceta = productoData[1];
                    idsRecetas += idReceta + ",";

                }
             });
             CargarTablaReceta(idsRecetas)

        }


        function cargarIngredienteEnvio(id, descripcion, cantidad, idCheckBox){

                 //Esta funcion valida si el producto ya existe en la tabla, de ser asi acumula la cantidad de ese producto

                 let checkBox = document.getElementById(idCheckBox);
                 console.log("check: " + checkBox)


                 if(checkBox.checked == true){
                    let existe = validarProductoExisteTabla(id, cantidad);
                    if(existe != true){
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
                 else{
                    quitarProductoTabla(id, cantidad)
                 }
            
             
        }


        function quitarProductoTabla(id, cantidad){
            let existe = false;
            for (var i = 1; i < document.getElementById('tableProductos').rows.length; i++) {
                let idTabla = document.getElementById('tableProductos').rows[i].cells[0].innerHTML

                if(id == idTabla){
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
                  if(cantDecimales == 0){
                     resta = resta.toFixed(cantDecimalesTotales);
                  }

                  console.log("cantidadTableSuma: " + resta)
                  document.getElementById('tableProductos').rows[i].cells[2].innerHTML = resta

                  if(resta == 0){
                    document.getElementById('tableProductos').deleteRow(i);
                  }

                }

                console.log(idTabla)
            }

           return existe;
        
        }

        function validarProductoExisteTabla(id, cantidad){
                
              let existe = false;
              for (var i = 1; i < document.getElementById('tableProductos').rows.length; i++) {
                  let idTabla = document.getElementById('tableProductos').rows[i].cells[0].innerHTML

                  if(id == idTabla){
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
                    if(cantDecimales == 0){
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
            var fechaActual = new Date(); // Obtiene la fecha actual
            var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1); // Primer día del mes actual
            var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0); // Último día del mes actual

            return {
                primerDia: primerDiaMes,
                ultimoDia: ultimoDiaMes
            };
        }


        function FiltrarIngredientesDeOrdenesDeProduccion() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");
            /*let SectorProducivo = document.getElementById("ContentPlaceHolder1_ddlSector");*/

            // Obtener el DropDownList
            let ddlSector = document.getElementById("ContentPlaceHolder1_ddlSector");

            // Obtener el texto de la opción seleccionada
            let idSectorProductivo = ddlSector.value;


            let checkBoxEnvio = document.getElementById("<%= checkboxEnvio.ClientID %>")
            let checkBoxRecepcion = document.getElementById("<%= checkboxRecepcion.ClientID %>")


            if(checkBoxEnvio.checked == true){
                window.location.href = "Pre-Produccion.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH + "&SP=" + idSectorProductivo + "&E=1";
            }


            if(checkBoxRecepcion.checked == true){
                window.location.href = "Pre-Produccion.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH + "&SP=" + idSectorProductivo + "&RP=1";
            }

        }

        function verIngredientesReceta(idCheckbox, idReceta, cantidad, descripcion, sectorProductivo)
        {

            var chk1 = document.getElementById(idCheckbox);
            cantidad = cantidad.replace(/,/g, '.');

              if (chk1.checked) {
                  
                  if(document.getElementById("<%= detalleRecetas.ClientID %>").value == ""){
                    document.getElementById("<%= detalleRecetas.ClientID %>").value = idCheckbox + "," + idReceta + "," + cantidad + "," + descripcion + "," + sectorProductivo + ";" 
                  DetalleIngredientes_Click();         
                }
                  else{
                    document.getElementById("<%= detalleRecetas.ClientID %>").value += idCheckbox + "," + idReceta + "," + cantidad + "," + descripcion + "," + sectorProductivo + ";" 
                  DetalleIngredientes_Click();         
                    
                  }
                  //CargarTablaReceta(idReceta);
                  
              }

              else{
                  let productosSeleccionados = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                  productosSeleccionados = productosSeleccionados.split(';');
                  let nuevosProductos = ""
                  for (var x = 0; x < productosSeleccionados.length; x++) {
                     if (productosSeleccionados[x] != "") {
                         if (!productosSeleccionados[x].includes(idReceta)) {
                             //guarda los productos actuales que hay en la tabla de la receta separados por ;, de esta forma quita de la cadena de productos 
                             //aquellos que fueron eliminados
                             nuevosProductos += productosSeleccionados[x] + ";";
                         }
                         else {
                             /* var productoAEliminar = productos[x].split(',')[2];*/
                             //ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) - parseFloat(productoAEliminar);
                             //if (ContentPlaceHolder1_txtRinde.value != "") {
                             //    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2);
                             //    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                             //}
                         }
                    }
                 }

                 document.getElementById("<%= detalleRecetas.ClientID %>").value = nuevosProductos;
                 DetalleIngredientes_Click();
                }
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

         function MostrarRecepcion(idReceta, cantidad){
              //$('#modalRecepcionEntrega').modal('show');


               fetch('Pre-Produccion.aspx/getIngredientesRecetaByIdRecetaSectorProductivo', {
                 method: 'POST',
                 body: JSON.stringify({ idReceta: idReceta, cantidad: cantidad }),
                 headers: { 'Content-Type': 'application/json' }
                 })
                 .then(response => response.json())
                 .then(data => {
          
          
                 //const ingredientesArray = data.d.split(';');
                 const ingredientesArray = data.d.split(';').filter(Boolean); 
                  document.getElementById('tablaRecepcionDerecha').innerHTML = "";
                  ingredientesArray.forEach(producto => {
                       // Dividir cada elemento en valores individuales usando coma como separador
                       const ingredienteData = producto.split(',');

                       // Ahora puedes acceder a valores específicos del producto
                       const ingredienteId = ingredienteData[0];
                       const ingredienteNombre = ingredienteData[1];
                       const Cantidad = ingredienteData[2];
                       const sectorProductivo = ingredienteData[3];


                        let plantillaIngredientes = `
                        <tr>
                            <td>${sectorProductivo}</td>
                            <td>${ingredienteNombre}</td>
                            <td style="text-align: right;">${Cantidad}</td>
                        </tr>
                    `;
                    document.getElementById('tablaRecepcionDerecha').innerHTML += plantillaIngredientes;

                  });
                  //Aca termina el foreach

                         let recetasSeleccionadas = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                         let hiddenFieldValue = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                         let elementos = hiddenFieldValue.split(";");
                         let idsRecetas = "";
                         elementos.forEach(producto => {
                             const productoData = producto.split(',');
                             if (productoData[0].trim() !== "") {

                                 let idReceta = productoData[1];
                                 idsRecetas += idReceta + ",";

                             }
                          //   idsRecetas += idReceta
                          });
                  //Aca empieza el segundo fetch
                        fetch('Pre-Produccion.aspx/getRecetasEntrega', {
                          method: 'POST',
                          body: JSON.stringify({ idsRecetas: idsRecetas, recetasSeleccionadas: recetasSeleccionadas, idReceta: idReceta }),
                          headers: { 'Content-Type': 'application/json' }
                          })
                          .then(response => response.json())
                          .then(data => {

                                const recetasArray = data.d.split(';').filter(Boolean); 
                                document.getElementById('tablaEntregaDerecha').innerHTML = "";
                                recetasArray.forEach(receta => {
                                    const recetaData = receta.split(',');

                                    const idReceta = recetaData[0];
                                    const cantidadReceta = recetaData[2];
                                    const descripcionReceta = recetaData[3];
                                    const sectorReceta = recetaData[4];


                                         let plantillaRecetas = `
                                         <tr>
                                             <td>${sectorReceta}</td>
                                             <td>${descripcionReceta}</td>
                                             <td style="text-align: right;">${cantidadReceta}</td>
                                         </tr>
                                     `;


                                    document.getElementById('tablaEntregaDerecha').innerHTML += plantillaRecetas;

                                });

                           })
                            .catch(error => {
                             // Código para manejar errores aquí
                             console.error('Error:', error);
                            });
                             $('#modalRecepcionEntrega').modal('show');
                        
          
                             })
                       .catch(error => {
                           // Código para manejar errores aquí
                           console.error('Error:', error);
                       });


         }


    </script>


</asp:Content>
