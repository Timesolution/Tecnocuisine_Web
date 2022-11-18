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

            .wizard > .content > .body {
    float: left;
    /* position: absolute; */
    width: 95%;
    height: 95%;
    padding: 2.5%;
}
            .wizard > .steps{
                display:none
            }
            .form-group{
                height:35px;
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
                        
                        <div class="ibox-content">
                            <%--<p>
                                This is basic example of Step
                            </p>--%>
                            <%--<form id="form" action="#" class="wizard-big" >--%>
                            <form id="form" action="#" class="wizard-big">
                                <h1 style="display:none" >DESCRIPCION</h1>
                                <fieldset style="position: relative">

                                    <div class="row">
                                        <div class="col-lg-6">

                                            <%-- <div class="form-group">
                                            <label id="valDocFrente">Imagen del producto</label><br/>
                                            <div class="image-crop btn-group">
                                                <label for="ContentPlaceHolder1_inputImage2" style="display:contents" class="lblImgDocF">
                                                <label for="inputImage2" style="display:contents" class="lblImgDocF">
                                                    <img src="../../Img/photo.png" id="imgDocF" width="20%"
                                                        height="20%"/>
                                                </label>
                                                <input type="file" accept="image/*" name="file2" id="inputImage2" class="hide"/>
                                                <input type="file" id="inputImage2" name="file2" class="hide" accept="image/*" onchange="cargarImagen()"/>
                                                <asp:FileUpload ID="inputImage2" runat="server" CssClass="hide" />
                                            </div>

                                        </div>--%>
                                            <div >
                                                <div class="row">
                                                    <div class="col-md-4">

                                                        <label>Descripcion *</label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox ID="ProdDescripcion" onchange="ActualizarLabels()" runat="server"  class="form-control required"></asp:TextBox>
                                                        <%--<input id="ProdDescripcion" onchange="ActualizarLabels()" name="ProdDescripcion" type="text" class="form-control required" />--%>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 2%">

                                                    <label class="col-sm-4 control-label editable">Categoria</label>
                                                    <div class="col-sm-8">
                                                        <datalist id="listaCategoria" runat="server"></datalist>
                                                        <div class="input-group">

                                                            <asp:TextBox ID="txtDescripcionCategoria" onfocusout="agregarDesdetxtCategoria(); return false;" list="ContentPlaceHolder1_listaCategoria" class="form-control" runat="server" />
                                                            <span class="input-group-btn">

                                                                <asp:LinkButton runat="server" ID="btnCategorias" class="btn btn-primary dim" data-toggle="modal" data-keyboard="false" data-backdrop="static" href="#modalCategoria"><i style="color: white" runat="server" class="fa fa-plus"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                        <asp:HiddenField ID="idCategoria" runat="server" />
                                                    </div>




                                                </div>
                                                <div class="row" style="margin-top: 2%">
                                                    <label class="col-sm-4 control-label editable">Atributo</label>
                                                    <div class="col-sm-8">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtDescripcionAtributo" class="form-control required" disabled="disabled" runat="server" />
                                                            <span class="input-group-btn">

                                                                <asp:LinkButton runat="server" disabled="disabled" ID="btnAtributos" class="btn btn-primary dim" data-toggle="modal" href="#modalAtributo"><i style="color: white"  class="fa fa-plus"></i></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                        <asp:HiddenField ID="idAtributo" runat="server" />

                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>

                                                </div>
                                                <div class="row" style="margin-top: 2%">
                                                    <label class="col-sm-4 control-label editable">Producto final</label>
                                                    <div class="col-sm-8">
                                                        <div class="input-group m-b">
                                                            <asp:CheckBox ID="cbxGestion" onclick="mostrarGestion()" runat="server" />
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row" runat="server" id="divGrupo" style="display: none;">
                                                    <label class="col-sm-4 control-label editable">Grupo</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ListGrupo" onchange="actualizarSubGrupo()" class="form-control m-b" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField runat="server" ID="hiddenGrupo" />
                                                    </div>

                                                </div>
                                                <div class="row" runat="server" id="divSubgrupo" style="display: none">
                                                    <label class="col-sm-4 control-label editable">Subgrupo</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ListSubgrupo" onchange="actualizarHiddenSubGrupo()" class="form-control m-b" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <asp:HiddenField runat="server" ID="hiddenSubGrupo" />

                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="row">
                                                <label class="col-sm-4 control-label editable">Costo</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        <span class="input-group-addon">$</span><asp:TextBox ID="txtCosto" onchange="ActualizarLabelCosto('valiva')" onkeypress="javascript:return validarNro(event)" class="form-control required" runat="server" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 2%">
                                                <label class="col-sm-4 control-label editable">Alicuota IVA</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ListAlicuota" onchange="ActualizarLabelIVA()" class="form-control m-b valid" runat="server">
                                                        </asp:DropDownList><span class="input-group-addon">%</span>
                                                    </div>
                                                    <p id="valiva" class="text-danger text-hide">
                                                        *Seleccione un valor de IVA.
                                                    </p>
                                                </div>

                                            </div>
                                            <div class="row" style="margin-top: 2%">
                                                <label class="col-sm-4 control-label editable">Unidad de medida</label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ListUnidadMedida" onchange="ActualizarLabelUnidad('valMedida')" class="form-control m-b" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <p id="valMedida" class="text-danger text-hide">
                                                    *Seleccione un valor de Medida.
                                                </p>
                                            </div>
                                            <div class="row" style="margin-top: 2%">
                                                <label class="col-sm-4 control-label editable">Presentacion</label>
                                                <div class="col-sm-8">
                                                    <datalist id="ListOptionsPresentacion" runat="server"></datalist>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtDescripcionPresentacion" list="ContentPlaceHolder1_ListOptionsPresentacion" onkeypress="AgregarPresentacionDesdeTxt(event)" class="form-control" runat="server" />
                                                        <span class="input-group-btn">
                                                            <asp:LinkButton runat="server" ID="btnPresentacion" OnClientClick="editarPresentaciones()" class="btn btn-primary dim" data-toggle="modal" href="#modalPresentacion"><i style="color: white"  class="fa fa-plus"></i></asp:LinkButton>
                                                        </span>
                                                    </div>
                                                    <asp:HiddenField ID="idPresentacion" runat="server" />
                                                </div>
                                               
                                            </div>
                                            <div class="row" style="margin-top: 2%; padding-left: 15px">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive col-lg-12 center-block" style="max-width: 97.5%;">
                                                            <table class="table table-striped table-bordered table-hover " id="editable1">

                                                                <thead>
                                                                    <tr>
                                                                        <th style="width: 10%">#</th>
                                                                        <th style="width: 40%">Descripcion</th>
                                                                        <th style="width: 10%">Cantidad</th>
                                                                        <th style="width: 10%"></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tbodyEditable1">
                                                                        <asp:PlaceHolder ID="PHPresentacionFinal" runat="server"></asp:PlaceHolder>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                  

                            <%--  </fieldset>
                                  <h1>CATEGORIA Y ATRIBUTOS</h1>
                                <fieldset style="position: relative;">
                                    <h1><label id="lblDescripcion1"></label> </h1>
                                   
                                </fieldset>

                                <h1>COSTOS</h1>
                                <fieldset  style="position: relative;">
                                    <h1><label id="lblDescripcion2"></label> </h1>
                                   
                                </fieldset>
                                
                                <h1>UNIDADES DE MEDIDA</h1>
                                <fieldset  style="position: relative;">
                                    <h1><label id="lblDescripcion3"></label> </h1>
                                    
                                  
                                </fieldset>--%>
                               <%-- <h1>REVISION</h1>--%>
                                <fieldset  style="position: relative; display:none">
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
                                            <label id="lblPresentacion"></label>
                                        </div>
                                     </div>
                                  
                                </fieldset>
                            </form>
                            </div>
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

                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover " id="editable22">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Descripcion</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="phInsumos" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
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

          <div id="modalCategoria" class="modal" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h2 class="modal-title">Empecemos identificando tu Categoria
                        <span>
                            <%--<i style='color:black;' class='fa fa-search'></i>--%>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                                <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                            </svg>

                        </span>
                        </h2>
                    </div>
                    <div class="modal-body">
                        <div class="input-group m-b">
                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" />
                        </div>

                        <div class="ibox-content">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table class="table table-bordered table-hover" id="editable6">
                                        <thead>
                                            <tr>

                                                <th>Descripcion</th>
                                                <th>Categoria</th>
                                                <%--<th style="width: 20%">Unidad Medida</th>--%>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="PHCategorias" runat="server" />
                                        </tbody>
                                    </table>

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
                            <a id="btnAgregarPresentacion" onclick="agregarPresentaciones()" class=" btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </a>
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                            <asp:HiddenField runat="server" ID="hfPresentaciones" />
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
        <%--<script src="../../js/plugins/nestable/jquery.nestable.js"></script>--%>


        <%--<script src="../../Scripts/plugins/iCheck/icheck.min.js"></script>--%>
        <%--<script src="/../Scripts/plugins/summernote/summernote.min.js"></script>--%>

        <%--<script src="../../Scripts/plugins/toastr/toastr.min.js"></script>--%>

        <script src="//cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
        <link href="//cdn.datatables.net/1.10.2/css/jquery.dataTables.css" rel="stylesheet" />
    </div>
        <script type="text/javascript">
            "use strict";
            var $image = $("#imgDocF")

            var $inputImageF = $("#inputImage2");

            if (window.FileReader) {
                //inputImageF.addEventListener("change",
                $("#form").on('change', '#inputImage2', function () {
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
                    finish: 'Guardar',
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
                    let selectUnidadMedida = document.getElementById('<%=ListUnidadMedida.ClientID%>');
                    let selectAliCuota = document.getElementById('<%=ListAlicuota.ClientID%>');

                    let url = window.location.href;
                    if (!url.includes("a=2")) {
                        $.ajax({
                            method: "POST",
                            url: "ProductosABM.aspx/GuardarProducto",
                            data: '{ descripcion: "' + document.querySelector('#lblDescripcionFinal').textContent
                                + '" , Categoria: "' + document.querySelector('#lblCategoriaFinal').textContent
                                + '" , Atributos: "' + document.querySelector('#lblAtributoFinal').textContent
                                + '" , Costo: "' + document.querySelector('#lblCosto').textContent
                                + '" , IVA: "' + selectAliCuota.selectedOptions[0].value
                                + '" , Unidad: "' + selectUnidadMedida.selectedOptions[0].value
                                + '" , Presentacion: "' + document.querySelector('#lblPresentacion').textContent
                                + '" , cbxGestion: "' + document.getElementById('ContentPlaceHolder1_cbxGestion').checked
                                + '" , img: "' + ""
                                + '"}',
                            contentType: "application/json",
                            dataType: 'json',
                            error: (error) => {
                                console.log(JSON.stringify(error));
                                $.msgbox("No se pudo cargar la tabla", { type: "error" });
                            },
                            success: recargarPagina()
                        });
                    } else {
                        let parameter = url.split("?")[1]
                        let queryString = new URLSearchParams(parameter);
                        let idProd = ''
                        for (let pair of queryString.entries()) {
                            if (pair[0] == "i")
                                idProd = pair[1];
                        }
                        $.ajax({
                            method: "POST",
                            url: "ProductosABM.aspx/EditarProducto",
                            data: '{ descripcion: "' + document.getElementById('<%=ProdDescripcion.ClientID%>').value
                                + '" , Categoria: "' + document.getElementById('<%=txtDescripcionCategoria.ClientID%>').value
                                + '" , Atributos: "' + document.getElementById('ContentPlaceHolder1_txtDescripcionAtributo').value
                                + '" , Costo: "' + document.querySelector('#lblCosto').textContent
                                + '" , IVA: "' + selectAliCuota.selectedOptions[0].value
                                + '" , Unidad: "' + selectUnidadMedida.selectedOptions[0].value
                                + '" , Presentacion: "' + document.getElementById('<%=hfPresentaciones.ClientID%>').value
                                + '" , cbxGestion: "' + document.getElementById('ContentPlaceHolder1_cbxGestion').checked
                                + '" , img: "' + ""
                                + '" , idProducto: "' + idProd
                                + '"}',
                            contentType: "application/json",
                            dataType: 'json',
                            error: (error) => {
                                console.log(JSON.stringify(error));
                                $.msgbox("No se pudo cargar la tabla", { type: "error" });
                            },
                            success: recargarPagina2
                        });
                    }
                   
                    // Start validation; Prevent form submission if false
                    /*return form.valid();*/
                },
                onCanceled: function (event, currentIndex) {
                    var form = $(this);
                    //----------------------------------------------------------------------------- REVISAR

                    // Submit form input
                    window.location.replace('Productos.aspx');
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

            // activate Nestable for list 1
            //$('#nestable3').nestable({
            //    group: 1
            //}).on('change', updateOutput);
            //updateOutput($('#nestable3').data('output', $('#nestable-output')));
            $('#editable6').DataTable({
                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "searchPlaceholder": "Escriba su busqueda",
                    "lengthMenu": "Mostrar _MENU_ Entradas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                },
                "bLengthChange": false,
                "bInfo": false
            });

            $('.dataTables_filter').hide();
            $('#txtBusqueda').on('keyup', function () {
                $('#editable6').DataTable().search(
                    this.value
                ).draw();
            });
        });

        function recargarPagina() {
            window.location.replace('ProductosABM.aspx?m=1');
          
        }
        function recargarPagina2(response) {

            var obj = JSON.parse(response.d);
            toastr.options = { "positionClass": "toast-bottom-right" };
            if (obj == null) {
                alert('Obj es null');
                //return;
            }
            else {
                if (obj.toUpperCase().includes("ERROR")) {
                    toastr.error(obj, "Error");
                }
                else {
                    console.log(obj);
                    toastr.success(obj, "Exito!");
                }
            }

        }
        function GuardarProducto2() {
            var formData = new FormData();
            var files = $('#inputImage2')[0].files[0];
            formData.append('file', files);

            $.ajax({
                method: "POST",
                url: "ProductosABM.aspx/GuardarProducto2",
                data: "{id: '1'}",               
                contentType: false,
                processData: false,
                async:false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: function (rta) {
                    console.log(rta);
                }
            });
        }
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
            let descripcion = document.getElementById('<%=ProdDescripcion.ClientID%>').value;
           
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

   <script>
       function agregarPresentaciones() {
           //let table1 = $('#editable1').DataTable();
           let table2 = document.getElementById('editable2');
           let cuerpor = document.getElementById("tbodyEditable1");
           let max = table2.rows.length;
           let presentacionFinal ='';
           //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
           for (let i = 1; i < max; i++) {
               /*if (i > 1) {*/
               if (table2.rows[i].cells[3].children[0].checked) {
                   
                   presentacionFinal += table2.rows[i].cells[0].innerHTML+" - "+ table2.rows[i].cells[1].innerHTML + ', ';
                       cuerpor.innerHTML += `<tr>
                        <td>${table2.rows[i].cells[0].innerHTML}</td>
                        <td>${table2.rows[i].cells[1].innerHTML}</td>
                        <td>${table2.rows[i].cells[2].innerHTML}</td>
                        <td><a onclick="EliminarFila('${table2.rows[i].cells[0].innerHTML}')" id="ContentPlaceHolder1_btnEliminar_${table2.rows[i].cells[0].innerHTML}" class="btn  btn-xs" style="background-color:transparent;"><span><i style="color:black" class="fa fa-trash - o"></i></span></a> </td>
                        </tr>`
               }
             
           }
          
           $('#modalPresentacion').modal('hide');
           document.querySelector('#lblPresentacion').textContent = presentacionFinal;
           document.getElementById('<%=hfPresentaciones.ClientID%>').value = presentacionFinal;
           return true;
       }

       function EliminarFila(id) {
           let table1 = document.getElementById('editable1');
           let presentacionFinal = document.querySelector('#lblPresentacion').textContent;
           let max = table1.rows.length;

           for (let i = 1; i < max; i++) {
               if (table1.rows[i].cells[0].innerHTML == id) {
                   if (presentacionFinal.includes(table1.rows[i].cells[1].innerHTML)) {
                       let texto = document.getElementById('editable1').rows[i].cells[0].innerHTML + " - " + table1.rows[i].cells[1].innerHTML;
                       document.querySelector('#lblPresentacion').textContent = document.querySelector('#lblPresentacion').textContent.replace(texto + ',', '');
                       document.getElementById('<%=hfPresentaciones.ClientID%>').value = document.getElementById('<%=hfPresentaciones.ClientID%>').value.replace(texto + ',', '');
                   }
                   document.getElementById('editable1').rows[i].remove();
                   return;
               }
           }
       }

       function agregarCategoria(id) {
           ContentPlaceHolder1_txtDescripcionCategoria.value = id.split('_')[2] + ' - ' + id.split('_')[3];
           $('#modalCategoria').modal('hide');
           document.querySelector('#txtBusqueda').value = '';
           let btnAtributos = document.getElementById('ContentPlaceHolder1_btnAtributos');
           btnAtributos.removeAttribute('disabled');
           document.querySelector('#lblCategoriaFinal').textContent = id.split('_')[2] + ' - ' + id.split('_')[3];
           $.ajax({
               method: "POST",
               url: "Categorias.aspx/GetSubAtributos2",
               data: '{id: "' + id.split('_')[2] + '" }',
               contentType: "application/json",
               dataType: 'json',
               error: (error) => {
                   console.log(JSON.stringify(error));
                   $.msgbox("No se pudo cargar la tabla", { type: "error" });
               },
               success: successAgregarTipoAtributo
           });
           
       }

       function agregarDesdetxtCategoria() {
           let btnAtributos = document.getElementById('<%=btnAtributos.ClientID%>');
           btnAtributos.removeAttribute('disabled');

           let txtcat = document.getElementById('<%=txtDescripcionCategoria.ClientID%>').value
           const idOption = document.querySelector('option[value="' + txtcat + '"]').id;

           document.querySelector('#lblCategoriaFinal').textContent = txtcat;

           $.ajax({
               method: "POST",
               url: "Categorias.aspx/GetSubAtributos2",
               data: '{id: "' + idOption.split('_')[1] + '" }',
               contentType: "application/json",
               dataType: 'json',
               error: (error) => {
                   console.log(JSON.stringify(error));
                   $.msgbox("No se pudo cargar la tabla", { type: "error" });
               },
               success: successAgregarTipoAtributo
           });
       }

       function AgregarPresentacionDesdeTxt(e) {
           if (e.keyCode === 13 && !e.shiftKey) {
               let txtPresentacion = document.getElementById('<%=txtDescripcionPresentacion.ClientID%>').value
               const idOption = document.querySelector('option[value="' + txtPresentacion + '"]').id;


               const columns = idOption.split("_");
               let cuerpor = document.getElementById("tbodyEditable1");
               cuerpor.innerHTML += `<tr>
                        <td>${columns[1]}</td>
                        <td>${txtPresentacion}</td>
                        <td>${columns[2]}</td>
                        <td><a onclick="EliminarFila('${columns[1]}')" id="ContentPlaceHolder1_btnEliminar_${columns[1]}" class="btn  btn-xs" style="background-color:transparent;"><span><i style="color:black" class="fa fa-trash - o"></i></span></a> </td>
                        </tr>`;
               let presentacionFinal = idOption.split("_")[1] + " - " + txtPresentacion;
               document.querySelector('#lblPresentacion').textContent += presentacionFinal + ", ";
               document.getElementById('<%=txtDescripcionPresentacion.ClientID%>').value = '';
           }
       }

   </script>
  <script>

      function successAgregarTipoAtributo(response) {
          var obj = JSON.parse(response.d);
          var inputs = document.querySelectorAll('input[type=checkbox]')

          if (obj == null || obj == '') {
              return;
          }

          var tiposAtributos = obj.split(',');

          let table2 = document.getElementById('editable22');
          let cuerpor = document.getElementById("tbodyEditable1");
          let max = table2.rows.length;
          table2.getElementsByTagName('tbody')[0].innerHTML = '';
          //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
          for (let j = 0; j < tiposAtributos.length; j++) {



              $('#editable22').append(`<tr>
                        <td>${tiposAtributos[j].split("_")[0]}</td>
                        <td>${tiposAtributos[j].split("_")[1]}</td>
                        <td style="text-align:right"> <input id="ContentPlaceHolder1_btnSelecAtrib_${tiposAtributos[j].split("_")[0]}_${tiposAtributos[j].split("_")[1]}" class="presentacion radio btn btn-primary btn-xs" type="checkbox" checked> </td>
                        </tr>`);

          }
      }
     
          function agregarAtributos() {
              let table2 = document.getElementById('editable22');
              let cuerpor = document.getElementById("tbodyEditable1");
              let max = table2.rows.length;
              let presentacionFinal = '';
              //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
              for (let i = 1; i < max; i++) {
                  /*if (i > 1) {*/
                  if (table2.rows[i].cells[2].children[0].checked) {

                      presentacionFinal += table2.rows[i].cells[0].innerHTML+ " - " + table2.rows[i].cells[1].innerHTML + ', ';
                    
                  }

              }

              $('#modalAtributo').modal('hide');
              document.getElementById('ContentPlaceHolder1_txtDescripcionAtributo').value = presentacionFinal.trimEnd(', ');
              document.querySelector('#lblAtributoFinal').textContent = presentacionFinal.trimEnd(', ');
              
              return true;
        }
  
  </script>
</asp:Content>
