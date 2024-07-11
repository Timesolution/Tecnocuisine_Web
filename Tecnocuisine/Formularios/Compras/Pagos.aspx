<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Pagos.aspx.cs" Inherits="Tecnocuisine.Compras.Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        tr > td > a {
            color: black !important;
            text-decoration: none;
        }

            tr > td > a:hover, a:focus {
                color: black;
                text-decoration: none;
            }
    </style>
   <div class="wrapper wrapper-content">
       <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px;padding-top: 0px;">
           <div class="col-lg-12">
               <div class="col-md-6" style="height: 100%; border-right: 1px solid #ccc;">
                            <div class="p-xs">
                                <div class="pull-left m-r-md">
                                    <i class="fa fa-dollar text-navy mid-icon"></i>
                                </div>
                                <h2 id="DivSaldo" style="font-weight: bold;">0.00</h2>
                                <span id="ClienteSelec">Proveedor no Seleccionado</span>
                            </div>
                 </div>
                 <div class="col-md-6">
                            <div class="p-xs">
                                <div class="pull-left m-r-md">
                                    <i class="fa fa-dollar text-navy mid-icon"></i>
                                </div>
                                <h2 id="DivSaldo2" style="font-weight: bold;">0.00</h2>
                                <span id="ClienteSelec2">Documentos Seleccionado</span>
                            </div>
                 </div>

               </div>
           </div>
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
                                                                       
                                                 <div class="col-md-6">

                                                      
                                                         <div class="input-group m-b">
                                                                <div style="display: flex;">
                                                                    <span class="input-group-addon" style="padding-right: 15%;"><i style='color: black;' class='fa fa-search'></i></span>
                                                                    <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 200%" />
                                                                </div>
                                                            </div>
                                                            

                                                         
                                              



                                            </div>
                                                   

                                                        
                                                    <div class="col-md-6">
                                                        <div class="row" style="display: flex;">

                                                       
                                                        <div class="input-group m-b">
                                                        <div class="row">
                                                            <div class="col-md-2" style="margin-right: 15px;">
                                                                <label style="margin-top: 5px;margin-right: 60px;" class="col-md-4">Proveedor</label>
                                                            </div>
                                                            <div class="col-md-8">

                                                                <datalist id="Datalist1" runat="server"></datalist>
                                                                <input name="txtProveedor" type="text" id="txtProveedor" onchange="ValidarProveedor()" list="ContentPlaceHolder1_ListaProveedores" class="form-control" style="margin-left: 0px; margin-bottom:15px;">
                                                                          <p id="ValivaProveedor" class="text-danger text-hide">Tienes que ingresar un Proveedor</p>
                                                            </div>
                                                       
                                                        </div>
                                                    </div>


                                                           <div class="input-group m-b">
                                                        <div class="row">
                                                            <div class="col-md-" style="margin-right: 15px;">
                                                                  <div style="display: flex; gap:15px">
                                                             <a type="button" data-toggle="modal" onclick="FiltrarCuentaCorriente()" class="btn btn-success">Filtrar&nbsp;<i style="color:white;" class="fa fa-filter"></i></a>
                                                             <a class="btn btn-sm btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="CALCULA FC A CANCELAR" onclick="AbrirModal()" text="Agregar" id="Button1">
                                                            <i style="color: white;padding: 2px;font-size: 18px;" class="fa fa-dollar"></i>
                                                        </a>
                                                        </div>
                                                            </div>
                                                         

                                                        </div>
                                                    </div>


                                                    <%--      <a type="button" data-toggle="modal" onclick="FiltrarCuentaCorriente()" class="btn btn-success">Filtrar&nbsp;<i style="color:white" class="fa fa-filter"></i></a>
                                                        <a class="btn btn-sm btn-primary" style="margin-right: 8px;" data-toggle="tooltip" data-placement="top" data-original-title="CALCULA FC A CANCELAR" onclick="AbrirModal()" text="Agregar" id="Button1">
                                                            <i style="color: white;padding: 2px;font-size: 18px;" class="fa fa-dollar"></i>
                                                        </a>--%>

                                                  
                                            
                                                    

                                      </div>
                                                       
                                                        </div>
                                                </div>

                                      <%--          <div class="col-md-12">
                                                    <div style="display:flex; flex-direction: row; justify-content:center; align-items: center;" id="DivSaldo">
                                                        <h4 style="margin-right: 15px;">Saldo:</h4>
                                                    </div>

                                                </div>--%>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th style=" text-align: right; width: 2%;">#</th>
                                                            <th  style="width: 2%;">Fecha</th>
                                                            <th style=" text-align: left; width: 8%;">Descripción</th>
                                                            <th style="width: 3%; text-align: right;">Debe</th>
                                                            <th style="width: 3%; text-align: right;">Haber</th>
                                                            <th style="width: 3%; text-align: right;">Saldo</th>
                                                            <th style="width: 2%";></th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phCuentaCorriente" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                                 <div style="display: flex; justify-content: end;">

                            <button class="btn btn-sm btn-primary" style="margin-right: 25px;" data-toggle="tooltip" data-placement="top" data-original-title="Imputar Facturas"
                                text="Imputar" validationgroup="AgregarEntregas" id="btnGuardar" onclick="ValidarOption(event)" disabled="disabled">
                                IMPUTAR</button>
                        </div>
                                            </div>
                                        </div>
                        <datalist id="ListaProveedores" runat="server">
                        </datalist>
                        <asp:HiddenField id="SaldoTotal" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="AliasCliente" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="CobroAutomatico" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="MontoCobroAutomatico" runat="server"></asp:HiddenField>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
   <%-- FILTRAR--%>
  <%--  <div id="modalBusqueda" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title">Busqueda</h4>
                        </div>
                        <div class="modal-body">
                            <div id="MainContent_UpdatePanel2">
	

                                    <div role="form" class="form-horizontal col-md-12">
                                    
                                            <label class="col-md-4">Desde</label>
                                            <div class="col-md-6">
                                                <div class="form-group" id="data_1">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                </div>
                                            </div>
                                            </div>
                                       
                                        
                                            <label class="col-md-4">Hasta</label>
                                            <div class="col-md-6">
                                            <div class="form-group" id="data_2">
                                                    <div class="input-group date">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            
                                        </div>
                                        
                                            <label class="col-md-4">Buscar Cliente</label>
                                            <div class="col-md-6" style="padding: 0px;">
                                                <input name="txtProveedor" type="text" id="txtProveedor" onchange="ValidarProveedor()" list="ContentPlaceHolder1_ListaProveedores" class="form-control" style="margin-left: 0px; margin-bottom:15px;">
                                                 <p id="ValivaProveedor" class="text-danger text-hide">Tienes que ingresar un Proveedor</p>
                                            </div>
                                       


                                        </div>
                                      
                                       
                                     
                                   
                                
                          </div>
                        </div>


                       
                        <div class="modal-footer" style="border-color: transparent;">
                    <a id="btnFiltrar" onclick="FiltrarCuentaCorriente()" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Filtrar </a>
                    <input type="hidden" name="ctl00$ContentPlaceHolder1$hiddenEditar" id="ContentPlaceHolder1_hiddenEditar">
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
                      
                    </div>

                </div>
            </div>--%>
    <%--AUTOSELECCION FACTURAS--%>
    <div id="modalAutoSelect" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h4 class="modal-title">Calcular Facturas a Cancelar</h4>
                        </div>
                        <div class="modal-body">
                            <div id="MainContent_UpdatePanel3">
	

                                    <div role="form" class="form-horizontal col-md-12">
                                    
                                           
                                       
                                            <label class="col-md-4">Ingrese Monto</label>
                                            <div class="col-md-6" style="padding: 0px;">
                                                <input name="txtMontoCancelar" type="text" id="txtMontoCancelar" onchange="FormatearNumero(this)" class="form-control" style="margin-left: 0px; margin-bottom:15px;"></input>
                                                 <p id="txtMontoCancelarValiva" class="text-danger text-hide">Tienes que ingresar un Monto</p>
                                            </div>
                                        
                                            
                                            
                                        </div>
                                        
                                       


                                      
                                      
                                       
                                     
                                   
                                
                          </div>
                        </div>


                       
                        <div class="modal-footer" style="border-color: transparent;">
                    <a id="btnFiltrar2" onclick="procesarMonto()" class="btn btn-primary"><i class="fa fa-check"></i>&nbsp;Aceptar </a>
                    <input type="hidden" name="ctl00$ContentPlaceHolder1$hiddenEditar" id="ContentPlaceHolder1_hiddenEditar2" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
                      
                    </div>

                </div>
            </div>


    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>
    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
            document.getElementById("ContentPlaceHolder1_CobroAutomatico").value = "false";

            saldo = document.getElementById("ContentPlaceHolder1_SaldoTotal").value;
            prov = document.getElementById("ContentPlaceHolder1_AliasCliente").value;
            if (saldo != "") {

                document.getElementById("DivSaldo").innerText = saldo;
                document.getElementById("ClienteSelec").innerText = prov;
            }
            document.getElementById("txtBusqueda").addEventListener("input", buscarEnTabla);
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

      
        });

        function ValidarOption(e) {
            e.preventDefault();
            /*console.log("ENTRE")*/
            var rows = $('#editable tbody tr'); // Obtener todas las filas de la tabla
            var proveedores = []; // Crear un array para almacenar los proveedores de las filas seleccionadas
            var ids = []; // Crear un array para almacenar los IDs de las filas seleccionadas

            rows.each(function () {
                var chkBox = $(this).find('input[type="checkbox"]'); // Obtener el input checkbox de la fila actual
                if (chkBox.is(':checked')) { // Verificar si el checkbox está seleccionado
                    var id = $(this).attr('id').split('_')[1]; // Obtener el ID de la fila sin el prefijo "ContextPlaceHolder_"
                    ids.push(id); // Agregar el ID al array
                }
            });


            let idsString = "";
            for (let item of ids) {
                idsString += item += "-";
            }
            let idcliente = document.getElementById("ClienteSelec").innerText.split("-")[0].trim();
            if (document.getElementById("ContentPlaceHolder1_CobroAutomatico").value == "true") {
                let total = document.getElementById("ContentPlaceHolder1_MontoCobroAutomatico").value;
                // es el proveedor pero deje cliente porque copie y pegue del de COBROS!
                window.location.href = "ImputarPagos.aspx?i=" + idsString + "&t=" + total + "&c=" + idcliente;
            } else {
                window.location.href = "ImputarPagos.aspx?i=" + idsString + "&c=" + idcliente;
            }
        }


        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function revertirNumero(numeroFormateado) {
            var numero = parseFloat(numeroFormateado.replace(/,/g, ''));
            return numero;
        }

        function FormatearNumero(input) {
            let value = input.value
            if (value != "" && !value.includes(",")) {
                input.value = formatearNumero(Number(input.value));

            } else {
                input.value = formatearNumero(revertirNumero(value));
            }
        }


        // Habilitar Boton
        function ChangeButton() {
            let input = document.getElementById("DivSaldo2").innerText;
            if (input.trim() != "0.00") {
                document.getElementById("btnGuardar").removeAttribute("disabled");
            } else {
                document.getElementById("btnGuardar").disabled = "disabled";
            }
        }



        function procesarMonto() {
            // Guarda Valor oculto
            document.getElementById("ContentPlaceHolder1_MontoCobroAutomatico").value = document.getElementById("txtMontoCancelar").value;
            let monto = revertirNumero(document.getElementById("txtMontoCancelar").value)
            document.getElementById("ContentPlaceHolder1_CobroAutomatico").value = "true";
            document.getElementById("DivSaldo2").innerText = document.getElementById("txtMontoCancelar").value;
            var filas = document.getElementById('editable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
            for (var i = 0; i < filas.length; i++) {
                var importe = parseFloat(filas[i].cells[5].textContent.replace(/,/g, ''));

                if (monto > 0) {
                    var checkBox = filas[i].cells[6].getElementsByTagName('input')[0];
                    checkBox.checked = true;
                    monto -= importe;
                    /* calcularImporte2(importe)*/
                } else {
                    var checkBox = filas[i].cells[6].getElementsByTagName('input')[0];
                    if (checkBox.checked == true) {
                        checkBox.checked = false;
                    }
                }
            }
            /*  toastr.info('Quedo un monto de: ' + monto + ' sin Cancelar');*/
            $("#modalAutoSelect").modal("hide");
            ChangeButton();
        }
        function calcularImporte2(importe) {
            // Obtener la fila padre del checkbox



            // Realizar la acción deseada con el importe (por ejemplo, acumularlo en una variable)
            // Aquí se muestra un ejemplo de acumulación en una variable llamada totalImporte
            var totalImporte = 0;
            let importeFinal = Number(document.getElementById("DivSaldo2").innerText);
            importeFinal += importe;

            document.getElementById("DivSaldo2").innerText = formatearNumero(importeFinal);
            // Hacer algo con el totalImporte (por ejemplo, mostrarlo en otro lugar de la página)
        }

        function calcularImporte(checkbox) {

            // Obtener la fila padre del checkbox
            var row = checkbox.parentNode.parentNode;

            // Obtener el valor de la columna "Importe" en la última celda de la fila
            var importeCell = row.cells[row.cells.length - 2]; // Última celda antes de la celda de acción
            var importe = revertirNumero(importeCell.innerText); // Eliminar caracteres no numéricos

            // Realizar la acción deseada con el importe (por ejemplo, acumularlo en una variable)
            // Aquí se muestra un ejemplo de acumulación en una variable llamada totalImporte
            var totalImporte = 0;
            let importeFinal = revertirNumero(document.getElementById("DivSaldo2").innerText);
            if (document.getElementById("ContentPlaceHolder1_CobroAutomatico").value == "true") {
                DeshabiliarTodosLosCheck();
            } else {
                if (checkbox.checked) {
                    importeFinal += importe;
                } else {
                    importeFinal -= importe;
                }

                document.getElementById("DivSaldo2").innerText = formatearNumero(importeFinal);
                ChangeButton()
            }
            // Hacer algo con el totalImporte (por ejemplo, mostrarlo en otro lugar de la página)
        }

        function DeshabiliarTodosLosCheck() {
            var filas = document.getElementById('editable').getElementsByTagName('tbody')[0].getElementsByTagName('tr');
            for (var i = 0; i < filas.length; i++) {
                var checkBox = filas[i].cells[6].getElementsByTagName('input')[0];
                checkBox.checked = false;
            }
            document.getElementById("DivSaldo2").innerText = "0.00";
            document.getElementById("ContentPlaceHolder1_CobroAutomatico").value = "false";
            ChangeButton();
        }


        function AbrirModal() {
            $("#modalAutoSelect").modal("show");
        }
        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function FormatearNumeroMontoCancelar(txt) {
            txt.value = formatearNumero(txt.value);
        }





        function FiltrarCuentaCorriente() {
            let Proveedor = document.getElementById("txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaProveedor");
            if (Proveedor == "") {
                proveedorValiva.className = "text-danger"
                return
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }

            window.location.href = "Pagos.aspx?p=" + Proveedor.split("-")[0].trim();
        }
        function ValidarProveedor() {
            let Proveedor = document.getElementById("txtProveedor").value
            let proveedorValiva = document.getElementById("ValivaProveedor");
            if (Proveedor == "") {
                proveedorValiva.className = "text-danger"
            } else {
                proveedorValiva.className = "text-danger text-hide"
            }
        }
        function establecerDiaHoy() {
            var fechaActual = new Date();

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = fechaActual.getDate() + '/' + (fechaActual.getMonth() + 1) + '/' + fechaActual.getFullYear();
            var fechaFormateada2 = ("01" + '/' + "01" + '/' + "2000");
            // Establecer la fecha actual como valor predeterminado del DatePicker 
            $('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada2);
            $('#ContentPlaceHolder1_txtFechaVencimiento').datepicker('setDate', fechaFormateada);

        }
        function buscarEnTabla() {
            // Obtener el valor del input de búsqueda
            var valorBusqueda = document.getElementById("txtBusqueda").value.toLowerCase();

            // Obtener las filas de la tabla
            var filasTabla = document.getElementById("editable").getElementsByTagName("tr");

            // Recorrer las filas de la tabla y ocultar/mostrar según la búsqueda
            for (var i = 1; i < filasTabla.length; i++) {
                var fila = filasTabla[i];
                var celdas = fila.getElementsByTagName("td");
                var coincide = false;

                for (var j = 0; j < celdas.length; j++) {
                    var celda = celdas[j];
                    if (celda.innerText.toLowerCase().indexOf(valorBusqueda) > -1) {
                        coincide = true;
                        break;
                    }
                }

                if (coincide) {
                    fila.style.display = "";
                } else {
                    fila.style.display = "none";
                }
            }
        }

        // Agregar evento de cambio en el input de búsqueda
        document.getElementById("txtBusqueda").addEventListener("input", buscarEnTabla);
    </script>

</asp:Content>
