<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Empresas.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.Empresas" %>

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
                        <h5>Empresas</h5>
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
                                        <th>Razon Social</th>
                                        <th>Cuit</th>
                                        <th>Ingresos Brutos</th>
                                        <th>Fecha de Inicio</th>
                                        <th>Condicion IVA</th>
                                        <th>Direccion</th>
                                        <th>Alias</th>
                                        <th>CBU</th>
                                    </tr>
                                </thead>
                                <tbody id="phEmpresas">
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
                        <label class="col-md-3">Razon social</label>
                        <div class="col-md-9">
                            <input id="txtRazonSocial" class="form-control" onchange="valRazonSocial()" />
                            <p id="valRazonSocial" class="valid hideValid">*Completar Razon Social</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">Cuit</label>
                        <div class="col-md-9">
                            <input id="txtCuit" class="form-control" type="number" onchange="valCuit()" />
                            <p id="valCuit" class="hideValid">*Completar Cuit</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">Ingresos Brutos</label>
                        <div class="col-md-9">
                            <input id="txtIngBrutos" class="form-control" onchange="valIngBrutos()" />
                            <p id="valIngBrutos" class="hideValid">*Completar Ingresos Brutos</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">Condicion IVA</label>
                        <div class="col-md-9">
                            <select class="form-control" id="ddlCondIVA" onchange="valCondIVA()">
                                <option value="0">Seleccione</option>
                            </select>
                            <p id="valCondIVA" class="hideValid">*Seleccione Condicion IVA</p>
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
                        <label class="col-md-3">Alias</label>
                        <div class="col-md-9">
                            <input id="txtAlias" class="form-control" onchange="valAlias()" />
                            <p id="valAlias" class="hideValid">*Completar Alias</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">CBU</label>
                        <div class="col-md-9">
                            <input id="txtCBU" class="form-control" onchange="valCBU()" />
                            <p id="valCBU" class="hideValid">*Completar CBU</p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnAgregar" class="hideBtn btn btn-primary" onclick="AddEmpresa()"><i class="fa fa-check"></i>&nbsp;Agregar</a>
                    <a id="btnModificar" class="hideBtn btn btn-primary" onclick="ChangeEmpresa()"><i class="fa fa-check"></i>&nbsp;Modificar</a>
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
                    <a id="btnEliminar" class="btn btn-danger" onclick="RemoveEmpresa()">Eliminar</a>
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>


    <script>  
        CargarEmpresas()
        CargarCondicion_IVA()
        function CargarCondicion_IVA() {

            $.ajax({
                method: "POST",
                url: "Empresas.aspx/CargarCondicion_IVA",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let CondIVA = respuesta.d
                    if (CondIVA != null && CondIVA != '[]') {
                        let cIVA = JSON.parse(CondIVA)

                        let ddlCondIVA = ''
                        cIVA.forEach(element => {
                            ddlCondIVA += `<option value="${element.id}">${element.descripcion}</option>`
                        })

                        document.getElementById('ddlCondIVA').innerHTML += ddlCondIVA
                    }
                }
            });
        }

        function CargarEmpresas() {

            $.ajax({
                method: "POST",
                url: "Empresas.aspx/CargarEmpresas",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let empresas = respuesta.d
                    if (empresas != null && empresas != '[]') {
                        let emp = JSON.parse(empresas)

                        let phEmpresas = ''
                        emp.forEach(element => {

                            phEmpresas += ` <tr>
                                                <td>${element.Razon_Social}</td>
                                                <td>${element.Cuit}</td>
                                                <td>${element.Ingresos_Brutos}</td>
                                                <td>${element.Fecha_inicio}</td>
                                                <td>${element.Condicion_IVA}</td>
                                                <td>${element.Direccion}</td>
                                                <td>${element.Alias}</td>
                                                <td>${element.CBU}</td>
                                                <td>
                                                    <a class="btn btn-success" onclick="vaciarInputs();ModalModificar('${element.Id}','${element.Razon_Social}','${element.Cuit}','${element.Ingresos_Brutos}','${element.idCondicion_IVA}','${element.Direccion}','${element.Alias}','${element.CBU}')"><i class="fa fa-pencil"></i></a>
                                                    <a class="btn btn-danger" onclick="ModalConfirmacion('${element.Id}')"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>`
                        })
                        document.getElementById('Progressbars').className ='hideBar'
                        document.getElementById('phEmpresas').innerHTML = phEmpresas
                    }
                }
            });
        }

        function ValAddEmpres() {
            let rta = true

            if (!valRazonSocial() || !valCuit() || !valIngBrutos() || !valCondIVA() || !valDir() || !valAlias() || !valCBU()) {
                rta = false
            }

            console.log(rta)
            return rta
        }

        function AddEmpresa() {

            if (ValAddEmpres()) {
                document.getElementById('btnAgregar').setAttribute('disabled', 'disabled')
                let razonSocial = document.getElementById('txtRazonSocial').value
                let cuit = document.getElementById('txtCuit').value
                let ingBrutos = document.getElementById('txtIngBrutos').value
                let condIVA = document.getElementById('ddlCondIVA').value
                let dir = document.getElementById('txtDir').value
                let alias = document.getElementById('txtAlias').value
                let cbu = document.getElementById('txtCBU').value

                $.ajax({
                    method: "POST",
                    url: "Empresas.aspx/AddEmpresas",
                    data: "{razonSocial: '" + razonSocial + "', cuit: '" + cuit + "', ingBrutos: '" + ingBrutos + "',condIVA: '" + condIVA + "',dir: '" + dir + "', alias: '" + alias + "', cbu: '" + cbu + "'}",
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
                            CargarEmpresas()
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

        function ModalModificar(id, rS, cuit, iB, cIVA, dir, alias, cbu) {
            document.getElementById('btnModificar').removeAttribute('disabled')
            document.getElementById('btnAgregar').className = 'hideBtn'
            document.getElementById('btnModificar').className = 'btn btn-primary'
            document.getElementById('titleModal').innerHTML = 'Modificar'
            document.getElementById('<%=hiddenEditar.ClientID%>').value = id
            CargarModalABM(rS, cuit, iB, cIVA, dir, alias, cbu)
            $('#modalABM').modal('show')
        }

        function ModalConfirmacion(id) {
            document.getElementById('btnEliminar').removeAttribute('disabled')
            document.getElementById('<%=hiddenID.ClientID%>').value = id
            $('#modalConfirmacion').modal('show')
        }

        function CargarModalABM(rS, cuit, iB, cIVA, dir, alias, cbu) {
            document.getElementById('txtRazonSocial').value = rS
            document.getElementById('txtCuit').value = cuit
            document.getElementById('txtIngBrutos').value = iB
            document.getElementById('ddlCondIVA').value = cIVA
            document.getElementById('txtDir').value = dir
            document.getElementById('txtAlias').value = alias
            document.getElementById('txtCBU').value = cbu
        }

        function ChangeEmpresa() {
            if (ValAddEmpres()) {
                document.getElementById('btnModificar').setAttribute('disabled', 'disabled')

                let id = document.getElementById('<%=hiddenEditar.ClientID%>').value
                let razonSocial = document.getElementById('txtRazonSocial').value
                let cuit = document.getElementById('txtCuit').value
                let ingBrutos = document.getElementById('txtIngBrutos').value
                let condIVA = document.getElementById('ddlCondIVA').value
                let dir = document.getElementById('txtDir').value
                let alias = document.getElementById('txtAlias').value
                let cbu = document.getElementById('txtCBU').value

                $.ajax({
                    method: "POST",
                    url: "Empresas.aspx/ChangeEmpresas",
                    data: "{id: '" + id + "',razonSocial: '" + razonSocial + "', cuit: '" + cuit + "', ingBrutos: '" + ingBrutos + "',condIVA: '" + condIVA + "',dir: '" + dir + "', alias: '" + alias + "', cbu: '" + cbu + "'}",
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
                            CargarEmpresas()
                            $('#modalABM').modal('hide')
                        } else {
                            toastr.warning('No se pudo modificar la Empresa!', 'Atencion')
                            document.getElementById('btnModificar').removeAttribute('disabled')
                        }

                    }
                });
            }
        }

        function RemoveEmpresa() {

            document.getElementById('btnEliminar').setAttribute('disabled', 'disabled')

            let id = document.getElementById('<%=hiddenID.ClientID%>').value

            $.ajax({
                method: "POST",
                url: "Empresas.aspx/RemoveEmpresa",
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
                        CargarEmpresas()
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
        function valRazonSocial() {
            let razonSocial = document.getElementById('txtRazonSocial').value
            document.getElementById('valRazonSocial').className = 'hideValid'

            if (razonSocial == '') {
                document.getElementById('valRazonSocial').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valCuit() {
            let cuit = document.getElementById('txtCuit').value
            document.getElementById('valCuit').className = 'hideValid'

            if (cuit == '') {
                document.getElementById('valCuit').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valIngBrutos() {
            let ingBrutos = document.getElementById('txtIngBrutos').value
            document.getElementById('valIngBrutos').className = 'hideValid'

            if (ingBrutos == '') {
                document.getElementById('valIngBrutos').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valCondIVA() {
            let condIVA = document.getElementById('ddlCondIVA').value
            document.getElementById('valCondIVA').className = 'hideValid'

            if (condIVA == 0) {
                document.getElementById('valCondIVA').className = 'valid'
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
        function valAlias() {
            let alias = document.getElementById('txtAlias').value
            document.getElementById('valAlias').className = 'hideValid'

            if (alias == '') {
                document.getElementById('valAlias').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valCBU() {
            let cbu = document.getElementById('txtCBU').value
            document.getElementById('valCBU').className = 'hideValid'

            if (cbu == '') {
                document.getElementById('valCBU').className = 'valid'
                return false
            } else {
                return true
            }
        }
    </script>

    <script>
        function vaciarInputs() {
            document.getElementById('txtRazonSocial').value = ''
            document.getElementById('txtCuit').value = ''
            document.getElementById('txtIngBrutos').value = ''
            document.getElementById('ddlCondIVA').value = 0
            document.getElementById('txtDir').value = ''
            document.getElementById('txtAlias').value = ''
            document.getElementById('txtCBU').value = ''
        }
    </script>
</asp:Content>
