<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Proveedores.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.Proveedores" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper wrapper-content">
        <div class="row wrapper border-bottom white-bg page-heading" style="padding-bottom: 0px;">
            <div class="panel blank-panel">
                <div class="panel-heading">

                    <div class="panel-options">

                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#tab-1">Datos generales</a></li>
                            <li class=""><a data-toggle="tab" href="#tab-2">Direcciones</a></li>
                            <li class=""><a data-toggle="tab" href="#tab-3">Telefonos</a></li>
                        </ul>
                    </div>
                </div>

                <div class="panel-body" style="padding-bottom: 0px;">

                    <div class="tab-content">
                        <div id="tab-1" class="tab-pane active">
                            <div class="ibox float-e-margins">
                                <div class="">
                                    <div class="">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Codigo Proveedor</label>

                                                <div class="col-sm-4">
                                                    <input type="text" placeholder="Ingrese Codigo" class="form-control">
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Razon social</label>

                                                <div class="col-sm-4">
                                                    <input type="text" placeholder="Ingrese Razon social" class="form-control">
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Alias</label>

                                                <div class="col-sm-4">
                                                    <input type="text" placeholder="Ingrese Alias" class="form-control">
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Pais</label>

                                                <div class="col-sm-4">
                                                    <select class="form-control" name="account">
                                                        <option>Seleccione</option>
                                                        <option>Argentina</option>
                                                        <option>Brasil</option>
                                                        <option>Estados Unidos</option>
                                                        <option>Mexico</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Tipo Documento</label>

                                                <div class="col-sm-4">
                                                     <select class="form-control" disabled name="account">
                                                        <option>Seleccione</option>
                                                        <option>CUIT</option>

                                                    </select>
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Numero</label>

                                                <div class="col-sm-4">
                                                    <input type="text" placeholder="Ingrese numero" class="form-control">
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Codigo Proveedor</label>

                                                <div class="col-sm-4">
                                                    <input type="text" placeholder="Ingrese Codigo" class="form-control">
                                                </div>
                                            </div>
                                            <div class="hr-line-dashed"></div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Estado</label>

                                                <div class="col-sm-4">
                                                    <select class="form-control m-b" name="account">
                                                        <option>Inactivo</option>
                                                        <option>Activo</option>

                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-4 col-sm-offset-2">
                                                    <button class="btn btn-primary" type="submit">Guardar</button>
                                                    <button class="btn btn-danger" type="submit">Cancelar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="tab-2" class="tab-pane">
                            <strong>Donec quam felis</strong>

                            <p>a confirmar panel 2</p>
                        </div>
                        <div id="tab-3" class="tab-pane">
                            <strong>Donec quam felis</strong>

                            <p>A confirmar panel 3</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
