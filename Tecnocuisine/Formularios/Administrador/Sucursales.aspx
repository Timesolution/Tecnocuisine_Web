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


                                    <input type="text" id="txtBusqueda" placeholder="Búsqueda..." class="form-control" style="width: 90%" />
                                </div>
                            </div>
                            <div class="col-md-2">

                                <a onclick="ModalAgregar()" class="btn btn-primary dim"
                                    style="margin-right: 1%; float: right"><i class='fa fa-plus' title="Agregar sucursales"></i>
                                </a>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="updatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="">
                                    <table class="table table-striped table-bordered table-hover" id="editable">
                                        <thead>
                                            <tr>
                                                <th>Nombre</th>
                                                <th>Dirección</th>
                                                <th>Estado</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="phSucursales">
                                            <asp:PlaceHolder ID="phSucursalesPH" runat="server"></asp:PlaceHolder>
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
                    <a id="btnEliminar" class="btn btn-danger" onclick="RemoveSucursal()">Eliminar</a>
                    <asp:HiddenField ID="hiddenID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script>  
        //CargarSucursales()
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
            vaciarInputs()
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
