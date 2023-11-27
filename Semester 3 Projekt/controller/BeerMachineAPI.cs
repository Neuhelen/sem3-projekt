using Opc.UaFx.Client;
using System.Diagnostics;

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

        public int get_produced_bad()
        {
            var produced_bad_value = common_get("product.bad");
            return (UInt16) produced_bad_value.Value;
        }

        public int get_produced_good()
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

        public int get_Stop_Reason()
        {
            var stop_reason = common_get("Cube.Admin.StopReason.ID");
            return (int) stop_reason.Value;
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

        public void stop()
        {

            common_post("Cube.Command.CntrlCmd", 3);

            common_post("Cube.Command.CmdChangeRequest", true);
        }

        public string stop_Reasons()
        {

            int stopCode = get_Stop_Reason();
            stop(); 

            string stop_reason;

            switch (stopCode)
            {
                case 10:
                    stop_reason = "Empty inventory";
                    break;
                case 11:
                    stop_reason = "Maintenance needed";
                    break;
                case 12:
                    stop_reason = "Manual stop";
                    break;
                case 13:
                    stop_reason = "Motor power loss";
                    break;
                default:
                    stop_reason = "Manual abort";
                    break;
            }

            return stop_reason;
        }

        public string state_Current()
        {

            int stateCode = get_Current_State();

            string state_current;

            switch (stateCode)
            {
                case 2:
                    state_current = "Stopped";
                    break;
                case 4:
                    state_current = "Idle";
                    break;
                case 5:
                    state_current = "Suspended";
                    break;
                case 9:
                    state_current = "Aborted";
                    break;
                default:
                    state_current = "Complete";
                    break;
            }

            return state_current;
        }
        
        private Stopwatch stopwatch = new Stopwatch();
        private double productionSpeed; 

        //This function measures the time taken to produce a desired amount of beers. 
        public void measureBeersPerSecond(int desiredAmount)
        {
            stopwatch.Start();

            //While the desired amount hasn't been reached, get the produced anounnt every second. 
            int producedAmount = 0;
            while (producedAmount < desiredAmount)
            {
                producedAmount = get_produced();

                System.Threading.Thread.Sleep(1000); 
            }

            stopwatch.Stop();

            double time = stopwatch.Elapsed.TotalSeconds;

            //if time has elapsed, write the production speed of beers per second to the Console. 
            if (time > 0)
            {
                productionSpeed = producedAmount / time;
                Console.WriteLine($"The production speed of beers per second is: {productionSpeed}");
                Console.WriteLine($"The production speed was set to: {get_cur_mach_speed()}");
            }
            else
            {
                Console.WriteLine("There has been an error.");
            }

            stopwatch.Reset();
        
        }
    }
}
