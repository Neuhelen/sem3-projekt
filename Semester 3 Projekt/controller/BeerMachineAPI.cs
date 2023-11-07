using Opc.UaFx.Client;

namespace Semester_3_Projekt.controller
{
    public sealed class BeerMachineAPI
    {
        // Singleton shenaniganz
        private static BeerMachineAPI instance = null;
        public static BeerMachineAPI Instance { 
            get { 
                if (instance == null)
                {
                    instance = new BeerMachineAPI();
                }
                return instance;
                }
        }
        // Connection variables
        String connection_string = "opc.tcp://localhost:4840/";
        OpcClient client = null;

        // API REFS
        String BASEAPI = "ns=6;s=::Program:";

        // Constructor for initializing the client
        private BeerMachineAPI()
        {
            this.client = new OpcClient(this.connection_string);
            client.Connect();
        }

        public Opc.UaFx.OpcValue common_get(String endpoint)
        {
            var res = client.ReadNode(this.BASEAPI + endpoint);
            return res;
        }

        public bool common_post(String endpoint, object value)
        {
            var res = client.WriteNode(this.BASEAPI + endpoint, value);
            if (res.IsGood)
            {
                return true;
            } else
            {
                return false;
            }
        }



    }
}
