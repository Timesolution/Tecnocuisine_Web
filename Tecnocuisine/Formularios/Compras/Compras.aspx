<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.Compras" %>

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
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">

                    <div class="ibox-content">
                        <div class="row" style="padding-left: 15px;">




                            <div class="row">
                                <%-- Columna 1--%>
                                <div class="col-lg-6">
  <%--                          <div class="row">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Agregar Entrega</label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtListEntrega" Style="width: 100%" onkeypress="AgregarEntregaLista(event)" list="ContentPlaceHolder1_ListaEntregas" class="form-control" runat="server" />

                                                <span class="input-group-btn" data-toggle="tooltip" data-placement="bottom" data-original-title="Agregar Entrega A Facturacion" style="padding-right: 3px; padding-left: 1px;">
                                                    <a id="LinkButtonMarcas2" class="btn btn-primary dim" data-backdrop="static" data-keyboard="false" onclick="AgregarEntregaLista(event)">
                                                        <i style="color: white;" class="fa fa-plus"></i>
                                                    </a>
                                                </span>

                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="row">

                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Fecha</label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group" id="data_1">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox class="form-control" runat="server" ID="txtFechaHoy" Style="margin-left: 0px; width: 100%;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Tipo Documento</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox Style="margin-left: 0px; width: 100%;" class="form-control" list="ContentPlaceHolder1_ListaTipoDoc" runat="server" ID="txtTipoDocumento"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 3%">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Importe Total</label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <span class="input-group-addon">$</span><asp:TextBox ID="txtImporteTotal" onchange="FormatearNumeroImpTotal(this)" class="form-control num required" runat="server" Style="text-align: right; width: 100%;" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 3%">
                                        <div class="col-md-4">
                                            <label style="margin-left: 5%">Establecer Impuestos</label>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <input type="checkbox" id="InputImpuestos" class="form-control num required" style="text-align: right; margin-top: 0px; top: -5px;" onclick="ChangeImpuestos()" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--Columna 2--%>
                                <div class="col-lg-6">
                                    <div>
                                        <div class="row" id="IdRowEntregasSeleccionadas" style="display: none">
                                            <div class="col-md-4">
                                                <label>Entregas Seleccionadas:</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div id="EntregasRellenar"></div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label>Proovedor</label>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox Style="width: 80%;" class="form-control" list="ContentPlaceHolder1_ListaProveedores" runat="server" ID="txtProveedor"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 3%">
                                            <div class="col-md-4">
                                                <label>Numero Documento</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <input type="text" class="form-control" placeholder="XXXX" maxlength="4" id="input1" onblur="formatInput(this)" />
                                                            </div>
                                                            <div class="col-md-7">
                                                                <input type="text" class="form-control" placeholder="XXXXXXXX" maxlength="8" id="input2" onblur="formatInput(this)" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-md-4">
                                                <label>Fecha Vencimiento</label>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group" id="data_2">
                                                    <div class="input-group date">
                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        <asp:TextBox class="form-control" runat="server" ID="txtFechaVencimiento" Style="margin-left: 0px; width: 80%;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-lg-12" id="DivImpuestos" style="display: none">
                                    <%--COLUMNA 1 IMPUESTOS--%>
                                    <div style="display: flex; flex-direction: column;">


                                        <div id="MainContent_divNeto2" class="form-group">
                                            <label for="name" class="col-md-3">Neto 2.5</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="txtNeto2" type="text" value="0" onchange="neto2(this)" id="txtNeto2" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="txtNeto2Value" type="text" value="0" id="txtNeto2Value" class="form-control" disabled="" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto2" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator16" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtIvaNeto2" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator10" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>

                                        </div>
                                        <div id="MainContent_divNeto5" class="form-group">
                                            <label for="name" class="col-md-3">Neto 5</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto5" type="text" value="0" onchange="Neto5(this)" id="divNeto5" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto5Value" type="text" value="0" id="divNeto5Value" class="form-control" disabled="" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto5" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator15" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtIvaNeto5" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator9" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divNeto10" class="form-group">
                                            <label for="name" class="col-md-3">Neto 10.5</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto10" type="text" value="0" onchange="Neto10(this)" id="divNeto10" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto10Value" type="text" value="0" id="divNeto10Value" class="form-control" disabled="" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto105" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator35" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto105" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator11" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>

                                        </div>
                                        <div id="MainContent_divNeto21" class="form-group">
                                            <label for="name" class="col-md-3">Neto 21</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto21" type="text" value="0" onchange="Neto21(this)" id="divNeto21" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto21Value" type="text" value="0" id="divNeto21Value" class="form-control" disabled="" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto21" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator36" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto21" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator12" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>

                                        </div>
                                        <div id="MainContent_divNeto27" class="form-group">
                                            <label for="name" class="col-md-3">Neto 27</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto27" type="text" value="0" onchange="Neto27(this)" id="divNeto27" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divNeto27Value" type="text" value="0" id="divNeto27Value" class="form-control" disabled="" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto27" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator37" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtNeto27" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator13" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divIngBru" class="form-group">
                                            <label for="name" class="col-md-3">Ingresos Brutos</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="txtIB" type="text" value="0" onchange="SumarTotal(this)" onchange="" id="txtIB" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtPIB" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator38" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtPIB" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator14" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divPercepcionIVA" class="form-group">
                                            <label for="name" class="col-md-3">Percepción IVA</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="PercepcionIVA" type="text" onchange="SumarTotal(this)" value="0" id="PercepcionIVA" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtPIva" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator39" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtPIva" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator15" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divRetIIBB" class="form-group">
                                            <label for="name" class="col-md-3">Retencion de IIBB</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="RetIIBB" type="text" value="0" onchange="SumarTotal(this)" id="RetIIBB" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionIIBB" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator9" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionIIBB" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator2" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divRetIVA" class="form-group">
                                            <label for="name" class="col-md-3">Retencion de IVA</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="RetIVA" type="text" value="0" onchange="SumarTotal(this)" id="RetIVA" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionIVA" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator10" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionIVA" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator4" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divRetGan" class="form-group">
                                            <label for="name" class="col-md-3">Retencion de ganancias</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="RetGan" type="text" value="0" onchange="SumarTotal(this)" id="RetGan" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionGanancias" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator11" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionGanancias" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator5" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divRetSUSS" class="form-group">
                                            <label for="name" class="col-md-3">Retencion SUSS</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="RetSUSS" type="text" onchange="SumarTotal(this)" value="0" id="RetSUSS" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionSuss" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator17" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtRetencionSuss" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator18" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div id="MainContent_divITC" class="form-group">
                                            <label for="name" class="col-md-3">ITC</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="divITC" type="text" value="0" onchange="SumarTotal(this)" id="divITC" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtITC" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator12" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtITC" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator6" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="name" class="col-md-3">Otros</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="Otros" type="text" value="0" id="Otros" onchange="SumarTotal(this)" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtOtros" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator41" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtOtros" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator17" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <%--OTROS--%>
                                        <div class="form-group">

                                            <label for="name" class="col-md-3">Neto No Grabado</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="NetoNOGrabado" onchange="SumarTotal(this)" type="text" value="0" id="NetoNOGrabado" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <span data-val-controltovalidate="MainContent_txtNetoNoGrabado" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator34" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                                <span data-val-controltovalidate="MainContent_txtNetoNoGrabado" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator1" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>

                                        </div>
                                        <%--Neto No Grabado--%>

                                        <div id="MainContent_divImpInt" class="form-group">
                                            <label for="name" class="col-md-3">Impuestos Internos</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="ImpInt" type="text" value="0" id="ImpInt" onchange="SumarTotal(this)" class="form-control" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtImpuestosInternos" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator40" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtImpuestosInternos" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator16" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <%--Impuestos Internos--%>
                                        <div class="form-group">
                                            <label for="name" class="col-md-3">Total</label>
                                            <div class="col-md-3">
                                                <div class="input-group">
                                                    <span class="input-group-addon">$</span>
                                                    <input name="TotalImp" onchange="SumarTotal(this)" type="text" value="0" id="TotalImp" class="form-control" disabled="" style="text-align: right;">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtOtros" data-val-focusonerror="t" data-val-errormessage="El campo es obligatorio" data-val-validationgroup="ArticuloGroup" id="MainContent_RequiredFieldValidator4" data-val="true" data-val-evaluationfunction="RequiredFieldValidatorEvaluateIsValid" data-val-initialvalue="" style="color: Red; font-weight: bold; visibility: hidden;">El campo es obligatorio</span>
                                            </div>
                                            <div class="col-md-4">
                                                <span data-val-controltovalidate="MainContent_txtOtros" data-val-focusonerror="t" data-val-errormessage="Formato incorrecto" data-val-validationgroup="ArticuloGroup" id="MainContent_RegularExpressionValidator3" data-val="true" data-val-evaluationfunction="RegularExpressionValidatorEvaluateIsValid" data-val-validationexpression="^(\d|-)?(\d|,)*\.?\d\,?\d*$" style="color: Red; font-weight: bold; visibility: hidden;">Formato incorrecto</span>
                                            </div>
                                        </div>
                                        <%--TOTAL--%>
                                        <div class="form-group">
                                            <label for="name" class="col-md-3">Observaciones</label>
                                            <div class="col-md-6">
                                                <textarea name="Observaciones" rows="6" cols="20" id="txtObservaciones" class="form-control"></textarea>
                                            </div>
                                        </div>
                                        <%--OBSERVACIONES--%>
                                    </div>
                                </div>
                                <button class="btn btn-sm btn-primary pull-right m-t-n-xs" style="margin-right: 55px; margin-bottom: 5px; margin-top: 5px;" data-toggle="tooltip" data-placement="top" data-original-title="Generar Facturacion"
                                    text="Confirmar Venta" validationgroup="AgregarEntregas" id="btnGuardar" onclick="GenerarFacturacion(event)">
                                    Generar Facturacion</button>
                            </div>
                            <asp:HiddenField runat="server" ID="NumeroEntregas" />
                            <datalist id="ListaTipoDoc" runat="server"></datalist>
                            <datalist id="ListaProveedores" runat="server"></datalist>
                            <datalist id="ListaEntregas" runat="server">
                            </datalist>





                        </div>
                    </div>
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
            let ValueNumeros = document.getElementById("ContentPlaceHolder1_NumeroEntregas").value
            if (ValueNumeros != "") {
                document.getElementById("IdRowEntregasSeleccionadas").removeAttribute("style");
                document.getElementById("EntregasRellenar").innerHTML = '<h4 id="CodigoEntregas"> ' + ValueNumeros + '</h4>'
            } else {
                document.getElementById("EntregasRellenar").innerHTML = '<h4 id="CodigoEntregas"></h4>'
            }
            $('#data_1 .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'mm/dd/yyyy'
            });
            $('#data_2 .input-group.date').datepicker({
                todayBtn: "linked",
                keyboardNavigation: false,
                forceParse: false,
                calendarWeeks: true,
                autoclose: true,
                format: 'mm/dd/yyyy'
            });
            establecerDiaHoy();
            establecer30Dias();
        });
        function ChangeImpuestos() {
            const miCheckbox = document.getElementById("InputImpuestos");
            if (miCheckbox.checked) {
                document.getElementById("DivImpuestos").removeAttribute("style")
                miCheckbox.checked = true;
            } else {
                document.getElementById("DivImpuestos").style.display = "none";
            }
            console.log()
        }
        function establecerDiaHoy() {
            var fechaActual = new Date();

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fechaActual.getMonth() + 1) + '/' + fechaActual.getDate() + '/' + fechaActual.getFullYear();

            // Establecer la fecha actual como valor predeterminado del DatePicker
            $('#ContentPlaceHolder1_txtFechaHoy').datepicker('setDate', fechaFormateada);
        }


        function formatInput(input) {
            // Obtener el valor del input
            let value = input.value;

            // Verificar si el valor es numérico
            if (!isNaN(value)) {
                // Agregar ceros por delante hasta alcanzar el número máximo de caracteres
                let maxLength = parseInt(input.getAttribute('maxlength'));
                if (value.length < maxLength) {
                    value = value.padStart(maxLength, '0');
                    // Establecer el nuevo valor del input
                    input.value = value;
                }
            }
        }

        function establecer30Dias() {
            var fechaActual = new Date();

            // Sumar 30 días a la fecha actual
            var fecha30DiasMas = new Date(fechaActual.getTime() + (30 * 86400000));

            // Convertir la fecha en un formato legible para el DatePicker   
            var fechaFormateada = (fecha30DiasMas.getMonth() + 1) + '/' + fecha30DiasMas.getDate() + '/' + fecha30DiasMas.getFullYear();

            // Establecer la fecha actual + 30 días como valor predeterminado del DatePicker
            $('#ContentPlaceHolder1_txtFechaVencimiento').datepicker('setDate', fechaFormateada);
        }

        function GenerarFacturacion(event) {
            event.preventDefault();
            check = document.getElementById("InputImpuestos");
            if (check.checked == false) {
                $.ajax({
                    method: "POST",
                    url: "Compras.aspx/GenerarFactura",
                    data: '{CodigoEntregas: "' + document.getElementById("CodigoEntregas").innerText
                        + '" , FechaActual: "' + document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
                        + '" , TipoDocumento: "' + document.getElementById("ContentPlaceHolder1_txtTipoDocumento").value
                        + '" , ImporteTotal: "' + document.getElementById("ContentPlaceHolder1_txtImporteTotal").value
                        + '" , Proveedor: "' + document.getElementById("ContentPlaceHolder1_txtProveedor").value
                        + '" , NumeroFactura: "' + document.getElementById("input1").value + "-" + document.getElementById("input2").value
                        + '" , FechaVencimiento: "' + document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value.replace(".", ",")
                        + '"}',
                    contentType: "application/json",
                    dataType: "json",
                    error: (error) => {
                        toastr.error('Error al generar factura')
                        let btn = document.getElementById("btnGuardar")
                        btn.disabled = false;
                    },
                    success: (respuesta) => {
                        switch (respuesta.d) {
                            case 99: toastr.error("Ya existe ese Numero de Factura vinculado a ese Proveedor")
                                break;
                            case 2: toastr.error("No se agrego correctamente el monto a la cuenta corriente")
                                break;
                            case 3: toastr.error("No se agrego el Detalle de la Factura")
                                break;
                            case 4: toastr.error("Hubo un error al intentar Generar la Factura.")
                                break;
                            case 1: window.location.href = "Compras.aspx?m=1"
                                break;
                            default: window.location.href = "Compras.aspx?m=1"

                        }
                    },
                });
            } else {
                let importTotal = revertirNumero(document.getElementById("ContentPlaceHolder1_txtImporteTotal").value)
                let TotalImpuesto = revertirNumero(document.getElementById("TotalImp").value)
                if (importTotal != TotalImpuesto) {
                    return toastr.error("El Importe total y el Total de los impuestos tiene que ser igual")
                }
                $.ajax({
                    method: "POST",
                    url: "Compras.aspx/GenerarFacturaImpuestos",
                    data: '{CodigoEntregas: "' + document.getElementById("CodigoEntregas").innerText
                        + '" , FechaActual: "' + document.getElementById("ContentPlaceHolder1_txtFechaHoy").value
                        + '" , TipoDocumento: "' + document.getElementById("ContentPlaceHolder1_txtTipoDocumento").value
                        + '" , ImporteTotal: "' + document.getElementById("ContentPlaceHolder1_txtImporteTotal").value
                        + '" , Proveedor: "' + document.getElementById("ContentPlaceHolder1_txtProveedor").value
                        + '" , NumeroFactura: "' + document.getElementById("input1").value + "-" + document.getElementById("input2").value
                        + '" , FechaVencimiento: "' + document.getElementById("ContentPlaceHolder1_txtFechaVencimiento").value
                        + '" , txtNeto2Value: "' + (document.getElementById("txtNeto2Value").value * 0.02).toString()
                        + '" , divNeto10Value: "' + (document.getElementById("divNeto10Value").value * 0.105).toString()
                        + '" , divNeto27Value: "' + (document.getElementById("divNeto27Value").value * 0.27).toString()
                        + '" , divNeto5Value: "' + (document.getElementById("divNeto5Value").value * 0.05).toString()
                        + '" , txtIB: "' + document.getElementById("txtIB").value
                        + '" , PercepcionIVA: "' + document.getElementById("PercepcionIVA").value
                        + '" , divNeto21Value: "' + (document.getElementById("divNeto21Value").value * 0.21).toString()
                        + '" , RetIIBB: "' + document.getElementById("RetIIBB").value
                        + '" , RetIVA: "' + document.getElementById("RetIVA").value
                        + '" , RetGan: "' + document.getElementById("RetGan").value
                        + '" , RetSUSS: "' + document.getElementById("RetSUSS").value
                        + '" , divITC: "' + document.getElementById("divITC").value
                        + '" , Otros: "' + document.getElementById("Otros").value
                        + '" , NetoNOGrabado: "' + document.getElementById("NetoNOGrabado").value
                        + '" , ImpInt: "' + document.getElementById("ImpInt").value
                        + '" , TotalImp: "' + document.getElementById("TotalImp").value
                        + '" , Observaciones: "' + document.getElementById("txtObservaciones").value
                        + '"}',
                    contentType: "application/json",
                    dataType: "json",
                    error: (error) => {
                        toastr.error('Error al generar factura')
                        let btn = document.getElementById("btnGuardar")
                        btn.disabled = false;
                    },
                    success: (respuesta) => {

                        switch (respuesta.d) {
                            case 99: toastr.error("Ya existe ese Numero de Factura vinculado a ese Proveedor")
                                break;
                            case 2: toastr.error("No se agrego correctamente el monto a la cuenta corriente")
                                break;
                            case 3: toastr.error("No se agrego el Detalle de la Factura")
                                break;
                            case 4: toastr.error("Hubo un error al intentar Generar la Factura.")
                                break;
                            case 1: window.location.href = "Compras.aspx?m=1"
                                break;
                            default: window.location.href = "Compras.aspx?m=1"

                        }
                        
                    },
                });
            }
        }

        // IMPUESTOS

        function neto2(input) {
            number = Number(input.value);
            newnumber = number * 0.02;
            input.value = formatearNumero(number);
            let numerofinal = formatearNumero(newnumber);
            document.getElementById("txtNeto2Value").value = numerofinal;
            SumarTotal()
        }

        function Neto10(input) {
            number = Number(input.value);
            newnumber = number * 0.105;
            input.value = formatearNumero(number);
            document.getElementById("divNeto10Value").value = formatearNumero(newnumber);
            SumarTotalFinal()
        }
        function Neto27(input) {
            number = Number(input.value);
            newnumber = number * 0.27;
            input.value = formatearNumero(number);
            document.getElementById("divNeto27Value").value = formatearNumero(newnumber);
            SumarTotalFinal()
        }
        function Neto5(input) {
            number = Number(input.value);
            newnumber = number * 0.05;
            input.value = formatearNumero(number);
            document.getElementById("divNeto5Value").value = formatearNumero(newnumber);
            SumarTotalFinal()
        }
        function Neto21(input) {
            number = Number(input.value);
            newnumber = number * 0.21;
            input.value = formatearNumero(number);
            document.getElementById("divNeto21Value").value = formatearNumero(newnumber);
            SumarTotalFinal()
        }
        function SumarTotal(input) {
            number = Number(input.value);
            newnumber = formatearNumero(number);
            input.value = newnumber
            SumarTotalFinal()
        }

        function SumarTotalFinal() {
            sum = 0;
            sum += revertirNumero((document.getElementById("txtNeto2Value").value))
            sum += revertirNumero((document.getElementById("txtNeto2").value))

            sum += revertirNumero((document.getElementById("divNeto10Value").value))
            sum += revertirNumero((document.getElementById("divNeto10").value))

            sum += revertirNumero((document.getElementById("divNeto27Value").value))
            sum += revertirNumero((document.getElementById("divNeto27").value))

            sum += revertirNumero((document.getElementById("divNeto5Value").value))
            sum += revertirNumero((document.getElementById("divNeto5").value))

            sum += revertirNumero((document.getElementById("divNeto21Value").value))
            sum += revertirNumero((document.getElementById("divNeto21").value))

            sum += revertirNumero((document.getElementById("txtIB").value))
            sum += revertirNumero((document.getElementById("PercepcionIVA").value))
            sum += revertirNumero((document.getElementById("RetIIBB").value))
            sum += revertirNumero((document.getElementById("RetIVA").value))
            sum += revertirNumero((document.getElementById("RetGan").value))
            sum += revertirNumero((document.getElementById("RetSUSS").value))
            sum += revertirNumero((document.getElementById("divITC").value))
            sum += revertirNumero((document.getElementById("Otros").value))
            sum += revertirNumero((document.getElementById("NetoNOGrabado").value))
            sum += revertirNumero((document.getElementById("ImpInt").value))
            document.getElementById("TotalImp").value = formatearNumero(sum);

        }
        function AgregarEntregaLista(event) {
            event.preventDefault();
            divText = document.getElementById("CodigoEntregas")
            Data = document.getElementById("ContentPlaceHolder1_txtListEntrega").value
            if (divText.innerHTML == "") {
                divText.innerHTML += "#" + Data
            } else {
                divText.innerHTML += " - " + "#" + Data
            }

            document.getElementById("ContentPlaceHolder1_txtListEntrega").value = "";
        }
        function FormatearNumeroImpTotal(input) {
            number = Number(input.value);
            newnumber = formatearNumero(number);
            input.value = newnumber
        }

        function formatearNumero(numero) {
            return numero.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        }
        function revertirNumero(numeroFormateado) {
            var numero = parseFloat(numeroFormateado.replace(/,/g, ''));
            return numero;
        }

    </script>

</asp:Content>
