using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Client.Web.App_Code;
using Services.Simulador;
using Services.Logs;
using DTO_Adapter.SQL;
using Infrastructure.Security.Usuario;
using System.Text;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using DTO_Adapter.CTG;
using WebApplication.Client.Web.App_Code;
namespace WebApplication.Client.Web.CO.PG.Usr
{
    public partial class DescVal : BPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    CargarControles();
            //    //MaxValAjuste = int.Parse(ServiciosGUI.GetMaxvalAjuste(101).First().Value);
            //}

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate("Numbers");
                if (Page.IsValid)
                {
                    //txtCodObra.Enabled = false;
                    //DCliente();
                    SimServ Validator = new SimServ();
                    if (CHKPlanta.SelectedItem.Value=="1")
                    {
                     
                        FillGrid(Validator.ValObra(Int64.Parse(txtCodObra.Text),true));
                    }
                    else
                    {
                        FillGrid(Validator.ValObra(Int64.Parse(txtCodObra.Text),false));
                    }
                    


                }
            }
            catch (Exception ex)
            {
                var a = ex;
                throw;
            }
        }

        
        //private static void LCtrlCtg(List<CtgDTO> DTO, ref DropDownList obj, String PValue)
        //{

            
        //    if (obj.Items.Count > 0) return;
        
        //    var o = new CtgDTO();
        //    if (DTO.Count != 1)
        //    {
        //        o.IdCtg = 0;
        //        o.NCtg = PValue;
        //        DTO.Add(o);
        //    }
         
        //    (obj).DataSource = DTO.Where(x => x.IdCtg != 19007).OrderBy((x) => x.IdCtg).ToList();
            
        //    (obj).DataTextField = "NCtg";
        //    (obj).DataValueField = "IdCtg";
        //    (obj).DataBind();
        //}




        //private void CargarControles()
        //{

        //    LCtrlCtg(ServiciosGUI.GetTipoAjuste(0).ToList(), ref DDLTipoAjuste, "<- Seleccione Tipo Ajuste ->");
         
        //    DDLTipoAjuste.Focus();

        //}


        //SA.Tipo_Ajuste = int.Parse(DDLTipoAjuste.SelectedValue);

        //private int ValidaciónAntesdeGuardar()
        //{
        //    if (DDLresponsableAjuste.SelectedValue == "0")
        //    {
        //        MostrarMensaje(0, "Por favor ingrese el responsable del Ajuste.. ");
        //        return 0;
        //    }
        //    if (DDLConceptoAjuste.SelectedValue == "265" && txtOtroConceptoAjuste.Text == string.Empty)
        //    {
        //        MostrarMensaje(0, "Por favor ingrese el Otro Concepto del Ajuste!! ");
        //        return 0;
        //    }
        //    if (txtCodigoObra.Text == string.Empty || int.Parse(txtCodigoObra.Text) < 60000000)
        //    {
        //        MostrarMensaje(0, "El Código de Obra no es Válido, por favor intente de nuevo.. ");
        //        return 0;
        //    }

        //    if (DDLPreciosInlcuyenImpuesto.SelectedValue == "-1")
        //    {
        //        MostrarMensaje(0, "Indique si los precios ingresados incluyen o no IVA..");
        //        return 0;
        //    }

        //    if (DDLTipoAjuste.SelectedValue != "0")
        //    {
        //        if (Session["Auth"] != null)
        //        {
        //            // var LAU = (List<List<object>>)Session["Auth"];
        //            var LAU = (List<List<object>>)Session["Autorizantes"];
        //            if (LAU.Count() > 0)
        //            {
        //                //if (DDLConceptoAjuste.SelectedValue == "255" && LAU.Count()<2)
        //                //{
        //                //    MostrarMensaje(0, "Para ajustes de Volumen, ingresar mínimo dos autorizantes");
        //                //    return 0;
        //                //}
        //                if (ConfAjusteHechas() == 1 && txtObservacionSol.Text != string.Empty)
        //                {
        //                    return 1;
        //                }
        //                else
        //                {
        //                    MostrarMensaje(0, "Por favor agregue una configuración de Ajuste (N° Factura) y Observación del ajuste para poder guardar");
        //                    return 0;
        //                }
        //            }
        //            else
        //            {
        //                MostrarMensaje(0, "Debe agregar al menos un autorizante!!");
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            MostrarMensaje(0, "Debe agregar al menos un autorizante!!");
        //            return 0;
        //        }
        //    }
        //    else
        //    {
        //        MostrarMensaje(0, "Eliga el Tipo de Ajuste");
        //        return 0;
        //    }


        //}


        //protected void DDLTipoAjuste_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (DDLTipoAjuste.SelectedValue == "321")
        //    {
        //        lblRefacturarOtraObra.Visible = true;
        //        CBRefacturarOtraObra.Visible = true;
        //    }
        //    else
        //    {
        //        lblRefacturarOtraObra.Visible = false;
        //        CBRefacturarOtraObra.Visible = false;
        //        CBRefacturarOtraObra.Checked = false;
        //        PanelObraFinal.Visible = false;
        //    }
        //    DDLTipoAjuste.Focus();
        //}

        private void FillGrid(IEnumerable<ValDTO> result)
        {
            if (result.Count() > 0)
            {
                gv_DataDesc.DataSource = result;
                gv_DataDesc.DataBind();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){$('#formulario').hide()});", true);
                if (gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "NACIONAL" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "GRANDE" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "MEDIANO" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "PEQUE&#209;O" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "INDEPENDIENTE" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "CONSTRUCTORA FILIAL" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "P&#218;BLICO GENERAL AUTOCONSTRUCTOR" && gv_DataDesc.Rows[0].Cells[4].Text.Trim() != "EMPRESA AUTOCONSTRUCTOR")
                {
                    gv_DataDesc.Visible = false;
                    if (gv_DataDesc.Rows[0].Cells[4].Text.Trim() == "EMPLEADO" || gv_DataDesc.Rows[0].Cells[4].Text.Trim() == "PROYECTO CEMEX")
                    {
                        Desc.Text = "El Cliente tiene segmentación: "+ gv_DataDesc.Rows[0].Cells[4].Text.Trim() + " .Valide el descuento con su supervisor";
                    }
                    else
                    {
                        
                        Desc.Text = "El Cliente no es del Segmento Constructor";
                        
                    }
                    Desc.Style.Remove("display");
                    Desc.Style.Add("display", "block");
                }
                else
                {



                    if (gv_DataDesc.Rows[0].Cells[7].Text != "&nbsp;")
                    {

                        gv_DataDesc.Visible = true;
                        Desc.Text = "El Descuento Máximo para la Obra es: " + gv_DataDesc.Rows[0].Cells[10].Text + " %";
                        Desc.Style.Remove("display");
                        Desc.Style.Add("display", "block");
                        ServLog desc = new ServLog();
                        desc.SavePLog(((IUsr)Session["Usr"]).usr.CemexId, int.Parse(gv_DataDesc.Rows[0].Cells[0].Text.Trim()),
                       RemoveDiacritics(gv_DataDesc.Rows[0].Cells[1].Text.Trim()),
                       int.Parse(gv_DataDesc.Rows[0].Cells[2].Text.Trim()),
                       RemoveDiacritics(gv_DataDesc.Rows[0].Cells[4].Text.Trim()),
                       RemoveDiacritics(gv_DataDesc.Rows[0].Cells[8].Text.Trim()),
                       gv_DataDesc.Rows[0].Cells[9].Text.Trim(),
                       System.Convert.ToDecimal(gv_DataDesc.Rows[0].Cells[10].Text.Trim()));
                    }
                    else if(gv_DataDesc.Rows[0].Cells[7].Text == "&nbsp;")
                    {
                        gv_DataDesc.Visible = false;

                        Desc.Text = "Error: Por favor verifique la relación segmentación cliente región y planta ";
                        Desc.Style.Remove("display");
                        Desc.Style.Add("display", "block");
                    }
                    else
                    {
                        gv_DataDesc.Visible = false;
                        Desc.Text = "La obra no está registrada aun. Diligencie el siguiente formulario";
                        Desc.Style.Remove("display");
                        Desc.Style.Add("display", "block");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){$('#formulario').show();fill();});", true);
                    }

                }

            }
            else
            {
                gv_DataDesc.Visible = false;
                Desc.Text = "La obra no está registrada aun. Diligencie el siguiente formulario";
                Desc.Style.Remove("display");
                Desc.Style.Add("display", "block");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){$('#formulario').show();fill();});", true);

            }
        }


     
   

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }

}