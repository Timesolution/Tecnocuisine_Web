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

        .hideBar {
            display: none;
        }

        #editable span {
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


                                    <input type="text" id="txtBusqueda" placeholder="Búsqueda..." class="form-control" style="width: 90%" />
                                </div>
                            </div>
                            <div class="col-md-2">

                                <a onclick="ModalAgregar()" class="btn btn-primary dim" title="Agregar empresa"
                                    style="margin-right: 1%; float: right"><i class='fa fa-plus'></i></a>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="">
                                    <table class="table table-striped table-bordered table-hover" id="editable">
                                        <thead>
                                            <tr>
                                                <th>Razón Social</th>
                                                <th>CUIT</th>
                                                <th>Ingresos Brutos</th>
                                                <th>Fecha de Inicio</th>
                                                <th>Condición IVA</th>
                                                <th>Dirección</th>
                                                <th>Alias</th>
                                                <th>CBU</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="phEmpresas">
                                            <asp:PlaceHolder ID="phEmpresas2" runat="server"></asp:PlaceHolder>
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
                        <label class="col-md-3">Razón social</label>
                        <div class="col-md-9">
                            <input id="txtRazonSocial" class="form-control" onchange="valRazonSocial()" />
                            <p id="valRazonSocial" class="valid hideValid">*Completar Razon Social</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">CUIT</label>
                        <div class="col-md-9">
                            <input id="txtCuit" class="form-control" type="text" inputmode="numeric" onchange="valCuit()" />
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
                        <label class="col-md-3">Condición IVA</label>
                        <div class="col-md-9">
                            <select class="form-control" id="ddlCondIVA" onchange="valCondIVA()">
                                <option value="0">Seleccione</option>
                            </select>
                            <p id="valCondIVA" class="hideValid">*Seleccione Condicion IVA</p>
                        </div>
                    </div>
                    <div class="row m-b">
                        <label class="col-md-3">Dirección</label>
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
        //CargarEmpresas()
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
            let ph = document.getElementById('<%=phEmpresas2.ClientID%>')
            console.log(ph + "Place")
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
                                                    <a class="btn btn-xs" style="background-color: transparent; margin-right: 10px;" onclick="vaciarInputs();ModalModificar('${element.Id}','${element.Razon_Social}','${element.Cuit}','${element.Ingresos_Brutos}','${element.idCondicion_IVA}','${element.Direccion}','${element.Alias}','${element.CBU}')"><i style="color: black;" class="fa fa-pencil"></i></a>
                                                    <a class="btn btn-xs" style="background-color: transparent; margin-right: 10px;" onclick="ModalConfirmacion('${element.Id}')"><i style="color: red;" class="fa fa-trash"></i></a>
                                                </td>
                                            </tr>`


                        })
                        //document.getElementById('Progressbars').className ='hideBar'
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
                            $('#modalABM').modal('hide')
                            CargarEmpresas()
                        } else {
                            toastr.warning('No se pudo agregar la Empresa!', 'Atencion')
                            document.getElementById('btnAgregar').removeAttribute('disabled')
                        }

                    }
                });
            }
        }

        function ModalAgregar() {
            vaciarInputs();
            limpiarMensajes(); //esta funcion limpia los mensajes en rojo cuando no se llena un campo
            document.getElementById('btnAgregar').removeAttribute('disabled')
            document.getElementById('btnAgregar').className = 'btn btn-primary'
            document.getElementById('btnModificar').className = 'hideBtn'
            document.getElementById('titleModal').innerHTML = 'Agregar'
            $('#modalABM').modal('show')
        }

        function limpiarMensajes(){
            document.getElementById('valRazonSocial').className = 'hideValid';
            document.getElementById('valCuit').className = 'hideValid';
            document.getElementById('valIngBrutos').className = 'hideValid';
            document.getElementById('valCondIVA').className = 'hideValid';
            document.getElementById('valDir').className = 'hideValid';
            document.getElementById('valAlias').className = 'hideValid';
            document.getElementById('valCBU').className = 'hideValid';
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

            validarCuitSoloNumerosGuion();
            validarIngresosBrutosNumerosPuntoYComa();
            validarCbuSoloNumerosGuion();
        });
    </script>
    <script>
        function validarCuitSoloNumerosGuion(){
            document.getElementById('txtCuit').addEventListener('keydown', function(event) {
                if (!/[0-9]/.test(event.key) && !event.ctrlKey && event.key.length === 1) {
                    event.preventDefault();
                }
            });
        
        }

        function validarIngresosBrutosNumerosPuntoYComa() {
            document.getElementById('txtIngBrutos').addEventListener('keydown', function(event) {
                if (!/[0-9.,]/.test(event.key) && !event.ctrlKey && event.key.length === 1) {
                    event.preventDefault();
                }
            });
        }

        function validarCbuSoloNumerosGuion() {
            document.getElementById('txtCBU').addEventListener('keydown', function(event) {
                if (!/[0-9]/.test(event.key) && !event.ctrlKey && event.key.length === 1) {
                    event.preventDefault();
                }
            });
        }


    </script>

</asp:Content>
