<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenesDeProduccion.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.OrdenesDeProduccion" %>

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
                                                <%--class="row"--%>
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
                                                                Style="margin-left: 0px; width: 100%;"></asp:TextBox>

                                                        </div>
                                                        <a id="btnfiltrar" onclick="filtrarordenesproduccion()" class="btn btn-primary" title="filtrar" style="margin-left: 23%; margin-right: 10px; height: 32px">
                                                            <i class="fa fa-check"></i>&nbsp;filtrar
                                                        </a>

                                                        <a href="OrdenesDeProduccionABM.aspx?Accion=1" class="buttonLoading btn btn-primary" style="height: 32px" title="Agregar orden"><i class='fa fa-plus'></i></a>

                                                    </div>



                                                    <div style="display: flex">
                                                        <label style="margin-top: 6px; margin-left: 10px">Cliente</label>
                                                        <div style="width: 17%; margin-left: 10px">
                                                            <div class="form-group" id="data_3">
                                                                <datalist id="ListaNombreCliente" runat="server">
                                                                </datalist>
                                                                <asp:TextBox class="form-control" runat="server" ID="TxtClientes"
                                                                    Style="margin-left: 0px; width: 100%;"
                                                                    list="ContentPlaceHolder1_ListaNombreCliente"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <label style="margin-top: 6px; margin-left: 10px; margin-right: 10px">Estado</label>
                                                        <datalist id="ListaEstados" runat="server">
                                                        </datalist>
                                                        <asp:TextBox class="form-control" runat="server" ID="txtEstados"
                                                            Style="margin-left: 0px; width: 15%;"
                                                            list="ContentPlaceHolder1_ListaEstados"></asp:TextBox>
                                                        <div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" style="display: flex; flex-direction: row; align-items: center; justify-content: end;">
                                                    </div>
                                                </div>

                                                <table class="table-striped table-bordered table-hover" style="width: 100%" id="editable">
                                                    <thead>
                                                        <tr style="height: 20px">
                                                            <th id="nOrdenTabla" style="width: 5%; margin-top: 20px; height: 10px; padding-left: 4px">Nº Orden</th>
                                                            <th id="clienteTabla" style="width: 5%; margin-top: 20px; padding-left: 4px">Cliente</th>
                                                            <th id="fechaEntregaTabla" style="text-align: left; width: 4%; margin-top: 20px; padding-left: 4px">Fecha entrega</th>
                                                            <th id="estadoTabla" style="width: 5%; text-align: left; margin-top: 20px; padding-left: 4px">Estado</th>
                                                            <th id="accionesTabla" style="width: 5%; margin-top: 20px; padding-left: 4px">Acciones</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phOrdenesProduccion" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>

                                          

                                                <asp:TextBox runat="server" ID="IDsOrdenesDeProduccion" Text="" Style="display: none"></asp:TextBox>

                                                <a href="CronogramaProduccion.aspx" class="btn btn-primary dim" target="_blank"
                                                    id="cronogramaProduccionURL" style="margin-right: 1%; float: right"
                                                    title="Cronograma Producción" onclick="SeleccioneOrden()">Cronograma Produccion</a>
                                                
                                                <a href="DetalleIngrediente.aspx" class="btn btn-primary dim" target="_blank"
                                                    id="detalleIngredienteURL" style="margin-right: 1%; float: right"
                                                    title="Detalle Ingredientes" onclick="SeleccioneOrden()">Detalle Ingredientes</a>
                                            </div>
                                        </div>
                                        <datalist id="ListaProveedores" runat="server">
                                        </datalist>
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

    <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title">Busqueda</h4>
                </div>
                <div class="modal-body">
                    <div id="MainContent_UpdatePanel2">
                        <div role="form" class="form-horizontal col-md-12">
                        </div>
                    </div>
                </div>


                <div class="modal-footer" style="border-color: transparent;">
                    <input type="hidden" name="ctl00$ContentPlaceHolder1$hiddenEditar" id="ContentPlaceHolder1_hiddenEditar">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" title="Cancelar"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
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





    <script>

        var countSelectedCheckboxes = 0;

        $(document).ready(function () {
            toastr.options = {
                "positionClass": "toast-bottom-right" // Puedes usar "toast-bottom-right", "toast-bottom-left", etc.
            };

            $("body").tooltip({ selector: '[data-toggle=tooltip]' });

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

             establecerDiaHoy();
             //establecerFechaActual():

            document.getElementById("cronogramaProduccionURL").removeAttribute("href");
            document.getElementById("detalleIngredienteURL").removeAttribute("href");
        });

        function cambiarEstado()
        {

               let idOrdenDeProduccion = document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value
                

               fetch('OrdenesDeProduccion.aspx/cambiarEstadoDeLaOrdenSeleccionada', {
               method: 'POST',
               body: JSON.stringify({idOrdenDeProduccion: idOrdenDeProduccion}),
               headers: { 'Content-Type': 'application/json' }
                  })
                   .then(response => response.json())
                   .then(data => {

                      

                          
                    window.location.href = "OrdenesDeProduccion.aspx";

                   })
                   .catch(error => {
                       console.error('Error:', error);
                   });
        }

        function establecerFechaActual(){
        
            // Obtén la fecha actual
            var fechaActual = new Date();

            // Formatea la fecha como "dd/mm/yyyy"
            var dd = String(fechaActual.getDate()).padStart(2, '0');
            var mm = String(fechaActual.getMonth() + 1).padStart(2, '0'); // Los meses son indexados desde 0
            var yyyy = fechaActual.getFullYear();

            var fechaFormateada = dd + '/' + mm + '/' + yyyy;

            // Establece la fecha formateada en los campos de entrada
            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechaFormateada;
            document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fechaFormateada;

        }


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

         function establecerFechasSeleccionadas() {
         let fechad = document.getElementById("ContentPlaceHolder1_FechaDesde").value
         let fehcah = document.getElementById("ContentPlaceHolder1_FechaHasta").value

         fechad = fechad.replaceAll("/", "-");
         fehcah = fehcah.replaceAll("/", "-");

         document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechad;
         document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value = fehcah
     }

        function filtrarordenesproduccion() {
            let FechaD = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
            let FechaH = document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
            let Cliente = document.getElementById("ContentPlaceHolder1_TxtClientes").value
            let numeroEncontrado = Cliente.match(/^\d+/);
            let IdCliente = parseInt(numeroEncontrado[0], 10)

            let estado = document.getElementById("ContentPlaceHolder1_txtEstados").value
            let numeroEstado = estado.match(/^\d+/);
            let idEstado = parseInt(numeroEstado[0], 10)

            window.location.href = "OrdenesDeProduccion.aspx?c=" + IdCliente + "&FechaD=" + FechaD + "&FechaH=" + FechaH + "&Estado=" + idEstado;
        }


   
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }


        function guardarIdOrdenDeProduccion(idOrdenDeProduccion) {

            let chk2 = document.getElementById("ContentPlaceHolder1_chkSeleccionar_" + idOrdenDeProduccion);

            if (chk2.checked == true) {
                countSelectedCheckboxes++;

                if ((document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value) == "") {
                    document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value = idOrdenDeProduccion + ",";
                }
                else {
                    document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value += idOrdenDeProduccion + ",";
                }

                <%--console.log(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value)--%>
            }

            else {
                let idsOrdenes = document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value;
                // Crear una expresión regular para buscar el ID en la cadena
                let regex = new RegExp('\\b' + idOrdenDeProduccion + '\\b', 'g');
                // Reemplazar el ID encontrado en la cadena por una cadena vacía
                let nuevaCadena = idsOrdenes.replace(regex, '');
                // Quitar las comas duplicadas si es necesario
                nuevaCadena = nuevaCadena.replace(/,+/g, ',');
                // Quitar comas al principio y al final de la cadena
                nuevaCadena = nuevaCadena.replace(/^,|,$/g, '');
                // Asignar la nueva cadena como el valor del elemento de texto oculto
                document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value = nuevaCadena + ",";

                countSelectedCheckboxes--;

                <%--console.log(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value)--%>
            }

         
            //Habilitar href en los botones si hay checkboxs seleccionados
            if (countSelectedCheckboxes > 0) {
                var enlace = document.getElementById("cronogramaProduccionURL");
                enlace.href = "CronogramaProduccion.aspx?ids=" + encodeURIComponent(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value);

                var enlace2 = document.getElementById("detalleIngredienteURL");
                enlace2.href = "DetalleIngrediente.aspx?ids=" + encodeURIComponent(document.getElementById('<%= IDsOrdenesDeProduccion.ClientID %>').value);
            }
            //Deshabilitar href en los botones si no hay checkboxs seleccionados
            else {
                document.getElementById("cronogramaProduccionURL").removeAttribute("href");
                document.getElementById("detalleIngredienteURL").removeAttribute("href");
            }

        }

        function SeleccioneOrden() {
            if (countSelectedCheckboxes <= 0) {
                toastr.error("Seleccione una orden", "Error")
            }
        }

    </script>

        <script>
            $(document).ready(function () {
                document.getElementById("lblSiteMap").innerText = "Produccion / Ordenes";
            });
        </script>
</asp:Content>
