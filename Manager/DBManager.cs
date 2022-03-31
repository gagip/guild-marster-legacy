using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//References
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;

/// <summary>
/// sqlite3 DB 매니저
/// </summary>
public class DBManager : SingletonBase<DBManager>
{

    private string conn, sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    private IDataReader reader;


    public void OpenDB(string dbName)
    {
        string filePath = Application.dataPath + "/Plugins/" + dbName;
        //open db connection
        conn = "URI=file:" + filePath;
    }

    public void CloseDB()
    {
        if (reader != null)
        {
            reader.Close();
            reader = null;
        }
        if (dbcmd != null)
        {
            dbcmd.Dispose();
            dbcmd = null;
        }
        if (dbconn != null)
        {
            dbconn.Close();
            dbconn = null;
        }

    }

    public void Insert(string table, string[] col, ArrayList value)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "INSERT INTO " + table + " (";
            for (int i = 0; i < col.Length; i++)
                sqlQuery += (i == col.Length - 1) ? col[i] : col[i] + ", ";

            sqlQuery += ") values (";
            for (int i = 0; i < value.Count; i++)
                sqlQuery += (i == value.Count - 1) ? string.Format("'{0}'", value[i]) :
                                                        string.Format("'{0}', ", value[i]);

            sqlQuery += ")";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();

        }
        Debug.Log("Insert Done");
        CloseDB();
    }


    public ArrayList SelectById(string table, int id, string var)
    {
        ArrayList result = new ArrayList();
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("SELECT {0} FROM {1} WHERE id={2}", var, table, id);
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                // ���� �ʵ忡 ����
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    // INTEGER
                    if (reader.GetFieldType(i).Equals(typeof(System.Int64)))
                    {
                        result.Add(reader.GetInt64(i));
                    }
                    else if (reader.GetFieldType(i).Equals(typeof(System.Int32)))
                    {
                        result.Add(reader.GetInt32(i));
                    }
                    // TEXT
                    else if (reader.GetFieldType(i).Equals(typeof(System.String)))
                    {
                        result.Add(reader.GetString(i));
                    }
                }
            }
            return result;
        }

    }


    public int GetLength(string table)
    {
        int result = 0;
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("SELECT COUNT(*) FROM {0}", table);
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                result = reader.GetInt32(0);
            }
        }

        return result;
    }
}
