using Opc.UaFx.Client;
using Semester_3_Projekt.Classes;

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

        public int get_Ingredient_Amount(string Ingredient)
        {
            var Ingredient_Amount_value = common_get("Inventory."+Ingredient);
            return (UInt16)Ingredient_Amount_value.Value;
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

		public bool set_production_Product(float id)
		{
			bool success = common_post("Cube.Command.Parameter[1].Value", id);
			return success;
        }

        public bool set_production_Batch(float id)
        {
            bool success = common_post("Cube.Command.Parameter[0].Value", id);
            return success;
        }

        public int get_Current_State()
        {
            var current_state = common_get("Cube.Status.StateCurrent");
            return (int)current_state.Value;
        }
        public int get_Control_Command()
        {
            var control_command = common_get("Cube.Command.CntrlCmd");
            return (int)control_command.Value;
        }

        public bool get_Command_Change_Request()
        {
            var control_command = common_get("Cube.Command.CmdChangeRequest");
            return (bool)control_command.Value;
        }

        public int get_Stop_Reason()
        {
            var stop_reason = common_get("Cube.Admin.StopReason");
            return (int)stop_reason.Value;
        }

        public int get_Current_BatchID()
        {
            var current_batchID = common_get("Cube.Status.Parameter.Parameter[0]");
            return (int)current_batchID.Value;
        }

        

        DBInsert dbInsert = new DBInsert();

        //This function stops the production and logs it. 
        public bool stop(string stopCode)
        {
            bool success = common_post("Cube.Command.CntrlCmd", 3);

            common_post("Cube.Command.CmdChangeRequest", true);

            dbInsert.addLog(get_Current_BatchID(), stopCode);

            return success;
        }

        //This function starts the production. 
        public bool start()
        {
            bool success = common_post("Cube.Command.CntrlCmd", 2);

            common_post("Cube.Command.CmdChangeRequest", true);

            dbInsert.addLog(get_Current_BatchID(), "Manual Start");

            return success;
        }

        //This function continues the production of the batch and logs it
        public bool continue_production()
        {
            bool success = start();

            dbInsert.addLog(get_Current_BatchID(), "Manual Continue");

            return success;
        }

        //This function is used on the creation of a batch in order to set and log the input information. 
        public void batchCreation(string logMessage)
        {
            dbInsert.addLog(get_Current_BatchID(), "Batch Creation");
        }

        //This function is used on the completion of a batch in order to log the results. 
        public void logSuccess(string logMessage)
        {
            dbInsert.addLog(get_Current_BatchID(), "Batch Completed");
        }
    }
}
