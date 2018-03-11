﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LackLusterVideo.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.MySqlClient;

namespace VideoStoreApi.Utils
{
    public class CustomerUtils
    {
        public bool MakeNewCustomer(Customer toAdd, ref string msg)
        {
            try
            {
                string createUserQuery;

                if (toAdd.AddLine2 == null)
                {
                    createUserQuery =
                        $"INSERT INTO {DatabaseUtils.Databasename}.customers(CUST_Name_First, CUST_Name_Middle_In, CUST_Name_Last, CUST_Add_Line1, CUST_Add_Line2, CUST_Add_City, CUST_Add_State, CUST_Add_Zip, CUST_PhoneNumber, CUST_Email, CUST_Newsletter, CUST_AccountBalance, CUST_Active ) " +
                        $"VALUES('{toAdd.NameFirst}', '{toAdd.NameMiddleIn}', '{toAdd.NameLast}', '{toAdd.AddLine1}', null , '{toAdd.AddCity}', '{toAdd.AddState}', '{toAdd.AddZip}', '{toAdd.PhoneNumber}', '{toAdd.Email}', {toAdd.Newsletter}, '{toAdd.AccountBalance}', {toAdd.Active});";
                }
                else
                {
                    createUserQuery =
                        $"INSERT INTO {DatabaseUtils.Databasename}.customers(CUST_Name_First, CUST_Name_Middle_In, CUST_Name_Last, CUST_Add_Line1, CUST_Add_Line2, CUST_Add_City, CUST_Add_State, CUST_Add_Zip, CUST_PhoneNumber, CUST_Email, CUST_Newsletter, CUST_AccountBalance, CUST_Active ) " +
                        $"VALUES('{toAdd.NameFirst}', '{toAdd.NameMiddleIn}', '{toAdd.NameLast}', '{toAdd.AddLine1}', '{toAdd.AddLine2}', '{toAdd.AddCity}', '{toAdd.AddState}', '{toAdd.AddZip}', '{toAdd.PhoneNumber}', '{toAdd.Email}', {toAdd.Newsletter}, '{toAdd.AccountBalance}', {toAdd.Active});";
                }

                DatabaseUtils createCustomer = DatabaseUtils.Instance();
                return createCustomer.MakeDbQuery(createUserQuery);
            }
            catch (Exception e)
            {
                msg = "An Exception was Thrown! " + e.ToString();
                return false;
            }
        }

        public Customer GetCustomerById(int id)
        {
            string getCustomerQuery = $"SELECT * " +
                                      $"FROM {DatabaseUtils.Databasename}.customers " +
                                      $"WHERE CUST_ID = {id};";

            return SqlGetCustomerById(getCustomerQuery);
        }

        public bool MakeCustomerInactive(int id, ref string msg)
        {
            try
            {
                string disableCustomerQuery = $"UPDATE {DatabaseUtils.Databasename}.customers " + 
                                              $"SET CUST_Active = 0 " + 
                                              $"WHERE CUST_ID = {id};";

                var updateEmployee = DatabaseUtils.Instance();
                return updateEmployee.MakeDbQuery(disableCustomerQuery);
            }
            catch (Exception e)
            {
                msg = "An Exception was thrown! " + e.ToString();
                return false;
            }
        }

        public bool UpdateCustomer(Customer updatedInfo, ref string msg)
        {
            try
            {
                string updateCustomerQuery;

            if (updatedInfo.AddLine2 == null)
            {
                updateCustomerQuery = $"UPDATE {DatabaseUtils.Databasename}.customers " +
                                      "SET " +
                                      $"CUST_Name_First = '{updatedInfo.NameFirst}', " +
                                      $"CUST_Name_Middle_In = '{updatedInfo.NameMiddleIn}', " +
                                      $"CUST_Name_Last = '{updatedInfo.NameLast}', " +
                                      $"CUST_Add_Line1 = '{updatedInfo.AddLine1}', " +
                                      $"CUST_Add_City = '{updatedInfo.AddCity}', " +
                                      $"CUST_Add_State = '{updatedInfo.AddState}', " +
                                      $"CUST_Add_Zip = '{updatedInfo.AddZip}', " +
                                      $"CUST_PhoneNumber = '{updatedInfo.PhoneNumber}', " +
                                      $"CUST_Email = '{updatedInfo.Email}', " +
                                      $"CUST_Newsletter = '{Convert.ToInt32(updatedInfo.Newsletter)}', " +
                                      $"CUST_AccountBalance = '{updatedInfo.AccountBalance}', " +
                                      $"CUST_Active = '{Convert.ToInt32(updatedInfo.Active)}' " +
                                      $"WHERE CUST_ID = '{updatedInfo.CustomerId}';";
            }
            else
            {
                updateCustomerQuery = $"UPDATE {DatabaseUtils.Databasename}.customers " +
                                      "SET " +
                                      $"CUST_Name_First = '{updatedInfo.NameFirst}', " +
                                      $"CUST_Name_Middle_In = '{updatedInfo.NameMiddleIn}', " +
                                      $"CUST_Name_Last = '{updatedInfo.NameLast}', " +
                                      $"CUST_Add_Line1 = '{updatedInfo.AddLine1}', " +
                                      $"CUST_Add_Line2 = '{updatedInfo.AddLine2}', " +
                                      $"CUST_Add_City = '{updatedInfo.AddCity}', " +
                                      $"CUST_Add_State = '{updatedInfo.AddState}', " +
                                      $"CUST_Add_Zip = '{updatedInfo.AddZip}', " +
                                      $"CUST_PhoneNumber = '{updatedInfo.PhoneNumber}', " +
                                      $"CUST_Email = '{updatedInfo.Email}', " +
                                      $"CUST_Newsletter = '{Convert.ToInt32(updatedInfo.Newsletter)}', " +
                                      $"CUST_AccountBalance = '{updatedInfo.AccountBalance}', " +
                                      $"CUST_Active = '{Convert.ToInt32(updatedInfo.Active)}' " +
                                      $"WHERE CUST_ID = '{updatedInfo.CustomerId}';";
            }

            var updateEmployee = DatabaseUtils.Instance();
            return updateEmployee.MakeDbQuery(updateCustomerQuery);
            }
            catch (Exception e)
            {
                msg = "An Exception Was Thrown! " + e.ToString();
                return false;
            }
            
        }


        private Customer SqlGetCustomerById(string dbQuery)
        {
            Customer temp = new Customer();
            var dbCon = DatabaseUtils.Instance();
            dbCon.DatabaseName = DatabaseUtils.Databasename;
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(dbQuery, dbCon.Connection);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp.CustomerId = reader.GetInt32($"CUST_ID");
                    temp.NameFirst = reader.GetString($"CUST_Name_First");
                    temp.NameMiddleIn = reader.GetString($"CUST_Name_Middle_In");
                    temp.NameLast = reader.GetString($"CUST_Name_Last");
                    temp.AddLine1 = reader.GetString($"CUST_Add_Line1");

                    int location = reader.GetOrdinal($"CUST_Add_Line2");
                    if (!reader.IsDBNull(location))
                    {
                        temp.AddLine2 = reader.GetString($"CUST_Add_Line2");
                    }

                    temp.AddCity = reader.GetString($"CUST_Add_City");
                    temp.AddState = reader.GetString($"CUST_Add_State");
                    temp.AddZip = reader.GetInt32($"CUST_Add_Zip");
                    temp.PhoneNumber = reader.GetInt64($"CUST_PhoneNumber");
                    temp.Email = reader.GetString($"CUST_Email");
                    temp.Newsletter = reader.GetBoolean($"CUST_Newsletter");
                    temp.AccountBalance = reader.GetInt32($"CUST_AccountBalance");
                    temp.Active = reader.GetBoolean($"CUST_Active");
                }

                dbCon.Close();
            }

            if (temp.CustomerId == 0)
                //No Match in the DB
                return null;
            else
                return temp;
        }
    }
}
