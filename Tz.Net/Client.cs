using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Tz.Net
{
    public class Client:INetImplimentor
    {
        private string _clientid;
        public string ClientID { get { return _clientid; } }
        public string ClientName { get; set; }
        public string ClientNo { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string OrganizationName { get; set; }
        public bool Status { get; set; }
        public string ClientHost { get; set; }

        private Data.Client dClient;
        /// <summary>
        /// 
        /// </summary>
        public Client() {

        }
        public Client(string clientID) {
            _clientid = clientID;
            ClientName = "";
            ClientNo = "";
            Load();
        }
        public Client(string clientName,
            string clientNo,
            string address,
            string state,
            string country,
            string email,
            string phoneNo,
            string orgName,
            string clientHost,bool status)
        {
            _clientid = "";
            ClientName = clientName;
            ClientNo = clientNo;
            Address = address;
            State = state;
            Country = country;
            Email = email;
            PhoneNo = phoneNo;
            OrganizationName = orgName;
            ClientHost = clientHost;
            Status = status;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {     

        DataTable dt = new DataTable();
            dClient = new Data.Client("");
            dt= dClient.GetClient(this.ClientID);
           Client c= dt.toList <Client> (new DataFieldMappings()                    
                    .Add( Tz.Data.TzAccount.Client.ClientName.Name, "ClientName")
                    .Add( Tz.Data.TzAccount.Client.ClientNo.Name, "ClientNo")
                    .Add( Tz.Data.TzAccount.Client.Address.Name, "Address")
                    .Add( Tz.Data.TzAccount.Client.State.Name,"State")
                    .Add( Tz.Data.TzAccount.Client.Country.Name, "Country")
                     .Add(Tz.Data.TzAccount.Client.Email.Name,"Email" )
                    .Add( Tz.Data.TzAccount.Client.PhoneNo.Name, "PhoneNo")
                    .Add( Tz.Data.TzAccount.Client.OrganizationName.Name, "OrganizationName")
                    .Add( Tz.Data.TzAccount.Client.Status.Name,"Status")
                    .Add( Tz.Data.TzAccount.Client.Host.Name, "ClientHost")
                    , null,null).FirstOrDefault();
            this.Merge<Client>(c);          
        }
        public static List<Client> GetClients() {
            var dClient = new Data.Client("");
            DataTable dt = new DataTable();
            dt = dClient.GetClients();
            if (dt == null) {
                return new List<Client>();
            }
          return  dt.toList<Client>(new DataFieldMappings()
                     .Add(Tz.Data.TzAccount.Client.ClientID.Name, "ClientID",true)
                     .Add(Tz.Data.TzAccount.Client.ClientName.Name, "ClientName")
                     .Add(Tz.Data.TzAccount.Client.ClientNo.Name, "ClientNo")
                     .Add(Tz.Data.TzAccount.Client.Address.Name, "Address")
                     .Add(Tz.Data.TzAccount.Client.State.Name, "State")
                     .Add(Tz.Data.TzAccount.Client.Country.Name, "Country")
                      .Add(Tz.Data.TzAccount.Client.Email.Name, "Email")
                     .Add(Tz.Data.TzAccount.Client.PhoneNo.Name, "PhoneNo")
                     .Add(Tz.Data.TzAccount.Client.OrganizationName.Name, "OrganizationName")
                     .Add(Tz.Data.TzAccount.Client.Status.Name, "Status")
                     .Add(Tz.Data.TzAccount.Client.Host.Name, "ClientHost")
                     , null, null).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Remove()
        {
            dClient = new Data.Client("");
            return dClient.Remove(ClientID);         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            dClient = new Data.Client("");
            if (_clientid == "")
            {
                _clientid = dClient.Save(ClientName,
                            ClientNo,
                            Address,
                            State,
                            Country,
                            Email,
                            PhoneNo,
                            OrganizationName,
                            Status,
                            ClientHost);
                if (_clientid != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {

                if ( dClient.Update(ClientID, ClientName,
                             ClientNo,
                             Address,
                             State,
                             Country,
                             Email,
                             PhoneNo,
                             OrganizationName,
                             Status,
                             ClientHost)) {
                    return true;
                } else {
                    return false;
                }                 
            }
            
        }
    }

}
