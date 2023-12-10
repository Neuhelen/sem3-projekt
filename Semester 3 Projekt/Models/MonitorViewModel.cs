using NuGet.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Semester_3_Projekt.Models
{
    public class MonitorViewModel
    {
        // Class for storing data for the monitor,
        // Serialize it into json, for easy parsing in html
        public int BatchID { get; set; }
        public string BeerType { get; set; } = "";
        public float Quantity { get; set; }
        public int ProducedProducts { get; set; }
        public int GoodProducts { get; set; }
        public int BadProducts { get; set; }
        public float SetSpeed { get; set; }
        public float CurSpeed { get; set; }
        public float BarleyAmount { get; set; }
        public float HopsAmount { get; set; }
        public float MaltAmount { get; set; }
        public float WheatAmount { get; set; }
        public float YeastAmount { get; set; }
        public int State { get; set; }
        public int StopReason { get; set; }

        public void clear_data()
        {
            this.BatchID = 0;
            this.BeerType = "";
            this.Quantity = 0;
            this.ProducedProducts = 0;
            this.GoodProducts = 0;
            this.BadProducts = 0;
            this.SetSpeed = 0;
            this.CurSpeed = 0;
            this.YeastAmount = 0;
            this.HopsAmount = 0;
            this.WheatAmount = 0;
            this.YeastAmount = 0;
            this.State = 0;
            this.StopReason = 0;
        }
    
}
}
