using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewApp
{

    public partial class GridView : System.Web.UI.Page
    {
        int operacion;
        int totalItemSeleccionados = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView_Clientes_DataBound (object sender, EventArgs e)
        {
            //Recupera la el PagerRow...
            GridViewRow pagerRow = GridView_Clientes.BottomPagerRow;
            //Recupera los controles DropDownList y label...
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

            if( pageList != null)
            {
                //Se crean los valores del DropDownList tomando el número total de páginas... 
                int i;
                for(i=0; i<= GridView_Clientes.PageCount - 1; i++)
                {
                    // Se crea un objeto ListItem para representar la página...

                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());
                    if (i == GridView_Clientes.PageIndex)
                        item.Selected = true;

                    // Se añade el ListItem a la colección de Items del DropDownList...
                    pageList.Items.Add(item);
                }
            }
            if (pageLabel != null)
            {
                // Calcula el nº de página actual...
                int currentPage = GridView_Clientes.PageIndex + 1;
                // Actualiza el Label control con la página actual.
                pageLabel.Text = "Página " + currentPage.ToString() + " de " + GridView_Clientes.PageCount.ToString();
            }

        }

        protected void GridView_Clientes_PreRender(object sender, EventArgs e)
        {
            if (totalItemSeleccionados > 0)
                btnQuitarSeleccionados.CssClass = "btn btn-lg btn-danger";
            else
                btnQuitarSeleccionados.CssClass = "btn btn-lg btn-danger disabled";
        }

        protected void GridView_Clientes_RowDeleted(object sender , GridViewDeletedEventArgs e)
        {
            if(e.Exception == null)
            {
                lblInfo.Text = " ¡Cliente/s eliminado/s OK! ";
                lblInfo.CssClass = "label label-success";
            }
            else{
                lblInfo.Text = " ¡Se ha producido un error al intentar elimnar el/los cliente/s! ";
                lblInfo.CssClass = "label label-danger";
                e.ExceptionHandled = true;
            }

        }

        protected void GridView_Clientes_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if(e.Exception == null)
            {
                lblInfo.Text = " ¡Modificación realizada OK! ";
                lblInfo.CssClass = "label label-success";
            }
            else
            {
                lblInfo.Text = " ¡Se ha producido un error al intentar modificar el cliente! ";
                lblInfo.CssClass = "label label-danger";
                e.ExceptionHandled = true;
            }
        }

        protected void GridView_Clientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            lblInfo.Text = "";
        }

        public void btnQuitarSeleccionados_Click(object sender, EventArgs e) {
            //Recorrer las filas del GridView...
            int i;

            for(i=0; i <= GridView_Clientes.Rows.Count- 1; i++)
            {
                CheckBox CheckBoxElim = (CheckBox)GridView_Clientes.Rows[i].FindControl("chkEliminar");
                if (CheckBoxElim.Checked)
                    GridView_Clientes.DeleteRow(i);
            }
            GridView_Clientes.DataBind();
        }

        protected void chk_OnCheckedChanged(object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
            int i;
            //'Quita el mensaje de información si lo hubiera...
            lblInfo.Text = "";

            for(i = 0; i <= GridView_Clientes.Rows.Count - 1; i++)
            {
                CheckBox CheckBoxElim = (CheckBox)GridView_Clientes.Rows[i].FindControl("chkEliminar");
                if (CheckBoxElim.Checked)
                {
                    totalItemSeleccionados ++;
                }
            }
        }

        protected void PageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Recupera la fila.

            GridViewRow pagerRow = GridView_Clientes.BottomPagerRow;
            // Recupera el control DropDownList...
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            // Se Establece la propiedad PageIndex para visualizar la página seleccionada...
            GridView_Clientes.PageIndex = pageList.SelectedIndex;
            // Quita el mensaje de información si lo hubiera...
            lblInfo.Text = "";
        }
    }
}
 