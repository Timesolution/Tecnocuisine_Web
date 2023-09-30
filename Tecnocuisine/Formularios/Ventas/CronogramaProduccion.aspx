<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CronogramaProduccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.CronogramaProduccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />


    <div class="row" style="padding-left: 14px; padding-right: 14px">
        <!-- Utiliza col-lg-8 para la parte izquierda -->
        <div class="ibox-content m-b-sm border-bottom">
            <div class="row">
                <div class="col-lg-6" style="height: 100%; border-right: 1px solid #ccc;">
                    <div class="p-xs">
                        <div class="pull-left m-r-md">
                            <i class="fa fa-utensils text-navy mid-icon"></i>
                        </div>
                        <h2 id="carpetaNumero">Orden de producción</h2>
                        <span id="caratula">
                            <asp:Label ID="lblIdsQueryString" runat="server" Text=""></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="pull-left m-r-md">
                        <i class="fa fa-utensils text-navy mid-icon"></i>
                    </div>
                    <%foreach (System.Data.DataRow dtCantidadXProducto in dtCantidadRecetasPorCadaOrden.Rows)
                        {%>
                    <h4><%= dtCantidadXProducto["Producto"].ToString() %> :  <%= dtCantidadXProducto["cantidad"].ToString() %></h4>
                    <% } %>
                </div>
            </div>
        </div>
    </div>




    <%
        int PrimerVuelta = 0;
        int TiempoActual = 0;
        DateTime fechaActual = DateTime.MinValue;
        int widgetCount = 0; // Variable para contar widgets
        int SegundoWiggetEnAdelante = 0; // Variable para contar widgets
        foreach (System.Data.DataRow dr in dt.Rows)
        {
            widgetCount++; // Incrementa el contador de widgets
            bool isCollapsed = widgetCount > 0;
            if (PrimerVuelta == 0)
            {
                //TiempoActual = int.Parse(dr["Tiempo"].ToString());
                fechaActual = Convert.ToDateTime(dr["Fecha"].ToString());
                PrimerVuelta++;

    %>


    <div class="row">
        <div class="col-lg-12">
            <%--<div class="ibox float-e-margins">--%>
            <div class="ibox float-e-margins" id="widget<%= SegundoWiggetEnAdelante %>" data-collapsed="<%= isCollapsed.ToString().ToLower() %>">
                <div class="ibox-title">
                    <h5><%= Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy") %></h5>

                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            <li><a href="#">Config option 1</a>
                            </li>
                            <li><a href="#">Config option 2</a>
                            </li>
                        </ul>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>




                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-striped table-bordered table-hover " id="editable">
                                <thead>
                                    <tr>
                                        <th style="width: 25%;">Sector Productivo</th>
                                        <%--<th style="width: 15%; text-align: right;"">Cantidad</th>--%>
                                        <%--<th style="width: 25%;">Receta</th>--%>
                                        <th style="width: 20%">Producto</th>
                                        <th style="width: 10%; text-align: right;">Cantidad</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <%foreach (System.Data.DataRow dtr in dt.Rows)
                                        {
                                            if (Convert.ToDateTime(dtr["Fecha"].ToString()) == fechaActual)
                                            {%>
                                    <tr>
                                        <td><%= dtr["SectorProductivo"].ToString() %></td>
                                        <%--<td style="text-align: right;">0</td>--%>
                                        <td><%= dtr["Producto"].ToString() %></td>
                                        <%--<td><%= dtr["descripcion1"].ToString() %></td>--%>
                                        <td style="text-align: right;"><%= dtr["Kilogramos"].ToString() %></td>
                                    </tr>

                                    <%}%>
                                    <% } %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%

        }


        if (Convert.ToDateTime(dr["Fecha"].ToString()) != fechaActual)
        {
            fechaActual = Convert.ToDateTime(dr["Fecha"].ToString());

    %>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox float-e-margins" id="widget<%= widgetCount %>" data-collapsed="<%= isCollapsed.ToString().ToLower() %>">
                <!-- Agregar la clase "collapsed" aquí -->
                <div class="ibox-title">
                    <%--<h5><%= Convert.ToDateTime(dr["fechaEntrega"]).AddDays(-TiempoActual).ToString("dd/MM/yyyy") %></h5>--%>
                    <h5><%= Convert.ToDateTime(dr["Fecha"]).ToString("dd/MM/yyyy") %></h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-down"></i>
                            <!-- Usar el ícono hacia abajo por defecto -->
                        </a>
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            <li><a href="#">Config option 1</a>
                            </li>
                            <li><a href="#">Config option 2</a>
                            </li>
                        </ul>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table table-striped table-bordered table-hover " id="editable2">
                                <thead>
                                    <tr>
                                        <th style="width: 25%;">Sector Productivo</th>
                                        <%--<th style="width: 15%; text-align: right;"">Cantidad</th>--%>
                                        <th style="width: 25%">Producto</th>
                                        <th style="width: 10%; text-align: right;">Cantidad</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%foreach (System.Data.DataRow dtr in dt.Rows)
                                        {
                                            if (Convert.ToDateTime(dtr["Fecha"].ToString()) == fechaActual)
                                            {%>
                                    <tr>
                                        <td><%= dtr["SectorProductivo"].ToString() %></td>
                                        <%--<td style="text-align: right;">0</td>--%>
                                        <%--<td><%= //dtr["descripcion"].ToString() %></td>--%>
                                        <td><%= dtr["Producto"].ToString() %></td>
                                        <td style="text-align: right;"><%= dtr["Kilogramos"].ToString() %></td>
                                    </tr>

                                    <%}%>
                                    <% } %>
                                    <%--<asp:PlaceHolder ID="phRubroRecaudacionMensual" runat="server"></asp:PlaceHolder>--%>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%
            }



        } %>

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

            //oTable.$('td').editable('../example_ajax.php', {
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

        });


        //Mas codigo para que se minimice el widget
        function toggleWidget(widgetId) {
            var $widget = $("#" + widgetId); // Encuentra el widget por su ID
            var $content = $widget.find(".ibox-content"); // Encuentra el contenido del widget

            // Toggle para mostrar/ocultar el contenido del widget
            $content.slideToggle();
        }

    </script>
</asp:Content>
