﻿<%@ Page Title="EstadoClientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstadoClientes.aspx.cs" Inherits="Tecnocuisine.EstadoClientes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container-fluid">

            <div class="ibox float-e-margins">

                <div class="ibox-content">
                    <div style="margin-left: 0px; margin-right: 0px;" class="row">
                        <div class="col-md-10">

                            <div class="input-group m-b">
                                <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                <input type="text" id="txtBusqueda" placeholder="Búsqueda..." class="form-control" style="width: 90%" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <a data-toggle="modal" data-backdrop="static" title="Agregar estado"
                                class="btn btn-primary dim" onclick="vaciarInputs(); openModalAgregar()"
                                style="margin-right: 1%; float: right"><i class='fa fa-plus'></i>
                            </a>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">
                                    <thead>
                                        <tr>
                                            <th style="width: 20%">#</th>
                                            <th style="width: 40%">Descripcion</th>
                                            <th style="width: 30%"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:PlaceHolder ID="phEstados" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div id="modalAgregar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title">Agregar</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <label class="col-sm-2 control-label editable">Descripción</label>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtDescripcionEstado" class="form-control" runat="server" />

                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary" OnClick="btnGuardar_Click"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                        <asp:HiddenField ID="hiddenEditar" runat="server" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>


                    </div>
                </div>
            </div>
        </div>


        <div id="modalConfirmacion2" class="modal fade" tabindex="-1" role="dialog">
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
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />
                        <asp:HiddenField ID="hiddenID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalEditar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Editar</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtDescripcionEditar" class="form-control" runat="server" />

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnEditar" class="buttonLoading btn btn-primary" OnClientClick="changeEstadoCliente()"><i class="fa fa-check"></i>&nbsp;Editar </asp:LinkButton>
                    <asp:HiddenField ID="idEstadoClienteEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                </div>
            </div>
        </div>
    </div>

    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }


        function vaciarInputs(){
            document.getElementById('<%= txtDescripcionEstado.ClientID %>').value = "";
        }
    </script>
    <script>
       function openModalEditar(idEstado){
           document.getElementById('<%= idEstadoClienteEditar.ClientID %>').value = idEstado;
           precargarCampos(idEstado);
            setTimeout(function() {
               $('#modalEditar').modal('show');
           }, 500); 
       }
    </script>
    <script>
        function precargarCampos(idEstado){
        
             $.ajax({
             method: "POST",
             url: "EstadoClientes.aspx/precargarCampos",
             data: JSON.stringify({ idEstado: idEstado }),
             contentType: "application/json",
             dataType: 'json',
             error: (error) => {
                 console.log(JSON.stringify(error));
             },
             success: (respuesta) => {
                 console.log(respuesta.d)
                 let estado = respuesta.d

                 if (estado != null && estado != '[]') {
                    let e = JSON.parse(estado);

                      e.forEach(element => {

                       document.getElementById('<%= txtDescripcionEditar.ClientID %>').value = element.Descripcion;

                      })

                 }


             }
         });
    
        }
    </script>

    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar .modal-title').text('Editar estado de cliente');
            document.getElementById('<%= btnGuardar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Editar';
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            ContentPlaceHolder1_txtDescripcionEstado.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'InsumosF', location.protocol + '//' + location.host + location.pathname);

        }
    </script>
    <script>
        function openModalAgregar() {
            $('#modalAgregar .modal-title').text('Agregar estado de cliente');
            document.getElementById('<%= btnGuardar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Agregar';
            $('#modalAgregar').modal('show');
        }
    </script>
    <script>
        $(document).ready(function () {


            $('.dataTables-example').dataTable({
                responsive: true,
                "dom": 'T<"clear">lfrtip',
                "tableTools": {
                    "sSwfPath": "js/plugins/dataTables/swf/copy_csv_xls_pdf.swf"
                }
            });
            /* Init DataTables */
            var oTable = $('#editable').dataTable();


            /* Apply the jEditable handlers to the table */
            oTable.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable.fnGetPosition(this);
                    oTable.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable.fnGetPosition(this)[2]
                    };
                },

                "width": "90%",
                "height": "100%",
                "pageLength": 25
            });


            $("#editable_filter").appendTo("#editable_length");

            $("#editable_filter").css('display', 'none');
            $("#editable_filter").css('padding-left', '5%');
            var parent = $("#editable_length")[0].parentNode;
            parent.className = 'col-sm-12';
            parent.style = 'display:none';
            var div = document.getElementById('editable_filter');
            var button = document.createElement('a');
            button.style.float = "right";
            button.style.marginRight = "1%";
            button.setAttribute("href", "ProductosABM.aspx");
            button.setAttribute("class", "btn");

            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';



            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });
        });
    </script>
    <script>
        function changeEstadoCliente() {


             document.getElementById('<%= btnEditar.ClientID %>').setAttribute('disabled', 'disabled')
             let descripcionEstadoClientes = document.getElementById('<%= txtDescripcionEditar.ClientID %>').value
             let idEstadoCliente = document.getElementById('<%= idEstadoClienteEditar.ClientID %>').value



             $.ajax({
                 method: "POST",
                 url: "EstadoClientes.aspx/EditarEstadoClientes",
                 data: "{descripcionEstadoClientes: '" + descripcionEstadoClientes + "', idEstadoCliente: '" + idEstadoCliente + "'}",
                 contentType: "application/json",
                 dataType: 'json',
                 error: (error) => {
                     console.log(JSON.stringify(error));
                     toastr.warning('No se pudo agregar el sector productivo!', 'Atencion')
                     document.getElementById('<%= btnEditar.ClientID %>').removeAttribute('disabled')

                 },
                 success: (respuesta) => {
                     console.log(respuesta.d)
                     let res = respuesta.d

                     if (res == '1') {
                         document.getElementById('<%= btnEditar.ClientID %>').removeAttribute('disabled')
                         //CargarSectoresProductivo()
                         $('#modalEditar').modal('hide')
                         window.location.href = "../../Formularios/Maestros/EstadoClientes.aspx";
                     }
     
                     if (res == '0') {
                         toastr.warning('Ya existe un Estado de cliente con ese nombre!', 'Atencion')
                          document.getElementById('<%= btnEditar.ClientID %>').removeAttribute('disabled')
                         $('#modalEditar').modal('hide')
                     } 

                     if (res == '-1') {
                         toastr.warning('No se pudo editar el Estado de cliente!', 'Atencion')
                         document.getElementById('<%= btnEditar.ClientID %>').removeAttribute('disabled')
                         $('#modalEditar').modal('hide')
                     } 

                     if (res == '-2') {
                         toastr.warning('Ya existe un Estado de cliente con ese nombre!', 'Atencion')
                         document.getElementById('<%= btnEditar.ClientID %>').removeAttribute('disabled')
                        $('#modalEditar').modal('hide')
                     } 

                 }
             });
        }
    </script>


</asp:Content>
