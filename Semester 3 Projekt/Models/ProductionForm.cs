namespace Semester_3_Projekt.Models
{
	public class ProductionFormValues
	{
		public float MachineID { get; set; }
		public float quantityInput { get; set; }
		public float speedInput { get; set; }

		public ProductionFormValues() { }

		public ProductionFormValues(float MachineID,  float quantityInput, float speedInput)
		{
			this.speedInput = speedInput;
			this.MachineID = MachineID;
			this.quantityInput = quantityInput;
		}
	}
}
