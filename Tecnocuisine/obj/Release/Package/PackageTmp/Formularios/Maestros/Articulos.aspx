<%@ Page Title="Articulos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Articulos.aspx.cs" Inherits="Tecnocuisine.Articulos" %>

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

                                                        <a data-toggle="modal" data-backdrop="static" data-target="#modalAgregar" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                                                    </div>
                                                </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive"  >
                                <table class="table table-striped table-bordered table-hover " id="editable" >
                                    <thead>
                                        <tr>
                                            <th style="width: 10%">Codigo</th>
                                            <th style="width:30%">Descripcion</th>
                                            <th style="width:15%">Grupo</th>
                                            <th style="width:10%">Ultima Actualizacion</th>
                                            <th style="width:8%">Precio Venta</th>
                                            <th style="width:5%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phArticulos" runat="server"></asp:PlaceHolder>
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

    <div id="modalAgregar" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Agregar</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <label class="col-sm-2 control-label editable">Codigo</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCodigo" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Codigo Barra</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCodigoBarra" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Descripcion</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDescripcion" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                   
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Marca</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListMarca" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Grupo</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListGrupo" style="margin-left: 3%;"  class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                 <%--    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">SubGrupo</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListSubgrupo" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hiddenSubgrupo" runat="server" />
                        </div>

                    </div>--%>
                     <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Categoria</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListCategoria" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                     <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">IVA</label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ListAlicuotas" style="margin-left: 3%;" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Costo FC</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCosto" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Precio de venta</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtPrecioVenta" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Fecha alta</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFechaAlta" style="margin-left: 3%;" disabled="disabled" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Fecha actualizacion</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFechaActualizacion" disabled="disabled" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Fecha modificacion</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtFechaModificacion" style="margin-left: 3%;" disabled="disabled" class="form-control" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Observaciones</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtObservaciones" style="margin-left: 3%;" class="form-control" runat="server" ></asp:TextBox>
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
<%--    <script>
        $(document).ready(function () {  
            $(document).on('change', "#ContentPlaceHolder1_ListGrupo", function () {
                var idGrupo = document.getElementById('ContentPlaceHolder1_ListGrupo');
                $.ajax({
                    type: "POST",
                    url: "Subgrupos.aspx/GetSubgrupos",
                    data: '{id: "' + idGrupo.value + '" }',
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                        //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                    },
                    success: function (data) {
                        var obj = JSON.parse(data.d);
                        var subgrupos = obj.split(',');
                        var s = '<option value="-1">Seleccione</option>';
                        for (var i = 0; i < subgrupos.length; i++) {
                            var subgrupo = subgrupos[i];
                            s += '<option value="' + subgrupo.split(';')[0] + '">' + subgrupo.split(';')[1] + '</option>';
                        }
                        $("#ContentPlaceHolder1_ListSubgrupo").html(s);
                    }
                });
            });  
        });
        $(document).on('change', "#ContentPlaceHolder1_ListSubgrupo", function () {
            
        });

            
    </script>--%>
    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            let date = new Date();

            let day = date.getDate();
            let month = date.getMonth() + 1;
            let year = date.getFullYear();
            var dia = `${day}/${month}/${year}`;
            ContentPlaceHolder1_hiddenEditar.value = "";
            ContentPlaceHolder1_txtCodigo.value = "";
            ContentPlaceHolder1_txtCodigoBarra.value = "";
            ContentPlaceHolder1_txtDescripcion.value = "";
            ContentPlaceHolder1_txtCosto.value = "";
            ContentPlaceHolder1_txtPrecioVenta.value = "";
            ContentPlaceHolder1_txtObservaciones.value = "";
            ContentPlaceHolder1_txtFechaActualizacion.value = dia;
            ContentPlaceHolder1_txtFechaAlta.value = dia;
            ContentPlaceHolder1_txtFechaModificacion.value = dia;
            ContentPlaceHolder1_ListGrupo.value = "-1";
            ContentPlaceHolder1_ListCategoria.value = "-1";
            ContentPlaceHolder1_ListAlicuotas.value = "-1";
            ContentPlaceHolder1_ListMarca.value = "-1";
            //ContentPlaceHolder1_ListSubgrupo.value = "-1";

          

            window.history.pushState('', 'Articulos', location.protocol + '//' + location.host + location.pathname);
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
                "height": "100%",
                "pageLength": 25
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
            $('#ContentPlaceHolder1_txtCosto').autoNumeric('init', { vMin: '0.00', vMax: '99999999999.99', aSign: '', aSep: ',', aPad: false, mDec: '2', aDec: '.', aForm: false })
        })
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtPrecioVenta').autoNumeric('init', { vMin: '0.00', vMax: '99999999999.99', aSign: '', aSep: ',', aPad: false, mDec: '2', aDec: '.', aForm: false })
        })
    </script>
    </asp:Content>
