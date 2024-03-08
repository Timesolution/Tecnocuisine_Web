<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerarVenta.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.GenerarVenta" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px; padding-top: 0px;">
            <div class="p-xs" style="padding-bottom: 80px; padding-top: 15px;">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-6">

                            <div class="pull-left m-r-md">
                                <i class="fa fa-dollar text-navy mid-icon"></i>
                            </div>
                            <h2 id="DivSaldo" style="font-weight: bold;">0.00</h2>
                            <span id="ClienteSelec">Cliente no Seleccionado</span>

                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">
                                    <div style="display: flex;">

                                     <h2 style="margin-left: 5%;font-weight: bold;" id="TipoFactura">Factura A:</h2>
                                    <div id="lblVentaNum" runat="server">
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">

                    <div class="ibox-content">
                        <div class="row" style="padding-left: 15px;">

                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="row">
                                        <%--style="margin-top: 2%"--%>
                                        <div class="col-md-4">
                                            <h4 style="margin-left: 5%">Cliente:</h4>
                                        </div>
                                        <div class="col-md-8">
                                            <datalist id="ListClientes" runat="server"></datalist>
                                            <div style="display: flex;">

                                                <asp:TextBox runat="server" ID="txtClientes" list="ContentPlaceHolder1_ListClientes" onchange="CambiarCliente()" class="form-control" Style="margin-left: 15px; width: 70%" />
                                                <a id="LinkButtonMarcas1" class="btn btn-primary dim" data-toggle="modal" data-target="#modalAgregar" data-backdrop="static" data-keyboard="false"><i style="color: white" class="fa fa-plus"></i></a>

                                            </div>
                                            <p id="valivaCliente" class="text-danger text-hide">Tienes que agregar un cliente</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h4 style="margin-left: 5%">Productos y Recetas:</h4>
                                        </div>
                                        <div class="col-md-8">
                                            <datalist id="ListaNombreProd" runat="server">
                                            </datalist>
                                            <asp:TextBox runat="server" ID="txtDescripcionProductos" 
                                                onfocusout="handle(event)" list="ContentPlaceHolder1_ListaNombreProd" 
                                                class="form-control" Style="margin-left: 15px; width: 70%" />
                                            <asp:HiddenField ID="Hiddentipo" runat="server" />
                                            <asp:HiddenField ID="HiddenUnidad" runat="server" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <%--Columna 1--%>
                                <div class="col-lg-6">
                                    <div>

                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Tipo Documento:</h4>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="txtTipoDocumento" runat="server" class="form-control" onchange="ObtenerCodigo()" Style="margin-left: 15px; width: 70%">
                                                    <%-- Harcodeamos pero le ponemos como value el id de los tipos de documentos de la base de datos --%>
                                                    <asp:ListItem Value="1">Factura A</asp:ListItem>
                                                    <asp:ListItem Value="6">Factura B</asp:ListItem>
                                                    <asp:ListItem Value="3">Nota de Credito A</asp:ListItem>
                                                    <asp:ListItem Value="8">Nota de Credito B</asp:ListItem>
                                                    <asp:ListItem Value="2">Nota de Debito A</asp:ListItem>
                                                    <asp:ListItem Value="7">Nota de Debito B</asp:ListItem>
                                                    <asp:ListItem Value="97">Presupuesto</asp:ListItem>
                                                    <asp:ListItem Value="99">Presupuesto Nota de Credito</asp:ListItem>
                                                    <asp:ListItem Value="100">Presupuesto Nota de Debito</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Forma de Pago:</h4>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="txtFormaPago" onchange="CambiarFormaPago()" runat="server" class="form-control" Style="margin-left: 15px; width: 70%">
                                                    <%-- Harcodeamos pero le ponemos como value el id de los tipos de documentos de la base de datos --%>
                                                    <asp:ListItem Value="1">Contado</asp:ListItem>
                                                    <asp:ListItem Value="2">Cuenta Corriente</asp:ListItem>
                                                    <asp:ListItem Value="3">Tarjeta de Credito</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <%--TARJETA DE CREDITO--%>

                                        <div id="DivTarjeta" class="row" style="margin-top: 2%; display: none">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Entidad:</h4>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="DropDownEntidadList" class="form-control" runat="server" onchange="BuscarTarjetasByEntidades(this)" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DivTarjeta2" class="row" style="margin-top: 2%; display: none">
                                        <div class="col-md-4">
                                            <h4 style="margin-left: 5%">Tarjeta:</h4>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="DropDownListTarjetaCredito" runat="server" class="form-control" Style="margin-left: 15px; width: 70%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <%--        <asp:DropDownList ID="txtTipoCliente" runat="server" class="form-control">

                                                                <asp:ListItem Value="1">Mujer</asp:ListItem>
                                                                <asp:ListItem Value="2">Hombre</asp:ListItem>
                                                                <asp:ListItem Value="3">Niño</asp:ListItem>

                                                            </asp:DropDownList>--%>
                                <%-- Columna 2--%>
                                
                                <div class="col-lg-6">
                                    <div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Cantidad:</h4>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NCantidad" type="number" onchange="ValidadCantidadProducida()" class="form-control" Style="margin-left: 15px; width: 70%;text-align: right" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Rinde: </h4>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NRinde" class="form-control" Style="margin-left: 15px; width: 70%;text-align: right" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Costo:</h4>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NCosto" class="form-control" Style="margin-left: 15px; width: 70%;text-align: right" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h4 style="margin-left: 5%">Precio Venta:</h4>
                                            </div>
                                            <div class="col-md-8">
                                                <div style="display: flex;">

                                                    <asp:TextBox runat="server" ID="PVenta" onfocusout="formatearNum()" class="form-control" Style="margin-left: 15px; width: 70%;text-align: right" />
                                                    <div style="margin-left: 20px;">
                                                        <linkbutton class="btn btn-sm btn-primary" style="margin-right: 8px; margin-top: 5%;" data-toggle="tooltip" data-placement="top" data-original-title="Agregar"
                                                            text="Agregar" validationgroup="AgregarEntregas" id="Button1" onclick="AgregarATabla(event)">
                                                            <i style="color: white" class="fa fa-check"></i>
                                                        </linkbutton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div style="margin: 20px;">

                                <table class="table table-bordered table-hover" id="tableProductos" style="margin-top: 2%; max-width: 99%;">
                                    <thead>
                                        <tr>
                                            <th style="width: 5%; text-align: right;">#</th>
                                            <th style="width: 5%">Producto</th>
                                            <th style="width: 5%; text-align: right;">Costo</th>
                                            <th style="width: 5%; text-align: right;">Cantidad</th>
                                            <th style="width: 10%; text-align: right;">Rinde</th>
                                            <th style="width: 10%; text-align: right;">Precio Venta</th>
                                            <th style="width: 10%; text-align: right;">Precio Total</th>
                                            <th style="width: 4%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phTablaProductos" runat="server" />
                                    </tbody>
                                </table>
                                <p id="valivaTable" class="text-danger text-hide">Tienes que agregar al menos 1 producto para continuar</p>
                            </div>
                        </div>
                        <div style="display: flex; justify-content: end;">


                            <button class="btn btn-sm btn-primary" style="margin-right: 25px;" data-toggle="tooltip" data-placement="top" data-original-title="Confirmar Venta"
                                text="Confirmar Venta" validationgroup="AgregarEntregas" id="btnGuardar" onclick="ConfirmarVenta(event)">
                                Generar Venta</button>
                        </div>


                        <asp:HiddenField runat="server" ID="idProductosRecetas" />
                        <asp:HiddenField runat="server" ID="hiddenReceta" />
                        <asp:HiddenField runat="server" ID="HiddenRinde" />
                        <asp:HiddenField runat="server" ID="HiddenCosto" />
                        <asp:HiddenField runat="server" ID="HiddenPrecioVenta" />
                        <asp:HiddenField runat="server" ID="VerSiElProductoFueCargado"></asp:HiddenField>

                    </div>
                </div>
            </div>
        </div>

    </div>
    </div>



    <div id="modalAgregar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Agregar</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <label class="col-sm-2 control-label editable">Codigo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCodigo" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Razon Social</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtRazonSocial" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">CUIT</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCuit" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Alias</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAlias" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">IVA</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListRegimen" Style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Forma de Pago</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListFormaPago" Style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Vendedor</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListVendedor" Style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Vencimiento FC</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtVencimientoFC" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Saldo Maximo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSaldoMax" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Estado</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListEstado" Style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Observaciones</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtObservaciones" Style="margin-left: 3%;" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="LinkButton1" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>


                </div>
            </div>
        </div>
    </div>



    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
            /*ObtenerCodigo("1");*/
            ObtenerCodigo();

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
        function handle(e) {
            let txtProd = document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value

            if (txtProd.includes(' - ')) {
                let array = txtProd.split("-")
                let type = array[2].trim()
                let id = array[0].trim()
                if (type == "Receta") {
                    CargarDllReceta(Number(id));
                } else {
                    CargarDllProducto(Number(id));
                }
            }
        }

        function CambiarCliente() {
            let cliente = document.getElementById("ContentPlaceHolder1_txtClientes").value;
            if (cliente.includes("-")) {
                document.getElementById("ClienteSelec").innerText = cliente.split("-")[1];
            } else {
                document.getElementById("ClienteSelec").innerText = cliente;

            }
            clientes = document.getElementById("ContentPlaceHolder1_txtClientes").value
            ValivaCliente = document.getElementById("valivaCliente")
            if (clientes != "") {
                ValivaCliente.className = "text-danger text-hide"

            } else {
                ValivaCliente.className = "text-danger"
            }

        }
        function CambiarFormaPago() {
            if (document.getElementById("ContentPlaceHolder1_txtFormaPago").value == "3") {
                document.getElementById("DivTarjeta").style = "margin-top: 2%"
                document.getElementById("DivTarjeta2").style = "margin-top: 2%"

            } else {
                document.getElementById("DivTarjeta").style = "margin-top: 2%; display: none;"
                document.getElementById("DivTarjeta2").style = "margin-top: 2%; display: none;"

            }
        }
        function ValidadCantidadProducida() {
            let Cantidad = document.getElementById("ContentPlaceHolder1_NCantidad").value;
            let Rinde = document.getElementById("ContentPlaceHolder1_NRinde").value;
        }
        function BuscarTarjetasByEntidades(droplist) {
            if (droplist.value == "-1") {
                return
            } else {
                $.ajax({
                    method: "POST",
                    url: "GenerarVenta.aspx/ddlOpciones_SelectedTarjeta",
                    data: '{idEntidad: "' + droplist.value + '"}',
                    contentType: "application/json",
                    dataType: "json",
                    dataType: "json",
                    async: false,
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        if (respuesta.d != "") {
                            listDrop = document.getElementById("ContentPlaceHolder1_DropDownListTarjetaCredito");
                            listDrop.innerHTML = "";
                            names = respuesta.d.split("%");
                            for (let i = 0; i < names.length; i++) {
                                if (names[i] != "") {

                                    finalnames = names[i].split("&");
                                    listDrop.innerHTML += '<option value="' + finalnames[0] + '">' + finalnames[1] + '</option>';
                                }
                            }
                        }
                    }
                });
            }
        }
        function ObtenerCodigo() {
            valueid = document.getElementById("ContentPlaceHolder1_txtTipoDocumento").value;
          /*  document.getElementById("ContentPlaceHolder1_txtTipoDocumento").value = "";*/
            var dropdown = document.getElementById("ContentPlaceHolder1_txtTipoDocumento");
            var opcionSeleccionada = dropdown.options[dropdown.selectedIndex];
            $.ajax({
                method: "POST",
                url: "GenerarVenta.aspx/GenerarCodigoFactura",
                data: '{id: "' + valueid + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    if (respuesta.d != "") {
                        factnum = document.getElementById("ContentPlaceHolder1_lblVentaNum");
                        factnum.innerHTML = "";
                        factnum.innerHTML = '<h2 style="margin-left:15px;">' + respuesta.d + '</h3>'
                        document.getElementById("TipoFactura").innerText = opcionSeleccionada.text;
                    }
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
                url: "GenerarVenta.aspx/GetReceta",
                data: '{idProd: "' + id + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    RellenarCampos(respuesta.d.split("-"))
                }
            });
        }
        function VaciarCampos() {
            toastr.success('Venta Completada');
            document.getElementById("btnGuardar").disabled = false;
            $('#tableProductos tbody')[0].innerHTML = ""
            document.getElementById('<%= idProductosRecetas.ClientID%>').value = "";
            document.getElementById("ClienteSelec").innerText = "";
            document.getElementById("DivSaldo").innerText = "0.00";
        }

        function RellenarCamposProducto(response) {
            document.getElementById('ContentPlaceHolder1_NRinde').value = 1;
            document.getElementById('ContentPlaceHolder1_NCosto').value = response[0];
            document.getElementById('ContentPlaceHolder1_PVenta').value = response[1];
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
            document.getElementById('ContentPlaceHolder1_NCosto').disabled = true;
        }
        /* formatearNumero revertirNumero*/
        function RellenarCampos(response) {
            document.getElementById('ContentPlaceHolder1_NRinde').value = response[2];
            document.getElementById('ContentPlaceHolder1_NCosto').value = formatearNumero(Number(response[0]));
            document.getElementById('ContentPlaceHolder1_PVenta').value = formatearNumero(Number(response[1]));
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
            document.getElementById('ContentPlaceHolder1_NCosto').disabled = true;
        }
    </script>
    <script>


        function ConfirmarVenta(event) {
            event.preventDefault();
            let btn = document.getElementById("btnGuardar")
            btn.disabled = true;
            let list = document.getElementById('<%= idProductosRecetas.ClientID%>').value;
            clientes = document.getElementById("ContentPlaceHolder1_txtClientes").value
            ValivaCliente = document.getElementById("valivaCliente")
            if (clientes == "") {
                ValivaCliente.className = "text-danger"
                btn.disabled = false;
                return
            } else {
                ValivaCliente.className = "text-danger text-hide"
            }
            ValivaTable = document.getElementById("valivaTable")
            if (list == "") {
                ValivaTable.className = "text-danger";
                btn.disabled = false;
                return
            } else {
                ValivaTable.className = "text-danger text-hide"
            }


            $.ajax({
                method: "POST",
                url: "GenerarVenta.aspx/ConfirmarLaVenta",
                data: '{list: "' + list
                    + '" , cliente: "' + document.getElementById("ContentPlaceHolder1_txtClientes").value
                    + '" , tipofac: "' + document.getElementById("ContentPlaceHolder1_txtTipoDocumento").value
                    + '" , formapago: "' + document.getElementById("ContentPlaceHolder1_txtFormaPago").value
                    + '" , idtarjeta: "' + document.getElementById("ContentPlaceHolder1_DropDownListTarjetaCredito").value
                    + '" , idEntidad: "' + document.getElementById("ContentPlaceHolder1_DropDownEntidadList").value

                    + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                    btn.disabled = false;
                },
                success: () => {
                    VaciarCampos();
                }
            });
        }

        function AgregarATabla(event) {
            event.preventDefault();
            ValivaTable = document.getElementById("valivaTable")
            ValivaTable.className = "text-danger text-hide"
            let Rinde = document.getElementById('ContentPlaceHolder1_NRinde').value;
            let totalFinalSaldo = revertirNumero(document.getElementById('DivSaldo').innerText);
            if (Rinde == 0) {
                Rinde = 1
            }
            let Costo = document.getElementById('ContentPlaceHolder1_NCosto').value;
            let Venta = document.getElementById('ContentPlaceHolder1_PVenta').value;
            let Cantidad = (document.getElementById('ContentPlaceHolder1_NCantidad').value);
            let producto = document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value;
            let totalFinal = formatearNumero(revertirNumero(Venta) * revertirNumero(Cantidad));
            totalFinalSaldo += revertirNumero(totalFinal);
            document.getElementById('DivSaldo').innerText = "";
            document.getElementById('DivSaldo').innerText = formatearNumero(totalFinalSaldo);
            let listaDesplegable = "<td> " + producto.split('-')[1] + "</td>";
            let id = "<td> " + producto.split('-')[0].trim() + "</td>";
            let cant = "<td style=\" text-align: right\"> " + Cantidad + "</td>";
            let rinde = "<td style=\" text-align: right\">" + Rinde + "</td>";
            let tipo = producto.split('-')[2].trim();
            let costo = "<td style=\" text-align: right\">" + Costo + "</td>";
            let venta = "<td style=\" text-align: right\">" + Venta + "</td>";

            let ventaTotal = "<td style=\" text-align: right\">" + totalFinal + "</td>";


            let styleCorrect = "";
            let btnrec = "";

            if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + id + "," + listaDesplegable)) {
                $('#tableProductos').append(
                    "<tr id=" + tipo + "_" + producto.split('-')[0].trim() + "_" + Cantidad.toString().trim() + ">" +
                    id +
                    listaDesplegable +
                    costo +
                    cant +
                    rinde +
                    venta +
                    ventaTotal +
                    "<td style=\" text-align: center\">" +
                    " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs \" onclick=\"javascript: return borrarProd('" + tipo + "_" + producto.split('-')[0].trim() + "_" + Cantidad.toString().trim() + "');\" >" +
                    "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> " +
                    btnrec + "</td > " +
                    "</tr>"
                );
                if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += producto.split('-')[0].trim() + "-" + tipo + "-" + totalFinal.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim() + "-" + Costo.toString().trim();
                }
                else {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + producto.split('-')[0].trim() + "-" + tipo + "-" + totalFinal.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim() + "-" + Costo.toString().trim();
                }
                document.getElementById('ContentPlaceHolder1_NRinde').value = "";
                document.getElementById('ContentPlaceHolder1_NCosto').value = "";
                document.getElementById('ContentPlaceHolder1_PVenta').value = "";
                document.getElementById('ContentPlaceHolder1_NCantidad').value = "";
                document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value = "";
                document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').focus();

            }




        }

        function borrarProd(idprod) {
            event.preventDefault();

            $('#' + idprod).remove();
            var productos = document.getElementById('<%= idProductosRecetas.ClientID%>').value.split(';');
            let arr = idprod.split("_")
            let tip = arr[0];
            let idproducto = arr[1];
            let cantProd = arr[2];
            var nuevosProductos = "";
            for (var x = 0; x < productos.length; x++) {
                if (productos[x] != "") {
                    let arrProduct = productos[x].split("-")
                    if (arrProduct[0] != idproducto) {
                        nuevosProductos += productos[x] + ";";
                    } else {
                        if (arrProduct[0] == idproducto && arrProduct[1] != tip) {
                            nuevosProductos += productos[x] + ";";
                        } else {
                            if (arrProduct[0] == idproducto && arrProduct[1] == tip && arrProduct[3] != cantProd) {
                                nuevosProductos += productos[x] + ";";
                            } else {
                                let descontar = revertirNumero(arrProduct[2]);
                                let totalFinalSaldo = revertirNumero(document.getElementById('DivSaldo').innerText);
                                let final = totalFinalSaldo - descontar;
                                document.getElementById('DivSaldo').innerText = "";
                                document.getElementById('DivSaldo').innerText = formatearNumero(final);

                            }
                        }

                    }

                }
            }
            ContentPlaceHolder1_idProductosRecetas.value = nuevosProductos;
        }


        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function revertirNumero(numeroFormateado) {
            var numero = parseFloat(numeroFormateado.replace(/,/g, ''));
            return numero;
        }
        function formatearNum() {
            input = document.getElementById("ContentPlaceHolder1_PVenta");
            num = Number(revertirNumero(input.value));
            newnum = formatearNumero(num);
            input.value = newnum;
        }


    </script>


</asp:Content>
