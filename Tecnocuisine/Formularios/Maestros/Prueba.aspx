<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="Tecnocuisine.Formularios.Maestros.Prueba" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <%-- <style>
        .dd-handle {
            display: table;
            width: 100%;
        }

        .cell {
            width: 20%; /* Esto asegura que cada celda ocupe el 25% del ancho del contenedor */
            display: inline-block; /* Para que las celdas floten una al lado de la otra */
            box-sizing: border-box; /* Para que el ancho incluya el relleno y el borde */
            border: 1px solid #000; /* Un borde para visualización */
            padding: 5px; /* Relleno para espacio interno */
        }

        .additional-content {
            position: absolute;
            bottom: -20px; /* Ajusta esto para controlar la distancia del contenido adicional debajo de la celda */
            left: 0;
            width: 100%;
            text-align: center; /* O ajusta según la alineación deseada */
        }


        .dd-handle {
            pointer-events: none; /* Desactiva la interacción del ratón */
        }


        .row {
            margin-bottom: 0; /* Establece el margen inferior a cero para eliminar el espacio entre filas */
        }
    </style>--%>


    <style>
        .cel {
            display: inline-block;
            border: 1px solid black; /* Establecer el borde */
            padding: 5px; /* Añadir espacio alrededor del contenido de la celda */
            margin-left: 10px; /* Espacio entre las celdas */
        }
    </style>

    <div class="dd" id="nestable2">
        <ol class="dd-list">

            <li class="dd-item" data-id="2">
                <div class="dd-handle">
                    <div class="cell">30/1/2024 00:00:00</div>
                    <div class="cell">ALMACEN</div>
                    <div class="cell">COCINA CALIENTE</div>
                    <div class="cell">#000001</div>
                    <div class="cell">A confirmar</div>
                </div>
                <ol class="dd-list">
                    <li class="dd-item" data-id="4">

                        <div class="row thead">
                            <div class="dd-handle">
                                <div class="cell">
                                    <strong>Sector Productivo</strong>
                                </div>
                                <div class="cell">
                                    <strong>Producto</strong>
                                </div>
                                <div class="cell">
                                    <strong>Cantidad</strong>
                                </div>
                                <div class="cell">
                                    <strong>Confirmada</strong>
                                </div>
                            </div>
                        </div>
                        <div class="row tbody">
                            <div class="dd-handle">

                                <div class="cell">
                                    Almacen
                                </div>
                                <div class="cell">
                                    ACEITE PARA FREIR x LT
                                </div>
                                <div class="cell">
                                    0.2000
                                </div>
                                <div class="cell">
                                    0.2000
                                </div>
                            </div>
                            <%--                                <ol class="dd-list">
                                    <li class="dd-item" data-id="4">
                                        <div class="cell">
                                            Almacen
                                        </div>
                                    </li>
                                </ol>--%>
                        </div>
                        <div class="row">
                            <div class="cell" style="width: 12.5%">
                                <strong>Sector Productivo</strong>
                            </div>
                            <div class="cell" style="width: 12.5%">
                                <strong>Producto</strong>
                            </div>
                            <div class="cell" style="width: 12.5%">
                                Cantidad
                            </div>
                            <div class="cell" style="width: 12.5%">
                                Confirmada
                            </div>
                            <div class="cell" style="width: 12.5%">
                                Producto Destino	
                            </div>
                            <div class="cell" style="width: 11%">
                                SectorDestino
                            </div>
                            <div class="cell" style="width: 11%">
                                Orden destino
                            </div>
                            <div class="cell" style="width: 11%">
                                Cliente destino
                            </div>
                        </div>
                        <div class="row">
                            <div class="cell" style="width: 12.5%">
                                ALMACEN	
                            </div>
                            <div class="cell" style="width: 12.5%">
                                ACEITE PARA FREIR x LT	
                            </div>
                            <div class="cell" style="width: 12.5%">
                                0.1000	
                            </div>
                            <div class="cell" style="width: 12.5%">
                                0.1000
                            </div>
                            <div class="cell" style="width: 12.5%">
                                SALSA POMMODORO x KG	
                            </div>
                            <div class="cell" style="width: 11%">
                                COCINA CALIENTE
                            </div>
                            <div class="cell" style="width: 11%">
                                #000204
                            </div>
                            <div class="cell" style="width: 11%">
                                AIR CANADA INTERNACIONA
                            </div>
                        </div>
                        <div class="row">
                            <div class="cell" style="width: 12.5%">
                                ALMACEN	
                            </div>
                            <div class="cell" style="width: 12.5%">
                                ACEITE PARA FREIR x LT	
                            </div>
                            <div class="cell" style="width: 12.5%">
                                0.1000	
                            </div>
                            <div class="cell" style="width: 12.5%">
                                0.1000
                            </div>
                            <div class="cell" style="width: 12.5%">
                                SALSA POMMODORO x KG	
                            </div>
                            <div class="cell" style="width: 11%">
                                COCINA CALIENTE
                            </div>
                            <div class="cell" style="width: 11%">
                                #000204
                            </div>
                            <div class="cell" style="width: 11%">
                                AIR CANADA INTERNACIONA
                            </div>
                        </div>

                    </li>
                </ol>
            </li>
            <%-- <li class="dd-item" data-id="5">
                <div class="dd-handle">5 - Consectetuer</div>
                <ol class="dd-list">
                    <li class="dd-item" data-id="6">
                        <div class="dd-handle">6 - Aliquam erat</div>
                    </li>
                    <li class="dd-item" data-id="7">
                        <div class="dd-handle">7 - Veniam quis</div>
                    </li>
                </ol>
            </li>--%>
        </ol>
    </div>


    <div class="row">
        <div class="col-lg-12">
            <div class="ibox  float-e-margins">
                <div class="ibox-content">

                    <div class="dd" id="nestable">
                        <ol id="main" runat="server" class="dd-list">
                        </ol>
                    </div>

                </div>
            </div>
            <linkbutton type="button" onclick="guardarNestedList();" style="float: right" class="btn btn-primary">Guardar&nbsp;<i style="color: white" class="fa fa-check"></i></linkbutton>

        </div>

    </div>



    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>

    <script src="js/plugins/nestable/jquery.nestable.js"></script>

    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>


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


           $('.dd-handle').click(function(){
            $(this).siblings('.additional-content').toggle();
           });
         });
    </script>
</asp:Content>
