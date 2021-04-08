using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Client.Web.App_Code;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication.Client.Web.ME.PG.Adm
{
    public partial class CBloqueos : BPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnUDoc_Click(object sender, EventArgs e)
        {
            try
            {

                

                if (fp_Doc_xls.HasFile)
                {
                    //throw new Exception("Error");
                    lblEL.Visible = false;
                    fp_Doc_xls.SaveAs(Server.MapPath(@"~\CO\Doc\Bloqueos\Bloqueos.xls"));
                    lblMsgL.Text = "El Nombre del archivo: Bloqueos.xls";
                    lblMsgL.Visible = true;
                    btnVPrevia.Visible = true;
                    btnUDoc.Enabled = false;
                    fp_Doc_xls.Enabled = false;
                }
                else
                {
                    btnVPrevia.Visible = false;
                    lblMsgL.Visible = false;
                    lblEL.Text = "Por favor seleccione un archivo de excepciones para subir.";
                    lblEL.Visible = true;
                }
            }
            catch (Exception )
            {
                btnUDoc.Enabled = true;
                fp_Doc_xls.Enabled = true;
                btnVPrevia.Visible = false;
                lblMsgL.Visible = false;
                lblEL.Text = "Se ha generado un error. Contacte al Administrador de la aplicación";
                lblEL.Visible = true;
                throw;
            }
        }
        protected void btnVPrevia_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection cnnEx = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;" + @"Data Source= " + Server.MapPath("~\\CO\\Doc\\Bloqueos\\Bloqueos.xls") + ";" + @"Extended Properties=" + '"' + "Excel 8.0;HDR=YES" + '"'))
                {
                    using (OleDbDataAdapter adpEx = new OleDbDataAdapter())
                    {

                        DataTable dt = new DataTable();
                        adpEx.SelectCommand = new OleDbCommand();
                        adpEx.SelectCommand.CommandText = "SELECT DISTINCT CLIENTE AS IdRsObr, BLOQ_CEN_PED AS BlCenPed, BLOQ_CEN_ENT AS BlCenEnt FROM [Bloqueos$] WHERE CLIENTE <> 0";
                        adpEx.SelectCommand.Connection = cnnEx;
                        cnnEx.Open();
                        adpEx.SelectCommand.ExecuteNonQuery();    
                        cnnEx.Close();
                        adpEx.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            bool val = false;
                            Int64 valComp;
                            for (int i = 0; i < dt.Rows.Count - 1; i++)
                            {
                                for (int j = i + 1; j <= dt.Rows.Count - 1; j++)
                                {
                                    if (Int64.TryParse(dt.Rows[i][0].ToString(), out valComp) & Int64.TryParse(dt.Rows[j][0].ToString(), out valComp))
                                    {
                                        if (Int64.Parse(dt.Rows[i][0].ToString()) == Int64.Parse(dt.Rows[j][0].ToString()))
                                        {
                                            btnF.Visible = true;
                                            lblEL.Text = "Datos repetidos del Cliente: " + dt.Rows[i][0].ToString() + " - Verifique los datos e Intente Nuevamente";
                                            lblEL.Visible = true;
                                            val = true;
                                        }
                                    }
                                    else
                                    {
                                        btnF.Visible = true;
                                        lblEL.Text = "Verifique los datos del archivo";
                                        lblEL.Visible = true;
                                        val = true;
                                    }
                                }
                            }
                            if (val == false)
                            {
                                lblEL.Visible = false;
                                Session["ClBl"] = dt;
                                gv_ClBl.DataSource = dt;
                                gv_ClBl.DataBind();
                                gv_ClBl.Visible = true;
                                btnLoadDB.Visible = true;
                                btnF.Visible = false;
                            }
                        }
                        else
                        {
                            btnF.Visible = true;
                            lblEL.Text = "Verifique los datos del archivo";
                            lblEL.Visible = true;
                        }
                        btnVPrevia.Visible = false;
                    }
                }
            }
            catch (Exception )
            {
                btnVPrevia.Visible = false;
                lblMsgL.Visible = false;
                lblEL.Text = "Se ha generado un error. Contacte al Administrador de la aplicación";
                lblEL.Visible = true;

            }
        }
        protected void btnLoadDB_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_ClBl.Rows.Count > 0)
                {
                    using (SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cnnCOS"].ToString()))
                    {
                        using (SqlDataAdapter adp = new SqlDataAdapter())
                        {
                            adp.SelectCommand = new SqlCommand();
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.CommandText = "SPSIT_BC";
                            adp.SelectCommand.Parameters.AddWithValue("@TCOM", 3);
                            adp.SelectCommand.Connection = cnn;
                            cnn.Open();
                            adp.SelectCommand.ExecuteNonQuery();
                            cnn.Close();
                            SqlBulkCopy bc = new SqlBulkCopy(cnn.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction);

                            for (int i = 0; i <= ((DataTable)Session["ClBl"]).Columns.Count - 1; i++)
                            {
                                bc.ColumnMappings.Add(((DataTable)Session["ClBl"]).Columns[i].ColumnName, ((DataTable)Session["ClBl"]).Columns[i].ColumnName);
                            }
                            bc.DestinationTableName = "TBBL";
                            bc.BatchSize = ((DataTable)Session["ClBl"]).Rows.Count;
                            bc.WriteToServer(((DataTable)Session["ClBl"]));
                        }
                    }
                    lblMsgLDB.Text = "Datos Guardados";
                    lblMsgLDB.Visible = true;
                    btnLoadDB.Visible = false;
                }
                btnF.Visible = true;
            }
            catch (Exception )
            {
                lblMsgLDB.Visible = false;
                btnVPrevia.Visible = false;
                lblMsgL.Visible = false;
                lblELDB.Text = "La carga del archivo no ha sido realizada. Verifique los datos e intente de nuevo.";
                lblELDB.Visible = true;
                btnF.Visible = true;
                throw;
            }
        }
        protected void btnF_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~\CO\PG\Adm\CBloqueos.aspx");
        }
        protected void gv_ClEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ClBl.PageIndex = e.NewPageIndex;
            gv_ClBl.DataSource = Session["ClBl"];
            gv_ClBl.DataBind();
        }

        protected void fp_Doc_xls_PreRender(object sender, EventArgs e)
        {
            if (sender is FileUpload)
            {
                var MyButton = (FileUpload)sender;
                ScriptManager NewScriptManager = ScriptManager.GetCurrent(this.Page);
                NewScriptManager.RegisterPostBackControl(MyButton);
            }
        }
    }
}