<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Proveedores.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.Proveedores" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="row wrapper border-bottom white-bg page-heading" style="padding-bottom: 0px;">
            <div class="panel blank-panel">
                <div class="panel-heading">

                    <div class="panel-options">

                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#tab-1">Datos generales</a></li>
                            <li class=""><a data-toggle="tab" href="#tab-2">Direcciones</a></li>
                            <li class=""><a data-toggle="tab" href="#tab-3">Telefonos</a></li>
                        </ul>
                    </div>
                </div>

                <div class="panel-body" style="padding-bottom: 0px;">

                    <div class="tab-content">
                        <div id="tab-1" class="tab-pane active">
                            <div class="ibox float-e-margins">
                                <div>
                                    <div>
                                        <div role="form" class="form-horizontal col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Codigo Proveedor</label>

                                                        <div class="col-sm-4">
                                                            <asp:TextBox runat="server" ID="txtCodigo" onfocusout="Validar(this.id)" placeholder="Ingrese Codigo" class="form-control"></asp:TextBox>
                                                            <label id="ValidadorCodigo" style="display: none">*Required Field</label>
                                                            <%--<asp:RequiredFieldValidator Style="display: none" ID="rfvCodigo" runat="server" ErrorMessage="<h3>*</h3>" SetFocusOnError="false" ForeColor="Red" Font-Bold="true" ValidationGroup="AgregarProveedor" ControlToValidate="txtCodigo"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="hr-line-dashed"></div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Razon social</label>

                                                        <div class="col-sm-4">
                                                            <asp:TextBox placeholder="Ingrese Razon social" class="form-control" runat="server" ID="txtRazonSocial"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Style="display: none" ID="RequiredFieldValidator1" runat="server" ErrorMessage="<h3>*</h3>" SetFocusOnError="false" ForeColor="Red" Font-Bold="true" ValidationGroup="AgregarProveedor" ControlToValidate="txtRazonSocial"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="hr-line-dashed"></div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Alias</label>

                                                        <div class="col-sm-4">
                                                            <asp:TextBox placeholder="Ingrese Alias" class="form-control" runat="server" ID="txtAlias"></asp:TextBox>
                                                    <%--        <asp:RequiredFieldValidator Style="display: none" 
                                                                ID="RequiredFieldValidator5" runat="server" ErrorMessage="<h3>*</h3>" 
                                                                SetFocusOnError="false" ForeColor="Red" Font-Bold="true" 
                                                                ValidationGroup="AgregarProveedor" 
                                                                ControlToValidate="txtRazonSocial"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="hr-line-dashed"></div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Pais / Documento</label>

                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="ddlPais" runat="server" class="form-control"></asp:DropDownList>
                                                            <%-- <select class="form-control" name="account">
                                                        <option>Seleccione</option>
                                                        <option>Argentina</option>
                                                        <option>Brasil</option>
                                                        <option>Estados Unidos</option>
                                                        <option>Mexico</option>
                                                    </select>--%>
                                                   <%--         <asp:RequiredFieldValidator Style="display: none" ID="RequiredFieldValidator3"
                                                                runat="server" InitialValue="-1" ErrorMessage="<h3>*</h3>"
                                                                SetFocusOnError="false" ForeColor="Red" Font-Bold="true"
                                                                ValidationGroup="AgregarProveedor" ControlToValidate="ddlPais">
                                                            </asp:RequiredFieldValidator>--%>

                                                        </div>
                                                    </div>
                                                    <div class="hr-line-dashed"></div>

                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Numero</label>

                                                        <div class="col-sm-4">
                                                            <asp:TextBox placeholder="Ingrese numero" class="form-control" runat="server" ID="txtNumero"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Style="display: none" ID="RequiredFieldValidator4" runat="server" ErrorMessage="<h3>*</h3>" SetFocusOnError="false" ForeColor="Red" Font-Bold="true" ValidationGroup="AgregarProveedor" ControlToValidate="txtNumero"></asp:RequiredFieldValidator>--%>

                                                        </div>
                                                    </div>
                                                    <%--       <div class="hr-line-dashed"></div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Rubro</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="ddlRubro" runat="server" class="form-control"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator Style="display: none" ID="RequiredFieldValidator2"
                                                                runat="server" InitialValue="-1" ErrorMessage="<h3>*</h3>"
                                                                SetFocusOnError="false" ForeColor="Red" Font-Bold="true"
                                                                ValidationGroup="AgregarProveedor" ControlToValidate="ddlRubro">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>--%>

                                                    <div class="hr-line-dashed"></div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Rubro</label>
                                                        <div class="col-sm-4">
                                                            <asp:DropDownList ID="ddlRubro" runat="server"
                                                                CssClass="chosen-select form-control"
                                                                DataTextField="CountryName" DataValueField="CountryCode"
                                                                Data-placeholder="Seleccione Rubro..." Width="100%">
                                                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                    <div class="hr-line-dashed"></div>
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">Estado</label>

                                                        <div class="col-sm-4">
                                                            <asp:DropDownList runat="server" ID="ddlEstado" class="form-control">
                                                                <asp:ListItem Value="1">Activo</asp:ListItem>
                                                                <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div class="col-sm-4 col-sm-offset-2">
                                                <asp:Button class="btn btn-primary" Text="Guardar" runat="server"
                                                    ID="btnGuardar" ValidationGroup="AgregarProveedor" OnClick="btnGuardar_Click" />
                                                <%--<button class="btn btn-primary" type="submit">Guardar</button>--%>
                                                <a class="btn btn-danger" href="ProveedoresP.aspx">Cancelar</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tab-2" class="tab-pane">
                            <strong>Donec quam felis</strong>

                            <p>a confirmar panel 2</p>
                        </div>
                        <div id="tab-3" class="tab-pane">
                            <strong>Donec quam felis</strong>

                            <p>A confirmar panel 3</p>
                        </div>
                    </div>
                </div>
                <div id="modalConfirmacion2" class="modal" role="dialog">
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
                                <button type="button" class="btn btn-white" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                                <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" />
                                <asp:HiddenField ID="hiddenID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Chosen -->
    <script src="/js/plugins/chosen/chosen.jquery.js"></script>
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
    </script>
    <script>
        function Validar(id) {
            let element = document.getElementById(id).value;
            if (element == "") {
                if (id.includes('txtCodigo')) {
                    document.getElementById('ValidadorCodigo').className = 'text-danger'
                    document.getElementById("ValidadorCodigo").style["display"] = "unset";
                }
            } else {
                if (id.includes('txtCodigo')) {
                    document.getElementById('ValidadorCodigo').classList.remove("text-danger");
                    document.getElementById("ValidadorCodigo").style["display"] = "none";
                }
            }
        }
    </script>

</asp:Content>
