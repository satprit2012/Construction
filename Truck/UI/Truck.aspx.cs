using System;
using System.Data;
using System.Web.UI.WebControls;
using Truck.Model;

namespace Truck.TruckDetails
{
    public partial class Truck : System.Web.UI.Page
    {
        DataAccess.TruckDetails truckDetails = new DataAccess.TruckDetails();
        DataTable dt = new DataTable();
        int truckId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTruckGrid();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (drpStatus.SelectedIndex > 0)
            {
                truckDetails = new DataAccess.TruckDetails();
                Model.Truck truck = new Model.Truck()
                {
                    Code = txtCode.Text,
                    Name = txtName.Text,
                    StatusId = Convert.ToInt32(drpStatus.SelectedValue),
                    Description = txtDesc.Text
                };
                int newId = truckDetails.AddTruckDetails(truck);
                if (newId > 0)
                {
                    BindTruckGrid();
                }
            }
        }
             
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                ((DataTable)ViewState["datatable"]).DefaultView.RowFilter = string.Empty;
                BindTruckGrid();
            }
            else
            {
                ((DataTable)ViewState["datatable"]).DefaultView.RowFilter = string.Format("Name LIKE '{0}%'", txtSearch.Text);
                grdTruck.DataSource = (DataTable)ViewState["datatable"];
                grdTruck.DataBind();
            }
        }
        protected void grdTruck_Sorting(object sender, GridViewSortEventArgs e)
        {
            dt = (DataTable)ViewState["datatable"];//for applying Sorting
            dt.DefaultView.Sort = e.SortExpression;
            grdTruck.DataSource = dt;
            grdTruck.DataBind();
        }
        protected void grdTruck_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int truckId = Convert.ToInt32(grdTruck.DataKeys[e.RowIndex].Values["Id"]);
            truckDetails.DeleteTruck(truckId);
            BindTruckGrid();
        }
        public void BindTruckGrid()
        {
            dt = truckDetails.GetTrucks();
            ViewState["datatable"] = dt;
            grdTruck.DataSource = dt;
            grdTruck.DataBind();
        }
    }
}