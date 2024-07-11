<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearVenta.aspx.cs" Inherits="Tecnocuisine.Formularios.Compras.CrearVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .ibox-content {
            padding-right: 20px !important;
            padding-left: 20px !important;
            padding-bottom: 20px !important;
        }

        #step-content {
            position: inherit;
        }

        .hide {
            display: none;
        }

        .lblImgDocF:hover {
            cursor: pointer;
        }

        .col-md-1 {
            padding-left: 2px;
            padding-right: 2px;
        }

        label {
            margin-bottom: auto;
        }

        .well {
            background-color: lightgray;
            border: 1px solid #E3E3D5;
        }

        .jstree-open > .jstree-anchor > .fa-folder:before {
            content: "\f07c";
        }

        .jstree-default .jstree-icon.none {
            width: 0;
        }
    </style>

    <div class="well" style="margin-top: 1%; margin-right: -15px; margin-left: -15px;">
        <asp:HiddenField ID="HFRecetas" runat="server" />
        <div id="productoh1" runat="server"></div>
        <div classname="d-flex p-2" style="display: flex; align-items: center; margin-bottom: 15px">
            <h2 style="margin: 0; margin-right: 15px;">Ingrese cantidad de ventas</h2>
            <input type="number" id="InputCantidad" placeholder="Ingresar Cantidad" style="height: 30px; margin-right: 15px; background-color: #FFFFFF;
    background-image: none;
    border: 1px solid #e5e6e7;
    border-radius: 1px;
    color: inherit;
    display: block;
    padding: 6px 12px;
    transition: border-color 0.15s ease-in-out 0s, box-shadow 0.15s ease-in-out 0s;
    font-size: 14px; " />
            <button onclick="Calcular(event)" class="btn btn-sm btn-primary pull-right">Calcular Cantidad</button>
            <div class="float-right" style="width: 50%;">
            <button  class="btn btn-sm btn-primary pull-right float-right">Confirmar</button>
            </div>

        </div>
        <asp:HiddenField runat="server" ID="idProductosRecetas" />
        <asp:HiddenField runat="server" ID="hiddenReceta" />
        <asp:HiddenField runat="server" ID="hiddenProducts" />
        <asp:HiddenField runat="server" ID="hiddenRinde" />
        <asp:HiddenField runat="server" ID="HiddenPruductoCalculo" />

        <table class="table table-bordered table-hover" id="tableProductos">
            <thead>
                <tr>
                    <th style="width: 5%">Cod. Producto</th>
                    <%--<th style="width: 10%">Tipo</th>--%>
                    <th style="width: 15%">Descripcion</th>
                    <th style="width: 10%; text-align: right">Cantidad</th>
                    <th style="width: 10%">Unidad Medida</th>
                    <th style="width: 10%; text-align: right">Costo $</th>
                    <th style="width: 10%; text-align: right">Costo Total $</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder ID="phProductos" runat="server" />
            </tbody>
        </table>

    </div>






    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../../Scripts/plugins/toastr/toastr.min.js"></script>
    <script src="../../js/bootstrap.min.js"></script>
    <script src="../../Scripts/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../../js/plugins/pace/pace.min.js"></script>
    <script src="/../js/plugins/jsTree/jstree.min.js"></script>

    <script>
        $(document).ready(function () {
            $("body").tooltip({ selector: '[data-toggle=tooltip]' });
           let valueRinde = document.getElementById('ContentPlaceHolder1_hiddenRinde');
           let valueRindeh1 = document.getElementById("ContentPlaceHolder1_productoh1").childNodes[1];
            valueRindeh1 = valueRindeh1.innerText.split(":")[1].trim()
            valueRinde.value = valueRindeh1;
            let idRecetas = document.getElementById('<%=HFRecetas.ClientID %>').value.split(',');
            if (document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',').length > 0) {

                for (let i = 0; i < document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',').length; i++) {
                    if (document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i] != "") {
                        let id = document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i];
                        $('#jstree' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i])
                            .on('after_open.jstree', function (e, data) {

                                let banderaC = false;
                                let JidCant = $("#jstree_C" + id).jstree()._model.data
                                let JidCantaux = Object.values(JidCant);

                                for (i = 0; i < JidCantaux.length; i++) {
                                    if (JidCantaux[i].id.includes('_')) {
                                        let res = JidCantaux[i].id.split('_')[1];
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaC == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_C" + id).jstree("open_node", "#RecetaC_LI_" + id);
                                                banderaC = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_C" + id).jstree("open_node", "#" + JidCantaux[i].id);
                                                    banderaC = true;
                                                }
                                        }
                                    }
                                }
                                let banderaUM = false;
                                let JidUM = $("#jstree_UM" + id).jstree()._model.data
                                let JidUMTaux = Object.values(JidUM);

                                for (i = 0; i < JidUMTaux.length; i++) {
                                    if (JidUMTaux[i].id.includes('_')) {
                                        let res = JidUMTaux[i].id.split('_')[1];
                                        console.log(data.node.id)
                                        console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaUM == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_UM" + id).jstree("open_node", "#RecetaUM_LI_" + id);
                                                banderaUM = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_UM" + id).jstree("open_node", "#" + JidUMTaux[i].id);
                                                    banderaUM = true;
                                                }
                                        }
                                    }
                                }

                                let JidCS = $("#jstree_CS" + id).jstree()._model.data
                                let JidCSaux = Object.values(JidCS);
                                let banderaCS = false;

                                for (i = 0; i < JidCSaux.length; i++) {
                                    if (JidCSaux[i].id.includes('_')) {
                                        let res = JidCSaux[i].id.split('_')[1];
                                        console.log(data.node.id)
                                        console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCS == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CS" + id).jstree("open_node", "#RecetaCS_LI_" + id);
                                                banderaCS = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CS" + id).jstree("open_node", "#" + JidCSaux[i].id);
                                                    banderaCS = true;
                                                }
                                        }
                                    }
                                }

                                let JidCST = $("#jstree_CST" + id).jstree()._model.data
                                let JidCSTaux = Object.values(JidCST);
                                let banderaCST = false
                                for (i = 0; i < JidCSTaux.length; i++) {
                                    if (JidCSTaux[i].id.includes('_')) {
                                        let res = JidCSTaux[i].id.split('_')[1];
                                        console.log(data.node.id)
                                        console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCST == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CST" + id).jstree("open_node", "#RecetaCST_LI_" + id);
                                                banderaCST = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CST" + id).jstree("open_node", "#" + JidCSTaux[i].id);
                                                    banderaCST = true;
                                                }
                                        }
                                    }
                                }

                                //$("#jstree_C" + id).jstree("open_all", JidCantaux.id);
                                //$("#jstree_UM" + id).jstree("open_all", JidUMTaux.id);
                                //$("#jstree_CS" + id).jstree("open_all", JidCSaux.id);
                                //$("#jstree_CST" + id).jstree("open_all", JidCSTaux.id);
                            })
                            .on('after_close.jstree', function (e, data) {
                                let JidCant = $("#jstree_C" + id).jstree()._model.data
                                let JidCantaux = Object.values(JidCant);
                                let banderaC = false;
                                for (i = 0; i < JidCantaux.length; i++) {
                                    if (JidCantaux[i].id.includes('_')) {
                                        let res = JidCantaux[i].id.split('_')[1];
                                        console.log(data.node.id)
                                        console.log(data.node.data)
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaC == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_C" + id).jstree("close_node", "#RecetaC_LI_" + id);
                                                banderaC = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_C" + id).jstree("close_node", "#" + JidCantaux[i].id);
                                                    banderaC = true;
                                                }
                                        }
                                    }
                                }
                                let banderaUM = false;
                                let JidUM = $("#jstree_UM" + id).jstree()._model.data
                                let JidUMTaux = Object.values(JidUM);

                                for (i = 0; i < JidUMTaux.length; i++) {
                                    if (JidUMTaux[i].id.includes('_')) {
                                        let res = JidUMTaux[i].id.split('_')[1];
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaUM == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_UM" + id).jstree("close_node", "#RecetaUM_LI_" + id);
                                                banderaUM = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_UM" + id).jstree("close_node", "#" + JidUMTaux[i].id);
                                                    banderaUM = true;
                                                }
                                        }
                                    }
                                }

                                let JidCS = $("#jstree_CS" + id).jstree()._model.data
                                let JidCSaux = Object.values(JidCS);
                                let banderaCS = false
                                for (i = 0; i < JidCSaux.length; i++) {
                                    if (JidCSaux[i].id.includes('_')) {
                                        let res = JidCSaux[i].id.split('_')[1];
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCS == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CS" + id).jstree("close_node", "#RecetaCS_LI_" + id);
                                                banderaCS = true
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CS" + id).jstree("close_node", "#" + JidCSaux[i].id);
                                                    banderaCS = true
                                                }
                                        }
                                    }
                                }

                                let JidCST = $("#jstree_CST" + id).jstree()._model.data
                                let JidCSTaux = Object.values(JidCST);
                                let banderaCST = false;
                                for (i = 0; i < JidCSTaux.length; i++) {
                                    if (JidCSTaux[i].id.includes('_')) {
                                        let res = JidCSTaux[i].id.split('_')[1];
                                        let res2 = data.node.id.split('_')[1];
                                        if (banderaCST == false) {

                                            if (res.includes("LI")) {
                                                $("#jstree_CST" + id).jstree("close_node", "#RecetaCST_LI_" + id);
                                                banderaCST = true;
                                            } else
                                                if (res == res2) {
                                                    $("#jstree_CST" + id).jstree("close_node", "#" + JidCSTaux[i].id);
                                                    banderaCST = true;
                                                }
                                        }
                                    }
                                }
                                //console.log('prueba2');
                                //let JidCant2 = $("#jstree_C" + id).jstree()._model.data
                                //let JidCantaux2 = Object.values(JidCant2)[11];

                                //let JidUM2 = $("#jstree_UM" + id).jstree()._model.data
                                //let JidUMTaux2 = Object.values(JidUM2)[11];

                                //let JidCS2 = $("#jstree_CS" + id).jstree()._model.data
                                //let JidCSaux2 = Object.values(JidCS2)[11];

                                //let JidCST2 = $("#jstree_CST" + id).jstree()._model.data
                                //let JidCSTaux2 = Object.values(JidCST2)[11];

                                //$("#jstree_C" + id).jstree("close_all",  "#"+JidCantaux2.id);
                                //$("#jstree_UM" + id).jstree("close_all", "#"+JidUMTaux2.id);
                                //$("#jstree_CS" + id).jstree("close_all", "#"+JidCSaux2.id);
                                //$("#jstree_CST" + id).jstree("close_all","#"+ JidCSTaux2.id);
                            }).jstree({
                                'core': {
                                    'check_callback': true
                                },
                                'plugins': ['types', 'dnd'],
                                'types': {
                                    'default': {
                                        'icon': 'fa fa-folder'
                                    },
                                    'html': {
                                        'icon': 'fa fa-file-code-o'
                                    },
                                    'svg': {
                                        'icon': 'fa fa-file-picture-o'
                                    },
                                    'css': {
                                        'icon': 'fa fa-file-code-o'
                                    },
                                    'img': {
                                        'icon': 'fa fa-file-image-o'
                                    },
                                    'js': {
                                        'icon': 'fa fa-file-text-o'
                                    }

                                }
                            });
                        $('#jstree_C' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            }
                        });
                        $('#jstree_UM' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            }
                        });
                        $('#jstree_CS' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            }
                        });
                        $('#jstree_CST' + document.getElementById('ContentPlaceHolder1_HFRecetas').value.split(',')[i]).jstree({
                            'core': {
                                'check_callback': true
                            },
                            'plugins': ['types', 'dnd'],
                            'types': {
                                'default': {
                                    'icon': 'fa fa-folder'
                                },
                                'html': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'svg': {
                                    'icon': 'fa fa-file-picture-o'
                                },
                                'css': {
                                    'icon': 'fa fa-file-code-o'
                                },
                                'img': {
                                    'icon': 'fa fa-file-image-o'
                                },
                                'js': {
                                    'icon': 'fa fa-file-text-o'
                                }

                            }
                        });
                    }
                }
            }

        });


        function AgregarVentas() {
            let table2 = document.getElementById('editable');
            let cuerpor = document.getElementById("tbodyEditable1");
            let lblFinal = document.getElementById("lblProductosVender");

            let max = table2.rows.length;
            let error = 0
            for (let i = 1; i < max; i++) {
                let stock = table2.rows[i].cells[6].textContent;
                let valueinput = table2.rows[i].cells[8].children[0];
                let idArticulo = Number(table2.rows[i].cells[0].textContent)
                let idPresentacion = table2.rows[i].cells[1].id
                let idMarca = table2.rows[i].cells[2].id
                let Lote = table2.rows[i].cells[3].id
                let idSector = table2.rows[i].cells[4].id


                if (valueinput.value <= stock) {
                    console.log("ACAAAA" + table2.rows[i].cells[8].children[0].value);
                    return valueinput.style = "border: 1px solid #e7e7e7;"
                } else {
                    error = 1;
                    return valueinput.style = "border: 1px solid red;"
                }
            }
            if (error == 1) {
                toastr.error('No puedes poner un numero MAYOR al stock')
                return false
            }
        }


        function GenerarVenta() {
            $.ajax({
                method: "POST",
                url: "CrearVenta.aspx/CrearNuevaVenta",
                data: '{ descripcion: "' + document.querySelector('#lblDescripcionFinal').textContent
                    + '" , Categoria: "' + document.querySelector('#lblCategoriaFinal').textContent + '"  }',
                contentType: "application/json",
                dataType: 'json',
                error: (error) => {
                    finishButton.removeClass();
                    console.log(JSON.stringify(error));
                    $.msgbox("No se pudo cargar la tabla", { type: "error" });
                    ulFinal.className = ""
                },
                success: setTimeout(function () {
                    recargarPagina();
                }, 2000)
            });
        }
        function ValidarInput(event) {
            console.log(event.currentTarget);
            let input = event.currentTarget
            if (input.value > input.max) {
                input.style = "border: 1px solid red;";
                toastr.error('No puedes poner un numero MAYOR al stock')

            } else if (input.value < 0) {
                toastr.error('No puedes poner un numero MENOR a 0')
                input.style = "border: 1px solid red;";
            } else {
                input.style = "border: 1px solid #e7e7e7;"
            }
            /* .style = "border: 1px solid #e7e7e7;;"*/

        }




    </script>
    <script>
        $(document).ready(function () {

            $('#jstree1').jstree({
                'core': {
                    'check_callback': true
                },
                'plugins': ['types', 'dnd'],
                'types': {
                    'default': {
                        'icon': 'fa fa-folder'
                    },
                    'html': {
                        'icon': 'fa fa-file-code-o'
                    },
                    'svg': {
                        'icon': 'fa fa-file-picture-o'
                    },
                    'css': {
                        'icon': 'fa fa-file-code-o'
                    },
                    'img': {
                        'icon': 'fa fa-file-image-o'
                    },
                    'js': {
                        'icon': 'fa fa-file-text-o'
                    }

                }
            });

            $('#using_json').jstree({
                'core': {
                    'data': [
                        'Empty Folder',
                        {
                            'text': 'Resources',
                            'state': {
                                'opened': true
                            },
                            'children': [
                                {
                                    'text': 'css',
                                    'children': [
                                        {
                                            'text': 'animate.css', 'icon': 'none'
                                        },
                                        {
                                            'text': 'bootstrap.css', 'icon': 'none'
                                        },
                                        {
                                            'text': 'main.css', 'icon': 'none'
                                        },
                                        {
                                            'text': 'style.css', 'icon': 'none'
                                        }
                                    ],
                                    'state': {
                                        'opened': true
                                    }
                                },
                                {
                                    'text': 'js',
                                    'children': [
                                        {
                                            'text': 'bootstrap.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'inspinia.min.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'jquery.min.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'jsTree.min.js', 'icon': 'none'
                                        },
                                        {
                                            'text': 'custom.min.js', 'icon': 'none'
                                        }
                                    ],
                                    'state': {
                                        'opened': true
                                    }
                                },
                                {
                                    'text': 'html',
                                    'children': [
                                        {
                                            'text': 'layout.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'navigation.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'navbar.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'footer.html', 'icon': 'none'
                                        },
                                        {
                                            'text': 'sidebar.html', 'icon': 'none'
                                        }
                                    ],
                                    'state': {
                                        'opened': true
                                    }
                                }
                            ]
                        },
                        'Fonts',
                        'Images',
                        'Scripts',
                        'Templates',
                    ]
                }
            });

        });

    </script>
    <script>
        function Calcular(event) {
            event.preventDefault();
            var resume_table = document.getElementById("tableProductos");
            let input = document.getElementById("InputCantidad");
            if (input.value == 0 || input.value == null || input.value == undefined) {
                return alert("INGRESAR UN VALOR")
            }
            let cantidadIgresada = InputCantidad.value;


            if (document.getElementById('ContentPlaceHolder1_hiddenRinde').value == "") {
                valueRinde = document.getElementById('ContentPlaceHolder1_hiddenRinde').value;
                valueRindeh1 = document.getElementById("ContentPlaceHolder1_productoh1").childNodes[1];
                valueRindeh1 = valueRindeh1.innerText.split(":")[1].trim()
                valueRinde.value = valueRindeh1;
            }
            valueRinde = Number(document.getElementById('ContentPlaceHolder1_hiddenRinde').value);
            console.log(valueRinde);
            divisor = (Number(cantidadIgresada) / valueRinde).toFixed(2)
            console.log(divisor,"ACA")
            let hidenproduct = document.getElementById("ContentPlaceHolder1_hiddenProducts");
            if (hidenproduct.value == "") {
                let HiddenPruductoCalculo = document.getElementById("ContentPlaceHolder1_HiddenPruductoCalculo");
                HiddenPruductoCalculo.value = "";
                for (var i = 1, row; row = resume_table.rows[i]; i++) {
                    if (hidenproduct.value != "") {
                        hidenproduct.value += ";"
                    }
                    if (HiddenPruductoCalculo.value != "") {
                        HiddenPruductoCalculo.value += ";"
                    }
                    let Cant = 0;
                    for (var j = 0, col; col = row.cells[j]; j++) {

                        if (j == 2) {
                            if (col.childElementCount == 0) {
                                hidenproduct.value += col.innerText + ","
                                cant = col.innerText;
                                col.innerText = (col.innerText * divisor).toFixed(2)
                                HiddenPruductoCalculo.value += col.innerText + ",";
                            } else {
                            Cant =  searchNodeCant(col, divisor);
                            }
                        }
                        if (j == 4) {
                            if (col.childElementCount == 0) {
                                hidenproduct.value += col.innerText + ","
                                let total = (divisor * col.innerText) / cant
                                col.innerText = (total).toFixed(2);
                                HiddenPruductoCalculo.value += col.innerText + ",";
                            } else {
                                searchNode(col, divisor, cant);
                            }
                        }
                        if (j == 5) {
                            if (col.childElementCount == 0) {
                                hidenproduct.value += col.innerText + ","
                                let total = (divisor * col.innerText) / cant
                                col.innerText = (total).toFixed(2);
                                HiddenPruductoCalculo.value += col.innerText + ",";
                            } else {
                                searchNode(col, divisor, cant);
                            }
                        }
                    }

                }

            } else {
                let HiddenPruductoCalculo = document.getElementById("ContentPlaceHolder1_HiddenPruductoCalculo");
                HiddenPruductoCalculo.value = "";
                 let array = hidenproduct.value.split(";")
                for (var i = 1, row; row = resume_table.rows[i]; i++) {
                    let arrayFinal = array[i - 1].split(",")
                    if (HiddenPruductoCalculo.value != "") {
                        HiddenPruductoCalculo.value += ";"
                    }
                    for (var j = 0, col; col = row.cells[j]; j++) {
                        if (j == 2) {
                            if (col.childElementCount == 0) {
                              
                                col.innerText = (divisor * arrayFinal[0]).toFixed(2)
                                HiddenPruductoCalculo.value += col.innerText + ","
                            } else {
                                searchNodeCant2(col, divisor, arrayFinal[0]);
                            }
                        }
                        if (j == 4) {
                            if (col.childElementCount == 0) {
                                col.innerText = ((divisor * arrayFinal[1]) / arrayFinal[0]).toFixed(2)
                                HiddenPruductoCalculo.value += col.innerText + ","
                            } else {
                                searchNode2(col, divisor, arrayFinal[1], arrayFinal[0]);
                            }
                        }
                        if (j == 5) {
                            if (col.childElementCount == 0) {
                            
                                col.innerText = (arrayFinal[2] / divisor).toFixed(2)
                            } else {
                                searchNode2(col, divisor, arrayFinal[2], arrayFinal[0]);
                            }
                        }
                    }

                }







            }
        }
        function searchNodeCant(col, divisor) {
            let hidenproduct = document.getElementById("ContentPlaceHolder1_hiddenProducts");
            let HiddenPruductoCalculo = document.getElementById("ContentPlaceHolder1_HiddenPruductoCalculo");
            nodePrime = col.childNodes[0]
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[1];
            total = nodePrime.text;
            total = Number(total.replace(",", ""));
            cant = total;
            hidenproduct.value += total + ","
            total = (divisor * total ).toFixed(2)
            nodeI = nodePrime.childNodes[0];
            nodeI = nodeI.outerHTML
            total = "" + total + ""
            HiddenPruductoCalculo.value += total + ",";
            nodePrime.innerHTML = nodeI + total
            return cant;
        }



        function searchNode(col, divisor, cant) {
            let hidenproduct = document.getElementById("ContentPlaceHolder1_hiddenProducts");
            let HiddenPruductoCalculo = document.getElementById("ContentPlaceHolder1_HiddenPruductoCalculo");
            nodePrime = col.childNodes[0]
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[1];
            total = nodePrime.text;
            total = Number(total.replace(",", ""));
            hidenproduct.value += total + ","
            total = ((divisor * total) / cant).toFixed(2)
            nodeI = nodePrime.childNodes[0];
            nodeI = nodeI.outerHTML
            total = "" + total + ""
            nodePrime.innerHTML = nodeI + total
            HiddenPruductoCalculo.value += total + ",";
            return true
        }





        function searchNodeCant2(col, divisor,value) {
            let HiddenPruductoCalculo = document.getElementById("ContentPlaceHolder1_HiddenPruductoCalculo");
            nodePrime = col.childNodes[0]
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[1];
            total = value;
            total = Number(total.replace(",", ""));
            total = (divisor * total).toFixed(2)
            nodeI = nodePrime.childNodes[0];
            nodeI = nodeI.outerHTML
            total = "" + total + ""
            nodePrime.innerHTML = nodeI + total
            HiddenPruductoCalculo.value += total + ",";
            return true
        }




        function searchNode2(col, divisor, value, value2) {
            let HiddenPruductoCalculo = document.getElementById("ContentPlaceHolder1_HiddenPruductoCalculo");
            nodePrime = col.childNodes[0]
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[0];
            nodePrime = nodePrime.childNodes[1];
            total = value;
            
            total = Number(total.replace(",", ""));
            total = ((divisor * total) / value2).toFixed(2)
            nodeI = nodePrime.childNodes[0];
            nodeI = nodeI.outerHTML
            total = "" + total + ""
            nodePrime.innerHTML = nodeI + total
            HiddenPruductoCalculo.value += total + ",";
            return true
        }
    </script>
</asp:Content>

<%--Cuantas veces tengo que entrar al nodo hijo?--%>
<%--
childNodes[0]
childNodes[0]
childNodes[0]
childNodes[1] = a
childnode[0] = i
me guardo el i y modifico el a
    nodo = prueba2.outerHTML
    .innerHTML = nodo + 
--%>