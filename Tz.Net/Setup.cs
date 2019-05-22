using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tz.Net
{
   public class Setup
   {
        public Setup() {
                       
        }

        public void Reset(string conn) {
            Tz.Data.Setup s = new Data.Setup(conn);
            s.Clear();
        }

        public void Execute(string conn) {
            try
            {
                Tz.Data.Setup s = new Data.Setup(conn);
                try
                {
                    s.CreateAccount();
                }
                catch (Exception ex) {
                    if (ex.Message.IndexOf("exist") == 0) {
                        throw ex;
                    }
                }
                try
                {
                    s.CreateClient();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("exist") == 0)
                    {
                        throw ex;
                    }
                }
                try
                {
                    s.CreateUser();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("exist") == 0)
                    {
                        throw ex;
                    }
                }
                try
                {
                    s.CreateServer();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("exist") == 0)
                    {
                        throw ex;
                    }
                }
                try
                {
                    s.CreateTable();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("exist") == 0)
                    {
                        throw ex;
                    }
                }


                try
                {
                    s.CreateField();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("exist") == 0)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            

        }
   }
}

