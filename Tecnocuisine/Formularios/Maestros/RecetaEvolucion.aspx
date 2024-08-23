<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecetaEvolucion.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.RecetaEvolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <%--  <div class="ibox-title">
                    <h5>Recetas</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>--%>
                <div class="">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="wrapper wrapper-content animated fadeInRight">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">

                                            <div class="ibox-content">
                                                <div style="margin-left: 0px; margin-right: 0px;" class="row">
                                                    <div class="col-md-10">

                                                        <h1>
                                                            <asp:Label runat="server" ID="lblDescripcion" Style="font-size: 2rem; font-weight: bold"></asp:Label>
                                                        </h1>

                                                        <div class="input-group m-b">
                                                            <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                                            <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                                        </div>

                                                    </div>

                                                </div>

                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>
                                                            <th>Fecha del cambio</th>
                                                            <th>Ingrediente</th>
                                                            <th style="text-align: right">Nuevo Costo Ingrediente</th>
                                                            <th style="text-align: right">Anterior Costo Ingrediente</th>
                                                            <th style="text-align: right">Nuevo Costo Total Receta</th>
                                                            <th style="text-align: right">Anterior Costo Total Receta</th>
                                                            <%--<th></th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phIngredientes" runat="server"></asp:PlaceHolder>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
