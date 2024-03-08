<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PedidosOrdenes.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.PedidosOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                                <div style="margin-left: 0px; margin-right: 0px;">
                                                    <div style="display: flex">
                                                        <div style="width: 35%; margin-left: 1rem">
                                                            <div class="input-group m-b">
                                                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                                <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                            </div>
                                                        </div>
                                                        <label style="margin-left: -40px; margin-top: 5px">Desde</label>
                                                        <div>
                                                            <asp:TextBox class="form-control" type="date" runat="server" ID="txtFechaHoy"
                                                                data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;">
                                                            </asp:TextBox>
                                                        </div>
                                                        <label style="margin-top: 5px; margin-left: 10px">Hasta</label>
                                                        <div>
                                                            <asp:TextBox class="form-control" runat="server" type="date"
                                                                ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy"
                                                                Style="margin-left: 0px; width: 100%;">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <table class="table-striped table-bordered table-hover" style="width: 100%" id="editable">
                                                    <thead>
                                                        <tr style="height: 20px">
                                                            <th id="nOrdenTabla" style="width: 5%; margin-top: 20px; height: 10px; padding-left: 4px">Fecha</th>
                                                            <th id="clienteTabla" style="width: 5%; margin-top: 20px; padding-left: 4px">Origen</th>
                                                            <th id="fechaEntregaTabla" style="text-align: left; width: 4%; margin-top: 20px; padding-left: 4px">Destino</th>
                                                            <th id="estadoTabla" style="width: 5%; text-align: left; margin-top: 20px; padding-left: 4px">Estado</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phPedidosOrdenes" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
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

    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
            });

            establecerDiaHoy();

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

           $('#txtBusqueda').on('keyup', function () {
           $('#editable').DataTable().search(
             this.value
             ).draw();


           });
           


           function establecerDiaHoy() {
              var fechas = obtenerRangoFechas();
              // Convertir la fecha en un formato legible para el DatePicker   
              var fechaFormateada1 = (fechas.primerDia.getFullYear() + '/' + (fechas.primerDia.getMonth() + 1) + '/' + fechas.diaActual.getDate())
              var fechaFormateada2 = (fechas.ultimoDia.getFullYear() + '/' + (fechas.ultimoDia.getMonth() + 1) + '/' + fechas.diaActual.getDate())


              document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = formatearFechas(fechaFormateada1);
              document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = formatearFechas(fechaFormateada2);;

           }


           function obtenerRangoFechas() {
              var fechaActual = new Date(); // Obtiene la fecha actual
              let diaActual = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), fechaActual.getDate());
              var primerDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth(), 1); // Primer día del mes actual
              var ultimoDiaMes = new Date(fechaActual.getFullYear(), fechaActual.getMonth() + 1, 0); // Último día del mes actual

              return {
                  primerDia: primerDiaMes,
                  ultimoDia: ultimoDiaMes, 
                  diaActual: diaActual
              };
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

    </script>


</asp:Content>
