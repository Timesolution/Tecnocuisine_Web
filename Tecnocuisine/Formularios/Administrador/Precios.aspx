<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Precios.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.Precios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">
            <div class="container-fluid-nestable">
                <div class="ibox nestable float-e-margins" style="padding: 1.5%;">
                    <div class="ibox-content" style="padding: 1.5%;">

                        <div style="padding-left: 2rem; padding-bottom: 2rem">

                            <h1>Configuracion de Precios</h1>
                            <br />
                            <h3>Selecciona tu preferencia para los costos de productos y recetas</h3>

                            <br />

                            <%--Radio Buttons--%>
                            <div>
                                <%--Ultimo precio--%>
                                <div class="form-check">
                                    <asp:RadioButton ID="rbUltimoPrecio" runat="server" GroupName="Precio" Text="Ultimo precio" CssClass="form-check-input" Style="cursor: pointer" onclick="showHideFields('ultimoPrecio');" />
                                </div>

                                <br />

                                <%--Precio mas barato--%>
                                <div class="form-check">
                                    <asp:RadioButton ID="rbPrecioBarato" runat="server" GroupName="Precio" Text="Precio más barato" CssClass="form-check-input" Style="cursor: pointer;" onclick="showHideFields('precioBarato');" />
                                    <asp:Panel runat="server" class="date-range" ID="divPrecioBarato">
                                        <p>Indica el rango de fechas a utilizar para el calculo</p>
                                        <div style="display: flex">
                                            <div>
                                                <label for="txtPrecioBaratoInicio" style="font-weight: 600;">Fecha inicial:</label>
                                                <asp:TextBox ID="txtPrecioBaratoInicio" runat="server" CssClass="form-control" Style="width: auto" TextMode="Date" />
                                            </div>
                                            <div>
                                                <label for="txtPrecioBaratoFin" style="font-weight: 600;">Fecha final:</label>
                                                <asp:TextBox ID="txtPrecioBaratoFin" runat="server" CssClass="form-control" Style="width: auto" TextMode="Date" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <br />
                                    <div>
                                        <asp:Label ID="lblErrorPrecioBarato" runat="server" ForeColor="Red" />
                                    </div>
                                </div>

                                <br />

                                <%--Promedio ponderado--%>
                                <div class="form-check">
                                    <asp:RadioButton ID="rbPromedioPonderado" runat="server" GroupName="Precio" Text="Promedio ponderado" CssClass="form-check-input" Style="cursor: pointer" onclick="showHideFields('promedioPonderado');" />
                                    <asp:Panel runat="server" class="date-range" ID="divPromedioPonderado">
                                        <p>Indica el rango de fechas a utilizar para el calculo</p>
                                        <div style="display: flex">
                                            <div>
                                                <label for="txtPromedioPonderadoInicio" style="font-weight: 600;">Fecha inicial:</label>
                                                <asp:TextBox ID="txtPromedioPonderadoInicio" runat="server" CssClass="form-control" Style="width: auto" TextMode="Date" />
                                            </div>
                                            <div>
                                                <label for="txtPromedioPonderadoFin" style="font-weight: 600;">Fecha final:</label>
                                                <asp:TextBox ID="txtPromedioPonderadoFin" runat="server" CssClass="form-control" Style="width: auto" TextMode="Date" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <br />
                                    <div>
                                        <asp:Label ID="lblErrorPromedioPonderado" runat="server" ForeColor="Red" />
                                    </div>
                                </div>
                            </div>

                            <%--Food Cost--%>
                            <div>
                                <label for="porcentajeAlertaFoodCost" style="font-weight: 600;">Porcentaje de alerta (Food Cost):</label>
                                <div class="input-group" style="display: flex">
                                    <div>
                                        <asp:TextBox ID="txtPorcentajeAlertaFoodCost" runat="server" CssClass="form-control" TextMode="Number" />
                                    </div>
                                    <span class="input-group-addon" style="align-items:center;align-content:center; width:auto">%</span>
                                </div>

                            </div>

                            <br />

                            <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardar_Click" OnClientClick="return validarCampos()" />

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showHideFields(option) {
            limpiarErrores();

            <%--// Obtener los RadioButton que usan rangos de fechas
            var rbPrecioBarato = document.getElementById('<%= rbPrecioBarato.ClientID %>');
            var rbPromedioPonderado = document.getElementById('<%= rbPromedioPonderado.ClientID %>');

            // Ocultar todos los campos primero
            document.getElementById('<%= divPrecioBarato.ClientID %>').style.display = 'none';
            document.getElementById('<%= divPromedioPonderado.ClientID %>').style.display = 'none';
            
            // Determinar cuál RadioButton está seleccionado y mostrar el campo correspondiente
            if (option === 'precioBarato' && rbPrecioBarato.checked) {
                document.getElementById('<%= divPrecioBarato.ClientID %>').style.display = 'block';
            }
            else if (option === 'promedioPonderado' && rbPromedioPonderado.checked) {
                document.getElementById('<%= divPromedioPonderado.ClientID %>').style.display = 'block';
            }--%>
        }

        function validarCampos() {
            var rbPrecioBarato = document.getElementById('<%= rbPrecioBarato.ClientID %>');
            var rbPromedioPonderado = document.getElementById('<%= rbPromedioPonderado.ClientID %>');

            if (rbPrecioBarato.checked) {
                return validarOpcionPrecioMasBarato()
            }
            else if (rbPromedioPonderado.checked) {
                return validarOpcionPromedioPonderado()
            }

            return true;
        }

        function validarOpcionPrecioMasBarato() {
            var txtPrecioBaratoInicio = document.getElementById('<%= txtPrecioBaratoInicio.ClientID %>');
            var txtPrecioBaratoFin = document.getElementById('<%= txtPrecioBaratoFin.ClientID %>');

            if (txtPrecioBaratoInicio.value === "" || txtPrecioBaratoFin.value === "") {
                var lblErrorPrecioBarato = document.getElementById('<%= lblErrorPrecioBarato.ClientID %>');
                lblErrorPrecioBarato.textContent = "Las fechas no fueron validas.";
                return false;
            }

            return true;
        }

        function validarOpcionPromedioPonderado() {
            var txtPromedioPonderadoInicio = document.getElementById('<%= txtPromedioPonderadoInicio.ClientID %>');
            var txtPromedioPonderadoFin = document.getElementById('<%= txtPromedioPonderadoFin.ClientID %>');

            if (txtPromedioPonderadoInicio.value === "" || txtPromedioPonderadoFin.value === "") {
                var lblErrorPromedioPonderado = document.getElementById('<%= lblErrorPromedioPonderado.ClientID %>');
                lblErrorPromedioPonderado.textContent = "Las fechas no fueron validas.";
                return false;
            }

            return true;
        }

        function limpiarErrores() {
            var lblErrorPrecioBarato = document.getElementById('<%= lblErrorPrecioBarato.ClientID %>');
            lblErrorPrecioBarato.textContent = "";

            var lblErrorPromedioPonderado = document.getElementById('<%= lblErrorPromedioPonderado.ClientID %>');
            lblErrorPromedioPonderado.textContent = "";
        }
    </script>


</asp:Content>
