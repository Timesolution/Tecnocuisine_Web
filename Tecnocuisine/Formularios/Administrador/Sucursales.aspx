<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Sucursales.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.Sucursales" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

        .hideBar{
            display:none;
        }
    </style>
    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">
            <div class="container-fluid-nestable">
                <div class="ibox nestable float-e-margins" style="padding: 1.5%;">
                    <div class="ibox-title">
                        <h5>Herramientas</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row m-l m-r">
                            <div class="col-md-12 text-right ">
                                <a onclick="vaciarInputs();ModalAgregar()" data-toggle="tooltip" data-placement="top" title="Agregar" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="wrapper wrapper-content">
        <div class="row animated fadeInRight">
            <div class="container-fluid-nestable">

                <div class="ibox nestable float-e-margins" style="padding: 1.5%">
                    <div class="ibox-title">
                        <h5>Sucursales</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">

                        <div class="table-responsive">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Nombre</th>
                                        <th>Direccion</th>
                                        <th>Estado</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody id="phSucursales">
                                </tbody>
                            </table>
                            <div id="Progressbars" class="progress progress-striped active">
                                <div style="width: 100%" aria-valuemax="100" aria-valuemin="0" aria-valuenow="75" role="progressbar" class="progress-bar progress-bar-danger">
                                    <span class="sr-only">100% Complete (success)</span>
                                </div>
                            </div>
                        </div>

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
                        <label class="col-md-3">Nombre</label>
                        <div class="col-md-9">
                            <input id="txtNombre" class="form-control" onchange="valNombre()" />
                            <p id="valNombre" class="valid hideValid">*Completar Razon Social</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">Direccion</label>
                        <div class="col-md-9">
                            <input id="txtDir" class="form-control" onchange="valDir()" />
                            <p id="valDir" class="hideValid">*Completar Direccion</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">Empresa</label>
                        <div class="col-md-9">
                            <select class="form-control" id="ddlEmpresa" onchange="valEmpresa()">
                                <option value="0">Seleccione</option>
                            </select>
                            <p id="valEmpresa" class="hideValid">*Seleccione Condicion IVA</p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnAgregar" class="hideBtn btn btn-primary" onclick="AddSucursal()"><i class="fa fa-check"></i>&nbsp;Agregar</a>
                    <a id="btnModificar" class="hideBtn btn btn-primary" onclick="ChangeSucursal()"><i class="fa fa-check"></i>&nbsp;Modificar</a>
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
                    <p>Esta seguro que lo desea eliminarlo?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-white" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                    <a id="btnEliminar" class="btn btn-danger" onclick="RemoveSucursal()">Eliminar</a>
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script>  
        CargarSucursales()
        CargarEmpresas()
        function CargarEmpresas() {

            $.ajax({
                method: "POST",
                url: "Sucursales.aspx/CargarEmpresas",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let Empresa = respuesta.d
                    if (Empresa != null && Empresa != '[]') {
                        let emp = JSON.parse(Empresa)

                        let ddlEmpresa = ''
                        emp.forEach(element => {
                            ddlEmpresa += `<option value="${element.id}">${element.descripcion}</option>`
                        })

                        document.getElementById('ddlEmpresa').innerHTML += ddlEmpresa
                    }
                }
            });
        }

        function CargarSucursales() {

            $.ajax({
                method: "POST",
                url: "Sucursales.aspx/CargarSucursales",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let sucursales = respuesta.d
                    if (sucursales != null && sucursales != '[]') {
                        let suc = JSON.parse(sucursales)

                        let phSucursales = ''
                        suc.forEach(element => {

                            phSucursales += ` <tr>
                                                <td>${element.nombre}</td>
                                                <td>${element.Direccion}</td>
                                                <td>${element.Estado}</td>
                                                <td>
                                                    <a class="btn btn-success" onclick="vaciarInputs();ModalModificar('${element.Id}','${element.nombre}','${element.Empresa}','${element.Direccion}')"><i class="fa fa-pencil"></i></a>
                                                    <a class="btn btn-danger" onclick="ModalConfirmacion('${element.Id}')"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>`
                        })
                        document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('phSucursales').innerHTML = phSucursales
                    } else {
                        document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('phSucursales').innerHTML = ""
                    }
                }
            });
        }

        function ValAddSucursal() {
            let rta = true

            if (!valNombre() || !valEmpresa() || !valDir()) {
                rta = false
            }

            console.log(rta)
            return rta
        }

        function AddSucursal() {

            if (ValAddSucursal()) {
                document.getElementById('btnAgregar').setAttribute('disabled', 'disabled')
                let nombre = document.getElementById('txtNombre').value
                let empresa = document.getElementById('ddlEmpresa').value
                let dir = document.getElementById('txtDir').value

                $.ajax({
                    method: "POST",
                    url: "Sucursales.aspx/AddSucursal",
                    data: "{nombre: '" + nombre + "', dir: '" + dir + "', id_emp: '" + empresa + "'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        console.log(respuesta.d)
                        let res = respuesta.d

                        if (res == '1') {
                            toastr.success('Se agrego correctamente la Empresa!', 'Felicitaciones')
                            CargarSucursales()
                            $('#modalABM').modal('hide')
                        } else {
                            toastr.warning('No se pudo agregar la Empresa!', 'Atencion')
                            document.getElementById('btnAgregar').removeAttribute('disabled')
                        }

                    }
                });
            }
        }

        function ModalAgregar() {
            document.getElementById('btnAgregar').removeAttribute('disabled')
            document.getElementById('btnAgregar').className = 'btn btn-primary'
            document.getElementById('btnModificar').className = 'hideBtn'
            document.getElementById('titleModal').innerHTML = 'Agregar'
            $('#modalABM').modal('show')
        }

        function ModalModificar(id, rS, emp, dir) {
            document.getElementById('btnModificar').removeAttribute('disabled')
            document.getElementById('btnAgregar').className = 'hideBtn'
            document.getElementById('btnModificar').className = 'btn btn-primary'
            document.getElementById('titleModal').innerHTML = 'Modificar'
            document.getElementById('<%=hiddenEditar.ClientID%>').value = id
            CargarModalABM(rS, emp, dir)
            $('#modalABM').modal('show')
        }

        function ModalConfirmacion(id) {
            document.getElementById('btnEliminar').removeAttribute('disabled')
            document.getElementById('<%=hiddenID.ClientID%>').value = id
            $('#modalConfirmacion').modal('show')
        }

        function CargarModalABM(rS, emp, dir) {
            document.getElementById('txtNombre').value = rS
            document.getElementById('ddlEmpresa').value = emp
            document.getElementById('txtDir').value = dir
        }

        function ChangeSucursal() {
            if (ValAddSucursal()) {
                document.getElementById('btnModificar').setAttribute('disabled', 'disabled')

                let id = document.getElementById('<%=hiddenEditar.ClientID%>').value
                let nombre = document.getElementById('txtNombre').value
                let empresa = document.getElementById('ddlEmpresa').value
                let dir = document.getElementById('txtDir').value

                $.ajax({
                    method: "POST",
                    url: "Sucursales.aspx/ChangeSucursal",
                    data: "{id: '" + id + "',nombre: '" + nombre + "', id_emp: '" + empresa + "',dir: '" + dir + "'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        console.log(respuesta.d)
                        let res = respuesta.d

                        if (res == '1') {
                            toastr.success('Se modifico correctamente la Empresa!', 'Felicitaciones')
                            CargarSucursales()
                            $('#modalABM').modal('hide')
                        } else {
                            toastr.warning('No se pudo modificar la Empresa!', 'Atencion')
                            document.getElementById('btnModificar').removeAttribute('disabled')
                        }

                    }
                });
            }
        }

        function RemoveSucursal() {

            document.getElementById('btnEliminar').setAttribute('disabled', 'disabled')

            let id = document.getElementById('<%=hiddenID.ClientID%>').value

            $.ajax({
                method: "POST",
                url: "Sucursales.aspx/RemoveSucursal",
                data: "{id: '" + id + "'}",
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    let res = respuesta.d

                    if (res == '1') {
                        toastr.success('Se elimino correctamente la Empresa!', 'Felicitaciones')
                        CargarSucursales()
                        $('#modalConfirmacion').modal('hide')
                    } else {
                        toastr.warning('No se pudo eliminar la Empresa!', 'Atencion')
                        document.getElementById('btnEliminar').removeAttribute('disabled')
                    }

                }
            });
        }
    </script>

    <script>
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
        function valEmpresa() {
            let empresa = document.getElementById('ddlEmpresa').value
            document.getElementById('valEmpresa').className = 'hideValid'

            if (empresa == 0) {
                document.getElementById('valEmpresa').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valDir() {
            let dir = document.getElementById('txtDir').value
            document.getElementById('valDir').className = 'hideValid'

            if (dir == '') {
                document.getElementById('valDir').className = 'valid'
                return false
            } else {
                return true
            }
        }
    </script>

    <script>
        function vaciarInputs() {
            document.getElementById('txtNombre').value = ''
            document.getElementById('ddlEmpresa').value = 0
            document.getElementById('txtDir').value = ''
        }
    </script>
</asp:Content>
