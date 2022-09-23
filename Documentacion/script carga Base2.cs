try
{
    string Delimitador = Dts.Variables["User::Delimitador"].Value.ToString();
    string Extension1 = Dts.Variables["User::Extension1"].Value.ToString();
    string Extension2 = Dts.Variables["User::Extension2"].Value.ToString();
    string FolderOrigen = Dts.Variables["User::FolderOrigen2"].Value.ToString();
    
    string borrarDatos1 = "TRUNCATE table temporalcomp_base1;";
    string borrarDatos2 = "TRUNCATE table temporalvent_base1;";

    // conexion a sql server

    SqlConnection myADONETConnection = new SqlConnection();
    myADONETConnection = (SqlConnection)(Dts.Connections["DESKTOP-F873KSL.semi2_base2"].AcquireConnection(Dts.Transaction) as SqlConnection);





    string[] fileEntries1 = Directory.GetFiles(FolderOrigen, "*" + Extension1); // archivos .comp
    string[] fileEntries2 = Directory.GetFiles(FolderOrigen, "*" + Extension2); // archivo .vent


    SqlCommand datos1 = new SqlCommand(borrarDatos1, myADONETConnection);
    datos1.ExecuteNonQuery();
    SqlCommand datos2 = new SqlCommand(borrarDatos2, myADONETConnection);
    datos2.ExecuteNonQuery();


    // ingreso a tabla COMPRAS 
    foreach (string fileName in fileEntries1)
    {
       
        int contador = 0;
        string linea;

        System.IO.StreamReader SourceFile = new System.IO.StreamReader(fileName);

        while ((linea = SourceFile.ReadLine()) != null)
        {
            if (contador > 0) //Con esto nos saltanos la fila de encabezados
            {
                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);
                string query = "INSERT INTO temporalcomp_base1"
                    + " VALUES ('"
                    + campos[0] + "','" + campos[1] + "','" + campos[2] + "', '"
                    + campos[3].Replace("\"", "") + "','" + campos[4] + "','" + campos[5] + "', '"
                    + campos[6] + "','" + campos[7] + "','" + campos[8] + "', '"
                    + campos[9] + "','" + campos[10] + "','" + campos[11] + "', '"
                    + campos[12].Replace("\"", "") + "','" + campos[13] + "','" + campos[14] + "', '"
                    + campos[15] + "','" + campos[16]
                    + "')";
                // Para SQL Server
                SqlCommand myCommand1 = new SqlCommand(query, myADONETConnection);
                myCommand1.ExecuteNonQuery();


            }
            contador++;
        }
        SourceFile.Close();
        Dts.TaskResult = (int)ScriptResults.Success;
    }

    // INGRESO A TABLA VENTAS

    foreach (string fileName in fileEntries2)
    {
        

        int contador = 0;
        string linea;

        System.IO.StreamReader SourceFile = new System.IO.StreamReader(fileName);

        while ((linea = SourceFile.ReadLine()) != null)
        {
            if (contador > 0) //Con esto nos saltanos la fila de encabezados
            {
                string[] campos = linea.Split(Delimitador.ToCharArray()[0]);
                string query = "INSERT INTO temporalvent_base1"
                    + " VALUES ('"
                    + campos[0] + "','" + campos[1] + "','" + campos[2] + "', '"
                    + campos[3] + "','" + campos[4].Replace("\"", "") + "','" + campos[5] + "', '"
                    + campos[6] + "','" + campos[7] + "','" + campos[8] + "', '"
                    + campos[9] + "','" + campos[10] + "','" + campos[11] + "', '"
                    + campos[12] + "','" + campos[13] + "','" + campos[14] + "', '"
                    + campos[15].Replace("\"", "") + "','" + campos[16] + "','" + campos[17] + "', '"
                    + campos[18] + "','" + campos[19]
                    + "')";
                // Para SQL Server
                SqlCommand myCommand2 = new SqlCommand(query, myADONETConnection);
                myCommand2.ExecuteNonQuery();


            }
            contador++;
        }
        SourceFile.Close();
        Dts.TaskResult = (int)ScriptResults.Success;
    }


}
catch (Exception ex)
{
    using (StreamWriter sw = File.CreateText(Dts.Variables["User::FolderError"].Value.ToString() + "\\" + "ErrorLog2.log"))
    {
        sw.WriteLine(ex.ToString());
        Dts.TaskResult = (int)ScriptResults.Failure;
    }
}


}