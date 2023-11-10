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
        public int get_Current_State()
        {
            var current_state = common_get("Cube.Status.StateCurrent");
            return (int) current_state.Value;
        }
        public int get_Control_Command()
        {
            var control_command = common_get("Cube.Command.CntrlCmd");
            return (int) control_command.Value;
        }

        public bool get_Command_Change_Request()
        {
            var control_command = common_get("Cube.Command.CmdChangeRequest");
            return (bool) control_command.Value;
        }

        public void stop()
        {

            common_post("Cube.Command.PackMLCmd", 3);

            common_post("Cube.Command.CmdChangeRequest", true);
        }

        public void ingredientStop()
        {

            if ((int)common_get("Cube.Admin.StopReason").Value == 10)
            {
                stop();
            }
        }

    }
}
