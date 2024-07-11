<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rubros.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.Rubros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .valid {
            color: red;
            font-size: 10px;
        }

        .hideValid {
            display: none;
        }

        .hideBtn {
            display: none;
        }

        .hideBar {
            display: none;
        }
    </style>


    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">
            <div class="container-fluid-nestable">
                <div class="ibox nestable float-e-margins" style="padding: 1.5%">
                    <div class="ibox-content">
                        <div style="margin-left: 0px; margin-right: 0px;" class="row">
                            <div class="col-md-10">
                                <div class="input-group m-b">
                                    <span class="input-group-addon"><i style='color: black;' class='fa fa-search'></i></span>
                                    <input type="text" id="txtBusqueda" placeholder="Busqueda..." class="form-control" style="width: 90%" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a onclick="ModalAgregar()" class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                                <%--<a  class="btn btn-primary dim" style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>--%>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="">
                                    <table class="table table-striped table-bordered table-hover" id="editable">
                                        <thead>
                                            <tr>
                                                <th>Nombre</th>
                                                <%--<th>Estado</th>--%>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="phRubrosProveedores">
                                            <asp:PlaceHolder runat="server" ID="rubrosProveedoresPH"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                    <%-- <div id="Progressbars" class="progress progress-striped active">
                                <div style="width: 100%" aria-valuemax="100" aria-valuemin="0" aria-valuenow="75" role="progressbar" class="progress-bar progress-bar-danger">
                                    <span class="sr-only">100% Complete (success)</span>
                                </div>
                            </div>--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="modalABM" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="titleModal"></h4>
                </div>
                <div class="modal-body">
                    <div class="row m-b">
                        <label class="col-md-3">Rubro</label>
                        <div class="col-md-9">
                            <input id="txtNombre" class="form-control" onchange="valNombre()" />
                            <p id="valNombre" class="valid hideValid">*Completar Razon Social</p>
                        </div>
                    </div>
                    <%--        <div class="row m-b">
                        <label class="col-md-3">Empresa</label>
                        <div class="col-md-9">
                            <select class="form-control" id="ddlEmpresa" onchange="valEmpresa()">
                                <option value="0">Seleccione</option>
                            </select>
                            <p id="valEmpresa" class="hideValid">*Seleccione Condicion IVA</p>
                        </div>
                    </div>--%>
                </div>
                <div class="modal-footer">
                    <a id="btnAgregar" class="hideBtn btn btn-primary" onclick="AddRubrosProveedores()"><i class="fa fa-check"></i>&nbsp;Agregar</a>
                    <a id="btnModificar" class="hideBtn btn btn-primary" onclick="ChangeSectores()"><i class="fa fa-check"></i>&nbsp;Modificar</a>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                </div>
            </div>
        </div>
    </div>

    <div id="modalConfirmacion" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">Confirmar eliminacion</h4>
                </div>
                <div class="modal-body">
                    <p>Esta seguro que desea eliminarlo?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <a id="btnEliminar" class="btn btn-danger" onclick="RemoveSectores()">Eliminar</a>
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function ModalAgregar() {
            document.getElementById('btnAgregar').removeAttribute('disabled')
            document.getElementById('btnAgregar').className = 'btn btn-primary'
            document.getElementById('btnModificar').className = 'hideBtn'
            document.getElementById('titleModal').innerHTML = 'Agregar'
            $('#modalABM').modal('show')
        }


        function AddRubrosProveedores() {

            //if (ValAddSectores()) {
            document.getElementById('btnAgregar').setAttribute('disabled', 'disabled')
            let DescripcionRubro = document.getElementById('txtNombre').value
            //let empresa = document.getElementById('ddlEmpresa').value

            $.ajax({
                method: "POST",
                url: "Rubros.aspx/AddRubros",
                data: "{descripcionRubro: '" + DescripcionRubro + "'}",
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    console.log(respuesta.d)
                    //let res = respuesta.d

                    //if (res == '1') {
                    //    toastr.success('Se agrego correctamente el sector!', 'Felicitaciones')
                    //    CargarSectores()
                    //    $('#modalABM').modal('hide')
                    //} else {
                    //    toastr.warning('No se pudo agregar el sector!', 'Atencion')
                    //    document.getElementById('btnAgregar').removeAttribute('disabled')
                    //}

                }
            });
            //}
        }


        function valNombre() {
            let nombre = document.getElementById('txtNombre').value
            document.getElementById('valNombre').className = 'hideValid'

            if (nombre == '') {
                document.getElementById('valNombre').className = 'valid'
                return false
            } else {
                return true
            }
        }


        function ModalConfirmacion(id) {
            document.getElementById('btnEliminar').removeAttribute('disabled')
            document.getElementById('<%=hiddenID.ClientID%>').value = id
            $('#modalConfirmacion').modal('show')
        }


        function RemoveSectores() {
            console.log("hola")
            document.getElementById('btnEliminar').setAttribute('disabled', 'disabled')

            let id = document.getElementById('<%=hiddenID.ClientID%>').value
            console.log(id)
            $.ajax({
                method: "POST",
                url: "Rubros.aspx/RemoveRubrosProveedores",
                data: "{id: '" + id + "'}",
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    let res = respuesta.d

                    if (res == '1') {
                        toastr.success('Se elimino correctamente el rubro!', 'Felicitaciones')
                        //CargarSectores()
                        $('#modalConfirmacion').modal('hide')
                    } else {
                        toastr.warning('No se pudo eliminar el rubro!', 'Atencion')
                        document.getElementById('btnEliminar').removeAttribute('disabled')
                    }

                }
            });
        }

    </script>

</asp:Content>
