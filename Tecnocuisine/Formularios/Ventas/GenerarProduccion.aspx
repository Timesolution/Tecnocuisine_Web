<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerarProduccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.GenerarProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                            <asp:TextBox runat="server" ID="txtDescripcionProductos" onfocusout="handle(event)" list="ContentPlaceHolder1_ListaNombreProd" class="form-control" Style="margin-left: 15px; width: 95%" />
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
                                                <asp:TextBox runat="server" onChange="ChangeTableCantidad()" ID="NCantidad" type="number" class="form-control" Style="margin-left: 15px; width: 70%" />
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
                                                <asp:TextBox runat="server" ID="NUnidadMedida" list="ContentPlaceHolder1_ListaDLLUnidadMedida" class="form-control" Style="margin-left: 15px; width: 70%" />
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
                                                    <asp:TextBox runat="server" ID="NCantidadProducida" type="number" class="form-control" Style="margin-left: 15px; width: 70%" />
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
                                                <th style="width: 5%; text-align: right;">Cantidad Necesaria</th>
                                                <th style="width: 5%; text-align: right;">Stock</th>
                                                <th style="width: 5%; text-align: right;">Unidad de Medida</th>
                                                <th style="width: 8%; text-align: center;">Cantidad Real</th>
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

                                <button class="btn btn-sm btn-primary pull-right m-t-n-xs" style="margin-right: 25px;" data-toggle="tooltip" data-placement="top" title data-original-title="Producir"
                                    text="Confirmar Venta" validationgroup="AgregarEntregas" id="btnGuardar" onclick="GenerarProduccion(event)">
                                    Generar Produccion</button>
                            </div>


                            <asp:HiddenField runat="server" ID="idProductosRecetas" />
                            <asp:HiddenField runat="server" ID="hiddenReceta" />
                            <asp:HiddenField runat="server" ID="HiddenRinde" />
                            <asp:HiddenField runat="server" ID="HiddenCosto" />
                            <asp:HiddenField runat="server" ID="HiddenCantidadAnterior" />
                            <asp:HiddenField runat="server" ID="HiddenPrecioVenta" />
                            <asp:HiddenField runat="server" ID="VerSiElProductoFueCargado"></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="ListaTablaFinal" />

                            <datalist id="ListaDLLMarca" runat="server">
                                <option>No tiene marca</option>
                            </datalist>
                            <datalist id="ListaDLLSector" runat="server">
                            </datalist>
                            <datalist id="ListaDLLPresentaciones" runat="server">
                            </datalist>
                            <datalist id="ListaDLLUnidadMedida" runat="server">
                            </datalist>
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
          let txtProd = document.getElementById("ContentPlaceHolder1_txtDescripcionProductos")

            if (txtProd.value != "") {
                let id = txtProd.value.split("-")[0].trim()
                PedirStockTotal(id);
            }
        });
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
                    AgregarATabla(respuesta.d)
                    PedirStockTotal(id)
                    
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
                            col.innerText = (Number(col.innerText) * divisor).toFixed(2);
                            cantidadNueva = Number(col.innerText);
                        } else {
                            col.innerText = (Number(arrCantGuardada[i - 1]) * divisor).toFixed(2);
                            cantidadNueva = Number(col.innerText);
                        }

                    }
                    if (j == 3) {
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
            document.getElementById('ContentPlaceHolder1_NRinde').value = response[2];
            document.getElementById('ContentPlaceHolder1_NRinde').disabled = true;
        }
    </script>
    <script>

        function PedirStockTotal(id) {
            let fa = document.getElementById("StockChange");
            fa.setAttribute("href", "/Formularios/Maestros/StockDetallado.aspx?t=2&i=" + id);
            $.ajax({
                method: "POST",
                url: "GenerarProduccion.aspx/GetStockRecetas",
                data: '{idProd: "' + id + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    stocktotal = respuesta.d
                    if (stocktotal == "") {
                        document.getElementById("StockDisponible").innerText = 0;

                    } else {
                    document.getElementById("StockDisponible").innerText = stocktotal;
                    }
                    
                }
            });
        }



        function AgregarATabla(list) {
            $('#tableProductos tbody')[0].innerHTML = "";
            let listArr = list.split(";");
            for (let i = 0; i < listArr.length; ++i) {
                if (listArr[i].length > 1) {

                    let item = listArr[i].split(",");
                    let id = item[0];
                    let name = item[1];
                    let cantNecesaria = item[2];
                    let stock = item[3]
                    let unidad = item[4];
                    let tipo = item[5];
                    let faReceta = "<td></td>";

                    
                    if (tipo == "Receta") {
                        faReceta = "<td> <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Producir_Receta href=GenerarProduccion.aspx?i=" + id + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-cutlery\"></i> </a>  </td>";
                    }

                    let Nombre = "<td> " + name + "</td>";
                    let ID = "<td style=\" text-align: right\"> " + id + "</td>";
                    let STOCK = stock > cantNecesaria ? "<td id=" + stock + "-" + name.replaceAll(" ", "") + "-" + id + " style=\" text-align: right;\"> " + stock + "</td>" : "<td id=" + stock + "-" + name.replaceAll(" ", "") + "-" + id + " style=\" text-align: right; color: red;\"> " + stock + "</td>"
                    let CantNecesaria = "<td style=\" text-align: left\"> " + cantNecesaria + "</td>";
                    let Unidad = "<td style=\" text-align: right\">" + unidad + "</td>";


                    if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + id + "," + cantNecesaria)) {
                        $('#tableProductos').append(
                            "<tr id=" + tipo + "," + id + "," + cantNecesaria + ">" +
                            ID +
                            Nombre +
                            CantNecesaria +
                            STOCK +
                            Unidad +
                            "<td style=\" text-align: center\">" +
                            "<input type=\"number\" style=\"withd 100%;\" placeholder=\"Ingresa la cantidad real que utilizaste\" /></td > " +
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

        function GenerarProduccion(event) {
            event.preventDefault();
            let btn = document.getElementById("btnGuardar")
            btn.disabled = true;
            let ListTablaFinal = document.getElementById("ContentPlaceHolder1_ListaTablaFinal");
            ListTablaFinal.value = "";
            var resume_table = document.getElementById("tableProductos");
            for (var i = 1, row; row = resume_table.rows[i]; i++) {
                let ListaAgregar = row.id
                let total = 0;
                let stock = 0;

                for (var j = 0, col; col = row.cells[j]; j++) {
                    //alert(col[j].innerText);
                    if (j == 2) {
                        total = col.innerText;
                    }

                    if (j == 3) {
                        stock = col.innerText;
                        if (Number(total) > Number(col.innerText)) {
                            //        toastr.error('No puedes tener un Stock menor a la Cantidad Necesaria')
                            ///*        return false*/
                        }
                    }

                    if (j == 5) {
                        console.log(col.childNodes[0].value)
                        if (col.childNodes[0].value == "" || col.childNodes[0].value <= 0) {
                            col2 = col.childNodes[0];
                            col2.style.color = "red";
                            //toastr.error('No puedes poner una Cantidad Real menor o igual a 0 ')
                            //return false
                            ListaAgregar += "," + col.childNodes[0].value;
                        }
                        else if (Number(stock) < Number(col.childNodes[0].value)) {
                            /*           toastr.error('No puedes poner una Cantidad Real mayor al stock disponible ')*/
                            col2 = col.childNodes[0];
                            col2.style.color = "red";
                            /* return false*/
                            ListaAgregar += "," + col.childNodes[0].value;
                        } else {
                            col2 = col.childNodes[0]
                            col2.style.color = "#676a6c";
                            ListaAgregar += "," + col.childNodes[0].value;
                        }
                    }
                }


                ListTablaFinal.value += ListaAgregar + ";"
            }
            Marca = document.getElementById("ContentPlaceHolder1_NMarca").value;
            Presentacion = document.getElementById("ContentPlaceHolder1_txtPresentacion").value;
            UnidadMedida = document.getElementById("ContentPlaceHolder1_NUnidadMedida").value;
            Sector = document.getElementById("ContentPlaceHolder1_NSector").value;
            Lote = document.getElementById("ContentPlaceHolder1_NLote").value;
            CantidadProducida = document.getElementById("ContentPlaceHolder1_NCantidadProducida").value;
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
                    + '" , CantidadProducida: "' + document.getElementById("ContentPlaceHolder1_NCantidadProducida").value
                    + '" , idReceta: "' + document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value.split("-")[0].trim()
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                error: (error) => {
                    toastr.error('Error al generar Produccion')
                    let btn = document.getElementById("btnGuardar")
                    btn.disabled = false;
                },
                success: (respuesta) => {
                    toastr.success('Produccion Generada con Exito');
                    window.location.href = "GenerarProduccion.aspx?m=1"
                }
            });
        }


    </script>


</asp:Content>
