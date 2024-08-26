<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecetaEvolucion.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.RecetaEvolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <%--  <div class="ibox-title">
                    <h5>Recetas</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>--%>
                <div class="">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="wrapper wrapper-content animated fadeInRight">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">

                                            <div class="ibox-content">
                                                <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                                    <div class="col-md-12">

                                                        <div style="display: flex; width: 100%">
                                                            <h1 style="width: 50%">
                                                                <asp:Label runat="server" ID="lblDescripcion" Style="font-size: 2rem; font-weight: bold"></asp:Label>
                                                            </h1>

                                                            <%--<div style="width: 50%; height: 120px;">
                                                                <div style="width:100%; height:100%">
                                                                    <canvas id="costChart" style="float: right; width: 100%"></canvas>
                                                                </div>
                                                            </div>--%>
                                                        </div>

                                                        <div class="input-group m-b">
                                                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>

                                                    </div>

                                                </div>

                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <%--<th>ID</th>--%>
                                                            <th>Fecha del cambio</th>
                                                            <th>Ingrediente</th>
                                                            <th style="text-align: right">Nuevo Costo Ingrediente</th>
                                                            <th style="text-align: right">Anterior Costo Ingrediente</th>
                                                            <th style="text-align: right">Nuevo Costo Total Receta</th>
                                                            <th style="text-align: right">Anterior Costo Total Receta</th>
                                                            <%--<th></th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phIngredientes" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            // Inicializa DataTable con opciones
            var oTable = $('#editable').DataTable({
                responsive: true,
                dom: 'T<"clear">lfrtip',
                order: [[0, 'desc']],
                pageLength: 25,
                language: {
                    emptyTable: "No hay datos disponibles en la tabla",
                    search: "Buscar:",
                    lengthMenu: "Mostrar _MENU_ entradas",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                    paginate: {
                        first: "Primero",
                        previous: "Anterior",
                        next: "Siguiente",
                        last: "Último"
                    }
                }
            });

            // Oculta los elementos dataTables_length y editable_filter
            $('#editable_length').hide();
            $('#editable_filter').hide();
            $('.DTTT_container').hide();
            $('#editable_wrapper').css('background-color', 'white');

            // Cambia el estilo del contenedor de longitud de tabla
            var parent = $("#editable_length").parent();
            parent.addClass('col-sm-12');

            // Añade un botón para agregar registros
            var div = $('#editable_filter');
            var button = $('<a>', {
                href: "RecetasABM.aspx",
                class: "btn",
                style: "float: right; margin-right: 1%;",
                html: "<i style='color: black' class='fa fa-plus'></i>"
            });
            div.prepend(button);

            // Configura el campo de búsqueda personalizado
            $('#txtBusqueda').on('keyup', function () {
                oTable.search(this.value).draw();
            });
        });
    </script>




    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText = "Recetas / Evolucion";
        });
    </script>

</asp:Content>
