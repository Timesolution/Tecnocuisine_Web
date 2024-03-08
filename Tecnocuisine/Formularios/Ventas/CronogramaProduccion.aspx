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
<%--                    <%foreach (System.Data.DataRow dtCantidadXProducto in dtCantidadRecetasPorCadaOrden.Rows)
                        {%>
                    <h4><%= dtCantidadXProducto["Producto"].ToString() %> :  <%= dtCantidadXProducto["cantidad"].ToString() %></h4>
                    <% } %>--%>
                </div>
            </div>
        </div>
    </div>
    <%-- Aca termina el primer widget --%>



    <%-- De aca en adelante, se van a generar widgets de manera dinamica --%>
    <%-- Este va a ser el primer widget que se cree --%>
    <%
        int PrimerVuelta = 0; //Esta variable la uso como bandera para saber si estoy en la primera vuelta del foreach

        string SectorActual = null;
        int widgetCount = 0; // Variable para contar widgets
        int SegundoWiggetEnAdelante = 0; // Variable para contar widgets

        //Este foreach recorre una datatable que tiene productos ordenados por sector productivo, y va a crear un
        //widget por cada sector distinto
        foreach (System.Data.DataRow dr in dt.Rows)
        {
            widgetCount++; // Incrementa el contador de widgets
            bool isCollapsed = widgetCount > 0;
            if (PrimerVuelta == 0)
            {
                SectorActual = dr["SectorProductivo"].ToString().ToUpper();
                PrimerVuelta++;

    %>

    <%-- A partir de aca en adelante va a crear widgets de manera dinamica --%>
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins" id="widget<%= SegundoWiggetEnAdelante %>" data-collapsed="<%= isCollapsed.ToString().ToLower() %>">
                <div class="ibox-title">
                    <h5><%= dr["SectorProductivo"].ToString().ToUpper() %></h5>

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

                <div class="ibox-content">
                    <table class="table table-hover no-margins table-bordered">
                        <thead>
                            <tr>
                                <%-- Este for crea las columnas de la cabecera --%>
                                <%for (int i = 6; i > 0; i--)
                                    {%>
                                <%DateTime fecha = fechaOrdenDeProduccion.AddDays(-i); %>
                                <td><strong><%= fecha.ToString("dd/MM/yyyy") %></strong></td>
                                <%  } %>
                                <%-- Esta es la ultima columna de la cabecera, y ademas es la fecha de la orden de produccion --%>
                                <td><strong><%= fechaOrdenDeProduccion.ToString("dd/MM/yyyy") %></strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <%
                                List<int> listaDeNumeros = new List<int>();
                                int DiasActual = -1;
                                int Flag = 0;
                                int countRows = 0;
                                int CantidadDeDiasPorgrupo = 0;
                            %>
                            <%
                                foreach (System.Data.DataRow dataRow in dt.Rows)
                                {
                                    if (dataRow["SectorProductivo"].ToString().ToUpper() == SectorActual)
                                    {
                                        countRows++;
                                    }
                                    listaDeNumeros.Add(countRows);
                                }
                            %>
                            <%
                                listaDeNumeros.Sort((a, b) => -a.CompareTo(b));
                                int CantFilas = listaDeNumeros.Count > 0 ? listaDeNumeros[0] : 0;
                                string[,] matriz = new string[CantFilas, 7];
                                for (int i = 0; i < CantFilas; i++)
                                {
                                    for (int j = 0; j < 7; j++)
                                    {
                                        matriz[i, j] = "-"; // Inicializa cada posición con un espacio en blanco
                                    }
                                }
                            %>
                            <%
                                int filas = 0;
                                int DiasAct = -1;
                                int Bandera = 0;
                                foreach (System.Data.DataRow dataRow in dt.Rows)
                                {

                                    if (dataRow["SectorProductivo"].ToString() == SectorActual)
                                    {
                                        if (Bandera == 0)
                                        {
                                            Bandera++;
                                            DiasAct = Convert.ToInt32(dataRow["DiasMasNivel"].ToString());
                                            matriz[filas, 6 - Convert.ToInt32(dataRow["DiasMasNivel"].ToString())] = dataRow["ProductoOReceta"].ToString() + ";" + dataRow["Descripcion"].ToString() + ";" + dataRow["id"].ToString() + ";" + dataRow["Cantidad"].ToString() + ";" + dataRow["Nivel"].ToString() + ";" + dataRow["DiasMasNivel"].ToString() + ";" + dataRow["descripcionReceta"].ToString() + ";" + dataRow["idReceta"].ToString();
                                            
                                        }

                                        else
                                        {
                                            if (Convert.ToInt32(dataRow["DiasMasNivel"].ToString()) != DiasAct)
                                            {
                                                DiasAct = Convert.ToInt32(dataRow["DiasMasNivel"].ToString());
                                                filas = 0;
                                            }
                                            else
                                            {
                                                filas++;
                                            }
                                            matriz[filas, 6 - Convert.ToInt32(dataRow["DiasMasNivel"].ToString())] = dataRow["ProductoOReceta"].ToString() + ";" + dataRow["Descripcion"].ToString() + ";" + dataRow["id"].ToString() + ";" + dataRow["Cantidad"].ToString() + ";" + dataRow["Nivel"].ToString() + ";" + dataRow["DiasMasNivel"].ToString() + ";" + dataRow["descripcionReceta"].ToString() + ";" + dataRow["idReceta"].ToString();
                                            
                                        }


                                    }
                                }
                            %>
                            <% 
                                int filasMatriz = matriz.GetLength(0);
                                int ColumnasMatriz = matriz.GetLength(1);

                                for (int fila = 0; fila < filasMatriz; fila++)
                                {%>
                            <tr>
                                <%  for (int columna = 0; columna < ColumnasMatriz; columna++)
                                    {%>
                                <% string valor = matriz[fila, columna];%>
                                <% 
                                    string[] partes = valor.Split(';'); // Dividir el valor en dos partes
                                    string productoOReceta = partes[0]; // Guardar la primera parte
                                %>
                                <td><%= productoOReceta%>
                                    <%if (productoOReceta != "-")
                                        {
                                            string segundaParte = partes[1];
                                            string id = partes[2];
                                            if (segundaParte == "Receta")
                                            {
                                    %>
                                    <a href="GenerarProduccion.aspx" target="_blank" style="float: right; margin-left: 10px;"
                                        title="Produccion">
                                        <i class="fa fa-cutlery" style="color: black"></i></a>
                                    <% }%>
                                    <a href="/Formularios/Maestros/StockDetallado.aspx?t=1&i=<%=id %>" target="_blank" style="float: right; margin-left: 10px;"
                                        title="Ver stock">
                                        <i style="color: black" class="fa fa-list-alt"></i></a>

                                    <%} %>
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


    <%

        }


        if (dr["SectorProductivo"].ToString().ToUpper() != SectorActual)
        {
            SectorActual = dr["SectorProductivo"].ToString().ToUpper();

    %>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins" id="widget<%= widgetCount %>" data-collapsed="<%= isCollapsed.ToString().ToLower() %>">
                <!-- Agregar la clase "collapsed" aquí -->
                <div class="ibox-title">
                    <h5><%= dr["SectorProductivo"].ToString().ToUpper() %></h5>
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

                                <%for (int i = 6; i > 0; i--)
                                    {%>
                                <%DateTime fecha = fechaOrdenDeProduccion.AddDays(-i); %>
                                <td><strong><%= fecha.ToString("dd/MM/yyyy") %></strong></td>
                                <%  } %>
                                <td><strong><%= fechaOrdenDeProduccion.ToString("dd/MM/yyyy") %></strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <%List<int> listaDeNumeros = new List<int>(); %>

                            <%int DiasActual = -1; %>
                            <%int Flag = 0; %>
                            <%int countRows = 0; %>
                            <%int CantidadDeDiasPorgrupo = 0; %>

                            <%foreach (System.Data.DataRow dataRow in dt.Rows)
                                {
                                    if (dataRow["SectorProductivo"].ToString().ToUpper() == SectorActual)
                                    {
                                        countRows++;
                                    }
                                }

                                listaDeNumeros.Add(countRows);

                            %>

                            <%listaDeNumeros.Sort((a, b) => -a.CompareTo(b)); %>

                            <%int CantFilas = listaDeNumeros.Count > 0 ? listaDeNumeros[0] : 0; %>

                            <%string[,] matriz = new string[CantFilas, 7]; %>
                            <%for (int i = 0; i < CantFilas; i++)
                                {
                                    for (int j = 0; j < 7; j++)
                                    {
                                        matriz[i, j] = "-"; // Inicializa cada posición con un espacio en blanco
                                    }
                                } %>


                            <%  
                                int filas = 0;
                                int DiasAct = -1;
                                int Bandera = 0;

                                foreach (System.Data.DataRow dataRow in dt.Rows)
                                {

                                    if (dataRow["SectorProductivo"].ToString().ToUpper() == SectorActual)
                                    {
                                        if (Bandera == 0)
                                        {
                                            Bandera++;
                                            DiasAct = Convert.ToInt32(dataRow["DiasMasNivel"].ToString());
                                            matriz[filas, 6 - Convert.ToInt32(dataRow["DiasMasNivel"].ToString())] = dataRow["ProductoOReceta"].ToString() + ";" + dataRow["Descripcion"].ToString() + ";" + dataRow["id"].ToString() + ";" + dataRow["Cantidad"].ToString() + ";" + dataRow["Nivel"].ToString() + ";" + dataRow["DiasMasNivel"].ToString() + ";" + dataRow["descripcionReceta"].ToString() + ";" + dataRow["idReceta"].ToString();
                                            
                                        }

                                        else
                                        {
                                            if (Convert.ToInt32(dataRow["DiasMasNivel"].ToString()) != DiasAct)
                                            {
                                                DiasAct = Convert.ToInt32(dataRow["DiasMasNivel"].ToString());
                                                filas = 0;
                                            }
                                            else
                                            {
                                                filas++;
                                            }
                                            matriz[filas, 6 - Convert.ToInt32(dataRow["DiasMasNivel"].ToString())] = dataRow["ProductoOReceta"].ToString() + ";" + dataRow["Descripcion"].ToString() + ";" + dataRow["id"].ToString() + ";" + dataRow["Cantidad"].ToString() + ";" + dataRow["Nivel"].ToString() + ";" + dataRow["DiasMasNivel"].ToString() + ";" + dataRow["descripcionReceta"].ToString() + ";" + dataRow["idReceta"].ToString();


                                        }


                                    }
                                }

                            %>

                            <% 
                                int filasMatriz = matriz.GetLength(0);
                                int ColumnasMatriz = matriz.GetLength(1);

                                for (int fila = 0; fila < filasMatriz; fila++)
                                {%>
                            <tr>
                                <%  for (int columna = 0; columna < ColumnasMatriz; columna++)
                                    {%>
                                <% string valor = matriz[fila, columna];%>
                                <% 
                                    string[] partes = valor.Split(';'); // Dividir el valor en dos partes
                                    string productoOReceta = partes[0]; // Guardar la primera parte
                                %>
                                <td><%= productoOReceta%>
                                    <%if (productoOReceta != "-")
                                        {
                                            string segundaParte = partes[1];
                                            string id = partes[2];
                                            if (segundaParte == "Receta")
                                            {
                                                string Cantidad = partes[3];
                                                string Nivel = partes[4];
                                                string DiasMasNivel = partes[5];
                                                string descripcionReceta = partes[6];
                                                string idReceta = partes[7];
                                    %>
                                    <a href="GenerarProduccion.aspx?PPro=2&PR=<%=productoOReceta %>&C=<%=Cantidad %>&i=<%=id %>"
                                        target="_blank" style="float: right; margin-left: 10px;"
                                        title="Produccion">
                                        <i class="fa fa-cutlery" style="color: black"></i></a>



                                    <button id="Recepcion" type="button" class="icon-button" style="float: right; margin-left: 10px;"
                                        title="Recepción" onclick="MostrarRecepcion('<%=Cantidad %>', <%=id %>, <%=Nivel %>, <%=DiasMasNivel %>, '<%=descripcionReceta %>', <%=idReceta %>, <%=dt %>)">
                                        <i class="fa fa-exchange"></i>
                                    </button>



                                    <% }%>
                                    <a href="/Formularios/Maestros/StockDetallado.aspx?t=1&i=<%=id %>"
                                        target="_blank" style="float: right; margin-left: 10px;"
                                        title="Ver stock">
                                        <i style="color: black" class="fa fa-list-alt"></i></a>

                                    <%} %>
                                </td>
                                <%}
                                %>
                            </tr>

                            <%}

                            %>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>



    <%
            }



        } %>


    <asp:LinkButton runat="server" ID="btnCancelar" class="btn btn-primary dim" style="float:right; margin-left:1%" Text="Cancelar" 
       OnClick="btnCancelar_Click">
    </asp:LinkButton>

    <asp:LinkButton runat="server" ID="btnProducir" class="btn btn-primary dim" style="float:right" Text="A producir" OnClientClick="Producir(); 
        return false;">
    </asp:LinkButton>




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

                             setTimeout(function() {
                              $('#modalBusqueda').modal('show');
                          }, 300);

                })
                .catch(error => {
                    // Código para manejar errores aquí
                    console.error('Error:', error);
                });

              
            }
              function cambiarEstadoAPediente(){
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
                body: JSON.stringify({id: id, estadoOrden: estadoOrden}),
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

            function Producir(){
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

                 // console.log(id);
                  let estadoOrden = 1;
                  fetch('CronogramaProduccion.aspx/cambiarEstadoDeLaOrden', {
                  method: 'POST',
                  body: JSON.stringify({id: id, estadoOrden: estadoOrden}),
                  headers: { 'Content-Type': 'application/json' },
              })
              .then(response => response.json())
              .then(data => {
                         
                   window.location.href = "OrdenesDeProduccion.aspx"
              })
              .catch(error => {
                  // Manejo de errores aquí si es necesario
                  console.error('Error:', error);
              });
            }

    </script>
</asp:Content>
