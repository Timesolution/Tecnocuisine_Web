<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Paises.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.Paises" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="container-fluid">
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="wrapper wrapper-content animated fadeInRight">
                                <div class="row">
                                    <div class="col-lg-12">
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

                                                        <a data-toggle="modal" data-backdrop="static" title="Agregar paises"
                                                            class="btn btn-primary dim"
                                                            style="margin-right: 1%; float: right" onclick="limpiarInputs(); openModalAgregar()"><i class='fa fa-plus'></i>
                                                        </a>
                                                    </div>
                                                </div>
                                                <table class="table table-striped table-bordered table-hover " id="editable">
                                                    <thead>
                                                        <tr>

                                                            <th style="width: 40%">País</th>
                                                            <th style="width: 40%">Categoria</th>
                                                            <th style="width: 20%">Acciones</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="phPaises" runat="server"></asp:PlaceHolder>
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


    <%-- Modal agregar --%>
    <div id="ModalNuevoPais" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h2 class="modal-title">Agregar pais
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
                        <label class="col-sm-2 control-label editable">Pais</label>
                        <div class="col-sm-9">
                            <asp:TextBox placeholder="Pais de procedencia" ID="txtPais"
                                oninput="this.value = this.value.replace(/[^A-Za-z0-9\s]/g, '');"
                                runat="server" class="form-control">
                            </asp:TextBox>
                            <div>
                                <p id="ValidaDescripcionPais" class="text-danger text-hide">Tienes que ingresar un pais</p>
                            </div>
                        </div>
                    </div>


                    <div class="form-group" style="padding-top: 7%">
                        <label class="col-sm-2 control-label editable">Tipo de documento</label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server"
                                oninput="this.value = this.value.replace(/[^A-Za-z0-9\s]/g, '');"
                                placeholder="Tipo documento"
                                ID="txtTipoDoc" class="form-control">
                            </asp:TextBox>
                            <div>
                                <p id="ValidaTipoDocumento" class="text-danger text-hide">Tienes que ingresar un tipo de documento</p>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary"
                        OnClick="btnAgregar_Click" OnClientClick="return validarFormulario();">
                        <span><i class="fa fa-check"></i>&nbsp;Agregar</span>
                    </asp:LinkButton>

                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                </div>
            </div>
        </div>
    </div>



    <%-- Modal editar --%>
    <div id="ModalEditarPais" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h2 class="modal-title">Editar pais 

                    </h2>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label editable">Pais</label>
                        <div class="col-sm-9">
                            <asp:TextBox placeholder="Pais de procedencia" ID="txtPaisEditar"
                                oninput="this.value = this.value.replace(/[^A-Za-z0-9\s]/g, '');"
                                runat="server" class="form-control">
                            </asp:TextBox>
                            <p id="ValidaDescripcionPaisEditar" class="text-danger text-hide">Tienes que ingresar un pais</p>
                        </div>
                    </div>


                    <div class="form-group" style="padding-top: 7%">
                        <label class="col-sm-2 control-label editable">Tipo de documento</label>
                        <div class="col-sm-9">
                            <asp:TextBox runat="server"
                                oninput="this.value = this.value.replace(/[^A-Za-z0-9\s]/g, '');"
                                placeholder="Tipo documento"
                                ID="txtTipoDocEditar" class="form-control">
                            </asp:TextBox>
                            <p id="ValidaTipoDocumentoEditar" class="text-danger text-hide">Tienes que ingresar un tipo de documento</p>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnEditarPais" runat="server" CssClass="btn btn-primary"
                        OnClientClick="changePais(); return false" Text="Editar">
                         <span><i class="fa fa-pencil"></i>&nbsp;Editar</span>
                    </asp:LinkButton>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:HiddenField ID="idPais" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div id="modalConfirmacion2" class="modal fade" role="dialog">
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
                    <button type="button" class="btn btn-white" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <asp:Button runat="server" ID="btnEliminar" Text="Eliminar" class="buttonLoading btn btn-danger" OnClick="btnSi_Click" />
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

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
            var oTable2 = $('#editable2').dataTable();

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

            /* Apply the jEditable handlers to the table */
            oTable2.$('td').editable('../example_ajax.php', {
                "callback": function (sValue, y) {
                    var aPos = oTable2.fnGetPosition(this);
                    oTable2.fnUpdate(sValue, aPos[0], aPos[1]);
                },
                "submitdata": function (value, settings) {
                    return {
                        "row_id": this.parentNode.getAttribute('id'),
                        "column": oTable2.fnGetPosition(this)[2]
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
            parent.style = 'display:none';
            var div = document.getElementById('editable_filter');
            var button = document.createElement('a');
            /* button.id = "btnAgregar";*/
            button.style.float = "right";
            button.style.marginRight = "1%";
            //button.setAttribute("type", "button");
            button.setAttribute("href", "ProductosABM.aspx");
            //button.setAttribute("href", "#modalAgregar");
            //button.setAttribute("onclick", "vaciarFormulario()");
            //button.setAttribute("data-toggle", "modal");
            button.setAttribute("class", "btn");

            button.innerHTML = "<i style='color: black' class='fa fa-plus'></i>";
            div.prepend(button);
            var filter = $("#editable_filter");
            filter[0].id = 'editable_filter2';

            //var filter = $("#editable_length");
            //filter[0].id = 'editable_length2';


            $('#txtBusqueda').on('keyup', function () {
                $('#editable').DataTable().search(
                    this.value
                ).draw();
            });
        });
    </script>
    <script type="text/javascript">

        function openModal() {
            $('#ModalNuevoPais .modal-title').text('Editar Pais');
            //document.getElementById('<%= btnAgregar.ClientID %>').innerHTML = '<i class="fa fa-check"></i>&nbsp;Editar';
            //document.getElementById('<%= btnAgregar.ClientID %>').innerHTML = "caca"
            $('#ModalNuevoPais').modal('show');
        }
        function abrirdialog(id) {
            $('#modalConfirmacion2').modal('show');
            document.getElementById('<%=hiddenID.ClientID%>').value = id;
        }


        function limpiarInputs()
        {
            document.getElementById('<%=txtPais.ClientID%>').value = "";
            document.getElementById('<%=txtTipoDoc.ClientID%>').value = "";
        }
    </script>

    <script>
        function openModalAgregar() {
            $('#ModalNuevoPais .modal-title').text('Agregar Pais');
            limpiarErrores();
            $('#ModalNuevoPais').modal('show');
        }
    </script>
    <script>
        function limpiarErrores(){
            let ValidaDescripcionPais = document.getElementById("ValidaDescripcionPais");
            ValidaDescripcionPais.className = "text-danger text-hide";
            let ValidatipoDocumento = document.getElementById("ValidaTipoDocumento");
            ValidatipoDocumento.className = "text-danger text-hide";

        }
    </script>
    <script>
        function limpiarErroresEditar(){
            let ValidaDescripcionPais = document.getElementById("ValidaDescripcionPaisEditar");
            ValidaDescripcionPais.className = "text-danger text-hide";
            let ValidatipoDocumento = document.getElementById("ValidaTipoDocumentoEditar");
            ValidatipoDocumento.className = "text-danger text-hide";

        }
    </script>
    <script>
        function openModalEditar(idPais){
            limpiarErroresEditar();
            document.getElementById('<%= idPais.ClientID %>').value = idPais;
            precargarCampos(idPais);
             setTimeout(function() {
                $('#ModalEditarPais').modal('show');
            }, 500); 
        }
    </script>

    <script>
        function changePais() {

             let rta = validarFormularioEditar();


             if(rta == true){
                 document.getElementById('<%= btnEditarPais.ClientID %>').setAttribute('disabled', 'disabled')
                 let pais = document.getElementById('<%= txtPaisEditar.ClientID %>').value
                 let tipoDocumento = document.getElementById('<%= txtTipoDocEditar.ClientID %>').value
                 let idPais = document.getElementById('<%= idPais.ClientID %>').value



                 $.ajax({
                     method: "POST",
                     url: "Paises.aspx/EditarPais",
                     data: "{pais: '" + pais + "', tipoDocumento: '" + tipoDocumento + "', idPais: '" + idPais + "'}",
                     contentType: "application/json",
                     dataType: 'json',
                     error: (error) => {
                         console.log(JSON.stringify(error));
                         toastr.warning('No se pudo agregar el sector productivo!', 'Atencion')
                         document.getElementById('<%= btnEditarPais.ClientID %>').removeAttribute('disabled')

                     },
                     success: (respuesta) => {
                         console.log(respuesta.d)
                         let res = respuesta.d

                         if (res == '1') {
                             document.getElementById('<%= btnEditarPais.ClientID %>').removeAttribute('disabled')
                             //CargarSectoresProductivo()
                             $('#ModalEditarPais').modal('hide')
                             window.location.href = "../../Formularios/Administrador/Paises.aspx";
                         }
     
                         if (res == '0') {
                             toastr.warning('Ya existe un pais con ese codigo!', 'Atencion')
                              document.getElementById('<%= btnEditarPais.ClientID %>').removeAttribute('disabled')
                             $('#ModalEditarPais').modal('hide')
                         } 

                         if (res == '-1') {
                             toastr.warning('No se pudo editar el pais!', 'Atencion')
                             document.getElementById('<%= btnEditarPais.ClientID %>').removeAttribute('disabled')
                             $('#ModalEditarPais').modal('hide')
                         } 

                         if (res == '-2') {
                             toastr.warning('Ya existe un pais con ese nombre!', 'Atencion')
                             document.getElementById('<%= btnEditarPais.ClientID %>').removeAttribute('disabled')
                            $('#ModalEditarPais').modal('hide')
                         } 

                     }
                 });
             }
            
        }
    </script>


    <script>
        function precargarCampos(idPais){
        
             $.ajax({
             method: "POST",
             url: "Paises.aspx/precargarCampos",
             data: JSON.stringify({ idPais: idPais }),
             contentType: "application/json",
             dataType: 'json',
             error: (error) => {
                 console.log(JSON.stringify(error));
             },
             success: (respuesta) => {
                 console.log(respuesta.d)
                 let pais = respuesta.d

                 if (pais != null && pais != '[]') {
                    let p = JSON.parse(pais);

                      p.forEach(element => {

                       document.getElementById('<%= txtPaisEditar.ClientID %>').value = element.Pais;
                       document.getElementById('<%= txtTipoDocEditar.ClientID %>').value = element.TipoDocumento;

                      })

                 }


             }
         });
    
        }
    </script>



    <script>
        function validarFormulario() {
            let errores = false;
            let descripcionPais = document.getElementById("ContentPlaceHolder1_txtPais").value;
            let ValidaDescripcionPais = document.getElementById("ValidaDescripcionPais");
            let tipoDocumento = document.getElementById("ContentPlaceHolder1_txtTipoDoc").value;
            let ValidatipoDocumento = document.getElementById("ValidaTipoDocumento");

            if (descripcionPais == "") {
                ValidaDescripcionPais.className = "text-danger";
                errores = true;
            } else {
                ValidaDescripcionPais.className = "text-danger text-hide";
            }

            if (tipoDocumento == "") {
                ValidatipoDocumento.className = "text-danger";
                errores = true;
            } else {
                ValidatipoDocumento.className = "text-danger text-hide";
            }

            return !errores; 
        }
    </script>



    <script>
        function validarFormularioEditar() {
            let errores = false;
            let descripcionPais = document.getElementById("ContentPlaceHolder1_txtPaisEditar").value;
            let ValidaDescripcionPaisEditar = document.getElementById("ValidaDescripcionPaisEditar");
            let tipoDocumento = document.getElementById("ContentPlaceHolder1_txtTipoDocEditar").value;
            let ValidatipoDocumentoEditar = document.getElementById("ValidaTipoDocumentoEditar");

            if (descripcionPais == "") {
                ValidaDescripcionPaisEditar.className = "text-danger";
                errores = true;
            } else {
                ValidaDescripcionPaisEditar.className = "text-danger text-hide";
            }

            if (tipoDocumento == "") {
                ValidatipoDocumentoEditar.className = "text-danger";
                errores = true;
            } else {
                ValidatipoDocumentoEditar.className = "text-danger text-hide";
            }

            return !errores; 
        }
    </script>



</asp:Content>
