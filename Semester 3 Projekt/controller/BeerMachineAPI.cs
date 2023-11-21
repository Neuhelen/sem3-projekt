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

        private Opc.UaFx.OpcValue common_get(String endpoint)
        {
            var res = client.ReadNode(this.BASEAPI + endpoint);
            return res;
        }

        private bool common_post(String endpoint, object value)
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

        public float get_cur_mach_speed()
        {
            var mach_speed_value = common_get("Cube.Status.CurMachSpeed");
            return (float) mach_speed_value.Value;
        }

        public int get_produced()
        {
            var produced_value = common_get("product.produced");
            return (UInt16) produced_value.Value;
        }

        public int get_prodcued_bad()
        {
            var produced_bad_value = common_get("product.bad");
            return (UInt16) produced_bad_value.Value;
        }

        public int get_prodcued_good()
        {
            var produced_good_value = common_get("product.good");
            return (UInt16) produced_good_value.Value;
        }

        public bool set_production_amount(float quantity)
        {
            bool success = common_post("Cube.Command.Parameter[2].Value", quantity);
            return success;
        }

        public bool set_production_speed(float speed)
        {
            bool success = common_post("Cube.Command.MachSpeed", speed);
            return success;
        }
    }
}
