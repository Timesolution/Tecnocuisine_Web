﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Precios.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.Precios" %>

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
                                    <div class="date-range" id="divPrecioBarato" style="display: none;">
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
                                        <br />
                                    </div>
                                </div>

                                <br />

                                <%--Promedio ponderado--%>
                                <div class="form-check">
                                    <asp:RadioButton ID="rbPromedioPonderado" runat="server" GroupName="Precio" Text="Promedio ponderado" CssClass="form-check-input" Style="cursor: pointer" onclick="showHideFields('promedioPonderado');" />
                                    <div class="date-range" id="divPromedioPonderado" style="display: none;">
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
                                        <br />
                                    </div>
                                </div>
                            </div>

                            <br />

                            <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showHideFields(option) {

            // Obtener los RadioButton que usaran fechas
            var rbPrecioBarato = document.getElementById('<%= rbPrecioBarato.ClientID %>');
            var rbPromedioPonderado = document.getElementById('<%= rbPromedioPonderado.ClientID %>');

            // Ocultar todos los campos primero
            document.getElementById('divPrecioBarato').style.display = 'none';
            document.getElementById('divPromedioPonderado').style.display = 'none';

            // Determinar cuál RadioButton está seleccionado y mostrar el campo correspondiente
            if (option === 'precioBarato' && rbPrecioBarato.checked) {
                document.getElementById('divPrecioBarato').style.display = 'block';
            }
            else if (option === 'promedioPonderado' && rbPromedioPonderado.checked) {
                document.getElementById('divPromedioPonderado').style.display = 'block';
            }
        }
</script>


</asp:Content>
