using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tz.Data;

namespace Tz.Deployment
{
    public class Setup
    {
        public Setup()
        {

        }

        public void Reset(string conn)
        {
            Tz.Data.Setup s = new Data.Setup(conn);
            s.Clear();
        }

        public void Execute(string conn)
        {
            try
            {
                Tz.Data.Setup s = new Data.Setup(conn);
                try
                {
                    s.CreateAccount();
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
                try
                {
                    s.CreateClientServer();
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
                    s.CreateDataScript();
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
                    s.CreateScriptIntend();
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
                    s.CreateImportEvents();
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("exist") == 0)
                    {
                        throw ex;
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void ExecuteClientSetup(string conn)
        {
            Tz.Data.Setup s = new Data.Setup(conn);
            try
            {
                s.CreateComponent();
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
                s.CreateComponentAttribute();
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
                s.CreateComponentModal();
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
                s.CreateComponentModalItem();
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
                s.CreateComponentModalRelation();
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
                s.CreateComponentLookup();
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
                s.CreateComponentLookUpItem();
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
    }
}
