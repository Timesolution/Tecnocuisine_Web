<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Productos.aspx.cs" Inherits="Tecnocuisine.Productos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="wrapper wrapper-content animated fadeInRight">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">

                                            <div class="ibox-content">
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Descripcion</th>
                                                            <th>Categoria</th>
                                                            <th>Atributo</th>
                                                            <th>Unidades</th>
                                                            <th>Costo</th>
                                                            <th>Presentaciones</th>
                                                            <th>Alicuota</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phProductos" runat="server"></asp:PlaceHolder>
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
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="btn btn-danger" OnClick="btnSi_Click" />
                    <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                    <asp:HiddenField ID="hiddenID" runat="server" />
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
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtDescripcionProducto" class="form-control" runat="server" />
                        </div>


                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Categoria</label>
                        <div class="col-sm-7">

                            <asp:TextBox ID="txtDescripcionCategoria" class="form-control" runat="server" />
                            <asp:HiddenField ID="idCategoria" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" ID="btnCategorias" class="btn btn-primary dim" data-toggle="modal" href="#modalCategoria"><i style="color: white" runat="server" class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Atributo</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtDescripcionAtributo" class="form-control" runat="server" />
                            <asp:HiddenField ID="idAtributo" runat="server" />

                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" disabled="disabled" ID="btnAtributos" class="btn btn-primary dim" data-toggle="modal" href="#modalAtributo"><i style="color: white"  class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Costo</label>
                        <div class="col-sm-7">
                            <div class="input-group m-b">
                                <span class="input-group-addon">$</span><asp:TextBox ID="txtCosto" class="form-control" runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Unidad de medida</label>
                        <div class="col-sm-7">
                            <asp:DropDownList ID="ListUnidadMedida" class="form-control m-b" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Presentacion</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtDescripcionPresentacion" class="form-control" runat="server" />
                            <asp:HiddenField ID="idPresentacion" runat="server" />
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" ID="btnPresentacion" OnClientClick="editarPresentaciones()" class="btn btn-primary dim" data-toggle="modal" href="#modalPresentacion"><i style="color: white"  class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Alicuota IVA</label>
                        <div class="col-sm-7">
                            <div class="input-group m-b">
                                <asp:DropDownList ID="ListAlicuota" class="form-control m-b" runat="server">
                                </asp:DropDownList><span class="input-group-addon">%</span>
                            </div>
                        </div>

                    </div>
                     <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Sistema Gestion</label>
                        <div class="col-sm-7">
                            <div class="input-group m-b">
                                <asp:CheckBox  ID="cbxGestion" onclick="mostrarGestion()"  runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="row" runat="server" id="divGrupo" style="margin-top: 2%;display: none;">
                        <label class="col-sm-2 control-label editable">Grupo</label>
                        <div class="col-sm-7">
                                <asp:DropDownList ID="ListGrupo" onchange="actualizarSubGrupo()"  class="form-control m-b" runat="server">
                                </asp:DropDownList>
                            <asp:HiddenField  runat="server" ID="hiddenGrupo"/>
                        </div>

                    </div>
                    <div class="row" runat="server" id="divSubgrupo" style="margin-top: 2%;display:none">
                        <label class="col-sm-2 control-label editable">Subgrupo</label>
                        <div class="col-sm-7">
                                <asp:DropDownList ID="ListSubgrupo" onchange="actualizarHiddenSubGrupo()" class="form-control m-b" runat="server">
                                </asp:DropDownList>
                        </div>
                            <asp:HiddenField  runat="server" ID="hiddenSubGrupo"/>

                    </div>

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary"  OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                </div>
            </div>
        </div>
              
    </div>



    <div id="modalAtributo" class="modal" tabindex="-2" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Elegir atributo</h4>
                </div>
                <div class="modal-body">
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="wrapper wrapper-content  animated fadeInRight">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <div class="ibox-content">



                                                    <div class="dd" id="nestable3">
                                                        <ol id="main" runat="server" class="dd-list">
                                                        </ol>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button id="btnAgregarAtributo" onclick="agregarAtributos();return false;" class="buttonLoading btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalCategoria" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Elegir Categoria</h4>
                </div>
                <div class="modal-body">
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="wrapper wrapper-content  animated fadeInRight">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <div class="ibox-content">



                                                    <div class="dd" id="nestable">
                                                        <ol id="olCategorias" runat="server" class="dd-list">
                                                        </ol>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                </div>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalPresentacion" class="modal" tabindex="-2" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Elegir Presentacion</h4>
                </div>
                <div class="modal-body">
                    <div class="ibox-content">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <div class="table-responsive" >
                                                                <table class="table table-striped table-bordered table-hover " id="editable2">

                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Descripcion</th>
                                                                            <th>Cantidad</th>
                                                                            <th></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:PlaceHolder ID="phPresentaciones" runat="server"></asp:PlaceHolder>
                                                                    </tbody>
                                                                </table>
                                                            </div>

                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button id="btnAgregarPresentacion" onclick="agregarPresentaciones();return false;" class="buttonLoading btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                    </div>
                </div>
            </div>
        </div>
    </div>





    <script>         
        function mostrarGestion() {
            var checkBox = document.getElementById("ContentPlaceHolder1_cbxGestion");
            // Get the output text
            var div1 = document.getElementById("ContentPlaceHolder1_divGrupo");
            var div2 = document.getElementById("ContentPlaceHolder1_divSubgrupo");

            // If the checkbox is checked, display the output text
            if (checkBox.checked == true) {
                div1.style.display = "block";
                div2.style.display = "block";
            } else {
                div1.style.display = "none";
                div2.style.display = "none";
            }
        }

        
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>

    


    <script type="text/javascript">
        function actualizarSubGrupo() {
            var hiddenGrupo = document.getElementById('ContentPlaceHolder1_hiddenGrupo');
            hiddenGrupo.value = ContentPlaceHolder1_ListGrupo.value;
            
            $.ajax({
                method: "POST",
                url: "Productos.aspx/GetSubgrupos",
                data: '{id: "' + ContentPlaceHolder1_ListGrupo.value + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: successCambiarListSubgrupos
            });


        }

        function successCambiarListSubgrupos(response) {
            var obj = JSON.parse(response.d);
            var data = obj.split(';');

            for (var i = 0; i < data.length; i++) {
                if (data[i] != "") {
                    $('#ContentPlaceHolder1_ListSubgrupo').append($('<option>', {
                        value: data[i].split(',')[0],
                        text: data[i].split(',')[1]
                    }));
                }
            }
        }
        function actualizarHiddenSubGrupo() {
            var hiddenSubGrupo = document.getElementById('ContentPlaceHolder1_hiddenSubGrupo');
            hiddenSubGrupo.value = ContentPlaceHolder1_ListSubgrupo.value;
        }
    </script> 
    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar').modal('show');
        }
    </script>

    <script type="text/javascript">
        function openModalCategorias() {
            $('#modalCategorias').modal('show');
        }
    </script>

    <script type="text/javascript">
        function openModalAtributos() {
            $('#modalAtributos').modal('show');
        }
    </script>

    <script type="text/javascript">
        function cargarCbx() {
            var hiddenCategoria = document.getElementById('ContentPlaceHolder1_idCategoria');
            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetSubAtributos",
                data: '{id: "' + hiddenCategoria.value + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: successCambiarDisplayDivs
            });
            var hiddenAtributo = document.getElementById('ContentPlaceHolder1_idAtributo');
            var atributos = hiddenAtributo.value.split(',');
            var inputs = document.querySelectorAll('input[type=checkbox]')
            for (var i = 0; i < inputs.length; i++) {
                for (var j = 0; j < atributos.length; j++) {
                    if (inputs[i].id.split('_')[2].split('-')[0].trim() == atributos[j]) {
                        inputs[i].checked = true;
                    }
                }
            }

            var elem = document.getElementById('ContentPlaceHolder1_main');
            for (var i = 0; i < elem.children.length; i++) {
                elem.children[i].style.display = "none"
            }
        }
        function successCambiarDisplayDivs(response) {
            var obj = JSON.parse(response.d);
            var tiposAtributos = obj.split(',');
            var elem = document.getElementById('ContentPlaceHolder1_main');

            for (var i = 0; i < elem.children.length; i++) {
                for (var j = 0; j < tiposAtributos.length; j++) {
                    if (elem.children[i].dataset.id == tiposAtributos[j]) {
                        elem.children[i].style.display = "block"
                    }
                }
            }

        }

    </script>

    <script type="text/javascript">
        function agregarAtributos() {
            var checkboxes = $('#ContentPlaceHolder1_main').find(':checkbox');
            var selectedcheckboxes = '';
            for (var i = 0; i < checkboxes.length; i++) { if (checkboxes[i].checked) { selectedcheckboxes += ',' + checkboxes[i].id.split('_')[2] } }
            var hiddenAtributo = document.getElementById('ContentPlaceHolder1_idAtributo');
            hiddenAtributo.value = selectedcheckboxes;

            var txrDescripcion = document.getElementById('ContentPlaceHolder1_txtDescripcionAtributo');
            txrDescripcion.value = selectedcheckboxes.replace(/^,/, "");;
            $('#modalAtributo').modal('hide');
            return false;
        }
    </script>

    <script type="text/javascript">
        function editarPresentaciones() {
            var hiddenPresentation = document.getElementById('ContentPlaceHolder1_idPresentacion');
            var inputs = $('#ContentPlaceHolder1_UpdatePanel5').find(':checkbox');
            for (var j = 0; j < inputs.length; j++) {
                inputs[j].checked = false;
            }


            var presentaciones = hiddenPresentation.value.split(',');
            var inputs = $('#ContentPlaceHolder1_UpdatePanel5').find(':checkbox');
            for (var i = 0; i < inputs.length; i++) {
                for (var j = 0; j < presentaciones.length; j++) {
                    if (inputs[i].id.split('_')[2].split('-')[0].trim() == presentaciones[j]) {
                        inputs[i].checked = true;
                    }
                }
            }

        }
    </script>

    <script type="text/javascript">
        function vaciarFormulario() {
            ContentPlaceHolder1_hiddenEditar.value = "";
            ContentPlaceHolder1_txtDescripcionProducto.value = "";
            ContentPlaceHolder1_txtDescripcionCategoria.value = "";
            ContentPlaceHolder1_txtDescripcionAtributo.value = "";
            ContentPlaceHolder1_txtCosto.value = "";
            ContentPlaceHolder1_txtDescripcionPresentacion.value = "";
            ContentPlaceHolder1_ListUnidadMedida.value = "-1";
            ContentPlaceHolder1_ListAlicuota.value = "-1";
            window.history.pushState('', 'Productos', location.protocol + '//' + location.host + location.pathname);

        }
        function agregarPresentaciones() {
            var checkboxes = $('#ContentPlaceHolder1_UpdatePanel5').find(':checkbox');
            var selectedcheckboxes = '';
            for (var i = 0; i < checkboxes.length; i++) { if (checkboxes[i].checked) { selectedcheckboxes += ',' + checkboxes[i].id.split('_')[2] } }
            var hiddenAtributo = document.getElementById('ContentPlaceHolder1_idPresentacion');
            hiddenAtributo.value = selectedcheckboxes;

            var txtDescripcion = document.getElementById('ContentPlaceHolder1_txtDescripcionPresentacion');
            txtDescripcion.value = selectedcheckboxes.replace(/^,/, "");;
            $('#modalPresentacion').modal('hide');
            return false;
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
                "height": "100%"
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
        });

 

 
    </script>
    <script>
        $(document).ready(function () {



            var updateOutput = function (e) {
                var list = e.length ? e : $(e.target),
                    output = list.data('output');
                if (window.JSON) {
                    output.val(window.JSON.stringify(list.nestable('serialize')));//, null, 2));
                } else {
                    output.val('JSON browser support required for this demo.');
                }
            };
            // activate Nestable for list 1
            $('#nestable').nestable({
                group: 1
            }).on('change', updateOutput);

            // activate Nestable for list 1
            $('#nestable3').nestable({
                group: 1
            }).on('change', updateOutput);



            // output initial serialised data
            updateOutput($('#nestable').data('output', $('#nestable-output')));
            updateOutput($('#nestable3').data('output', $('#nestable-output')));

            $('#nestable-menu').on('click', function (e) {
                var target = $(e.target),
                    action = target.data('action');
                if (action === 'expand-all') {
                    $('.dd').nestable('expandAll');
                }
                if (action === 'collapse-all') {
                    $('.dd').nestable('collapseAll');
                }

            });
        });
    </script>





</asp:Content>
