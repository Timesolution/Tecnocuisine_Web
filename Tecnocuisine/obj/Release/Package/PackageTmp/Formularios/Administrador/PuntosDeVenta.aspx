<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PuntosDeVenta.aspx.cs" Inherits="Tecnocuisine.Formularios.Administrador.PuntosDeVenta" %>

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
                                                    </div>
                                                </div>
                        <asp:UpdatePanel ID="updatePanel1" runat="server">
                            <ContentTemplate>



                                <div class="">
                                    <table class="table table-striped table-bordered table-hover" id="editable">
                                        <thead>
                                            <tr>
                                                <th>Punto de Venta</th>
                                                <th>Forma Facturar</th>
                                                <th>Retiene Ingresos Brutos</th>
                                                <th>Retiene Ganancias</th>
                                                <th>Nombre Fantasia</th>
                                                <th>Dirección</th>
                                                <th>Empresa</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="phPtsDeVta">
                                            <asp:PlaceHolder ID="PuntoDeVentaPH" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
   
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalABM" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="titleModal"></h4>
                </div>
                <div class="modal-body">

                    <div class="row m-b">
                        <label class="col-md-4">Sucursal</label>
                        <div class="col-md-8">
                            <select class="form-control" id="ddlSucursal" onchange="valSucursal();CargarEmpresas()">
                                <option value="0">Seleccione</option>
                            </select>
                            <p id="valSucursal" class="hideValid">*Seleccione Sucursal</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">Empresa</label>
                        <div class="col-md-8">
                            <select class="form-control" id="ddlEmpresa" onchange="valEmpresa();GetLastPuntoVta()">
                                <option value="0">Seleccione</option>
                            </select>
                            <p id="valEmpresa" class="hideValid">*Seleccione Empresa</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">Punto de Venta</label>
                        <div class="col-md-8">
                            <input id="txtPuntoDeVenta" class="form-control" onchange="valPuntoDeVenta()" disabled="disabled"/>
                            <p id="valPuntoDeVenta" class="valid hideValid">*Completar Punto de Venta</p>
                        </div>
                    </div>
                    
                    <div class="row m-b">
                        <label class="col-md-4">Forma de Facturar</label>
                        <div class="col-md-8">
                            <select class="form-control" id="ddlFormaDeFacturar" onchange="valFormaDeFacturar()">
                                <option value="0">Seleccione</option>
                                <option value="1">Electronica</option>
                                <option value="2">Preimpresa</option>
                                <option value="3">Fiscal</option>
                            </select>
                            <p id="valFormaDeFacturar" class="hideValid">*Seleccione Forma de Facturar</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">Moneda Facturacion</label>
                        <div class="col-md-8">
                            <select class="form-control" id="ddlMonedaFacturacion" onchange="valMonedaFacturacion()">
                                <option value="0">Seleccione</option>
                                <option value="1">Peso</option>
                                <option value="2">Dolar Blue</option>
                                <option value="3">Euro</option>
                                <option value="6">Dolar</option>
                                <option value="7">Dolar Bison</option>
                                <option value="8">Dolar SP</option>
                                <option value="9">MONKEY</option>
                            </select>
                            <p id="valMonedaFacturacion" class="hideValid">*Seleccione Moneda Facturacion</p>
                        </div>
                    </div>
                    
                    <div class="row m-b">
                        <label class="col-md-4">Retiene Ganancias</label>
                        <div class="col-md-8">
                            <select class="form-control" id="ddlRetieneGanancias" onchange="valRetieneGanancias()">
                                <option value="-1">Seleccione</option>
                                <option value="0">No</option>
                                <option value="1">Si</option>
                            </select>
                            <p id="valRetieneGanancias" class="hideValid">*Seleccione Retiene Ganancias</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">Retiene Ingresos Brutos</label>
                        <div class="col-md-8">
                            <select class="form-control" id="ddlRetieneIngresosBrutos" onchange="valRetieneIngresosBrutos()">
                                <option value="-1">Seleccione</option>
                                <option value="0">No</option>
                                <option value="1">Si</option>
                            </select>
                            <p id="valRetieneIngresosBrutos" class="hideValid">*Seleccione Retiene Ingresos Brutos</p>
                        </div>
                    </div>
                    
                    <div class="row m-b">
                        <label class="col-md-4">Nombre de Fantasia</label>
                        <div class="col-md-8">
                            <input id="txtNombre" class="form-control" onchange="valNombre()" />
                            <p id="valNombre" class="valid hideValid">*Completar Nombre de Fantasia</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">Direccion</label>
                        <div class="col-md-8">
                            <input id="txtDir" class="form-control" onchange="valDir()" />
                            <p id="valDir" class="hideValid">*Completar Direccion</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">CAI Remito</label>
                        <div class="col-md-8">
                            <input id="txtCAIRemito" class="form-control" onchange="valCAIRemito()" />
                            <p id="valCAIRemito" class="hideValid">*Completar CAI Remito</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-4">CAI Vencimiento</label>
                        <div class="col-md-8">
                            <input type="date" id="txtCAIVencimiento" class="form-control" onchange="valCAIVencimiento()" />
                            <p id="valCAIVencimiento" class="hideValid">*Completar CAI Vencimiento</p>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <a id="btnAgregar" class="hideBtn btn btn-primary" onclick="AddPuntoDeVenta()"><i class="fa fa-check"></i>&nbsp;Agregar</a>
                    <a id="btnModificar" class="hideBtn btn btn-primary" onclick="ChangePuntoDeVenta()"><i class="fa fa-check"></i>&nbsp;Modificar</a>
                    <asp:HiddenField ID="hiddenEditar" runat="server" />
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancelar</button>

                </div>
            </div>
        </div>
    </div>

    <div id="modalConfirmacion" class="modal fade" tabindex="-1" role="dialog">
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
                    <a id="btnEliminar" class="btn btn-danger" onclick="RemovePtsDeVta()">Eliminar</a>
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script>  
        //CargarPuntoDeVenta()
        CargarSucursales()
        function CargarSucursales() {

            $.ajax({
                method: "POST",
                url: "PuntosDeVenta.aspx/CargarSucursales",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {

                    let Sucursal = respuesta.d
                    if (Sucursal != null && Sucursal != '[]') {
                        let suc = JSON.parse(Sucursal)

                        let ddlSucursal = '<option value="0">Seleccione</option>'
                        suc.forEach(element => {
                            ddlSucursal += `<option value="${element.Id}">${element.nombre}</option>`
                        })

                        document.getElementById('ddlSucursal').innerHTML = ddlSucursal
                    }
                }
            });
        }
        function CargarEmpresas() {
            let idSuc = document.getElementById('ddlSucursal').value

            if (idSuc != 0) {
                $.ajax({
                    method: "POST",
                    url: "PuntosDeVenta.aspx/CargarEmpresas",
                    data: "{idSuc: '" + idSuc + "'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {

                        let Empresa = respuesta.d
                        if (Empresa != null && Empresa != '[]') {
                            let emp = JSON.parse(Empresa)

                            let ddlEmpresa = '<option value="0">Seleccione</option>'
                            emp.forEach(element => {
                                ddlEmpresa += `<option value="${element.id}">${element.descripcion}</option>`
                            })

                            document.getElementById('ddlEmpresa').innerHTML = ddlEmpresa
                        }
                    }
                });
            }
        }

        function CargarEmpresasModalModif(idEmpresa) {
            let idSuc = document.getElementById('ddlSucursal').value

            if (idSuc != 0) {
                $.ajax({
                    method: "POST",
                    url: "PuntosDeVenta.aspx/CargarEmpresas",
                    data: "{idSuc: '" + idSuc + "'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {

                        let Empresa = respuesta.d
                        if (Empresa != null && Empresa != '[]') {
                            let emp = JSON.parse(Empresa)

                            let ddlEmpresa = '<option value="0">Seleccione</option>'
                            emp.forEach(element => {
                                ddlEmpresa += `<option value="${element.id}">${element.descripcion}</option>`
                            })

                            document.getElementById('ddlEmpresa').innerHTML = ddlEmpresa
                            document.getElementById('ddlEmpresa').value = idEmpresa
                        }
                    }
                });
            }
        }

        function CargarPuntoDeVenta() {

            $.ajax({
                method: "POST",
                url: "PuntosDeVenta.aspx/CargarPuntoDeVenta",
                data: null,
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    console.log(respuesta.d)

                    let sucursales = respuesta.d
                    if (sucursales != null && sucursales != '[]') {
                        let suc = JSON.parse(sucursales)

                        let phPtsDeVta = ''
                        suc.forEach(element => {

                            let RetieneGanancias = ""
                            if (element.retieneGanancias == "True" ) {
                                RetieneGanancias = "Si"
                            } else {
                                RetieneGanancias = "No"
                            }

                            let RetieneIngresosBrutos = ""
                            if (element.retieneIngresosBrutos == "True") {
                                RetieneIngresosBrutos = "Si"
                            } else {
                                RetieneIngresosBrutos = "No"
                            }

                            let FormaFacturar = ""
                            if (element.formaDeFacturar == "1") {
                                FormaFacturar = "Electronica"
                            } else if (element.formaDeFacturar == "2") {
                                FormaFacturar = "Preimpresa"
                            } else if (element.formaDeFacturar == "3") {
                                FormaFacturar = "Fiscal"
                            }

                            phPtsDeVta += ` <tr>
                                                <td>${element.puntoDeVenta}</td>
                                                <td>${FormaFacturar}</td>
                                                <td>${RetieneIngresosBrutos}</td>
                                                <td>${RetieneGanancias}</td>
                                                <td>${element.nombre}</td>
                                                <td>${element.Direccion}</td>
                                                <td>${element.Empresa}</td>
                                                <td>
                                                    <a class="btn btn-success" onclick="vaciarInputs();ModalModificar('${element.Id}','${element.nombre}','${element.Empresa}','${element.Direccion}','${element.puntoDeVenta}','${element.formaDeFacturar}','${element.retieneIngresosBrutos}','${element.retieneGanancias}','${element.CAIRemito}','${element.CAIVencimiento}','${element.monedaFacturacion}','${element.idSuc}')"><i class="fa fa-pencil"></i></a>
                                                    <a class="btn btn-danger" onclick="ModalConfirmacion('${element.Id}')"><i class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>`
                        })
                        document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('phPtsDeVta').innerHTML = phPtsDeVta
                    } else {
                        document.getElementById('Progressbars').className = 'hideBar'
                        document.getElementById('phPtsDeVta').innerHTML = ""
                    }
                }
            });
        }

        function ValAddPuntoDeVenta() {
            let rta = true

            valNombre()
            valEmpresa()
            valSucursal()
            valDir()
            valFormaDeFacturar()
            valMonedaFacturacion()
            valRetieneGanancias()
            valCAIRemito()
            valRetieneIngresosBrutos()
            valCAIVencimiento()
            if (!valNombre() || !valEmpresa() || !valDir() || !valFormaDeFacturar() || !valMonedaFacturacion() || !valRetieneGanancias() || !valRetieneIngresosBrutos() || !valSucursal())
            {
                rta = false
            }

            console.log(rta)
            return rta
        }

        function AddPuntoDeVenta() {

            if (ValAddPuntoDeVenta()) {
                document.getElementById('btnAgregar').setAttribute('disabled', 'disabled')
                let nombre = document.getElementById('txtNombre').value
                let empresa = document.getElementById('ddlEmpresa').value
                let dir = document.getElementById('txtDir').value
                let CAIRemito = document.getElementById('txtCAIRemito').value
                let CAIVencimiento = document.getElementById('txtCAIVencimiento').value
                let formaDeFacturar = document.getElementById('ddlFormaDeFacturar').value
                let monedaFacturacion = document.getElementById('ddlMonedaFacturacion').value
                let retieneGanancias = document.getElementById('ddlRetieneGanancias').value
                let retieneIngresosBrutos = document.getElementById('ddlRetieneIngresosBrutos').value
                let puntoDeVenta = document.getElementById('txtPuntoDeVenta').value
                let idSuc = document.getElementById('ddlSucursal').value

                $.ajax({
                    method: "POST",
                    url: "PuntosDeVenta.aspx/AddPuntoDeVenta",
                    data: "{nombre: '" + nombre + "',empresa: '" + empresa + "', dir: '" + dir + "', CAIRemito: '" + CAIRemito + "', CAIVencimiento: '" + CAIVencimiento + "', formaDeFacturar: '" + formaDeFacturar + "',monedaFacturacion: '" + monedaFacturacion + "', retieneGanancias: '" + retieneGanancias + "', retieneIngresosBrutos: '" + retieneIngresosBrutos + "', puntoDeVenta: '" + puntoDeVenta + "', idSuc: '" + idSuc+"'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        console.log(respuesta.d)
                        let res = respuesta.d

                        if (res == '1') {
                            toastr.success('Se agrego correctamente el Punto de Venta!', 'Felicitaciones')
                            CargarPuntoDeVenta()
                            $('#modalABM').modal('hide')
                        } else {
                            toastr.warning('No se pudo agregar el Punto de Venta!', 'Atencion')
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

        function ModalModificar(id, nombre, Empresa, Direccion, puntoDeVenta, FormaFacturar, RetieneIngresosBrutos, RetieneGanancias, CAIRemito, CAIVencimiento, monedaFacturacion, idSuc) {

            document.getElementById('btnModificar').removeAttribute('disabled')
            document.getElementById('btnAgregar').className = 'hideBtn'
            document.getElementById('btnModificar').className = 'btn btn-primary'
            document.getElementById('titleModal').innerHTML = 'Modificar'
            document.getElementById('<%=hiddenEditar.ClientID%>').value = id
            CargarModalABM(nombre, Empresa, Direccion, puntoDeVenta, FormaFacturar, RetieneIngresosBrutos, RetieneGanancias, CAIRemito, CAIVencimiento, monedaFacturacion, idSuc)
            $('#modalABM').modal('show')
        }

        function ModalConfirmacion(id) {
            document.getElementById('btnEliminar').removeAttribute('disabled')
            document.getElementById('<%=hiddenID.ClientID%>').value = id
            $('#modalConfirmacion').modal('show')
        }

        function CargarModalABM(nombre, Empresa, Direccion, puntoDeVenta, FormaFacturar, RetieneIngresosBrutos, RetieneGanancias, CAIRemito, CAIVencimiento, monedaFacturacion, idSuc) {
            document.getElementById('ddlSucursal').value = idSuc
            CargarEmpresasModalModif(Empresa)
            document.getElementById('txtNombre').value = nombre
            document.getElementById('txtDir').value = Direccion
            document.getElementById('txtPuntoDeVenta').value = puntoDeVenta
            document.getElementById('ddlFormaDeFacturar').value = FormaFacturar
            document.getElementById('ddlRetieneIngresosBrutos').value = RetieneIngresosBrutos == "True" ? 1 : 0
            document.getElementById('ddlRetieneGanancias').value = RetieneGanancias == "True" ? 1 : 0
            document.getElementById('txtCAIRemito').value = CAIRemito
            let fecha = CAIVencimiento.split(' ')[0].split('/')
            let dd = fecha[0] < 10 ? '0' + fecha[0] : fecha[0]
            let MM = fecha[1] < 10 ? '0' + fecha[1] : fecha[1]
            let yyyy = fecha[2]
            document.getElementById('txtCAIVencimiento').value = yyyy + '-' + MM + '-' + dd
            document.getElementById('ddlMonedaFacturacion').value = monedaFacturacion
        }

        function ChangePuntoDeVenta() {
            if (ValAddPuntoDeVenta()) {
                document.getElementById('btnModificar').setAttribute('disabled', 'disabled')

                let id = document.getElementById('<%=hiddenEditar.ClientID%>').value
                let nombre = document.getElementById('txtNombre').value
                let empresa = document.getElementById('ddlEmpresa').value
                let dir = document.getElementById('txtDir').value
                let CAIRemito = document.getElementById('txtCAIRemito').value
                let CAIVencimiento = document.getElementById('txtCAIVencimiento').value
                let formaDeFacturar = document.getElementById('ddlFormaDeFacturar').value
                let monedaFacturacion = document.getElementById('ddlMonedaFacturacion').value
                let retieneGanancias = document.getElementById('ddlRetieneGanancias').value
                let retieneIngresosBrutos = document.getElementById('ddlRetieneIngresosBrutos').value
                let puntoDeVenta = document.getElementById('txtPuntoDeVenta').value
                let idSuc = document.getElementById('ddlSucursal').value

                $.ajax({
                    method: "POST",
                    url: "PuntosDeVenta.aspx/ChangePuntoDeVenta",
                    data: "{id: '" + id + "',nombre: '" + nombre + "', id_emp: '" + empresa + "',dir: '" + dir + "', CAIRemito: '" + CAIRemito + "', CAIVencimiento: '" + CAIVencimiento + "', formaDeFacturar: '" + formaDeFacturar + "',monedaFacturacion: '" + monedaFacturacion + "', retieneGanancias: '" + retieneGanancias + "', retieneIngresosBrutos: '" + retieneIngresosBrutos + "', puntoDeVenta: '" + puntoDeVenta + "', idSuc: '" + idSuc+"'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        console.log(respuesta.d)
                        let res = respuesta.d

                        if (res == '1') {
                            toastr.success('Se modifico correctamente el Punto de Venta!', 'Felicitaciones')
                            CargarPuntoDeVenta()
                            $('#modalABM').modal('hide')
                        } else {
                            toastr.warning('No se pudo modificar el Punto de Venta!', 'Atencion')
                            document.getElementById('btnModificar').removeAttribute('disabled')
                        }

                    }
                });
            }
        }

        function RemovePtsDeVta() {

            document.getElementById('btnEliminar').setAttribute('disabled', 'disabled')

            let id = document.getElementById('<%=hiddenID.ClientID%>').value

            $.ajax({
                method: "POST",
                url: "PuntosDeVenta.aspx/RemovePtsDeVta",
                data: "{id: '" + id + "'}",
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    console.log(JSON.stringify(error));
                },
                success: (respuesta) => {
                    let res = respuesta.d

                    if (res == '1') {
                        toastr.success('Se elimino correctamente el Punto de Venta!', 'Felicitaciones')
                        CargarPuntoDeVenta()
                        $('#modalConfirmacion').modal('hide')
                    } else {
                        toastr.warning('No se pudo eliminar el Punto de Venta!', 'Atencion')
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
        function valCAIRemito() {
            let CAIRemito = document.getElementById('txtCAIRemito').value
            document.getElementById('valCAIRemito').className = 'hideValid'

            if (CAIRemito == '') {
                document.getElementById('valCAIRemito').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valCAIVencimiento() {
            let CAIVencimiento = document.getElementById('txtCAIVencimiento').value
            document.getElementById('valCAIVencimiento').className = 'hideValid'

            if (CAIVencimiento == '') {
                document.getElementById('valCAIVencimiento').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valSucursal() {
            let empresa = document.getElementById('ddlSucursal').value
            document.getElementById('valSucursal').className = 'hideValid'

            if (empresa == 0) {
                document.getElementById('valSucursal').className = 'valid'
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
        function valFormaDeFacturar() {
            let formaDeFacturar = document.getElementById('ddlFormaDeFacturar').value
            document.getElementById('valFormaDeFacturar').className = 'hideValid'

            if (formaDeFacturar == 0) {
                document.getElementById('valFormaDeFacturar').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valMonedaFacturacion() {
            let monedaFacturacion  = document.getElementById('ddlMonedaFacturacion').value
            document.getElementById('valMonedaFacturacion').className = 'hideValid'

            if (monedaFacturacion  == 0) {
                document.getElementById('valMonedaFacturacion').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valRetieneGanancias() {
            let retieneGanancias = document.getElementById('ddlRetieneGanancias').value
            document.getElementById('valRetieneGanancias').className = 'hideValid'

            if (retieneGanancias == -1) {
                document.getElementById('valRetieneGanancias').className = 'valid'
                return false
            } else {
                return true
            }
        }
        function valRetieneIngresosBrutos() {
            let retieneIngresosBrutos = document.getElementById('ddlRetieneIngresosBrutos').value
            document.getElementById('valRetieneIngresosBrutos').className = 'hideValid'

            if (retieneIngresosBrutos == -1) {
                document.getElementById('valRetieneIngresosBrutos').className = 'valid'
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
            document.getElementById('ddlSucursal').value = 0
            document.getElementById('ddlEmpresa').value = 0
            document.getElementById('txtDir').value = ''
            document.getElementById('txtCAIRemito').value = ''
            document.getElementById('txtCAIVencimiento').value=''
            document.getElementById('ddlFormaDeFacturar').value = 0
            document.getElementById('ddlMonedaFacturacion').value = 0
            document.getElementById('ddlRetieneGanancias').value = -1
            document.getElementById('ddlRetieneIngresosBrutos').value = -1
        }
    </script>

    <script>
        function GetLastPuntoVta() {
            let idSuc = document.getElementById('ddlSucursal').value

            if (idSuc != 0) {
                $.ajax({
                    method: "POST",
                    url: "PuntosDeVenta.aspx/GetLastPuntoVta",
                    data: "{idSuc: '" + idSuc +"'}",
                    contentType: "application/json",
                    dataType: 'json',
                    error: (error) => {
                        console.log(JSON.stringify(error));
                    },
                    success: (respuesta) => {
                        document.getElementById('txtPuntoDeVenta').value = respuesta.d
                    }
                });
            }
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
</asp:Content>
