<%@ Page Title="Stock" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="Tecnocuisine.Stock" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid" style="background: white">

            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="widget lazur-bg p-lg text-center">
                                <div class="m-b-md">
                                    <h3 class="font-bold no-margins">
                                        <asp:Label Text="" ID="lblSucursal" runat="server" />
                                    </h3>
                                    <h1 class="m-xs">
                                        <asp:Label Text="" ID="lblCantidadSucursal" runat="server" />
                                    </h1>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="widget lazur-bg p-lg text-center">
                                <div class="m-b-md">
                                    <h3 class="font-bold no-margins">
                                        Todas las sucursales

                                    </h3>
                                    <h1 class="m-xs">
                                        <asp:Label Text="" ID="lblStockTotal" runat="server" />
                                    </h1>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content">
        <div class="container-fluid" style="background: white">

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Stock</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="col-lg-12">
                        <div class="panel blank-panel">

                            <ul id="myTab" class="nav nav-tabs">
                                <li class="active"><a href="#home" data-toggle="tab">Stock</a></li>
                                <li class=""><a href="#profile2" data-toggle="tab">Auditoria Stock</a></li>

                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane fade active in" id="home">
                                    <div class="col-md-12 col-xs-12">
                                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                            <ContentTemplate>
                                                <table class="table table-striped table-bordered table-hover " id="editable3" style="margin-top: 3% !important">
                                                    <thead>

                                                        <tr>
                                                            <th>Sucursal</th>
                                                            <th style="text-align: right">Stock</th>

                                                            <asp:PlaceHolder runat="server" ID="phImportacionesPendientes" Visible="false">
                                                                <th>Importaciones Pendientes</th>
                                                            </asp:PlaceHolder>

                                                            <asp:PlaceHolder runat="server" ID="phRemitosPendientes" Visible="false">
                                                                <th>Remitos Pendientes</th>
                                                            </asp:PlaceHolder>

                                                            <asp:PlaceHolder runat="server" ID="phStockReal" Visible="false">
                                                                <th>Stock Real</th>
                                                            </asp:PlaceHolder>

                                                            <th></th>
                                                        </tr>

                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phStock" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>



                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>



                                <div class="tab-pane fade" id="profile2">

                                    <div class="col-md-12 col-xs-12">
                                        <div class="widget big-stats-container stacked">
                                            <div class="widget-content">

                                                <div id="big_stats" class="cf">
                                                    <%--<div class="stat">
                                                        <h4>En su sucursal:</h4>
                                                        <asp:Label ID="labelSaldo" runat="server" Text="" class="value"></asp:Label>
                                                    </div>--%>
                                                    <!-- .stat -->
                                                </div>

                                            </div>
                                            <!-- /widget-content -->
                                        </div>
                                        <!-- /widget -->
                                        <div class=" widget-content" style="width: 100%">
                                            <%--<div class="btn-group">
                                                <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" id="btnAccion" runat="server" visible="false">Accion    <span class="caret"></span></button>
                                                <ul class="dropdown-menu">
                                                    <li>
                                                        <asp:LinkButton ID="btnExportarExcel" runat="server" OnClick="btnExportarExcel_Click">Exportar a Excel</asp:LinkButton>

                                                    </li>
                                                    <li>
                                                        <%--<asp:LinkButton ID="LinkButton1" Visible="True" runat="server" OnClick="LinkButton1_Click">Corregir stock</asp:LinkButton>--%>

                                            <%-- </li>
                                                </ul>
                                            </div>--%>
                                            <!-- /btn-group -->
                                            <%--<asp:Label ID="lblParametros" runat="server" Style="color: #CCCCCC;"></asp:Label>--%>
                                            <div class="btn-group pull-right" style="height: 100%">
                                                <linkbutton type="button" data-toggle="modal" href="#modalBusqueda" class="btn btn-success">Filtrar&nbsp;<i style="color:white" class="fa fa-filter"></i></linkbutton>

                                            </div>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
                                            <ContentTemplate>
                                                <table class="table table-striped table-bordered table-hover " style="margin-top: 3% !important">
                                                    <thead>
                                                        <tr>
                                                            <th>Fecha</th>
                                                            <th>Tipo</th>
                                                            <th>Descripcion</th>
                                                            <th style="text-align: right">Cantidad</th>
                                                            <th>Cliente</th>
                                                        </tr>

                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phMovimientoStock" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>

                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>


                                    </div>

                                </div>




                                <!-- /.content -->

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Filtrar</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                        <ContentTemplate>

                            <div class="row" style="margin-top: 2%">
                                <label class="col-sm-2 control-label editable">Fecha Desde</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFechaDesdeMov" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 2%">
                                <label class="col-sm-2 control-label editable">Fecha Hasta</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFechaHastaMov" class="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 2%">
                                <label class="col-sm-2 control-label editable">Sucursal</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="lstSucursal" runat="server" class="form-control"></asp:DropDownList>

                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>

                </div>


                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnFiltrar" class="buttonLoading btn btn-primary" OnClick="btnFiltrar_Click"><i class="fa fa-check"></i>&nbsp;Filtrar </asp:LinkButton>

                    <%--<asp:LinkButton ID="btnFiltrar" runat="server" Text="<span class='shortcut-icon icon-ok'></span>" class="btn btn-success" OnClick="btnFiltrar_Click" />--%>
                </div>
            </div>

        </div>
    </div>



    <div id="modalAgregar" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Agregar</h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Codigo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCodigo" class="form-control" disabled runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Sucursal</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSucursal" class="form-control" disabled runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Articulo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtArticulo" class="form-control" disabled runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Stock Actual</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtStockActual" class="form-control" disabled runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Agregar</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAgregarStock" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Comentario</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtComentarios" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_Agregar" runat="server" Text="Guardar" class="btn btn-success" OnClick="btn_Agregar_Click" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:HiddenField runat="server" ID="hiddenidStock" />


                </div>
            </div>
        </div>
    </div>

    <script>
        function create(idBoton) {
            document.getElementById('ContentPlaceHolder1_hiddenidStock').value = idBoton;
            $.ajax({
                method: "POST",
                url: "Stock.aspx/ObtenerStock",
                data: '{id: "' + idBoton + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: cargarStock
            });

        }

        function cargarStock(response) {
            var obj = JSON.parse(response.d);
            var data = obj.split('|');
            document.getElementById('ContentPlaceHolder1_txtCodigo').value = data[0];
            document.getElementById('ContentPlaceHolder1_txtSucursal').value = data[1];
            document.getElementById('ContentPlaceHolder1_txtArticulo').value = data[2];
            document.getElementById('ContentPlaceHolder1_txtStockActual').value = data[3];



        }

        function edit(id) {
            var modal = new DayPilot.Modal();
            modal.closed = function () {
                if (this.result == "OK") {
                    __doPostBack("UpdateButton", "");
                    location.reload();
                }
            };
            modal.showUrl("ModalEdit.aspx?id=" + id);
        }
    </script>


    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtPorcentajeAlicuota.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'Alicuotas', location.protocol + '//' + location.host + location.pathname);
        }
    </script>
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
                "height": "100%"
            });
            $("#editable_filter").appendTo("#editable_length");

            $("#editable_filter").css('display', 'inline');
            $("#editable_filter").css('padding-left', '5%');
            var parent = $("#editable_length")[0].parentNode;
            parent.className = 'col-sm-12';
            var div = document.getElementById('editable_filter');
            var button = document.createElement('linkbutton');
            button.id = "btnAgregar";
            button.style.float = "right";
            button.style.marginRight = "1%";
            button.setAttribute("type", "button");
            button.setAttribute("href", "#modalAgregar");
            button.setAttribute("onclick", "vaciarFormulario()");
            button.setAttribute("data-toggle", "modal");
            button.setAttribute("class", "btn");
            
            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';
            var filter = $("#editable_length");
            filter[0].id = 'editable_length2';

        });
    </script>
</asp:Content>
