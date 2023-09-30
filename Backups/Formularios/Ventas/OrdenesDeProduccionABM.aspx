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
                        <div class="row" style="padding-left: 15px;">
                            <div class="row">
                                <%-- Columna 1--%>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">OP numero</label>
                                        </div>
                                        <div class="col-md-8" runat="server">
                                            <%--<label id="lblProdNum" style="margin-left: 5%">OP numero</label>--%>
                                            <asp:Label ID="lblOPNumero" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 3%">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Cliente</label>
                                        </div>
                                        <div class="col-md-8">
                                            <datalist id="ListaNombreCliente" runat="server">
                                            </datalist>
                                            <asp:TextBox ID="TxtClientes"
                                                class="form-control num required" runat="server" list="ContentPlaceHolder1_ListaNombreCliente"
                                                Style="margin-left: 0px; width: 100%;" />
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 3%">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Fecha de entrega</label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group" id="data_1">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <%--Columna 2--%>
                                <div class="col-lg-6" style="margin-bottom: 20px;">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label>Producto</label>
                                            </div>

                                            <div class="col-md-6">
                                                <datalist id="ListaNombreProd" runat="server">
                                                </datalist>
                                                <asp:TextBox runat="server" ID="txtDescripcionProductos"
                                                    list="ContentPlaceHolder1_ListaNombreProd" class="form-control" Style="margin-left: 15px; width: 95%" />
                                                <asp:HiddenField ID="Hiddentipo" runat="server" />
                                                <asp:HiddenField ID="HiddenUnidad" runat="server" />

                                            </div>
                                            <div class="col-md-1" style="margin-top: 8px;">
                                                <a id="StockChange" target="_blank" data-toggle="tooltip" data-placement="top" title data-original-title="Stock" style="color: black;">
                                                    <h5 id="StockDisponible"></h5>
                                                </a>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 3%">
                                            <div class="col-md-4">
                                                <label>Cantidad</label>
                                                <%--<h5 style="margin-left: 5%">:</h5>--%>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox runat="server" ID="NCantidad" type="number" class="form-control" Style="margin-left: 15px; width: 70%;" />
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <button class="btn btn-sm btn-primary pull-right m-t-n-xs"
                                    style="margin-right: 55px; margin-bottom: 5px; margin-top: 5px;" data-toggle="tooltip" data-placement="top" data-original-title="Agregar productos"
                                    text="Confirmar Venta" validationgroup="AgregarEntregas" id="btnGuardar" onclick="agregarProductoPH(); return false;">
                                    Agregar productos</button>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <table class="table table-striped table-bordered table-hover " id="editable">
                                        <thead>
                                            <tr>
                                                <th style="width: 35%">Producto</th>                                                
                                                <th style="text-align: right; width: 25%; padding-right: 5px;">Cantidad</th>
                                                <th style="width: 5%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phRubroRecaudacionMensual" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <asp:LinkButton runat="server" class="btn btn-sm btn-primary pull-right m-t-n-xs"
                                Style="margin-right: 55px; margin-bottom: 5px; margin-top: 5px;" data-toggle="tooltip"
                                data-placement="top" data-original-title="Guardan orden de compra" Text="Guardar orden de produccion"
                                ValidationGroup="AgregarEntregas" ID="btnGuardarOrdenDeCompra" OnClick="btnGuardarOrdenDeCompra_Click">
                            </asp:LinkButton>


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




            $('#data_1 .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'dd/mm/yyyy'
            });



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

            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });

            let tabla = document.getElementById("editable");
            tabla.deleteRow(1);

        });



        $('.dataTables_filter').hide();
        $('#filter').on('keyup', function () {
            $('#dataTablesP-example').DataTable().search(
                this.value
            ).draw();
        });





        function agregarProductoPH() {


            let ProductoDescripcion = ContentPlaceHolder1_txtDescripcionProductos.value;
            let Cantidad = ContentPlaceHolder1_NCantidad.value;

            let txtClientes = document.getElementById("<%= TxtClientes.ClientID %>");
            let txtFechaHoy = document.getElementById("<%= txtFechaHoy.ClientID %>");

            VaciarInputs();
            GuardarInputs();


            txtClientes.disabled = true;
            txtFechaHoy.disabled = true;

            let idAutoincrementable = document.getElementById('<%= idAutoincrementable.ClientID %>').getAttribute('value');
            let ID = parseInt(idAutoincrementable);

            ID++;
            document.getElementById('<%= idAutoincrementable.ClientID %>').setAttribute('value', ID);
            document.getElementById('<%= DatosProductos.ClientID%>').value += "ID=" + ID + "," + ProductoDescripcion + "," + Cantidad + ";"
          
            let btneliminar = "<div style=\"display: flex; align-items: center;\">" +
                " <a style=\"padding: 0% 5% 2% 5.5%; background-color: transparent;\" class=\"btn btn-xs\" onclick=\"javascript: return borrarDocumentoSelect('" + ID + "');\" title=\"Eliminar\">" +
                "<i class=\"fa fa-trash-o\" style=\"color: black;\"></i> </a> " +
                "</div>";



            let ProductoDescripcionColumna = "<td> " + ProductoDescripcion + "</td>";
            let CantidadColumna = "<td> " + Cantidad + "</td>"
            let btneliminarColumna = "<td> " + btneliminar + "</td>"

            let appendfinal = "<tr id =" + ID + ">" +
                ProductoDescripcionColumna +
                CantidadColumna +
                btneliminarColumna +
                "</tr>";

            $('#editable').append(appendfinal);

            /*let stringifyData = '['*/
            //for (var i = 0; i < length; i++) 
            //{
            //    let data = {
            //        Producto: ProductoDescripcion,
            //        Cantidad: Cantidad,
            //        Accion: btneliminar
            //    }

            //    stringifyData += JSON.stringify(data) + ','

            //}
         
            //stringifyData = stringifyData.slice(0, -1) + ']'
            //let jsonData = JSON.parse(stringifyData)


            //let pEPITo = JSON.parse(data)
            //console.log(pEPITo)

            //$('#editable').DataTable({
            //    "bLengthChange": false,
            //    "bFilter": true,
            //    "bInfo": false,
            //    "bPaginate": false,
            //    destroy: true,
            //    data: jsonData,
            //    columns: [
            //        { data: 'Producto' },
            //        { data: 'Cantidad' },    
            //        { data: 'Accion' }
            //    ],
          
            //});


            //$('.dataTables_filter').hide();
            //$('#filter').on('keyup', function () {
            //    $('#editable').DataTable().search(
            //        this.value
            //    ).draw();
            //});

        }

        //Esta funcioo guarda los inputs cliente y fecha en variables de tipo textbox, para poder
        //recuperarlas en el backend 
        function GuardarInputs() {


            let txtClientes = document.getElementById("<%= TxtClientes.ClientID %>").value;
            document.getElementById("<%= Cliente.ClientID %>").value = txtClientes

            let txtFechaEntrega = document.getElementById("<%= txtFechaHoy.ClientID %>").value;
            document.getElementById("<%= fechaEntrega.ClientID %>").value = txtFechaEntrega


            //txtClientes.disabled = true;
            //txtFechaEntrega.disabled = true;
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


            let datosProductos = document.getElementById('<%=DatosProductos.ClientID%>').value;

            // Construye una expresión regular para buscar el ID y el contenido entre ID y ;
            let regex = new RegExp(`ID=${ID}[^;]*;`, 'g');

            // Reemplaza todas las coincidencias de la expresión regular con una cadena vacía
            let nuevoContenido = datosProductos.replace(regex, '');

            // Actualiza el valor del elemento con el nuevo contenido
            document.getElementById('<%=DatosProductos.ClientID%>').value = nuevoContenido;
        }

    </script>

</asp:Content>
