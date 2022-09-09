<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductosABM.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.ProductosABM" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        #step-content {
            position: inherit;
        }

        .hide {
            display: none;
        }
        
            .lblImgDocF:hover
            {
                cursor: pointer;
            }
    </style>


    <div class="row wrapper border-bottom white-bg page-heading">
       <%-- <div class="col-lg-10">
            <h2>Notificaciones</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li class="active">
                    <strong>Notificaciones </strong><span id="resolution"></span>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>--%>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">

                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-title">
                            <%--<h5>Basic Wizzard</h5>--%>
                            <div class="ibox-tools">
                                <%--<a class="collapse-link">
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
                                </a>--%>
                            </div>
                        </div>
                        <fieldset class="ibox-content">
                            <%--<p>
                                This is basic example of Step
                            </p>--%>
                            <%--<form id="form" action="#" class="wizard-big" >--%>
                            <form id="form" action="#" class="wizard-big">
                                <h1>DESCRIPCION</h1>
                                <fieldset>
                                    <h1>Datos del producto</h1>
                                    <div class="row">
                                        <div class="col-lg-8">
                                            <div class="form-group">
                                                <label>Descripcion *</label>
                                                <input id="ProdDescripcion" onchange="ActualizarLabels()" name="ProdDescripcion" type="text" class="form-control required" />
                                                <%--<p id="valUser" class="hide">Ingresar usuario</p>--%>
                                            </div>
                                              <div class="form-group">
                                            <label id="valDocFrente">Imagen del producto</label><br/>
                                            <div class="image-crop btn-group">
                                                <%--<label for="ContentPlaceHolder1_inputImage2" style="display:contents" class="lblImgDocF">--%>
                                                <label for="ContentPlaceHolder1_inputImage2" class="lblImgDocF">
                                                    <img src="../../Img/photo.png" id="imgDocF" width="35%"
                                                        height="35%"/>
                                                </label>
                                                <%--<input type="file" accept="image/*" name="file2" id="inputImage2" class="hide">--%>
                                                <%--<input type="file" id="inputImage2" name="file2" class="hide" accept="image/*" onchange="cargarImagen()"/>--%>
                                                <asp:FileUpload ID="inputImage2" runat="server" CssClass="hide" />
                                            </div>

                                        </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="text-center">
                                                <div style="margin-top: 20px">
                                                    <i class="fa fa-sign-in" style="font-size: 180px; color: #e5e5e5"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                      

                                    </div>

                                </fieldset>
                                <h1>CATEGORIA Y ATRIBUTOS</h1>
                                <fieldset>
                                    <h1><label id="lblDescripcion1"></label> </h1>
                                    <div class="text-center m-t-md">
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
                                            <label class="col-sm-2 control-label editable">Producto final</label>
                                            <div class="col-sm-7">
                                                <div class="input-group m-b">
                                                    <asp:CheckBox ID="cbxGestion" onclick="mostrarGestion()" runat="server" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row" runat="server" id="divGrupo" style="margin-top: 2%; display: none;">
                                            <label class="col-sm-2 control-label editable">Grupo</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ListGrupo" onchange="actualizarSubGrupo()" class="form-control m-b" runat="server">
                                                </asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hiddenGrupo" />
                                            </div>

                                        </div>
                                        <div class="row" runat="server" id="divSubgrupo" style="margin-top: 2%; display: none">
                                            <label class="col-sm-2 control-label editable">Subgrupo</label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="ListSubgrupo" onchange="actualizarHiddenSubGrupo()" class="form-control m-b" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:HiddenField runat="server" ID="hiddenSubGrupo" />

                                        </div>


                                    </div>
                                </fieldset>

                                <h1>COSTOS</h1>
                                <fieldset>
                                    <h1><label id="lblDescripcion2"></label> </h1>
                                    <div class="row" style="margin-top: 2%">
                                        <label class="col-sm-2 control-label editable">Costo</label>
                                        <div class="col-sm-7">
                                            <div class="input-group m-b">
                                                <span class="input-group-addon">$</span><asp:TextBox ID="txtCosto" onchange="ActualizarLabelCosto('valiva')" onkeypress="javascript:return validarNro(event)" class="form-control required" runat="server" />
                                            </div>
                                                   
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 2%">
                                        <label class="col-sm-2 control-label editable">Alicuota IVA</label>
                                        <div class="col-sm-7">
                                            <div class="input-group m-b">
                                                <asp:DropDownList ID="ListAlicuota" onchange="ActualizarLabelIVA()" class="form-control m-b valid" runat="server">
                                                </asp:DropDownList><span class="input-group-addon">%</span>
                                            </div>
                                            <p id="valiva" class="text-danger text-hide">
                                                *Seleccione un valor de IVA.
                                            </p>
                                        </div>

                                    </div>
                                </fieldset>
                                
                                <h1>UNIDADES DE MEDIDA Y PRESENTACIÓN</h1>
                                <fieldset>
                                    <h1><label id="lblDescripcion3"></label> </h1>
                                    <div class="row" style="margin-top: 2%">
                                        <label class="col-sm-2 control-label editable">Unidad de medida</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ListUnidadMedida" onchange="ActualizarLabelUnidad('valMedida')" class="form-control m-b" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <p id="valMedida" class="text-danger text-hide">
                                                *Seleccione un valor de Medida.
                                            </p>
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
                                </fieldset>
                                <h1>REVISION</h1>
                                <fieldset>
                                    <h1><label id="lblDescripcion4"></label> </h1>
                                   <%-- <input id="acceptTerms" name="acceptTerms" type="checkbox" class="required"/>
                                    <label for="acceptTerms">I agree with the Terms and Conditions.</label>--%>
                                     <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Descripcion</label>
                                          <div class="col-sm-7">
                                            <label id="lblDescripcionFinal" ></label>
                                        </div>
                                     </div>
                                      <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Categoria</label>
                                          <div class="col-sm-7">
                                            <label id="lblCategoriaFinal"></label>
                                        </div>
                                     </div>
                                     <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Atributo</label>
                                          <div class="col-sm-7">
                                            <label id="lblAtributoFinal"></label>
                                        </div>
                                     </div>
                                      <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Costo</label>
                                          <div class="col-sm-7">
                                            $<label id="lblCosto"></label>
                                        </div>
                                     </div>
                                    <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Alicuota IVA</label>
                                          <div class="col-sm-7">
                                            <label id="lbliva"></label>
                                        </div>
                                     </div>
                                      <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Unidad de medida</label>
                                          <div class="col-sm-7">
                                            <label id="lblUnidadMedida"> </label>
                                        </div>
                                     </div>
                                    <div class="row" style="margin-top: 2%">
                                         <label class="col-sm-2 control-label editable" >Presentacion</label>
                                          <div class="col-sm-7">
                                            <label> aca va una grilla</label>
                                        </div>
                                     </div>
                                  
                                </fieldset>
                            </form>
                            </fieldset>
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
                                                        <div class="table-responsive">
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

        <!-- Mainly scripts -->
        <script src="/../js/jquery-2.1.1.js"></script>
        <!-- Mainly scripts -->
        <%--<script src="/../Scripts/jquery-3.4.1.js"></script>--%>
        <script src="../../Scripts/bootstrap.min.js"></script>
        <script src="../../Scripts/plugins/metisMenu/jquery.metisMenu.js"></script>
        <%--<script src="../../Scripts/plugins/slimscroll/jquery.slimscroll.min.js"></script>--%>

        <!-- Custom and plugin javascript -->
        
        <%--<script src="../Scripts/plugins/pace/pace.min.js"></script>--%>

        <!-- Steps -->
        <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>

        <!-- Jquery Validate -->
        <script src="/../Scripts/plugins/validate/jquery.validate.min.js"></script>


        <%--<script src="../../Scripts/plugins/iCheck/icheck.min.js"></script>--%>
        <%--<script src="/../Scripts/plugins/summernote/summernote.min.js"></script>--%>

        <%--<script src="../../Scripts/plugins/toastr/toastr.min.js"></script>--%>

        <script src="//cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
        <link href="//cdn.datatables.net/1.10.2/css/jquery.dataTables.css" rel="stylesheet" />
    </div>
        <script type="text/javascript">
            "use strict";
            var $image = $("#imgDocF")

            var $inputImageF = $("#<%=inputImage2.ClientID%>");

            if (window.FileReader) {
                //inputImageF.addEventListener("change",
                $("#form").on('change', '#ContentPlaceHolder1_inputImage2', function () {
                    var fileReader = new FileReader(),
                        files = this.files,
                        file;
                    if (!files.length) {
                        return;
                    }
                    file = files[0];
                    if (/^image\/\w+$/.test(file.type)) {
                        fileReader.readAsDataURL(file);
                        fileReader.onload = function () {
                            $("#imgDocF").attr("src", this.result)
                        };
                    } else {
                        showMessage("Please choose an image file.");
                    }
                });
            }
        </script>
    <script>
        $(document).ready(function () {
            $("#wizard").steps();
            $("#form").steps({

                labels: {
                    cancel: "Cancelar",
                    previous: 'Anterior',
                    next: 'Siguiente',
                    finish: 'Finalizar',
                    current: ''
                },
                bodyTag: "fieldset",
                onStepChanging: function (event, currentIndex, newIndex) {
                    // Always allow going backward even if the current step contains invalid fields!
                    if (currentIndex > newIndex) {
                        return true;
                    }

                    // Forbid suppressing "Warning" step if the user is to young
                    if (newIndex === 3 && Number($("#age").val()) < 18) {
                        return false;
                    }

                    let valCorreo = true
                    let selectCorreo = document.getElementById('<%=ListAlicuota.ClientID%>').value
                    if (selectCorreo == -1 && currentIndex == 2) {
                        valCorreo = false
                        document.getElementById('valiva').className = 'text-danger'
                        return false
                    }

                    let valMedida = true
                    let selectMedida = document.getElementById('<%=ListUnidadMedida.ClientID%>').value
                    if (selectMedida == -1 && currentIndex == 3) {
                        valMedida = false
                        document.getElementById('valMedida').className = 'text-danger'
                        return false
                    }

                    var form = $(this);

                    // Clean up if user went backward before
                    if (currentIndex < newIndex) {
                        // To remove error styles
                        $(".body:eq(" + newIndex + ") label.error", form).remove();
                        $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
                    }

                    // Disable validation on fields that are disabled or hidden.
                    form.validate().settings.ignore = ":disabled,:hidden";

                    // Start validation; Prevent going forward if false
                    return form.valid();
                },
                onStepChanged: function (event, currentIndex, priorIndex) {
                    // Suppress (skip) "Warning" step if the user is old enough.
                    if (currentIndex === 2 && Number($("#age").val()) >= 18) {
                        $(this).steps("next");
                    }

                    // Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
                    if (currentIndex === 2 && priorIndex === 3) {
                        $(this).steps("previous");
                    }
                },
                onFinishing: function (event, currentIndex) {
                    var form = $(this);

                    // Disable validation on fields that are disabled.
                    // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
                    form.validate().settings.ignore = ":disabled";

                    // Start validation; Prevent form submission if false
                    return form.valid();
                },
                onFinished: function (event, currentIndex) {
                    var form = $(this);

                    // Submit form input
                    form.submit();
                }
            })
                .validate({
                errorPlacement: function (error, element) {
                    element.before(error);
                },
                rules: {
                    confirm: {
                        equalTo: "#password"
                    }
                }
            });
        });
    </script>

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
    <script>
        function ActualizarLabels() {
            let descripcion = document.querySelector('#ProdDescripcion').value;
            document.querySelector('#lblDescripcion1').textContent = descripcion; 
            document.querySelector('#lblDescripcion2').textContent = descripcion;
            document.querySelector('#lblDescripcion3').textContent = descripcion;
            document.querySelector('#lblDescripcion4').textContent = descripcion;
            document.querySelector('#lblDescripcionFinal').textContent = descripcion;

        }
        function ActualizarLabelCosto(id) {
            document.getElementById(id).className = 'text-hide';
            let Costo = document.getElementById('<%=txtCosto.ClientID%>').value;
            document.querySelector('#lblCosto').textContent = Costo;
        }
        function ActualizarLabelIVA() {
            let select = document.getElementById('<%=ListAlicuota.ClientID%>');
            let value = select.options[select.selectedIndex].text;

            document.querySelector('#lbliva').textContent = value;
        }
        function ActualizarLabelUnidad(id) {
            document.getElementById(id).className = 'text-hide';
            let select = document.getElementById('<%=ListUnidadMedida.ClientID%>');
            let value = select.options[select.selectedIndex].text;
            document.querySelector('#lblUnidadMedida').textContent = value;
        }
        
    </script>

   

</asp:Content>
