<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PruebaDespegable.aspx.cs" Inherits="Tecnocuisine.Formularios.Ventas.PruebaDespegable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>


    <table>
        <tr>
            <td style="padding-right: 20px;"><strong>Fecha</strong></td>
            <td style="padding-right: 20px;"><strong>Origen</strong></td>
            <td style="padding-right: 20px;"><strong>Destino</strong></td>
            <td style="padding-right: 20px;"><strong>Orden</strong></td>
            <td style="padding-right: 20px;"><strong>Estado</strong></td>
        </tr>
        <tr class="clicker">
            <td style="padding-right: 20px;">2024-01-30</td>
            <td style="padding-right: 20px;">ALMACEN</td>
            <td style="padding-right: 20px;">COCINA CALIENTE</td>
            <td style="padding-right: 20px;">#000001</td>
            <td style="padding-right: 20px;">Confirmado</td>
        </tr>
        <tr>
            <td style="padding-right: 20px;">data1</td>
            <td style="padding-right: 20px;">data2</td>
            <td style="padding-right: 20px;">data3</td>
            <td style="padding-right: 20px;">data4</td>
            <td style="padding-right: 20px;">data5</td>
            <td style="padding-right: 20px;">data6</td>
            <td style="padding-right: 20px;">data7</td>
            <td style="padding-right: 20px;">data8</td>
        </tr>
        <tr class="clicker">
            <td>clicker</td>
        </tr>
        <tr>
            <td>of</td>
        </tr>
        <tr>
            <td>rows</td>
        </tr>
    </table>



    <div class="dd" id="nestable">
        <ol class="dd-list">

            <li class="dd-item">
                <div class="dd-handle">
                    2024-01-30
                    ALMACEN
                    COCINA CALIENTE	
                    #000001	
                    Confirmado
                </div>
                <ol class="dd-list">
                    <li class="dd-item" data-id="3">
                        ALMACEN
                        ACEITE PARA FREIR x LT	
                        0.2000	
                        0.2000	
                    </li>
                </ol>

            </li>

        </ol>
    </div>

    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>

    <script src="js/plugins/nestable/jquery.nestable.js"></script>

    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>


    <script>
        $('.clicker').click(function(){
          $(this).nextUntil('.clicker').slideToggle('normal');
        });
    </script>
    <script>
         $(document).ready(function(){

             var updateOutput = function (e) {
                 var list = e.length ? e : $(e.target),
                         output = list.data('output');
                 if (window.JSON) {
                     output.val(window.JSON.stringify(list.nestable('serialize')));
                 } else {
                     output.val('JSON browser support required for this demo.');
                 }
             };
             $('#nestable').nestable({
                 group: 1
             }).on('change', updateOutput);

             $('#nestable2').nestable({
                 group: 1
             }).on('change', updateOutput);

             updateOutput($('#nestable').data('output', $('#nestable-output')));
             updateOutput($('#nestable2').data('output', $('#nestable2-output')));

             $('#nestable-menu').on('click', function (e) {
                 var target = $(e.target),
                         action = target.data('action');
                 if (action === 'expand-all') {
                     $('.dd').nestable('expandAll');
                 }
                 if (action === 'collapse-all') {
                     $('.dd').nestable('collapseAll');
                 }
             });
         });
    </script>



</asp:Content>
