using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Tecnocuisine.Formularios.Maestros
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarTransferencias();
        }

        public void cargarTransferencias()
        {

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "dd-item");
            li.Attributes.Add("data-id", "1");
            li.Attributes.Add("id", "1");
            li.Attributes.Add("runat", "server");


            main.Controls.Add(li);

            HtmlGenericControl div = new HtmlGenericControl("div");
            //div.Attributes.Add("class", "dd-handle drag_disabled editable");



            HtmlGenericControl cellContainer = new HtmlGenericControl("div");
            cellContainer.Attributes.Add("class", "cel-container");


            HtmlGenericControl cell = new HtmlGenericControl("div");
            cell.Attributes.Add("class", "cel");
            cell.InnerText = "30/1/2024 00:00:00";
            cellContainer.Controls.Add(cell);


            HtmlGenericControl cell2 = new HtmlGenericControl("div");
            cell2.Attributes.Add("class", "cel");
            cell2.InnerText = "ALMACEN";
            cellContainer.Controls.Add(cell2);


            HtmlGenericControl cell3 = new HtmlGenericControl("div");
            cell3.Attributes.Add("class", "cel");
            cell3.InnerText = "COCINA CALIENTE";
            cellContainer.Controls.Add(cell3);


            HtmlGenericControl cell4 = new HtmlGenericControl("div");
            cell4.Attributes.Add("class", "cel");
            cell4.InnerText = "#000001";
            cellContainer.Controls.Add(cell4); 
            
            HtmlGenericControl cell5 = new HtmlGenericControl("div");
            cell5.Attributes.Add("class", "cel");
            cell5.InnerText = "A confirmar";
            cellContainer.Controls.Add(cell5);


            // Agregar contenedor de celdas al div principal
            div.Controls.Add(cellContainer);

            // Agregar div al li
            li.Controls.Add(div);

            cargarDatosTransferencias("1", li);
        }


        public void cargarDatosTransferencias(string id, HtmlGenericControl li)
        {
            HtmlGenericControl ol = new HtmlGenericControl("ol");
            ol.Attributes.Add("class", "dd-list");
            li.Controls.Add(ol);


            HtmlGenericControl liHijo = new HtmlGenericControl("li");

            liHijo.Attributes.Add("class", "dd-item");
            liHijo.Attributes.Add("data-id", "2");
            liHijo.Attributes.Add("id", "2");
            liHijo.Attributes.Add("runat", "server");

            ol.Controls.Add(liHijo);

            HtmlGenericControl div = new HtmlGenericControl("div");
            //div.Attributes.Add("class", "dd-handle drag_disabled editable");


            HtmlGenericControl cellContainer = new HtmlGenericControl("div");
            cellContainer.Attributes.Add("class", "cel-container");


            HtmlGenericControl cell = new HtmlGenericControl("div");
            cell.Attributes.Add("class", "cel");
            cell.InnerText = "Sector Productivo";
            cellContainer.Controls.Add(cell);


            HtmlGenericControl cell2 = new HtmlGenericControl("div");
            cell2.Attributes.Add("class", "cel");
            cell2.InnerText = "Producto";
            cellContainer.Controls.Add(cell2);


            HtmlGenericControl cell3 = new HtmlGenericControl("div");
            cell3.Attributes.Add("class", "cel");
            cell3.InnerText = "Cantidad";
            cellContainer.Controls.Add(cell3);


            HtmlGenericControl cell4 = new HtmlGenericControl("div");
            cell4.Attributes.Add("class", "cel");
            cell4.InnerText = "Confirmada";
            cellContainer.Controls.Add(cell4);

            HtmlGenericControl cellContainer2 = new HtmlGenericControl("div");
            cellContainer2.Attributes.Add("class", "cel-container");


            //HtmlGenericControl cell6 = new HtmlGenericControl("div");
            //cell6.Attributes.Add("class", "cel");
            //cell6.InnerText = "Almacen";
            //cellContainer2.Controls.Add(cell6); 
            
            //HtmlGenericControl cell7 = new HtmlGenericControl("div");
            //cell7.Attributes.Add("class", "cel");
            //cell7.InnerText = "ACEITE PARA FREIR x LT";
            //cellContainer2.Controls.Add(cell7);


            //HtmlGenericControl cell8 = new HtmlGenericControl("div");
            //cell8.Attributes.Add("class", "cel");
            //cell8.InnerText = "0.2000";
            //cellContainer2.Controls.Add(cell8);

            //HtmlGenericControl cell9 = new HtmlGenericControl("div");
            //cell9.Attributes.Add("class", "cel");
            //cell9.InnerText = "0.2000";
            //cellContainer2.Controls.Add(cell9);


            // Agregar contenedor de celdas al div principal
            div.Controls.Add(cellContainer);
            div.Controls.Add(cellContainer2);

            // Agregar div al li
            liHijo.Controls.Add(div);

            cargarDatosTransferencias2("2", liHijo);

        }

        public void cargarDatosTransferencias2(string id, HtmlGenericControl li)
        {

            HtmlGenericControl ol = new HtmlGenericControl("ol");
            ol.Attributes.Add("class", "dd-list");
            li.Controls.Add(ol);


            HtmlGenericControl liHijo = new HtmlGenericControl("li");

            liHijo.Attributes.Add("class", "dd-item");
            liHijo.Attributes.Add("data-id", "3");
            liHijo.Attributes.Add("id", "3");
            liHijo.Attributes.Add("runat", "server");

            ol.Controls.Add(liHijo);

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "dd-handle drag_disabled editable");
            div.InnerText = "descripcion hijo hijo";


            liHijo.Controls.Add(div);
        }
    }
}