<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pre-Produccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.Pre_Produccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />

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
                                                                      <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row" style="padding-left: 14px; padding-right: 14px">
        <!-- Utiliza col-lg-8 para la parte izquierda -->
        <div class="ibox-content m-b-sm border-bottom">
            <div class="col-lg-12" style="background-color: white">
                <div class="row">
                    <div class="col-lg-12">
                        <table class="table table-striped table-bordered table-hover " id="editable">
                            <thead>
                                <tr>
                                    <th style="width: 35%">Fecha</th>
                                    <th width: 25%">Receta</th>
                                    <th style="text-align: right; width: 20%">Cantidad</th>
                                    <th width: 25%">Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="phIngredientesFiltrados" runat="server"></asp:PlaceHolder>
                            </tbody>
                        </table>
                    </div>
                </div>
 
                <asp:LinkButton runat="server" ID="DetalleIngredientes" 
                    OnClick="DetalleIngredientes_Click" 
                    class="btn btn-primary dim"
                    style="margin-bottom: 10px;">Detalle ingredientes</asp:LinkButton>
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

        $(document).ready(function () {

            let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
            let fechah = document.getElementById("ContentPlaceHolder1_FechaHasta").value


            if (fechad != "" && fechah != "") {

                establecerFechasSeleccionadas();
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
            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.primerDia.getDate())
            var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.ultimoDia.getDate())


            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

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
            let SectorProducivo = ddlSector.options[ddlSector.selectedIndex].text;

            window.location.href = "Pre-Produccion.aspx?FechaD=" + FechaD + "&FechaH=" + FechaH + "&SP=" + SectorProducivo;
        }


    </script>


</asp:Content>
