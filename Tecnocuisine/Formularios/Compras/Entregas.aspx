<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Entregas.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.Entregas" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">

                    <div class="ibox-content" style="margin-top: 10px">
                        <div class="row" style="padding-left: 30px; padding-right: 30px; padding-bottom: 10rem">

                            <%--Encabezado--%>
                            <div style="display: flex; align-self: center; justify-content: space-between;">
                                <h1 class="m-t-none m-b">Ingreso de Compra</h1>

                                <div class="form-group" style="display: flex; align-items: center; gap: 1rem">
                                    <label>Entrega Numero:</label>
                                    <div id="lblProdNum" runat="server"></div>
                                </div>
                            </div>


                            <%--Fila 1--%>
                            <div role="form" style="display: flex; column-gap: 1rem; justify-content: space-between; flex-wrap: nowrap">

                                <%--Proveedor--%>
                                <div class="">
                                    <label>Proveedor</label>
                                    <asp:TextBox runat="server" ID="txtProveedor" list="ContentPlaceHolder1_ListaNombreProveedores" class="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ErrorMessage="<p>Seleccione un Proveedor</p>" SetFocusOnError="false" ForeColor="Red" Font-Bold="false" ValidationGroup="AgregarEntregas" ControlToValidate="txtProveedor"></asp:RequiredFieldValidator>
                                    <datalist id="ListaNombreProveedores" runat="server">
                                    </datalist>
                                </div>

                                <%--Tipo Documento--%>
                                <div class="" style="width: 10%">
                                    <label>Documento</label>
                                    <asp:DropDownList ID="ddlDocumentos" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="FC" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="PR" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <%--Nro Factura--%>
                                <div class="">
                                    <label>Nro Factura</label>
                                    <asp:TextBox runat="server" ID="txtNroFactura" class="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<p>Ingrese Nro Factura</p>" SetFocusOnError="false" ForeColor="Red" Font-Bold="false" ValidationGroup="AgregarEntregas" ControlToValidate="txtNroFactura"></asp:RequiredFieldValidator>
                                    <datalist id="Datalist1" runat="server">
                                    </datalist>
                                </div>

                                <%--Sector--%>
                                <%--<div class="">
                                    <label>Sector</label>
                                    <asp:TextBox runat="server" ID="txtSector" list="ContentPlaceHolder1_ListaNombreSectores" class="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<p>Seleccione un Sector</p>" SetFocusOnError="false" ForeColor="Red" Font-Bold="false" ValidationGroup="AgregarEntregas" ControlToValidate="txtSector"></asp:RequiredFieldValidator>

                                    <datalist id="ListaNombreSectores" runat="server">
                                    </datalist>
                                </div>--%>


                                <%--Fecha entrega--%>
                                <div class="" id="data_1" style="width: auto">
                                    <label>Fecha de Factura</label>
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaEntrega"></asp:TextBox>
                                        <%--<input type="text">--%>
                                    </div>
                                </div>

                                <%--Observaciones--%>
                                <div class="" style="width: 30%">
                                    <label>Observaciones</label>
                                    <asp:TextBox ID="txtObservaciones" class="form-control" runat="server" />
                                </div>

                            </div>


                            <hr style="margin-top: 0" />


                            <%--Fila 2--%>
                            <div>
                                <div class="row" style="display: flex; flex-wrap: nowrap; justify-content: space-between; gap: 1rem">

                                    <%--Producto--%>
                                    <div class="" style="width: 25%; height: auto">
                                        <label>Producto </label>
                                        <div class="input-group" style="text-align: right;">
                                            <datalist id="ListaNombreProd" runat="server">
                                            </datalist>

                                            <asp:TextBox ID="txtDescripcionProductos" onChange="ValidarProducto()" onfocusout="handle(event)" list="ContentPlaceHolder1_ListaNombreProd" class="form-control" runat="server" />
                                            <asp:HiddenField ID="Hiddentipo" runat="server" />
                                            <asp:HiddenField ID="HiddenUnidad" runat="server" />
                                            <asp:HiddenField ID="HiddenCosto" runat="server" />

                                            <span class="input-group-btn">
                                                <%--<asp:LinkButton runat="server" ID="btnProductos" class="btn btn-primary dim" data-toggle="modal" data-backdrop="static" data-target="#modalTabsProductos"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>--%>
                                                <a class="btn btn-primary dim" data-toggle="modal" href="#modalTabsProductos">
                                                    <i style="color: white" class="fa fa-plus" data-toggle="tooltip" data-placement="top" title data-original-title="Selecionar Ingrediente"></i>
                                                </a>
                                            </span>
                                        </div>
                                        <p id="valivaProducto" class="text-danger text-hide">Seleccione un producto</p>
                                    </div>

                                    <%--Cantidad--%>
                                    <div class="" style="padding-left: 0; width: 10%">
                                        <asp:HiddenField ID="idProducto" runat="server" />
                                        <div>
                                            <label>Cantidad</label>
                                            <asp:TextBox ID="txtCantidad" onchange="ValidadCantidad()" Text="0" onkeypress="javascript:return validarNro(event)" Style="text-align: right;" class="form-control money" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=""
                                                SetFocusOnError="false" ForeColor="Red" Font-Bold="false"
                                                ValidationGroup="AgregarProductos" ControlToValidate="txtCantidad">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <%--Precio--%>
                                    <div class="" style="padding-left: 0; width: 10%">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <div>
                                            <label>Precio</label>
                                            <asp:TextBox ID="txtPrecio" onchange="ValidarPrecio()" Text="0" onkeypress="javascript:return validarNro(event)" Style="text-align: right;" class="form-control money" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=""
                                                SetFocusOnError="false" ForeColor="Red" Font-Bold="false"
                                                ValidationGroup="AgregarProductos" ControlToValidate="txtPrecio">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <%-- Sector/Deposito --%>
                                    <div class="" style="width: 20%">
                                        <label>Sector/Deposito</label>
                                        <asp:DropDownList onchange="handleDepositoChange()" runat="server" ID="ddlDepositos" class="form-control"></asp:DropDownList>
                                    </div>

                                    <%-- Marca --%>
                                    <div class="" style="width: 20%">
                                        <label>Marca</label>
                                        <asp:DropDownList runat="server" ID="ddlMarca" class="form-control"></asp:DropDownList>
                                    </div>

                                    <%--Presentacion--%>
                                    <div class="" style="text-align: left; width: 20%">
                                        <label>Presentacion</label>
                                        <%--<asp:TextBox ID="txtUnidadMed" disabled="disabled" Style="text-align: right" class="form-control" runat="server" />--%>
                                        <asp:DropDownList runat="server" ID="ddlPresentaciones" class="form-control"></asp:DropDownList>
                                    </div>

                                    <%--Lote--%>
                                    <div class="" style="width: 10%">
                                        <label>Lote</label>
                                        <asp:TextBox runat="server" onChange="ValidadLote()" ID="txtLote" class="form-control"></asp:TextBox>
                                    </div>

                                    <%--Vencimiento--%>
                                    <div class="" style="display: flex; flex-wrap: nowrap; width: 15%">
                                        <div class="form-group " id="data_2">
                                            <label>Vencimiento</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox onChange="ValidarDias()" class="form-control" runat="server" ID="txtFechaVencimiento"></asp:TextBox>
                                            </div>
                                            <p id="valiva" class="text-danger text-hide">La fecha de vencimiento no puede ser menor a 30 dias </p>
                                        </div>
                                    </div>

                                    <%--Boton Agregar--%>
                                    <div>
                                        <label style="visibility: hidden">Añadir</label>
                                        <linkbutton id="btnAgregarProducto" onclick="agregarProductoPH();" data-toggle="tooltip" data-placement="top"
                                            data-original-title="Agregar Producto" class="btn btn-primary dim required" validationgroup="AgregarProductos">
                                            <i style="color: white" class="fa fa-check"></i>
                                        </linkbutton>
                                    </div>

                                </div>
                            </div>


                            <%--Tabla--%>
                            <div style="margin-top: 1rem">
                                <asp:HiddenField runat="server" ID="idProductosRecetas" />
                                <asp:HiddenField runat="server" ID="hiddenReceta" />
                                <table class="table table-bordered table-hover" id="tableProductos" style="max-width: 99%;">
                                    <thead>
                                        <tr>
                                            <%--<th style="width: 6%">Cod Producto</th>--%>
                                            <th style="width: 10%">Descripcion</th>
                                            <th style="width: 10%">Deposito</th>
                                            <th style="width: 6%">Marca</th>
                                            <th style="width: 9%">Presentacion</th>
                                            <th style="width: 5%; text-align: right">Cantidad</th>
                                            <th style="width: 5%; text-align: right">Precio</th>
                                            <th style="width: 5%; text-align: right">Total</th>
                                            <%--<th style="width: 5%">Lote</th>
                                            <th style="width: 5%">Vencimiento</th>--%>
                                            <th style="width: 3%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phProductos" runat="server" />
                                    </tbody>
                                </table>

                                <br />

                                <%--Suma Total--%>
                                <div style="text-align: right; padding-right: 1rem;">
                                    <span style="font-size: 2rem">Total: </span>

                                    <div style="display: inline-block; font-size: 2rem; font-weight: bold;">
                                        <span>$</span>
                                        <span id="total">0</span>
                                    </div>
                                </div>

                                <br />

                                <div>
                                    <asp:Button class="btn btn-md btn-primary pull-right m-t-n-xs" Style="margin-right: 8px;" data-toggle="tooltip"
                                        data-placement="top" title data-original-title="Guardar"
                                        Text="Guardar" runat="server" ValidationGroup="AgregarEntregas" ID="btnGuardar" disabled="true" OnClick="btnGuardar_Click" />
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="modalTabsProductos" class="modal" style="z-index: 2000;" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h2 class="modal-title">identifiquemos tu Ingrediente
                        <span>
                            <%--<i style='color:black;' class='fa fa-search'></i>--%>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                                <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                            </svg>

                        </span>
                    </h2>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel blank-panel">

                            <div class="panel-heading">
                                <div class="panel-title m-b-md"></div>
                                <div class="panel-options">

                                    <ul class="nav nav-tabs">
                                        <li class="active"><a data-toggle="tab" href="#tab-1">Productos</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-2">Recetas</a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="input-group m-b">
                                    <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                                    <input type="text" id="txtBusquedaIngredientes" placeholder="Busqueda..." class="form-control" />
                                </div>
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane active">
                                        <table class="table table-bordered table-hover" id="editable2">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%">Cod.</th>
                                                    <th style="width: 20%">Descripcion</th>
                                                    <th style="width: 20%">Costo $</th>
                                                    <%--<th style="width: 20%">Unidad Medida</th>--%>
                                                    <th style="width: 10%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="phProductosAgregar" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>

                                    <div id="tab-2" class="tab-pane">
                                        <table class="table table-bordered table-hover" id="editable3">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%">Cod.</th>
                                                    <th style="width: 20%">Descripcion</th>
                                                    <th style="width: 20%">Costo $</th>
                                                    <th style="width: 10%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="phRecetasModal" runat="server" />
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
        <!-- Mainly scripts -->
        <script src="/../js/jquery-2.1.1.js"></script>
        <!-- Mainly scripts -->
        <%--<script src="/../Scripts/jquery-3.4.1.js"></script>--%>
        <script src="../../js/bootstrap.min.js"></script>
        <script src="../../Scripts/plugins/metisMenu/jquery.metisMenu.js"></script>
        <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/3.2.6/jquery.inputmask.bundle.min.js"></script>

        <%--  <script src="../../Scripts/jquery.mask.js"></script>
            <script src="../../Scripts/jquery.mask.min.js"></script>--%>
        <%--<script src="../../Scripts/plugins/slimscroll/jquery.slimscroll.min.js"></script>--%>

        <!-- Custom and plugin javascript -->

        <%--<script src="../Scripts/plugins/pace/pace.min.js"></script>--%>
        <%--            <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
            <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>--%>
        <!-- Steps -->
        <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>

        <!-- Jquery Validate -->
        <script src="/../Scripts/plugins/validate/jquery.validate.min.js"></script>


        <%--<script src="../../Scripts/plugins/iCheck/icheck.min.js"></script>--%>
        <%--<script src="/../Scripts/plugins/summernote/summernote.min.js"></script>--%>

        <%--<script src="../../Scripts/plugins/toastr/toastr.min.js"></script>--%>

        <script src="//cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
        <link href="//cdn.datatables.net/1.10.2/css/jquery.dataTables.css" rel="stylesheet" />
    </div>

    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });


            let url = new URL(window.location.href);
            let idIngrediente = "";
            let inngredienteReceta = "";
            let sectorDescripcion = "";
            let idSector = "";
            idIngrediente = url.searchParams.get('idP');
            inngredienteReceta = url.searchParams.get('Desc');
            sectorDescripcion = url.searchParams.get('SP');
            idSector = url.searchParams.get('idS');

            if (idIngrediente != null) {
                document.getElementById('<%=txtDescripcionProductos.ClientID%>').value = idIngrediente + " - " + inngredienteReceta;
                handle();
                <%--document.getElementById('<%=txtSector.ClientID%>').value = idSector + " - " + sectorDescripcion;--%>
            }

        });
        $('#data_1 .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#data_2 .input-group.date').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    </script>
    <script>
        function handle(e) {

            document.getElementById('btnAgregarProducto').removeAttribute('disabled');
            //let x = ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[1];
            let txtProd = document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value
            if (txtProd.includes(' - ')) {

                const idOption = document.querySelector('option[value="' + txtProd + '"]').id;
                let costo = idOption.split("_")[5];
                //let prod = document.getElementById('ContentPlaceHolder1_Productos_' + idOption.split("_")[1] + "_" + idOption.split("_")[2]).children[0].innerHTML;
                //let costo = document.getElementById('ContentPlaceHolder1_Productos_' + idOption.split("_")[1] + "_" + idOption.split("_")[2]).children[1].innerHTML;
                if (idOption.includes("c_p_")) {
                    agregarProducto(idOption, costo);
                    //CargarRubro(txtProd.split('-')[0].trim(), 1);
                    CargarDepositos(txtProd.split('-')[0].trim(), 1);
                    CargarOptionsDllPresentaciones(txtProd.split('-')[0].trim(), 1);
                    CargarOptionDllMarcas(txtProd.split('-')[0].trim(), 1);
                }
                else if (idOption.includes("c_r_")) {
                    agregarReceta(idOption, costo)
                    CargarDepositos(txtProd.split('-')[0].trim(), 2);
                    CargarOptionsDllPresentaciones(txtProd.split('-')[0].trim(), 2);
                    CargarOptionDllMarcas(txtProd.split('-')[0].trim(), 2);
                }
            }
        }

        function CargarOptionDllMarcas(id, tipo) {
            document.getElementById('<%=ddlMarca.ClientID%>').removeAttribute('disabled');

            $.ajax({
                method: "POST",
                url: "Entregas.aspx/GetMarca",
                data: '{idItem: "' + id + '", tipo:"' + tipo + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: function (respuesta) {
                    //quito los options que pudiera tener previamente el combo
                    $("#<%=ddlMarca.ClientID%>").html("");

                    if (respuesta.d.length > 0) {
                        $.each(respuesta.d, function () {
                            $("#<%=ddlMarca.ClientID%>").append($("<option></option>").attr("value", this.id).text(this.descripcion))
                            });

                    } else {
                        toastr.error("El item no tiene marcas asignadas.", "Error", {
                            "positionClass": "toast-bottom-right"
                        });

                        // Deshabilitar boton para agregar el item
                        document.getElementById('btnAgregarProducto').setAttribute('disabled', 'true');
                    }
                }
            });
        }

        function CargarDepositos(id, tipo) {
            $.ajax({
                method: "POST",
                url: "Entregas.aspx/GetIdSectorByIdProd",
                data: '{idProd: "' + id + '", tipo:"' + tipo + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: function (respuesta) {
                    var ddlDepositos = $("#<%=ddlDepositos.ClientID%>");
                    ddlDepositos.val(respuesta.d);
                }
            });
        }

        function CargarOptionsDllPresentaciones(id, tipo) {
            $.ajax({
                method: "POST",
                url: "Entregas.aspx/GetPresentaciones",
                data: '{idProd: "' + id + '",tipo:"' + tipo + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: function (respuesta) {
                    //quito los options que pudiera tener previamente el combo
                    $("#<%=ddlPresentaciones.ClientID%>").html("");
                    //recorro cada item que devuelve el servicio web y lo aÃ±ado como un opcion
                    if (respuesta.d.length > 0) {
                        $.each(respuesta.d, function () {
                            $("#<%=ddlPresentaciones.ClientID%>").append($("<option></option>").attr("value", this.id).text(this.descripcion))
                        });
                    } else {
                        $("#<%=ddlPresentaciones.ClientID%>").append($("<option></option>").attr("value", -1).text("No tiene presentaciones asignadas"))
                        toastr.error("El producto no tiene presentacion.", "Error", {
                            "positionClass": "toast-bottom-right"
                        });

                        // Deshabilitar boton para agregar el item
                        document.getElementById('btnAgregarProducto').setAttribute('disabled', 'true');
                    }
                }
            });
        }

        function handleDepositoChange() {
            let deposito = document.getElementById('<%= ddlDepositos.ClientID %>');

            if (deposito.value != "-1")
                deposito.style.border = "1px solid #e5e6e7";
            else
                deposito.style.border = "1px solid red";
        }

        function ComprobarFecha(date, DiaIngresado) {
            DiaIngresado = DiaIngresado.replaceAll("/", "-")
            DiaIngresado = DiaIngresado.split(['-']).reverse().join("-")
            let date1 = date
            let date2 = new Date(DiaIngresado)
            console.log(date1, date2)
            if (date2 > date1) {
                var diff = date2.getTime() - date1.getTime();
                return dias = Math.round(diff / (1000 * 60 * 60 * 24));
            }
            else if (date2 != null && date2 < date1) {
                return false
            }

        }

        function agregarProductoPH() {

            // Validaciones
            let ValDias = ValidarDias();
            if (ValDias == false) {
                return false
            }

            let prod = document.getElementById('<%=txtDescripcionProductos.ClientID%>')
            if (prod.value.length < 1) {
                document.getElementById('valivaProducto').className = 'text-danger'
                return false;
            }

            let cant = document.getElementById('<%=txtCantidad.ClientID%>');
            if (cant.value <= 0) {
                cant.style.border = "1px solid red";
                return false;
            }

            let precio = document.getElementById('<%=txtPrecio.ClientID%>');
            if (precio.value < 0) {
                precio.style.border = "1px solid red";
                return false;
            }

            let fechaVencimiento = document.getElementById('<%= txtFechaVencimiento.ClientID%>');
            let lot = document.getElementById('<%=txtLote.ClientID%>');

            // Sector/Deposito
            let deposito = document.getElementById('<%=ddlDepositos.ClientID%>');
            let tdDeposito = "";

            if (deposito.value != "-1") {
                tdDeposito = "<td> " + deposito.selectedOptions[0].text + "</td>";
                deposito.style.border = "1px solid #e5e6e7";
            }
            else {
                deposito.style.border = "1px solid red";
                return false
            }

            //
            lot.style.border = "1px solid #e5e6e7";
            cant.style.border = "1px solid #e5e6e7";

            var codigo = ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0];
            var cantidad = ContentPlaceHolder1_txtCantidad.value.replace(',', '');
            var precioFormated = precio.value.replace(',', '');
            var tipo = ContentPlaceHolder1_Hiddentipo.value;
            let btec = "";
            let styleCorrect = "";
            let unidad = document.getElementById('ContentPlaceHolder1_ddlPresentaciones').selectedOptions[0].text;

            //costototal = costototal.toString().replace('.', ',');

            let tdDescripcion = "<td> " + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[1] + "</td>";
            let tdCantidad = "<td style=\" text-align: right\"> " + myFormat(cantidad) + "</td>";
            let tdPrecio = "<td style=\" text-align: right\"> $ " + myFormat(precioFormated) + "</td>";
            let tdTotalProducto = "<td style=\" text-align: right\"> $ " + (myFormat(cantidad) * myFormat(precioFormated)).toFixed(2) + "</td>";
            let tdUnidad = "<td> " + unidad + "</td>";
            let btnRec = "";

            marca = "<td> " + document.getElementById('<%=ddlMarca.ClientID%>').selectedOptions[0].text + "</td>";
            idMarca = document.getElementById('<%=ddlMarca.ClientID%>').value.trim()

            if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + codigo + "," + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value)) {
                $('#tableProductos').append(
                    "<tr id=" + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "_" + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value.trim() + ">" +
                    /*"<td style=\" text-align: right\"> " + codigo + "</td>" +*/
                    tdDescripcion +
                    tdDeposito +
                    marca +
                    //"<td ondblclick=\"CargarmodalRecetaDetalle('" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0]+"')\" > " + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[1] + "</td>" +
                    tdUnidad +
                    <%--"<td style=\" text-align: center\"> " + document.getElementById('<%=txtLote.ClientID%>').value + "</td>" +
                    "<td style=\" text-align: center\"> " + document.getElementById('<%=txtFechaVencimiento.ClientID%>').value + "</td>" +--%>

                    tdCantidad +
                    tdPrecio +
                    tdTotalProducto +

                    "<td style=\" text-align: center\">" +
                    " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs \" onclick=\"javascript: return borrarProd('" + tipo + "_" + codigo.trim() + "_" + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value + "', " + (myFormat(cantidad) * myFormat(precioFormated)).toFixed(2) + ");\" >" +
                    "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> " +
                    btnRec
                    + "</td > " +
                    "</tr>"
                );
                if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += (codigo + "%" + tipo + "%" + idMarca + "%" + cantidad + "%" + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "_" + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value + "%" + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value + "%" + document.getElementById('<%=txtLote.ClientID%>').value + "%" + document.getElementById('<%= txtFechaVencimiento.ClientID%>').value + "%" + precioFormated + "%" + deposito.value).replaceAll(".", ",");
                }
                else {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += (";" + codigo + "%" + tipo + "%" + idMarca + "%" + cantidad + "%" + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "_" + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value + "%" + document.getElementById('ContentPlaceHolder1_ddlPresentaciones').value + "%" + document.getElementById('<%=txtLote.ClientID%>').value + "%" + document.getElementById('<%=txtFechaVencimiento.ClientID%>').value + "%" + precioFormated + "%" + deposito.value).replaceAll(".", ",");;
                }

                // Aumentar total general
                //let totalActual = Number(document.getElementById('total').textContent);
                //totalActual = Number(totalActual) + (Number(precio.value) * Number(cantidad))
                //document.getElementById('total').textContent = totalActual;

                // Aumentar total general
                let total = document.getElementById('total');
                let totalActual = parseFloat(total.textContent);
                totalActual += parseFloat(precio.value).toFixed(2) * parseFloat(cantidad).toFixed(3);
                total.textContent = totalActual.toFixed(2);


                // Limpiar campos
                prod.value = "";
                cant.value = "0";
                precio.value = "0";
                lot.value = "";
                fechaVencimiento.value = "";
                unidad = "";

                $("#<%=ddlDepositos.ClientID%>").val("-1");;

                $("#<%=ddlPresentaciones.ClientID%>").html("");

                  //document.getElementById('<%--<%=txtUnidadMed.ClientID%>--%>').value = "";
                document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').focus();

                document.getElementById('<%= btnGuardar.ClientID %>').disabled = false;
            }
        }

        function agregarProducto(clickedId, costo) {
            ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[2] + ' - ' + clickedId.split('_')[3];
            ContentPlaceHolder1_Hiddentipo.value = "Producto";
            //ContentPlaceHolder1_HiddenUnidad.value = document.getElementById('ContentPlaceHolder1_ddlPresentaciones').selectedOptions[0].text;
            //ContentPlaceHolder1_txtUnidadMed.value = clickedId.split('_')[4];
            ContentPlaceHolder1_HiddenCosto.value = costo;

            $('input[type=search]').val('');// Clear Search input.
            document.querySelector('#txtBusquedaIngredientes').value = ''

            $('#modalTabsProductos').modal('hide');
            document.getElementById('<%=txtCantidad.ClientID%>').value = '';
            document.getElementById('<%=txtCantidad.ClientID%>').focus();
        }

        function agregarReceta(clickedId, costo) {
            ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[2] + ' - ' + clickedId.split('_')[3];
            ContentPlaceHolder1_Hiddentipo.value = "Receta";
            ContentPlaceHolder1_HiddenUnidad.value = clickedId.split('_')[4];
            //ContentPlaceHolder1_txtUnidadMed.value = clickedId.split('_')[4];
            ContentPlaceHolder1_HiddenCosto.value = costo;

            $('input[type=search]').val('');// Clear Search input.
            document.querySelector('#txtBusquedaIngredientes').value = ''

            $('#modalTabsProductos').modal('hide');
            document.getElementById('<%=txtCantidad.ClientID%>').value = '';
            document.getElementById('<%=txtCantidad.ClientID%>').focus();
        }

        function borrarProd(idprod, totalToRemove) {
            event.preventDefault();

            $('#' + idprod).remove();
            var productos = ContentPlaceHolder1_idProductosRecetas.value.split(';');

            var nuevosProductos = "";
            for (var x = 0; x < productos.length; x++) {
                if (productos[x] != "") {
                    if (!productos[x].includes(idprod)) {
                        nuevosProductos += productos[x] + ";";
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
            ContentPlaceHolder1_idProductosRecetas.value = nuevosProductos;

            if (nuevosProductos == "") {
                document.getElementById('<%= btnGuardar.ClientID %>').disabled = true;
            }


            // Descontar en total general
            let totalElement = document.getElementById('total');
            let totalActual = parseFloat(totalElement.textContent);
            console.log(totalToRemove);
            totalActual -= totalToRemove;
            totalElement.textContent = totalActual.toFixed(2);

        }
    </script>
    <script>
        function validarNro(e) {
            var key;
            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            if (key < 48 || key > 57) {
                if (key == 46 || key == 8)//|| key == 44)  Detectar . (punto) , backspace (retroceso) y , (coma)
                { return true; }
                else { return false; }
            }
            return true;
        }
        (function () {
            /**
             * Ajuste decimal de un número.
             *
             * @param {String}  tipo  El tipo de ajuste.
             * @param {Number}  valor El numero.
             * @param {Integer} exp   El exponente (el logaritmo 10 del ajuste base).
             * @returns {Number} El valor ajustado.
             */
            function decimalAdjust(type, value, exp) {
                // Si el exp no está definido o es cero...
                if (typeof exp === 'undefined' || +exp === 0) {
                    return Math[type](value);
                }
                value = +value;
                exp = +exp;
                // Si el valor no es un número o el exp no es un entero...
                if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0)) {
                    return NaN;
                }
                // Shift
                value = value.toString().split('e');
                value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));
                // Shift back
                value = value.toString().split('e');
                return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
            }

            // Decimal round
            if (!Math.round10) {
                Math.round10 = function (value, exp) {
                    return decimalAdjust('round', value, exp);
                };
            }
            // Decimal floor
            if (!Math.floor10) {
                Math.floor10 = function (value, exp) {
                    return decimalAdjust('floor', value, exp);
                };
            }
            // Decimal ceil
            if (!Math.ceil10) {
                Math.ceil10 = function (value, exp) {
                    return decimalAdjust('ceil', value, exp);
                };
            }
        })();
        function myFormat(str) {
            //const cleaned = str.replace(/[^\d,]/g, '').replace(",", ".")
            return Number(str).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 4 })
        }
        function ValidadCantidad() {
            let cant = document.getElementById('<%=txtCantidad.ClientID%>')
            cant.style.border = "1px solid #e5e6e7";
        }
        function ValidarPrecio() {
            let precio = document.getElementById('<%=txtPrecio.ClientID%>')
            precio.style.border = "1px solid #e5e6e7";
        }
        function ValidadLote() {
            let lot = document.getElementById('<%=txtLote.ClientID%>')
            lot.style.border = "1px solid #e5e6e7";
        }
        function ValidarProducto() {
            document.getElementById('valivaProducto').className = 'text-danger text-hide'
        }
        function ValidarDias() {
            let today = new Date();
            let DiasDiferencia = ComprobarFecha(today, document.getElementById('<%=txtFechaVencimiento.ClientID%>').value)
            if (DiasDiferencia == false) {
                document.getElementById('valiva').className = 'text-danger'
                return false
            } else {

                if (DiasDiferencia < 30) {
                    document.getElementById('valiva').className = 'text-danger'
                    return false
                } else {
                    document.getElementById('valiva').className = 'text-danger text-hide'
                    return true
                }
            }
        }
    </script>


    <script>
        $(document).ready(function () {
            $('#editable2').DataTable({
                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "searchPlaceholder": "Escriba su busqueda",
                    "lengthMenu": "Mostrar _MENU_ Entradas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }
            });

            $('.dataTables_filter').hide();
            $('#txtBusquedaIngredientes').on('keyup', function () {
                $('#editable2').DataTable().search(
                    this.value
                ).draw();
            });



            // Modal buscar recetas
            $('#editable3').DataTable({
                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "searchPlaceholder": "Escriba su busqueda",
                    "lengthMenu": "Mostrar _MENU_ Entradas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }
            });

            $('.dataTables_filter').hide();
            $('#txtBusquedaIngredientes').on('keyup', function () {
                $('#editable3').DataTable().search(
                    this.value
                ).draw();
            });




            $('.dataTables_filter').hide();


            $(document).on('keyup', "input[type='search']", function () {
                var oTable = $('.dataTable').dataTable();
                oTable.fnFilter($(this).val());
            });

        });
        $(document).ready(function () {

            $(".money").inputmask({
                'alias': 'decimal',
                rightAlign: true,
                'groupSeparator': ',',
                'autoGroup': true
            });
        })
    </script>

</asp:Content>
