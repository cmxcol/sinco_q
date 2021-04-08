using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTO_Adapter.SAP;
using WebApplication.Client.Web.WSDL_CC_MRP;
using WebApplication.Client.Web.WSDL_SO_MRP;
using Services.Proforma;
using Services.Simulador;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;
using System.Net.Mime;
using DTO_Adapter.SQL;
using System.Text.RegularExpressions;
using DTO_Adapter.Proforma;
using Infrastructure.Security.Usuario;
using WebApplication.Client.Web.App_Code;


namespace WebApplication.Client.Web.CO.PG.Usr
{
    public partial class Proforma : BPage
    {
        private List<proformaDatos> productList;
        double total = 0, Iva = 0;
        SimServ Price = new SimServ();
        ServMaster dt = new ServMaster();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) + "-" + (DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString());
               

        }
        private void CargarControles()
        {
            CtrlCtgCom(ServiciosGUI.GetDtosPrfm(1, "", "", "", "", "", "","","").ToList(), ref DDCom);//cargarComerciales       
            CtrlCtgCargo(ServiciosGUI.GetDtosPrfm(6, "", "", "", "", "", "", "", "").ToList(), ref DDCargo);//cargarCargos         

        }

        private static void CtrlCtgCom(List<proformaDatos> DTO, ref DropDownList obj)
        {
            if (obj.Items.Count > 0) return;
            (obj).DataSource = DTO.ToList();//muestra la lista del select        
            (obj).DataTextField = "nombre";
            (obj).DataValueField = "valor";//"iva"
            (obj).DataBind();
        }

        private static void CtrlCtgCargo(List<proformaDatos> DTO, ref DropDownList obj)
        {
            if (obj.Items.Count > 0) return;
            (obj).DataSource = DTO.ToList();//muestra la lista del select        
            (obj).DataTextField = "nombre";
            (obj).DataValueField = "valor";//"iva"
            (obj).DataBind();
            

            obj.Items.Add(new System.Web.UI.WebControls.ListItem("Otro...", "Otro"));

        }

        public void DDCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = DDCargo.SelectedValue;

            if (selected.CompareTo("Otro") == 0)
            {
                txtCargoAdd.Enabled = true;
                //txtnomMaterial.Enabled = true;
                pnlMsgCargo.Visible = true;
                //pnlMsgg2.Visible = false;
                contentPage.Visible = false;
                MoPoUpCargo.Show();
                
            }
            else
            {
               
            }

        }


        private  void CtrlCtgProd(List<proformaDatos> DTO)
        {
           
        }

        private static void CtrlCtgMedi(List<proformaDatos> DTO, ref DropDownList obj)
        {
            if (obj.Items.Count > 0) return;
            (obj).DataSource = DTO.ToList();//muestra la lista del select
            (obj).DataTextField = "nombre";
            (obj).DataValueField = "valor";
            (obj).DataBind();
        }

        protected void product_concat(object sender, EventArgs e)
        {
            this.productList = ServiciosGUI.GetDtosPrfm(2, "", "", "", "", "", "", "", "").ToList();
            int i = 0;
            string aux="";

            

            while (aux=="")
            {
                string codigo = productList[i].nombre;
                int p = 9;
                codigo = codigo.Substring(0, codigo.IndexOf('-') - 1);
                if (CodProducto.Text==codigo)
                {
                    aux = productList[i].nombre;
                    CodProducto.Text = aux;
                    onErrorGrid.Text = "";
                }
                if (i == productList.Count-1)
                {
                    onErrorGrid.Text = "No se encontro el cod del producto";
                    break;

                }
                i++;
            }
        }

        protected void Obra_Changed(object sender, EventArgs e)
        {
            limpiarControlesMat();
            if (txtCodigoObra.Text != "")
            {
                CargarControles();
                var Maestros = dt.GetMaestrosByObra(long.Parse(txtCodigoObra.Text));
                if (Maestros != null)
                {
                    txtNombreObra.Text = Maestros.NObra;
                    txtCodigoCliente.Text = Maestros.CodCliente.ToString();
                    txtNombreCliente.Text = Maestros.NCliente;
                    txtNIT.Text = Maestros.NIT.ToString();
                    txtDir.Text = Maestros.Dir;
                    DDCom.SelectedValue = Maestros.Comercial;
                    onErrorObra.Text = "";
                    txtSolMail.Enabled = true;
                    txtSolName.Enabled = true;
                    DDCargo.Enabled = true;
                    txtSolTel.Enabled = true;
                }
                else
                {
                    onErrorObra.Text = "No se encuentra la obra";
                    limpiarControlesMat();
                }
            }
            else
            {
                onErrorObra.Text = "Ingrese una Obra ID";
                limpiarControlesMat();
            }
        }

        protected void DatosSol_Changed(object sender, EventArgs e)
        {
            if (txtSolName.Text != "" && DDCargo.Text != "" && txtSolMail.Text != "")
            {
                onErrorSol.Text = "";
                CodProducto.Enabled = true;
                DDUniM.Enabled = true;
                TxtCantidad.Enabled = true;
                TxtValUnidad.Enabled = true;
                TextAreaObserv.Enabled = true;
                CtrlCtgMedi(ServiciosGUI.GetDtosPrfm(3, "", "", "", "", "", "", "", "").ToList(), ref DDUniM);//cargarUnidadesMedida
            }
            else
            {
                onErrorSol.Text = "Rellene todos los campos para pasar a la siguiente seccion";
                CodProducto.Enabled = false;
                DDUniM.Enabled = false;
                TxtCantidad.Enabled = false;
                TxtValUnidad.Enabled = false;
                TxtCantidad.Text = "";
                TxtValUnidad.Text = "";
                btn_Agregar_Prod.Enabled = false;
                GVProduct.Visible = false;
            }
        }

        protected void Prod_Changed(object sender, EventArgs e)
        {
            
            if (CodProducto.Text != "" && DDUniM.SelectedItem.Text != "" && TxtCantidad.Text != "" && TxtValUnidad.Text != "")
            {
                TxtValTotal.Text = calcularValorTotalUnidad(getIvaProd(CodProducto.Text)).ToString("N2");
                btn_Agregar_Prod.Enabled = true;
            }
            else
            {
                btn_Agregar_Prod.Enabled = false;
            }
        }

        protected string getIvaProd(string productName)
        {
            string iva = "";
            int i = 0;
            this.productList = ServiciosGUI.GetDtosPrfm(2, "", "", "", "", "", "", "", "").ToList();

            while (iva == "")
            {
                if (productName==productList[i].nombre)
                {
                    iva = productList[i].valor;
                }
                i++;
            }

            return iva;
        }

        protected void add_mat_btn(object sender, EventArgs e)
        {
            txtProdAdd.Enabled = true;
            txtnomMaterial.Enabled = true;
            pnlMsgg2.Visible = true;
            contentPage.Visible = false;
            MoPoUpMsg2.Show();
        }

        protected void addMattBtn_click(object sender, EventArgs e)
        {
            if (txtProdAdd.Text.Count() == 8) {
                if (txtProdAdd.Text != null && txtProdAdd.Text != ""  && txtnomMaterial.Text != null && txtnomMaterial.Text != "")
                {
                    string prod = txtProdAdd.Text+" - "+ txtnomMaterial.Text;
                    bool ivaChecked = CheckBoxIvaAdd.Checked;
                    if (ivaChecked) {
                        ServiciosGUI.GetDtosPrfm(5, "", "", "", "", "", "", prod, "1").ToList();
                    } else
                    {
                        ServiciosGUI.GetDtosPrfm(5, "", "", "", "", "", "", prod, "0").ToList();
                    }
                    txtProdAdd.Text = "";
                    CheckBoxIvaAdd.Checked = false;
                    txtProdAdd.Enabled = false;
                    pnlMsgg2.Visible = false;
                    contentPage.Visible = true;
                    OnErrorAddMat.Text = "";
                }else
                {
                    OnErrorAddMat.Text = "Ingrese un codigo y nombre para el producto";
                    txtProdAdd.Enabled = true;
                    txtnomMaterial.Enabled = true;
                    pnlMsgg2.Visible = true;
                    contentPage.Visible = false;
                    MoPoUpMsg2.Show();
                }
            }else
            {
                OnErrorAddMat.Text = "Codigo invalido, no cumple la cantidad de caracteres 8";
                txtProdAdd.Enabled = true;
                txtnomMaterial.Enabled = true;
                pnlMsgg2.Visible = true;
                contentPage.Visible = false;
                MoPoUpMsg2.Show();
            }
           
        }

        protected void addCargoBtn_click(object sender, EventArgs e)
        {
            if (txtCargoAdd.Text.Count() > 0)
            {
                string value = txtCargoAdd.Text;
                
                DDCargo.Items.Insert(0, (new System.Web.UI.WebControls.ListItem(value, value)) );
                DDCargo.ClearSelection();
                DDCargo.Items.FindByText(value).Selected = true;

                pnlMsgCargo.Visible = false;
                contentPage.Visible = true;
                OnErrorAddCargo.Text = "";
                txtCargoAdd.Text = "";
                txtCargoAdd.Enabled = false;
            }
            else
            {
                OnErrorAddCargo.Text = "Ingrese un Cargo";
                txtCargoAdd.Enabled = true;
                pnlMsgCargo.Visible = true;
                contentPage.Visible = false;
                MoPoUpCargo.Show();
            }

        }




        protected void add_Unidad(object sender, EventArgs e)
        {
            
            txtUnidadMedi.Visible = true;
            txtUnidadMedi.Enabled = true;
            DDUniM.Visible = false;
        }

        protected void add_Unidad_click(object sender, EventArgs e)
        {
            if (txtProdAdd.Text.Count() == 8)
            {
                if (txtProdAdd.Text != null && txtProdAdd.Text != "" && txtnomMaterial.Text != null && txtnomMaterial.Text != "")
                {
                    string prod = txtProdAdd.Text + " - " + txtnomMaterial.Text;
                    bool ivaChecked = CheckBoxIvaAdd.Checked;
                    if (ivaChecked)
                    {
                        ServiciosGUI.GetDtosPrfm(5, "", "", "", "", "", "", prod, "1").ToList();
                    }
                    else
                    {
                        ServiciosGUI.GetDtosPrfm(5, "", "", "", "", "", "", prod, "0").ToList();
                    }
                    txtProdAdd.Text = "";
                    CheckBoxIvaAdd.Checked = false;
                    txtProdAdd.Enabled = false;
                    pnlMsgg2.Visible = false;
                    contentPage.Visible = true;
                    OnErrorAddMat.Text = "";
                }
                else
                {
                    OnErrorAddMat.Text = "Ingrese un codigo y nombre para el producto";
                    txtProdAdd.Enabled = true;
                    txtnomMaterial.Enabled = true;
                    pnlMsgg2.Visible = true;
                    contentPage.Visible = false;
                    MoPoUpMsg2.Show();
                }
            }
            else
            {
                OnErrorAddMat.Text = "Codigo invalido, no cumple la cantidad de caracteres 8";
                txtProdAdd.Enabled = true;
                txtnomMaterial.Enabled = true;
                pnlMsgg2.Visible = true;
                contentPage.Visible = false;
                MoPoUpMsg2.Show();
            }

        }




        protected void show_content(object sender, EventArgs e)
        {
            txtProdAdd.Text = "";
            CheckBoxIvaAdd.Checked = false;
            txtProdAdd.Enabled = false;
            pnlMsgg2.Visible = false;
            contentPage.Visible = true;
        }

        protected void btn_Agregar_Prod_Click(object sender, EventArgs e)
        {
            if (GVProduct.Rows.Count >= 7)
            {
                onErrorGrid.Text = "Alcanzo el numero maximo de productos";
                TxtCantidad.Text = "";
                TxtValUnidad.Text = "";
                TxtValTotal.Text = "";
                btn_Agregar_Prod.Enabled = false;
                btn_Agregar_Prod.Visible = false;
                txtUnidadMedi.Visible = false;
                txtUnidadMedi.Enabled = false;
                txtUnidadMedi.Text = "";
                DDUniM.Visible = true;

            }
            else
            {
                onErrorGrid.Text = "";
                btn_Agregar_Prod.Visible = true;
                var LConfAjus = new List<List<object>>();
                var LConf = new List<object>();
                if (Session["Products"] != null)
                {
                    LConfAjus = (List<List<object>>)Session["Products"];
                }

                LConf.Add(CodProducto.Text);

                if (txtUnidadMedi.Text=="")
                {
                    LConf.Add(DDUniM.SelectedItem.Text);
                }
                else
                {
                    LConf.Add(txtUnidadMedi.Text);
                }
                LConf.Add(TxtCantidad.Text);
                LConf.Add(TxtValUnidad.Text);
                LConf.Add(calcularIvaUnidad(getIvaProd(CodProducto.Text)).ToString("N2"));
                LConf.Add(calcularValorTotalUnidad(getIvaProd(CodProducto.Text)).ToString("N2"));

                DataTable dtdatos = new DataTable();
                dtdatos.Columns.Add("Codigo Producto");
                dtdatos.Columns.Add("Unidad M");
                dtdatos.Columns.Add("Cantidad");
                dtdatos.Columns.Add("Valor Unidad");
                dtdatos.Columns.Add("Valor Iva");
                dtdatos.Columns.Add("Valor total");

                LConfAjus.Add(LConf);
                Session["Products"] = LConfAjus;
                var listConts = (from l in LConfAjus
                                 select new
                                 {
                                     Cod = l[0],
                                     Uni = l[1],
                                     Vol = l[2],
                                     VrUni = l[3],
                                     VrIva = l[4],
                                     VrTotal = l[5]
                                 }).ToList();
                GVProduct.DataSource = listConts;
                GVProduct.DataBind();
                GVProduct.Visible = true;
                TxtCantidad.Text = "";
                TxtValUnidad.Text = "";
                TxtValTotal.Text = "";
                CodProducto.Text = "";
                 txtUnidadMedi.Visible = false;
                txtUnidadMedi.Enabled = false;
                txtUnidadMedi.Text = "";
                DDUniM.Visible = true;
                btn_Agregar_Prod.Enabled = false;

                ValSubtotal.Text = calcularSubTotal().ToString("N2");
                ValIva.Text = calcularIvaNeto().ToString("N2");
                ValTotal.Text = calcularValorNeto().ToString("N2");
                if (GVProduct.Rows.Count >= 1)
                {
                    btnGenFactura.Enabled = true;
                    var a = btnGenFactura.BackColor;
                    btnGenFactura.BackColor = Color.FromArgb(14, 83, 217);
                    btnGenFactura.ForeColor = Color.White;
                }
                else
                {
                    btnGenFactura.Enabled = false;
                    btnGenFactura.BackColor = Color.FromArgb(224, 224, 224);
                    btnGenFactura.ForeColor = Color.FromArgb(128, 128, 128);
                }
            }
        }

        private double calcularIvaUnidad(string getValIVa)
        {
            double iva = 0, aux = 0;
            total =  (Int64.Parse(TxtCantidad.Text) * Double.Parse(TxtValUnidad.Text));
            if (getValIVa == "1")
            {
                aux = total / 1.19;
                iva = total - aux;
            }
            else
            {
                iva = 0;
            }
            return iva;
        }

        private double calcularIvaNeto()
        {
            double ivaNeto = 0;
           // string patron = @"[^\w]";
            //Regex regex = new Regex(patron);
            foreach (GridViewRow x in GVProduct.Rows)
            {
                string aux = (x.Cells[4].Text);
                ivaNeto = ivaNeto + Convert.ToDouble(aux);
            }
            return ivaNeto;
        }

        private double calcularValorTotalUnidad(string getValIVa)
        {

            double totalUnidad = 0.0;
            if (getValIVa == "1")
            {
                totalUnidad = (Convert.ToDouble(TxtCantidad.Text) * Convert.ToDouble(TxtValUnidad.Text)) / 1.19;
            }
            else
            {
                totalUnidad = (Convert.ToDouble(TxtCantidad.Text) * Convert.ToDouble(TxtValUnidad.Text));
            }
            return totalUnidad;
        }

        private double calcularValorNeto()
        {
            double ValorNeto = 0.0;
            // string patron = @"[^\w]";
            // Regex regex = new Regex(patron);
            //ValorNeto = (Convert.ToDouble(regex.Replace(ValIva.Text, "")) + Convert.ToDouble(regex.Replace(ValSubtotal.Text, "")));
            ValorNeto = (Convert.ToDouble(ValIva.Text) + Convert.ToDouble(ValSubtotal.Text));

            return ValorNeto;
        }


        private double calcularSubTotal()
        {
            double subTotal = 0;
           // string patron = @"[^\w]";
            //Regex regex = new Regex(patron);
            foreach (GridViewRow x in GVProduct.Rows)
            {
                //string aux = regex.Replace((x.Cells[5].Text), "");
                string aux = (x.Cells[5].Text);
                subTotal = subTotal + Convert.ToDouble(aux);
            }
            return subTotal;
        }

        private void limpiarControlesMat()
        {
            txtNombreObra.Text = "";
            txtCodigoCliente.Text = "";
            txtNombreCliente.Text = "";
            txtDir.Text = "";
            txtNIT.Text = "";
            txtSolName.Text = "";
            txtSolName.Enabled = false;
            txtSolTel.Text = "";
            txtSolTel.Enabled = false;
            txtSolMail.Text = "";
            txtSolMail.Enabled = false;
            CodProducto.Enabled = true;
            DDUniM.Enabled = false;
            TxtCantidad.Text = "";
            TxtCantidad.Enabled = false;
            TxtValUnidad.Text = "";
            TxtValUnidad.Enabled = false;
            TxtValTotal.Text = "";
            GVProduct.Visible = false;
            ValSubtotal.Text = "";
            ValIva.Text = "";
            onErrorGrid.Text = "";
            btn_Agregar_Prod.Visible = true;
            ValTotal.Text = "";
            Session["Products"] = null;
            btnGenFactura.Enabled = false;
            btnGenFactura.BackColor = Color.FromArgb(224, 224, 224);
            btnGenFactura.ForeColor = Color.FromArgb(128, 128, 128);
            GVProduct.DataSource = null;
            GVProduct.DataBind();
        }




        protected void GVProduct_RowCommand(object sender, CommandEventArgs e)
        {
            GVProduct.DeleteRow(int.Parse((e.CommandArgument).ToString()));
            if (GVProduct.Rows.Count == 0)
            {
                btnGenFactura.BackColor = Color.FromArgb(224, 224, 224);
                btnGenFactura.Enabled = false;
            }
        }

        protected void GVProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var LConfAjus = new List<List<object>>();
            List<object> LConf;
            int cont = 0;
            foreach (GridViewRow x in GVProduct.Rows)
            {
                if (cont != e.RowIndex)
                {
                    LConf = new List<object>();
                    for (int i = 0; i < 6; i++)
                    {
                        LConf.Add(x.Cells[i].Text);
                    }
                    LConfAjus.Add(LConf);
                }
                cont++;
            }
            Session["Products"] = LConfAjus;
            var listConts = (from l in LConfAjus
                             select new
                             {
                                 Cod = l[0],
                                 Uni = l[1],
                                 Vol = l[2],
                                 VrUni = l[3],
                                 VrIva = l[4],
                                 VrTotal = l[5]
                             }).ToList();
            GVProduct.DataSource = listConts;
            GVProduct.DataBind();
            ValSubtotal.Text = calcularSubTotal().ToString("N2");
            ValIva.Text = calcularIvaNeto().ToString("N2");
            onErrorGrid.Text = "";
            btn_Agregar_Prod.Visible = true;
           // string patron = @"[^\w]";
           // Regex regex = new Regex(patron);
            //ValTotal.Text = (Convert.ToDouble(regex.Replace(ValIva.Text, "")) + Convert.ToDouble(regex.Replace(ValSubtotal.Text, ""))).ToString("N2");
            ValTotal.Text = (Convert.ToDouble((ValIva.Text)) + Convert.ToDouble((ValSubtotal.Text))).ToString("N2");


        }

        protected void btn_Gen_Factura_Click(object sender, EventArgs e)
        {
            NewFactura();

        }




        protected void NewFactura()
        {
            string id = ((IUsr)Session["Usr"]).usr.CemexId.ToString();
            var NumFact = ServiciosGUI.GetDtosPrfm(4, txtCodigoCliente.Text, txtCodigoObra.Text, DDCom.SelectedValue, id, ValTotal.Text, txtFecha.Text, "", "").ToList();
            //Save(DocFate.Checked ? "Cotizacion" : "Proforma", txtCodigoObra.Text, txtCodigoCliente.Text, DDCom.SelectedValue.ToString(), DDSector.SelectedValue.ToString(), ValTotal.Text);
            //Parallel.Invoke(
            //() => Parallel.ForEach(GVMat.Rows.Cast<GridViewRow>(), (currentRow) =>
            //{
            //    SaveMats(NumFact, currentRow.Cells[0].Text.ToString(), double.Parse(currentRow.Cells[3].Text.ToString()), currentRow.Cells[1].Text.ToString());
            //}

            //    ),
            //() => SaveSolici(NumFact, txtSolName.Text, DDCargo.SelectedItem.ToString(), txtSolTel.Text, txtSolMail.Text)
            //);
            string patron = @"[^\w]";
            Regex regex = new Regex(patron);
            string fechaCod = regex.Replace(txtFecha.Text, "");
            string nomFact = txtCodigoObra.Text + "-" + fechaCod + NumFact[0].nombre + ".pdf";
            string BasePDF = "Factura.pdf", Nuevo = Server.MapPath("~/CO/Doc/Proforma/Generados/" + nomFact);
            PdfReader reader = new PdfReader(Server.MapPath("~/CO/Doc/Proforma/" + BasePDF));

            iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            Document document = new Document(size);
            FileStream fs = new FileStream(Nuevo, FileMode.Create, FileAccess.Write);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            PdfContentByte cb = writer.DirectContent;

            // select the font properties
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 8);

            // create the new page and add it to the pdf
            PdfImportedPage page = writer.GetImportedPage(reader, 1);
            cb.AddTemplate(page, 0, 0);

            // Nit Negrita
            cb.BeginText();
            string text = DropDownListNit.SelectedItem.Text
                ;
            cb.ShowTextAligned(3, text, 100, 510, 0);
            cb.EndText();

            cb.BeginText();
            text = "Nit: " + DropDownListNit.SelectedValue;
            cb.ShowTextAligned(3, text, 100, 500, 0);
            cb.EndText();


            // letra normal
            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Fecha:";
            
            cb.ShowTextAligned(3, text, 85, 482, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtFecha.Text;
            cb.ShowTextAligned(3, text, 200, 482, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "NIT:";
            cb.ShowTextAligned(3, text, 85, 471, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtNIT.Text;
            cb.ShowTextAligned(3, text, 200, 471, 0);
            cb.EndText();

            
            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Cliente:";
            cb.ShowTextAligned(3, text, 85, 460, 0);
            cb.EndText();

            

            cb.BeginText();
            text = txtCodigoCliente.Text + "                 " + txtNombreCliente.Text;
            cb.ShowTextAligned(3, text, 200, 460, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Obra:";
            cb.ShowTextAligned(3, text, 85, 449, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtCodigoObra.Text + "                 " + txtNombreObra.Text;
            cb.ShowTextAligned(3, text, 200, 449, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Dirección Facturación:";
            cb.ShowTextAligned(3, text, 85, 438, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtDir.Text;
            cb.ShowTextAligned(3, text, 200, 438, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Persona Solicitante:";
            cb.ShowTextAligned(3, text, 85, 425, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtSolName.Text;
            cb.ShowTextAligned(3, text, 200, 425, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Cargo:";
            cb.ShowTextAligned(3, text, 85, 414, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = DDCargo.SelectedItem.ToString();
            cb.ShowTextAligned(3, text, 200, 414, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Teléfono:";
            cb.ShowTextAligned(3, text, 85, 402, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtSolTel.Text;
            cb.ShowTextAligned(3, text, 200, 402, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 8);
            // Segunda Columna
            cb.BeginText();
            fechaCod = regex.Replace(txtFecha.Text, "");
            text = "PROFORMA No.       " + txtCodigoObra.Text + "-" + fechaCod + NumFact[0].nombre;
            cb.ShowTextAligned(3, text, 370, 480, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Comercial Responsable:";
            cb.ShowTextAligned(3, text, 455, 449, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = DDCom.SelectedItem.ToString();
            cb.ShowTextAligned(3, text, 530, 449, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Email:";
            cb.ShowTextAligned(3, text, 455, 402, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = txtSolMail.Text;
            cb.ShowTextAligned(3, text, 480, 402, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetFontAndSize(bf, 7);
            cb.BeginText();
            text = "Codigo Producto";
            cb.ShowTextAligned(3, text, 98, 383, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetFontAndSize(bf, 7);
            cb.BeginText();
            text = "Descripción";
            cb.ShowTextAligned(3, text, 286, 383, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetFontAndSize(bf, 7);
            cb.BeginText();
            text = "Unidad";
            cb.ShowTextAligned(3, text, 453, 383, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetFontAndSize(bf, 7);
            cb.BeginText();
            text = "Cantidad";
            cb.ShowTextAligned(3, text, 503, 383, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetFontAndSize(bf, 7);
            cb.BeginText();
            text = "Vr/Unitario";
            cb.ShowTextAligned(3, text, 569, 383, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.WHITE);
            cb.SetFontAndSize(bf, 7);
            cb.BeginText();
            text = "Vr/Total";
            cb.ShowTextAligned(3, text, 640, 383, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Observaciones:";
            cb.ShowTextAligned(3, text, 85, 235, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA_OBLIQUE, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Proforma valida por un mes aparir de la fecha generada;";
            cb.ShowTextAligned(3, text, 85, 220, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Importe en letras:";
            cb.ShowTextAligned(3, text, 85, 199, 0);
            cb.EndText();

            cb.BeginText();
            text = "SON:";
            cb.ShowTextAligned(3, text, 85, 182, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            var aux = "";
            string[] auxGrid;
            string anticipo = "                                  ";
            if (anticipoCheck.Checked)
            {
                anticipo = "                          ANTICIPO ";
            }

            foreach (GridViewRow p in GVProduct.Rows)
            {

                text = null;
                cb.BeginText();
                auxGrid = p.Cells[0].Text.Split(new[] { '-' }, 2);
                text = auxGrid[0] + anticipo + auxGrid[1];
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, 110, 360 - (p.RowIndex * 15), 0);
                cb.EndText();

                cb.BeginText();
                text = p.Cells[1].Text.Trim();
                cb.ShowTextAligned(3, text, 460, 360 - (p.RowIndex * 15), 0);
                cb.EndText();

                cb.BeginText();
                text = p.Cells[2].Text;
                cb.ShowTextAligned(2, text, 515, 360 - (p.RowIndex * 15), 0);
                cb.EndText();

                cb.BeginText();
                text = "$ " + p.Cells[3].Text;
                cb.ShowTextAligned(2, text, 590, 360 - (p.RowIndex * 15), 0);
                cb.EndText();

                cb.BeginText();
                text = p.Cells[5].Text;
                cb.ShowTextAligned(2, text, 660, 360 - (p.RowIndex * 15), 0);
                cb.EndText();
            }


            //Importe en Letras
            //aux = regex.Replace(ValTotal.Text, "");
            aux = ValTotal.Text;
            cb.BeginText();
            text = enletras(aux) + " PESOS";
            cb.ShowTextAligned(3, text, 115, 182, 0);
            cb.EndText();

            //OBSERVACION
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "       "+TextAreaObserv.Text;
            cb.ShowTextAligned(3, text, 228, 220, 0);
            cb.EndText();

            //Totales
            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "SubTotal:";
            cb.ShowTextAligned(3, text, 500, 198, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = ValSubtotal.Text.Trim('$');
            cb.ShowTextAligned(3, text, 625, 198, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "IVA (19%):";
            cb.ShowTextAligned(3, text, 500, 187, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);

            if (ivaChecked.Checked)
            {
                cb.BeginText();
                text = ValIva.Text.Trim('$');
                cb.ShowTextAligned(3, text, 626, 187, 0);
                cb.EndText();
            } else { 

            cb.BeginText();
            text = " --";
            cb.ShowTextAligned(3, text, 626, 187, 0);
            cb.EndText();
        }
            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Neto a Pagar:";
            cb.ShowTextAligned(3, text, 500, 177, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = ValTotal.Text.Trim('$');
            cb.ShowTextAligned(3, text, 625, 177, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Banco de Bogotá";
            cb.ShowTextAligned(3, text, 110, 160, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "9088881";
            cb.ShowTextAligned(3, text, 137, 152, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Helm Bank";
            cb.ShowTextAligned(3, text, 235, 160, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "12395265";
            cb.ShowTextAligned(3, text, 240, 152, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Davivienda";
            cb.ShowTextAligned(3, text, 360, 160, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "470-169985731";
            cb.ShowTextAligned(3, text, 350, 152, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "BBVA";
            cb.ShowTextAligned(3, text, 485, 160, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "297 950453";
            cb.ShowTextAligned(3, text, 470, 152, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Bancolombia";
            cb.ShowTextAligned(3, text, 610, 160, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Convenio 51245";
            cb.ShowTextAligned(3, text, 605, 152, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "No practicar retención, este documento es una proforma, sólo tiene efectos informativos para la generación de anticipos y los precios estipulados, no condiciona las facturas que se generen ni sus variaciones. Enviar soporte de";
            cb.ShowTextAligned(3, text, 85, 120, 0);
            cb.EndText();
            cb.BeginText();
            text = "consignación al correo ";
            cb.ShowTextAligned(3, text, 85, 112, 0);
            cb.EndText();

            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "ingresos.colombia@cemex.com.";
            cb.ShowTextAligned(3, text, 147, 112, 0);
            cb.EndText();
            

            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "Durante y posterior a la venta de materiales o prestación de servicios se podrán generar cargos adicionales que en esta factura proforma no están incluidos.";
            cb.ShowTextAligned(3, text, 85, 104, 0);
            cb.EndText();


            bf = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 6);
            cb.BeginText();
            text = "________________________________________________________________________________________";
            cb.ShowTextAligned(3, text, 255, 60, 0);
            cb.EndText();

            cb.BeginText();
            text = "Aceptada";
            cb.ShowTextAligned(3, text, 392, 52, 0);
            cb.EndText();

            document.Close();
            writer.Close();
            reader.Close();
            fs.Close();

            hyperlinkPreviewPDF.NavigateUrl = "../../Doc/Proforma/Generados/" + nomFact;
            MostrarMensaje(1, "Se ha generado la Proforma N° " + nomFact+"¿Desea enviarla?");

            string NombreFac =nomFact;
            //string path = "D:\\ADMAPPS\\Calidad\\SINCO Proforma\\WebApplication.Client.Web\\CO\\Doc\\Proforma\\Generados\\" + nomFact;
            //string path = "D:\\ADMAPPS\\APPS Versiones\\SINCO\\SINCO\\WebApplication.Client.Web\\CO\\Doc\\Proforma\\Generados\\" + nomFact;
            string path = "D:\\ADMAPPS\\Productivo\\SINCO\\CO\\Doc\\Proforma\\Generados\\" + nomFact;
            // string path = "C:\\Users\\jsanchezrjs\\Desktop\\JohanPruebas\\SINCO Proforma\\WebApplication.Client.Web\\CO\\Doc\\Proforma\\Generados\\" + nomFact;
            Session["path"] = path;
            Session["nomFac"] = NombreFac;
            //string confirmacion = Notificar_Email(nomFact, txtNombreCliente.Text, txtNombreObra.Text, ValSubtotal.Text, ValIva.Text, ValTotal.Text, txtSolMail.Text, path);


            //Notificar_Email(NumFact, txtNombreCliente.Text, txtNombreObra.Text, ValSubtotal.Text, ValIva.Text, ValTotal.Text, txtSolMail.Text, Nuevo);
            //MostrarMensaje(1, "El documento con ID: " + NumFact + " ha sido generado y enviado");
            ///* FileInfo fi = new FileInfo(Nuevo);
            // fi.Delete();*/

        }


        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }


        private void MostrarMensaje(int Tipo, string mensaje)
        {
            pnlMsgg.Visible = false;
            btnAceptarMsg.OnClientClick = null;
            lblMensaje.Text = mensaje;
            if (Tipo == 1)
            {
                lblMensaje.ForeColor = Color.Green; hyperlinkPreviewPDF.Visible = true;
                //btnAceptarMsg.OnClientClick = "recargar()";
            }
            else
            {
                lblMensaje.ForeColor = Color.Red; hyperlinkPreviewPDF.Visible = false;
            }
            btnAceptarMsg.Focus();
            pnlMsgg.Visible = true;
            MoPoUpMsg.Show();
            contentPage.Visible = false;

        }

        private void MostrarMensaje2(int Tipo, string mensaje)
        {
            pnlMsgg3.Visible = false;
            btnAceptarMsg2.OnClientClick = null;
            lblMensaje2.Text = mensaje;
            if (Tipo == 1)
            {
                lblMensaje2.ForeColor = Color.Green; hyperlinkPreviewPDF.Visible = true;
                btnAceptarMsg2.OnClientClick = "recargar()";
            }
            else
            {
                lblMensaje2.ForeColor = Color.Red; hyperlinkPreviewPDF.Visible = false;
            }
            btnAceptarMsg2.Focus();
            pnlMsgg3.Visible = true;
            MoPoUpMsg3.Show();
            contentPage.Visible = false;

        }

        protected void Cancelar_EmailBtn(object sender, EventArgs e)
        {
            pnlMsgg3.Visible = false;
            MoPoUpMsg3.Show();
            contentPage.Visible = true;
        }
            

        protected void Notificar_EmailBtn(object sender, EventArgs e)
        {
            
            string estado=Notificar_Email(Session["nomFac"].ToString(), txtNombreCliente.Text, txtNombreObra.Text, ValSubtotal.Text, ValIva.Text, ValTotal.Text, txtSolMail.Text, Session["path"].ToString());
            MostrarMensaje2(1, estado);
        }


        public static string Notificar_Email(string IdSolicitud, string Cliente, string Obra, string Sub, string IVA, string Total, string Email, string path)
        //Modulo principal de envio de mensajes de correo electronico
        {
             


            string Asunto = "Proforma N° : " + IdSolicitud + " Cliente: " + Cliente;

            List<string> destinatarios = new List<string>();
            List<string> Copiados = new List<string>();

            if (Email != "")
            {
                string[] auxEmail;
                auxEmail = Email.Split(';');

                for (int i = 0; i < auxEmail.Length; i++)
                {
                    if (auxEmail[i] != "")
                    {
                        destinatarios.Add(auxEmail[i]);
                    }

                }
            }
            /* IEnumerable<SelectableDTO> Recipents2 = destiny.GetRecipent(HttpContext.Current.User.Identity.Name);
             foreach (var x in Recipents2)
             {
                 destinatarios.Add(x.Name);
             }*/

            string Mensaje = ConstruirMensaje(Cliente, Obra, Sub, IVA, Total);
            // destinatarios.Add("julianalfonso.puentes@cemex.com");
             destinatarios.Add("solicitudes.quejas@cemex.com");

            

            string From = "SINCO@cemex.com";

            return sMail(Asunto, Mensaje, destinatarios, From, path);

        }


        private static string ConstruirMensaje(string Cliente, string Obra, string Sub, string IVA, string Total)//Contrucción del mensaje que irá en la notificación por E-mail
        {
            string Encabezado = " <table width=\"100%\" style=\"font-family:Arial;font-size:10pt;\"> " +
                                      " <tr><td align=\"justify\">MEncabezado</td></tr>" +
                                      " <tr><td align=\"justify\">&nbsp;</td></tr> " +
                                        //" <tr><td align=\"justify\"><b>ID SOLICITUD :  </b>" + ddlEstadoSolicitud.SelectedItem.Text + "</td></tr> " +
                                        " <tr><td align=\"justify\">&nbsp;</td></tr> " +
                                  " </table> ";
            Encabezado = Encabezado.Replace("MEncabezado", "De acuerdo a su solicitud, adjunto factura.");


            string Datos_Generales = " <table width=\"100%\" style=\"font-family:Arial;font-size:10pt;\"  > " +
                                    " <tr><td align=\"justify\"><b>DATOS GENERALES DE LA SOLICITUD</b> </td></tr> " +
                                     " <tr><td align=\"justify\"></td></tr>" +
                                    " <tr><td align=\"justify\"><b>Cliente:</b> DCodCliente</td></tr>" +
                                    " <tr><td align=\"justify\"><b>Obra:</b> DObra</td></tr> " +
                                    " <tr><td align=\"justify\"><b>Total:</b> DTotal</td></tr> " +
                                    " </table> ";
            Datos_Generales = Datos_Generales.Replace("DCodCliente", Cliente);
            Datos_Generales = Datos_Generales.Replace("DObra", Obra);
            Datos_Generales = Datos_Generales.Replace("DTotal", Total);







            string Mensaje = " <table width=\"100%\" style=\"font-family:Arial;font-size:10pt;\"  > " +
                                  " <tr><td align=\"justify\">Buen día</td></tr>" +
                                  " <tr><td align=\"justify\"> </td></tr>" +
                                  " <tr><td align=\"justify\"> &nbsp; </td></tr>" +
                                  " <tr><td align=\"justify\">" + Encabezado + "</td></tr>" +
                                  " <tr><td align=\"justify\"></td></tr>" +
                                  " <tr><td align=\"justify\">" + Datos_Generales + "</td></tr>" +
                                  " <tr><td align=\"justify\"> &nbsp;</td></tr>" +
                                  " <tr><td align=\"justify\"> </td></tr>" +
                                  " <tr><td align=\"justify\">  &nbsp;</td></tr>" +
                                    " <tr><td align=\"justify\">Cordialmente,</td></tr>" + " <tr><td align=\"justify\"><img src='cid:firma' /></td></tr>" +

                              " </table> ";

            //"<h3>Buen día</h3> <br></br>" +
            //             "<h3> " + Encabezado + "</h3> <br></br>" +
            //             "<h3> " + Datos_Generales + "</h3> <br></br>" +
            //              "<h3> " + TablaConf + "</h3><br></br>" +
            //               "<h3> " + Observacion + "</h3><br></br>" +
            //                   "<h3> Observación del autorizante: " + txtObservacion_Aut.Text + "</h3><br></br>" +
            //                       "<h3>Observación del CSR: " + txtObservacion_CSR.Text + "</h3><br></br>" +
            //              "<br></br><h3> Cordialmente, </h3> " +
            //              "<img src='cid:imagen' />";
            return Mensaje;
        }
        public static string sMail(String Subject, String MsjHtml, List<string> destinatarios, string From, string FilePath)//Construcción y envío del E-mail
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("10.26.3.42", 25);
                Attachment File = new Attachment(FilePath);
                //SmtpClient smtp = new SmtpClient("127.0.0.1", 25);
                // SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //smtp.Port = 587;
                //smtp.Credentials = new System.Net.NetworkCredential("cristofer951@gmail.com", "medina951*-");
                //smtp.EnableSsl = true;

                String MensajeHtml;
                mail.From = new MailAddress(From);

                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                MensajeHtml = MsjHtml;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(MensajeHtml, null, "text/html");
                LinkedResource firma = new LinkedResource(System.AppDomain.CurrentDomain.BaseDirectory + "CO\\Images\\FirmaSinco.jpg", MediaTypeNames.Image.Jpeg);
                firma.ContentId = "firma";
                htmlView.LinkedResources.Add(firma);
                mail.Attachments.Add(File);
                mail.AlternateViews.Add(htmlView);
                mail.Priority = System.Net.Mail.MailPriority.High;
                smtp.Timeout = 999999999;
                //smtp.Timeout = 0;
                try
                {
                    foreach (var l in destinatarios)
                    {
                        mail.To.Add(l);
                    }

                    smtp.Send(mail);
                    //FileInfo fi = new FileInfo(FilePath);

                    //fi.Delete();

                    return "OK";
                }
                catch (Exception e)
                {
                    return " No se puede enviar la proforma debido a un error en el campo email, por favor  Verifica e intenta de nuevo ";
                }
            }
            catch (Exception e)
            {
                #region Error
                return "No se Pudo realizar la Notificación " + e.Message;
                //String Error = String.Format("{0:yyyy-MM-dd}", DateTime.Today) + "|" + String.Format("{0:HH:mm:ss}", DateTime.Now) + "|" + "SCSAP_Q_EnvN" + "|" + "EnvioNotificaciones" + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + e.Message.ToString() + "|" + e.StackTrace.ToString();
                //Dts.Variables["Error"].Value = (Dts.Variables["Error"].Value.ToString() == String.Empty ? Error : Dts.Variables["Error"].Value.ToString() + ";" + Error);
                //Dts.TaskResult = (int)ScriptResults.Failure;
                throw;
                #endregion
            }
        }

    }


}

// protected void CalculaWebServ()
// {
//     txtPrUni.Enabled = true;
//     txtPrUni.Text = "0";
//     txtPrUni.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrUni.Text.ToString()));
//     txtPrTot.Text = "0";
//     txtPrTot.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrTot.Text.ToString()));

// }

// protected void DDMat_TextChanged(object sender, EventArgs e)
// {
//     if(TipoMat.SelectedValue!="Concreto" || ( DDFamilia.Text != "-" && DDNorma.Text != "-" && DDResistencia.Text != "-" && DDTamGrava.Text != "-" && DDTipoGrava.Text != "-" && DDEdad.Text != "-" && DDAsentamiento.Text != "-" && DDBombeo.Text != "-" && DDTipoCemento.Text != "-" && DDVariante.Text != "-"))
//     {
//         if(TipoMat.SelectedValue == "Concreto")
//         MaestroMaterial();
//         if (!txtCodMat.Text.Contains("0000000"))
//         {
//             txtPrUni.Enabled = false;

//             if (TipoMat.SelectedValue == "Concreto")
//             {
//                 CalculaPrecio();
//             }
//             else {
//                 CalculaWebServ();
//             }

//             btn_Agregar_Mat.Enabled = double.Parse(txtPrTot.Text.Replace(",", "").Replace(".", "").Replace("$", "").ToString()) / 100 > 0;
//         }
//         else
//         {
//             txtPrUni.Enabled = true;

//         }
//     }
// }
// protected void btn_Agregar_Mat_Click(object sender, EventArgs e)
// {
//     if (txtCodMat.Text != string.Empty & txtCodMat.Text.Length >= 8)
//     {
//         if (txtUni.Text != string.Empty & txtUni.Text.Length < 1)
//         {
//             MostrarMensaje(0, "La unidad es inválida");
//             return;
//         }

//         if (txtVol.Text == string.Empty & txtPrUni.Text == string.Empty & txtPrTot.Text == string.Empty)
//         {
//             MostrarMensaje(0, "Se tiene que ingresar un volumen y debe tener un precio");
//             return;
//         }


//         var LConfAjus = new List<List<object>>();
//         var LConf = new List<object>();
//         long mayor = 0;
//         if (Session["Mats"] != null)
//         {
//             LConfAjus = (List<List<object>>)Session["Mats"];
//         }
//         if (LConfAjus != null)
//             for (int i = 0; i <= LConfAjus.Count() - 1; i++)
//             {
//                 if (mayor < long.Parse((LConfAjus[i][0]).ToString()))
//                 {
//                     mayor = long.Parse((LConfAjus[i][0]).ToString());
//                 }
//             }

//         LConf.Add(txtCodMat.Text); //1
//         LConf.Add(txtDesc.Text); //2
//         LConf.Add(txtUni.Text); //3
//         LConf.Add(txtVol.Text); //4
//         LConf.Add(txtPrUni.Text); //5
//         LConf.Add(txtPrTot.Text); //6

//         SimServ Price = new SimServ();
//         DataTable dtdatos = new DataTable();
//         dtdatos.Columns.Add("CodObra");
//         dtdatos.Columns.Add("IdPedido");
//         dtdatos.Columns.Add("IdMaterial");
//         dtdatos.Columns.Add("IdCentro");
//         dtdatos.Columns.Add("Cantidad");
//         dtdatos.Columns.Add("PBase");
//         dtdatos.Columns.Add("PNeto");

//         DataRow row = dtdatos.NewRow();
//         row[0] = txtCodigoObra.Text;
//         row[1] = "1";
//         row[2] = txtCodMat.Text;
//         row[3] = txtPlanta.Text.ToUpper();
//         row[4] = double.Parse(txtVol.Text);
//         DT_ResponseSimulateOrderTaking DT_RSOT = new DT_ResponseSimulateOrderTaking();
//         DT_RSOT = SOrder(row, (DDSector.SelectedValue.Trim() == "" ? "0" : DDSector.SelectedValue.Trim().Length == 1 & int.Parse(DDSector.SelectedValue) < 10 ? "0" + DDSector.SelectedValue.Trim() : DDSector.SelectedValue.Trim()), DateTime.Parse(txtFecha.Text));
//         Subtotal = (ValSubtotal.Text != "" ? double.Parse(ValSubtotal.Text.Replace("$", "").Replace(".", "").Replace(",", "")) / 100 : 0) + double.Parse(txtPrTot.Text.Replace("$", "").Replace(".", "").Replace(",", "")) / 100;
//         Iva = (ValIva.Text != "" ? double.Parse(ValIva.Text.Replace("$", "").Replace(".", "").Replace(",", "")) / 100 : 0) + (DT_RSOT.ItemOutList != null ? double.Parse(DT_RSOT.ItemOutList[0].TaxTot.ToString()) * double.Parse(txtPrTot.Text.Replace("$", "").Replace(".", "").Replace(",", "")) / 10000 : 0);
//         total = Subtotal + Iva;
//         ValSubtotal.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(Subtotal.ToString()));
//         ValIva.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(Iva.ToString()));
//         ValTotal.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(total.ToString()));

//         LConfAjus.Add(LConf); //7
//         Session["Mats"] = LConfAjus;
//         var listConts = (from l in LConfAjus
//                          select new
//                          {
//                              Cod = l[0],
//                              Desc = l[1],
//                              Uni = l[2],
//                              Vol = l[3],
//                              VrUni = l[4],
//                              VrTotal = l[5]
//                          }).ToList();
//         Session["Con"] = listConts;
//         GVMat.DataSource = listConts;
//         GVMat.DataBind();
//         GVMat.Visible = true;
//         limpiarControlesMat();
//         btnGenFactura.Enabled = true;


//     }
//     else
//     {
//         MostrarMensaje(0, "Por favor ingrese un Código de Material válido");
//     }
//     txtCodMat.Focus();
// }
// protected void GVMat_RowCommand(object sender, CommandEventArgs e)
// {
//     GVMat.DeleteRow(int.Parse((e.CommandArgument).ToString()));

// }

// protected void GVMat_RowDeleting(object sender, GridViewDeleteEventArgs e)
// {
//     var LConfAjus = new List<List<object>>();
//     List<object> LConf;
//     int cont = 0;
//     foreach (GridViewRow x in GVMat.Rows)
//     {
//         if (cont != e.RowIndex)
//         {
//             LConf = new List<object>();
//             for (int i = 0; i < 6; i++)
//             {
//                 LConf.Add(x.Cells[i].Text);
//             }
//             LConfAjus.Add(LConf);
//         }
//         cont++;
//     }
//     Session["Mats"] = LConfAjus;
//     var listConts = (from l in LConfAjus
//                      select new
//                      {
//                          Cod = l[0],
//                          Desc = l[1],
//                          Uni = l[2],
//                          Vol = l[3],
//                          VrUni = l[4],
//                          VrTotal = l[5]
//                      }).ToList();
//     GVMat.DataSource = listConts;
//     GVMat.DataBind();
//     if (GVMat.Rows.Count == 0)
//     {
//         btnGenFactura.Enabled = false;
//     }
// }
// private void MostrarMensaje(int Tipo, string mensaje)
// {
//     lblMensaje.Text = mensaje;
//     if (Tipo == 1) { lblMensaje.ForeColor = Color.Green; }
//     else { lblMensaje.ForeColor = Color.Red; }
//     btnAceptarMsg.Focus();
//     pnlMsgg.Visible = true;
//     MoPoUpMsg.Show();
// }
// private void limpiarControlesMat()
// {
//     txtCodMat.Text = string.Empty;
//     txtDesc.Text = string.Empty;
//     txtUni.Text = string.Empty;//ClearSelection();
//     txtVol.Text = string.Empty;
//     txtPrUni.Text = string.Empty;
//     txtPrTot.Text = string.Empty;
//     DDFamilia.Text = "-";
//     DDNorma.Text = "-";
//     DDResistencia.Text = "-";
//     DDTamGrava.Text = "-";
//     DDTipoGrava.Text = "-";
//     DDEdad.Text = "-";
//     DDAsentamiento.Text = "-";
//     DDBombeo.Text = "-";
//     DDTipoCemento.Text = "-";
//     DDVariante.Text = "-";

//     txtPrUni.Enabled = false;
//     btn_Agregar_Mat.Enabled = false;
// }

//protected void MatType_Change(object sender, EventArgs e)
// {
//     try
//     {
//         switch(TipoMat.SelectedValue)
//         {
//             case "Concreto":
//                 FilterTitles.Visible = true;
//                 FilterFields.Visible = true;
//                 if(DisplayList.Visible ==true)
//                 {
//                     txtCodMat.Text = "";
//                     txtDesc.Text = "";
//                     txtUni.Text = "";
//                     txtVol.Text = "";
//                     txtPrUni.Text = "";
//                     txtPrTot.Text = "";
//                 }
//                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hide", "$('#ctl00_MainContent_DisplayList').hide();", true);
//                 break;

//             default:
//                 FilterTitles.Visible = false;
//                 FilterFields.Visible = false;

//                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "show", "$('#ctl00_MainContent_DisplayList').show();", true);

//                 List<MatbytypeDTO> listMat=dt.GetMaterialesPorTipo(TipoMat.SelectedValue);
//                 foreach (MatbytypeDTO x in listMat)
//                 {
//                     ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Insert"+ x.Codigo, "document.getElementById('ctl00_MainContent_DescAuto').innerHTML+=\"<li role='option' class='OptionAutoDesc' onclick='assign();' style='cursor:pointer;' value=" + x.Codigo + ">" + x.Descripcion + "</li>\";", true);
//                 }
//                 if(!listMat.Any(x=> x.Codigo == long.Parse(txtCodMat.Text==""?"-1":txtCodMat.Text) ) && txtCodMat.Text != "")
//                 {
//                     txtCodMat.Text = "";
//                     txtDesc.Text = "";
//                     txtUni.Text = "";
//                     txtVol.Text = "";
//                     txtPrUni.Text = "";
//                     txtPrTot.Text = "";
//                 }
//                 break;

//         }
//     }
//     catch(Exception d)
//     {
//         ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", "alert("+d+");", true);
//     }
// }

// protected void CalculaPrecio()
// {

//     if (txtVol.Text == "")
//     {
//         txtVol.Text = "1";
//     }
//     NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("SAPConnectionConfig");
//     SAPCredentialsDTO credentials = new SAPCredentialsDTO()
//     {
//         UserName = section["User"],
//         Password = section["Password"]
//     };

//     var pricel = Price.PrecioT(long.Parse(txtCodigoObra.Text), long.Parse(txtCodMat.Text), float.Parse(txtVol.Text), txtPlanta.Text, Price.GetZTERM(long.Parse(txtCodigoObra.Text), DDSector.SelectedValue), credentials);
//     txtPrTot.Text = pricel.Equals("Nulo") ? "0" : pricel;
//     txtPrTot.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrTot.Text));
//     txtPrUni.Text = pricel.Equals("Nulo") ? "0" : (double.Parse(pricel.ToString()) / double.Parse(txtVol.Text)).ToString();
//     txtPrUni.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrUni.Text));
// }

// protected void MaestroMaterial()
// {
//     SimServ Mat = new SimServ();
//     var Master = Mat.GetMatUM(DDFamilia.Text, DDNorma.Text, DDResistencia.Text,DDTamGrava.Text,DDTipoGrava.Text,DDEdad.Text,DDAsentamiento.Text,DDBombeo.Text,DDTipoCemento.Text,DDVariante.Text);
//     txtDesc.Text = Master.Desc;
//     txtCodMat.Text = Master.Mat;
//     txtUni.Text = Master.UM;

// }


// protected void txtPlanta_TextChange(object sender, EventArgs e)
// {
//     if(txtCodMat.Text!="")
//     {
//         DDMat_TextChanged(sender,e);
//     }
//     if(DDSector.SelectedValue != "0" && txtPlanta.Text != "")
//     {
//         TipoMat.Enabled = true;
//         TipoMat.SelectedValue = "Concreto";
//     }
//     else
//     {
//         TipoMat.Enabled = false;
//     }
// }


// protected int Save(string Tipo, string Obra, string Cliente, string Comercial, string Sector, string Total)
// {
//     ServProforma saver = new ServProforma();
//     return saver.Save(Tipo, Obra, Cliente, Comercial, Sector, long.Parse(Total.Replace("$", "").Replace(",", "").Replace(".", "")) / 100);
// }
// protected void SaveMats(int NumFact, string Mat, double Vol, string Deno)
// {
//     ServProforma saver = new ServProforma();
//     saver.SaveMats(NumFact, Mat, Vol, Deno);
// }
// protected void SaveSolici(int NumFact, string Name, string Cargo, string Tel, string Mail)
// {
//     ServProforma saver = new ServProforma();
//     saver.SaveSolici(NumFact, Name, Cargo, Tel, Mail);
// }
// protected void txtVol_TextChanged(object sender, EventArgs e)
// {
//     if (txtCodMat.Text != "")
//     {
//         if (txtPrUni.Text != "")
//         {
//             txtPrUni.Text = txtPrUni.Text.Contains("$") ? txtPrUni.Text : "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrUni.Text));
//             txtPrTot.Text = (double.Parse(txtVol.Text) * double.Parse(txtPrUni.Text.Replace(".", "").Replace(",", "").Replace("$", "")) / 100).ToString();
//             txtPrTot.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrTot.Text));
//             btn_Agregar_Mat.Enabled = double.Parse(txtPrTot.Text.Replace(",", "").Replace(".", "").Replace("$", "").ToString()) / 100 > 0;
//         }
//         else
//             DDMat_TextChanged(sender, e);
//     }
// }
// protected void txtPrUni_TextChanged(object sender, EventArgs e)
// {
//     txtPrUni.Text = txtPrUni.Text.Contains("$") ? txtPrUni.Text : "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrUni.Text));
//     txtPrTot.Text = (double.Parse(txtVol.Text) * double.Parse(txtPrUni.Text.Replace(".", "").Replace(",", "").Replace("$", "")) / 100).ToString();
//     txtPrTot.Text = "$" + string.Format("{0:#,##0.00}", double.Parse(txtPrTot.Text));
//     btn_Agregar_Mat.Enabled = double.Parse(txtPrTot.Text.Replace(",", "").Replace(".", "").Replace("$", "").ToString()) / 100 > 0;
// }
// protected void btnGenFactura_Click(object sender, EventArgs e)
// {
//     NewFactura();
// }

// protected void DDSector_SelectedIndexChanged(object sender, EventArgs e)
// {
//     if (DDSector.Text != "0")
//     {

//             DDFamilia.Enabled = true;
//             DDNorma.Enabled = true;
//             DDResistencia.Enabled = true;
//             DDTamGrava.Enabled = true;
//             DDTipoGrava.Enabled = true;
//             DDEdad.Enabled = true;
//             DDAsentamiento.Enabled = true;
//             DDBombeo.Enabled = true;
//             DDTipoCemento.Enabled = true;
//             DDVariante.Enabled = true;

//         txtVol.Enabled = true;
//         txtPlanta.Text = Price.GetCentro(long.Parse(txtCodigoObra.Text), DDSector.SelectedValue.ToString()).ToUpper();
//         TipoMat.Enabled = true;
//         TipoMat.SelectedValue = "Concreto";
//     }
//     else
//     {
//         DDFamilia.Enabled = false;
//         DDNorma.Enabled = false;
//         DDResistencia.Enabled = false;
//         DDTamGrava.Enabled = false;
//         DDTipoGrava.Enabled = false;
//         DDEdad.Enabled = false;
//         DDAsentamiento.Enabled = false;
//         DDBombeo.Enabled = false;
//         DDTipoCemento.Enabled = false;
//         DDVariante.Enabled = false;
//         txtVol.Enabled = false;
//         txtPlanta.Text = "";
//         TipoMat.Enabled = false;
//     }
// }
// public DT_ResponseSimulateOrderTaking SOrder(DataRow row, String idSector, DateTime dtEnt)
// {
//     WSDL_SO_MRP.DT_SimulateHeader DT_SH = new WSDL_SO_MRP.DT_SimulateHeader();
//     WSDL_SO_MRP.DT_SimulateItemCRM DT_SICRM = new WSDL_SO_MRP.DT_SimulateItemCRM();
//     WSDL_SO_MRP.DT_SimulatePartner DT_SP = new WSDL_SO_MRP.DT_SimulatePartner();
//     WSDL_SO_MRP.SI_IS_SimulateOrderTakingRequest SI_IS_SOTRQ = new WSDL_SO_MRP.SI_IS_SimulateOrderTakingRequest();
//     WSDL_SO_MRP.SI_IS_SimulateOrderTakingResponse SI_IS_SOTRP = new WSDL_SO_MRP.SI_IS_SimulateOrderTakingResponse();
//     WSDL_SO_MRP.DT_RequestSimulateOrderTakingCRM DT_RQSOTCRM = new WSDL_SO_MRP.DT_RequestSimulateOrderTakingCRM();
//     WSDL_SO_MRP.DT_ResponseSimulateOrderTaking DT_RPSOT = new WSDL_SO_MRP.DT_ResponseSimulateOrderTaking();
//     WSDL_SO_MRP.SI_IS_External_to_OrderTakingCRMClient SI_EOTCRMCLI = new WSDL_SO_MRP.SI_IS_External_to_OrderTakingCRMClient("ZB_ORDTAKINGCRM");
//     WSDL_SO_MRP.SI_IS_External_to_OrderTakingCRM SI_ISEOTCRM_I;

//     // Header
//     DT_SH.Doc_Type = "ZTA";
//     DT_SH.Sales_Org = "7460";
//     DT_SH.Channel = "00";
//     DT_SH.Division = idSector;
//     DT_SH.Ship_Cond = "01";
//     DT_SH.Country = "CO";
//     DT_SH.Currency = "COP";
//     //DT_SH.Purch_Ord = "8000000523";
//     DT_SH.Purch_Ord = "1";
//     DT_SH.Order_Dte = String.Format("{0:yyyyMMdd}", dtEnt);
//     //DT_SH.Order_Dte = "20120111";
//     DT_SH.Inbo_Outb = "";
//     DT_SH.Agent = "";
//     DT_SH.Cond_Man = "";
//     DT_SH.Cap_Min = "";
//     DT_SH.Cap_Max = "";

//     //Item
//     DT_SICRM.Item = "10";
//     DT_SICRM.Material = row[2].ToString().Trim();
//     DT_SICRM.Dlv_Grp = "";
//     DT_SICRM.Tar_Qty = "";
//     DT_SICRM.Tar_QU = "";
//     DT_SICRM.Req_Qty = row[4].ToString().Replace(',', '.');
//     DT_SICRM.Req_Dte = String.Format("{0:yyyyMMdd}", dtEnt);
//     DT_SICRM.Req_Time = "";
//     DT_SICRM.Item_typ = "";
//     //DT_SICRM.Paym_Trm = "ZCON";
//     DT_SICRM.Paym_Trm = "";
//     DT_SICRM.Plant = row[3].ToString().Trim().ToUpper();

//     //Partners
//     DT_SP.Part_Role = "SH";
//     DT_SP.Part_Number = row[0].ToString().Trim();

//     //Input
//     DT_RQSOTCRM.Header = DT_SH;
//     DT_RQSOTCRM.Item = new DT_SimulateItemCRM[1];
//     DT_RQSOTCRM.Item[0] = DT_SICRM;
//     DT_RQSOTCRM.Partners = new DT_SimulatePartner[1];
//     DT_RQSOTCRM.Partners[0] = DT_SP;

//     //Request
//     SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM = DT_RQSOTCRM;

//     //SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Header = new DT_SimulateHeader();
//     //SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Item = new DT_SimulateItemCRM[0];
//     //SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Partners = new DT_SimulatePartner[0];

//     SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Header = DT_SH;
//     SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Item[0] = DT_SICRM;
//     SI_IS_SOTRQ.MT_RequestSimulateOrderTakingCRM.Partners[0] = DT_SP;

//     SI_EOTCRMCLI.ClientCredentials.UserName.UserName = "ITOPERCOL";
//     SI_EOTCRMCLI.ClientCredentials.UserName.Password = "cemex2011";

//     //SI_EOTCRMCLI.ClientCredentials.UserName.UserName = "E0RASANDOV";
//     //SI_EOTCRMCLI.ClientCredentials.UserName.Password = "ramiro11";

//     SI_ISEOTCRM_I = SI_EOTCRMCLI;

//     DT_RPSOT = SI_EOTCRMCLI.SI_IS_SimulateOrderTaking(DT_RQSOTCRM);

//     //DT_RPSOT.MessageOutList = new DT_SimulateMessa[1];

//     if (DT_RPSOT.MessageOutList != null)
//     {
//         return DT_RPSOT;
//     }
//     else
//     {
//         //return Double.Parse((DT_RPSOT.ItemOutList[0].SubTot/100).ToString());
//         return DT_RPSOT;
//     }
// }



// protected void DocFate_CheckedChanged(object sender, EventArgs e)
// {
//     lblProforma.Text = DocFate.Checked ? "FORMATO COTIZACIÓN" : "FORMATO FACTURA PROFORMA";
// }



