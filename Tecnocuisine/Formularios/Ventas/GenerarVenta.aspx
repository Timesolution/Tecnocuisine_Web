<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerarVenta.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.GenerarVenta" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">

                    <div class="ibox-content">
                        <div class="row" style="padding-left: 15px;">
                            <%--  <div class="col-sm-8">


                                <div class="form-group" style="padding-right: 5px;">
                                    <%--<div class="col-md-6">


                                    <label>Productos y Recetas </label>

                                    <div class="input-group" style="text-align: right;">
                                        <datalist id="ListaNombreProd" runat="server">
                                        </datalist>

                                        <asp:TextBox runat="server" ID="txtDescripcionProductos" onfocusout="handle(event)" list="ContentPlaceHolder1_ListaNombreProd" class="form-control" Style="width: 300%;" />
                                        <asp:HiddenField ID="Hiddentipo" runat="server" />
                                        <asp:HiddenField ID="HiddenUnidad" runat="server" />
                                    </div>

                                    <p id="valivaProducto" class="text-danger text-hide">Tienes que agregar un producto</p>
                             
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h5 style="margin-left: 5%">Productos y Recetas:</h5>
                                        </div>
                                        <div class="col-md-8">
                                            <datalist id="ListaNombreProd" runat="server">
                                            </datalist>
                                            <asp:TextBox runat="server" ID="txtDescripcionProductos" onfocusout="handle(event)" list="ContentPlaceHolder1_ListaNombreProd" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            <asp:HiddenField ID="Hiddentipo" runat="server" />
                                            <asp:HiddenField ID="HiddenUnidad" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h4 style="margin-left: 5%">Venta Numero:</h4>
                                        </div>
                                        <div class="col-md-8">
                                            <div id="lblVentaNum" runat="server">
                                            </div>
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
                                                <h5 style="margin-left: 5%">Cantidad:</h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NCantidad" type="number" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Rinde: </h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NRinde" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Costo:</h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NCosto" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 2%">
                                            <div class="col-md-4">
                                                <h5 style="margin-left: 5%">Precio Venta:</h5>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="PVenta" class="form-control" Style="margin-left: 15px; width: 70%" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-8">
                                                <div style="display: flex; align-items: end; justify-content: end;">
                                                    <linkbutton class="btn btn-sm btn-primary" style="margin-right: 8px;" data-toggle="tooltip" data-placement="top" data-original-title="Agregar"
                                                        text="Agregar" validationgroup="AgregarEntregas" id="Button1" onclick="AgregarATabla(event)">
                                                        <i style="color: white" class="fa fa-check"></i>
                                                    </linkbutton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- Columna 2--%>
                                <div class="col-lg-6">
                                    <div>
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
                                                <th style="width: 4%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phTablaProductos" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div>

                                <button class="btn btn-sm btn-primary pull-right m-t-n-xs" style="margin-right: 25px;" data-toggle="tooltip" data-placement="top" title data-original-title="Confirmar Venta"
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
    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
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
        }

        function RellenarCamposProducto(response) {
            document.getElementById('ContentPlaceHolder1_NRinde').value = 1;
            document.getElementById('ContentPlaceHolder1_NCosto').value = response[0];
            document.getElementById('ContentPlaceHolder1_PVenta').value = response[1];
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
            document.getElementById('ContentPlaceHolder1_NCosto').disabled = true;
        }

        function RellenarCampos(response) {
            document.getElementById('ContentPlaceHolder1_NRinde').value = response[2];
            document.getElementById('ContentPlaceHolder1_NCosto').value = response[0];
            document.getElementById('ContentPlaceHolder1_PVenta').value = response[1];
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
            document.getElementById('ContentPlaceHolder1_NCosto').disabled = true;
        }
    </script>
    <script>


        function ConfirmarVenta(event) {
            event.preventDefault();
            let btn = document.getElementById("btnGuardar")
            btn.disabled = true;
            $.ajax({
                method: "POST",
                url: "GenerarVenta.aspx/ConfirmarLaVenta",
                data: '{list: "' + document.getElementById('<%= idProductosRecetas.ClientID%>').value + '"}',
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
            let Rinde = document.getElementById('ContentPlaceHolder1_NRinde').value;
            if (Rinde == 0) {
                Rinde = 1
            }
            let Costo = document.getElementById('ContentPlaceHolder1_NCosto').value;
            let Venta = document.getElementById('ContentPlaceHolder1_PVenta').value;
            console.log(Venta);
            let Cantidad = document.getElementById('ContentPlaceHolder1_NCantidad').value;
            let producto = document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value;

            let listaDesplegable = "<td> " + producto.split('-')[1] + "</td>";
            let id = "<td> " + producto.split('-')[0].trim() + "</td>";
            let cant = "<td style=\" text-align: right\"> " + Cantidad + "</td>";
            let rinde = "<td style=\" text-align: right\">" + Rinde + "</td>";
            let tipo = producto.split('-')[2].trim();
            let costo = "<td style=\" text-align: right\">" + Costo + "</td>";
            let venta = "<td style=\" text-align: right\">" + Venta + "</td>";
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
                    "<td style=\" text-align: center\">" +
                    " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs \" onclick=\"javascript: return borrarProd('" + tipo + "_" + producto.split('-')[0].trim() + "_" + Cantidad.toString().trim() + "');\" >" +
                    "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> " +
                    btnrec + "</td > " +
                    "</tr>"
                );
                if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += producto.split('-')[0].trim() + "-" + tipo + "-" + Venta.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim() + "-" + Costo.toString().trim();
                }
                else {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + producto.split('-')[0].trim() + "-" + tipo + "-" + Venta.toString().trim() + "-" + Cantidad.toString().trim() + "-" + Rinde.toString().trim() + "-" + Costo.toString().trim();
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
                            }
                        }

                    }

                }
            }
            ContentPlaceHolder1_idProductosRecetas.value = nuevosProductos;
        }

    </script>


</asp:Content>
