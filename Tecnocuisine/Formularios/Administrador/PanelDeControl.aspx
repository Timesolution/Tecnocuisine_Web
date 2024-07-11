<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PanelDeControl.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.PanelDeControl" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-6" style="margin-top: 15px;">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h4 style="margin-left: 5%">Descontar Stock de Recetas:</h4>
                                        </div>
                                            <div class="col-md-6">
                                                <select id="selection" class="form-control" style=" width 50%">
                                                   <option value='0' id="StockProduction">Descontar Stock por Produccion</option>  
                                                    <option value='1' id="StockVentas" >Descontar Stock por Ventas</option>
                                                </select>
                                            </div>
                                        <div class="col-md-2">
                                                 <div style="display: flex; align-items: end; justify-content: end;">
                                            <linkbutton class="btn btn-sm btn-primary" style="margin-right: 8px;" data-toggle="tooltip" data-original-title="Agregar"
                                                text="Agregar" validationgroup="AgregarEntregas" id="Button1"  onclick="GuardarPanel(event)">
                                                <i style="color: white" class="fa fa-check"></i>
                                            </linkbutton>
                                        </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="HiddenOption" runat="server" />

                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
        <script src="../Scripts/plugins/toastr/toastr.min.js"></script>
<script>
    $(document).ready(function () {
        let TrueOrFalse = document.getElementById("ContentPlaceHolder1_HiddenOption").value;
        console.log(TrueOrFalse)
        ChangeValue(TrueOrFalse)
    });


    function ChangeValue(value) {
        if (value == "true") {
            document.getElementById("selection").options.item(1).selected = 'selected';
        } else {
            document.getElementById("selection").options.item(0).selected = 'selected';

        }
    }

        function GuardarPanel(event) {
            event.preventDefault();
            let valor = document.getElementById("selection").value
            $.ajax({
                method: "POST",
                url: "PanelDeControl.aspx/CambiarPanel",
                data: '{value: "' + valor + '"}',
                contentType: "application/json",
                dataType: "json",
                dataType: "json",
                async: false,
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    toastr.success('Actualizo el Panel de Control');
                }
            });
        }

</script>
</asp:Content>

