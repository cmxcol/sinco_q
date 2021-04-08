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
using Components.PTO.Ex;
using Infrastructure.Security.Usuario;
using Services.Excp;
using WebApplication.Client.Web.App_Code;

namespace WebApplication.Client.Web.CO.PG.Adm
{
    public partial class ECartera : BPage
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
                        var excp = er.AsDataSet().Tables["Excepciones"].AsEnumerable();
                        Int64 i64;
                        int cont = 0;
                        DateTime dt;
                        List<IExcpPTO> lEx = new List<IExcpPTO>();
                        foreach (var a in excp)
                        {
                            if (cont > 0)
                            {
                                lEx.Add(new ExcpPTO(a[0].ToString(), a[1].ToString(), (DateTime.TryParse(a[2].ToString(), out dt) ? String.Format("{0:yyyy-MM-dd}",DateTime.Parse(a[2].ToString())): a[2].ToString()), a[3].ToString(), idEmp: (Int64.TryParse(a[4].ToString(), out i64) ? Int64.Parse(a[4].ToString()) : 0)));
                            }
                            cont++;
                        }
                        if (lEx.Count > 0)
                        {
                            List<Object> lv = new List<object> { "", null };
                            var dArch = (from lex in lEx
                                         where !lv.Contains(lex.IdCliente) & !lv.Contains(lex.TEx)
                                         select lex).Distinct().ToList();
                            if (dArch.Count > 0)
                            {
                                IExS serv = new ExS();
                                var lError = serv.InsExArch(dArch, ((IUsr)Session["Usr"]).usr.CemexId, ((IUsr)Session["Usr"]).usr.Rol.IdRol, ((IUsr)Session["Usr"]).usr.Pais.IdPais);

                                var valid = (from ex in lError.ToList()[0]
                                             select new { ex.IdCliente, ex.TEx, ex.DtVig, ex.MsgEx, IdEmp = (ex.IdEmp == 0?"-": ex.IdEmp.ToString()) }).ToList();
                                Session["Ex_Va"] = valid;

                                var invalid = (from ex in lError.ToList()[1]
                                               select new { ex.IdCliente, ex.TEx, ex.DtVig, ex.MsgEx, IdEmp = (ex.IdEmp == 0 ? "-" : ex.IdEmp.ToString()),ex.EMsg }).ToList();
                                //var Invalid = lError.ToList()[1];
                                Session["Ex_Inv"] = invalid;
                                if (valid.Count() > 0)
                                {
                                    gv_ClEx.DataSource = valid;
                                    gv_ClEx.DataBind();
                                    gv_ClEx.Visible = true;
                                    lblMsgLDB.Text = "Datos Cargados";
                                    lblMsgLDB.Visible = true;
                                }
                                else
                                {
                                    gv_ClEx.Visible = false;
                                    lblMsgLDB.Visible = false;
                                }
                                if (invalid.Count() > 0)
                                {
                                    gv_Error.DataSource = invalid;
                                    gv_Error.DataBind();
                                    gv_Error.Visible = true;
                                    lblELDB.Text = "Datos no Cargados";
                                    lblELDB.Visible = true;
                                }
                                else
                                {
                                    gv_Error.Visible = false;
                                    lblELDB.Visible = false;
                                }
                            }
                            else
                            {
                                lblMsgLDB.Visible = false;
                                lblELDB.Visible = false;
                                lblEL.Text = "El archivo no contiene datos Validos";
                                lblEL.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsgLDB.Visible = false;
                            lblELDB.Visible = false;
                            lblEL.Text = "El archivo no contiene datos";
                            lblEL.Visible = true;
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
                    lblEL.Text = "Por favor seleccione un archivo de excepciones a Cargar.";
                    lblEL.Visible = true;
                }
            }
            catch (Exception )
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
            Response.Redirect(@"~\CO\PG\Adm\ECartera.aspx");
        }
        protected void gv_ClEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_ClEx.PageIndex = e.NewPageIndex;
            gv_ClEx.DataSource = Session["Ex_Va"];
            gv_ClEx.DataBind();
        }

        protected void gv_Error_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_Error.PageIndex = e.NewPageIndex;
            gv_Error.DataSource = Session["Ex_Inv"];
            gv_Error.DataBind();
        }
    }
}