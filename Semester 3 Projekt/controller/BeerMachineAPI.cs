﻿using Opc.UaFx.Client;

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

        public int get_Stop_Reason()
        {
            var stop_reason = common_get("Cube.Admin.StopReason.ID");
            return (int) stop_reason.Value;
        }

        public void stop()
        {

            common_post("Cube.Command.PackMLCmd", 3);

            common_post("Cube.Command.CmdChangeRequest", true);
        }

        public string stop_Reasons()
        {
            var stop_reason = ""; 
            if (get_Stop_Reason() == 10)
            {
                //Empty inventory: 
                stop();
                stop_reason = "Empty inventory"; 
            }

            else if (get_Stop_Reason() == 11)
            {
                //Maintenance needed: 
                stop();
                stop_reason = "Maintenance needed";
            }

            else if (get_Stop_Reason() == 12)
            {
                //Manual stop: 
                stop();
                stop_reason = "Manual stop";
            }

            else if (get_Stop_Reason() == 13)
            {
                //Motor power loss: 
                stop();
                stop_reason = "Motor power loss";
            }

            else 
            {
                //Manual abort: 
                stop();
                stop_reason = "Manual abort";
            }

            return stop_reason;
        }

    }
}
