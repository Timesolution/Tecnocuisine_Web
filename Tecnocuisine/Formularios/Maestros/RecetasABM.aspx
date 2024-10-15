<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecetasABM.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.RecetasABM" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .image-container {
            width: 100px; /* Ancho fijo del contenedor */
            height: 100px; /* Alto fijo del contenedor */
            overflow: hidden; /* Oculta cualquier parte de la imagen que exceda el contenedor */
            position: relative; /* Posiciona de forma relativa para el hijo */
            border-radius: 10px;
        }

        #imgDocF {
            width: 100%; /* Asegura que la imagen ocupe el 100% del ancho del contenedor */
            height: 100%; /* Asegura que la imagen ocupe el 100% del alto del contenedor */
            object-fit: cover; /* Ajusta la imagen para cubrir completamente el contenedor */
            cursor: pointer; /* Cambia el cursor para indicar que es clickeable */
        }

        #step-content {
            position: inherit;
        }

        .hide {
            display: none;
        }

        .lblImgDocF:hover {
            cursor: pointer;
        }

        .col-md-1 {
            padding-left: 2px;
            padding-right: 2px;
        }

        label {
            margin-bottom: auto;
        }

        .well {
            background-color: lightgray;
            border: 1px solid #E3E3D5;
        }

        .jstree-open > .jstree-anchor > .fa-folder:before {
            content: "\f07c";
        }

        .jstree-default .jstree-icon.none {
            width: 0;
        }

        .invalid {
            border-color: #ed5565 !important;
        }

        .wizard > .content > .body {
            width: 100%;
            /*        white-space: normal !important;*/
        }

        /*
        td ul, td a, td li{
            white-space: normal !important;
        }
        .jstree-default .jstree-anchor {
            white-space: normal !important;
        }*/


        .product-container {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 5px;
        }

        .product {
            /*background-color: #f9f9f9;*/
            padding: 5px;
            /*margin: 5px 0;*/
            cursor: pointer;
        }

        .children {
            /*margin-left: 20px;*/
            /*padding: 10px;*/
            padding-left: 0;
            background-color: #e9e9e9;
        }

        .hidden {
            display: none;
        }
    </style>


    <div class="row wrapper border-bottom white-bg page-heading" style="padding-bottom: 0px;">

        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">

                <div class="col-lg-12">
                    <div class="ibox">
                        <%--<div class="ibox-title">
                        
                            <div class="ibox-tools">
                           
                            </div>
                        </div>--%>
                        <div class="ibox-content">

                            <form id="form" action="#" class="wizard-big">
                                <h1>RECETAS</h1>
                                <fieldset style="position: relative;">
                                    <h1>Datos de la receta</h1>
                                    <div class="column">

                                        <div class="row">
                                            <div class="col-lg-7">
                                                
                                                <div class="form-group">
                                                    <label>Codigo *</label>
                                                    <%--<input id="ProdDescripcion" onchange="ActualizarLabels()" name="ProdDescripcion" type="text" class="form-control required" />--%>
                                                    <asp:TextBox ID="txtCodigo" class="form-control required" 
                                                        Style="width: 30%" runat="server" MaxLength="50" oninput="this.value = this.value.replace(/[^A-Za-z0-9\s]/g, '');"/>
                                                </div>
                                                <div class="form-group">
                                                    <label>Receta *</label>
                                                    <%--<input id="ProdDescripcion" onchange="ActualizarLabels()" name="ProdDescripcion" type="text" class="form-control required" />--%>
                                                    <asp:TextBox ID="txtDescripcionReceta" Style="max-width: 300px;" MaxLength="100"
                                                        onchange="ActualizarLabels()" class="form-control required" runat="server" />
                                                    <asp:HiddenField ID="Hiddentipo" runat="server" />
                                                    <asp:HiddenField ID="HiddenUnidad" runat="server" />
                                                    <asp:HiddenField ID="HiddenCosto" runat="server" />
                                                    <%--<p id="valUser" class="hide">Ingresar usuario</p>--%>
                                                </div>

                                                                                                <%--Producto final--%>
                                                <div class="row" style="margin-top: 2%;">
                                                    <div class="form-check">
                                                        <div class="col-sm-4">
                                                            <label class="form-check-label" for="CheckProductoFinal">Se puede vender</label>
                                                        </div>                                 
                                                        <asp:CheckBox ID="CheckProductoFinal" style="transform: scale(1.4);" runat="server" data-toggle="tooltip" data-placement="top" data-original-title="Selecionar como Producto Final" />
                                                    </div>
                                                </div>

                                                <%--Se puede comprar--%>
                                                <div class="row" style="margin-top: 2%">
                                                    <div class="form-check">
                                                        <div class="col-sm-4">
                                                          <label class="form-check-label" for="CheckSePuedeComprar">Se puede comprar</label>
                                                        </div>
                                                        <asp:CheckBox ID="CheckSePuedeComprar" style="transform: scale(1.4);" runat="server" data-toggle="tooltip" data-placement="top" data-original-title="" />
                                                    </div>
                                                </div>
                                                
                                            </div>
<%--                                            <div class="col-lg-2"></div>--%>
                                            <div class="col-lg-5">

                                                <%--Imagen--%>
                                                <div class="form-group">
                                                    <label id="valDocFrente">Imagen de la receta</label><br />
                                                    <div class="image-crop btn-group">
                                                        <%--<label for="ContentPlaceHolder1_inputImage2" style="display:contents" class="lblImgDocF">--%>
                                                        <label for="ContentPlaceHolder1_inputImage2" style="display: contents" class="lblImgDocF">
                                                            
                                                        </label>

                                                        <div class="image-container">
                                                            <img title="" alt="" src="../../Img/photo.png" id="imgDocF" onclick="document.getElementById('inputImage2').click();"/>
                                                        <input type="file" id="inputImage2" class="hide" accept="image/*" onchange="renderizarImagen(event)"/>
                                                        </div>
                                                        <%--<input type="file" accept="image/*" name="file2" id="inputImage2" class="hide">--%>
                                                        <%--<input type="file" id="inputImage2" name="file2" class="hide" accept="image/*" onchange="cargarImagen()"/>--%>
                                                    </div>
                                                </div>



                                              <%--  <div class="text-center">
                                                    <div style="margin-top: 20px">
                                                        <i class="fa fa-sign-in" style="font-size: 180px; color: #e5e5e5"></i>
                                                    </div>

                                                </div>--%>

                                                <%--<div class="row" style="margin-top:5.09%">
                                                    <label class="col-md-3 control-label editable" style="margin-top: 5px">Categorias</label>
                                                    <div class="col-md-9">

                                                        <div class="input-group">
                                                            <datalist id="listaCategoria" runat="server"></datalist>
                                                            <asp:TextBox ID="txtDescripcionCategoria" list="ContentPlaceHolder1_listaCategoria" onfocusout="agregarDesdetxtCategoria(); return false;" onchange="ValidationCategoria()"  class="form-control" runat="server" />
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="btnCategorias" runat="server" class="btn btn-primary dim" onclientclick="FocusSearch()" data-toggle="modal" data-backdrop="static" data-target="#modalCategoria"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>

                                                            </span>
                                                        </div>
                                                        <p id="valCategoria" class="text-danger text-hide">
                                                            *Seleccione una Categoria.
                                                        </p>
                                                    </div>
                                                </div>--%>
                                                
                                                <%--<div class="row" style="margin-top:10% ">

                                                    <label class="col-md-3 control-label editable" style="margin-top: 5px">Atributos</label>
                                                    <div class="col-md-9">

                                                        <div class="input-group" style="text-align: right">
                                                            <asp:TextBox ID="txtDescripcionAtributo" disabled="disabled" class="form-control" runat="server" />
                                                            <span class="input-group-btn">
                                                                <linkbutton id="btnAtributos" class="btn btn-primary dim" onclick="FocusSearch()" data-toggle="modal" disabled="disabled" data-backdrop="static" data-target="#modalAtributo"><i style="color: white" class="fa fa-plus"></i></linkbutton>

                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                            </div>
                                        </div>
                                      
                                    </div>
                                </fieldset>
                                <h1>INGREDIENTES</h1>
                                <fieldset style="position: relative;padding-top: 10px;display: block;padding-bottom: 0px;"">
                                    <h1>
                                        <label id="lblDescripcion1" style="font-size:2rem"></label>
                                    </h1>
                                    <%--<div class="row">
                                        <label class="control-label editable" style="padding-left:15px">Propiedades</label>
                                    </div>--%>
                                    <div class="row" style="display:flex; flex-wrap:wrap; row-gap:1rem">
                                        <%--Tipo de receta--%>
                                        <div class="col-md-2" style="padding-right:0;">
                                            <asp:DropDownList ID="ddlTipoReceta" runat="server" class="form-control" onchange="EsTipoRecetaValido()">
                                                <%--<asp:ListItem Value="-1" Text="Tipo receta"> </asp:ListItem>
                                                <asp:ListItem Value="1" Text="Baño"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Cartoon"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Glaseada"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Masa"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Relleno"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="Salsa"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                             <p id="error-tipo" class="text-danger text-hide">
                                                *Seleccione un Tipo de Receta.
                                            </p>
                                        </div>
                                        <%--Unidad de medida--%>
                                        <div class="col-md-2" style="padding-right:0; margin-right:1rem">
                                            <asp:DropDownList ID="ddlUnidadMedida" onchange="ActualizarUnidades()" runat="server" class="form-control">
                                            </asp:DropDownList>
                                             <p id="valiva" class="text-danger text-hide">
                                                *Seleccione Unidad de medida.
                                            </p>
                                        </div>
                                        <%--Rubro--%>
                                        <div class="col-md-2" style="padding-right:0;padding-left:0;">
                                             <asp:DropDownList ID="ddlRubros" runat="server" class="form-control" onchange="EsRubroValido()">
                                             </asp:DropDownList>
                                              <p id="errorRubro" class="text-danger text-hide">
                                                 *Seleccione un Rubro.
                                             </p>
                                         </div>
                                        <%--Sector--%>
                                        <div class="col-md-3" style="padding-right:0;">
                                            <asp:HiddenField id="HFRecetas" runat="server" />
                                                <div class="input-group">
                                                    <%--<asp:TextBox ID="txtSector" disabled="disabled" placeholder="Sector" class="form-control" runat="server" />
                                                    <span class="input-group-btn">
                                                        <asp:LinkButton runat="server" ID="btnSectores" class="btn btn-primary dim" onclientclick="FocusSearch()" data-toggle="modal" data-backdrop="static" data-target="#modalSectores"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>
                                                    </span>--%>

                                                    <asp:DropDownList ID="ddlSectorProductivo" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>                                                                                          
                                        </div>
                                        
                                        
                                    </div>

                                    <div class="row">
                                        <%--Presentaciones--%>
                                        <div class="col-sm-4" style="padding-right:0;">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtPresentaciones" disabled="disabled" placeholder="Presentaciones" class="form-control" runat="server" />
                                                <span class="input-group-btn">
                                                 <asp:LinkButton runat="server" ID="LinkButton1" class="btn btn-primary dim" data-toggle="modal"  data-keyboard="false" data-backdrop="static" href="#modalPresentacion"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>

                                                     <%--<button  class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-target="#modalPresentacion"> <i style="color: white" class="fa fa-plus"></i></button>--%>
                                                </span>
                                            </div>
                                        </div>
                                        <%--Marcas--%>
                                        <div class="col-sm-4" style="padding-right:0;">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtMarcas" disabled="disabled" placeholder="Marcas" class="form-control" runat="server" />
                                                <span class="input-group-btn">
                                                 <asp:LinkButton runat="server" ID="LinkButton2" class="btn btn-primary dim" data-toggle="modal" data-keyboard="false" data-backdrop="static" data-target="#modalMarca"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>

                                                     <%--<button  class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-target="#modalPresentacion"> <i style="color: white" class="fa fa-plus"></i></button>--%>
                                                </span>
                                            </div>
                                        </div>
                                    </div>

                                    <br />
                                   

                                       <div class="row" style="padding-left:15px; display:flex">

                                           <div class="col-md-1" style="text-align: left; margin-right:1rem">
                                                <label id="lblBrutoTotal" style="margin-bottom: auto;">Kg Br. Total</label>
                                                <asp:TextBox disabled="disabled" Text="0.00" Style="text-align: right;" ID="txtKgBrutTotal" class="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1" style="text-align: left; margin-right:1rem">
                                                <label style="margin-bottom: auto;">Costo total</label>
                                                <asp:TextBox Text="0.00" disabled="disabled" Style="text-align: right;" ID="txtCostoTotal" class="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1" style="text-align: left; margin-right:1rem">
                                                <label id="lblTotalUnidad" style="margin-bottom: auto;margin-top: -25%;"> Rinde </label>
                                                <asp:TextBox Style="text-align: right;" Text="0" type="number" min="0" ID="txtRinde" onkeyUp="" oninput="actualizarCantidadesTabla();ActualizarxPorcion();" class="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1" style="text-align: left; margin-right:1rem">
                                                <label id="lblBrutoUnidad" style="margin-bottom: auto;margin-top: -25%;">Kg Br.</label>
                                                <asp:TextBox disabled="disabled" Text="0.00" Style="text-align: right;" ID="txtKgxPorcion" class="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1" style="text-align: left; margin-right:1rem"">
                                                <label id="lblCosto" style="margin-bottom: auto;margin-top: -25%;">Costo</label>
                                                <asp:TextBox disabled="disabled" Text="0.00" Style="text-align: right;" ID="txtCostoxPorcion" class="form-control" runat="server" />
                                            </div>
                                   

                                                                                                                               
  
                                        <div class="col-md-1" style="text-align: left; margin-right:1rem"">
                                            <label id="rxrt" style="margin-bottom:0px">Pr.Venta</label>
                                            <asp:TextBox Text="0" Style="text-align: right;" ID="txtPrVenta"  onkeyUp="ActualizarxPrVenta()" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="text-align: left; margin-right:1rem"">
                                            <label style="margin-bottom: auto;">Food Cost</label>
                                            <asp:TextBox Text="0%" disabled="disabled" Style="text-align: right;" ID="txtPFoodCost" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="text-align: left; margin-right:1rem"">
                                            <label id="erwt" style="margin-bottom: auto;margin-top: -25%;"> Cont. Marg. </label>
                                            <asp:TextBox Style="text-align: right;" disabled="disabled" Text="0.00" ID="txtContMarg" class="form-control" runat="server" />
                                        </div>
                                        
                                    </div>

                                    <div class="well"style="margin-top:2%;margin-right: -15px;margin-left: -15px;">
                                    <div id="containerAddItem" class="row" style="margin-top: 0.5%; margin-bottom: 2%; display:flex;flex-wrap:nowrap;justify-content:space-between">
                                        <div class="col-md-3">
                                          
                                            <label  style="margin-bottom: 0px;"> Ingredientes </label>
                                             
                                            <div class="input-group" style="text-align: right;">
                                                <datalist id="ListaNombreProd" runat="server">

                                                </datalist>
                                               
                                                <asp:TextBox ID="txtDescripcionProductos" onfocusout="handle(event)" list="ContentPlaceHolder1_ListaNombreProd" class="form-control" runat="server"/>
                                                <span class="input-group-btn">
                                                    <asp:LinkButton runat="server" id="btnProductos" class="btn btn-primary dim" onclientclick="FocusSearch('1')" data-toggle="modal" data-backdrop="static" data-target="#modalTabsProductos"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>

                                                </span>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="idProducto" runat="server" />

                                     
                                        <div class="col-md-1" style="text-align: left;">
                                            <label style="margin-bottom: auto;">Cantidad</label>

                                            <asp:TextBox ID="txtCantidad" Text="0" onkeydown="PasarAFactor(event)" onchange="CalcularCantBruta()" onkeypress="javascript:return validarNro(event)" Style="text-align: right" class="form-control money" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="text-align: left;">
                                            <label style="margin-bottom: auto;">U. Medida.</label>

                                            <asp:TextBox ID="txtUnidadMed" disabled="disabled" Style="text-align: right" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="text-align: left; display:none">
                                            <label style="margin-bottom: auto;">Factor</label>

                                            <asp:TextBox ID="txtFactor" Text="1" onkeyUp="CalcularCantBruta()" Style="text-align: right" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="text-align: left; ">
                                            <label style="margin-bottom: auto;">Cant. Bruta</label>

                                            <asp:TextBox disabled="disabled" ID="txtCantBruta" Style="text-align: right" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-1" style="text-align: left;">
                                            <label style="margin-bottom: auto; vertical-align: middle">Kg limpio $</label>
                                            <asp:TextBox ID="txtCostoLimpio" disabled="disabled" 
                                                onkeypress="javascript:return validarNro(event)" 
                                                Style="text-align: right" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-2" style="text-align: left; padding-left:2px; padding-right:2px">
                                            <label style="margin-bottom: auto; vertical-align: middle">Sector productivo</label>
                                               <asp:DropDownList ID="ddlSector" runat="server"
                                                     CssClass="chosen-select form-control"
                                                     DataTextField="CountryName" DataValueField="CountryCode"
                                                     Data-placeholder="Seleccione Rubro..." Width="100%">
                                               <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                               </asp:DropDownList>                                            
                                        </div>
                                            <div class="col-md-1" style="text-align: left;">
                                             <label id="TiempoPreparacion" style="margin-bottom:0px">Dias Previos</label>
                                               <asp:TextBox Text="0" Style="text-align: right;" ID="TiempoDePreparacion"  
                                                    class="form-control" runat="server" onkeypress="javascript:return validarNro(event)"/>
                                             </div>
                                         <div style="float: right; margin-right: 10px; margin-top: 19px">
                                            <LinkButton ID="btnAgregarProducto" onclick="agregarProductoPH();" class="btn btn-primary dim required"><i style="color: white" class="fa fa-check"></i></LinkButton>
                                            <linkbutton id="btnEditarProducto" style="display:none" onclick="editarProductoPH();" class="btn btn-primary dim" data-toggle="tooltip" data-placement="top" title="Editar ingrediente"><i style="color: white" class="fa fa-pencil"></i></linkbutton>
                                        </div>

                                    </div>
                                    <asp:HiddenField runat="server" ID="idProductosRecetas" />
                                    <asp:HiddenField runat="server" ID="hiddenReceta" />
                                    <table class="table table-bordered table-hover" id="tableProductos">
                                        <thead>
                                            <tr style="white-space:normal">
                                                <th style="width: 0%;"></th> <!--Codigo-->
                                                <th style="width: 41%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Descripción</th>
                                                <th style="width: 8%; text-align:right">Cant.</th>
                                                <th style="width: 6%">Uni.</th>
                                                <%--<th style="width: auto">Factor</th>--%>
                                                <%--<th style="width: auto">C. Bruta</th>--%>
                                                <th style="width: 10%; text-align:right">Costo</th>
                                                <th style="width: 11%; text-align:right">C. Total</th>
                                                <th style="width: 13%; text-align:left">Sector</th>
                                                <th style="text-align:right; width:7%">Tiempo</th>
                                                <th style="width:4%; white-space: nowrap;"></th>
                                                <th style="width: 0%;"></th> 
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phProductos" runat="server" />
                                        </tbody>
                                    </table>

                                    </div>
                                     <%--<div class="row" style="margin-top: 2%">
                                                    <label class="col-sm-2 control-label editable">Producto Final</label>
                                                    <div class="col-sm-8">
                                                        <div class="input-group m-b">
                                                            <asp:CheckBox ID="CheckProductoFinal" runat="server"  data-toggle="tooltip" data-placement="top" data-original-title="Selecionar como Producto Final" />
                                                        </div>
                                                    </div>
                                         </div>--%>
                                </fieldset>


                                <h1>BUENAS PRACTICAS</h1>
                                <fieldset>
                                    <h1>
                                        <label id="lblDescripcion3"></label>
                                    </h1>

                                    <div class="row" style="margin-top: 2%">
                                        <label class="col-sm-2 control-label editable">Buenas prácticas</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" Rows="4" class="form-control" runat="server" />
                                            <asp:HiddenField ID="idPresentacion" runat="server" />
                                        </div>


                                    </div>
                                    

                                </fieldset>
                                <h1>INF. NUTRICIONAL</h1>
                                <fieldset style="position: relative;">
                                    <h1>
                                        <label id="lblDescripcion4"></label>
                                    </h1>
                                    <%-- <input id="acceptTerms" name="acceptTerms" type="checkbox" class="required"/>
                                    <label for="acceptTerms">I agree with the Terms and Conditions.</label>--%>

                                    <div class="row" style="margin-top: 2%">
                                        <label class="col-sm-2 control-label editable">Informacion nutricional</label>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtInfoNutr" TextMode="MultiLine" Rows="4" class="form-control" runat="server" />
                                        </div>
                                    </div>
                                    
                                </fieldset>
                                <h1>PROCEDIMIENTO</h1>
                                <fieldset style="position: relative;">
                                    <h1>
                                        <label id="lblDescripcion5"></label>
                                    </h1>
                                    <div class="row" style="margin-top: 2%; margin-left: 2%">
                                        <label class="col-sm-2 control-label editable">Paso:</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtPasoNum" Text="1" class="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-9">
                                            <div class="input-group" style="text-align: right">
                                                <asp:TextBox ID="txtPasoDesc" placeholder="Detalle el paso" onkeypress="pulsar(event)" class="form-control" runat="server" />
                                                <span class="input-group-btn">
                                                    <%--<linkbutton id="btnProductos" class="btn btn-primary dim" onclick="FocusSearch()" data-toggle="modal" data-backdrop="static" data-target="#modalTabsProductos"><i style="color: white" class="fa fa-plus"></i></linkbutton>--%>
                                                    <asp:LinkButton ID="btnAgregarPaso" runat="server" onclientclick="AgregarPaso(event)" class="btn btn-primary dim"><i style="color: white" class="fa fa-plus"></i></asp:LinkButton>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 2%; margin-left: 2%">
                                        <label class="col-sm-2 control-label editable">Paso a paso</label>
                                        <div class="col-md-10">
                                            <%--<asp:TextBox ID="txtPasoAPaso" placeholder="Reemplazar por grilla"  TextMode="MultiLine" Rows="4" class="form-control" runat="server" />--%>
                                            <table class="table table-bordered table-hover" id="tablePasos">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 10%">Paso</th>
                                                        <%--<th style="width: 10%">Tipo</th>--%>
                                                        <th style="width: 80%">Descripcion</th>
                                                        <th style="width: 10%"></th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:PlaceHolder ID="PasosPH" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                    <asp:HiddenField ID="hfPasos" runat="server" />
                                    <div class="row" style="margin-top: 2%; margin-left: 2%; text-align: right">
                                        <asp:LinkButton runat="server" ID="btnGuardar" style=" display:none" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                                        <linkbutton onclick="OrdenarTablaPasos()" class="btn btn-warning" ><i style="color: white" class="fa fa-refresh"></i>&nbsp; Ordenar</linkbutton>
                                        <linkbutton class="btn btn-primary" disabled><i style="color: white" class="fa fa-file-pdf-o"></i> &nbsp;Generar PDF</linkbutton>
                                    </div>
                                </fieldset>
                                <%--<h1>FICHA TECNICA</h1>
                                <fieldset>
                                     
                                    <div class="row" style="margin-top: 2%">
                                        <label class="col-sm-2 control-label editable">Ficha tecnica</label>
                                        <div class="col-sm-10">
                                            <h2><label class="col-sm-2 control-label editable">Procedimiento</label></h2>
                                            <asp:TextBox ID="txtProcedimiento" placeholder="Campo a llenar" TextMode="MultiLine" Rows="4" class="form-control" runat="server" />
                                            
                                        </div>
                                       

                                    </div>
                                </fieldset>--%>
                            </form>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hiddenID" runat="server" />

                <asp:HiddenField ID="hiddenidReceta" runat="server" />
            </div>

                                        <div class="product-container" style="display:none" id="">
        <asp:Literal ID="productContainer" runat="server"></asp:Literal>
    </div>

        </div>


        <div id="modalTabsProductos" class="modal" style="z-index: 2000;" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                     <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h2 class="modal-title">Identifiquemos tu Ingrediente
                        <span>
                            <%--<i style='color:black;' class='fa fa-search'></i>--%>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                                <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                            </svg>

                        </span>
                        </h2>
                    </div>
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
                                      <div class="input-group m-b">
                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                            <input type="text" id="txtBusquedaIngredientes" placeholder="Busqueda..." class="form-control" />
                        </div>
                                    <div class="tab-content">
                                        <div id="tab-1" class="tab-pane active">
                                            <table class="table table-bordered table-hover" id="editable2">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 10%">Cod.</th>
                                                        <th style="width: 20%">Descripcion</th>
                                                        <th style="width: 20%">Costo $</th>
                                                        <%--<th style="width: 20%">Unidad Medida</th>--%>
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
                                                        <th style="width: 10%">Cod.</th>
                                                        <th style="width: 20%">Descripcion</th>
                                                        <th style="width: 20%">Costo $</th>
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

    

            <!-- Mainly scripts -->
            <script src="/../js/jquery-2.1.1.js"></script>
            <!-- Mainly scripts -->
         
            <script src="../../js/bootstrap.min.js"></script>
            <script src="../../Scripts/plugins/metisMenu/jquery.metisMenu.js"></script>
         
            <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/3.2.6/jquery.inputmask.bundle.min.js"></script>

    
            <!-- Steps -->
            <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>

            <!-- Jquery Validate -->
            <script src="/../Scripts/plugins/validate/jquery.validate.min.js"></script>


            <script src="//cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
            <link href="//cdn.datatables.net/1.10.2/css/jquery.dataTables.css" rel="stylesheet" />
        </div>
         <div id="modalSectores" class="modal" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h2 class="modal-title">Indicanos tu Sector
                        <span>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                                <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                            </svg>

                        </span>
                        </h2>
                    </div>
                    <div class="modal-body">
                       <%-- <div class="input-group m-b">
                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                            <input type="text" id="txtBusquedaSector" placeholder="Busqueda..." class="form-control" />
                        </div>--%>

                        <div class="ibox-content">
                            <asp:UpdatePanel ID="UpdatePanel66" runat="server">
                                <ContentTemplate>
                                    <table class="table table-bordered table-hover" id="editable7">
                                        <thead>
                                            <tr>

                                                <th>Num</th>
                                                <th>Codigo</th>
                                                <th>Sector</th>
                                                <%--<th style="width: 20%">Unidad Medida</th>--%>
                                                <th style="width: 20%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="phSectores" runat="server" />
                                        </tbody>
                                    </table>

                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                     <div class="modal-footer">
                         <linkbutton id="btnSectores2" class="btn btn-primary" onclick="agregarSector()"><i class="fa fa-check"></i>&nbsp;Agregar</linkbutton>

                         <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="modalRecetaDetalle" class="modal" role="dialog">
            <div class="modal-dialog modal-sm" style="width: 75%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                       <h2 class="modal-title">Componentes de la receta
                        <span>
                            <%--<i style='color:black;' class='fa fa-search'></i>--%>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                                <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                            </svg>

                        </span>
                        </h2>
                    </div>
                    <div class="modal-body">
                       <%-- <div class="input-group m-b">
                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>


                            <input type="text" id="txtBusquedaSector" placeholder="Busqueda..." class="form-control" />
                        </div>--%>

                        <div class="ibox-content">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <table class="table table-bordered table-hover" id="editable27">
                                        <thead>
                                            
                                              <th style="width: 10%">Cod. Producto</th>
                                                <%--<th style="width: 10%">Tipo</th>--%>
                                                <th style="width: 20%">Descripcion</th>
                                                <th style="width: 10%; text-align:right">Cantidad</th>
                                                <th style="width: 10%">Unidad Medida</th>
                                                <th style="width: 10%;text-align:right">Costo $</th>
                                                <th style="width: 10%;text-align:right">Costo Total $</th>
                                                <th style="width: 10%"></th>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                                        </tbody>
                                    </table>

                                </ContentTemplate>

                            </asp:UpdatePanel>
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
                            <button id="btnAgregarAtributo" onclick="agregarAtributos();return false;" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="modalFamilia" class="modal" style="z-index: 2000; padding-left: 80px; padding-top: 80px" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Familia</h4>
                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                            <h2>
                                <label id="lblFamilia"></label>
                            </h2>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cerrar</button>

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

    <%-- Modal MARCAS--%>
        <div id="modalMarca" class="modal" tabindex="-2" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Elegir Marca</h4>
                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered table-hover " id="tableMarcas">

                                                                <thead>
                                                                    <tr>
                                                                        <th>Descripcion</th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tbodyMarcas">
                                                                    <asp:PlaceHolder ID="phMarcas" runat="server"></asp:PlaceHolder>
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
                            <a id="btnAgregarMarca" onclick="agregarMarcas()" class=" btn btn-primary"><i class="fa fa-check"></i>&nbsp;Agregar </a>
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                            <asp:HiddenField runat="server" ID="hfMarcas" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="modalPresentacion" class="modal" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Elegir Presentacion</h4>
                    </div>
                    <div class="modal-body">
                        <div class="ibox-content">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox ">
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                    <ContentTemplate>
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered table-hover " id="editable4">

                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Descripcion</th>
                                                                        <th>Cantidad</th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tbodyEditable1">
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

        <div id="modalAtributos" class="modal" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h2 class="modal-title">Indicanos tu Sector
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


                            <input type="text" id="txtBusquedaAtributo" placeholder="Busqueda..." class="form-control" />
                        </div>

                        <div class="ibox-content">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table class="table table-bordered table-hover" id="editable8">
                                        <thead>
                                            <tr>

                                                <th>Num</th>
                                                <th>Codigo</th>
                                                <th>Sector</th>
                                                <%--<th style="width: 20%">Unidad Medida</th>--%>
                                                <th style="width: 20%"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="PHAtributos" runat="server" />
                                        </tbody>
                                    </table>

                                </ContentTemplate>

                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <script src="/../js/plugins/pace/pace.min.js"></script>

    <script src="/../js/plugins/jsTree/jstree.min.js"></script>
        <!-- Chosen -->
 <%--   <script src="/js/plugins/chosen/chosen.jquery.js"></script>--%>

 <%--   <script src="/js/plugins/chosen/chosen.jquery.js"></script>
    <script>
        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>--%>


    <script>
        //function toggleChildren(event) {
        //    // Evita que el evento se propague a los elementos padres
        //    event.stopPropagation();

        //    // Obtiene el siguiente elemento hermano que contiene los hijos
        //    const children = event.currentTarget.nextElementSibling;

        //    // Alterna la clase 'hidden' para mostrar o ocultar los hijos
        //    if (children) {
        //        children.classList.toggle('hidden');
        //    }
        //}

        //function toggleChildren(row) {
        //    var nextRow = row.nextElementSibling;

        //    // Mostrar/ocultar todas las filas hijas que sigan a la fila padre
        //    while (nextRow && nextRow.classList.contains('child')) {
        //        nextRow.classList.toggle('hidden');
        //        nextRow = nextRow.nextElementSibling;
        //    }
        //}

        function toggleChildren(row) {
            // Obtener el nivel actual de la fila expandida
            var currentLevel = parseInt(row.getAttribute('data-nivel')) || 0; // Por defecto nivel 0 si no está definido

            // Obtener la siguiente fila en la tabla
            var nextRow = row.nextElementSibling;

            // Bandera para alternar el estado (expandir/colapsar)
            var isExpanding = nextRow && nextRow.classList.contains('hidden');

            // Mientras haya una fila siguiente
            while (nextRow) {
                var rowLevel = parseInt(nextRow.getAttribute('data-nivel')) || 0;

                // Si es un nivel mayor al actual, mostrar/ocultar
                if (rowLevel > currentLevel) {
                    if (isExpanding) {
                        nextRow.classList.remove('hidden'); // Mostrar todas las filas hijas de mayor nivel
                    } else {
                        nextRow.classList.add('hidden'); // Ocultar todas las filas hijas de mayor nivel
                    }
                } else {
                    // Si encontramos una fila de nivel igual o menor, detener el bucle
                    break;
                }

                // Continuar con la siguiente fila
                nextRow = nextRow.nextElementSibling;
            }
        }





    </script>

    <script>
        toastr.options = {
            "positionClass": "toast-bottom-right" // Puedes usar "toast-bottom-right", "toast-bottom-left", etc.
        };

        function BuscarlistaProd() {
            let numLetras = document.getElementById('txtDescripcionProductos').value.length;
            let descripcion = document.getElementById('txtDescripcionProductos').value;
            if (numLetras > 3) {
                let param;
                $.ajax({
                    method: "POST",
                    url: "RecetasABM.aspx/GetProd",
                    data: '{d:"' + descripcion + '" }"',
                    contentType: "application/json",
                    dataType: 'json',

                    error: (error) => {
                        console.log(JSON.stringify(error));
                        //$.msgbox("No se pudo cargar la tabla", { type: "error" });
                    },
                    success: agregarDescripcionProd
                });
            }

        }
        function agregarDescripcionProd(response) {
            let lista = document.getElementById
            let option = document.createElement('option');
            for (var i in Arraylist.response) {
                $('ListaNombreProd').append(
                    " <option label=" + i + " value=" + i + " />"
                );
            }
        }
    </script>
    <script>
        function handle(e) {


            //let x = ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[1];
            let txtProd = document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').value
            if (txtProd.includes(' - ')) {

                const idOption = document.querySelector('option[value="' + txtProd + '"]').id;
                let costo = idOption.split("_")[5].trim();
                //let prod = document.getElementById('ContentPlaceHolder1_Productos_' + idOption.split("_")[1] + "_" + idOption.split("_")[2]).children[0].innerHTML;
                //let costo = document.getElementById('ContentPlaceHolder1_Productos_' + idOption.split("_")[1] + "_" + idOption.split("_")[2]).children[1].innerHTML;
                const itemId = idOption.split('_')[2].trim();

                if (idOption.includes("c_p_")) {
                    agregarProducto(idOption, costo);
                    CargarDepositos(itemId, 1);
                }
                else if (idOption.includes("c_r_")) {
                    agregarReceta(idOption, costo)
                    CargarDepositos(itemId, 2);
                }
            }
        }
    </script>

    <script>
        function CargarDepositos(id, tipo) {
            $.ajax({
                method: "POST",
                url: "RecetasABM.aspx/GetIdSectorByIdProd",
                data: '{idProd: "' + id + '", tipo:"' + tipo + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: function (respuesta) {
                    var ddlDepositos = $("#<%=ddlSector.ClientID%>");
                    ddlDepositos.val(respuesta.d);
                }
            });
        }
    </script>

    <script>
        function agregarPresentaciones() {
            //let table1 = $('#editable1').DataTable();
            let table2 = document.getElementById('editable4');
            let max = table2.rows.length;
            let presentacionFinal = '';
            //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
            for (let i = 1; i < max; i++) {
                /*if (i > 1) {*/
                if (table2.rows[i].cells[3].children[0].checked) {

                    presentacionFinal += table2.rows[i].cells[0].innerHTML + " - " + table2.rows[i].cells[1].innerHTML + ', ';
                    table2.rows[i].cells[3].children[0].checked = false;
                }

            }

            $('#modalPresentacion').modal('hide');
            document.getElementById('<%=hfPresentaciones.ClientID%>').value = presentacionFinal;
            document.getElementById('<%=txtPresentaciones.ClientID%>').value = presentacionFinal;
            return true;
        }
        function AgregarPaso(event) {
            event.preventDefault();
            let descripcion = document.getElementById('<%=txtPasoDesc.ClientID%>').value;
            let num = document.getElementById('<%=txtPasoNum.ClientID%>').value;

            $('#tablePasos').append(
                "<tr id=\"Row_" + num + "\">" +
                "<th style=\"width: 10%\">" + num + "</th >" +
                "<th style=\"width: 80%\">" + descripcion + "</th >" +
                "<td style=\" text-align: center\"> <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent;\" class=\"btn  btn-xs \" onclick=\"javascript: return borrarPaso('Row_" + num + "');\" >" +
                "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> </td>" +
                "</tr>"
            )
            let inum = parseInt(num);
            inum++;
            let procedimientosAll = document.getElementById('<%=hfPasos.ClientID%>').value;
            procedimientosAll += num + "-" + descripcion + ";";

            document.getElementById('<%=hfPasos.ClientID%>').value = procedimientosAll;

            document.getElementById('<%=txtPasoNum.ClientID%>').value = inum;
            document.getElementById('<%=txtPasoDesc.ClientID%>').value = '';

        }

        function borrarPaso(idRow) {


            $('#' + idRow).remove();
            let Paso = document.getElementById('<%=hfPasos.ClientID%>').value.split(';');
            let nuevosPaso = "";
            for (let x = 0; x < Paso.length; x++) {
                if (Paso[x] != "") {
                    if (!Paso[x].includes(idRow)) {
                        nuevosPaso += Paso[x] + ";";
                    }
                    else {
                        /* var productoAEliminar = Paso[x].split(',')[2];*/
                        //ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) - parseFloat(productoAEliminar);
                        //if (ContentPlaceHolder1_txtRinde.value != "") {
                        //    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2);
                        //    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                        //}
                    }
                }
            }
            document.getElementById('<%=hfPasos.ClientID%>').value = nuevosPaso;
        }

    </script>
    <script type="text/javascript">
        "use strict";
        //var $image = $("#imgDocF")

        //var $inputImageF = document.getElementById('inputImage2');

        //if (window.FileReader) {
        //    //inputImageF.addEventListener("change",
        //    $("#form").on('change', '#ContentPlaceHolder1_inputImage2', function () {
        //        var fileReader = new FileReader(),
        //            files = this.files,
        //            file;
        //        if (!files.length) {
        //            return;
        //        }
        //        file = files[0];
        //        if (/^image\/\w+$/.test(file.type)) {
        //            fileReader.readAsDataURL(file);
        //            fileReader.onload = function () {
        //                $("#imgDocF").attr("src", this.result)
        //            };
        //        } else {
        //            showMessage("Please choose an image file.");
        //        }
        //    });
        //}


    </script>

    <script>
        // Función para renderizar la imagen
        function renderizarImagen(event) {
            var file = event.target.files[0]; // Obtiene el archivo seleccionado

            if (file) {
                var reader = new FileReader(); // Crea una instancia de FileReader

                reader.onload = function (e) {
                    var img = document.getElementById('imgDocF');
                    img.src = e.target.result; // Establece la fuente de la imagen
                    //img.style.display = 'block'; // Muestra la imagen
                };

                reader.readAsDataURL(file); // Lee el archivo como URL de datos
            }
        }

        function precargarImagen() {
            // Precargar la imagen al cargar la página
            let url = window.location.href;
            let parameter = url.split("?")[1]
            let queryString = new URLSearchParams(parameter);
            let idReceta = ''
            for (let pair of queryString.entries()) {
                if (pair[0] == "i")
                    idReceta = pair[1];
            }

            window.onload = function () {
                var img = document.getElementById('imgDocF');

                // Buscar una imagen enviando el id
                $.ajax({
                    method: "POST",
                    url: "RecetasABM.aspx/GetImagenByIdReceta",
                    data: '{ idReceta: "' + idReceta + '"}',
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {

                    },
                    success: function (response) {
                        if (response.d !== "") {
                            img.src = response.d;
                        }
                    }
                });
            };
        }
    </script>
    <script>
        $(document).ready(function () {

            let url = window.location.href;
            if (url.includes("a=2")) {
                ActualizarLabels();
                precargarImagen();
            }

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
                    <%--let cat = document.getElementById('<%= txtDescripcionCategoria.ClientID%>');
                    if (cat.value.trim().length == 0 && currentIndex == 0) {
                        document.getElementById('valCategoria').className = 'text-danger'
                        return false;
                    }--%>


                    let ValUM = true
                    let selectUM = document.getElementById('<%=ddlUnidadMedida.ClientID%>').value
                    if (selectUM == -1 && currentIndex == 1) {
                        ValUM = false
                        document.getElementById('valiva').className = 'text-danger'
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
                    //if (currentIndex === 2 && Number($("#age").val()) >= 18) {
                    //    $(this).steps("next");
                    //}

                    // Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
                    if (currentIndex === 2 && currentIndex === 3) {
                        $(this).steps("previous");
                    }
                },
                onFinishing: function (event, currentIndex) {
                    var form = $(this);
                    //----------------------------------------------------------------------------- REVISAR 
                    // Disable validation on fields that are disabled.
                    // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
                    form.validate().settings.ignore = ":disabled";

                    // Start validation; Prevent form submission if false
                    let ValUM = true
                    let selectUM = document.getElementById('<%=ddlUnidadMedida.ClientID%>').value
                    if (selectUM == -1 && currentIndex == 1) {
                        ValUM = false
                        document.getElementById('valiva').className = 'text-danger'
                        return false
                    }
                    return form.valid();
                },
                onCanceled: function (event, currentIndex) {
                    var form = $(this);
                    //----------------------------------------------------------------------------- REVISAR

                    // Submit form input
                    window.location.replace('Recetas.aspx');
                },
                onFinished: function (event, currentIndex) {

                    //----------------------------------------------------------------------------- REVISAR

                    let rinde = document.getElementById('<%=txtRinde.ClientID%>').value;
                    //Validar Sector Productivo
                    if (rinde === "" || rinde <= 0) {
                        document.getElementById('<%= txtRinde.ClientID %>').classList.add('invalid');
                        return false;
                    }
                    else {
                        document.getElementById('<%= txtRinde.ClientID %>').classList.remove('invalid');
                    }

                    if (!EsTipoRecetaValido()) return false;

                    if (!EsRubroValido()) return false;


                    let selectUnidadMedida = document.getElementById('<%=ddlUnidadMedida.ClientID%>');
                    let selectTipoReceta = document.getElementById('<%=ddlTipoReceta.ClientID%>');
                    let selectRubro = document.getElementById('<%=ddlRubros.ClientID%>');

                    let url = window.location.href;
                    if (!url.includes("a=2")) {
                        let selectUM = document.getElementById('<%=ddlUnidadMedida.ClientID%>').value
                        let rowsCount = document.getElementById('tableProductos').value
                        if (selectUM != -1) {

                            /* alert("esta en onfinished!");*/

                            //Este ajax se ejecuta cuando se esta guardando la receta por primera vez, lo que que hace es guardar la recetas
                            //con todos los datos de la receta y ademas guarda todos los productos de la receta

                            $.ajax({
                                method: "POST",
                                url: "ProductosABM.aspx/GuardarReceta2",
                                data: '{ descripcion: "' + document.getElementById('<%=txtDescripcionReceta.ClientID%>').value
                                    + '" , codigo: "' + document.getElementById('<%=txtCodigo.ClientID%>').value
                                    <%--+ '" , Categoria: "' + document.getElementById('<%=txtDescripcionCategoria.ClientID%>').value--%>
                                    + '" , Sector: "' + document.getElementById('<%=ddlSectorProductivo.ClientID%>').value
                                    <%--+ '" , Atributos: "' + document.getElementById('<%=txtDescripcionAtributo.ClientID%>').value--%>
                                    + '" , Unidad: "' + selectUnidadMedida.selectedOptions[0].value
                                    + '" , Tipo: "' + selectTipoReceta.selectedOptions[0].value
                                    + '" , rinde: "' + document.getElementById('<%=txtRinde.ClientID%>').value
                                    + '" , prVenta: "' + document.getElementById('<%=txtPrVenta.ClientID%>').value
                                    + '" , idProductosRecetas: "' + document.getElementById('<%=idProductosRecetas.ClientID%>').value
                                    + '" , BrutoT: "' + document.getElementById('<%=txtKgBrutTotal.ClientID%>').value
                                    + '" , CostoT: "' + document.getElementById('<%=txtCostoTotal.ClientID%>').value
                                    + '" , BrutoU: "' + document.getElementById('<%=txtKgxPorcion.ClientID%>').value
                                    + '" , CostoU: "' + document.getElementById('<%=txtCostoxPorcion.ClientID%>').value
                                    + '" , FoodCost: "' + document.getElementById('<%=txtPFoodCost.ClientID%>').value.replace('%', '')
                                    + '" , ContMarg: "' + document.getElementById('<%=txtContMarg.ClientID%>').value
                                    + '" , BuenasPract: "' + document.getElementById('<%=txtObservaciones.ClientID%>').value
                                    + '" , InfoNut: "' + document.getElementById('<%=txtInfoNutr.ClientID%>').value
                                    + '" , idPasosRecetas: "' + document.getElementById('<%=hfPasos.ClientID%>').value
                                    + '" , Presentaciones: "' + document.getElementById('<%=hfPresentaciones.ClientID%>').value
                                    + '" , ProdFinal: "' + document.getElementById('ContentPlaceHolder1_CheckProductoFinal').checked
                                    + '" , Comprable: "' + document.getElementById('ContentPlaceHolder1_CheckSePuedeComprar').checked
                                    + '" , Rubro: "' + selectRubro.selectedOptions[0].value
                                    + '" , Marcas: "' + document.getElementById('<%=txtMarcas.ClientID%>').value
                                    + '"}',
                                contentType: "application/json",
                                dataType: 'json',
                                error: (error) => {
                                    console.log(JSON.stringify(error));
                                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                                },
                                success: function (response) {
                                    var data = response.d;
                                    data = JSON.parse(data); // Convierte la cadena a objeto

                                    // Se guardara la imagen con nombre de id de la receta creada
                                    guardarImagen(data, true);
                                }
                            });



                        }
                    } else if (!url.includes('b=1')) {
                        let parameter = url.split("?")[1]
                        let queryString = new URLSearchParams(parameter);
                        let idReceta = ''
                        for (let pair of queryString.entries()) {
                            if (pair[0] == "i")
                                idReceta = pair[1];
                        }

                        //Este ajax se ejecuta cuando se esta editardo la receta, lo que hace es guardar la recetas
                        //con todos los datos de la receta y ademas guarda todos los productos de la receta
                        $.ajax({
                            method: "POST",
                            url: "ProductosABM.aspx/EditarReceta2",
                            data: '{ descripcion: "' + document.getElementById('<%=txtDescripcionReceta.ClientID%>').value
                                + '" , codigo: "' + document.getElementById('<%=txtCodigo.ClientID%>').value
                                <%--+ '" , Categoria: "' + document.getElementById('<%=txtDescripcionCategoria.ClientID%>').value--%>
                                + '" , Sector: "' + document.getElementById('<%=ddlSectorProductivo.ClientID%>').value
                                <%--+ '" , Atributos: "' + document.getElementById('<%=txtDescripcionAtributo.ClientID%>').value--%>
                                + '" , Unidad: "' + selectUnidadMedida.selectedOptions[0].value
                                + '" , Tipo: "' + selectTipoReceta.selectedOptions[0].value
                                + '" , rinde: "' + document.getElementById('<%=txtRinde.ClientID%>').value
                                + '" , prVenta: "' + document.getElementById('<%=txtPrVenta.ClientID%>').value
                                + '" , idProductosRecetas: "' + document.getElementById('<%=idProductosRecetas.ClientID%>').value
                                + '" , BrutoT: "' + document.getElementById('<%=txtKgBrutTotal.ClientID%>').value
                                + '" , CostoT: "' + document.getElementById('<%=txtCostoTotal.ClientID%>').value
                                + '" , BrutoU: "' + document.getElementById('<%=txtKgxPorcion.ClientID%>').value
                                + '" , CostoU: "' + document.getElementById('<%=txtCostoxPorcion.ClientID%>').value
                                + '" , FoodCost: "' + document.getElementById('<%=txtPFoodCost.ClientID%>').value.replace('%', '')
                                + '" , ContMarg: "' + document.getElementById('<%=txtContMarg.ClientID%>').value
                                + '" , BuenasPract: "' + document.getElementById('<%=txtObservaciones.ClientID%>').value
                                + '" , InfoNut: "' + document.getElementById('<%=txtInfoNutr.ClientID%>').value
                                + '" , idPasosRecetas: "' + document.getElementById('<%=hfPasos.ClientID%>').value
                                + '" , idReceta: "' + idReceta
                                + '" , Presentaciones: "' + document.getElementById('<%=hfPresentaciones.ClientID%>').value
                                + '" , ProdFinal: "' + document.getElementById('ContentPlaceHolder1_CheckProductoFinal').checked
                                + '" , Comprable: "' + document.getElementById('ContentPlaceHolder1_CheckSePuedeComprar').checked
                                + '" , Rubro: "' + document.getElementById('<%=ddlRubros.ClientID%>').selectedOptions[0].value
                                + '" , Marcas: "' + document.getElementById('<%=txtMarcas.ClientID%>').value
                                + '"}',
                            contentType: "application/json",
                            dataType: 'json',
                            error: (error) => {
                                console.log(JSON.stringify(error));
                                $.msgbox("No se pudo cargar la tabla", { type: "error" });
                            },
                            success: function (response) {
                                var data = response.d;
                                data = JSON.parse(data); // Convierte la cadena a objeto

                                // Se guardara la imagen con nombre de id de la receta creada
                                guardarImagen(data, false);
                            }
                        });
                    }
                    // Submit form input

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


            let idRecetas = document.getElementById('<%= HFRecetas.ClientID %>').value.split(',');
            if (document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',').length > 0) {

                for (let i = 0; i < document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',').length; i++) {
                    if (document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i] != "") {
                        let id = document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i];
                        $('#jstree' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i])
                            .on('after_open.jstree', function (e, data) {

                                let banderaC = false;
                                let JidCant = $("#jstree_C" + id).jstree()._model.data
                                let JidCantaux = Object.values(JidCant);

                                for (i = 0; i < JidCantaux.length; i++) {
                                    if (JidCantaux[i].id.includes('_')) {
                                        let res = JidCantaux[i].id.split('_')[1];
                                        console.log(data.node.id)
                                        console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaC == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_C" + id).jstree("open_node", "#RecetaC_LI_" + id);
                                                banderaC = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_C" + id).jstree("open_node", "#" + JidCantaux[i].id);
                                                    banderaC = true;
                                                }
                                        }
                                    }
                                }
                                let banderaUM = false;
                                let JidUM = $("#jstree_UM" + id).jstree()._model.data
                                let JidUMTaux = Object.values(JidUM);

                                for (i = 0; i < JidUMTaux.length; i++) {
                                    if (JidUMTaux[i].id.includes('_')) {
                                        let res = JidUMTaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaUM == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_UM" + id).jstree("open_node", "#RecetaUM_LI_" + id);
                                                banderaUM = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_UM" + id).jstree("open_node", "#" + JidUMTaux[i].id);
                                                    banderaUM = true;
                                                }
                                        }
                                    }
                                }

                                let JidCS = $("#jstree_CS" + id).jstree()._model.data
                                let JidCSaux = Object.values(JidCS);
                                let banderaCS = false;

                                for (i = 0; i < JidCSaux.length; i++) {
                                    if (JidCSaux[i].id.includes('_')) {
                                        let res = JidCSaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCS == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CS" + id).jstree("open_node", "#RecetaCS_LI_" + id);
                                                banderaCS = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CS" + id).jstree("open_node", "#" + JidCSaux[i].id);
                                                    banderaCS = true;
                                                }
                                        }
                                    }
                                }

                                let JidCST = $("#jstree_CST" + id).jstree()._model.data
                                let JidCSTaux = Object.values(JidCST);
                                let banderaCST = false
                                for (i = 0; i < JidCSTaux.length; i++) {
                                    if (JidCSTaux[i].id.includes('_')) {
                                        let res = JidCSTaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCST == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CST" + id).jstree("open_node", "#RecetaCST_LI_" + id);
                                                banderaCST = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CST" + id).jstree("open_node", "#" + JidCSTaux[i].id);
                                                    banderaCST = true;
                                                }
                                        }
                                    }
                                }

                                $("#jstree_C" + id).jstree("open_all", JidCantaux.id);
                                $("#jstree_UM" + id).jstree("open_all", JidUMTaux.id);
                                $("#jstree_CS" + id).jstree("open_all", JidCSaux.id);
                                $("#jstree_CST" + id).jstree("open_all", JidCSTaux.id);
                            })
                            .on('after_close.jstree', function (e, data) {
                                //$("#jstree_C" + id).jstree("close_node", JidCantaux.id);
                                //$("#jstree_UM" + id).jstree("close_node", JidUMTaux.id);
                                //$("#jstree_CS" + id).jstree("close_node", JidCSaux.id);
                                //$("#jstree_CST" + id).jstree("close_node", JidCSTaux.id);
                                let JidCant = $("#jstree_C" + id).jstree()._model.data
                                let JidCantaux = Object.values(JidCant);
                                let banderaC = false;
                                for (i = 0; i < JidCantaux.length; i++) {
                                    if (JidCantaux[i].id.includes('_')) {
                                        let res = JidCantaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaC == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_C" + id).jstree("close_node", "#RecetaC_LI_" + id);
                                                banderaC = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_C" + id).jstree("close_node", "#" + JidCantaux[i].id);
                                                    banderaC = true;
                                                }
                                        }
                                    }
                                }
                                let banderaUM = false;
                                let JidUM = $("#jstree_UM" + id).jstree()._model.data
                                let JidUMTaux = Object.values(JidUM);

                                for (i = 0; i < JidUMTaux.length; i++) {
                                    if (JidUMTaux[i].id.includes('_')) {
                                        let res = JidUMTaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaUM == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_UM" + id).jstree("close_node", "#RecetaUM_LI_" + id);
                                                banderaUM = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_UM" + id).jstree("close_node", "#" + JidUMTaux[i].id);
                                                    banderaUM = true;
                                                }
                                        }
                                    }
                                }

                                let JidCS = $("#jstree_CS" + id).jstree()._model.data
                                let JidCSaux = Object.values(JidCS);
                                let banderaCS = false
                                for (i = 0; i < JidCSaux.length; i++) {
                                    if (JidCSaux[i].id.includes('_')) {
                                        let res = JidCSaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCS == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CS" + id).jstree("close_node", "#RecetaCS_LI_" + id);
                                                banderaCS = true
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CS" + id).jstree("close_node", "#" + JidCSaux[i].id);
                                                    banderaCS = true
                                                }
                                        }
                                    }
                                }

                                let JidCST = $("#jstree_CST" + id).jstree()._model.data
                                let JidCSTaux = Object.values(JidCST);
                                let banderaCST = false;
                                for (i = 0; i < JidCSTaux.length; i++) {
                                    if (JidCSTaux[i].id.includes('_')) {
                                        let res = JidCSTaux[i].id.split('_')[1];
                                        //console.log(data.node.id)
                                        //console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCST == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CST" + id).jstree("close_node", "#RecetaCST_LI_" + id);
                                                banderaCST = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CST" + id).jstree("close_node", "#" + JidCSTaux[i].id);
                                                    banderaCST = true;
                                                }
                                        }
                                    }
                                }
                                //console.log('prueba2');
                                //let JidCant2 = $("#jstree_C" + id).jstree()._model.data
                                //let JidCantaux2 = Object.values(JidCant2)[11];

                                //let JidUM2 = $("#jstree_UM" + id).jstree()._model.data
                                //let JidUMTaux2 = Object.values(JidUM2)[11];

                                //let JidCS2 = $("#jstree_CS" + id).jstree()._model.data
                                //let JidCSaux2 = Object.values(JidCS2)[11];

                                //let JidCST2 = $("#jstree_CST" + id).jstree()._model.data
                                //let JidCSTaux2 = Object.values(JidCST2)[11];
                                console.log("LLEGE ACA")
                                $("#jstree_C" + id).jstree("close_node", "#RecetaC_LI_" + id);
                                $("#jstree_UM" + id).jstree("close_node", "#RecetaUM_LI_" + id);
                                $("#jstree_CS" + id).jstree("close_node", "#RecetaCS_LI_" + id);
                                $("#jstree_CST" + id).jstree("close_node", "#RecetaCST_LI_" + id);
                            })
                            .jstree({
                                'core': {
                                    'check_callback': true,
                                    'themes': {
                                        'icons': false // Aquí se deshabilitan los íconos
                                    }
                                },
                                'plugins': ['types', 'dnd'],
                                'types': {
                                    'default': {
                                        'icon': 'fa fa-folder'
                                    },
                                    'html': {
                                        'icon': 'fa fa-file-code-o'
                                    },
                                    'svg': {
                                        'icon': 'fa fa-file-picture-o'
                                    },
                                    'css': {
                                        'icon': 'fa fa-file-code-o'
                                    },
                                    'img': {
                                        'icon': 'fa fa-file-image-o'
                                    },
                                    'js': {
                                        'icon': 'fa fa-file-text-o'
                                    }

                                },
                                
                            });
                        $('#jstree_C' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true,
                                'themes': {
                                    'icons': false // Aquí se deshabilitan los íconos
                                }
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            },
                            
                        });
                        $('#jstree_UM' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true,
                                'themes': {
                                    'icons': false // Aquí se deshabilitan los íconos
                                }
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            },
                            
                        });
                        $('#jstree_CS' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true,
                                'themes': {
                                    'icons': false // Aquí se deshabilitan los íconos
                                }
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            },
                            
                        });
                        $('#jstree_CST' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true,
                                'themes': {
                                    'icons': false // Aquí se deshabilitan los íconos
                                }
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            }
                        });
                    }
                }
            }


            if (url.includes('b=1')) {
                var inputImage = document.getElementById('imgDocF');
                inputImage.removeAttribute('onclick');

                var btnGuardar = document.querySelector('a[href="#finish"]');
                btnGuardar.style.display = 'none';

                var btnCancelar = document.querySelector('a[href="#cancel"]');
                btnCancelar.innerHTML = 'Salir';

                var btnContainerAddItem = document.getElementById('containerAddItem');
                btnContainerAddItem.style.display = 'none';
            }
        });

        //var config = {
        //    '.chosen-select': {},
        //    '.chosen-select-deselect': { allow_single_deselect: true },
        //    '.chosen-select-no-single': { disable_search_threshold: 10 },
        //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        //    '.chosen-select-width': { width: "95%" }
        //}

        //for (var selector in config) {
        //    console.log('entre al for');
        //    try {
        //        console.log('entre al try');
        //        $(selector).chosen("destroy");
        //        $(selector).chosen(config[selector]);
        //    } catch (e) {
        //        console.log('algo salio mal');
        //    }
        //}

        //var config = {
        //    '.chosen-select': {},
        //    '.chosen-select-deselect': { allow_single_deselect: true },
        //    '.chosen-select-no-single': { disable_search_threshold: 10 },
        //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        //    '.chosen-select-width': { width: "95%" }
        //}
        //for (var selector in config) {
        //    $(selector).chosen(config[selector]);
        //}




    </script>


    
<script>
    function guardarImagen(data, esCreacion) {
        var input = document.getElementById('inputImage2');
        var file = input.files[0];

        if (file) {
            var formData = new FormData();
            formData.append('image', file);
            formData.append("idReceta", data.id);

            fetch('ImageUploadHandler.ashx', {
                method: 'POST',
                body: formData
            })
                .then(response => response.text())
                .then(result => {
                    if (esCreacion)
                        recargarPagina(data.mensaje)
                    else
                        recargarPagina2()
                })
                .catch(error => {
                    console.error('Error:', error);
                });
        } else {
            if (esCreacion)
                recargarPagina(data.mensaje)
            else
                recargarPagina2()
        }
    }
</script>

    <script>         
        function recargarPagina(mensaje) {
            //toastr.success("guardado con exito!", "Exito")
            //window.location.href = 'RecetasABM.aspx?m=1';

            //var obj = JSON.parse(response.d);
            toastr.options = { "positionClass": "toast-bottom-right" };

            if (mensaje == "") {
                /*alert('Obj es null');*/
                //return;
            }
            else {
                if (mensaje.toUpperCase().includes("ERROR")) {
                    toastr.error(mensaje, "Error");
                }
                else {
                    //console.log(obj);
                    window.location.href = 'RecetasABM.aspx?m=1';
                }
            }
        }
        function recargarPagina2() {
            toastr.success("Guardado con exito!", "Exito")
            //window.location.href = 'Recetas.aspx?m=1';
            //alert('Guardado con exito');
        }
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


    </script>

    <script>
        function MostrarModalFamilia(familia) {
            document.querySelector('#lblFamilia').textContent = familia;
            $('#modalFamilia').modal('show');
        }
        function CalcularCantBruta() {
            let Cantidad = parseFloat(document.getElementById('<%= txtCantidad.ClientID%>').value.replace(',', ''));
            let Factor;
            if (document.getElementById('<%= txtFactor.ClientID%>').value == "")
                Factor = 0;
            else
                Factor = parseFloat(document.getElementById('<%= txtFactor.ClientID%>').value);

            //let CantBruta = Cantidad * Factor;
            let CantBruta = Cantidad;
            document.getElementById('<%=txtCantBruta.ClientID%>').value = myFormat(Math.round10(CantBruta, -3));
        }
        function ValidationCategoria() {
            <%--let cat = document.getElementById('<%= txtDescripcionCategoria.ClientID%>');
            if (cat.value.trim().length > 0) {
                document.getElementById('valCategoria').className = 'text-hide';
            }--%>
        }
        function ActualizarUnidades() {
            let select = document.getElementById('<%= ddlUnidadMedida.ClientID%>');
            let txtSelect = select.options[select.selectedIndex].text;
            let idSelect = select.options[select.selectedIndex].value;
            if (select.value != -1) {
                document.getElementById('valiva').className = 'text-hide';
            }
            if (txtSelect.toUpperCase() == "KILOGRAMOS") {
                document.getElementById('lblBrutoUnidad').textContent = "Kg Br. ";
                document.getElementById('lblBrutoTotal').textContent = "Kg Br. Total";

            } else if (txtSelect.toUpperCase() == "LITRO") {
                document.getElementById('lblBrutoUnidad').textContent = "l Br. ";
                document.getElementById('lblBrutoTotal').textContent = "l Br. Total";
            } else if (txtSelect.toUpperCase() == "METROS") {
                document.getElementById('lblBrutoUnidad').textContent = "m Br. ";
                document.getElementById('lblBrutoTotal').textContent = "m Br. Total";
            } else {
                document.getElementById('lblBrutoUnidad').textContent = "Br. ";
                document.getElementById('lblBrutoTotal').textContent = "Br. Total";
            }

            if (idSelect == "-1") {
                document.getElementById('lblTotalUnidad').textContent = "Rinde ";
                document.getElementById('lblBrutoUnidad').textContent = " Br. ";
                document.getElementById('lblCosto').textContent = "Costo ";
            } else {


                document.getElementById('lblBrutoUnidad').textContent = document.getElementById('lblBrutoUnidad').textContent + txtSelect;

            }


        }

        function EsTipoRecetaValido() {
            let select = document.getElementById('<%= ddlTipoReceta.ClientID%>');

            // Mostrar mensaje de error y devolver false si el valor es -1
            if (select.value == -1) {
                document.getElementById('error-tipo').classList.remove('text-hide');
                return false;
            }
            // Ocultar mensaje de error y devolver true si el valor es válido
            else {
                document.getElementById('error-tipo').classList.add('text-hide');
                return true;
            }
        }

        function EsRubroValido() {
            let select = document.getElementById('<%= ddlRubros.ClientID%>');

            // Mostrar mensaje de error y devolver false si el valor es -1
            if (select.value == -1) {
                document.getElementById('errorRubro').classList.remove('text-hide');
                return false;
            }
            // Ocultar mensaje de error y devolver true si el valor es válido
            else {
                document.getElementById('errorRubro').classList.add('text-hide');
                return true;
            }
        }


        function ActualizarxPorcion() {

            let costototal = parseFloat(document.getElementById('<%=txtCostoTotal.ClientID%>').value.replace(',', ''));
            let cantTotal = parseFloat(document.getElementById('<%= txtKgBrutTotal.ClientID%>').value.replace(',', ''));
            let porciones = parseFloat(document.getElementById('<%=txtRinde.ClientID%>').value);
            if (porciones > 0) {

                document.getElementById('<%=txtKgxPorcion.ClientID%>').value = myFormat(Math.round10(cantTotal / porciones));
                document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = myFormat(costototal / porciones);
            } else {
                document.getElementById('<%=txtKgxPorcion.ClientID%>').value = "0.00";
                document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = "0.00";
            }
        }

        function actualizarCantidadesTabla() {
            let rindeInput = document.getElementById("ContentPlaceHolder1_txtRinde").value;
            // Intentar convertir a float
            let rinde = parseFloat(rindeInput);

            // Si el valor no es un numero o es 0, se asigna 1
            rinde = isNaN(rinde) || rinde === 0 ? 1 : rinde;

            // Obtener todas las filas (tr) dentro del tbody de la tabla
            var filas = document.querySelectorAll("#tableProductos tbody tr.parent");

            // Recorrer cada fila
            filas.forEach(function (fila) {
                // Se toma el valor original y hace el calculo siempre sobre ese valor
                var cantidadOriginal = parseFloat(fila.cells[9].textContent); 
                // Reemplazar valor en la columna Cantidad
                fila.cells[2].textContent = (cantidadOriginal / rinde).toFixed(3);
            });
        }

        function ActualizarxPrVenta() {
            let PRVenta = parseFloat(document.getElementById('<%=txtPrVenta.ClientID%>').value.replace(',', ''));
            let costototal = parseFloat(document.getElementById('<%=txtCostoxPorcion.ClientID%>').value.replace(',', ''));
            let ContMarg = parseFloat(document.getElementById('<%=txtContMarg.ClientID%>').value);
            if (PRVenta > 0) {

                document.getElementById('<%=txtPFoodCost.ClientID%>').value = myFormat(Math.round10(costototal / PRVenta)) + '%';
                document.getElementById('<%=txtContMarg.ClientID%>').value = myFormat(Math.round10(PRVenta - costototal));
            } else {
                document.getElementById('<%=txtPFoodCost.ClientID%>').value = "0%";
                document.getElementById('<%=txtContMarg.ClientID%>').value = "0.00";
            }
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
            let descripcion = document.getElementById('<%=txtDescripcionReceta.ClientID%>').value;
            descripcion = descripcion.toUpperCase();
            document.querySelector('#lblDescripcion1').textContent = descripcion;
            //document.querySelector('#lblDescripcion2').textContent = descripcion;
            document.querySelector('#lblDescripcion3').textContent = descripcion;
            document.querySelector('#lblDescripcion4').textContent = descripcion;
            //document.querySelector('#lblDescripcionFinal').textContent = descripcion;

        }


    </script>

    <script>

        function agregarCategoria(id) {
            ContentPlaceHolder1_txtDescripcionCategoria.value = id.split('_')[2] + ' - ' + id.split('_')[3];
            $('#modalCategoria').modal('hide');
            document.querySelector('#txtBusqueda').value = '';
            let btnAtributos = document.getElementById('btnAtributos');
            btnAtributos.removeAttribute('disabled');
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
            let btnAtributos = document.getElementById('btnAtributos');
            btnAtributos.removeAttribute('disabled');

           <%-- let txtcat = document.getElementById('<%=txtDescripcionCategoria.ClientID%>').value
            const idOption = document.querySelector('option[value="' + txtcat + '"]').id;--%>



            //$.ajax({
            //    method: "POST",
            //    url: "Categorias.aspx/GetSubAtributos2",
            //    data: '{id: "' + idOption.split('_')[1] + '" }',
            //    contentType: "application/json",
            //    dataType: 'json',
            //    error: (error) => {
            //        console.log(JSON.stringify(error));
            //        $.msgbox("No se pudo cargar la tabla", { type: "error" });
            //    },
            //    success: successAgregarTipoAtributo
            //});
        }

        function pulsar(e) {
            if (e.keyCode === 13 && !e.shiftKey) {
                e.preventDefault();
                AgregarPaso();
            }
        }

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

        function OrdenarTablaPasos() {
            var table, rows, switching, i, x, y, shouldSwitch;
            table = document.getElementById("tablePasos");
            switching = true;
            /* Make a loop that will continue until
            no switching has been done: */
            while (switching) {
                // Start by saying: no switching is done:
                switching = false;
                rows = table.rows;
                /* Loop through all table rows (except the
                first, which contains table headers): */
                for (i = 1; i < (rows.length - 1); i++) {
                    // Start by saying there should be no switching:
                    shouldSwitch = false;
                    /* Get the two elements you want to compare,
                    one from current row and one from the next: */
                    x = rows[i].getElementsByTagName("TD")[0];
                    y = rows[i + 1].getElementsByTagName("TD")[0];
                    // Check if the two rows should switch place:
                    if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                        // If so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
                if (shouldSwitch) {
                    /* If a switch has been marked, make the switch
                    and mark that a switch has been done: */
                    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                    switching = true;
                }
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

                    presentacionFinal += table2.rows[i].cells[0].innerHTML + " - " + table2.rows[i].cells[1].innerHTML + ', ';

                }

            }

            $('#modalAtributo').modal('hide');
            document.getElementById('ContentPlaceHolder1_txtDescripcionAtributo').value = presentacionFinal.trimEnd(', ');


            return true;
        }

        function EliminarFila(id) {
            let table1 = document.getElementById('editable1');
            let presentacionFinal = document.querySelector('#lblPresentacion').textContent;
            let max = table1.rows.length;

            for (let i = 1; i < max; i++) {
                if (table1.rows[i].cells[0].innerHTML == id) {
                    if (presentacionFinal.includes(table1.rows[i].cells[1].innerHTML)) {
                        let texto = table1.rows[i].cells[1].innerHTML;
                        presentacionFinal = presentacionFinal.replace(texto, '');
                    }
                    document.getElementById('editable1').rows[i].remove();
                    return;
                }
            }
        }
    </script>
    <script>
        function FocusSearch(accion) {
            //$("#editable2 [type='search']").focus();
            //$("div.editable2_filter input").focus();

            switch (parseInt(accion)) {
                case 1:
                    document.getElementById('txtBusquedaIngredientes').focus;
                    break;
            }
        }
        function editarProductoPH() {
            //document.getElementById('btnAgregarProducto').style.display = 'none';
            document.getElementById('btnEditarProducto').style.display = null

        }


        //Esta funcion agrega un registro a la tabla de productos de la receta
        function agregarProductoPH() {
            let rindeInput = document.getElementById("ContentPlaceHolder1_txtRinde").value;
            // Intentar convertir a float
            let rinde = parseFloat(rindeInput);
            // Si el valor no es un numero o es 0, se asigna 1
            rinde = isNaN(rinde) || rinde === 0 ? 1 : rinde;


            var codigo = ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0];
            var cantidad = ContentPlaceHolder1_txtCantidad.value.replace(',', '');
            var tipo = ContentPlaceHolder1_Hiddentipo.value;
            let unidad = ContentPlaceHolder1_HiddenUnidad.value;
            let costo = ContentPlaceHolder1_HiddenCosto.value;
            let costAux = parseFloat(costo.replace(',', '.'));
            let CantAux = parseFloat(cantidad);
            ////////let factorValue = parseFloat(document.getElementById('<%= txtFactor.ClientID %>').value);
            let factorValue = 1;
            let factor = factorValue.toFixed(2);
            let cantBruta = document.getElementById('<%= txtCantBruta.ClientID %>').value;
            let costototal = 0;
            let ddlSector = document.getElementById('<%= ddlSector.ClientID %>');
            let Tiempo = document.getElementById('<%= TiempoDePreparacion.ClientID %>').value;
            let TiempoFloat = parseFloat(Tiempo);
            let opcionSeleccionada = ddlSector.options[ddlSector.selectedIndex].text;
            let ddlSectoridSectorProductivo = document.getElementById('<%= ddlSector.ClientID %>').value


            //Validar Codigo
            if (codigo == "") {
                ContentPlaceHolder1_txtDescripcionProductos.classList.add('invalid');
                return;
            }
            else {
                ContentPlaceHolder1_txtDescripcionProductos.classList.remove('invalid');
            }


            //Validar Costo Positivo 
            if (isNaN(CantAux) || CantAux <= 0) {
                document.getElementById('<%=txtCantidad.ClientID%>').classList.add('invalid');
                return;
            }
            else {
                document.getElementById('<%=txtCantidad.ClientID%>').classList.remove('invalid');
            }


            //Validar Sector Productivo
            if (ddlSectoridSectorProductivo == "-1") {
                document.getElementById('<%= ddlSector.ClientID %>').classList.add('invalid');
                return;
            }
            else {
                document.getElementById('<%= ddlSector.ClientID %>').classList.remove('invalid');
            }


            //Validar Tiempo
            if (isNaN(TiempoFloat) || TiempoFloat < 0) {
                document.getElementById('<%= TiempoDePreparacion.ClientID %>').classList.add('invalid');
                return;
            }
            else {
                document.getElementById('<%= TiempoDePreparacion.ClientID %>').classList.remove('invalid');
            }



            costototal = Math.round10(costAux * CantAux, -3);
            costototal *= factor; // Multiplicar por factor
            let auxCostoTotal = myFormat(costototal.toString());
            if (!auxCostoTotal.includes('.'))
                auxCostoTotal += ".00";
            //costototal = costototal.toString().replace('.', ',');


            // Redondear cantidad hacia arriba siempre
            let cantRedondeadaAux = Math.ceil(CantAux * 1000) / 1000;  // Redondeo hacia arriba
            let cantRedondeada = cantRedondeadaAux.toFixed(3); // Cantidad inicial
            let cantFinal = (cantRedondeadaAux / rinde).toFixed(3); // Cantidad proporcional al rinde

            let btnRec = "";
            let styleCorrect = "";
            let listaDesplegable = "";
            let listaCantidadDesplegable = "";
            let listaUnidadesDesplegable = "";
            let listaCostosDesplegable = "";
            let listaCostototalDesplegable = "";
            let listaDdlSectorProductivoDesplegable = "";
            let ListaTiempo = "";
            let listaCantidadInicial = "";
            let cellFactor = "";


            //Si lo que se esta agregando a la tabla de productos es una receta entra a este if 
            if (tipo == "Receta") {

                //Verificar que el item no exista en la tabla
                if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + codigo)) 
                {
                    // Obtener del servidor la cadena html cib las rows a insertar
                    $.ajax({
                        method: "POST",
                        url: "RecetasABM.aspx/GenerarFilaReceta_Add",
                        data: JSON.stringify({ id: codigo.trim(), cantidad: cantRedondeada, idSector: ddlSectoridSectorProductivo, tiempo: Tiempo, rinde: rinde }),
                        contentType: "application/json",
                        dataType: 'json',
                        error: (error) => {
                            console.log(JSON.stringify(error));
                        },
                        success: function (respuesta) {
                                <%--var ddlDepositos = $("#<%=ddlSector.ClientID%>");
                                ddlDepositos.val(respuesta.d);--%>

                                $('#tableProductos tbody').append(respuesta.d);
                            }
                        });
                }
                
                //btnRec = "<a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent;\" class=\"btn  btn-xs \" onclick=\"javascript: return CargarmodalRecetaDetalle('" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0] + "');\" >" +
                //    "<i><svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 576 512\" style=\"width: 15px; vertical-align: middle; \">" +
                //    "<path d=\"M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z\" />" +
                //    "</svg>" +

                //    "</i>  </a> ";
                //styleCorrect = "";
                ////Crea cada columna del registro que se esta agregando, pero como es una receta a cada columna le agrega un desplegable, que muestra los ingredientes, cantidades y demas datos de la receta, todo en desplegables
                //listaDesplegable = "<td> <div id=\"jstree" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "\"> <ul><li id='RecetaLI_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "' class=\"jstree-open\">" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[1] + "</li></ul></div></td>";
                //listaCantidadDesplegable = "<td> <div id=\"jstree_C" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "\"> <ul><li id='RecetaC_LI_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "' class=\"jstree-open\">" + myFormat(cantidad) + "</li></ul></div></td>";
                //listaUnidadesDesplegable = "<td> <div id=\"jstree_UM" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "\"> <ul><li id='RecetaUM_LI_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "' class=\"jstree-open\">" + unidad + "</li></ul></div></td>";
                ////cellFactor = "<td> <div <ul><li 'class=\"jstree-open\">" + factor + "</li></ul></div></td>";
                //cellCantBruta = "<td> <div <ul><li 'class=\"jstree-open\">" + cantBruta + "</li></ul></div></td>";
                //listaCostosDesplegable = "<td> <div id=\"jstree_CS" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "\"> <ul><li id='RecetaCS_LI_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "' class=\"jstree-open\">" + costo + "</li></ul></div></td>";
                //listaCostototalDesplegable = "<td> <div id=\"jstree_CST" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "\"> <ul><li id='RecetaCST_LI_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim() + "' class=\"jstree-open\">" + auxCostoTotal + "</li></ul></div></td>";
                //listaDdlSectorProductivoDesplegable = "<td> <div id=\"jstree_SP" + ContentPlaceHolder1_ddlSector.value.split('-')[0].trim() + "\"> <ul><li id='RecetaSP_LI_" + ContentPlaceHolder1_ddlSector.value.split('-')[0].trim() + "' class=\"jstree-open\">" + opcionSeleccionada + "</li></ul></div></td>";
                //ListaTiempo = "<td style=\" text-align:right;\"> <div id=\"jstree_T" + ContentPlaceHolder1_TiempoDePreparacion.value.split('-')[0].trim() + "\"> <ul><li id='RecetaT_LI_" + ContentPlaceHolder1_TiempoDePreparacion.value.split('-')[0].trim() + "' class=\"jstree-open\">" + Tiempo + "</li></ul></div></td>";

            }
            //Si es un producto, entonces viene por el else y genera el html de la row a insertar en la tabla
            else {
                //Aca simplemente agrega cada producto sin desplegables
                listaDesplegable = "<td>&nbsp;&nbsp;&nbsp;&nbsp;" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[1] + "</td>";
                listaCantidadDesplegable = "<td style=\" text-align: right\"> " + cantFinal + "</td>";
                listaUnidadesDesplegable = "<td> " + unidad + "</td>";
                cellFactor = "<td> " + factor + "</td>";
                cellCantBruta = "<td> " + cantBruta + "</td>";
                listaCostosDesplegable = "<td style=\" text-align:right;\"> $" + costo + "</td>";
                listaCostototalDesplegable = "<td style=\" text-align:right;\"> $" + auxCostoTotal + "</td>";
                listaDdlSectorProductivoDesplegable = "<td style=\" text-align:left;\"> " + opcionSeleccionada + "</td>";
                ListaTiempo = "<td style=\" text-align:right;\"> " + Tiempo + "</td>";
                listaCantidadInicial = "<td style=\" text-align:right;\"> " + cantRedondeada + "</td>";
            }

            // Verifica que el item no exista ya en la tabla
            if (!document.getElementById('<%= idProductosRecetas.ClientID%>').value.includes(tipo + '_' + codigo)) {

                // Verificar que se este agregando un producto
                if (tipo !== "Receta") {
                    // Inserta en la tabla una row con el producto ingresado
                    $('#tableProductos').append(
                        "<tr class='parent' id=" + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0] + "\">" +
                        "<td style=\" text-align: right\"> " + codigo + "</td>" +
                        listaDesplegable +
                        listaCantidadDesplegable +
                        listaUnidadesDesplegable +
                        //cellFactor +
                        //cellCantBruta +
                        listaCostosDesplegable +
                        listaCostototalDesplegable +
                        listaDdlSectorProductivoDesplegable +
                        ListaTiempo +

                        //Crea el boton eliminar de cada producto en la tabla, dicho boton tiene tiene un onclick al cual le pasa dos parametros
                        "<td style=\" text-align: center\">" +
                        " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs \" onclick=\"javascript: return borrarProd('" + tipo + "_" + codigo + "');\" >" +
                        "<i class=\"fa fa-trash - o\" style=\"color: darkred\"></i> </a> " +
                        btnRec
                        + "</td > " +
                        listaCantidadInicial +
                        "</tr>"
                    );
                }

                // Actualizar costo total de la receta
                let CostoTotalFinal = document.getElementById('<%=txtCostoTotal.ClientID%>').value;
                CostoTotalFinal = CostoTotalFinal.replace(',', '');
                CostoTotalFinal = parseFloat(CostoTotalFinal);
                let aux;

                // Obtiene el costo total del producto o receta agregado a la tabla y se lo acumula al costo total de la receta
                aux = costototal;
                CostoTotalFinal += parseFloat(aux);
                let CostoTotalFinalAux = CostoTotalFinal.toString();

                if (!CostoTotalFinalAux.includes('.'))
                    CostoTotalFinalAux += ".00";
                document.getElementById('<%=txtCostoTotal.ClientID%>').value = myFormat(CostoTotalFinalAux);



                // Obtiene el kg br total y le acumula los kg br total del ingrediente o receta que se haya agregado a la tabla
                let KgBrutoTotal = parseFloat(document.getElementById('<%=txtKgBrutTotal.ClientID%>').value);
                KgBrutoTotal += CantAux;
                document.getElementById('<%=txtKgBrutTotal.ClientID%>').value = myFormat(KgBrutoTotal);


                // Agrega el item insertado a una variable que acumula los items en un string
                ////////if (document.getElementById('<%= idProductosRecetas.ClientID%>').value == "") {
                document.getElementById('<%= idProductosRecetas.ClientID%>').value += codigo + "," + tipo + "," + cantFinal + "," + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0] + "," + "idSectorProductivo_" + ddlSectoridSectorProductivo + "," + "Tiempo_" + Tiempo + "," + "Factor_" + factor + ";";
                //}
<%--                // Si no es el primer item insertado en la tabla (se agrega un ';' al inicio)
                else {
                    document.getElementById('<%= idProductosRecetas.ClientID%>').value += ";" + codigo + "," + tipo + "," + cantRedondeada + "," + ContentPlaceHolder1_Hiddentipo.value + "_" + ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0] + "," + "idSectorProductivo_" + ddlSectoridSectorProductivo + "," + "Tiempo_" + Tiempo + "," + "Factor_" + factor;
                }--%>


                ////////if (!document.getElementById('<%= txtRinde.ClientID%>').value == "") {
                let rinde = parseFloat(document.getElementById('<%= txtRinde.ClientID%>').value);
                if (rinde > 0) {
                    document.getElementById('<%=txtKgxPorcion.ClientID%>').value = myFormat(myFormat(KgBrutoTotal) / rinde);
                    document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = myFormat(CostoTotalFinal / rinde);
                }
                //}

                // Si hay precio de venta, actualiza las variables relacionadas a este valor (food cost, etc)
                if (!document.getElementById('<%= txtPrVenta.ClientID%>').value == "") {
                    let prVenta = parseFloat(document.getElementById('<%= txtPrVenta.ClientID%>').value);
                    if (prVenta > 0) {
                        ActualizarxPrVenta();

                    }
                }


                if (tipo == "Receta") {
                    //ObtenerListaIntegradoraDetalleReceta(ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim());
                    //setTimeout(ObtenerListaIntegradoraDetalleCantidadReceta(ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim(), myFormat(cantidad)), 10);
                    //setTimeout(ObtenerListaIntegradoraDetalleUnidadesReceta(ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim()), 45);
                    //setTimeout(ObtenerListaIntegradoraDetalleCostosReceta(ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim()), 20);
                    //setTimeout(ObtenerListaIntegradoraDetalleCostosTotalReceta(ContentPlaceHolder1_txtDescripcionProductos.value.split('-')[0].trim(), myFormat(cantidad)), 25);
                }


                // Limpiar campos
                ContentPlaceHolder1_txtDescripcionProductos.value = "";
                ContentPlaceHolder1_txtCantidad.value = "0";
                document.getElementById('<%=txtFactor.ClientID%>').value = "1";
                document.getElementById('<%=txtCantBruta.ClientID%>').value = "0";
                document.getElementById('<%=txtCostoLimpio.ClientID%>').value = "";
                document.getElementById('<%=txtUnidadMed.ClientID%>').value = "";
                document.getElementById('<%=TiempoDePreparacion.ClientID%>').value = "0";
                document.getElementById('<%=ddlSector.ClientID%>').value = "-1";

                document.getElementById('ContentPlaceHolder1_txtDescripcionProductos').focus();
            }
            else {
                if (tipo == "Receta") {
                    toastr.warning("La receta ya fue ingresada.", "Aviso");
                }
                else {
                    toastr.warning("El producto ya fue ingresado.", "Aviso");
                }
            }
        }

        function CargarmodalRecetaDetalle(id) {

            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetProductosRecetaByIdReceta",
                data: '{idReceta: "' + id.trim() + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: successAgregarDetallesRecetas
            });




        }

        function successAgregarDetallesRecetas(response) {
            var obj = JSON.parse(response.d);


            if (obj == null) {
                return;
            }

            var Recetas_ListProduc = obj.split(',');

            let table2 = document.getElementById('editable27');

            table2.getElementsByTagName('tbody')[0].innerHTML = '';
            //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
            for (let j = 0; j < Recetas_ListProduc.length; j++) {



                $("#editable27").append(
                    "<tr>" +
                    "<td style=\" text-align: right\"> " + Recetas_ListProduc[j].split("_")[0] + "</td>" +

                    "<td> " + Recetas_ListProduc[j].split("_")[1] + "</td>" +
                    "<td style=\" text-align: right\"> " + Recetas_ListProduc[j].split("_")[2] + "</td>" +
                    "<td> " + Recetas_ListProduc[j].split("_")[3] + "</td>" +
                    "<td style=\" text-align:right;\"> " + Recetas_ListProduc[j].split("_")[4] + "</td>" +
                    "<td style=\" text-align:right;\"> " + Recetas_ListProduc[j].split("_")[5] + "</td>" +
                    "<td style=\" text-align: center\">" +
                    "</td > " +
                    "</tr>"
                );

            }
            $('#modalRecetaDetalle').modal('show');
        }

        function ObtenerListaIntegradoraDetalleReceta(id) {
            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetListProductosRecetaByIdReceta",
                data: '{idReceta: "' + id.trim() + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: function (respuesta) {
                    console.log(respuesta);
                    var obj2 = JSON.parse(respuesta.d);

                    if (obj2 == null) {
                        return;
                    }
                    $("#RecetaLI_" + id).append(
                        obj2
                    );

                    $('#jstree' + id)
                        .on('after_open.jstree', function (e, data) {

                            let banderaC = false;
                            let JidCant = $("#jstree_C" + id).jstree()._model.data
                            let JidCantaux = Object.values(JidCant);

                            for (i = 0; i < JidCantaux.length; i++) {
                                if (JidCantaux[i].id.includes('_')) {
                                    let res = JidCantaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaC == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_C" + id).jstree("open_node", "#RecetaC_LI_" + id);
                                            banderaC = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_C" + id).jstree("open_node", "#" + JidCantaux[i].id);
                                                banderaC = true;
                                            }
                                    }
                                }
                            }
                            let banderaUM = false;
                            let JidUM = $("#jstree_UM" + id).jstree()._model.data
                            let JidUMTaux = Object.values(JidUM);

                            for (i = 0; i < JidUMTaux.length; i++) {
                                if (JidUMTaux[i].id.includes('_')) {
                                    let res = JidUMTaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaUM == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_UM" + id).jstree("open_node", "#RecetaUM_LI_" + id);
                                            banderaUM = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_UM" + id).jstree("open_node", "#" + JidUMTaux[i].id);
                                                banderaUM = true;
                                            }
                                    }
                                }
                            }

                            let JidCS = $("#jstree_CS" + id).jstree()._model.data
                            let JidCSaux = Object.values(JidCS);
                            let banderaCS = false;

                            for (i = 0; i < JidCSaux.length; i++) {
                                if (JidCSaux[i].id.includes('_')) {
                                    let res = JidCSaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaCS == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_CS" + id).jstree("open_node", "#RecetaCS_LI_" + id);
                                            banderaCS = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_CS" + id).jstree("open_node", "#" + JidCSaux[i].id);
                                                banderaCS = true;
                                            }
                                    }
                                }
                            }

                            let JidCST = $("#jstree_CST" + id).jstree()._model.data
                            let JidCSTaux = Object.values(JidCST);
                            let banderaCST = false
                            for (i = 0; i < JidCSTaux.length; i++) {
                                if (JidCSTaux[i].id.includes('_')) {
                                    let res = JidCSTaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaCST == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_CST" + id).jstree("open_node", "#RecetaCST_LI_" + id);
                                            banderaCST = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_CST" + id).jstree("open_node", "#" + JidCSTaux[i].id);
                                                banderaCST = true;
                                            }
                                    }
                                }
                            }

                            //$("#jstree_C" + id).jstree("open_all", JidCantaux.id);
                            //$("#jstree_UM" + id).jstree("open_all", JidUMTaux.id);
                            //$("#jstree_CS" + id).jstree("open_all", JidCSaux.id);
                            //$("#jstree_CST" + id).jstree("open_all", JidCSTaux.id);
                        })
                        .on('after_close.jstree', function (e, data) {
                            let JidCant = $("#jstree_C" + id).jstree()._model.data
                            let JidCantaux = Object.values(JidCant);
                            let banderaC = false;
                            for (i = 0; i < JidCantaux.length; i++) {
                                if (JidCantaux[i].id.includes('_')) {
                                    let res = JidCantaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaC == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_C" + id).jstree("close_node", "#RecetaC_LI_" + id);
                                            banderaC = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_C" + id).jstree("close_node", "#" + JidCantaux[i].id);
                                                banderaC = true;
                                            }
                                    }
                                }
                            }
                            let banderaUM = false;
                            let JidUM = $("#jstree_UM" + id).jstree()._model.data
                            let JidUMTaux = Object.values(JidUM);

                            for (i = 0; i < JidUMTaux.length; i++) {
                                if (JidUMTaux[i].id.includes('_')) {
                                    let res = JidUMTaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaUM == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_UM" + id).jstree("close_node", "#RecetaUM_LI_" + id);
                                            banderaUM = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_UM" + id).jstree("close_node", "#" + JidUMTaux[i].id);
                                                banderaUM = true;
                                            }
                                    }
                                }
                            }

                            let JidCS = $("#jstree_CS" + id).jstree()._model.data
                            let JidCSaux = Object.values(JidCS);
                            let banderaCS = false
                            for (i = 0; i < JidCSaux.length; i++) {
                                if (JidCSaux[i].id.includes('_')) {
                                    let res = JidCSaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaCS == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_CS" + id).jstree("close_node", "#RecetaCS_LI_" + id);
                                            banderaCS = true
                                        } else
                                            if (res == res2) {
                                                $("#jstree_CS" + id).jstree("close_node", "#" + JidCSaux[i].id);
                                                banderaCS = true
                                            }
                                    }
                                }
                            }

                            let JidCST = $("#jstree_CST" + id).jstree()._model.data
                            let JidCSTaux = Object.values(JidCST);
                            let banderaCST = false;
                            for (i = 0; i < JidCSTaux.length; i++) {
                                if (JidCSTaux[i].id.includes('_')) {
                                    let res = JidCSTaux[i].id.split('_')[1];
                                    console.log(data.node.id)
                                    console.log(data.node.data)
                                    let res2 = data.node.id.split('_')[1];
                                    if (banderaCST == false) {

                                        if (res.includes("LI")) {
                                            $("#jstree_CST" + id).jstree("close_node", "#RecetaCST_LI_" + id);
                                            banderaCST = true;
                                        } else
                                            if (res == res2) {
                                                $("#jstree_CST" + id).jstree("close_node", "#" + JidCSTaux[i].id);
                                                banderaCST = true;
                                            }
                                    }
                                }
                            }
                            //console.log('prueba2');
                            //let JidCant2 = $("#jstree_C" + id).jstree()._model.data
                            //let JidCantaux2 = Object.values(JidCant2)[11];

                            //let JidUM2 = $("#jstree_UM" + id).jstree()._model.data
                            //let JidUMTaux2 = Object.values(JidUM2)[11];

                            //let JidCS2 = $("#jstree_CS" + id).jstree()._model.data
                            //let JidCSaux2 = Object.values(JidCS2)[11];

                            //let JidCST2 = $("#jstree_CST" + id).jstree()._model.data
                            //let JidCSTaux2 = Object.values(JidCST2)[11];

                            //$("#jstree_C" + id).jstree("close_all",  "#"+JidCantaux2.id);
                            //$("#jstree_UM" + id).jstree("close_all", "#"+JidUMTaux2.id);
                            //$("#jstree_CS" + id).jstree("close_all", "#"+JidCSaux2.id);
                            //$("#jstree_CST" + id).jstree("close_all","#"+ JidCSTaux2.id);
                        })
                        .jstree({
                            'core': {
                                'check_callback': true,
                                'themes': {
                                    'icons': false // Aquí se deshabilitan los íconos
                                }
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            }
        
                            //'open_all': "#j2_2",
                            //'closed_all':"#j2_2"
                        });

                }
            });

        }

        function ObtenerListaIntegradoraDetalleCantidadReceta(id, cantidad) {
            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetListProductosCantidadRecetaByIdReceta",
                data: '{idReceta: "' + id.trim() + '",Cant: "' + cantidad + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: function (respuesta) {
                    console.log(respuesta);
                    var obj2 = JSON.parse(respuesta.d);

                    if (obj2 == null) {
                        return;
                    }
                    $("#RecetaC_LI_" + id).append(
                        obj2
                    );

                    $('#jstree_C' + id).jstree({
                        'core': {
                            'check_callback': true,
                            'themes': {
                                'icons': false // Aquí se deshabilitan los íconos
                            }
                        },
                        'plugins': ['types', 'dnd'],
                        'types': {
                            'default': {
                                'icon': 'fa fa-folder'
                            },
                            'html': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'svg': {
                                'icon': 'fa fa-file-picture-o'
                            },
                            'css': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'img': {
                                'icon': 'fa fa-file-image-o'
                            },
                            'js': {
                                'icon': 'fa fa-file-text-o'
                            }

                        }
              
                    });
                }
            });

        }
        function ObtenerListaIntegradoraDetalleUnidadesReceta(id) {
            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetListProductosUMRecetaByIdReceta",
                data: '{idReceta: "' + id.trim() + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: function (respuesta) {
                    console.log(respuesta);
                    var obj2 = JSON.parse(respuesta.d);

                    if (obj2 == null) {
                        return;
                    }
                    $("#RecetaUM_LI_" + id).append(
                        obj2
                    );

                    $('#jstree_UM' + id).jstree({
                        'core': {
                            'check_callback': true,
                            'themes': {
                                'icons': false // Aquí se deshabilitan los íconos
                            }
                        },
                        'plugins': ['types', 'dnd'],
                        'types': {
                            'default': {
                                'icon': 'fa fa-folder'
                            },
                            'html': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'svg': {
                                'icon': 'fa fa-file-picture-o'
                            },
                            'css': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'img': {
                                'icon': 'fa fa-file-image-o'
                            },
                            'js': {
                                'icon': 'fa fa-file-text-o'
                            }

                        }
                
                    });
                }
            });

        }
        function ObtenerListaIntegradoraDetalleCostosReceta(id) {
            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetListProductosCostosRecetaByIdReceta",
                data: '{idReceta: "' + id.trim() + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: function (respuesta) {
                    console.log(respuesta);
                    var obj2 = JSON.parse(respuesta.d);

                    if (obj2 == null) {
                        return;
                    }
                    $("#RecetaCS_LI_" + id).append(
                        obj2
                    );

                    $('#jstree_CS' + id).jstree({
                        'core': {
                            'check_callback': true,
                            'themes': {
                                'icons': false // Aquí se deshabilitan los íconos
                            }
                        },
                        'plugins': ['types', 'dnd'],
                        'types': {
                            'default': {
                                'icon': 'fa fa-folder'
                            },
                            'html': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'svg': {
                                'icon': 'fa fa-file-picture-o'
                            },
                            'css': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'img': {
                                'icon': 'fa fa-file-image-o'
                            },
                            'js': {
                                'icon': 'fa fa-file-text-o'
                            }

                        }
     
                    });
                }
            });

        }

        function ObtenerListaIntegradoraDetalleCostosTotalReceta(id, cantidad) {
            $.ajax({
                method: "POST",
                url: "Categorias.aspx/GetListProductosCostosTotalRecetaByIdReceta",
                data: '{idReceta: "' + id.trim() + '",Cant: "' + cantidad + '" }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                },
                success: function (respuesta) {
                    console.log(respuesta);
                    var obj2 = JSON.parse(respuesta.d);

                    if (obj2 == null) {
                        return;
                    }
                    $("#RecetaCST_LI_" + id).append(
                        obj2
                    );

                    $('#jstree_CST' + id).jstree({
                        'core': {
                            'check_callback': true,
                            'themes': {
                                'icons': false // Aquí se deshabilitan los íconos
                            }
                        },
                        'plugins': ['types', 'dnd'],
                        'types': {
                            'default': {
                                'icon': 'fa fa-folder'
                            },
                            'html': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'svg': {
                                'icon': 'fa fa-file-picture-o'
                            },
                            'css': {
                                'icon': 'fa fa-file-code-o'
                            },
                            'img': {
                                'icon': 'fa fa-file-image-o'
                            },
                            'js': {
                                'icon': 'fa fa-file-text-o'
                            }

                        }
                    });
                }
            });

        }

        function CargarlistaDesplegableRecetaDetalle(respuesta) {
            console.log(respuesta);
            var obj2 = JSON.parse(respuesta.d);

            if (obj2 == null) {
                return;
            }
            $("#RecetaLI_" + "29").append(
                obj2
            );
        }
        function myFormat(str) {
            //const cleaned = str.replace(/[^\d,]/g, '').replace(",", ".")
            return Number(str).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 4 })
        }

    </script>
    <script>
        function agregarProducto(clickedId, costo) {
            ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[2] + ' - ' + clickedId.split('_')[3];
            ContentPlaceHolder1_Hiddentipo.value = "Producto";
            ContentPlaceHolder1_HiddenUnidad.value = clickedId.split('_')[4];
            ContentPlaceHolder1_txtUnidadMed.value = clickedId.split('_')[4];
            ContentPlaceHolder1_HiddenCosto.value = costo;
            document.getElementById('<%=txtCostoLimpio.ClientID%>').value = costo;

            $('input[type=search]').val('');// Clear Search input.
            document.querySelector('#txtBusquedaIngredientes').value = ''

            $('#modalTabsProductos').modal('hide');
            document.getElementById('<%=txtCantidad.ClientID%>').value = '';
            document.getElementById('<%=txtCantidad.ClientID%>').focus();
        }
        function agregarReceta(clickedId, costo) {
            ContentPlaceHolder1_txtDescripcionProductos.value = clickedId.split('_')[2] + ' - ' + clickedId.split('_')[3];
            ContentPlaceHolder1_Hiddentipo.value = "Receta";
            ContentPlaceHolder1_HiddenUnidad.value = clickedId.split('_')[4];
            ContentPlaceHolder1_txtUnidadMed.value = clickedId.split('_')[4];
            ContentPlaceHolder1_HiddenCosto.value = costo;
            document.getElementById('<%=txtCostoLimpio.ClientID%>').value = costo;

            $('input[type=search]').val('');// Clear Search input.
            document.querySelector('#txtBusquedaIngredientes').value = ''

            $('#modalTabsProductos').modal('hide');
            document.getElementById('<%=txtCantidad.ClientID%>').value = '';
            document.getElementById('<%=txtCantidad.ClientID%>').focus();


        }
        function agregarSector() {
            //ContentPlaceHolder1_txtSector.value = id.split('_')[2] + ' - ' + id.split('_')[3];

            let table2 = document.getElementById('editable7');
            let cuerpor = document.getElementById("tbodyEditable1");
            let max = table2.rows.length;
            let presentacionFinal = '';
            //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
            for (let i = 1; i < max; i++) {
                /*if (i > 1) {*/
                if (table2.rows[i].cells[3].children[0].checked) {

                    presentacionFinal += table2.rows[i].cells[0].innerHTML + " - " + table2.rows[i].cells[2].innerHTML + ', ';

                }

            }
            document.getElementById('ContentPlaceHolder1_txtSector').value = presentacionFinal.replace(/,\s*$/, '');

            /*document.getElementById('ContentPlaceHolder1_txtSector').value = presentacionFinal.trimEnd(', ');*/
            //document.getElementById('btnAgregarSectores').classList.remove("buttonLoading");
            /*  document.querySelector('#txtBusquedaSector').value = '';*/
            $('#modalSectores').modal('hide');
        }

        function agregarMarcas() {
            //ContentPlaceHolder1_txtSector.value = id.split('_')[2] + ' - ' + id.split('_')[3];

            let table = document.getElementById('tableMarcas');
            let body = document.getElementById("tbodyMarcas");
            let max = table.rows.length;
            let listaMarcas = '';
            //document.getElementById("btnAgregarPresentacion").children[0].className = "fa fa-check"; 
            for (let i = 1; i < max; i++) {
                /*if (i > 1) {*/
                if (table.rows[i].cells[1].children[0].checked) {
                    let id = table.rows[i].id.split("_")[2]
                    listaMarcas += id + " - " + table.rows[i].cells[0].innerHTML + ', ';
                }
            }
            document.getElementById('ContentPlaceHolder1_txtMarcas').value = listaMarcas.replace(/,\s*$/, '');

            /*document.getElementById('ContentPlaceHolder1_txtSector').value = presentacionFinal.trimEnd(', ');*/
            //document.getElementById('btnAgregarSectores').classList.remove("buttonLoading");
            /*  document.querySelector('#txtBusquedaSector').value = '';*/
            $('#modalMarca').modal('hide');
        }

        //Esta funcion se encarga de borrar el producto de la receta
        function borrarProd(idprod) {
            event.preventDefault();
            //Evalua si el elemento que se esta eliminando de la receta es un producto o una receta usada como ingrediente
            //si es un producto entra aca
            if (idprod.includes("Producto")) {
                let precio = document.getElementById(idprod.trim()).children[5].innerHTML;
                precio = parseFloat(precio.replace(',', ''));
                let CostoTotalFinal = document.getElementById('<%=txtCostoTotal.ClientID%>').value;
                if (CostoTotalFinal.includes(','))
                    CostoTotalFinal = parseFloat(CostoTotalFinal.replace(',', ''));

                CostoTotalFinal -= precio;
                if (!CostoTotalFinal.toString().includes('.'))
                    CostoTotalFinal += ".00";
                document.getElementById('<%=txtCostoTotal.ClientID%>').value = CostoTotalFinal.toString();

                let Cantidad = parseFloat(document.getElementById(idprod.trim()).children[2].innerHTML.replace(',', ''));

                let CantTotal = parseFloat(document.getElementById('<%=txtKgBrutTotal.ClientID%>').value.replace(',', ''));

                CantTotal -= Cantidad;

                document.getElementById('<%=txtKgBrutTotal.ClientID%>').value = Math.round10(CantTotal, -3);

                if (parseFloat(document.getElementById('<%=txtRinde.ClientID%>').value) == 0
                    && precio == 0 && CostoTotalFinal == 0
                ) {
                    document.getElementById('<%=txtKgxPorcion.ClientID%>').innerText = "0";
                    document.getElementById('<%=txtCostoxPorcion.ClientID%>').textContent = "0";
                } else {
                    let CantxUnidad = parseFloat(document.getElementById('<%=txtRinde.ClientID%>').value);
                    if (CantxUnidad > 0) {
                        let x = CantTotal / CantxUnidad;
                        if (!x.toString().includes('.'))
                            x += ".00";
                        document.getElementById('<%=txtKgxPorcion.ClientID%>').value = myFormat(x);
                        let y = CostoTotalFinal / CantxUnidad;
                        if (!y.toString().includes('.'))
                            y += ".00";
                        document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = myFormat(y);
                    } else {
                        document.getElementById('<%=txtKgxPorcion.ClientID%>').value = "0.00";
                        document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = "0.00";
                    }
                }

                $('#' + idprod).remove(); //Elimina la fila seleccionada de la tabla 
                var productos = ContentPlaceHolder1_idProductosRecetas.value.split(';'); //Obtiene todos los productos de la cadena de texto separada por ; y los guarda en la variable
                var nuevosProductos = ""; //En esta variable se van almacenar los productos actuales en la tabla de la receta separados por ;
                for (var x = 0; x < productos.length; x++) {
                    if (productos[x] != "") {
                        if (!productos[x].includes(idprod)) {
                            //guarda los productos actuales que hay en la tabla de la receta separados por ;, de esta forma quita de la cadena de productos 
                            //aquellos que fueron eliminados
                            nuevosProductos += productos[x] + ";";
                        }
                        else {
                            /* var productoAEliminar = productos[x].split(',')[2];*/
                            //ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) - parseFloat(productoAEliminar);
                            //if (ContentPlaceHolder1_txtRinde.value != "") {
                            //    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2);
                            //    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                            //}
                        }
                    }
                }
                //Actualiza la cadena de texto de productos que tenia todos los productos separados por ;, y en su lugar le asigna la cadena de productos actual
                ContentPlaceHolder1_idProductosRecetas.value = nuevosProductos;
            }
            //Si es una receta entra aca
            else {
                let numId = idprod.split('_')[1].trim();
                if (numId.includes("Receta")) {
                    numId = idprod.split('_')[2].trim();
                }
                let precio = document.getElementById('RecetaCST_LI_' + numId).children[1].innerText
                precio = parseFloat(precio.replace(',', ''));
                let CostoTotalFinal = document.getElementById('<%=txtCostoTotal.ClientID%>').value;
                if (CostoTotalFinal.includes(','))
                    CostoTotalFinal = parseFloat(CostoTotalFinal.replace(',', ''));

                CostoTotalFinal -= precio;
                if (!CostoTotalFinal.toString().includes('.'))
                    CostoTotalFinal += ".00";
                document.getElementById('<%=txtCostoTotal.ClientID%>').value = CostoTotalFinal.toString();

                let Cantidad = parseFloat(document.getElementById('RecetaC_LI_' + numId).children[1].innerText.replace(',', ''));

                let CantTotal = parseFloat(document.getElementById('<%=txtKgBrutTotal.ClientID%>').value.replace(',', ''));

                CantTotal -= Cantidad;

                document.getElementById('<%=txtKgBrutTotal.ClientID%>').value = Math.round10(CantTotal, -3);

                if (parseFloat(document.getElementById('<%=txtRinde.ClientID%>').value) == 0
                    && precio == 0 && CostoTotalFinal == 0
                ) {
                    document.getElementById('<%=txtKgxPorcion.ClientID%>').innerText = "0";
                    document.getElementById('<%=txtCostoxPorcion.ClientID%>').textContent = "0";
                } else {
                    let CantxUnidad = parseFloat(document.getElementById('<%=txtRinde.ClientID%>').value);
                    if (CantxUnidad > 0) {
                        let x = CantTotal / CantxUnidad;
                        if (!x.toString().includes('.'))
                            x += ".00";
                        document.getElementById('<%=txtKgxPorcion.ClientID%>').value = myFormat(x);
                        let y = CostoTotalFinal / CantxUnidad;
                        if (!y.toString().includes('.'))
                            y += ".00";
                        document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = myFormat(y);
                    } else {
                        document.getElementById('<%=txtKgxPorcion.ClientID%>').value = "0.00";
                        document.getElementById('<%=txtCostoxPorcion.ClientID%>').value = "0.00";
                    }
                }

                $('#' + idprod).remove(); //Elimina la fila seleccionada de la tabla 
                var productos = ContentPlaceHolder1_idProductosRecetas.value.split(';'); //Obtiene todos los productos de la cadena de texto separada por ; y los guarda en la variable
                var nuevosProductos = ""; //En esta variable se van almacenar los productos actuales en la tabla de la receta separados por ;
                for (var x = 0; x < productos.length; x++) {
                    if (productos[x] != "") {
                        if (!productos[x].includes(idprod)) {
                            //guarda los productos actuales que hay en la tabla de la receta separados por ;, de esta forma quita de la cadena de productos 
                            //aquellos que fueron eliminados
                            nuevosProductos += productos[x] + ";";
                        }
                        else {
                            /* var productoAEliminar = productos[x].split(',')[2];*/
                            //ContentPlaceHolder1_txtPesoBruto.value = parseFloat(ContentPlaceHolder1_txtPesoBruto.value) - parseFloat(productoAEliminar);
                            //if (ContentPlaceHolder1_txtRinde.value != "") {
                            //    ContentPlaceHolder1_txtCoeficiente.value = (parseFloat(ContentPlaceHolder1_txtPesoBruto.value) / parseFloat(ContentPlaceHolder1_txtRinde.value)).toFixed(2);
                            //    ContentPlaceHolder1_hiddenCoeficiente.value = ContentPlaceHolder1_txtCoeficiente.value;

                            //}
                        }
                    }
                }

                //Actualiza la cadena de texto de productos que tenia todos los productos separados por ;, y en su lugar le asigna la cadena de productos actual
                ContentPlaceHolder1_idProductosRecetas.value = nuevosProductos;
            }

        }
        function PasarAFactor(event) {
            if (event.which == 13) {

                ////////document.getElementById('<%=txtFactor.ClientID%>').focus();
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            $('#editable2').DataTable({
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
                }
            });

            $('#editable3').DataTable({
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
                }
            });

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

            $('.dataTables_filter').hide();
            $('#txtBusquedaIngredientes').on('keyup', function () {
                $('#editable2').DataTable().search(
                    this.value
                ).draw();
            });

            $('.dataTables_filter').hide();
            $('#txtBusquedaIngredientes').on('keyup', function () {
                $('#editable3').DataTable().search(
                    this.value
                ).draw();
            });

            $('.dataTables_filter').hide();


            $(document).on('keyup', "input[type='search']", function () {
                var oTable = $('.dataTable').dataTable();
                oTable.fnFilter($(this).val());
            });

        });
        $(document).ready(function () {

            $(".money").inputmask({
                'alias': 'decimal',
                rightAlign: true,
                'groupSeparator': ',',
                'autoGroup': true
            });
        })
    </script>
    <script>
        $(document).ready(function () {

            $('#jstree1').jstree({
                'core': {
                    'check_callback': true,
                    'themes': {
                        'icons': false // Aquí se deshabilitan los íconos
                    }
                },
                'plugins': ['types', 'dnd'],
                'types': {
                    'default': {
                        'icon': 'fa fa-folder'
                    },
                    'html': {
                        'icon': 'fa fa-file-code-o'
                    },
                    'svg': {
                        'icon': 'fa fa-file-picture-o'
                    },
                    'css': {
                        'icon': 'fa fa-file-code-o'
                    },
                    'img': {
                        'icon': 'fa fa-file-image-o'
                    },
                    'js': {
                        'icon': 'fa fa-file-text-o'
                    }

                }
            });

            $('#using_json').jstree({
                'core': {
                    'data': [
                        'Empty Folder',
                        {
                            'text': 'Resources',
                            'state': {
                                'opened': true
                            },
                            'children': [
                                {
                                    'text': 'css',
                                    'children': [
                                        {
                                            'text': 'animate.css', 'icon': 'none'
                                        },
                                        {
                                            'text': 'bootstrap.css', 'icon': 'none'
                                        },
                                        {
                                            'text': 'main.css', 'icon': 'none'
                                        },
                                        {
                                            'text': 'style.css', 'icon': 'none'
                                        }
                                    ],
                                    'state': {
                                        'opened': true
                                    }
                                },
                                {
                                    'text': 'js',
                                    'children': [
                                        {
                                            'text': 'bootstrap.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'inspinia.min.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'jquery.min.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'jsTree.min.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'custom.min.js', 'icon': 'none'
                                        }
                                    ],
                                    'state': {
                                        'opened': true
                                    }
                                },
                                {
                                    'text': 'html',
                                    'children': [
                                        {
                                            'text': 'layout.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'navigation.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'navbar.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'footer.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'sidebar.html', 'icon': 'none'
                                        }
                                    ],
                                    'state': {
                                        'opened': true
                                    }
                                }
                            ]
                        },
                        'Fonts',
                        'Images',
                        'Scripts',
                        'Templates',
                    ]
                    ,
                    'themes': {
                        'icons': false // Aquí se deshabilitan los íconos
                    }
                }
            });

        });

    </script>
    <script>
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
                if (key == 46 || key == 8)//|| key == 44)  Detectar . (punto) , backspace (retroceso) y , (coma)
                { return true; }
                else { return false; }
            }
            return true;
        }
        (function () {
            /**
             * Ajuste decimal de un número.
             *
             * @param {String}  tipo  El tipo de ajuste.
             * @param {Number}  valor El numero.
             * @param {Integer} exp   El exponente (el logaritmo 10 del ajuste base).
             * @returns {Number} El valor ajustado.
             */
            function decimalAdjust(type, value, exp) {
                // Si el exp no está definido o es cero...
                if (typeof exp === 'undefined' || +exp === 0) {
                    return Math[type](value);
                }
                value = +value;
                exp = +exp;
                // Si el valor no es un número o el exp no es un entero...
                if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0)) {
                    return NaN;
                }
                // Shift
                value = value.toString().split('e');
                value = Math[type](+(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp)));
                // Shift back
                value = value.toString().split('e');
                return +(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp));
            }

            // Decimal round
            if (!Math.round10) {
                Math.round10 = function (value, exp) {
                    return decimalAdjust('round', value, exp);
                };
            }
            // Decimal floor
            if (!Math.floor10) {
                Math.floor10 = function (value, exp) {
                    return decimalAdjust('floor', value, exp);
                };
            }
            // Decimal ceil
            if (!Math.ceil10) {
                Math.ceil10 = function (value, exp) {
                    return decimalAdjust('ceil', value, exp);
                };
            }
        })();
    </script>
</asp:Content>
