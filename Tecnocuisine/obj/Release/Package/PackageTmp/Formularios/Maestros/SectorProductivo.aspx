<%@ Page Title="Sectores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SectorProductivo.aspx.cs" Inherits="Tecnocuisine.SectorProductivo" %>

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

                            <linkbutton id="btnAgregar" onclick="openModalAgregar(); return false"
                                data-toggle="modal" class="btn btn-primary dim" style="margin-right: 1%; float: right">
                                <i class='fa fa-plus' data-toggle="tooltip" data-placement="top" title="Agregar sector productivo"></i>
                            </linkbutton>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover " id="editable">

                                    <thead>
                                        <tr>
                                            <%--<th>#</th>--%>
                                            <th style="text-align: right">Codigo</th>
                                            <th>Sector productivo</th>
                                            <th>Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody id="placeHolderSectores">
                                        <asp:PlaceHolder ID="phSectores" runat="server"></asp:PlaceHolder>
                                    </tbody>
                                </table>
                                <asp:HiddenField ID="toastr" runat="server" Value="0" />
                            </div>

                        </ContentTemplate>

                    </asp:UpdatePanel>
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
                        <%--<asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />--%>
                        <asp:LinkButton runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click"></asp:LinkButton>
                        <button type="button" class="btn btn-white" data-dismiss="modal">Cancelar</button>
                        <asp:HiddenField ID="hiddenID" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalAgregar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h2 class="modal-title">Agregar sector productivo
                        <span>
                            <%--<i style='color:black;' class='fa fa-search'></i>--%>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                                <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                            </svg>

                        </span>
                    </h2>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-2 control-label editable">Código</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtCodigoSector" class="form-control" runat="server" />
                            <p id="ValidaCodigoSector" class="text-danger text-hide">Tienes que ingresar un codigo de sector</p>
                        </div>
                    </div>


                    <div class="form-group" style="padding-top: 7%">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtDescripcionSector" class="form-control"
                                runat="server" pattern="/^[0-9]+$/" />
                            <p id="ValidaDescripcionSector" class="text-danger text-hide">Tienes que ingresar una descripcion de sector</p>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnGuardar" class="buttonLoading btn btn-primary"
                        OnClientClick="AddSectorProductivo(); return false"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>



    <div id="modalEditar" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h2 class="modal-title">Editar sector productivo
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512" style="width: 50px; vertical-align: middle; margin-left: 25px;">
                            <path d="M240 144c0-53-43-96-96-96s-96 43-96 96s43 96 96 96s96-43 96-96zm44.4 32C269.9 240.1 212.5 288 144 288C64.5 288 0 223.5 0 144S64.5 0 144 0c68.5 0 125.9 47.9 140.4 112h71.8c8.8-9.8 21.6-16 35.8-16H496c26.5 0 48 21.5 48 48s-21.5 48-48 48H392c-14.2 0-27-6.2-35.8-16H284.4zM144 208c-35.3 0-64-28.7-64-64s28.7-64 64-64s64 28.7 64 64s-28.7 64-64 64zm256 32c13.3 0 24 10.7 24 24v8h96c13.3 0 24 10.7 24 24s-10.7 24-24 24H280c-13.3 0-24-10.7-24-24s10.7-24 24-24h96v-8c0-13.3 10.7-24 24-24zM288 464V352H512V464c0 26.5-21.5 48-48 48H336c-26.5 0-48-21.5-48-48zM48 320h80 16 32c26.5 0 48 21.5 48 48s-21.5 48-48 48H160c0 17.7-14.3 32-32 32H64c-17.7 0-32-14.3-32-32V336c0-8.8 7.2-16 16-16zm128 64c8.8 0 16-7.2 16-16s-7.2-16-16-16H160v32h16zM24 464H200c13.3 0 24 10.7 24 24s-10.7 24-24 24H24c-13.3 0-24-10.7-24-24s10.7-24 24-24z" />
                        </svg>

                    </span>
                    </h2>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-2 control-label editable">Código</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="TextBox1" class="form-control" runat="server" />

                        </div>
                    </div>


                    <div class="form-group" style="padding-top: 7%">
                        <div class="col-sm-1">
                            <label style="color: red;" class="danger">*</label>
                        </div>
                        <label class="col-sm-2 control-label editable">Descripción</label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="TextBox2" class="form-control" runat="server" pattern="/^[0-9]+$/" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="LinkButton1" class="buttonLoading btn btn-primary"
                        OnClientClick="changeSectorProductivo(); return false"><i class="fa fa-check"></i>&nbsp;Agregar </asp:LinkButton>
                    <asp:HiddenField ID="idButton" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        function AddSectorProductivo() {


             let r = validarFormulario();

             if(r == false){
             
                document.getElementById('btnAgregar').setAttribute('disabled', 'disabled')
                let codigoSector = document.getElementById('<%= txtCodigoSector.ClientID %>').value
                let descripcionSector = document.getElementById('<%= txtDescripcionSector.ClientID %>').value

               
                $.ajax({
                    method: "POST",
                    url: "SectorProductivo.aspx/GuardarSector",
                    data: "{codigoSector: '" + codigoSector + "', descripcionSector: '" + descripcionSector + "'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                        toastr.warning('No se pudo agregar el sector productivo!', 'Atencion')
                        document.getElementById('btnAgregar').removeAttribute('disabled')
               
                    },
                    success: (respuesta) => {
                        console.log(respuesta.d)
                        let res = respuesta.d
               
                        if (res == '1') {
                            document.getElementById('btnAgregar').removeAttribute('disabled')
                            //CargarSectoresProductivo()
                            $('#modalAgregar').modal('hide')
                            window.location.href = "../../Formularios/Maestros/SectorProductivo.aspx";
                        }
                        
                        if (res == '0') {
                            toastr.warning('Ya existe un sector productivo con ese codigo!', 'Atencion')
                            document.getElementById('btnAgregar').removeAttribute('disabled')
                            $('#modalAgregar').modal('hide')
                        } 

                        if (res == '-1') {
                            toastr.warning('No se pudo agregar el sector productivo!', 'Atencion')
                            document.getElementById('btnAgregar').removeAttribute('disabled')
                            $('#modalAgregar').modal('hide')
                        } 

                        if (res == '-2') {
                            toastr.warning('Ya existe un sector productivo con ese nombre!', 'Atencion')
                            document.getElementById('btnAgregar').removeAttribute('disabled')
                           $('#modalAgregar').modal('hide')
                        } 
               
                    }
                });
           }
        }
    </script>
    <script>
        function validarFormulario(){
        
        let errores = false;
        let codigoSector = document.getElementById("ContentPlaceHolder1_txtCodigoSector").value
        let ValivaCodigoSector = document.getElementById("ValidaCodigoSector");
        let txtSector = document.getElementById("ContentPlaceHolder1_txtDescripcionSector").value
        let ValivaSector = document.getElementById("ValidaDescripcionSector");


            if (codigoSector == "") {
                ValivaCodigoSector.className = "text-danger"
                errores = true;

            } else {
                ValivaCodigoSector.className = "text-danger text-hide"
                errores = false;
            }

            if (txtSector == "") {
                ValivaSector.className = "text-danger"
                errores = true;

            } else {
                ValivaSector.className = "text-danger text-hide"
                errores = false;
            }

            return errores;
        
        }
    </script>
    <script>
        function precargarCampos(idSector){
            
             $.ajax({
             method: "POST",
             url: "SectorProductivo.aspx/precargarCampos",
             data: JSON.stringify({ idSector: idSector }),
             contentType: "application/json",
             dataType: 'json',
             error: (error) => {
                 console.log(JSON.stringify(error));
             },
             success: (respuesta) => {
                 console.log(respuesta.d)
                 let sector = respuesta.d

                 if (sector != null && sector != '[]') {
                    let sec = JSON.parse(sector);

                      sec.forEach(element => {

                       document.getElementById('<%= TextBox1.ClientID %>').value = element.Codigo;
                       document.getElementById('<%= TextBox2.ClientID %>').value = element.Descripcion;

                      })

                 }


             }
         });
        
        }
    </script>
    <script>
    function changeSectorProductivo() {


         document.getElementById('<%= LinkButton1.ClientID %>').setAttribute('disabled', 'disabled')
         let codigoSector = document.getElementById('<%= TextBox1.ClientID %>').value
         let descripcionSector = document.getElementById('<%= TextBox2.ClientID %>').value
         let idSector = document.getElementById('<%= idButton.ClientID %>').value



         $.ajax({
             method: "POST",
             url: "SectorProductivo.aspx/EditarSector",
             data: "{codigoSector: '" + codigoSector + "', descripcionSector: '" + descripcionSector + "', idSector: '" + idSector + "'}",
             contentType: "application/json",
             dataType: 'json',
             error: (error) => {
                 console.log(JSON.stringify(error));
                 toastr.warning('No se pudo agregar el sector productivo!', 'Atencion')
                 document.getElementById('<%= LinkButton1.ClientID %>').removeAttribute('disabled')

             },
             success: (respuesta) => {
                 console.log(respuesta.d)
                 let res = respuesta.d

                 if (res == '1') {
                     document.getElementById('<%= LinkButton1.ClientID %>').removeAttribute('disabled')
                     //CargarSectoresProductivo()
                     $('#modalEditar').modal('hide')
                     window.location.href = "../../Formularios/Maestros/SectorProductivo.aspx";
                 }
         
                 if (res == '0') {
                     toastr.warning('Ya existe un sector productivo con ese codigo!', 'Atencion')
                      document.getElementById('<%= LinkButton1.ClientID %>').removeAttribute('disabled')
                     $('#modalEditar').modal('hide')
                 } 

                 if (res == '-1') {
                     toastr.warning('No se pudo editar el sector productivo!', 'Atencion')
                     document.getElementById('<%= LinkButton1.ClientID %>').removeAttribute('disabled')
                     $('#modalEditar').modal('hide')
                 } 

                 if (res == '-2') {
                     toastr.warning('Ya existe un sector productivo con ese nombre!', 'Atencion')
                     document.getElementById('<%= LinkButton1.ClientID %>').removeAttribute('disabled')
                    $('#modalEditar').modal('hide')
                 } 

             }
         });
    }
    </script>
    <script>
        function editarSector(valor) {
            document.getElementById('<%= idButton.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>
    <script>
        function CargarSectoresProductivo(){
            
                $.ajax({
                method: "POST",
                url: "SectorProductivo.aspx/CargarSectoresProductivo",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let sectores = respuesta.d
                    if (sectores != null && sectores != '[]') {
                        let sec = JSON.parse(sectores)

                        let placeHolderSectores = ''
                        sec.forEach(element => {

                            placeHolderSectores += ` <tr>
                                                <td>${element.Codigo}</td>
                                                <td>${element.Descripcion}</td>
                                               </tr>`
                        })
                        //document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('placeHolderSectores').innerHTML = placeHolderSectores
                    } else {
                        //document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('placeHolderSectores').innerHTML = ""
                    }
                }
            });
        
        }
    </script>
    <script>
        function ModalAgregar() {
            //vaciarInputs()
            //document.getElementById('btnAgregar').removeAttribute('disabled')
            //document.getElementById('btnAgregar').className = 'btn btn-primary'
            //document.getElementById('btnModificar').className = 'hideBtn'
            //document.getElementById('titleModal').innerHTML = 'Agregar'
            $('#modalAgregar').modal('show')
        }
    </script>
    <script>
        function openModalEditar(idSector){
            document.getElementById('<%= idButton.ClientID %>').value = idSector;
            precargarCampos(idSector);
             setTimeout(function() {
                $('#modalEditar').modal('show');
            }, 500); 
        }
    </script>
    <script>           
        function abrirdialog(valor) {
            document.getElementById('<%= hiddenID.ClientID %>').value = valor;
            $('#modalconfirmacion2').modal('show');
        }
    </script>

    <script>
        function openModalAgregar() {
            $('#modalAgregar .modal-title').text('Agregar Sector productivo');
            document.getElementById('<%= btnGuardar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Agregar';
            borrarMensajeError();
            $('#modalAgregar').modal('show');
        }
    </script>
    <script>
        //esta funcion limpia los mensajes de error cuando apenas se abre el modal
        function borrarMensajeError(){
           let ValivaSector = document.getElementById("ValidaDescripcionSector");
           let ValivaCodigoSector = document.getElementById("ValidaCodigoSector");
           ValivaCodigoSector.className = "text-danger text-hide"
           ValivaSector.className = "text-danger text-hide"
        
        }
    </script>
    <script type="text/javascript">
        function openModal() {
            $('#modalAgregar .modal-title').text('Editar Sector productivo');
            document.getElementById('<%= btnGuardar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Editar';
            $('#modalAgregar').modal('show');
        }
        function vaciarFormulario() {
            //ContentPlaceHolder1_txtDescripcionClasificacion.value = "";
            //ContentPlaceHolder1_txtCantidadClasificacion.value = "";
            ContentPlaceHolder1_txtDescripcionSector.value = "";
            ContentPlaceHolder1_txtCodigoSector.value = "";
            ContentPlaceHolder1_hiddenEditar.value = "";
            window.history.pushState('', 'SectorProductivo', location.protocol + '//' + location.host + location.pathname);

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

            //Este codigo pertenece al filtro dinamico
            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                this.value
                ).draw();
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
                "height": "100%"
            });
            $("#editable_filter").appendTo("#editable_length");

            $("#editable_filter").css('display', 'none');
            $("#editable_filter").css('padding-left', '5%');
            var parent = $("#editable_length")[0].parentNode;
            parent.className = 'col-sm-12';
         
            var div = document.getElementById('editable_filter');
            var button = document.createElement('linkbutton');
            button.id = "btnAgregar";
            button.style.float = "right";
            button.style.marginRight = "1%";
            button.setAttribute("type", "button");
            button.setAttribute("href", "#modalAgregar");
            button.setAttribute("onclick", "vaciarFormulario()");
            button.setAttribute("data-toggle", "modal");
            button.setAttribute("class", "btn");
            
            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';
            var filter = $("#editable_length").css('display', 'none');
            filter[0].id = 'editable_length2';

            validarDescripcionSectorSoloLetrasYNumeros();
        });
    </script>
    <script>
            function validarDescripcionSectorSoloLetrasYNumeros() {
                document.getElementById('<%=txtDescripcionSector.ClientID%>').addEventListener('keydown', function(event) {
                    if (!/^[a-zA-Z0-9]*$/.test(event.key) && !event.ctrlKey && event.key.length === 1) {
                        event.preventDefault();
                    }
                });
            }
    </script>
</asp:Content>
