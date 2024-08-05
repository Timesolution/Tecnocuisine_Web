<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CronogramaProduccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.CronogramaProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />--%>
    <style>
        .icon-button {
            border: none;
            background: transparent;
            padding: 0; /* Opcional: para eliminar cualquier espacio adicional */
        }
    </style>

    <%-- Este es el primer widget en el que se muestra el numero de orden de produccion --%>
    <div class="row" style="padding-left: 14px; padding-right: 14px">
        <!-- Utiliza col-lg-8 para la parte izquierda -->
        <div class="ibox-content m-b-sm border-bottom">
            <div class="row">
                <div class="col-lg-6" style="height: 100%; border-right: 1px solid #ccc;">
                    <div class="p-xs">
                        <div class="pull-left m-r-md">
                            <i class="fa fa-cutlery text-navy mid-icon"></i>
                        </div>
                        <h2 id="carpetaNumero">Orden de producción</h2>
                        <span id="caratula">
                            <asp:Label ID="lblIdsQueryString" runat="server" Text=""></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="pull-left m-r-md">
                        <i class="fa fa-cutlery text-navy mid-icon"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Aca termina el primer widget --%>


    <%if (sectorTables != null)
        {

            foreach (var keyFecha in sectorTables)
            {
                foreach (System.Data.DataRow col3 in keyFecha.Value.DefaultView.Table.Rows)
                {
                    fechasHead.Add(col3["fechaProducto"].ToString());
                }
            }


            List<string> fechasOrdenadasList = fechasHead.ToList();
            fechasOrdenadasList.Sort((x, y) => DateTime.Parse(x).CompareTo(DateTime.Parse(y)));
            fechasHead.Clear();
            foreach (var fecha in fechasOrdenadasList)
            {
                fechasHead.Add(fecha);
            }


            //De aca en adelante, se van a generar widgets de manera dinamica 
            foreach (var kvp in sectorTables)
            {%>
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5><%= kvp.Key.ToString()%></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            <li><a href="#">Config option 1</a>
                            </li>
                            <li><a href="#">Config option 2</a>
                            </li>
                        </ul>
                    </div>

                </div>

                <div class="ibox-content" style="display: none;">
                    <table class="table table-hover table-bordered" style="padding-left: 10px">
                        <thead>
                            <tr>
                                <%foreach (var item in fechasHead)
                                    {%>
                                <td><strong><%=item.ToString() %></strong></td>
                                <%} %>
                            </tr>
                        </thead>
                        <tbody>
                            <% sectorTablesGroupByFechas = new Dictionary<string, System.Data.DataTable>();%>
                            <%foreach (System.Data.DataRow col1 in kvp.Value.DefaultView.Table.Rows)
                                {
                                    string sector = col1["fechaProducto"].ToString();

                                    if (!sectorTablesGroupByFechas.ContainsKey(sector))
                                    {
                                        System.Data.DataTable newTable = dtGlobal.Clone();
                                        newTable.TableName = sector;
                                        sectorTablesGroupByFechas.Add(sector, newTable);
                                    }

                                    sectorTablesGroupByFechas[sector].ImportRow(col1);
                                }%>

                            <%cantMax = 0;%>
                            <%foreach (var key in sectorTablesGroupByFechas)
                                {
                                    if (key.Value.Rows.Count > cantMax)
                                    {
                                        cantMax = key.Value.Rows.Count;
                                    }
                                }%>


                            <%for (int i = 0; i < cantMax; i++)
                                {%>
                            <tr>
                                <%foreach (var item in fechasHead)
                                    {%>
                                <td>
                                    <div style="display:flex">
                                        <%foreach (var key2 in sectorTablesGroupByFechas)
                                            {%>
                                        <%if (key2.Key == item)
                                            {
                                                if (key2.Value.Rows.Count >= i + 1)
                                                {
                                                    object obj = key2.Value.Rows[i]["descripcion"];
                                                    object objUnidadMedida = key2.Value.Rows[i]["unidadMedida"];
                                        %>

                                        <div style="width: 35%; white-space: normal;">
                                            <%= obj.ToString()%>
                                        </div>


                                        <%decimal cant = 0;%>
                                        <%if (key2.Value.Rows[i]["cantidad"].ToString().Contains(";"))
                                            {
                                                string cantidad = key2.Value.Rows[i]["cantidad"].ToString();
                                                string[] partes = cantidad.Split(';');
                                                for (int j = 0; j < partes.Length; j++)
                                                {
                                                    //cant += decimal.Parse(partes[j]);
                                                    decimal value;
                                                    if (decimal.TryParse(partes[j], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out value))
                                                    {
                                                        cant += value;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                cant = Convert.ToDecimal(key2.Value.Rows[i]["cantidad"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                                            }

                                        %>

                                        <div style="width: 15%;">
                                            <strong style="text-align: right; display: inline-block;"><%= cant %></strong>
                                        </div>


                                        <div style="width: 30%;">
                                            <p style="text-align: right; display: inline-block;"><%= objUnidadMedida.ToString() %></p>
                                        </div>


                                        <div style="width: 20%; justify-self: end">
                                            <%if (key2.Value.Rows[i]["ingredienteOreceta"].ToString() == "Receta")
                                                {%>
                                            <button id="btnVerIngredientes" type="button" class="icon-button" style="float: right; margin-left: 10px;"
                                                title="Ver ingredientes" onclick="verIngredientes('<%=key2.Value.Rows[i]["idProductoOReceta"].ToString()%>')">
                                                <i class="fa fa-cutlery" style="float: right;"></i>
                                            </button>
                                            <%} %>

                                            <button id="btn" type="button" class="icon-button" style="float: right; margin-left: 10px;"
                                                title="OrigenDestino" onclick="verOrigenYDestino('<%=key2.Value.Rows[i]["descripcion"].ToString()%>', 
                                        '<%=key2.Value.Rows[i]["cantidad"].ToString()%>', '<%=key2.Value.Rows[i]["sectorProductivo"].ToString()%>', 
                                        '<%=key2.Value.Rows[i]["Column1"].ToString()%>', '<%=key2.Value.Rows[i]["CantidadPadre"].ToString()%>', 
                                        '<%=key2.Value.Rows[i]["sectorPadre"].ToString()%>', '<%=key2.Value.Rows[i]["OPNumero"].ToString() %>',
                                        '<%=key2.Value.Rows[i]["RazonSocial"].ToString() %>' )">
                                                <i class="fa fa-exchange"></i>
                                            </button>




                                            <a href="/Formularios/Maestros/StockDetallado.aspx?t=1&i=<%=key2.Value.Rows[i]["idProductoOReceta"].ToString()%>"
                                                target="_blank" style="float: right; margin-left: 10px;"
                                                title="Ver stock">
                                                <i style="color: black" class="fa fa-list-alt"></i>
                                            </a>

                                        </div>

                                        <%}%>
                                        <%}%>
                                        <%}%>
                                    </div>
                                </td>

                                <%}%>
                            </tr>
                            <%}%>
                        </tbody>
                    </table>
                </div>

            </div>
        </div>
    </div>
    <%}%>


    <asp:LinkButton runat="server" ID="btnCancelar" class="btn btn-primary dim" Style="float: right; margin-left: 1%" Text="Cancelar"
        OnClick="btnCancelar_Click">
    </asp:LinkButton>

    <asp:LinkButton runat="server" ID="btnProducir"
        class="btn btn-primary dim" Style="float: right" Text="A producir" OnClientClick="Producir(); 
        return false;">
    </asp:LinkButton>
    <%}%>







    <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
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
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Producto</strong></td>
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




    <div id="modalOrigenDestino" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 60%; height: 60%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Origen/Destino</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel6">
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
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Producto</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                    <td><strong>Producto Destino</strong></td>
                                                    <td><strong>SectorDestino</strong></td>
                                                    <td><strong>Orden destino</strong></td>
                                                    <td><strong>Cliente destino</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tableOrigen">
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


    <div id="modalVerIngredientesReceta" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 40%; height: 50%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Ingredientes</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel4">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Ingredientes de la receta</h5>
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
                                                    <td><strong>Producto</strong></td>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tableIngredientres">
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

    <script>
        $(document).ready(function () {

            var oTable = $('#editable').dataTable({
                "bLengthChange": false,
                "pageLength": 100, // Establece la cantidad predeterminada de registros por página
                "lengthMenu": [25, 50, 87, 100], // Opciones de cantidad de registros por página
            });


            //Codigo para que se minimize el widget

            // Itera a través de los widgets y llama a la función toggleWidget según el estado inicial
            $(".ibox").each(function () {
                var $widget = $(this);
                var isCollapsed = $widget.data("collapsed"); // Obtiene el estado inicial desde el atributo de datos

                if (isCollapsed) {
                    toggleWidget($widget.attr("id")); // Llama a toggleWidget para colapsar widgets iniciales
                }
            });



            $("#editable_filter").css('display', 'none');



        });


        //Mas codigo para que se minimice el widget
        function toggleWidget(widgetId) {
            var $widget = $("#" + widgetId); // Encuentra el widget por su ID
            var $content = $widget.find(".ibox-content"); // Encuentra el contenido del widget

            // Toggle para mostrar/ocultar el contenido del widget
            $content.slideToggle();
        }

        function verIngredientes(idReceta) {
            console.log(idReceta)
            fetch('CronogramaProduccion.aspx/getIngredientesRecetaByid', {
                method: 'POST',
                body: JSON.stringify({ idReceta: idReceta }),
                headers: { 'Content-Type': 'application/json' },
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data.d)
                    document.getElementById('tableIngredientres').innerHTML = "";
                    const ingredientesArray = data.d.split(';').filter(Boolean);
                    ingredientesArray.forEach(ingredientes => {

                        const campos = ingredientes.split(',').filter(Boolean);
                        const descripcion = campos[0];
                        const sectorProductivo = campos[1];
                        const cantidad = campos[2];

                        let plantillaIngredientes = `
                    <tr>
                        <td>${descripcion}</td>
                        <td>${sectorProductivo}</td>
                        <td style="text-align: right;">${cantidad}</td>
                    </tr>
                   `;
                        document.getElementById('tableIngredientres').innerHTML += plantillaIngredientes;
                        $('#modalVerIngredientesReceta').modal('show');
                    });
                })
                .catch(error => {
                    // Manejo de errores aquí si es necesario
                    console.error('Error:', error);
                });
        }


        function MostrarRecepcion(cantidad, id, nivel, DiasMasNivel, descripcionReceta, idReceta, dt) {
            nivel++;
            console.log(dt)
            fetch('CronogramaProduccion.aspx/getProductoByidReceta', {
                method: 'POST',
                body: JSON.stringify({ id: id, nivel: nivel }),
                headers: { 'Content-Type': 'application/json' },
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    //let Productos = JSON.parse(data.d)

                    const productosArray = data.d.split(';');
                    document.getElementById('tablaRecepcion').innerHTML = "";
                    // document.getElementById('tablaEntrega').innerHTML = "";
                    productosArray.forEach(producto => {
                        // Dividir cada elemento en valores individuales usando coma como separador
                        const productoData = producto.split(',');

                        // Ahora puedes acceder a valores específicos del producto
                        const productoId = productoData[0];
                        const sectorProductivo = productoData[1];
                        const productoNombre = productoData[2];
                        const dias = productoData[3];
                        const nivel = productoData[4];
                        const diasMasNivel = productoData[5];
                        const descripcion = productoData[6];
                        const cantidad = productoData[7];

                        if (diasMasNivel - DiasMasNivel == 1) {
                            const table = document.getElementById('tablaRecepcion');
                            let plantillaIngredientes = `
                                <tr>
                                    <td>${sectorProductivo}</td>
                                    <td>${productoNombre}</td>
                                    <td style="text-align: right;">${productoData[7]}</td>
                                </tr>
                            `;
                            document.getElementById('tablaRecepcion').innerHTML += plantillaIngredientes;
                        }


                    });
                })
                .catch(error => {
                    // Manejo de errores aquí si es necesario
                    console.error('Error:', error);
                });

            fetch('CronogramaProduccion.aspx/getSectorProductivoByIdReceta', {
                method: 'POST',
                body: JSON.stringify({ idReceta: idReceta }),
                headers: { 'Content-Type': 'application/json' }
            })
                .then(response => response.json())
                .then(data => {
                    const tablaEntrega = document.getElementById('tablaEntrega');
                    const [sectorProductivo, cantidad] = data.d.split(';');
                    let plantillaReceta = `
                             <tr>
                                 <td>${sectorProductivo}</td>
                                 <td>${descripcionReceta}</td>
                                 <td style="text-align: right;">${cantidad}</td>
                             </tr>
                         `;
                    document.getElementById('tablaEntrega').innerHTML = plantillaReceta;

                    setTimeout(function () {
                        $('#modalBusqueda').modal('show');
                    }, 300);

                })
                .catch(error => {
                    // Código para manejar errores aquí
                    console.error('Error:', error);
                });


        }
        function cambiarEstadoAPediente() {
            //let url = window.location.href;
            const valores = window.location.search;
            // console.log(valores);

            //Creamos la instancia
            const urlParams = new URLSearchParams(valores);
            // console.log(urlParams);

            let id = urlParams.get('ids');
            //console.log(id);


            if (id && id.endsWith(',')) {
                id = id.slice(0, -1);
            }

            let estadoOrden = 2;
            // console.log(id);

            fetch('CronogramaProduccion.aspx/cambiarEstadoDeLaOrden', {
                method: 'POST',
                body: JSON.stringify({}),
                headers: { 'Content-Type': 'application/json' },
            })
                .then(response => response.json())
                .then(data => {

                })
                .catch(error => {
                    // Manejo de errores aquí si es necesario
                    console.error('Error:', error);
                });
        }

        function Producir() {
            const valores = window.location.search;

            const urlParams = new URLSearchParams(valores);

            let ids = urlParams.get('ids');


            if (ids && ids.endsWith(',')) {
                ids = ids.slice(0, -1);
            }

            let estadoOrden = 1;
            fetch('CronogramaProduccion.aspx/cambiarEstadoDeLaOrden', {
                method: 'POST',
                body: JSON.stringify({ ids: ids, estadoOrden: estadoOrden }),
                headers: { 'Content-Type': 'application/json' },
            })
                .then(response => response.json())
                .then(data => {
                    window.location.href = "OrdenesDeProduccion.aspx"
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        }


        function verOrigenYDestino(ProductoOrigen, cantidadOrigen, SectorOrigen, productoPadre, cantidadPadre, sectorPadre, OPNumero, razonSocial) {

            document.getElementById('tableOrigen').innerHTML = "";
            if (productoPadre.includes(";")) {

                const arrayProductosDestino = productoPadre.split(";");
                const arrayOPNumero = OPNumero.split(";");
                const arrayCantidadOrigen = cantidadOrigen.split(";");
                const arrayrazonSocial = razonSocial.split(";");

                arrayProductosDestino.forEach((producto, index) => {
                    const orden = arrayOPNumero[index] || '';
                    const cantidadOrigenActual = arrayCantidadOrigen[index] || '';
                    const razonSocialActual = arrayrazonSocial[index] || '';
                    let plantillaIngredientesOrigenDestino = `
                    <tr>
                        <td>${SectorOrigen}</td>
                        <td>${ProductoOrigen}</td>
                        <td style="text-align: right;">${cantidadOrigenActual}</td>
                        <td>${producto}</td>
                        <td>${sectorPadre}</td>
                        <td>${orden}</td>
                        <td>${razonSocialActual}</td>
                    </tr>
                    `;
                    document.getElementById('tableOrigen').innerHTML += plantillaIngredientesOrigenDestino;
                });
            }
            else {
                let plantillaIngredientesOrigenDestino = `
                       <tr>
                           <td>${SectorOrigen}</td>
                           <td>${ProductoOrigen}</td>
                           <td style="text-align: right;">${cantidadOrigen}</td>
                           <td>${productoPadre}</td>
                           <td>${sectorPadre}</td>
                           <td>${OPNumero}</td>
                           <td>${razonSocial}</td>
                       </tr>
                       `;
                document.getElementById('tableOrigen').innerHTML += plantillaIngredientesOrigenDestino;

            }
            $('#modalOrigenDestino').modal('show');
        }

    </script>

    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText = "Produccion / Cronograma";
        });
    </script>
</asp:Content>
