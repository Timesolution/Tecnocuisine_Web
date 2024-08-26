<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecetaEvolucion.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.RecetaEvolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                                    <div class="col-md-10">

                                                        <h1>
                                                            <asp:Label runat="server" ID="lblDescripcion" Style="font-size: 2rem; font-weight: bold"></asp:Label>
                                                        </h1>

                                                        <div class="input-group m-b">
                                                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>

                                                    </div>

                                                </div>

                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
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
            $('.dataTables-example').dataTable({
                responsive: true,
                "dom": 'T<"clear">lfrtip',
                "tableTools": {
                    "sSwfPath": "js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
                }
            });

            /* Init DataTables */
            var oTable = $('#editable').dataTable();

            /* Apply the jEditable handlers to the table */
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
                "height": "100%",
                "pageLength": 25
            });


            $("#editable_filter").appendTo("#editable_length");

            $("#editable_filter").css('display', 'inline');
            $("#editable_filter").css('padding-left', '5%');
            var parent = $("#editable_length")[0].parentNode;
            parent.className = 'col-sm-12';

            var div = document.getElementById('editable_filter');
            var button = document.createElement('a');
            //button.id = "btnAgregar";
            button.style.float = "right";
            button.style.marginRight = "1%";
            //button.setAttribute("type", "a");
            button.setAttribute("href", "RecetasABM.aspx");
            //button.setAttribute("onclick", "vaciarFormulario()");
            //button.setAttribute("data-toggle", "modal");
            button.setAttribute("class", "btn");

            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';
            var filter = $("#editable_length").css('display', 'none');
            filter[0].id = 'editable_length2';

            $('.dataTables_filter').hide();
            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });


            //document.getElementById('editable_length2').children[0].remove();
        });
    </script>

</asp:Content>
