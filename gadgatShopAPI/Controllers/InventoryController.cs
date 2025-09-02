using gadgatShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json.Serialization;

namespace gadgatShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost]
        public ActionResult SaveInventory(InventoryRequestDto requestDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=localhost;Database=gadgetShop;User=sa;password=sa123"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "ap_SaveInventoryData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            command.Parameters.AddWithValue("@ProductId", requestDto.ProductId);
            command.Parameters.AddWithValue("@ProductName", requestDto.ProductName);
            command.Parameters.AddWithValue("@AvailableQty", requestDto.AvailableQty);
            command.Parameters.AddWithValue("@ReOrderPoint", requestDto.ReOrderPoint);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return Ok();
        }
        [HttpGet]
        public ActionResult GetInventory()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=localhost;Database=gadgetShop;User=sa;password=sa123"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetAllInventory",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            
            List<InventoryDto> response  = new List<InventoryDto>();

            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    InventoryDto inventoryDto = new InventoryDto();
                    inventoryDto.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                    inventoryDto.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                    inventoryDto.AvailableQty = Convert.ToInt32(sqlDataReader["AvailableQty"]);
                    inventoryDto.ReOrderPoint = Convert.ToInt32(sqlDataReader["ReOrderPoint"]);

                    response.Add(inventoryDto);
                }
            }

                connection.Close();

            return Ok(response);
        }

      [HttpDelete]
        public ActionResult DeleteInventory(int prodcutId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=localhost;Database=gadgetShop;User=sa;password=sa123"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteInventory",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();

            command.Parameters.AddWithValue("@ProductId", prodcutId);

            command.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }
        [HttpPut]
        public ActionResult UpdateInventory(InventoryRequestDto requestDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=localhost;Database=gadgetShop;User=sa;password=sa123"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateInventory",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();

            command.Parameters.AddWithValue("@ProductId", requestDto.ProductId);
            command.Parameters.AddWithValue("@ProductName", requestDto.ProductName);
            command.Parameters.AddWithValue("@AvailableQty", requestDto.AvailableQty);
            command.Parameters.AddWithValue("@ReOrderPoint", requestDto.ReOrderPoint);

            command.ExecuteNonQuery();

            connection.Close();

            return Ok();
        }

    }


}
