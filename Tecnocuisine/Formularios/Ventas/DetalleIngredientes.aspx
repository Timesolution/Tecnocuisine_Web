<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleIngredientes.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.DetalleIngredientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="row" style="padding-left: 14px; padding-right: 14px">
       <!-- Utiliza col-lg-8 para la parte izquierda -->
       <div class="ibox-content m-b-sm border-bottom">
           <div class="col-lg-12" style="background-color: white">
               <div class="row">
                   <div class="col-lg-6">
                       <table class="table table-striped table-bordered table-hover " id="editable">
                           <thead>
                               <tr>
                                   <%--<th style="width: 35%">Fecha</th>--%>
                                   <th width: 25%">Receta</th>
                                   <th style="text-align: right; width: 15%">Cantidad</th>
                                   <th style="text-align: right; width: 20%">Costo Unitario</th>
                                   <th style="text-align: right; width: 10%">Stock</th>

                               </tr>
                           </thead>
                           <tbody>
                               <asp:PlaceHolder ID="phProductos" runat="server"></asp:PlaceHolder>
                           </tbody>
                       </table>
                   </div>
               </div>
       <%--         <a href="DetalleIngredientes.aspx" class="btn btn-primary dim" target="_blank"
                       id="detalleIngredienteURL" style="margin-right: 1%; float: left">Detalle Ingredientes</a>--%>
<%--               <asp:LinkButton runat="server" ID="DetalleIngredientes" 
                   OnClick="DetalleIngredientes_Click" class="btn btn-primary dim">Detalle ingredientes</asp:LinkButton>--%>
           </div>
       </div>
   </div>

</asp:Content>
