<%@ Page Title="Imputar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Imputar.aspx.cs" Inherits="Tecnocuisine.Caja.Imputar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="ibox-content m-b-sm border-bottom" style="margin-top: 10px; padding-top: 0px;">

            <div class="p-xs">


                <div style="display: flex;">
                    <div style="display: flex; justify-content: flex-start">
                        <h3 style="color:#1ab394;font-weight: bold;">Cliente </h3>
                        <h3 style="font-weight: bold; margin-left:5px" id="ClienteSelec"></h3>
                    </div>
                    <div style="width: 70%; display: flex; justify-content: end;">
                        <h3 style="color:#1ab394;font-weight: bold;">Recibo De Cobro N°</h3>
                        <h3 style="font-weight: bold; margin-left:5px" id="ReciboCobroNumero"></h3>
                    </div>
                </div>

            </div>
        </div>
        <div style="margin-left: 0px; margin-right: 0px;" class="row">
            <h3 style="margin-left: 15px" class="h3">Documentos Impagos</h3>
            <table class="table table-striped table-bordered table-hover " id="editable">
                <thead>
                    <tr>
                        <th style="text-align: left; width: 5%;">ID</th>
                        <th style="text-align: left; width: 50%;">Numero</th>
                        <th style="width: 25%; text-align: left;">Saldo</th>
                        <th style="width: 25%; text-align: left;">Imputar</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="phCuentaCorriente" runat="server"></asp:PlaceHolder>
                </tbody>
            </table>
        </div>
        <div class="container-fluid">

            <div class="ibox float-e-margins">

                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>


                            <div class="panel-body">
                                <div class="panel-group" id="accordion">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h5 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" class="collapsed">Efectivo</a>
                                            </h5>
                                        </div>
                                        <div id="collapseOne" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                            <div class="panel-body">
                                                <div class="col-lg-6">
                                                    <div class="row">

                                                        <div>

                                                            <div class="col-md-4">
                                                                <h4 style="margin-left: 5%">Tipo:</h4>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList class="form-control" ID="ListClientes" runat="server">
                                                                    <asp:ListItem Value="Pesos">Pesos</asp:ListItem>
                                                                </asp:DropDownList>


                                                            </div>
                                                        </div>


                                                        <div>

                                                            <div class="col-md-4" style="margin-top: 15px;">
                                                                <h4 style="margin-left: 5%">Importe:</h4>
                                                            </div>
                                                            <div class="col-md-8" style="margin-top: 15px;">
                                                                <asp:TextBox class="form-control" ID="txtImporteEfectivo" onchange="ChangeEfectivo(this)" Style="text-align: right;" runat="server">
                                                                    
                                                                </asp:TextBox>


                                                            </div>
                                                        </div>



                                                        <div>

                                                            <div class="col-md-4" style="margin-top: 15px;">
                                                                <button class="btn btn-sm btn-primary" style="margin-right: 25px;" data-toggle="tooltip" data-placement="top" data-original-title="Guardar"
                                                                    text="Guardar" validationgroup="AgregarEntregas" id="btnGuardar" onclick="AgregarEfectivo(event)" disabled="disabled">
                                                                    Guardar</button>
                                                            </div>
                                                            <div class="col-md-8">
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" class="collapsed" aria-expanded="false">Cheques</a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">




                                            <div class="col-lg-6" style="margin-bottom: 15px">
                                                <div class="row">

                                                    <div>

                                                        <div class="col-md-4">
                                                            <h4 style="margin-left: 5%">Fecha:</h4>
                                                        </div>
                                                        <div class="col-md-8">
                                                           
                                                                    <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" type="date" data-date-format="dd/mm/yyyy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                               
                                                        </div>
                                                    </div>


                                                    <div>

                                                        <div class="col-md-4">
                                                            <h4 style="margin-left: 5%;margin-top: 15px;">Importe:</h4>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox class="form-control" ID="txtImporteCheque" Style="text-align: right;margin-top: 15px;" onchange="ChangeCheque(this)" runat="server">
                                                                    
                                                            </asp:TextBox>


                                                        </div>
                                                    </div>

                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Numero:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:TextBox class="form-control" type="number" ID="txtNumeroCheque" runat="server">
                                                                    
                                                            </asp:TextBox>


                                                        </div>
                                                    </div>

                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Banco:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <datalist id="ListBancos" runat="server"></datalist>
                                                            <asp:TextBox class="form-control" ID="txtBancoList" list="ContentPlaceHolder1_ListBancos" runat="server"></asp:TextBox>


                                                        </div>
                                                    </div>






                                                </div>

                                            </div>

                                            <div class="col-lg-6" style="margin-bottom: 15px">
                                                <div class="row">
                                                </div>

                                                <div>

                                                    <div class="col-md-4">
                                                        <h4 style="margin-left: 5%">Cuenta:</h4>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:TextBox class="form-control" ID="txtCuenta" runat="server">
                                                                    
                                                        </asp:TextBox>


                                                    </div>
                                                </div>


                                                <div>

                                                    <div class="col-md-4" style="margin-top: 15px;">
                                                        <h4 style="margin-left: 5%">Cuit:</h4>
                                                    </div>
                                                    <div class="col-md-8" style="margin-top: 15px;">
                                                        <asp:TextBox class="form-control" ID="txtCuitCheque" runat="server">
                                                                    
                                                        </asp:TextBox>


                                                    </div>
                                                </div>

                                                <div>

                                                    <div class="col-md-4" style="margin-top: 15px;">
                                                        <h4 style="margin-left: 5%">Librador:</h4>
                                                    </div>
                                                    <div class="col-md-8" style="margin-top: 15px;">
                                                        <asp:TextBox class="form-control" ID="txtLibrador" runat="server">
                                                                    
                                                        </asp:TextBox>


                                                    </div>
                                                </div>
                                                <div>

                                                    <div class="col-md-4" style="margin-top: 15px;">
                                                    </div>
                                                    <div class="col-md-8">
                                                        <div style="display: flex; justify-content: end;">

                                                            <button class="btn btn-sm btn-primary" style="margin-right: 25px; margin-top: 15px;" data-toggle="tooltip" data-placement="top" data-original-title="Guardar"
                                                                text="Guardar" validationgroup="AgregarEntregas" id="btnGuardarGuardarCheque" onclick="AgregarChequeATabla(event)" disabled="disabled">
                                                                Guardar Cheque</button>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <table class="table table-striped table-bordered table-hover " id="editableCheques">
                                                <thead>
                                                    <tr>


                                                        <th style="width: 7%">Fecha</th>
                                                        <th style="text-align: right; width: 10%">Importe</th>
                                                        <th style="text-align: left; width: 10%">Numero</th>
                                                        <th style="text-align: left; width: 10%">Banco</th>
                                                        <th style="text-align: right; width: 10%">Cuenta</th>
                                                        <th style="text-align: right; width: 10%">Cuit</th>
                                                        <th style="text-align: right; width: 10%">Librador</th>
                                                        <th style="width: 2%"></th>

                                                    </tr>
                                                </thead>
                                                <tbody id="IdTBODYCHEQUE">
                                                    <asp:PlaceHolder ID="phProductosyRecetas" runat="server"></asp:PlaceHolder>
                                                </tbody>
                                            </table>

                                            <asp:HiddenField ID="Montotxt" runat="server" />

                                        </div>
                                    </div>
                                </div>








                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree" class="collapsed" aria-expanded="false">Transferencias</a>
                                        </h4>
                                    </div>
                                    <div id="collapseThree" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="col-lg-6" style="margin-bottom: 15px">
                                                <div class="row">

                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Importe:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:TextBox class="form-control" ID="txtImporteTransferencias" onchange="ChangeTransferencia(this)" runat="server">
                                                                    
                                                            </asp:TextBox>


                                                        </div>
                                                    </div>

                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Cuentas Bancarias:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:DropDownList class="form-control" ID="DropListCuentasBancarias" onchange="FormatearNumero(this)" runat="server">
                                                            </asp:DropDownList>


                                                        </div>
                                                    </div>





                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <button class="btn btn-sm btn-primary" style="margin-right: 25px; margin-top: 15px;" data-toggle="tooltip" data-placement="top" data-original-title="Guardar Transferencia"
                                                                text="Guardar" validationgroup="AgregarEntregas" id="buttonTxtTransferencia" onclick="AgregarTransferencia(event)" disabled="disabled">
                                                                Guardar Transferencia</button>
                                                        </div>
                                                        <div class="col-md-8">
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour" class="collapsed" aria-expanded="false">Tarjetas</a>
                                        </h4>
                                    </div>
                                    <div id="collapseFour" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="col-lg-6" style="margin-bottom: 15px">
                                                <div class="row">





                                                    <div id="DivTarjeta">
                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Entidad:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:DropDownList ID="DropDownEntidadList" class="form-control" runat="server" onchange="BuscarTarjetasByEntidades(this)" />
                                                        </div>
                                                    </div>




                                                    <div id="DivTarjeta2">
                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Tarjeta:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:DropDownList ID="DropDownListTarjetaCredito" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Importe:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:TextBox class="form-control" ID="txtImporteTarjeta" onchange="ChangeTarjeta(this)" runat="server">
                                                                    
                                                            </asp:TextBox>


                                                        </div>
                                                    </div>
                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <button class="btn btn-sm btn-primary" style="margin-right: 25px; margin-top: 15px;" data-toggle="tooltip" data-placement="top" data-original-title="Guardar Transferencia"
                                                                text="Guardar" validationgroup="AgregarEntregas" id="buttonTxtTarjeta" onclick="AgregarTarjeta(event)" disabled="disabled">
                                                                Guardar Pago Tarjeta</button>
                                                        </div>
                                                        <div class="col-md-8">
                                                        </div>
                                                    </div>




                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive" class="collapsed" aria-expanded="false">Retenciones</a>
                                        </h4>
                                    </div>
                                    <div id="collapseFive" class="panel-collapse collapse" aria-expanded="false">
                                        <div class="panel-body">
                                            <div class="col-lg-6">
                                                <div class="row">

                                                    <div>

                                                        <div class="col-md-4">
                                                            <h4 style="margin-left: 5%">Tipo:</h4>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList class="form-control" ID="txtListRetenciones" onchange="onchangeRetenciones(this)" runat="server">

                                                                <asp:ListItem Value="SUSS">SUSS</asp:ListItem>
                                                                <asp:ListItem Value="GANANCIAS">GANANCIAS</asp:ListItem>
                                                                <asp:ListItem Value="IVA">IVA</asp:ListItem>
                                                                <asp:ListItem Value="INGRESOS BRUTOS">INGRESOS BRUTOS</asp:ListItem>

                                                            </asp:DropDownList>


                                                        </div>
                                                    </div>

                                                    <div id="divIngresosBrutos" style="display: none">

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Provincia:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:DropDownList class="form-control" ID="dllProvincias" runat="server">
                                                                <asp:ListItem Value="Buenos Aires">Buenos Aires</asp:ListItem>
                                                                <asp:ListItem Value="Catamarca">Catamarca</asp:ListItem>
                                                                <asp:ListItem Value="Chaco">Chaco</asp:ListItem>
                                                                <asp:ListItem Value="Chubut">Chubut</asp:ListItem>
                                                                <asp:ListItem Value="Córdoba">Córdoba</asp:ListItem>
                                                                <asp:ListItem Value="Corrientes">Corrientes</asp:ListItem>
                                                                <asp:ListItem Value="Entre Ríos">Entre Ríos</asp:ListItem>
                                                                <asp:ListItem Value="Formosa">Formosa</asp:ListItem>
                                                                <asp:ListItem Value="Jujuy">Jujuy</asp:ListItem>
                                                                <asp:ListItem Value="La Pampa">La Pampa</asp:ListItem>
                                                                <asp:ListItem Value="La Rioja">La Rioja</asp:ListItem>
                                                                <asp:ListItem Value="Mendoza">Mendoza</asp:ListItem>
                                                                <asp:ListItem Value="Misiones">Misiones</asp:ListItem>
                                                                <asp:ListItem Value="Neuquén">Neuquén</asp:ListItem>
                                                                <asp:ListItem Value="Río Negro">Río Negro</asp:ListItem>
                                                                <asp:ListItem Value="Salta">Salta</asp:ListItem>
                                                                <asp:ListItem Value="San Juan">San Juan</asp:ListItem>
                                                                <asp:ListItem Value="San Luis">San Luis</asp:ListItem>
                                                                <asp:ListItem Value="Santa Cruz">Santa Cruz</asp:ListItem>
                                                                <asp:ListItem Value="Santa Fe">Santa Fe</asp:ListItem>
                                                                <asp:ListItem Value="Santiago del Estero">Santiago del Estero</asp:ListItem>
                                                                <asp:ListItem Value="Tierra del Fuego">Tierra del Fuego</asp:ListItem>
                                                                <asp:ListItem Value="Tucumán">Tucumán</asp:ListItem>


                                                            </asp:DropDownList>




                                                        </div>
                                                    </div>



                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Importe:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:TextBox class="form-control" ID="txtImporte" onchange="ChangeRetenciones(this)" Style="text-align: right;" runat="server">
                                                                    
                                                            </asp:TextBox>


                                                        </div>
                                                    </div>


                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <h4 style="margin-left: 5%">Retencion Numero:</h4>
                                                        </div>
                                                        <div class="col-md-8" style="margin-top: 15px;">
                                                            <asp:TextBox class="form-control" ID="txtNumeroRetencion" Style="text-align: right;" runat="server">
                                                                    
                                                            </asp:TextBox>


                                                        </div>
                                                    </div>



                                                    <div>

                                                        <div class="col-md-4" style="margin-top: 15px;">
                                                            <button class="btn btn-sm btn-primary" style="margin-right: 25px;" data-toggle="tooltip" data-placement="top" data-original-title="Guardar"
                                                                text="Guardar" validationgroup="AgregarEntregas" id="btnGuardarRetenciones" onclick="AgregarRetenciones(event)" disabled="disabled">
                                                                Guardar</button>
                                                        </div>
                                                        <div class="col-md-8">
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="row">
                                        <table class="table table-striped table-bordered table-hover " id="editableGeneral">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: left; width: 70%">Tipo</th>
                                                    <th style="text-align: right; width: 20%">Importe</th>
                                                    <th style="text-align: center; width: 5%"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="EditableGeneralPH" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="row">
                                        <div>
                                            <div class="col-md-4" style="margin-top: 15px;">
                                                <h4 style="margin-left: 5%">Total a Ingresar:</h4>
                                            </div>
                                            <div class="col-md-8" style="margin-top: 15px;">
                                                <div style="display: flex; justify-content: end">

                                                    <h2 style="font-weight: bold;" id="totalAingresar"></h2>
                                                </div>
                                            </div>
                                        </div>

                                        <div>
                                            <div class="col-md-4" style="margin-top: 15px;">
                                                <h4 style="margin-left: 5%">Ingresado:</h4>
                                            </div>
                                            <div class="col-md-8" style="margin-top: 15px;">
                                                <div style="display: flex; justify-content: end">
                                                    <h2 style="font-weight: bold;" id="ingresadoTotal">$ 0.00</h2>

                                                </div>
                                            </div>
                                        </div>

                                        <div>
                                            <div class="col-md-4" style="margin-top: 15px;">
                                                <h4 style="margin-left: 5%">Restan:</h4>
                                            </div>
                                            <div class="col-md-8" style="margin-top: 15px;">
                                                <div style="display: flex; justify-content: end">
                                                    <h2 style="font-weight: bold;" id="RestanIngresar">$ 0.00</h2>
                                                </div>
                                            </div>
                                        </div>

                                        <div>

                                            <div class="col-md-4" style="margin-top: 15px;">
                                            </div>
                                            <div class="col-md-8">
                                                <div style="display: flex; justify-content: end">

                                                    <button class="btn btn-sm btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Generar Cobranza"
                                                        text="Generar Cobranza" validationgroup="AgregarEntregas" disabled="disabled" id="btnGenerarCobranza" onclick="ImputarCobranza(event)">
                                                        IMPUTAR COBRANZA</button>
                                                </div>
                                            </div>
                                        </div>




                                    </div>
                                </div>


                            </div>
                            <asp:HiddenField ID="contadorid" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="idClienteFinal" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="idNumeroCobro" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="ListTarjetas" runat="server"></asp:HiddenField>



                            </div>






                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>




    </div>



    <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="/../Scripts/plugins/staps/jquery.steps.min.js"></script>
    <script src="../../js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <script>
        $(document).ready(function () {

            if (document.getElementById("ContentPlaceHolder1_Montotxt").value != "") {
                restarMonto(revertirNumero(document.getElementById("ContentPlaceHolder1_Montotxt").value))
            }


         



            //$('#data_1 .input-group.date').datepicker({
            //    todayBtn: true,
            //    todayHighlight: true,
            //    keyboardNavigation: false,
            //    forceParse: false,
            //    calendarWeeks: true,
            //    autoclose: true,
            //    format: 'dd/mm/yyyy',
            //    beforeShow: function (input) {
            //        console.log("beforeShow");
            //    },
            //})
            establecerDiaHoy();
            //var todayBtn = $('.datepicker').find('.today');
            //todayBtn.addEventListener('click', establecerDiaHoy);
       
            preciofinal = document.getElementById("SumaFinal").value;
            document.getElementById("totalAingresar").innerText = "$ " + preciofinal;
            let cliente = document.getElementById("ContentPlaceHolder1_idClienteFinal").value.split("-")[1].trim();
            document.getElementById("ClienteSelec").innerText = cliente;
            document.getElementById("ReciboCobroNumero").innerText =  document.getElementById("ContentPlaceHolder1_idNumeroCobro").value

        });

        function onchangeRetenciones() {
            console.log("Prueba");
            if (document.getElementById("ContentPlaceHolder1_txtListRetenciones").value == "INGRESOS BRUTOS") {
                document.getElementById("divIngresosBrutos").style = ""

            } else {
                document.getElementById("divIngresosBrutos").style = "display:none"

            }
        }
        /*formatearNumero revertirNumero*/
        function IngresadoTotal() {


            // Obtener la tabla por su ID
            var tabla = document.getElementById("editableGeneral");

            // Obtener todas las filas de la tabla
            var filas = tabla.getElementsByTagName("tr");

            // Variable para almacenar la sumatoria de los importes
            var sumatoriaImportes = 0;

            // Recorrer cada fila a partir del índice 1 (se salta la fila de encabezados)
            for (var i = 1; i < filas.length; i++) {
                var celdas = filas[i].getElementsByTagName("td");
                var importe = celdas[1].innerText; // Obtener el valor del importe en la segunda celda
                importe = revertirNumero(importe); // Convertir el importe a un número decimal
                sumatoriaImportes += importe; // Sumar el importe a la sumatoria
            }


            preciofinal = document.getElementById("SumaFinal").value;
            document.getElementById("totalAingresar").innerText = "$ " + preciofinal;
            totalAIngresar = revertirNumero(preciofinal);
            totalingresado = sumatoriaImportes;
            document.getElementById("ingresadoTotal").innerText = "$ " + formatearNumero(sumatoriaImportes);
            restaingresar = formatearNumero(totalAIngresar - totalingresado);
            document.getElementById("RestanIngresar").innerText = "$ " + (restaingresar);
            if (restaingresar == 0) {
                document.getElementById("btnGenerarCobranza").removeAttribute("disabled");
            } else {
                document.getElementById("btnGenerarCobranza").disabled = "disabled";
            }
        }


        function BuscarTarjetasByEntidades(droplist) {
            if (droplist.value == "-1") {
                return
            } else {
                $.ajax({
                    method: "POST",
                    url: "Imputar.aspx/ddlOpciones_SelectedTarjeta",
                    data: '{idEntidad: "' + droplist.value + '"}',
                    contentType: "application/json",
                    dataType: "json",
                    dataType: "json",
                    async: false,
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        if (respuesta.d != "") {
                            listDrop = document.getElementById("ContentPlaceHolder1_DropDownListTarjetaCredito");
                            listDrop.innerHTML = "";
                            names = respuesta.d.split("%");
                            for (let i = 0; i < names.length; i++) {
                                if (names[i] != "") {

                                    finalnames = names[i].split("&");
                                    listDrop.innerHTML += '<option value="' + finalnames[0] + '">' + finalnames[1] + '</option>';
                                }
                            }
                        }
                    }
                });
            }
        }
        function ChangeTarjeta(input) {
            if (input.value != "") {
                document.getElementById("buttonTxtTarjeta").removeAttribute("disabled");
                FormatearNumero(input)
            } else {
                document.getElementById("buttonTxtTarjeta").disabled = "disabled";
            }
        }

        function ChangeRetenciones(input) {
            if (input.value != "") {
                document.getElementById("btnGuardarRetenciones").removeAttribute("disabled");
                FormatearNumero(input)
            } else {
                document.getElementById("btnGuardarRetenciones").disabled = "disabled";
            }
        }

        function ChangeEfectivo(input) {
            if (input.value != "") {
                document.getElementById("btnGuardar").removeAttribute("disabled");
                FormatearNumero(input)
            } else {
                document.getElementById("btnGuardar").disabled = "disabled";
            }
        }
        function ChangeCheque(input) {

            if (input.value != "") {
                document.getElementById("btnGuardarGuardarCheque").removeAttribute("disabled");
                FormatearNumero(input)
            } else {
                document.getElementById("btnGuardarGuardarCheque").disabled = "disabled";
            }
        }

        function ChangeTransferencia(input) {
            if (input.value != "") {
                document.getElementById("buttonTxtTransferencia").removeAttribute("disabled");
                FormatearNumero(input)
            } else {
                document.getElementById("buttonTxtTransferencia").disabled = "disabled";
            }
        }
        function restarMonto(monto) {
            var tabla = document.getElementById('editable');
            var filas = tabla.getElementsByTagName('tr');
            let montofinal = monto;
            let FINALTOTAL = 0;
            for (var i = 0; i < filas.length - 1; i++) {
                var celdas = filas[i].getElementsByTagName('td');

                if (celdas.length > 2) {
                    var saldo = formatearNumero(celdas[2].textContent);
                    var input = celdas[3].getElementsByTagName('input')[0];
                    var inputSaldo = revertirNumero(input.value);

                    if (montofinal > saldo) {
                        montofinal = montofinal - saldo;

                        if (montofinal >= 0) {
                            input.value = formatearNumero(inputSaldo);
                            FINALTOTAL += inputSaldo;

                        } else {
                            input.value = formatearNumero(montofinal);
                            FINALTOTAL += montofinal;
                        }
                    } else {
                        input.value = formatearNumero(montofinal);
                        FINALTOTAL += (montofinal);
                    }
                }
            }
            document.getElementById("SumaFinal").value = formatearNumero(FINALTOTAL);
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

        function establecerDiaHoy() {
            var fechaActual = new Date();
            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fechaActual.getFullYear() + '/' + (fechaActual.getMonth() + 1) + '/' + fechaActual.getDate())
            // Establecer la fecha actual como valor predeterminado del DatePicker 
            //$('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada);
            //$('#ContentPlaceHolder1_txtFechaHoy').datepicker('todayBtn', true);
            var partes = fechaFormateada.split('/');
            var dia = partes[2];
            var mes = partes[1];
            var anio = partes[0];

            if (dia < 10) {
                dia = '0' + dia;
            }

            if (mes < 10) {
                mes = '0' + mes;
            }

            fechafinal = anio + '-' + mes + '-' + dia;
            document.getElementById("ContentPlaceHolder1_txtFechaHoy").value = fechafinal;
          /*  document.getElementById("ContentPlaceHolder1_txtFechaHoy").datepicker('setDate', fechaFormateada);*/

        }

    
        function ActualizarPrecioFinal(textbox) {
            var totalImputar = 0;
            FormatearNumero(textbox);
            $('#editable tbody tr').each(function () {
                var imputar = $(this).find('input[type="text"]');
                if (imputar) {
                    if (imputar[0].id != "SumaFinal" && imputar[0].id != "textBoxIgnorado") {
                        totalImputar += parseFloat(revertirNumero(imputar[0].value));
                    }
                }
            });
            document.getElementById("SumaFinal").value = formatearNumero(totalImputar);
            document.getElementById("totalAingresar").innerText = "$ " + formatearNumero(totalImputar);
            IngresadoTotal();

        }
        function AgregarRetenciones(e) {
            e.preventDefault();
            let importe = document.getElementById("ContentPlaceHolder1_txtImporte").value;
            let retencionNum = document.getElementById("ContentPlaceHolder1_txtNumeroRetencion").value;
            let id = "ret_" + retencionNum
            let tipo = document.getElementById("ContentPlaceHolder1_txtListRetenciones").value;

            if (tipo == "INGRESOS BRUTOS") {
                let provincia = document.getElementById("ContentPlaceHolder1_dllProvincias").value;
                let final = "Retenciones " + tipo + " " + provincia + " N° " + retencionNum;
                AgregarATabla(final, importe, id);

            } else {
                let final = "Retenciones " + tipo + " N°" + retencionNum;
                AgregarATabla(final, importe, id);
            }
            IngresadoTotal();

        }

        function AgregarEfectivo(e) {
            e.preventDefault();
            let cont;
            if (document.getElementById("ContentPlaceHolder1_contadorid").value == "") {

                document.getElementById("ContentPlaceHolder1_contadorid").value = "1";
                cont = 1;
            } else {
                cont = Number(document.getElementById("ContentPlaceHolder1_contadorid").value)
                cont++;
                document.getElementById("ContentPlaceHolder1_contadorid").value = cont.toString();
            }
            let importe = document.getElementById("ContentPlaceHolder1_txtImporteEfectivo").value;
            document.getElementById("ContentPlaceHolder1_txtImporteEfectivo").value = "";
            document.getElementById("btnGuardar").disabled = true;



            /*      document.getElementsByName("Efectivo_TableFinal")[0].innerText           */
            let id = "Efectivo_TableFinal";
            let documento = document.getElementsByName(id);
            let documentofinal;
            if (documento.length > 0) {
                documentofinal = documento[0];
            }
            if (documento.length > 0) {

                let monto = revertirNumero(documentofinal.innerText);
                let importefinal = revertirNumero(importe);
                documentofinal.innerText = formatearNumero((importefinal + monto));
                IngresadoTotal();
            } else {
                AgregarATabla("Efectivo", importe, id);
                IngresadoTotal();
            }

        }

        function AgregarTransferencia(e) {
            e.preventDefault();
            /*let cont;*/
            //if (document.getElementById("ContentPlaceHolder1_contadorid").value == "") {

            //    document.getElementById("ContentPlaceHolder1_contadorid").value = "1";
            //    cont = 1;
            //} else {
            //    cont = Number(document.getElementById("ContentPlaceHolder1_contadorid").value)
            //    cont++;
            //    document.getElementById("ContentPlaceHolder1_contadorid").value = cont.toString();
            //}
            let importe = document.getElementById("ContentPlaceHolder1_txtImporteTransferencias").value;
            document.getElementById("ContentPlaceHolder1_txtImporteTransferencias").value = "";
            document.getElementById("buttonTxtTransferencia").disabled = true;
            var dropdown = document.getElementById("ContentPlaceHolder1_DropListCuentasBancarias");
            var opcionSeleccionada = dropdown.options[dropdown.selectedIndex];



            let id = "Transferencia_TableFinal";
            let documento = document.getElementsByName(id);
            let documentofinal;
            if (documento.length > 0) {
                documentofinal = documento[0];
            }
            if (documento.length > 0) {

                let monto = revertirNumero(documentofinal.innerText);
                let importefinal = revertirNumero(importe);
                documentofinal.innerText = formatearNumero((importefinal + monto));
                IngresadoTotal();
            } else {
                AgregarATabla("Transferencia", importe, id);
                IngresadoTotal();
            }


        }
        function AgregarTarjeta(e) {
            e.preventDefault();
            /*            ContentPlaceHolder1_ListTarjetas   */
            let entidad = document.getElementById("ContentPlaceHolder1_DropDownEntidadList").value;
            let tarjeta = document.getElementById("ContentPlaceHolder1_DropDownListTarjetaCredito").value;
            let importe = document.getElementById("ContentPlaceHolder1_txtImporteTarjeta").value;
            document.getElementById("ContentPlaceHolder1_txtImporteTarjeta").value = "";
            document.getElementById("buttonTxtTarjeta").disabled = true;
            var dropdown = document.getElementById("ContentPlaceHolder1_DropListCuentasBancarias");
            var opcionSeleccionada = dropdown.options[dropdown.selectedIndex];


            let id = "Tarjeta_TableFinal";
            let documento = document.getElementsByName(id);
            let documentofinal;
            if (documento.length > 0) {
                documentofinal = documento[0];
            }
            if (documento.length > 0) {

                let monto = revertirNumero(documentofinal.innerText);
                let importefinal = revertirNumero(importe);
                documentofinal.innerText = formatearNumero((importefinal + monto));
                IngresadoTotal();
            } else {
                AgregarATabla("Tarjeta", importe, id);
                IngresadoTotal();
            }

            let listtarjetas = document.getElementById("ContentPlaceHolder1_ListTarjetas").value;
            if (listtarjetas == "") {
                document.getElementById("ContentPlaceHolder1_ListTarjetas").value += entidad + "/" + tarjeta + "/" + importe;

            } else {
                document.getElementById("ContentPlaceHolder1_ListTarjetas").value += "%" + entidad + "/" + tarjeta + "/" + importe;
            }
        }



        function AgregarATabla(tipo, importe, id) {
            let importefinal = "";
            switch (tipo) {
                case "Transferencia": importefinal = "<td name=\"" + id + "\" style=\" text-align: right\"> " + importe + "</td>";
                    break
                case "Efectivo": importefinal = "<td name=\"" + id + "\" style=\" text-align: right\"> " + importe + "</td>";
                    break
                case "Cheque": importefinal = "<td name=\"" + id + "\" style=\" text-align: right\"> " + importe + "</td>";
                    break
                case "Tarjeta": importefinal = "<td name=\"" + id + "\" style=\" text-align: right\"> " + importe + "</td>";
                    break
                default: importefinal = "<td name=\"" + id + "\" style=\" text-align: right\"> " + importe + "</td>";
                    break
            }
            let styleCorrect = "";
            let btnrec = "";
            let chequeinfo = "Cheque"
            let btneliminar = "";
            if (tipo == "Cheque") {
                btneliminar = "<td style=\" text-align: center\">" +
                    " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs\" onclick=\"javascript: return borrarProd2('" + id + "','" + chequeinfo + "');\" >" +
                    "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> " +
                    btnrec + "</td > ";
            } else {

                btneliminar = "<td style=\" text-align: center\">" +
                    " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs\" onclick=\"javascript: return borrarProd2('" + id + "');\" >" +
                    "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> " +
                    btnrec + "</td > ";
            }
            let tipofinal = "<td> " + tipo + "</td>";
            let appendfinal = "<tr id=" + id + ">" +
                tipofinal +
                importefinal +
                btneliminar
            "</tr>"
            $('#editableGeneral').append(appendfinal);
        }

        function AgregarChequeATabla(e) {
            e.preventDefault()

            var opcionSeleccionada = document.getElementById("ContentPlaceHolder1_txtBancoList").value;
            let fecha = document.getElementById("ContentPlaceHolder1_txtFechaHoy").value;
            let importe = document.getElementById("ContentPlaceHolder1_txtImporteCheque").value;
            let numerocheque = document.getElementById("ContentPlaceHolder1_txtNumeroCheque").value;
            let bancodll = opcionSeleccionada;
            let cuenta = document.getElementById("ContentPlaceHolder1_txtCuenta").value;
            let txtcuitCheque = document.getElementById("ContentPlaceHolder1_txtCuitCheque").value;
            let txtLibrador = document.getElementById("ContentPlaceHolder1_txtLibrador").value;
            if (fecha == "" || importe == "" || numerocheque == "" || bancodll == "" || cuenta == "" || txtcuitCheque == "" || txtLibrador == "") {
                toastr.info("Debes rellenar todos los formularios para agregar un Cheque");
                return;
            } else {


                let fechafinal = "<td> " + fecha + "</td>";
                let importefinal = "<td style=\" text-align: right\"> " + importe + "</td>";
                let numerochequefinal = "<td style=\" text-align: right\">" + numerocheque + "</td>";
                let bancodllfinal = "<td style=\" text-align: right\">" + bancodll + "</td>";
                let cuentafinal = "<td style=\" text-align: right\">" + cuenta + "</td>";
                let txtcuitChequefinal = "<td style=\" text-align: right\">" + txtcuitCheque + "</td>";
                let txtLibradorfinal = "<td style=\" text-align: right\">" + txtLibrador + "</td>";
                let styleCorrect = "";
                let btnrec = "";
                let btneliminar = "<td style=\" text-align: center\">" +
                    " <a style=\"padding: 0% 5% 2% 5.5%;background-color: transparent; " + styleCorrect + "\" class=\"btn  btn-xs\" onclick=\"javascript: return borrarProd('" + numerocheque + "_" + cuenta + "_" + txtcuitCheque + "','" + importe + "');\" >" +
                    "<i class=\"fa fa-trash - o\" style=\"color: black\"></i> </a> " +
                    btnrec + "</td > ";

                let appendfinal = "<tr id=" + numerocheque + "_" + cuenta + "_" + txtcuitCheque + ">" +
                    fechafinal +
                    importefinal +
                    numerochequefinal +
                    bancodllfinal +
                    cuentafinal +
                    txtcuitChequefinal +
                    txtLibradorfinal +
                    btneliminar +
                    "</tr>"
                $('#editableCheques').append(appendfinal);
                document.getElementById("ContentPlaceHolder1_txtImporteCheque").value = "";
                document.getElementById("ContentPlaceHolder1_txtNumeroCheque").value = "";
                document.getElementById("ContentPlaceHolder1_txtNumeroCheque").value = "";
                document.getElementById("ContentPlaceHolder1_txtCuenta").value = "";
                document.getElementById("ContentPlaceHolder1_txtCuitCheque").value = "";
                document.getElementById("ContentPlaceHolder1_txtLibrador").value = "";
                let id = "Cheque_TableFinal"
                let documento = document.getElementsByName(id);
                let documentofinal;
                if (documento.length > 0) {
                    documentofinal = documento[0];
                }
                if (documento.length > 0) {

                    let monto = revertirNumero(documentofinal.innerText);
                    let importefinal = revertirNumero(importe);
                    documentofinal.innerText = formatearNumero((importefinal + monto));
                    IngresadoTotal();
                } else {
                    AgregarATabla("Cheque", importe, id);
                    IngresadoTotal();
                }



            }
        }

        function borrarProd(idprod, importe) {
            event.preventDefault();
            $('#' + idprod).remove();
            let documento = document.getElementsByName("Cheque_TableFinal");
            let documentofinal;
            if (documento.length > 0) {
                documentofinal = documento[0];
            }
            if (documento.length > 0) {

                let monto = revertirNumero(documentofinal.innerText);
                let importefinal = revertirNumero(importe);
                let total = (monto - importefinal);
                if (total == 0) {
                    $('#Cheque_TableFinal').remove();
                } else {
                    documentofinal.innerText = formatearNumero((monto - importefinal));
                }
            }
            IngresadoTotal();
        }
        function borrarProd2(idprod, cheque = "") {
            event.preventDefault();
            if (cheque != "") {
                document.getElementById("IdTBODYCHEQUE").innerHTML = "";
            }
            $('#' + idprod).remove();

            IngresadoTotal();
        }


        function ImputarCobranza(e) {
            e.preventDefault();


            var tabla = document.getElementById("editable");

            // Obtener todas las filas de la tabla
            var filas = tabla.getElementsByTagName("tr");



            // Recorrer cada fila a partir del índice 1 (se salta la fila de encabezados)
            var listFacImputar = "";
            var listTarjeta = "";
            var listCheques = "";
            var totalefectivo = "";
            for (var i = 1; i < filas.length-1; i++) {
                var celdas = filas[i].getElementsByTagName("td");
                var id = celdas[0].innerText;
                var importeRestar = celdas[3].getElementsByTagName("input")[0].value; // Obtener el valor del importe en la segunda celda
                var importeReal = celdas[2].innerText;
                var importefinal = revertirNumero(importeReal) - revertirNumero(importeRestar);
                if (listFacImputar == "") {
                    listFacImputar = id + "-" + importefinal;

                } else {
                    listFacImputar +="%" + id + "-" + importefinal;

                }

            }
            let ingresadototal = revertirNumero(document.getElementById("ingresadoTotal").innerText.split(" ")[1].trim());

            // TABLA ABAJO
            var tabla2 = document.getElementById("editableGeneral");

            // Obtener todas las filas de la tabla
            var filas2 = tabla2.getElementsByTagName("tr");
            listPagos = "";
            for (var i = 1; i < filas2.length; i++) {
                var celdas = filas2[i].getElementsByTagName("td");
                var text = celdas[0].innerText;
                if (listPagos == "") {
                    listPagos += text;
                } else {
                    listPagos += "%" + text;
                }

            }

            if (listPagos.includes("Cheque")) {

                var tablaCheques = document.getElementById("editableCheques");

                // Obtener todas las filas de la tabla
                var filasCheques = tablaCheques.getElementsByTagName("tr");

                for (var i = 1; i < filasCheques.length; i++) {
                    var celdas = filasCheques[i].getElementsByTagName("td");
                    var importe = celdas[1].innerText;
                    var numero = celdas[2].innerText;
                    var idBanco = celdas[3].innerText;
                    var Cuenta = celdas[4].innerText;
                    var Cuit = celdas[5].innerText;
                    var Librador = celdas[6].innerText;
                    var fecha = celdas[0].innerText.replaceAll("-","$");
                    if (listCheques == "") {
                        listCheques += importe + "/" + numero + "/" + idBanco.split("-")[0].trim() + "/" + Cuenta + "/" + Cuit + "/" + Librador + "/" + fecha;
                    } else {
                        listCheques += "%" + importe + "/" + numero + "/" + idBanco + "/" + Cuenta + "/" + Cuit + "/" + Librador + "/" + fecha;
                    }


                }
            }
            if (listPagos.includes("Tarjeta")) {

                listTarjeta = document.getElementById("ContentPlaceHolder1_ListTarjetas").value

            }
            if (listPagos.includes("Efectivo")) {
                totalefectivo = document.getElementsByName("Efectivo_TableFinal")[0].innerText;
            }



            $.ajax({
                method: "POST",
                url: "Imputar.aspx/ImputarFacturas",
                data: '{listFacturas: "' + listFacImputar
                    + '" , ImporteCobro: "' + ingresadototal
                    + '" , idCliente: "' + document.getElementById("ContentPlaceHolder1_idClienteFinal").value.split("-")[0].trim()
                    + '" , listCheques: "' + listCheques
                    + '" , listTarjetas: "' + listTarjeta
                    + '" , totalefectivo: "' + totalefectivo
                    + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                   /* btn.disabled = false;*/
                },
                success: (response) => {
                    if (response.d == "") {
                        window.location.href = "Cobros.aspx?m=5";
                    } else {
                        toastr.info(response.d);
                    }
                }
            });


               
        }

    </script>

</asp:Content>
