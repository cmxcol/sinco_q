using System;

namespace DTO_Adapter.WS
{
    #region CustomerStatement
    #region Request
    public class CsParametersDto
    {
        public String IdBusinessEntity { get; set; }
        public String IdSOrg { get; set; }
        public String CDist { get; set; }
        public String IdSector { get; set; }
        public String IdFCurr { get; set; }
        public String IdDCurr { get; set; }

        public CsParametersDto()
        {
            IdBusinessEntity = "";
            IdSOrg = "";
            CDist = "";
            IdSector = "";
            IdFCurr = "";
            IdDCurr = "";
        }
    }
    #endregion
    #region Response
    public class CsResponseDto
    {
        public String NBusinessEntity { get; set; }
        public String IdBlqBusinessEntity { get; set; }
        public String NBlqBusinessEntity { get; set; }
        public String IdCliente { get; set; }
        public String NCliente { get; set; }
        public String IdBlqCliente { get; set; }
        public String NBlqCliente { get; set; }
        public String IdCpObra { get; set; }
        public String NCpObra { get; set; }
        public String CartT { get; set; }
        public String SldAf { get; set; }
        public String CartN { get; set; }
        public String LimCr { get; set; }
        public String FConvCurr { get; set; }
        public String VComp { get; set; }
        public String Exception { get; set; }

        public CsResponseDto()
        {

        }
    }
    #endregion
    #endregion


    #region SimulateOrder

    #region Request

    public class SoParametersDto
    {
        public OrdHeaderDto Header { get; set; }
        public OrdItemDto[] Item { get; set; }
        public OrdPartnerDto[] Partner { get; set; }

        public SoParametersDto()
        {
            Header = null;
            Item = null;
            Partner = null;
        }
    }
    public class OrdHeaderDto
    {

        public string doc_Type { get; set; }
        public string sales_Org { get; set; }
        public string channel { get; set; }
        public string division { get; set; }
        public string ship_Cond { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string purch_Ord { get; set; }
        public string order_Dte { get; set; }
        public string inbo_Outb { get; set; }
        public string agent { get; set; }
        public string cond_Man { get; set; }
        public string cap_Min { get; set; }
        public string cap_Max { get; set; }
        public string ship_Method { get; set; }

        public OrdHeaderDto()
        {
            doc_Type = "";
            sales_Org = "";
            channel = "";
            division = "";
            ship_Cond = "";
            country = "";
            currency = "";
            purch_Ord = "";
            order_Dte = "";
            inbo_Outb = "";
            agent = "";
            cond_Man = "";
            cap_Min = "";
            cap_Max = "";
            ship_Method = "";
        }
    }
    public class OrdItemDto
    {
        public String Item { get; set; }
        public String Material { get; set; }
        public String Dlv_Grp { get; set; }
        public String Tar_Qty { get; set; }
        public String Tar_QU { get; set; }
        public String Req_Qty { get; set; }
        public String Req_Dte { get; set; }
        public String Req_Time { get; set; }
        public String Item_typ { get; set; }
        public String Paym_Trm { get; set; }
        public String Plant { get; set; }

        public OrdItemDto()
        {
            Item = "";
            Material = "";
            Dlv_Grp = "";
            Tar_Qty = "";
            Tar_QU = "";
            Req_Qty = "";
            Req_Dte = "";
            Req_Time = "";
            Item_typ = "";
            Paym_Trm = "";
            Plant = "";
        }
    }
    public class OrdPartnerDto
    {
        public String Part_Role { get; set; }
        public String Part_Number { get; set; }

        public OrdPartnerDto()
        {
            Part_Role = "";
            Part_Number = "";
        }
    }

    #endregion

    #region Response

    public class SoResponseDto
    {
        public SoRespOutList[] OutList { get; set; }
        public SoRespErrorMsgList[] ExceptionMessageList { get; set; }

        public SoResponseDto()
        {

        }
    }

    public class SoRespOutList
    {

        public string Item { get; set; }
        public string Material { get; set; }
        public decimal NetVal { get; set; }
        public bool NetValSpecified { get; set; }
        public decimal TaxVal { get; set; }
        public bool TaxValSpecified { get; set; }
        public string Currency { get; set; }
        public string SalesUnit { get; set; }
        public DateTime DelDate { get; set; }
        public bool DelDateSpecified { get; set; }
        public decimal SubTot { get; set; }
        public bool SubTotSpecified { get; set; }
        public decimal TaxTot { get; set; }
        public bool TaxTotSpecified { get; set; }
        public string ItemCat { get; set; }
        public string Plant { get; set; }
        public string ShipPoint { get; set; }
        public SoRespCondList[] Conditions { get; set; }


        public SoRespOutList()
        {

        }
    }

    public class SoRespCondList
    {

        public string Cond_Code { get; set; }
        public string Cond_Name { get; set; }
        public string Cond_Value { get; set; }
        public string Cond_Type { get; set; }
        public string Value_Cond { get; set; }
        public string Cond_Curr { get; set; }

        public SoRespCondList()
        {

        }
    }

    public class SoRespErrorMsgList
    {
        public String Type { get; set; }
        public String Id { get; set; }
        public String Number { get; set; }
        public String Message { get; set; }
        public String MessageV1 { get; set; }
        public String MessageV2 { get; set; }
        public String MessageV3 { get; set; }
        public String MessageV4 { get; set; }

        public SoRespErrorMsgList()
        {

        }
    }

    #endregion

    #endregion
}
