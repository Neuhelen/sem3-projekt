using Opc.UaFx.Client;
using Semester_3_Projekt.Classes;
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
        
        //String connection_string = "opc.tcp://192.168.0.122:4840/";
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

        public float get_Ingredient_Amount(string Ingredient)
        {
            var Ingredient_Amount_value = common_get("Inventory."+Ingredient);
            return (float)Ingredient_Amount_value.Value;
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
            var stop_reason = common_get("Cube.Admin.StopReason.Value");
            
            return (int)stop_reason.Value;
        }

        public int get_Current_BatchID()
        {
            var current_batchID = common_get("Cube.Status.Parameter.Parameter[0].Value");
            if(current_batchID.Value != null)
            {
                return (int)current_batchID.Value;
            } else
            {
                return -1;
            }
            
        }


        DBInsert dbInsert = new DBInsert();

        //This function resets the production and logs it. 
        public bool reset()
        {
            if(get_batch_id() != 0) dbInsert.addLog(get_batch_id(), "Reset");

            bool success = common_post("Cube.Command.CntrlCmd", 1);

            common_post("Cube.Command.CmdChangeRequest", true);

            return success;
        }

        //This function starts the production and logs it. 
        public bool start()
        {
            bool success = false;
            if (get_command_batch_id() != 0)
            {
                dbInsert.addLog(get_command_batch_id(), "Manual Start");

                success = common_post("Cube.Command.CntrlCmd", 2);

                common_post("Cube.Command.CmdChangeRequest", true);
            }

            return success;
        }

        //This function stops the production and logs it. 
        public bool stop()
        {
            bool success = common_post("Cube.Command.CntrlCmd", 3);

            common_post("Cube.Command.CmdChangeRequest", true);

            return success;
        }

        //This function aborts the production and logs it. 
        public bool abort()
        {
            dbInsert.addLog(get_batch_id(), "Manual Abort");

            bool success = common_post("Cube.Command.CntrlCmd", 4);

            common_post("Cube.Command.CmdChangeRequest", true);

            return success;
        }

        //This function clears the production and logs it. 
        public bool clear()
        {
            dbInsert.addLog(get_batch_id(), "Manual Clear");

            bool success = common_post("Cube.Command.CntrlCmd", 5);

            common_post("Cube.Command.CmdChangeRequest", true);

            return success;
        }

        public bool manual_stop()
        {
            dbInsert.addLog(get_batch_id(), "Manual Stop");

            bool success = stop();

            return success;
        }

        public bool stop_check(int stopCode)
        {
            bool success = false;

            if (stopCode == 10)
            {
                dbInsert.addLog(get_batch_id(), "Empty inventory");
                stop();
                success = true;
            }
            else if (stopCode == 11)
            {
                dbInsert.addLog(get_batch_id(), "Maintenance needed");
                stop();
                success = true;
            }
            else if (stopCode == 13)
            {
                dbInsert.addLog(get_batch_id(), "Motor power loss");
                stop();
                success = true;
            }
            else if (stopCode == 14)
            {
                dbInsert.addLog(get_batch_id(), "Manual abort");
                stop();
                success = true;
            }

            return success;
        }

        //This function is used on the completion of a batch in order to log the results. 
        public void logSuccess()
        {
            if (get_state() == 17)
            {
                dbInsert.addLog(get_batch_id(), "Total Beer", get_produced());

                dbInsert.addLog(get_batch_id(), "Successful Beer", get_produced_good());

                dbInsert.addLog(get_batch_id(), "Defective Beer", get_produced_bad());
            }
        }

        public float get_quantity()
        {
            var quantity_value = common_get("Cube.Status.Parameter[1].Value");
            return (float) quantity_value.Value;
        }

        public float get_beer_type()
        {
            var beer_type = common_get("Cube.Admin.Parameter[0].Value");
            return (float)beer_type.Value;
        }

        public int get_batch_id()
        {
            var batch_id = common_get("batch_id");
            return (UInt16)batch_id.Value;
        }

        public int get_command_batch_id()
        {
            var batch_id = common_get("Cube.Command.Parameter[0].Value");
            return Convert.ToUInt16(batch_id.Value);
        }
        public float get_machine_speed()
        {
            var machine_speed = common_get("Cube.Command.MachSpeed");
            return (float)machine_speed.Value;
        }

        public int get_state()
        {
            var state = common_get("Cube.Status.StateCurrent");
            return (int)state.Value;
        }
    }
}
