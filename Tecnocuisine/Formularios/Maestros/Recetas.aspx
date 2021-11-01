<%@ Page Title="Recetas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recetas.aspx.cs" Inherits="Tecnocuisine.Recetas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Recetas</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
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
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phRecetas" runat="server"></asp:PlaceHolder>
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

    <div id="modalTabsProductos" class="modal" style="z-index:2000;" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                 <div class="row">
                <div class="col-lg-12">
                    <div class="panel blank-panel">

                        <div class="panel-heading">
                            <div class="panel-title m-b-md"></div>
                            <div class="panel-options">

                                <ul class="nav nav-tabs">
                                    <li class="active"><a data-toggle="tab" href="#tab-1">Productos</a></li>
                                    <li class=""><a data-toggle="tab" href="#tab-2">Recetas</a></li>
                                </ul>
                            </div>
                        </div>

                        <div class="panel-body">

                            <div class="tab-content">
                                <div id="tab-1" class="tab-pane active">
                                    <table class="table table-bordered table-hover" id="editable2">
                        <thead>
                            <tr>
                                <th style="width: 10%">Cod. Producto</th>
                                <th style="width: 20%">Descripcion</th>
                                <th style="width: 10%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="phProductosAgregar" runat="server" />
                        </tbody>
                    </table>
                                </div>

                                <div id="tab-2" class="tab-pane">
                                    <table class="table table-bordered table-hover" id="editable3">
                        <thead>
                            <tr>
                                <th style="width: 10%">Cod. Receta</th>
                                <th style="width: 20%">Descripcion</th>
                                <th style="width: 10%"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="phRecetasModal" runat="server" />
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

    <div id="modalAgregarAtributo" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Agregar</h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Categoria</label>
                        <div class="col-sm-7">

                            <asp:TextBox ID="txtDescripcionCategoria" class="form-control" disabled="disabled" runat="server" />
                            <asp:HiddenField ID="idCategoria" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" ID="btnCategorias" class="btn btn-primary dim" data-toggle="modal" href="#modalCategoria"><i style="color: white" runat="server" class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Atributo</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtDescripcionAtributo"  disabled="disabled" class="form-control" runat="server" />
                            <asp:HiddenField ID="idAtributo" runat="server" />

                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton runat="server" disabled="disabled" ID="btnAtributos" class="btn btn-primary dim" data-toggle="modal" href="#modalAtributo"><i style="color: white"  class="fa fa-plus"></i></asp:LinkButton>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <asp:HiddenField id="hiddenidReceta" runat="server"/>
                    <asp:LinkButton runat="server" ID="btnAgregar" class="buttonLoading btn btn-primary" style="margin-bottom: 0px !important;" OnClick="btnAgregar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
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

    <div id="modalAgregar" class="modal" style="z-index:1999;"  tabindex="-1" role="dialog">
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
                            <asp:TextBox ID="txtDescripcionReceta" class="form-control" runat="server" />
                            <asp:HiddenField ID="Hiddentipo" runat="server" />
                        </div>


                    </div>
                     <div class="row" style="margin-top: 2%;margin-left: 15%">
                        <label class="col-sm-2 control-label editable">Peso Bruto</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtPesoBruto" style="margin-left:6.5%;text-align:right" disabled="disabled" class="form-control" runat="server" />
                            <asp:HiddenField runat="server" ID="hiddenPeso"/>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 2%;margin-left: 15%">
                        <label class="col-sm-2 control-label editable">Rinde</label>
                        <div class="col-md-6">
                            <asp:TextBox style="margin-left:6.5% ; text-align:right;"  ID="txtRinde"   class="form-control" runat="server" />
                        </div>
                    </div>
                   <div class="row" style="margin-top: 2%;margin-left: 15%">
                        <label class="col-sm-2 control-label editable">Merma</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtMerma" style="margin-left:6.5%;text-align:right"  class="form-control" runat="server" />
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%;margin-left: 15%">
                        <label class="col-sm-2 control-label editable">Desperdicio</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtDesperdicio" style="margin-left:6.5%;text-align:right" class="form-control" runat="server" />
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%;margin-left: 15%">
                        <label class="col-sm-2 control-label editable">Coeficiente</label>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtCoeficiente" style="margin-left:6.5%;text-align:right" disabled="disabled" class="form-control" runat="server" />
                            <asp:HiddenField runat="server" ID="hiddenCoeficiente"/>
                        </div>

                    </div>
                    <hr class="my-4" />
                    <div class="row" style="margin-top: 2%">
                        <label class="col-sm-2 control-label editable">Productos</label>
                        <div class="col-sm-7">

                            <asp:TextBox ID="txtDescripcionProductos" disabled="disabled" class="form-control" runat="server" />
                            <asp:HiddenField ID="idProducto" runat="server" />
                        </div>

                        <div class="col-sm-2">
                            <linkButton  ID="btnProductos" class="btn btn-primary dim" data-toggle="modal" href="#modalTabsProductos"><i style="color: white"  class="fa fa-plus"></i></linkButton>
                        </div>

                    </div>
                    <div class="row" style="margin-top: 2%; margin-bottom: 2%;">
                        <label class="col-sm-2 control-label editable">Cantidad</label>
                        <div class="col-sm-7">
                            <asp:TextBox ID="txtCantidad" style="text-align:right" class="form-control" runat="server" />
                        </div>
                         <div class="col-sm-2">
                            <linkButton  ID="btnAgregarProducto" onclick="agregarProductoPH();" class="btn btn-primary dim" ><i style="color: white"   class="fa fa-check"></i></linkButton
                        </div>
                    </div>
                    
                    
                    <table class="table table-bordered table-hover"  id="tableProductos">
                        <thead>
                            <tr>
                                <th style="width: 10%">Cod. Producto</th>
                                <th style="width: 10%">Tipo</th>
                                <th style="width: 20%">Descripcion</th>
                                <th style="width: 10%">Cantidad</th>
                                <th style="width: 10%"></th>
                            </tr>
                        </thead>
                        <tbody> 
                            <asp:PlaceHolder ID="phProductos" runat="server" />
                        </tbody>
                    </table>


                </div>

                <div class="modal-footer" style="margin-top: 7%">
                    <asp:LinkButton runat="server" ID="btnGuardar" style="margin-bottom: 0px !important;" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:HiddenField runat="server" ID="idProductosRecetas" />
                    <asp:HiddenField runat="server" ID="hiddenReceta" />

                </div>
            </div>
        </div>
    </div>














    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>

     <script>           
         function openModalAtributos() {
             $('#modalAgregarAtributo').modal('show');
         }
     </script>




        <script>
            function cargarProductos() {
                var hiddenReceta = document.getElementById('ContentPlaceHolder1_hiddenReceta');
                $.ajax({
                    method: "POST",
                    url: "Recetas.aspx/GetIngredientes",
                    data: '{id: "' + hiddenReceta.value + '" }',
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                        //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                    },
                    success: successAgregarColumnasIngredientes
                });

                function successAgregarColumnasIngredientes(response) {
                    var ingredientes = JSON.parse(response.d).split(';');

                    for (var x = 0; x < ingredientes.length; x++) {
                        if (ingredientes[x] != "") {
                            var ingrediente = ingredientes[x].split(',');
                            var codigo = ingrediente[0];
                            var tipo = ingrediente[1];
                            var descripcion = ingrediente[3];

                            var cantidad = ingrediente[2];
                            $('#tableProductos').append(
                                "<tr id=" + tipo + "_" + codigo + ">" +
                                "<td style=\" text-align: right\"> " + codigo + "</td>" +
                                "<td> " + tipo + "</td>" +
                                "<td> " + descripcion + "</td>" +
                                "<td style=\" text-align: right\"> " + cantidad + "</td>" +
                                "<td style=\" text-align: center\"> <a style=\"padding: 0% 5% 2% 5.5%;\" class=\"btn btn-danger \" onclick=\"javascript: return borrarProd('" + tipo + "_" + codigo + "');\" >" +
                                "<i class=\"fa fa-trash - o\"></i> </a> " +
                                "</td > " +
                                "</tr>"
                            );

                            if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                                document.getElementById('<%= idProductosRecetas.ClientID%>').value += codigo + "," + tipo + "," + cantidad + "," + tipo + "_" + codigo;
                            }
                            else {
                                document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + codigo + "," + tipo + "," + cantidad + "," + tipo + "_" + codigo;
                            }
                            if (ContentPlaceHolder1_txtPesoBruto.value != "") {
                                ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) + parseFloat(cantidad);
                            }
                            else {
                                ContentPlaceHolder1_txtPesoBruto.value = 0;
                                ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) + parseFloat(cantidad);
                            }
                            ContentPlaceHolder1_hiddenPeso.value = ContentPlaceHolder1_txtPesoBruto.value;
                        }
                    }
                    $('#modalAgregar').modal('show');
                }
            }

        </script>




    <script>
        function agregarProductoPH() {
            var codigo = ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0];
            var cantidad = ContentPlaceHolder1_txtCantidad.value;
            var tipo = ContentPlaceHolder1_Hiddentipo.value;
            if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + codigo)) {
                $('#tableProductos').append(
                    "<tr id=" + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0] + "\">" +
                    "<td style=\" text-align: right\"> " + codigo + "</td>" +
                    "<td> " + tipo + "</td>" +
                    "<td> " + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[1] + "</td>" +
                    "<td style=\" text-align: right\"> " + cantidad + "</td>" +
                    "<td style=\" text-align: center\"> <a style=\"padding: 0% 5% 2% 5.5%;\" class=\"btn btn-danger \" onclick=\"javascript: return borrarProd('" + tipo + "_" + codigo + "');\" >" +
                    "<i class=\"fa fa-trash - o\"></i> </a> " +
                    "</td > " +
                    "</tr>"
                );

                if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += codigo + "," + tipo + "," + cantidad + "," + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0];
                }
                else {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + codigo + "," + tipo + "," + cantidad + "," +  ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0];
                }
                if (ContentPlaceHolder1_txtPesoBruto.value != "") {
                    ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) + parseFloat(cantidad);

                }
                else {
                    ContentPlaceHolder1_txtPesoBruto.value = 0;
                    ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) + parseFloat(cantidad);
                }
                ContentPlaceHolder1_hiddenPeso.value = ContentPlaceHolder1_txtPesoBruto.value;
                if (ContentPlaceHolder1_txtRinde.value != "") {
                    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2);
                    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                }
                ContentPlaceHolder1_txtDescripcionProductos.value = "";
                ContentPlaceHolder1_txtCantidad.value = "";


            }
        }

        function borrarProd(idprod) {
            event.preventDefault();
            $('#' + idprod).remove();
            var productos = ContentPlaceHolder1_idProductosRecetas.value.split(';');
            var nuevosProductos = "";
            for (var x = 0; x < productos.length; x++) {
                if (productos[x] != "") {
                    if (!productos[x].includes(idprod)) {
                        nuevosProductos += productos[x] + ";";
                    }
                    else {
                        var productoAEliminar = productos[x].split(',')[2];
                        ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) - parseFloat(productoAEliminar);
                        if (ContentPlaceHolder1_txtRinde.value != "") {
                            ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2);
                            ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                        }
                    }
                }
            }
            ContentPlaceHolder1_idProductosRecetas.value = nuevosProductos;
        }
    </script>

    <script>
        function agregarProducto(clickedId) {
            ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[2] + ' - ' + clickedId.split('_')[3];
            ContentPlaceHolder1_Hiddentipo.value = "Producto";
            $('#modalTabsProductos').modal('hide');

        }
    </script>
    <script>
        function actualizarBotonAtributo(idReceta) {
            $.ajax({
                method: "POST",
                url: "Recetas.aspx/GetSubAtributos",
                data: '{id: "' + idReceta + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: successAtributo
            });
            function successAtributo(response) {
                var obj = JSON.parse(response.d);
                if (obj.split(',')[0] != 'true') {
                    $(ContentPlaceHolder1_hiddenidReceta).attr('Value', idReceta);
                    ContentPlaceHolder1_txtDescripcionCategoria.value = "";
                    ContentPlaceHolder1_idCategoria.value = "";
                    ContentPlaceHolder1_txtDescripcionAtributo.value = "";
                    ContentPlaceHolder1_idAtributo.value = "";
                    $('#modalAgregarAtributo').modal('show');

                }
                else {
                    window.location.replace(location.protocol + '//' + location.host + location.pathname + '?a=3&i='+obj.split(',')[1]);
                }
            }
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
            openModalAtributos();
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

    <script>
        function agregarReceta(clickedId) {
            ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[2] + ' - ' + clickedId.split('_')[3]
            ContentPlaceHolder1_Hiddentipo.value = "Receta";
            $('#modalTabsProductos').modal('hide');


        }
        function vaciarFormulario() {
            ContentPlaceHolder1_hiddenEditar.value = "";
            ContentPlaceHolder1_txtDescripcionReceta.value = "";
            ContentPlaceHolder1_txtPesoBruto.value = "";
            ContentPlaceHolder1_txtRinde.value = "";
            ContentPlaceHolder1_txtDesperdicio.value = "";
            ContentPlaceHolder1_txtMerma.value = "";
            ContentPlaceHolder1_txtCoeficiente.value = "";
            ContentPlaceHolder1_txtDescripcionProductos.value = "";
            ContentPlaceHolder1_txtCantidad.value = "";
            $("#tableProductos > tbody").html("");
            window.history.pushState('', 'Recetas', location.protocol + '//' + location.host + location.pathname);

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
            var oTable3 = $('#editable3').dataTable();

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

            oTable3.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable3.fnGetPosition(this);
                    oTable3.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable3.fnGetPosition(this)[2]
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

          
            $("#ContentPlaceHolder1_txtRinde").on('change paste', function () {
                if (ContentPlaceHolder1_txtRinde.value == "") {
                    $("#ContentPlaceHolder1_txtMerma").removeAttr('disabled')
                    $("#ContentPlaceHolder1_txtDesperdicio").removeAttr('disabled')
                }
                else {
                    $("#ContentPlaceHolder1_txtMerma").attr("disabled", "disabled");
                    $("#ContentPlaceHolder1_txtDesperdicio").attr("disabled", "disabled");
                }
              
                if (ContentPlaceHolder1_txtPesoBruto.value != "") {
                    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2)
                    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;
                    
                }
            });

            $("#ContentPlaceHolder1_txtDesperdicio").on('change paste', function () {
                if (ContentPlaceHolder1_txtDesperdicio.value == "" && ContentPlaceHolder1_txtMerma.value == "") {
                    $("#ContentPlaceHolder1_txtRinde").removeAttr('disabled')
                }
                else {
                    $("#ContentPlaceHolder1_txtRinde").attr("disabled", "disabled");
                }

                if (ContentPlaceHolder1_txtMerma.value != "" && ContentPlaceHolder1_txtDesperdicio.value != "") {
                    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtMerma.value) * parseFloat(ContentPlaceHolder1_txtDesperdicio.value)).toFixed(2)
                    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;
                }
                else {
                    ContentPlaceHolder1_txtCoeficiente.value = "";
                    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;
                }
            });

            $("#ContentPlaceHolder1_txtMerma").on('change paste', function () {
                if (ContentPlaceHolder1_txtMerma.value == "" && ContentPlaceHolder1_txtDesperdicio.value == "") {
                    $("#ContentPlaceHolder1_txtRinde").removeAttr('disabled')
                }
                else {
                    $("#ContentPlaceHolder1_txtRinde").attr("disabled", "disabled");
                }

                if (ContentPlaceHolder1_txtDesperdicio.value != "" && ContentPlaceHolder1_txtMerma.value != "") {
                    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtMerma.value) * parseFloat(ContentPlaceHolder1_txtDesperdicio.value)).toFixed(2)
                    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                }
                else {
                    ContentPlaceHolder1_txtCoeficiente.value = "";
                    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;
                }
            });

            var updateOutput = function (e) {

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


</asp:Content>
