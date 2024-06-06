﻿<%@ Page Title="Alicuotas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alicuotas.aspx.cs" Inherits="Tecnocuisine.Alicuotas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">

            <div class="ibox float-e-margins">

                <div class="ibox-content">
                    <div style="margin-left: 0px; margin-right: 0px;" class="row">
                        <div class="col-md-10">

                            <div class="input-group m-b">
                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                                <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <a data-toggle="modal" data-backdrop="static" onclick="vaciarInputs(); openModalAgregar()"
                                title="Agregar Alicuota"
                                class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%">#</th>
                                            <th style="width: 40%; text-align: right">Porcentaje</th>
                                            <th style="width: 30%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phAlicuotas" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>

                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>



        <div id="modalConfirmacion2" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Confirmar eliminacion</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            Esta seguro que lo desea eliminar?
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <asp:HiddenField ID="hiddenID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalAgregar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Agregar Alícuota IVA</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-2 control-label editable">Porcentaje</label>
                        <div class="col-sm-8">
                            <div class="input-group m-b">
                                <asp:TextBox ID="txtPorcentajeAlicuota" class="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon">%</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>


                </div>
            </div>
        </div>
    </div>

    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }


        function vaciarInputs(){
            document.getElementById('<%= txtPorcentajeAlicuota.ClientID %>').value = "";
        }
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar .modal-title').text('Editar Alícuota IVA');
            document.getElementById('<%= btnGuardar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Editar';
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtPorcentajeAlicuota.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'Alicuotas', location.protocol + '//' + location.host + location.pathname);
        }
    </script>
    <script>
        function openModalAgregar() {
            $('#modalAgregar .modal-title').text('Agregar Alícuota IVA');
            document.getElementById('<%= btnGuardar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Agregar';
            $('#modalAgregar').modal('show');
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
            var oTable2 = $('#editable2').dataTable();

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

            /* Apply the jEditable handlers to the table */
            oTable2.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable2.fnGetPosition(this);
                    oTable2.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable2.fnGetPosition(this)[2]
                    };
                },

                "width": "90%",
                "height": "100%"
            });
            $("#editable_filter").appendTo("#editable_length");

            $("#editable_filter").css('display', 'none');
            $("#editable_filter").css('padding-left', '5%');
            var parent = $("#editable_length")[0].parentNode;
            parent.className = 'col-sm-12';
            parent.style = 'display:none';
            var div = document.getElementById('editable_filter');
            var button = document.createElement('a');
            /* button.id = "btnAgregar";*/
            button.style.float = "right";
            button.style.marginRight = "1%";
            //button.setAttribute("type", "button");
            button.setAttribute("href", "ProductosABM.aspx");
            //button.setAttribute("href", "#modalAgregar");
            //button.setAttribute("onclick", "vaciarFormulario()");
            //button.setAttribute("data-toggle", "modal");
            button.setAttribute("class", "btn");

            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';

            //var filter = $("#editable_length");
            //filter[0].id = 'editable_length2';


            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });
        });
    </script>
    <script src="/../Scripts/autoNumeric/autoNumeric.js"></script>
    <script>

        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtPorcentajeAlicuota').autoNumeric('init', { vMin: '0.000', vMax: '99999999999.999', aSign: '', aSep: ',', aPad: false, mDec: '3', aDec: '.', aForm: false })
        })

    </script>
</asp:Content>
