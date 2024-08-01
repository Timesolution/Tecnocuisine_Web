<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenesDeProduccionABM.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.OrdenesDeProduccionABM" %>

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
                        <div class="row" style="padding-left: 15px; padding-right: 15px; padding-bottom: 15px">

                            <div class="row" style="padding-left: 15px; padding-right: 15px">
                                <div class="col-md-4">
                                    <label>Número de orden: </label>
                                    <asp:Label ID="lblOPNumero" runat="server" Text=""></asp:Label>
                                </div>
                            </div>

                            <br />

                            <div class="row" style="padding-left: 15px; padding-right: 15px">
                                <%--Cliente--%>
                                <div class="col-md-3">
                                    <div>
                                        <label>Cliente</label>
                                    </div>
                                    <div>
                                        <datalist id="ListaNombreCliente" runat="server">
                                        </datalist>
                                        <asp:TextBox ID="TxtClientes"
                                            class="form-control num required" runat="server"
                                            list="ContentPlaceHolder1_ListaNombreCliente" />
                                        <p id="ValivaDescripcion"
                                            class="text-danger text-hide">
                                            Tienes que ingresar un cliente
                                        </p>
                                    </div>
                                </div>

                                <%--Fecha de entrega--%>
                                <div class="col-md-2">
                                    <div>
                                        <label>Fecha de entrega</label>
                                    </div>
                                    <div>
                                        <div class="form-group" id="data_1">
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox class="form-control" runat="server"
                                                    ID="txtFechaHoy"></asp:TextBox>
                                                <p id="ValidaFecha"
                                                    class="text-danger text-hide">
                                                    Tienes que ingresar una fecha
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <div class="row" style="padding-left: 15px; padding-right: 15px">

                                                                <div class="col-md-3">
                                    <div>
                                        <label>Producto/Receta</label>
                                    </div>

                                    <div>
                                        <datalist id="ListaNombreProd" runat="server">
                                        </datalist>
                                        <asp:TextBox runat="server" ID="txtDescripcionProductos"
                                            list="ContentPlaceHolder1_ListaNombreProd"
                                            class="form-control" />
                                        <p id="ValidaProducto"
                                            class="text-danger text-hide">
                                            Tienes que ingresar un producto
                                        </p>
                                        <asp:HiddenField ID="Hiddentipo" runat="server" />
                                        <asp:HiddenField ID="HiddenUnidad" runat="server" />

                                    </div>
                                    <div class="col-md-1" style="margin-top: 8px;">
                                        <a id="StockChange" target="_blank" data-toggle="tooltip" data-placement="top" title data-original-title="Stock" style="color: black;">
                                            <h5 id="StockDisponible"></h5>
                                        </a>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div>
                                        <label>Cantidad</label>
                                    </div>
                                    <div>
                                        <asp:TextBox runat="server" ID="NCantidad" type="number"
                                            class="form-control" min="1" />
                                        <p id="ValidaCantidad"
                                            class="text-danger text-hide">
                                            Tienes que ingresar una cantidad
                                        </p>
                                        <p id="ValidaCantidadMayorAcero"
                                            class="text-danger text-hide">
                                            La cantidad de la orden debe ser mayor a '0'
                                        </p>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div>
                                        <label style="visibility: hidden">Agregar</label>
                                    </div>
                                    <%-- Este es el boton para agregar una receta a la tabla de abajo --%>
                                    <button class="btn btn-md btn-primary m-t-n-xs"
                                        data-toggle="tooltip" data-placement="top"
                                        data-original-title="Agregar producto"
                                        text="Confirmar Venta" validationgroup="AgregarEntregas"
                                        id="btnGuardar" onclick="agregarProductoPH(); return false;" style="height: 100%; margin-top: 0">
                                        <i style="color: white" class="fa fa-check"></i>
                                    </button>
                                </div>
                            </div>


                            <%-- Aca empieza la tabla en la que se cargan las recetas para la orden de produccion --%>
                            <div class="row">
                                <div class="col-lg-11" style="margin-left: 10px">
                                    <table class="table table-striped table-bordered table-hover" id="editable">
                                        <thead>
                                            <tr>
                                                <th style="width: 30%">Producto</th>
                                                <th style="text-align: right; width: 10%; padding-right: 5px;">Cantidad</th>
                                                <th style="width: auto"></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableOrdenesProduccion">
                                            <asp:PlaceHolder ID="phRecetasOrdenProduccion" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%-- Aca termina la tabla --%>

                            <%-- Este es el boton para guardar la orden de produccion --%>
                            <asp:LinkButton runat="server" class="btn btn-sm btn-primary pull-right m-t-n-xs"
                                Style="margin-right: 145px; margin-bottom: 5px; margin-top: 5px;" data-toggle="tooltip"
                                data-placement="top" data-original-title="Guardan orden de producción" Text="Guardar orden de producción"
                                ValidationGroup="AgregarEntregas" ID="btnGuardarOrdenDeCompra" OnClientClick="guardarOrden(); return false;">
                            </asp:LinkButton>

                            <%-- Estas son textboxOcultas que se usan para varias cosas --%>
                            <asp:TextBox runat="server" ID="DatosProductos" Text="" Style="display: none;" />
                            <asp:TextBox runat="server" ID="idAutoincrementable" Text="0" Style="display: none;" />
                            <asp:TextBox runat="server" ID="Cliente" Text="" Style="display: none;" />
                            <asp:TextBox runat="server" ID="fechaEntrega" Text="" Style="display: none;" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });

            //Este codigo es para que el input txtFechaHoy tenga un datepicker 
            $('#data_1 .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'dd/mm/yyyy'
            });


            //Esto desactiva el filtro que tiene por defecto la tabla 
            $("#editable_filter").css('display', 'none');

            //Este codigo es para la barra buscadora dinamica
            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });



        });



        $('.dataTables_filter').hide();
        $('#filter').on('keyup', function () {
            $('#dataTablesP-example').DataTable().search(
                this.value
            ).draw();
        });



        function guardarOrden() {
            // Contar filas de la tabla
            let rowCountTable = $("#editable tbody tr").length;

            // Contar filas del PlaceHolder
            let rowCountPlaceHolder = $("#phRecetasOrdenProduccion tr").length;

            // Sumar las filas de la tabla y del PlaceHolder
            let totalRowCount = rowCountTable + rowCountPlaceHolder;

            let Cliente = document.getElementById("<%= Cliente.ClientID %>").value


            console.log("Cantidad total de filas: " + totalRowCount);

            //if(totalRowCount > 0){
            let url = window.location.href;
            if (!url.includes("Accion=2")) {
                console.log("Se esta agregando");
                let OrdenNumero = document.getElementById("<%= lblOPNumero.ClientID %>").innerHTML;

                let textoLabel = OrdenNumero.innerText;

                let fechaEntrega = document.getElementById("<%= fechaEntrega.ClientID %>").value

                console.log(typeof fechaEntrega);

                let DatosProductos = document.getElementById("<%= DatosProductos.ClientID %>").value

                if (totalRowCount > 0) {
                    fetch('OrdenesDeProduccionABM.aspx/btnGuardarOrdenDeCompra_Click', {
                        method: 'POST',
                        body: JSON.stringify({ OrdenNumero: OrdenNumero, fechaEntrega: fechaEntrega, Cliente: Cliente, DatosProductos: DatosProductos }),
                        headers: { 'Content-Type': 'application/json' }
                    })
                        .then(response => response.json())
                        .then(data => {
                            window.location.href = "OrdenesDeProduccion.aspx";
                        })
                        .catch(error => {
                            console.error('Error:', error);
                        });
                }

                else {
                    console.log("No se pudo guardar la orden")
                }


            }

        }



        //Esta es la funcion que se ejecuta al hacer click en el boton btnGuardar, 
        //lo que hace es agregar la receta en la tabla
        function agregarProductoPH() {

            var i = ValidarForm();
            if (i == false) {
                return false
            }

            let ProductoDescripcion = ContentPlaceHolder1_txtDescripcionProductos.value;
            let Cantidad = ContentPlaceHolder1_NCantidad.value;

            let txtClientes = document.getElementById("<%= TxtClientes.ClientID %>");
            let txtFechaHoy = document.getElementById("<%= txtFechaHoy.ClientID %>");

            VaciarInputs(); //Esta funcion vacia los inputs cada vez que se agrega alguna receta a la tabla
            GuardarInputs();

            txtClientes.disabled = true;
            txtFechaHoy.disabled = true;

            let idAutoincrementable = document.getElementById('<%= idAutoincrementable.ClientID %>').getAttribute('value');
            let ID = parseInt(idAutoincrementable);

            ID++;
            document.getElementById('<%= idAutoincrementable.ClientID %>').setAttribute('value', ID);
            document.getElementById('<%= DatosProductos.ClientID%>').value += "ID=" + ProductoDescripcion + "," + Cantidad + ";"


            var numeroInicial = ProductoDescripcion.match(/^\d+/);
            numeroInicial = parseInt(numeroInicial[0], 10);

            //console.log(numeroInicial)


            let btneliminar = "<div style=\"display: flex; align-items: center;\">" +
                " <a style=\"padding: 0% 5% 2% 5.5%; background-color: transparent; margin-left: 3px;\" class=\"btn btn-xs\" onclick=\"javascript: return borrarDocumentoSelect('" + "ContentPlaceHolder1_" + numeroInicial + "');\" title=\"Eliminar\">" +
                "<i class=\"fa fa-trash-o\" style=\"color: red;\"></i> </a> " +
                "</div>";




            let ProductoDescripcionColumna = "<td> " + ProductoDescripcion + "</td>";
            let CantidadColumna = "<td style='text-align: right;'>" + Cantidad + "</td>";
            let btneliminarColumna = "<td> " + btneliminar + "</td>"



            let appendfinal = "<tr id=" + "ContentPlaceHolder1_" + numeroInicial + ">" +
                ProductoDescripcionColumna +
                CantidadColumna +
                btneliminarColumna +
                "</tr>";

            $('#tableOrdenesProduccion').append(appendfinal);



        }

        //Esta funcioo guarda los inputs cliente y fecha en variables de tipo textbox, para poder
        //recuperarlas en el backend 
        function GuardarInputs() {


            let txtClientes = document.getElementById("<%= TxtClientes.ClientID %>").value;
            document.getElementById("<%= Cliente.ClientID %>").value = txtClientes

            let txtFechaEntrega = document.getElementById("<%= txtFechaHoy.ClientID %>").value;
            document.getElementById("<%= fechaEntrega.ClientID %>").value = txtFechaEntrega

        }

        //Esta funcion vacia los inputs cantidad y producto 
        function VaciarInputs() {
            document.getElementById("<%= txtDescripcionProductos.ClientID %>").value = "";
            document.getElementById("<%= NCantidad.ClientID %>").value = "";
        }

        function contarfilasObtenerGenerarId() {
            // Obtener el número de filas actuales en la tabla
            var rowCount = $('#editable tbody tr').length;

            // Generar un nuevo ID basado en el número de filas
            var CantFilas = rowCount;
            return CantFilas;
        }

        function borrarDocumentoSelect(ID) {
            $('#' + ID).remove();


            //let tabla = document.getElementById("editable");
            let txtClientes = document.getElementById("<%= TxtClientes.ClientID %>");
            let txtFechaHoy = document.getElementById("<%= txtFechaHoy.ClientID %>");

            borrarDatosProductoDeCadena(ID);
            let CantFilas = contarfilasObtenerGenerarId()



            if (CantFilas > 0) {
                txtClientes.disabled = true;
                txtFechaHoy.disabled = true;
            }

            else {
                // Si la tabla no tiene filas, habilitar el TextBox
                txtClientes.disabled = false;
                txtFechaHoy.disabled = false;
            }

        }


        function borrarDatosProductoDeCadena(ID) {


            var numero;
            var match = ID.match(/ContentPlaceHolder1_(\d+)/);
            ID = "ID=" + match[1];

            console.log(numero)

            var Recetas = document.getElementById('<%=DatosProductos.ClientID%>').value.split(';');
            var nuevasRecetas = "";
            for (var x = 0; x < Recetas.length; x++) {
                if (Recetas[x] != "") {
                    if (!Recetas[x].includes(ID)) {
                        nuevasRecetas += Recetas[x] + ";";
                        console.log("Entre al if")
                    }
                    else {

                    }


                }


            }
            document.getElementById('<%=DatosProductos.ClientID%>').value = nuevasRecetas
            console.log(document.getElementById('<%=DatosProductos.ClientID%>').value)

        }


        function ValidarForm() {
            let formValido = true

            // Descripcion
            let des = document.getElementById('<%=TxtClientes.ClientID%>')
            if (des.value.length < 1) {
                document.getElementById('ValivaDescripcion').className = 'text-danger'
                formValido = false;
            } else {
                document.getElementById('ValivaDescripcion').className = "text-danger text-hide"
            }

<%--      let fecha = document.getElementById('<%=/txtFechaHoy.ClientID%>')
      if (fecha.value.length < 1) {
          document.getElementById('Validafecha').className = 'text-danger'
          return false
      } else {
          document.getElementById('Validafecha').className = "text-danger text-hide"
      }--%>


            let Pro = document.getElementById('<%=txtDescripcionProductos.ClientID%>')
            if (Pro.value.length < 1) {
                document.getElementById('ValidaProducto').className = 'text-danger'
                formValido = false;
            } else {
                document.getElementById('ValidaProducto').className = "text-danger text-hide"
            }

            let cant = document.getElementById('<%=NCantidad.ClientID%>')
            if (cant.value.length < 1) {
                document.getElementById('ValidaCantidad').className = 'text-danger'
                formValido = false;
            } else {
                document.getElementById('ValidaCantidad').className = "text-danger text-hide"
            }


            if (cant.value <= 0) {
                document.getElementById('ValidaCantidadMayorAcero').className = 'text-danger'
                formValido = false;
            } else {
                document.getElementById('ValidaCantidadMayorAcero').className = "text-danger text-hide"
            }

            return formValido;

        }

    </script>

    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText = "Produccion / Nueva Orden";
        });
    </script>

</asp:Content>
