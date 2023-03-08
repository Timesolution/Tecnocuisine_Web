<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Produccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Produccion" %>

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
                                                <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                                    <div class="col-md-10">

                                                        <div class="input-group m-b">
                                                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <a href="../Ventas/GenerarProduccion.aspx" class="btn btn-primary dim" data-toggle="tooltip" data-placement="top" data-original-title="Agregar Nueva Produccion" style="margin-right: 1%; float: right"><i class="fa fa-plus"></i></a>
                                                    </div>
                                                </div>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style=" text-align: right;">#</th>
                                                            <th>Receta</th>
                                                            <th style=" text-align: right;">Numero Produccion</th>
                                                            <th>Fecha Produccion</th>
                                                            <th style=" text-align: right;">Cantidad Producida</th>
                                                            <th>Lote</th
                                                            <th style="width: 2%";></th>
                                                    
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













    <script>    

        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
        });
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
        function validarNro(e) {
            var key;
            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            if (key < 48 || key > 57) {
                if (key == 46 || key == 8)// || key == 44) // Detectar . (punto) , backspace (retroceso) y , (coma)
                { return true; }
                else { return false; }
            }
            return true;
        }
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
