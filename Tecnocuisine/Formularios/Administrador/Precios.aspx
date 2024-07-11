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
                                <div class="form-check">
                                    <asp:RadioButton ID="rbUltimoPrecio" runat="server" GroupName="Precio" Text="Ultimo precio" CssClass="form-check-input" style="cursor:pointer" />
                                </div>
                                <div class="form-check">
                                    <asp:RadioButton ID="rbPrecioBarato" runat="server" GroupName="Precio" Text="Precio mas barato" CssClass="form-check-input" style="cursor:pointer"/>
                                </div>
                                <div class="form-check">
                                    <asp:RadioButton ID="rbPromedioPonderado" runat="server" GroupName="Precio" Text="Promedio ponderado" CssClass="form-check-input" style="cursor:pointer"/>
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

</asp:Content>
