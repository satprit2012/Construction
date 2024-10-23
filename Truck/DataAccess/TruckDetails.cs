using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace Truck.DataAccess
{
    public class TruckDetails
    {
        string connStr = ConfigurationManager.ConnectionStrings["TruckConn"].ConnectionString.ToString();
        SqlConnection cn;
        SqlCommand cmd;
      
        public TruckDetails()
        {
            cn = new SqlConnection(connStr);            
        }
        /// <summary>
        /// Get detials of all the truck
        /// </summary>
        /// <returns></returns>
        public DataTable GetTrucks()
        {
            DataSet ds;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("GetTruckDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                ds = new DataSet();
                adapter.Fill(ds);
                
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// Update Truck details 
        /// </summary>
        /// <param name="truck">code,status,name and description of the truck</param>
        public void UpdateTruckDetails(Model.Truck truck)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "UpdateTruckDetails",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", truck.Id);
                cmd.Parameters.AddWithValue("@Code", truck.Code);
                cmd.Parameters.AddWithValue("@Name", truck.Name);
                cmd.Parameters.AddWithValue("@Status", truck.StatusId);
                cmd.Parameters.AddWithValue("@Description", truck.Description);
               
                cmd.ExecuteNonQuery();
            }
        }

        public int AddTruckDetails(Model.Truck truck)
        {
            int newId = 0;
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "AddTruckDetails",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Code", truck.Code);
                cmd.Parameters.AddWithValue("@Name", truck.Name);
                cmd.Parameters.AddWithValue("@Status", truck.StatusId);
                cmd.Parameters.AddWithValue("@Description", truck.Description);
                SqlParameter outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);
                cmd.ExecuteNonQuery();
                newId = (int)outputIdParam.Value;
            }
            return newId;
        }
        /// <summary>
        /// Delete truck details
        /// </summary>
        /// <param name="truckId"></param>
        public void DeleteTruck(int truckId) 
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand()
                {
                    CommandText = "DeleteTruckDetails",
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", truckId);
                cmd.ExecuteNonQuery();
             }
        }
    }
}