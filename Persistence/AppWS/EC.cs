using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DTO_Adapter.WS;
using Persistence.AppWS.WS;


namespace Persistence.AppWS
{
    public class CustomerStatement : IWService<CsParametersDto, CsResponseDto>
    {
        private DT_CustomerStatementReq _request;
        private DT_CustomerStatementResp _response;
        private BS_SINCO_PRD_GBL_SI_OS_CustomerStatement_x_x _exec;

        public CustomerStatement(String wsUrl)
        {
            _request = new DT_CustomerStatementReq();
            _response = new DT_CustomerStatementResp();
            _exec = new BS_SINCO_PRD_GBL_SI_OS_CustomerStatement_x_x(wsUrl);
        }
        public void RequestParameters(CsParametersDto parameters)
        {
            if (!(_request != null & _response != null & _exec != null))
            {
                throw new NullReferenceException("");
            }
            _request.I_KUNNR = parameters.IdBusinessEntity;
            _request.I_VKORG = parameters.IdSOrg;
            _request.I_VTWEG = parameters.CDist;
            _request.I_SPART = parameters.IdSector;
            _request.I_FCURR = parameters.IdFCurr;
            _request.I_TCURR = parameters.IdDCurr;
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
        public CsResponseDto Execute()
        {
            if (_request != null & _response != null & _exec != null)
            {
                var result = _exec.requestCustomerStatement(_request);
                var response = new CsResponseDto();
                if (result != null)
                {
                    if (result.E_EXCEPTION == string.Empty | result.E_EXCEPTION == null)
                    {
                        response.NBusinessEntity = result.E_CNAME;
                        response.IdBlqBusinessEntity = result.E_AUFSO;
                        response.NBlqBusinessEntity = result.E_TAUSO;
                        response.IdCliente = result.E_BITOC;
                        response.NCliente = result.E_BITON;
                        response.IdBlqCliente = result.E_AUFSD;
                        response.NBlqCliente = result.E_TAUSD;
                        response.IdCpObra = result.E_ZTERM;
                        response.NCpObra = result.E_NTERM;
                        response.CartT = result.E_CARTT;
                        response.SldAf = result.E_SLDAF;
                        response.CartN = result.E_CARNT;
                        response.LimCr = result.E_LIMCR;
                        response.FConvCurr = result.E_UKURS;
                        response.VComp = result.E_IPFAC;
                    }
                    else
                    {
                        response.Exception = result.E_EXCEPTION;
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
