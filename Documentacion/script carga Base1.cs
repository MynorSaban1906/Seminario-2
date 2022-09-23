try
{
    string Delimitador = Dts.Variables["User::Delimitador"].Value.ToString();
    string Extension1 = Dts.Variables["User::Extension1"].Value.ToString();
    string Extension2 = Dts.Variables["User::Extension2"].Value.ToString();
    string FolderOrigen = Dts.Variables["User::FolderOrigen1"].Value.ToString();
    string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=root123;database=semi2_base1;";

    string[] fileEntries1 = Directory.GetFiles(FolderOrigen, "*" + Extension1); //Arreglo con todos los archivos .comp que se encuentran en la carpeta origen
    string[] fileEntries2 = Directory.GetFiles(FolderOrigen, "*" + Extension2); //Arreglo con todos los archivos .vent que se encuentran en la carpeta origen

    MySqlConnection myADONETConnection = new MySqlConnection(connectionString);

    // eliminando los datos de sql 
    string querydelete = "TRUNCATE TABLE temporalcomp_base1; TRUNCATE TABLE temporalvent_base1;";
    MySqlCommand myCommanddelete = new MySqlCommand(querydelete, myADONETConnection);
    myADONETConnection.Open();
    myCommanddelete.ExecuteReader();
    myADONETConnection.Close();

    // INGRESO DE DATA PARA LA TABLA COMPRA EN LA BASE 1
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
                    + campos[3].Replace("\"","") + "','" + campos[4] + "','" + campos[5] + "', '"
                    + campos[6] + "','" + campos[7] + "','" + campos[8] + "', '"
                    + campos[9] + "','" + campos[10] + "','" + campos[11] + "', '"
                    + campos[12].Replace("\"", "") + "','" + campos[13] + "','" + campos[14] + "', '"
                    + campos[15] + "','" + campos[16]
                    + "')";
                MySqlCommand myCommand = new MySqlCommand(query, myADONETConnection);
                myADONETConnection.Open();
                myCommand.ExecuteReader();
                myADONETConnection.Close();
            }
            contador++;

        }
        SourceFile.Close();
        Dts.TaskResult = (int)ScriptResults.Success;
    }

    // INGRESO DE DATA PARA LA TABLA VENTA EN LA BASE 1
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
                Console.Write(campos);
                string query = "INSERT INTO temporalvent_base1 "
                    + " VALUES ('"
                    + campos[0] + "','" + campos[1] + "','" + campos[2] + "', '"
                    + campos[3] + "','" + campos[4].Replace("\"", "") + "','" + campos[5] + "', '"
                    + campos[6] + "','" + campos[7] + "','" + campos[8] + "', '"
                    + campos[9] + "','" + campos[10] + "','" + campos[11] + "', '"
                    + campos[12] + "','" + campos[13] + "','" + campos[14] + "', '"
                    + campos[15].Replace("\"", "") + "','" + campos[16] + "','" + campos[17] + "', '"
                    + campos[18] + "','" + campos[19] 
                    + "')";
                MySqlCommand myCommand = new MySqlCommand(query, myADONETConnection);
                myADONETConnection.Open();
                myCommand.ExecuteReader();
                myADONETConnection.Close();
            }
            contador++;

        }
        SourceFile.Close();
        Dts.TaskResult = (int)ScriptResults.Success;
    }

}
catch (Exception ex)
{
    using (StreamWriter sw = File.CreateText(Dts.Variables["User::FolderError"].Value.ToString() + "\\" + "ErrorLog.log"))
    {
        sw.WriteLine(ex.ToString());
        Dts.TaskResult = (int)ScriptResults.Failure;
    }
}