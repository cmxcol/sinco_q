using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using DTO_Adapter.WS;
using Persistence.AppWS.WS;


namespace Persistence.AppWS
{
    public class SOrder : IWService<SoParametersDto, SoResponseDto>
    {
        private DT_RequestSimulateOrderTakingCRM _request;
        private DT_ResponseSimulateOrderTaking _response;
        private ZS_ORDTAKINGCRM _exec;

        public SOrder(String wsUrl)
        {
            _request = new DT_RequestSimulateOrderTakingCRM();
            _response = new DT_ResponseSimulateOrderTaking();
            _exec = new ZS_ORDTAKINGCRM(wsUrl);
        }
        public void RequestParameters(SoParametersDto parameters)
        {
            if (!(_request != null & _response != null & _exec != null))
            {
                throw new NullReferenceException("");
            }

            /* Header */
            var header = new DT_SimulateHeader
                             {
                                 Doc_Type = parameters.Header.doc_Type,
                                 Sales_Org = parameters.Header.sales_Org,
                                 Channel = parameters.Header.channel,
                                 Division = parameters.Header.division,
                                 Ship_Cond = parameters.Header.ship_Cond,
                                 Country = parameters.Header.country,
                                 Currency = parameters.Header.currency,
                                 Purch_Ord = parameters.Header.purch_Ord,
                                 Order_Dte = parameters.Header.order_Dte,
                                 Inbo_Outb = parameters.Header.inbo_Outb,
                                 Agent = parameters.Header.agent,
                                 Cond_Man = parameters.Header.cond_Man,
                                 Cap_Min = parameters.Header.cap_Min,
                                 Cap_Max = parameters.Header.cap_Max
                             };
            _request.Header = header;

            /* Items */

            var p = parameters.Item.Length;
            var item = new DT_SimulateItemCRM[p];

            for (int i = 0; i < p; i++)
            {
                item[i] = new DT_SimulateItemCRM
                              {
                                  Item = parameters.Item[i].Item,
                                  Material = parameters.Item[i].Material,
                                  Dlv_Grp = parameters.Item[i].Dlv_Grp,
                                  Tar_Qty = parameters.Item[i].Tar_Qty,
                                  Tar_QU = parameters.Item[i].Tar_QU,
                                  Req_Qty = parameters.Item[i].Req_Qty,
                                  Req_Dte = parameters.Item[i].Req_Dte,
                                  Req_Time = parameters.Item[i].Req_Time,
                                  Item_typ = parameters.Item[i].Item_typ,
                                  Paym_Trm = parameters.Item[i].Paym_Trm,
                                  Plant = parameters.Item[i].Plant
                              };
            }
            _request.Item = item;

            /* Partners */

            p = parameters.Partner.Length;
            var partners = new DT_SimulatePartner[p];

            for (int i = 0; i < p; i++)
            {
                partners[i] = new DT_SimulatePartner();
                partners[i].Part_Number = parameters.Partner[i].Part_Number;
                partners[i].Part_Role = parameters.Partner[i].Part_Role;
            }
            _request.Partners = partners;

        }
        public void Credentials(String user, String password)
        {
            if (_request != null & _response != null & _exec != null)
            {
                _exec.Credentials = new NetworkCredential(user, password);
            }
            else
            {
                throw new NullReferenceException("");
            }
        }
        public SoResponseDto Execute()
        {
            if (_request != null & _response != null & _exec != null)
            {
                var result = _exec.SI_IS_SimulateOrderTaking(_request);
                var response = new SoResponseDto();
                if (result != null)
                {
                    if (result.MessageOutList == null)
                    {
                        var cI = result.ItemOutList.Length;
                        var outlist = new SoRespOutList[cI];
                        for (var i = 0; i < cI; i++)
                        {
                            outlist[i] = new SoRespOutList
                                             {
                                                 Item = result.ItemOutList[i].Item,
                                                 Material = result.ItemOutList[i].Material,
                                                 NetVal = result.ItemOutList[i].NetVal,
                                                 NetValSpecified = result.ItemOutList[i].NetValSpecified,
                                                 TaxVal = result.ItemOutList[i].TaxVal,
                                                 TaxValSpecified = result.ItemOutList[i].TaxValSpecified,
                                                 Currency = result.ItemOutList[i].Currency,
                                                 SalesUnit = result.ItemOutList[i].SalesUnit,
                                                 DelDate = result.ItemOutList[i].Del_Date,
                                                 DelDateSpecified = result.ItemOutList[i].Del_DateSpecified,
                                                 SubTot = result.ItemOutList[i].SubTot,
                                                 SubTotSpecified = result.ItemOutList[i].SubTotSpecified,
                                                 TaxTot = result.ItemOutList[i].TaxTot,
                                                 TaxTotSpecified = result.ItemOutList[i].TaxTotSpecified,
                                                 ItemCat = result.ItemOutList[i].ItemCat,
                                                 Plant = result.ItemOutList[i].Plant,
                                                 ShipPoint = result.ItemOutList[i].ShipPoint
                                             };

                            var cC = result.ItemOutList[i].Conditions.Length;
                            var conditions = new SoRespCondList[cC];
                            for (var j = 0; j < cC; j++)
                            {
                                conditions[j] = new SoRespCondList
                                                    {
                                                        Cond_Code = result.ItemOutList[i].Conditions[j].Cond_Code,
                                                        Cond_Name = result.ItemOutList[i].Conditions[j].Cond_Name,
                                                        Cond_Value = result.ItemOutList[i].Conditions[j].Cond_Value,
                                                        Cond_Type = result.ItemOutList[i].Conditions[j].Cond_Type,
                                                        Value_Cond = result.ItemOutList[i].Conditions[j].Value_Cond,
                                                        Cond_Curr = result.ItemOutList[i].Conditions[j].Cond_Curr
                                                    };
                            }
                            outlist[i].Conditions = conditions;
                        }
                        response.OutList = outlist;
                    }
                    else
                    {
                        var cont = result.MessageOutList.Length;
                        var eMsgList = new SoRespErrorMsgList[cont];
                        for (var i = 0; i < cont; i++)
                        {
                            eMsgList[i] = new SoRespErrorMsgList
                                              {
                                                  Type = result.MessageOutList[i].Type,
                                                  Id = result.MessageOutList[i].ID,
                                                  Number = result.MessageOutList[i].Number,
                                                  Message = result.MessageOutList[i].Message,
                                                  MessageV1 = result.MessageOutList[i].MessageV1,
                                                  MessageV2 = result.MessageOutList[i].MessageV2,
                                                  MessageV3 = result.MessageOutList[i].MessageV3,
                                                  MessageV4 = result.MessageOutList[i].MessageV4
                                              };
                        }
                        response.ExceptionMessageList = eMsgList;
                    }
                    _exec.Dispose();
                    return response;
                }
                throw new NullReferenceException();
            }
            throw new NullReferenceException();
        }
    }
}
