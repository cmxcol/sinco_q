using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Excel;
using ICSharpCode;
using ICSharpCode.SharpZipLib;
using Components.CBloqueados.Cli;
using Infrastructure.Security.Usuario;
using Services.Excp;
using WebApplication.Client.Web.App_Code;
using Services.CBloqueados;
using System.Drawing;

namespace WebApplication.Client.Web.ME.PG.Adm
{
    /// <summary>
    ///     Año:2019
    ///     Modulo para el cargue masivo de los clientes bloqueados de excepciones
    /// </summary>
    public partial class CBloMasivo : BPage
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
                    lblEL.Visible = false;
                    btnUDoc.Enabled = false;
                    fp_Doc_xls.Enabled = false;
                    if (fp_Doc_xls.FileName.Contains(".xls") | fp_Doc_xls.FileName.Contains(".xlsx"))
                    {
                        var er = fp_Doc_xls.FileName.Contains(".xlsx") ? ExcelReaderFactory.CreateOpenXmlReader(fp_Doc_xls.PostedFile.InputStream) : ExcelReaderFactory.CreateBinaryReader(fp_Doc_xls.PostedFile.InputStream);
                        var excp = er.AsDataSet().Tables["Clientes"].AsEnumerable();
                        Int64 i64;
                        int inserted = 0;
                        int cont = 0;
                        string res = "";
                        string tEx = "";
                        string ex = "";
                        DateTime dt;
                        List<ICliPTO> lEx = new List<ICliPTO>();
                        ServCBloqueados serv = new ServCBloqueados();
                        serv.truncateClients("2");

                        foreach (var a in excp)
                        {
                            if (cont > 0)
                            {
                                tEx = a[2].ToString();
                                if (tEx.ToLower().Trim().CompareTo("cartera vencida") == 0 || tEx.ToLower().Trim().CompareTo("cv") == 0) {
                                    ex = "CV";
                                } else
                                if (tEx.ToLower().Trim().CompareTo("sobrecupo") == 0 || tEx.ToLower().Trim().CompareTo("sc") == 0){
                                    ex = "SC";
                                }
                                else
                                if (tEx.ToLower().Trim().CompareTo("sobrecupo y cartera vencida") == 0 || tEx.ToLower().Trim().CompareTo("scv") == 0){
                                    ex = "SCV";
                                }
                                else
                                 if (tEx.ToLower().Trim().CompareTo("sin saldo para programar") == 0 || tEx.ToLower().Trim().CompareTo("ssp") == 0){
                                    ex = "SSP";
                                }

                                    if (a[0].ToString().CompareTo("") == 0 || ex.CompareTo("") == 0)
                                    {
                                        if(a[0].ToString().CompareTo("") != 0)
                                            lEx.Add(new CliPTO(a[0].ToString(), a[1].ToString(), a[2].ToString()));
                                    }
                                    else{
                                        res = serv.insertClient(a[0].ToString(), a[1].ToString(), ((IUsr)Session["Usr"]).usr.CemexId.ToString(), ex, ((IUsr)Session["Usr"]).usr.Pais.IdPais.ToString(),"3");
                                    }


                                if (res.CompareTo("0") == 0 && a[0].ToString().CompareTo("") != 0) {
                                    lEx.Add(new CliPTO(a[0].ToString(), a[1].ToString(), a[2].ToString()));
                                }

                                if (res.CompareTo("1") == 0) {
                                    inserted++;
                                }

                            }
                            cont++;
                            ex = "";
                        }

                        if (cont < 1) {
                            lblMsgLDB.Visible = false;
                            lblELDB.Visible = false;
                            lblEL.ForeColor = Color.Red;
                            lblEL.Text = "El archivo no contiene datos Validos";
                            lblEL.Visible = true;
                        }

                        if (lEx.Count > 0)
                        {

                            gv_Error.DataSource = lEx;
                            gv_Error.DataBind();
                            gv_Error.Visible = true;
                            lblELDB.ForeColor = Color.Red;
                            lblELDB.Text = "Datos no Cargados";
                            lblELDB.Visible = true;
                            
                            
                        }
                        if (lEx.Count == 0 && inserted == 0)
                        {
                            lblMsgLDB.Visible = false;
                            lblELDB.Visible = false;
                            lblEL.ForeColor = Color.Red;
                            lblEL.Text = "El archivo no contiene datos";
                            lblEL.Visible = true;
                        }
                        else {
                            if (inserted > 0 && lEx.Count == 0) {
                                lblMsgLDB.Visible = false;
                                lblELDB.Visible = false;
                                lblEL.ForeColor = Color.Green;
                                lblEL.Text = "Datos insertados correctamente";
                                lblEL.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblMsgLDB.Visible = false;
                        lblELDB.Visible = false;
                        lblEL.Text = "Tipo de archivo no valido";
                        lblEL.Visible = true;
                    }
                    btnF.Visible = true;
                }
                else
                {
                    lblMsgLDB.Visible = false;
                    lblELDB.Visible = false;
                    lblEL.Text = "Por favor seleccione un archivo de clientes a Cargar.";
                    lblEL.Visible = true;
                }
            }
            catch (Exception exe)
            {
                btnUDoc.Enabled = true;
                fp_Doc_xls.Enabled = true;
                lblMsgLDB.Visible = false;
                lblELDB.Visible = false;
                lblEL.Text = "Se ha generado un error. Contacte al Administrador de la aplicación";
                lblEL.Visible = true;
                //throw;
            }
        }
        protected void btnF_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~\ME\PG\Adm\CBloMasivo.aspx");
        }
        protected void gv_ClEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ClBloCli.PageIndex = e.NewPageIndex;
            gv_ClBloCli.DataSource = Session["Ex_Va"];
            gv_ClBloCli.DataBind();
        }

        protected void gv_Error_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Error.PageIndex = e.NewPageIndex;
            gv_Error.DataSource = Session["Ex_Inv"];
            gv_Error.DataBind();
        }
    }
}