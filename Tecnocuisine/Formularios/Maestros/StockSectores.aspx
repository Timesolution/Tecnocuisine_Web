<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockSectores.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.StockSectores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">

                <div class="wrapper wrapper-content animated fadeInRight">

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">

                                <div class="ibox-content">
                                    <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                        <div class="col-md-12" style="display: flex; width: 100%; column-gap: 1rem">

                                            <div class="input-group m-b col-md-5">
                                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" />
                                            </div>

                                            <div class="input-group m-b col-md-7" style="display: flex; justify-content: flex-end;">
                                                <button id="btnImprimir" onclick="imprimir()" class="btn btn-sm btn-primary m-t-n-xs" data-toggle="tooltip" title="Imprimir" style="font-weight: bold; margin-left: 1rem; margin-bottom: 5px; margin-top: 5px;">
                                                    <i class="fa fa-print" aria-hidden="true"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>

                                    <table class="table table-striped table-bordered table-hover" id="editable">
                                        <thead>
                                            <tr>
                                                <%--<th style="display:none"></th>--%>
                                                <th style="width: 15%">Codigo</th>
                                                <th>Nombre</th>
                                                <th style="width: 15%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phSectores" runat="server"></asp:PlaceHolder>
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


    <script>
        $(document).ready(function () {
            document.getElementById("lblSiteMap").innerText = "Stock / Stock Sectores";
        });
    </script>

    <script>
        $(document).ready(function () {
            // Inicializa DataTable con opciones
            var oTable = $('#editable').DataTable({
                responsive: true,
                dom: 'T<"clear">lfrtip',
                order: [[0, 'desc']],
                pageLength: 10,
                language: {
                    url: '//cdn.datatables.net/plug-ins/2.1.5/i18n/es-ES.json',
                },
                "order": [[1, 'asc']], // Ordena por la segunda columna de manera ascendente
                initComplete: function () {
                    // Oculta los elementos dataTables_length y editable_filter una vez que la tabla esté completamente cargada
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
                }
            });



            // Función de filtro personalizada para buscar registros que comiencen con el término ingresado
            $.fn.dataTable.ext.search.push(
                function (settings, data, dataIndex) {
                    var searchTerm = $('#txtBusqueda').val().toLowerCase();
                    if (searchTerm === "") {
                        return true; // No hay filtro, mostrar todas las filas
                    }

                    // Comprueba si alguna columna comienza con el término de búsqueda
                    return data.some(function (value) {
                        return value.toLowerCase().startsWith(searchTerm);
                    });
                }
            );
            // Configura el campo de búsqueda personalizado
            $('#txtBusqueda').on('keyup', function () {
                oTable.search(this.value).draw();
            });
        });
    </script>

    <script>
        function imprimir() {
            // Prevenir el comportamiento predeterminado del botón (para que no recargue la pagina)
            event.preventDefault();

            let url = "ImpresionStockSectores.aspx";
            window.open(url, "_blank");
        }
    </script>

</asp:Content>
