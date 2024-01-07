<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pre-Produccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Pre_Produccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />--%>

    <%-- ACA EMPIEZA EL CONTAINER --%>
    <div class="container-fluid"> 
        <div class="ibox float-e-margins">
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="wrapper wrapper-content animated fadeInRight">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content">
                                            <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                                    <%--<div style="display: flex;">--%>
                                                        <%--<div class="input-group m-b">--%>
                                                            <div class="row">
                                                                <%-- Label de la ddl --%>
                                                                <div class="col-md-1" style="margin-left: 15px; margin-right: 15px;">
                                                                    <label class="col-sm-2 control-label">Sector</label>
                                                                </div>
                                                                <%-- Ddl sectores --%>
                                                                <div class="col-md-2">
                                                                    <asp:DropDownList ID="ddlSector" runat="server"
                                                                        CssClass="chosen-select form-control"
                                                                        DataTextField="CountryName" DataValueField="CountryCode"
                                                                        Data-placeholder="Seleccione Rubro..." Width="100%">
                                                                        <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                   <%-- Label desde --%>
                                                               <div class="col-md-1" style="margin-left: 15px; margin-right: 15px;">
                                                                   <label class="col-md-4" style="margin-top: 5px;">Desde</label>
                                                               </div>
                                                                  <%-- DatePicker desde --%>
                                                                  <div class="col-md-2">
                                                                      <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy" 
                                                                          data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                                  </div>
                                                                  <%-- Label hasta --%>
                                                                  <div class="col-md-1" style="margin-right: 15px;">
                                                                      <label style="margin-top: 5px;" class="col-md-4">Hasta</label>
                                                                  </div>
                                                                   <%-- DatePicker hasta --%>
                                                               <div class="col-md-2">
                                                                   <asp:TextBox class="form-control" runat="server" type="date" ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                               </div>
                                                                <%-- Boton Filtrar --%>
                                                                     <div class="col-md-2" >
                                                                         <a id="btnFiltrar" onclick="FiltrarIngredientesDeOrdenesDeProduccion()" class="btn btn-primary pull-right" style="margin-right: 15px;"><i class="fa fa-paper-plane"></i>&nbsp;Filtrar </a>
                                                                     </div>
                                                            </div>
                                                            <%-- Fin de la row --%>
                                                        </div>                                                                                                        
                                                </div>                                          
                                            <%--</div>--%>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="FechaDesde" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="FechaHasta" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="ddlSectorSelecionado" runat="server"></asp:HiddenField>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row" style="padding-left: 14px; padding-right: 14px">
        <!-- Utiliza col-lg-8 para la parte izquierda -->
      <div class="col-lg-7"> 
        <div class="ibox-content m-b-sm border-bottom">
            <div class="col-lg-12" style="background-color: white">


                 <a id="btnDesmarcarTodo" onclick="unCheckAll()" class="btn btn-primary pull-right" 
                     style="margin-right: 15px;" >Desmarcar todo</a>

                 <a id="btnMarcarTodo" onclick="CheckAll()" class="btn btn-primary pull-right" 
                     style="margin-right: 15px;" >Marcar todo</a>
              <%--   <asp:LinkButton ID="LinkButton2" runat="server" 
                     Text="<span class='shortcut-icon icon-check'></span>" 
                     class="btn btn-success"  />--%>

                <div class="row">
                    <div class="col-lg-12">
                        <table class="table table-striped table-bordered table-hover " id="editable">
                            <thead>
                                <tr>
                                    <th style="width: 17%">Fecha</th>
                                    <th style="width: 32%">Receta</th>
                                    <th style="text-align: right; width: 14%">Cantidad</th>
                                    <th style="width: 16%">Estado</th>
                                    <th width: 15%">Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="phIngredientesFiltrados" runat="server"></asp:PlaceHolder>
                            </tbody>
                        </table>
                    </div>
                </div>
 
            <%--    <asp:LinkButton runat="server" ID="DetalleIngredientes" 
                    OnClientClick="DetalleIngredientes_Click(); return false" 
                    class="btn btn-primary dim"
                    style="margin-bottom: 10px;">Detalle ingredientes
                </asp:LinkButton>--%>
                <asp:HiddenField runat="server" ID="detalleRecetas" />
            </div>
        </div>
     </div>

    <%-- Termina el primer wigget --%>

    <%-- Empieza el segundo widget --%>

      <div class="col-lg-5"> 
    <div class="ibox-content m-b-sm border-bottom">
        <div class="col-lg-12" style="background-color: white">

             <%--<a id="btnAccion" onclick="" class="btn btn-primary pull-right" style="margin-right: 15px;">Accion</a>--%>

        <%--     <a id="btnRecepcionSectores" href="#recepcionSectores" class="btn btn-primary pull-right" 
                style="margin-right: 15px;">Recepcion sectores</a>--%>



            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary pull-right" 
                style="margin-right: 15px;" 
                OnClientClick="DetalleIngredientes_Click(); return false">Actualizar</asp:LinkButton>


            <asp:LinkButton ID="btnRecepcionSectores" runat="server" class="btn btn-primary pull-right" 
                style="margin-right: 15px;" 
                OnClientClick="abrirModalRecepcionSector(); return false">Recepcion sectores</asp:LinkButton>


            <div class="row">
                <div class="col-lg-12">
             <table class="table table-bordered table-hover" id="tableProductos" style="margin-top: 2%; max-width: 99%;">
       <thead>
           <tr>
               <th style="width: 1%; text-align: right;">#</th>
               <th style="width: 5%">Insumo/Receta</th>
               <th style="width: 2%; text-align: right;">Cant. Necesaria</th>
               <th style="width: 2%; text-align: right;">Costo Unitario</th>
               <th style="width: 2%; text-align: right;">Stock</th>
               <th style="width: 2%; text-align: left;">Unidad de Med.</th>
               <th style="width: 5%; text-align: center;" hidden="hidden">
                   <div class="row">
                       Unidad Real

           <%--            <div style="float: right; margin-right: 15px;">

                           <a onclick="CargarDatosDeLaTabla()" data-toggle="tooltip" data-placement="top" data-original-title="Rellenar con Cantidad Necesaria">
                               <i class="fa fa-arrow-circle-o-down" style="color: black;"></i>
                           </a>
                       </div>--%>
                   </div>


               </th>
               <th style="width: 2%; text-align: right;" hidden="hidden">Costo total</th>

               <th style="width: 1%; text-align: left;"></th>
           </tr>
       </thead>
       <tbody>
           <asp:PlaceHolder ID="phTablaProductos" runat="server"></asp:PlaceHolder>
       </tbody>
   </table>
                </div>
            </div>
 
        <%--    <asp:LinkButton runat="server" ID="LinkButton1" 
                OnClick="DetalleIngredientes_Click" 
                class="btn btn-primary dim"
                style="margin-bottom: 10px;">Detalle ingredientes</asp:LinkButton>--%>
        </div>
    </div>
 </div>
    </div>




        <div id="modalIngredientes" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" style="width: 40%; height: 50%;">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Recepción/Entrega</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel2">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Recepción</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Ingredientes</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaRecepcion">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <!-- Agregar la clase "collapsed" aquí -->
                                    <div class="ibox-title">
                                        <h5>Entrega</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-down"></i>
                                                <!-- Usar el ícono hacia abajo por defecto -->
                                            </a>
                                            <ul class="dropdown-menu dropdown-user">
                                                <li><a href="#">Config option 1</a>
                                                </li>
                                                <li><a href="#">Config option 2</a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-hover no-margins table-bordered">
                                            <thead>
                                                <tr>
                                                    <td><strong>Sector Productivo</strong></td>
                                                    <td><strong>Receta</strong></td>
                                                    <td class="text-right"><strong>Cantidad</strong></td>
                                                </tr>
                                            </thead>
                                            <tbody id="tablaEntrega">
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


     <div id="modalRecepcionEntrega" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
     <div class="modal-dialog" style="width: 40%; height: 50%;">
         <div class="modal-content">

             <div class="modal-header">
                 <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                 <h4 class="modal-title">Recepción/Entrega</h4>
             </div>
             <div class="modal-body">
                 <div id="MainContent_UpdatePanel3">
                     <div class="row">
                         <div class="col-lg-12">
                             <div class="ibox float-e-margins">
                                 <!-- Agregar la clase "collapsed" aquí -->
                                 <div class="ibox-title">
                                     <h5>Recepción</h5>
                                     <div class="ibox-tools">
                                         <a class="collapse-link">
                                             <i class="fa fa-chevron-down"></i>
                                             <!-- Usar el ícono hacia abajo por defecto -->
                                         </a>
                                         <ul class="dropdown-menu dropdown-user">
                                             <li><a href="#">Config option 1</a>
                                             </li>
                                             <li><a href="#">Config option 2</a>
                                             </li>
                                         </ul>
                                     </div>
                                 </div>
                                 <div class="ibox-content">
                                     <table class="table table-hover no-margins table-bordered">
                                         <thead>
                                             <tr>
                                                 <td><strong>Sector Productivo</strong></td>
                                                 <td><strong>Producto</strong></td>
                                                 <td class="text-right"><strong>Cantidad</strong></td>
                                             </tr>
                                         </thead>
                                         <tbody id="tablaRecepcionDerecha">
                                         </tbody>
                                     </table>
                                 </div>
                             </div>
                         </div>
                     </div>



                     <div class="row">
                         <div class="col-lg-12">
                             <div class="ibox float-e-margins">
                                 <!-- Agregar la clase "collapsed" aquí -->
                                 <div class="ibox-title">
                                     <h5>Entrega</h5>
                                     <div class="ibox-tools">
                                         <a class="collapse-link">
                                             <i class="fa fa-chevron-down"></i>
                                             <!-- Usar el ícono hacia abajo por defecto -->
                                         </a>
                                         <ul class="dropdown-menu dropdown-user">
                                             <li><a href="#">Config option 1</a>
                                             </li>
                                             <li><a href="#">Config option 2</a>
                                             </li>
                                         </ul>
                                     </div>
                                 </div>
                                 <div class="ibox-content">
                                     <table class="table table-hover no-margins table-bordered">
                                         <thead>
                                             <tr>
                                                 <td><strong>Sector Productivo</strong></td>
                                                 <td><strong>Receta</strong></td>
                                                 <td class="text-right"><strong>Cantidad</strong></td>
                                             </tr>
                                         </thead>
                                         <tbody id="tablaEntregaDerecha">
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


    <%-- Aca empieza el modal RwcepcionPorSectores --%>


           <div id="recepcionSectores" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
            <div class="modal-dialog" style="width: 55%; height: 65%;">
                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">Recepción/Entrega</h4>
                    </div>
                    <div class="modal-body">
                        <div id="MainContent_UpdatePanel6">
                        
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <!-- Agregar la clase "collapsed" aquí -->
                                            <div class="ibox-title">
                                                <h5>Recepción</h5>
                                                <div class="ibox-tools">
                                                    <a class="collapse-link">
                                                        <i class="fa fa-chevron-down"></i>
                                                        <!-- Usar el ícono hacia abajo por defecto -->
                                                    </a>
                                                    <ul class="dropdown-menu dropdown-user">
                                                        <li><a href="#">Config option 1</a>
                                                        </li>
                                                        <li><a href="#">Config option 2</a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="ibox-content">
                                                <table class="table table-hover no-margins table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <td><strong>Insumo/Receta</strong></td>
                                                            <td class="text-right"><strong>Cant. Necesaria</strong></td>
                                                            <td class="text-right"><strong>Costo Unitario</strong></td>
                                                            <td class="text-right"><strong>Unidad de Med.</strong></td>
                                                            <td class="text-right"><strong>Sector Productivo</strong></td>
                                                            <td><strong>Acciones</strong></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tablaRecepcionSector">
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

    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <!-- Chosen -->
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
           function abrirModalRecepcionSector(){
                 
                 cargarRecepcionAgrupadaPorSector()
                 $('#recepcionSectores').modal('show');   
           }

           function cargarRecepcionAgrupadaPorSector(){
                
                let hiddenFieldValue = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                let hiddenFieldValueArray = hiddenFieldValue.split(";").filter(Boolean);
                let idsRecetas = "";
                hiddenFieldValueArray.forEach(producto => {
                     const ingredienteData = producto.split(',');
                      let idReceta = ingredienteData[1];
                      idsRecetas += idReceta + ",";
                         console.log("ingredientes: " + idsRecetas);
                  });

                      $.ajax({
                      method: "POST",
                      url: "Pre-Produccion.aspx/getRecepcionEntregaAgrupadoPorSector",
                      data: '{idsRecetas: "' + idsRecetas + '"}',
                      contentType: "application/json",
                      dataType: "json",
                      dataType: "json",
                      async: false,
                      error: (error) => {
                          console.log(JSON.stringify(error));
                      },
                      success: (data) => {
                           const ingredientesArray = data.d.split(';').filter(Boolean); 
                           document.getElementById('tablaRecepcionSector').innerHTML = "";
                           ingredientesArray.forEach(producto => {
                               const ingredienteData = producto.split(',');


                                   const ingredienteId = ingredienteData[0];
                                   const ingredienteNombre = ingredienteData[1];
                                   const Cantidad = ingredienteData[2];
                                   const unidadMedida = ingredienteData[3];
                                   const costo = ingredienteData[4];
                                   const SectorProductivo = ingredienteData[5];
                                   const idSector = ingredienteData[6];
                                   let faRecepcion = "";
                                   faRecepcion = "<button id=\"Recepcion\" type=\"button\" class=\"icon-button\" style=\"margin-left: 5px; color: black; border: none;\" title=\"Recepción\" onclick=\"Recepcionar(" + ingredienteId + ",'" + ingredienteNombre + "','" + idSector + "','" + SectorProductivo + "')\"><i class=\"fa fa-shopping-cart\"></i></button>";


                                       let plantillaIngredientes = `
                                        <tr>
                                            <td>${ingredienteNombre}</td>
                                            <td style="text-align: right;">${Cantidad}</td>
                                            <td style="text-align: right;">${costo}</td>
                                            <td style="text-align: right;">${unidadMedida}</td>
                                            <td style="text-align: right;">${SectorProductivo}</td>
                                            <td>${faRecepcion}</td>
                                        </tr>
                                    `;
                                    document.getElementById('tablaRecepcionSector').innerHTML += plantillaIngredientes;



                                   console.log(ingredienteId + " " + ingredienteNombre + " " + Cantidad + " " + unidadMedida + " " + costo + " " + SectorProductivo)
                                   //const sectorProductivo = ingredienteData[3];
                           });

                          //AgregarATabla(respuesta.d);
                          //document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent = "";
                          /*  PedirStockTotal(id);*/
             
                      }
                  });
           }

       </script>

       <script>
       function CheckAll() {
           var total = 0;
           for (var i = 1; i < document.getElementById('editable').rows.length; i++) {
              // document.getElementById('editable').rows[i].cells[4].childNodes[1].children[0].checked = true;
             let checkbox = document.getElementById('editable').rows[i].cells[6].getElementsByTagName('input')[0];


             if(checkbox != null){
                checkbox.checked = true;



                let id = document.getElementById('editable').rows[i].cells[5].innerText;
                let sectorProductivo = document.getElementById('editable').rows[i].cells[4].innerText;
                let cantidad = document.getElementById('editable').rows[i].cells[2].innerText;
                let descripcion = document.getElementById('editable').rows[i].cells[1].innerText;


                  console.log(id + " " + sectorProductivo + " " + checkbox.id + " " + cantidad + " " + descripcion)
                    // Ejecutar manualmente el evento onchange


                   // let rowId = document.getElementById('editable').rows[i].id;
                   //  let onchangeFunction = checkbox.onchange;
                   //
                     verIngredientesReceta(checkbox.id, id, cantidad, descripcion, sectorProductivo);
             }

               //  function verIngredientesReceta(idCheckbox, idReceta, cantidad, descripcion, sectorProductivo)
               //
               // if (onchangeFunction && typeof onchangeFunction === 'function') {
                //  onchangeFunction.apply(checkbox);  // Asegurar que 'this' sea la casilla de verificación
       // }
           }
       }
       </script>
        <script>
            function Recepcionar(ingredienteId, ingredienteNombre, idSector, SectorProductivo){
                //window.location.href = "../Compras/Entregas.aspx";
                //console.log("Me recepciono");
                //window.location.href = "Pre-Produccion.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH + "&SP=" + idSectorProductivo;

                //console.log(SectorProductivo)
                window.open("../Compras/Entregas.aspx?idP=" + ingredienteId + "&Desc=" + ingredienteNombre + "&idS=" + idSector + "&SP=" + SectorProductivo, '_blank');
            }
        </script>

     <script>
             function unCheckAll() {
                 var total = 0;
                 for (var i = 1; i < document.getElementById('editable').rows.length; i++) {
                    // document.getElementById('editable').rows[i].cells[4].childNodes[1].children[0].checked = true;

                       let checkbox = document.getElementById('editable').rows[i].cells[6].getElementsByTagName('input')[0];
                     if(checkbox != null){
                          checkbox.checked = false;



                      let id = document.getElementById('editable').rows[i].cells[5].innerText;
                      let sectorProductivo = document.getElementById('editable').rows[i].cells[4].innerText;
                      let cantidad = document.getElementById('editable').rows[i].cells[2].innerText;
                      let descripcion = document.getElementById('editable').rows[i].cells[1].innerText;


                        console.log(id + " " + sectorProductivo + " " + checkbox.id + " " + cantidad + " " + descripcion)
                          // Ejecutar manualmente el evento onchange


                         // let rowId = document.getElementById('editable').rows[i].id;
                         //  let onchangeFunction = checkbox.onchange;
                         //
                        verIngredientesReceta(checkbox.id, id, cantidad, descripcion, sectorProductivo);
                         //  function verIngredientesReceta(idCheckbox, idReceta, cantidad, descripcion, sectorProductivo)
                         //
                         // if (onchangeFunction && typeof onchangeFunction === 'function') {
                          //  onchangeFunction.apply(checkbox);  // Asegurar que 'this' sea la casilla de verificación
                    }

             // }
                 }
             }
     </script>


    <script>

        $(document).ready(function () {

            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value
            let ddlSectorProductivo = document.getElementById("ContentPlaceHolder1_ddlSector").value
            ddlSectorProductivo.value;


            if (fechad != "" && fechah != "" && ddlSectorProductivo.value != "") {

                establecerFechasSeleccionadas();
                establecerSectorSeleccionado();
            } else {

                establecerDiaHoy();
            }

            var oTable = $('#editable').dataTable({
                "bLengthChange": false,
                "pageLength": 100, // Establece la cantidad predeterminada de registros por página
                "lengthMenu": [25, 50, 87, 100], // Opciones de cantidad de registros por página
            });


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

            $("#editable_filter").css('display', 'none');
        });

        function establecerSectorSeleccionado(){
            let ddlSectorProductivo = document.getElementById("ContentPlaceHolder1_ddlSectorSelecionado").value
            document.getElementById("ContentPlaceHolder1_ddlSector").value = ddlSectorProductivo;
            
            
        }


        function verStock(idReceta, cantidad){
        
            console.log(idReceta)
              fetch('Pre-Produccion.aspx/getIngredientesRecetaByIdReceta', {
              method: 'POST',
              body: JSON.stringify({ idReceta: idReceta, cantidad: cantidad }),
              headers: { 'Content-Type': 'application/json' }
              })
              .then(response => response.json())
              .then(data => {
          
          
              //const ingredientesArray = data.d.split(';');
              const ingredientesArray = data.d.split(';').filter(Boolean); 
              console.log(ingredientesArray)
               document.getElementById('tablaRecepcion').innerHTML = "";
               ingredientesArray.forEach(producto => {
                    // Dividir cada elemento en valores individuales usando coma como separador
                    const ingredienteData = producto.split(',');

                    // Ahora puedes acceder a valores específicos del producto
                    const ingredienteId = ingredienteData[0];
                    const ingredienteNombre = ingredienteData[1];
                    const Cantidad = ingredienteData[2];


                     let plantillaIngredientes = `
                     <tr>
                         <td>${ingredienteNombre}</td>
                         <td style="text-align: right;">${Cantidad}</td>
                     </tr>
                 `;
                 document.getElementById('tablaRecepcion').innerHTML += plantillaIngredientes;

               });
              $('#modalIngredientes').modal('show');
                                    
          
              })
              .catch(error => {
                  // Código para manejar errores aquí
                  console.error('Error:', error);
              });

        }

        function establecerFechasSeleccionadas() {
            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fehcah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

            fechad = fechad.replaceAll("/", "-");
            fehcah = fehcah.replaceAll("/", "-");

            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechad;
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fehcah
        }

        function establecerDiaHoy() {
            var fechas = obtenerRangoFechas();
            // Convertir la fecha en un formato legible xpara el DatePicker   
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.primerDia.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.ultimoDia.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

        }

        function DetalleIngredientes_Click(){
        
            let hiddenFieldValue = document.getElementById("<%= detalleRecetas.ClientID %>").value;
            let elementos = hiddenFieldValue.split(";");
            let idsRecetas = "";
            console.log(elementos)
            elementos.forEach(producto => {
                const productoData = producto.split(',');
                if (productoData[0].trim() !== "") {

                    let idCheckbox = productoData[0];
                    let idReceta = productoData[1];
                    idsRecetas += idReceta + ",";

                    console.log("id: " +idReceta)
                }
             //   idsRecetas += idReceta
             });
            //console.log(idsRecetas);
             CargarTablaReceta(idsRecetas)

        }

        function formatearFechas(fecha) {

            var partes = fecha.split('/');
            var dia = partes[2];
            var mes = partes[1];
            var anio = partes[0];

            if (dia < 10) {
                dia = '0' + dia;
            }

            if (mes < 10) {
                mes = '0' + mes;
            }

            return fechafinal = anio + '-' + mes + '-' + dia;

        }


        function obtenerRangoFechas() {
            var fechaActual = new Date(); // Obtiene la fecha actual
            var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1); // Primer día del mes actual
            var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0); // Último día del mes actual

            return {
                primerDia: primerDiaMes,
                ultimoDia: ultimoDiaMes
            };
        }


        function FiltrarIngredientesDeOrdenesDeProduccion() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value.replaceAll("-", "/");
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replaceAll("-", "/");
            /*let SectorProducivo = document.getElementById("ContentPlaceHolder1_ddlSector");*/

            // Obtener el DropDownList
            let ddlSector = document.getElementById("ContentPlaceHolder1_ddlSector");

            // Obtener el texto de la opción seleccionada
            let idSectorProductivo = ddlSector.value;

            window.location.href = "Pre-Produccion.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH + "&SP=" + idSectorProductivo;
        }

        function verIngredientesReceta(idCheckbox, idReceta, cantidad, descripcion, sectorProductivo)
        {

            var chk1 = document.getElementById(idCheckbox);
            cantidad = cantidad.replace(/,/g, '.');

              if (chk1.checked) {
                  
                  console.log("Estoy checkeada") 
                  if(document.getElementById("<%= detalleRecetas.ClientID %>").value == ""){
                    document.getElementById("<%= detalleRecetas.ClientID %>").value = idCheckbox + "," + idReceta + "," + cantidad + "," + descripcion + "," + sectorProductivo + ";" 
                  console.log(document.getElementById("<%= detalleRecetas.ClientID %>").value) 
                  DetalleIngredientes_Click();         
                }
                  else{
                    document.getElementById("<%= detalleRecetas.ClientID %>").value += idCheckbox + "," + idReceta + "," + cantidad + "," + descripcion + "," + sectorProductivo + ";" 
                  console.log(document.getElementById("<%= detalleRecetas.ClientID %>").value) 
                  DetalleIngredientes_Click();         
                    
                  }
                  //CargarTablaReceta(idReceta);
                  
              }

              else{
                  console.log("No estoy checkeada")
                  let productosSeleccionados = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                  console.log("prodSeleccionados:" + productosSeleccionados)
                  productosSeleccionados = productosSeleccionados.split(';');
                  let nuevosProductos = ""
                  for (var x = 0; x < productosSeleccionados.length; x++) {
                     if (productosSeleccionados[x] != "") {
                         if (!productosSeleccionados[x].includes(idReceta)) {
                             //guarda los productos actuales que hay en la tabla de la receta separados por ;, de esta forma quita de la cadena de productos 
                             //aquellos que fueron eliminados
                             nuevosProductos += productosSeleccionados[x] + ";";
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

                 document.getElementById("<%= detalleRecetas.ClientID %>").value = nuevosProductos;
                 DetalleIngredientes_Click();
                }
            }


         function CargarTablaReceta(idsRecetas) {
             $.ajax({
                 method: "POST",
                 url: "Pre-Produccion.aspx/GetProductosEnRecetas2",
                 data: '{idsRecetas: "' + idsRecetas + '"}',
                 contentType: "application/json",
                 dataType: "json",
                 dataType: "json",
                 async: false,
                 error: (error) => {
                     console.log(JSON.stringify(error));
                 },
                 success: (respuesta) => {
                     AgregarATabla(respuesta.d);
                     //document.getElementById("ContentPlaceHolder1_StockAlteradoFinal").textContent = "";
                     /*  PedirStockTotal(id);*/

                 }
             });
         }


          function AgregarATabla(list) {
             $('#tableProductos tbody')[0].innerHTML = "";
             let listArr = list.split(";");
             for (let i = 0; i < listArr.length; ++i) {
                 if (listArr[i].length > 1) {
                     idCostoFinal = "costofinal" + (i + 1);
                     let item = listArr[i].split(",");
                     let id = item[0].trim();
                     let name = item[1].trim();
                     let cantNecesaria = Number(item[2].replace(',', '.')).toFixed(2);
                     let stock = Number(item[3].replace(',', '.')).toFixed(2)
                     let unidad = item[4].trim();
                     let tipo = item[5].trim();
                     let costo = item[6].trim();

                     let faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-dollar\"></i> <a>"
                    
                     let Costo = "<td style=\" text-align: right\"> " + formatearNumero(Number(costo)) + "</td>";
                     let CostoFinal = "<td style=\" text-align: right\" id=\"" + idCostoFinal.trim() + "\" text-align: right\"> " + 0 + "</td>"
                     let faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-P" + "')>  <i id=\"" + id + "-P" + "\" class=\"fa fa-exchange\"></i> <a>"
                     //let faReceta = "<td></td > ";
                     let faReceta = "";

                     
                     if (tipo == "Receta") {
                         faChangeProduct = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Producto  style=\"color: black;\"  onclick=ModalCambiarProducto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-exchange\"></i> <a>"
                         //faReceta = "<td><a data-toggle=tooltip data-placement=top data-original-title=Elegir_Stock  style=\"color: black;\"  onclick=ModalTablaStocks('" + id + "-R" + "')> <i id=\"" + id + "-R" + "\" class=\"fa fa-calculator\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Ver_Receta href=/Formularios/Maestros/RecetasABM.aspx?a=2&i=" + id + "&b=1" + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-search-plus\"></i> </a>   <a data-toggle=tooltip data-placement=top data-original-title=Producir_Receta href=GenerarProduccion.aspx?i=" + id + " " + "target=\"_blank\" style=\"color: black;\" > <i class=\"fa fa-cutlery\"></i> </a> " + faChangeProduct + faCosto + "  <button id=\"Recepcion\" type=\"button\" class=\"icon-button\" style=\"float: right; margin-left: 5px; color: black;\" title=\"Recepción\" onclick=\"MostrarRecepcion(" + id + "," + cantNecesaria + ")\"><i class=\"fa fa-arrows-h\"></i></button></td>";
                         faReceta = "<td> <button id=\"Recepcion\" type=\"button\" class=\"icon-button\" style=\"float: right; margin-left: 5px; color: black; border: none;\" title=\"Recepción\" onclick=\"MostrarRecepcion(" + id + "," + cantNecesaria + ")\"><i class=\"fa fa-arrows-h\"></i></button></td>";
                         faCosto = "<a data-toggle=tooltip data-placement=top data-original-title=Cambiar_Costo  style=\"color: black;\"  onclick=ModalCambiarCosto('" + id + "-R" + "')>  <i id=\"" + id + "-R" + "\" class=\"fa fa-dollar\"></i> <a>"
                     }
                     
                     let Nombre = "<td> " + name + "</td>";
                     let ID = "<td style=\" text-align: right\"> " + id + "</td>";
                     let STOCK = stock > cantNecesaria ? "<td id=\"" + stock + "-" + name.replaceAll(" ", "") + "-" + id.trim() + "\" style=\" text-align: right;\"> " + stock + "</td>" : "<td id=\"" + stock + "-" + name.replaceAll(" ", "") + "-" + id.trim() + "\" style=\" text-align: right; color: red;\"> " + stock + "</td>"
                     let CantNecesaria = "<td style=\" text-align: right\"> " + formatearNumero(Number(cantNecesaria)) + "</td>";
                     let Unidad = "<td style=\" text-align: left\">" + unidad + "</td>";
                     

                    $('#tableProductos').append(
                        "<tr id='" + tipo + "%" + id + "%" + cantNecesaria + "'>" +
                        ID +
                        Nombre +
                        CantNecesaria +
                        Costo +
                        STOCK +
                        Unidad +
                        //"<td style=\" text-align: right\">" +
                        //"<input type=\"number\" onchange=\"CambiarCostoTotal('" + idCostoFinal + "','" + costo + "',this)\" style=\"width: 100%; text-align: right;\" placeholder=\"Ingresa la cantidad real que utilizaste\" /></td>" +
                        //CostoFinal +
                        faReceta +
                        "</tr>"
                    );

                 }
             }
     //document.getElementById("DivTablaRow").style.display = "flex"
 }

          function formatearNumero(numero) {
             return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
         }

         function MostrarRecepcion(idReceta, cantidad){
             console.log("modal" + idReceta, cantidad)
              //$('#modalRecepcionEntrega').modal('show');


               fetch('Pre-Produccion.aspx/getIngredientesRecetaByIdRecetaSectorProductivo', {
                 method: 'POST',
                 body: JSON.stringify({ idReceta: idReceta, cantidad: cantidad }),
                 headers: { 'Content-Type': 'application/json' }
                 })
                 .then(response => response.json())
                 .then(data => {
          
          
                 //const ingredientesArray = data.d.split(';');
                 const ingredientesArray = data.d.split(';').filter(Boolean); 
                 console.log(ingredientesArray)
                  document.getElementById('tablaRecepcionDerecha').innerHTML = "";
                  ingredientesArray.forEach(producto => {
                       // Dividir cada elemento en valores individuales usando coma como separador
                       const ingredienteData = producto.split(',');

                       // Ahora puedes acceder a valores específicos del producto
                       const ingredienteId = ingredienteData[0];
                       const ingredienteNombre = ingredienteData[1];
                       const Cantidad = ingredienteData[2];
                       const sectorProductivo = ingredienteData[3];


                        let plantillaIngredientes = `
                        <tr>
                            <td>${sectorProductivo}</td>
                            <td>${ingredienteNombre}</td>
                            <td style="text-align: right;">${Cantidad}</td>
                        </tr>
                    `;
                    document.getElementById('tablaRecepcionDerecha').innerHTML += plantillaIngredientes;

                  });
                  //Aca termina el foreach

                         let recetasSeleccionadas = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                         let hiddenFieldValue = document.getElementById("<%= detalleRecetas.ClientID %>").value;
                         let elementos = hiddenFieldValue.split(";");
                         let idsRecetas = "";
                         elementos.forEach(producto => {
                             const productoData = producto.split(',');
                             if (productoData[0].trim() !== "") {

                                 let idReceta = productoData[1];
                                 idsRecetas += idReceta + ",";

                             }
                          //   idsRecetas += idReceta
                          });
                  //Aca empieza el segundo fetch
                        fetch('Pre-Produccion.aspx/getRecetasEntrega', {
                          method: 'POST',
                          body: JSON.stringify({ idsRecetas: idsRecetas, recetasSeleccionadas: recetasSeleccionadas, idReceta: idReceta }),
                          headers: { 'Content-Type': 'application/json' }
                          })
                          .then(response => response.json())
                          .then(data => {

                                const recetasArray = data.d.split(';').filter(Boolean); 
                                document.getElementById('tablaEntregaDerecha').innerHTML = "";
                                console.log("Recetas:" + recetasArray)
                                recetasArray.forEach(receta => {
                                    const recetaData = receta.split(',');

                                    const idReceta = recetaData[0];
                                    const cantidadReceta = recetaData[2];
                                    const descripcionReceta = recetaData[3];
                                    const sectorReceta = recetaData[4];


                                         let plantillaRecetas = `
                                         <tr>
                                             <td>${sectorReceta}</td>
                                             <td>${descripcionReceta}</td>
                                             <td style="text-align: right;">${cantidadReceta}</td>
                                         </tr>
                                     `;


                                    document.getElementById('tablaEntregaDerecha').innerHTML += plantillaRecetas;

                                    console.log( "elemento0: " + recetaData[0])
                                    console.log( "elemento1:" + recetaData[1])
                                    console.log( "elemento2:" + recetaData[2])
                                    console.log( "elemento3:" + recetaData[3])
                                    console.log( "elemento4:" + recetaData[4])
                                });

                           })
                            .catch(error => {
                             // Código para manejar errores aquí
                             console.error('Error:', error);
                            });
                             $('#modalRecepcionEntrega').modal('show');
                        
          
                             })
                       .catch(error => {
                           // Código para manejar errores aquí
                           console.error('Error:', error);
                       });


         }


    </script>


</asp:Content>
